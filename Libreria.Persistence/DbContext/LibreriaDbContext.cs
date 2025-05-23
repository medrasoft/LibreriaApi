using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Persistence
{
    public class LibreriaDbContext : DbContext
    {
        public LibreriaDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Autores> Autores { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prestamos>()
                        .HasOne(p => p.Libros)
                        .WithMany(l => l.Prestamos)
                        .HasForeignKey(p => p.LibroId)
                        .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
