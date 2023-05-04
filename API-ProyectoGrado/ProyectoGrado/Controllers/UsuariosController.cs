using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoGrado.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Bytewizer.Backblaze.Client;

namespace ProyectoGrado.Controllers
{

    public class LoginModel
    {
        public string usuario { get; set; }
        public string password { get; set; }
    }

    public class RegisterModel
    {
        public string usuario { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public DateTime fechaNac { get; set; }
        public IFormFile Imagen { get; set; }

    }


    [Route("Usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly PeliculasContext _peliculasContext;
        private readonly IConfiguration _config;
        private static IStorageClient _storage;
        private Usuario user;

        public UsuariosController(PeliculasContext peliculasContext,IConfiguration config)
        {
            _peliculasContext = peliculasContext;
            _config = config;
        }

         

        [HttpPost]
        [Route("Register")]
        public async Task<dynamic> RegisterUsuario([FromForm] RegisterModel r)
        {
            if (_peliculasContext.Usuarios.Where(e => e.User == r.usuario).Count() > 0) return "El usuario ya existe";

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: r.password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            _storage = new BackblazeClient();
            _storage.Connect("f8203b393185", "005b131b73818cbe375cc17c0d51253c2a1412d068");
            var result = await _storage.UploadAsync("ffd80230f30bc39983710815", "Pfps/" + r.usuario+".jpg", r.Imagen.OpenReadStream());

            Usuario u = new Usuario();

            u.Nombre = r.nombre;
            u.User = r.usuario;
            u.PasswordHash = hashed;
            u.PasswordSalt = Convert.ToBase64String(salt);
            u.Email = r.email;
            u.FechaNac = r.fechaNac;
            u.Rol = 0;
            u.Apellidos = r.apellidos;
            u.Imagen = "https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Pfps/" + r.usuario+".jpg"; ;

            _peliculasContext.Usuarios.Add(u);
            _peliculasContext.SaveChanges();
            return new
            {
                succes = true,
            };

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> LoginUsuario(LoginModel l)
        {

            var u = await _peliculasContext.Usuarios.Where(e => e.User == l.usuario).FirstOrDefaultAsync();

            if (u == null)
            {
                return BadRequest("Login fail");

            }

            byte[] salt = Convert.FromBase64String(u.PasswordSalt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: l.password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));


            if (u.PasswordHash.Equals(hashed))
            {
                string token = CreateToken(u);
                return new { 
                    success= true,
                    id = u.Id,
                    jwt = token };
            }

            return BadRequest("Login fail");
        }

        [HttpGet]
        [Route("Validame")]
        [Authorize]

        public async Task<ActionResult<dynamic>> Validame()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var u = await _peliculasContext.Usuarios.Where(e => e.Id == id).FirstOrDefaultAsync();
            return new
            {
                u.Id,
                u.Rol,
                u.User,
                u.Imagen
            };
        }

        private string CreateToken(Usuario usuario) 
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims : claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);  

            return jwt;
        }
    }
}
