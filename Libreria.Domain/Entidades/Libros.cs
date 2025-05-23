using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Domain.Entidades
{
    public class Libros
    {
        [Key]
        public int LibroId { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int AnoPublicacion { get; set; }
        public string Genero { get; set; }
        public Autores Autor { get; set; }
        public ICollection<Prestamos> Prestamos { get; set; }
    }
}
