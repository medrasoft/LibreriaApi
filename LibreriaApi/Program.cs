
using Microsoft.EntityFrameworkCore;
using System;
using Libreria.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Libreria.Application.Common.Mappings;
using Libreria.Domain.Interfaces;
using Libreria.Infrastructure;
using MediatR;
using Libreria.Application.Features.Libros.Commands;
using FluentValidation;
using Libreria.Application.Features.Libros.Validators;
using FluentValidation.AspNetCore;
using Libreria.Application.Features.Autores.Commands;
using Libreria.Application.Features.Autores.Validators;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using Microsoft.OpenApi.Models;
using Libreria.Infrastructure.Repository;
using Libreria.Infrastructure.Services;
using Libreria.Application.Features.Usuario.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibreriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
builder.Services.AddScoped<IMediator , Mediator>();
builder.Services.AddScoped<IUsuarioRepository , UsuarioRepository>();
builder.Services.AddScoped<IJwtService,JwtService>();

builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(CrearAutorCommand).Assembly));

//builder.Services.AddValidatorsFromAssembly(typeof(AutorValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(RegistrarUsuarioValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();




// Implementacion de JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["JwtSettings:Key"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false ,
            ValidateAudience = false ,
            ValidateLifetime = true ,
            ValidateIssuerSigningKey = true ,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });
builder.Services.AddAuthorization();

// Agregando a Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer" , new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header ,
        Description = "Ingresa: Bearer {token}" ,
        Name = "Authorization" ,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


// End


//Configurar AutoMapper Manualmente
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile<MappingProfile>();
});

// Registrar como  singleton
IMapper mapper=mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
