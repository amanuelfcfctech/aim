CREATE PROCEDURE [dbo].[usp_ExportPlantGroup_UPD_AIMOwnerLocationID]			  
			 @ExportPlantGroupID				INT			
			, @AIMOwnerLocationID				INT			= NULL			
			, @CFCUserID						INT			= NULL			
			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		dbo.ExportPlantGroup  WITH(ROWLOCK)
			SET 
				 AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, IsActive)														
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
				, UpdateStamp = GETDATE()								
					
			WHERE 
				ExportPlantGroupID = @ExportPlantGroupID

		SET @RecordID = @@IDENTITY 
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END