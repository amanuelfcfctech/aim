CREATE PROCEDURE [dbo].[usp_AlternateName_UPD_LabelFields]
				@ControlID					INT 
			  , @FormID						INT 
			  , @AlternateNameID			INT
			  , @LabelField 				INT   -- 1 or 2
			  , @LabelValue					BIT	  -- 0 or 1
			  , @CFCUserID					INT = NULL			

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
	
BEGIN

	IF @LabelField = 1
	Begin
		UPDATE 
			dbo.AlternateName 
				SET 
					LabelFieldFirst = 0 
					, CFCUserID = @CFCUserID
				WHERE FormID = @FormID	
		UPDATE
			dbo.AlternateName
				SET 
					LabelFieldFirst = @LabelValue
					, CFCUserID = @CFCUserID
				WHERE
					FormID = @FormID
					AND ControlID = @ControlID
					AND AlternateNameID = @AlternateNameID
	End

	IF @LabelField =2
	Begin
		UPDATE 
			dbo.AlternateName 
				SET 
					LabelFieldLast = 0 
					, CFCUserID = @CFCUserID
				WHERE FormID = @FormID	
		UPDATE
			dbo.AlternateName
				SET 
					LabelFieldLast = @LabelValue
					, CFCUserID = @CFCUserID
				WHERE
					FormID = @FormID
					AND ControlID = @ControlID
					AND AlternateNameID = @AlternateNameID				
	End

END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END