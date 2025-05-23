using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string nombre , object key): base($"Entity \"{nombre}\" ({key}) no fue encontrado") { }
    }
}
