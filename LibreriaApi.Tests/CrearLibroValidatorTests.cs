using Libreria.Application.Features.Libros.Commands;
using Libreria.Application.Features.Libros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.Application.Features.Libros.Commands;

namespace LibreriaApi.Tests
{
    public class CrearLibroValidatorTests
    {
        [Fact]
        public void Validator_InvalidData_ReturnsErrors()
        {
            var validator = new CrearLibroValidator();

            var result = validator.Validate(new CrearLibroCommand(new LibroDto
            {
                Titulo = "" , // inválido
                AutorId = 0 , // inválido
                AnoPublicacion = 1400 ,
                Genero = ""
            }));

            Assert.False(result.IsValid);
            Assert.Equal(4 , result.Errors.Count);
        }
    }

}
