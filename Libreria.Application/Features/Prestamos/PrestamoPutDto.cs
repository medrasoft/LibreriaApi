using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Prestamos
{
    public  class PrestamoPutDto
    {
        public int LibroId { get; set; }
        public DateTime? FechaDevolucion { get; set; }
    }
}
