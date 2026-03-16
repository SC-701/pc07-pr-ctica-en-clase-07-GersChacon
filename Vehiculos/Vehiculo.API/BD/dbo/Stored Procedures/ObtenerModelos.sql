-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Obtiene todos los registros de la tabla Modelos, incluyendo el nombre de la marca correspondiente.
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerModelos]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT [Modelos].[Id]
		  ,[Modelos].[IdMarca]
		  ,[Modelos].[Nombre]
		  ,[Marcas].[Nombre] AS [NombreMarca]  -- Mismo alias que en ObtenerModelo
	  FROM [dbo].[Modelos]
	  INNER JOIN [dbo].[Marcas] ON [Modelos].[IdMarca] = [Marcas].[Id]
	ORDER BY [Marcas].[Nombre], [Modelos].[Nombre];
END