using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Prestamos
{
    public class PrestamosNoDevueltosDto
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public int LibroId { get; set; }
        public string Titulo { get; set; }
    }
}
