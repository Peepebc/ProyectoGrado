using Bytewizer.Backblaze.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGrado.Models;

namespace ProyectoGrado.Controllers
{

    public class PeliculaForm
    {
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Director { get; set; }
        public string? Descripcion { get; set; }
        public IFormFile Imagen { get; set; }
        public string? Generos { get; set; }
        public float? Puntuacion { get; set; }
}

    [Route("Peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {

        private readonly PeliculasContext _peliculasContext;
        private static IStorageClient _storage;


        public PeliculasController(PeliculasContext peliculasContext)
        {
            _peliculasContext = peliculasContext;
        }

        [HttpGet]
        [Route("UltimasPeliculas")]

        public dynamic UltimasPeliculas()
        {
            return  _peliculasContext.Peliculas.Select(p => new { p.Id, p.Imagen }).OrderByDescending(p => p.Id).Take(7).ToList();
        }

        [HttpPost]
        [Route("UltimasVistasUsuario/{id}")]

        public dynamic UltimasPeliculasVistas(int id)
        {
            return  _peliculasContext.Peliculas.Join(_peliculasContext.Vistas, p=> p.Id, v=>v.IdPelicula, (p,v)=>new { p,v}).Where(x=> x.v.IdUsuario==id).OrderByDescending(pel => pel.v.Id).Select(pel => new { pel.p.Id,pel.p.Imagen }).Take(7).ToList();
        }

        //TODO TODAS LAS VISTAS DEL USUARIO

        [HttpGet]
        [Route("TodasistasUsuario/{id}")]

        public dynamic TodasistasUsuario(int id)
        {
            return _peliculasContext.Peliculas.Join(_peliculasContext.Vistas, p => p.Id, v => v.IdPelicula, (p, v) => new { p, v }).Where(x => x.v.IdUsuario == id).OrderByDescending(pel => pel.v.Id).Select(pel => new { pel.p.Id, pel.p.Imagen }).ToList();
        }


        [HttpPost]
        [Route("UltimasFavoritasUsuario/{id}")]

        public dynamic UltimasPeliculasFavoritas(int id)
        {
            return  _peliculasContext.Peliculas.Join(_peliculasContext.Favoritos, p => p.Id, f => f.IdPelicula, (p, f) => new { p, f }).Where(x => x.f.IdUsuario == id).OrderByDescending(pel => pel.f.Id).Select(pel => new { pel.p.Id, pel.p.Imagen }).Take(7).ToList();
        }

        //TODO TODAS LAS FAVORITAS DEL USUARIO

        [HttpGet]
        [Route("TodasFavoritasUsuario/{id}")]

        public dynamic TodasFavoritasUsuario(int id)
        {
            return _peliculasContext.Peliculas.Join(_peliculasContext.Favoritos, p => p.Id, f => f.IdPelicula, (p, f) => new { p, f }).Where(x => x.f.IdUsuario == id).OrderByDescending(pel => pel.f.Id).Select(pel => new { pel.p.Id, pel.p.Imagen }).ToList();
        }

        [HttpGet]
        [Route("/{id}")]

        public dynamic Pelicula(int id)
        {
            var pelicula =  _peliculasContext.Peliculas.Where(p => p.Id == id).FirstOrDefault();
            if (pelicula == null) return Ok(false);
            return pelicula;
        }

        [HttpPost]
        [Route("AnadirPelicula")]
        public async Task<dynamic> AnadirPelicula([FromForm] PeliculaForm p)
        {

            _storage = new BackblazeClient();
            _storage.Connect("f8203b393185", "005b131b73818cbe375cc17c0d51253c2a1412d068");
            var result = await _storage.UploadAsync("ffd80230f30bc39983710815","Peliculas/"+ p.Titulo + "-" + p.Fecha+".jpg", p.Imagen.OpenReadStream());
            
            Pelicula peli = new Pelicula();

            peli.Titulo = p.Titulo;
            peli.Fecha = p.Fecha;
            peli.Director = p.Director;
            peli.Descripcion = p.Descripcion;
            peli.Puntuacion = p.Puntuacion;
            peli.Generos = p.Generos;
            peli.Imagen = "https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/" + p.Titulo+"-"+p.Fecha + ".jpg";

            _peliculasContext.Peliculas.Add(peli);
            _peliculasContext.SaveChanges();
            return new
            {
                succes = true,
            };

        }

        [HttpDelete]
        [Route("EliminarPelicula/{id}")]
        [Authorize(Roles = "1")]
        public dynamic EliminarPelicula(int id)
        {
            var p = _peliculasContext.Peliculas.Where(_ => _.Id == id).FirstOrDefault();
            if (p == null) return Ok(false);
            _peliculasContext.Peliculas.Remove(p);
            _peliculasContext.SaveChanges();
            return new
            {
                succes = true,
            };

        }
        //TODO TODAS LAS PELICULAS DE UNA LISTA
    }
}
