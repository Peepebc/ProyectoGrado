using Microsoft.EntityFrameworkCore;
using ProyectoGrado.Models;

namespace ProyectoGrado
{
    public class PeliculasContext : DbContext
    {

        public PeliculasContext(DbContextOptions<PeliculasContext> options) 
             :base (options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Contiene> Contiene { get; set; }
        public DbSet<Fav> Favoritos { get; set; }
        public DbSet<Ver> Vistas { get; set; }
        public DbSet<Resena> Resenas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Pelicula>().ToTable("Pelicula");
            modelBuilder.Entity<Lista>().ToTable("Lista");
            modelBuilder.Entity<Contiene>().ToTable("Contiene");
            modelBuilder.Entity<Fav>().ToTable("Fav");
            modelBuilder.Entity<Ver>().ToTable("Ver");
            modelBuilder.Entity<Resena>().ToTable("Resena");
        }
    }
}
