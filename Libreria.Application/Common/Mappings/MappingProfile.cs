using AutoMapper;
using Libreria.Application.Features.Libros;
using Libreria.Application.Features.Autores;
using Libreria.Application.Features.Prestamos;
using Libreria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Common.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile() 
        {
            CreateMap<Libros, LibroDto>().ReverseMap();
            CreateMap<Autores,AutorDto>().ReverseMap();
            CreateMap<Prestamos, PrestamosDto>().ReverseMap();
            CreateMap<Prestamos , PrestamosNoDevueltosDto>().ReverseMap();
            CreateMap<Prestamos , PrestamoPutDto>().ReverseMap();
        }
    }
}
