CREATE DATABASE BibliotecaDB;
GO

USE BibliotecaDB;
GO

CREATE TABLE Autores (
    AutorId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Nacionalidad NVARCHAR(50) NOT NULL
);

CREATE TABLE Libros (
    LibroId INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(150) NOT NULL,
    AutorId INT NOT NULL,
    AnoPublicacion INT NOT NULL,
    Genero NVARCHAR(50),
    FOREIGN KEY (AutorId) REFERENCES Autores(AutorId)
);

CREATE TABLE Prestamos (
    PrestamoId INT PRIMARY KEY IDENTITY(1,1),
    LibroId INT NOT NULL,
    FechaPrestamo DATE NOT NULL,
    FechaDevolucion DATE,
    FOREIGN KEY (LibroId) REFERENCES Libros(LibroId)
);

CREATE TABLE Usuarios (
    UsuarioId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL
);

-- Insertar usuarios de prueba (contraseñas: admin / usuario)
INSERT INTO Usuarios (Username, PasswordHash, Rol)
VALUES 
('admin', '$2a$11$2XfpC33N5T9QHKYptldh2uvPoTZqx3d4JY8ObxD4oYIbLDxdrksme', 'Admin'),
('usuario', '$2a$11$O3qE4P1s3HbqKmmPfPFdRe1o.m8jPb7j1S4oLqx4hzypmFYs6FJmC', 'Usuario');



--Consulta de prueba sin índices

SELECT a.AutorId, a.nombre, l.LibroId, l.titulo
FROM prestamos p
JOIN libros l ON p.LibroId = l.LibroId
JOIN autores a ON l.AutorId = a.AutorId
WHERE p.FechaDevolucion IS NULL;

--Índice en prestamos.fecha_devolucion
CREATE INDEX idx_prestamos_fecha_devolucion
ON prestamos(FechaDevolucion);
