-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Obtiene todos los registros de la tabla Marcas
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerMarcas]
	-- No se necesitan parámetros para obtener todas las marcas
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT [Id]
		  ,[Nombre]
	  FROM [dbo].[Marcas]
	ORDER BY [Nombre] ASC; -- Opcional: Ordenar por nombre para mejor visualización

END