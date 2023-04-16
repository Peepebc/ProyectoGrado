using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGrado.Models;
using System.Security.Claims;

namespace ProyectoGrado.Controllers
{

    public class ContieneLista
    {
        public int IdPelicula { get; set; }
        public int IdLista { get; set; }
    }

    [Route("Contiene")]
    [ApiController]
    [Authorize]
    public class ContieneController : ControllerBase
    {

        private readonly PeliculasContext _peliculasContext;

        public ContieneController(PeliculasContext peliculasContext)
        {
            _peliculasContext = peliculasContext;
        }

        [HttpPost]
        [Route("AnadirPeliculaLista")]
        public async Task<ActionResult<string>> AnadirFavorito(ContieneLista p)
        {

            Contiene c = new Contiene();
            c.IdPelicula = p.IdPelicula;
            c.IdLista = p.IdLista;
            await _peliculasContext.Contiene.AddAsync(c);
            _peliculasContext.SaveChanges();
            return "Pelicula agregada correctamente";

        }

        [HttpDelete]
        [Route("AnadirPeliculaLista")]
        public async Task<ActionResult<string>> EliminarFavorito(ContieneLista p)
        {
            var contiene = await _peliculasContext.Contiene.Where(e => e.IdPelicula == p.IdPelicula && e.IdLista == p.IdLista).FirstOrDefaultAsync();
            if (contiene == null) return "Error";
            _peliculasContext.Remove<Contiene>(contiene);
            _peliculasContext.SaveChanges();
            return "Favorito eliminado";
        }
    }
}
