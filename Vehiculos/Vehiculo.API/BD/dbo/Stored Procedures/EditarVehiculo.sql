-- =============================================
-- Author:		@Author,,Name>
-- Create date: @Create Date,,>
-- Description:	@Description,,>
-- =============================================
CREATE PROCEDURE EditarVehiculo
	-- Add the parameters for the stored procedure here
		@Id as uniqueidentifier
		,@IdModelo as uniqueidentifier
		,@Placa as varchar(max)
		,@Color as varchar(max)
		,@Anio as int
		,@Precio as decimal(18,0)
		,@CorreoPropietario as varchar(max)
		,@TelefonoPropietario as varchar(max)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Begin TRANSACTION
	UPDATE [dbo].[Vehiculo]
	   SET [IdModelo] = @IdModelo 
		  ,[Placa] = @Placa
		  ,[Color] = @Color 
		  ,[Anio] = @Anio
		  ,[Precio] = @Precio
		  ,[CorreoPropietario] = @CorreoPropietario 
		  ,[TelefonoPropietario] = @TelefonoPropietario 
	 WHERE Id=@Id
	 Select @Id
	 COMMIT TRANSACTION

	END