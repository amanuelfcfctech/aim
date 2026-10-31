CREATE PROCEDURE  [dbo].[usp_ExportPlant_INS_ExportPlantGroupID_NewGroupID]
			@ExportPlantGroupID		INT
			, @NewGroupID					INT
			, @CFCUserID					INT
	

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0


	BEGIN
		INSERT INTO
			dbo.ExportPlant
				(ExportPlantGroupID
				, CFCPlantID						
				, UpdateStamp
				, IsActive
				, CFCUserID)
		SELECT 			
				@NewGroupID
				, CFCPlantID				
				, GETDATE()
				, IsActive
				, @CFCUserID		
		FROM
			dbo.ExportPlant

		WHERE ExportPlantGroupID = @ExportPlantGroupID		
	END
		

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END