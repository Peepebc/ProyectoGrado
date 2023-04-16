using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProyectoGrado.Models;
using System.Security.Claims;

namespace ProyectoGrado.Controllers
{

    [Route("Resenas")]
    [ApiController]
    public class ResenasController : ControllerBase
    {

        private readonly PeliculasContext _peliculasContext;

        public ResenasController(PeliculasContext peliculasContext)
        {
            _peliculasContext = peliculasContext;
        }

        [HttpPost]
        [Route("AnadirResena/{id}")]
        [Authorize]
        public dynamic AnadirResena(int id, [FromBody] Resena resena)
        {
            Resena r = new Resena();
            r.IdPelicula = id;
            r.Descripcion = resena.Descripcion;
            r.Valoracion = resena.Valoracion;
            r.IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _peliculasContext.Resenas.Add(r);
            _peliculasContext.SaveChanges();
            return new
            {
                succes = true,
            };

        }

        [HttpPost]
        [Route("/{id}")]

        public  dynamic ResenasPelicula(int id)
        {
            return  _peliculasContext.Resenas.Where(r => r.IdPelicula == id).Select(r => new { r.IdUsuario, r.Usuario.Imagen, r.Usuario.User, r.Valoracion, r.Descripcion }).ToList();
        }

        [HttpGet]
        [Route("/UltimasResenas")]

        public  dynamic UltimasResenas()
        {
            return  _peliculasContext.Resenas.OrderByDescending(r=> r.Id).Select(r => new { r.IdUsuario, r.Usuario.User, r.Usuario.Imagen, r.Valoracion, r.Descripcion, r.IdPelicula, Poster=r.Pelicula.Imagen }).Take(10).ToList();
        }

        [HttpPost]
        [Route("UltimasResenasUsuario/{id}")]

        public  dynamic UltimasResenasUsuario(int id)
        {
            return  _peliculasContext.Resenas.Where(x => x.IdUsuario == id).OrderByDescending(r => r.Id).Select(r => new {r.Valoracion, r.Descripcion, r.Pelicula.Imagen, r.Pelicula.Titulo }).Take(4).ToList();
        }

        [HttpGet]
        [Route("TodasResenasUsuario/{id}")]

        public  dynamic TodasResenasUsuario(int id)
        {
            return  _peliculasContext.Resenas.Where(x => x.IdUsuario == id).OrderByDescending(r => r.Id).Select(r => new { r.Valoracion, r.Descripcion, r.Pelicula.Imagen, r.Pelicula.Titulo }).ToList();
        }

    }
}
