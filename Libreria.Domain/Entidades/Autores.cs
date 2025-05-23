using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Domain.Entidades
{
    public class Autores
    {
        [Key]
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }

        public ICollection<Libros> Libros { get; set; }
    }
}
