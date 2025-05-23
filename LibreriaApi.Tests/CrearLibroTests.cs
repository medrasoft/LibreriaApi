using AutoMapper;
using Libreria.Application.Features.Libros.Commands;
using Libreria.Application.Features.Libros;
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
    public class CrearLibroTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly CrearLibroHandler _handler;

        public CrearLibroTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LibroDto , Libros>();
            });
            _mapper = config.CreateMapper();

            _handler = new CrearLibroHandler(_unitOfWorkMock.Object , _mapper);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsNewId()
        {
            // Arrange
            var dto = new LibroDto { Titulo = "Nuevo libro" , AutorId = 1 , AnoPublicacion = 1995 , Genero = "Novela" };
            var command = new CrearLibroCommand(dto);

            _unitOfWorkMock.Setup(u => u.Libros.AddAsync(It.IsAny<Libros>()))
                .Callback<Libros>(l => l.LibroId = 123)
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command , CancellationToken.None);

            // Assert
            Assert.Equal(123 , result);
        }
    }

}
