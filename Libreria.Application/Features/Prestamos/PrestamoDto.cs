using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Prestamos
{
    public class PrestamoDto
    {
        public int LibroId { get; set; }
        public DateTime FechaPrestamo { get; set; } = DateTime.UtcNow;
        

    }
}
