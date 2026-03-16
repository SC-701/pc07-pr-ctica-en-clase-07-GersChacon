-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Obtiene un registro específico de la tabla Marcas por su ID
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerMarca]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT [Id]
		  ,[Nombre]
	  FROM [dbo].[Marcas]
	WHERE [Id] = @Id

END