using AutoMapper;
using Libreria.Application.Features.Libros;
using Libreria.Application.Features.Libros.Queries;
using Libreria.Domain.Entidades;
using Libreria.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaApi.Tests
{
    public class GetLibrosAntes2000Tests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly GetAllLibroHandler _handler;

        public GetLibrosAntes2000Tests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Libros , LibroDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetAllLibroHandler(_unitOfWorkMock.Object , _mapper);
        }

        [Fact]
        public async Task Handle_ReturnsOnlyLibrosBefore2000()
        {
            // Arrange
            var libros = new List<Libros>
        {
            new() { LibroId = 1, Titulo = "Viejo", AnoPublicacion = 1990 },
            new() { LibroId = 2, Titulo = "Reciente", AnoPublicacion = 2005 }
        };

            _unitOfWorkMock.Setup(u => u.Libros.GetLibrosAntesDe2000Async())
                .ReturnsAsync(libros.Where(l => l.AnoPublicacion < 2000).ToList());

            // Act
            var result = await _handler.Handle(new GetLibrosAntesDe2000Async() , CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Equal("Viejo" , result[0].Titulo);
        }
    }

}
