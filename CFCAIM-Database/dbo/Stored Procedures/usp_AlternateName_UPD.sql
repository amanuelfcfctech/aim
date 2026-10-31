CREATE PROCEDURE [dbo].[usp_AlternateName_UPD]
				@ControlID					INT = NULL
			  , @FormID						INT = NULL
			  , @AlternateName				NVARCHAR(50) = NULL			  
			  , @CFCUserID					INT = NULL			
			  , @Visible					BIT = NULL
			  , @RequiredField				BIT = NULL
			  , @ShowCodeNameOrBothID		INT = NULL					-- 6/19/20
			 

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
	
BEGIN
	
	UPDATE
		dbo.AlternateName with (rowlock)
			SET 
				  AlternateName = ISNULL(@AlternateName, AlternateName)		
				, ShowCodeNameOrBothID = ISNULL(@ShowCodeNameOrBothID, ShowCodeNameOrBothID)			-- 6/19/20  		
				, UpdateStamp = GETDATE()
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				
			WHERE
				ControlID = @ControlID
				and FormID = @FormID
				
	------------------------------------------------------------------------------
	-- allow only fields that are not Required (on the Control table) to be hidden
	UPDATE
		dbo.AlternateName with (rowlock)
			SET 
				Visible = ISNULL(@Visible, Visible)
				, UpdateStamp = GETDATE()
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				
			WHERE
				ControlID = @ControlID
				and FormID = @FormID
				and @RequiredField = 0
	
END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END