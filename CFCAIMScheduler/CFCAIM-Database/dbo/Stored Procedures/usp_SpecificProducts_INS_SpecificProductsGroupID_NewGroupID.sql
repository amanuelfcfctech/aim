CREATE PROCEDURE  [dbo].[usp_SpecificProducts_INS_SpecificProductsGroupID_NewGroupID]
			@SpecificProductsGroupID		INT
			, @NewGroupID					INT
			, @CFCUserID					INT
	
	
-- exec usp_SpecificProducts_INS_SpecificProductsGroupID_NewGroupID 4, 10, 1
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0


	BEGIN
		INSERT INTO
			dbo.SpecificProducts
				(SpecificProductsGroupID
				, AIMMaterialID						
				, UpdateStamp
				, IsActive
				, CFCUserID)
		SELECT 			
				@NewGroupID
				, AIMMaterialID						
				, GETDATE()
				, IsActive
				, @CFCUserID		
		FROM
			dbo.SpecificProducts

		WHERE SpecificProductsGroupID = @SpecificProductsGroupID		
	END
		

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END