-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Obtiene un registro específico de la tabla Modelos por su ID, incluyendo el nombre de la marca
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerModelo]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT [Modelos].[Id]
		  ,[Modelos].[IdMarca]
		  ,[Modelos].[Nombre]
		  ,[Marcas].[Nombre] AS [NombreMarca]  -- Importante: el alias debe coincidir con la propiedad en C#
	  FROM [dbo].[Modelos]
	  INNER JOIN [dbo].[Marcas] ON [Modelos].[IdMarca] = [Marcas].[Id]
	WHERE [Modelos].[Id] = @Id
END