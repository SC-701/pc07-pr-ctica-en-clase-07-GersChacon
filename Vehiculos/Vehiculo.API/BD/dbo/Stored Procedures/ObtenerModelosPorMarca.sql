CREATE PROCEDURE ObtenerModelosPorMarca
    @IdMarca UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        m.Id,
        m.Nombre,
        m.IdMarca,
        ma.Nombre AS NombreMarca
    FROM Modelos m
    INNER JOIN Marcas ma ON m.IdMarca = ma.Id
    WHERE m.IdMarca = @IdMarca
    ORDER BY m.Nombre;
END