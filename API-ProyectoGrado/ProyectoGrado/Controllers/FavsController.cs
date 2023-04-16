using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGrado.Models;
using System.Security.Claims;

namespace ProyectoGrado.Controllers
{

    [Route("Favs")]
    [ApiController]
    [Authorize]
    public class FavsController : ControllerBase
    {

        private readonly PeliculasContext _peliculasContext;

        public FavsController(PeliculasContext peliculasContext)
        {
            _peliculasContext = peliculasContext;
        }

        [HttpPost]
        [Route("AnadirFav/{id}")]
        public async Task<ActionResult<string>> AnadirFavorito(int id)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Fav f = new Fav();
            f.IdPelicula = id;
            f.IdUsuario = userId;
            await _peliculasContext.Favoritos.AddAsync(f);
            _peliculasContext.SaveChanges();
            return "Favorito agregado correctamente";

        }

        [HttpDelete]
        [Route("EliminarFav/{id}")]
        public async Task<ActionResult<string>> EliminarFavorito(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var fav = await _peliculasContext.Favoritos.Where(e => e.IdPelicula == id && e.IdUsuario == userId).FirstOrDefaultAsync();
            if (fav == null) return "Error";
            _peliculasContext.Remove<Fav>(fav);
            _peliculasContext.SaveChanges();
            return "Favorito eliminado";
        }
    }
}
