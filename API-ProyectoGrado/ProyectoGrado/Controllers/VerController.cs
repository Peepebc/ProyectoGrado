﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGrado.Models;
using System.Security.Claims;

namespace ProyectoGrado.Controllers
{

    [Route("Ver")]
    [ApiController]
    [Authorize]
    public class VerController : ControllerBase
    {

        private readonly PeliculasContext _peliculasContext;

        public VerController(PeliculasContext peliculasContext)
        {
            _peliculasContext = peliculasContext;
        }

        [HttpPost]
        [Route("AnadirVer/{id}")]
        public async Task<ActionResult<string>> AnadirVer(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Ver v = new Ver();
            v.IdPelicula = id;
            v.IdUsuario = userId;
            await _peliculasContext.Vistas.AddAsync(v);
            _peliculasContext.SaveChanges();
            return "Vista agregado correctamente";
        }

        [HttpDelete]
        [Route("EliminarVer/{id}")]
        public async Task<ActionResult<string>> EliminarVer(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var ver = await _peliculasContext.Vistas.Where(e => e.IdPelicula == id && e.IdUsuario == userId).FirstOrDefaultAsync();
            if (ver == null) return "Error";
            _peliculasContext.Remove<Ver>(ver);
            _peliculasContext.SaveChanges();
            return "Vista eliminado";
        }
    }
}
