CREATE PROCEDURE [licensing].[usp_Config_SAV_CompanyName]
	  @ConfigID						INT	
	, @CompanyName					VARCHAR(40)
		
AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	licensing.Config WITH(ROWLOCK)
SET  
	CompanyName = @CompanyName
WHERE
	ConfigID = @ConfigID

END