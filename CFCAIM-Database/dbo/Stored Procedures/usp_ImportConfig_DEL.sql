create PROCEDURE [dbo].[usp_ImportConfig_DEL]
				@ImportConfigID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.ImportConfig WITH(ROWLOCK)
WHERE
	ImportConfigID = @ImportConfigID

END
