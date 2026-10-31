

CREATE PROCEDURE [dbo].[usp_CFCPlant_INS]
			  @CFCPlantCode			VARCHAR(10)
			, @CFCPlantName			VARCHAR(30)
			, @CFCUserID			INT
			, @AIMDatabaseID		INT

AS
BEGIN
SET NOCOUNT ON;

INSERT
	dbo.CFCPlant 
		(
		  CFCPlantCode
		, CFCPlantName
		, UpdateStamp
		, CFCUserID
		, AIMDatabaseID
		)
VALUES
		(
		  @CFCPlantCode
		, @CFCPlantName
		, GETDATE()
		, @CFCUserID
		, @AIMDatabaseID
		)

END
