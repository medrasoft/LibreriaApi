--Consulta de prueba sin índices

SELECT a.AutorId, a.nombre, l.LibroId, l.titulo
FROM prestamos p
JOIN libros l ON p.LibroId = l.LibroId
JOIN autores a ON l.AutorId = a.AutorId
WHERE p.FechaDevolucion IS NULL;

--Índice en prestamos.fecha_devolucion
CREATE INDEX idx_prestamos_fecha_devolucion
ON prestamos(FechaDevolucion);





