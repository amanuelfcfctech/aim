

-------------------------------------------------
CREATE PROCEDURE [dbo].[usp_LabelFormat_SAV]
				@LabelFormatID				INT
			  , @LabelID					INT
			  , @MaterialTypeID 			INT
			  , @OrderNumber				INT
			  , @IsBold						BIT
			  , @FontSize					INT
			  , @CFCUserID					INT = NULL					  

AS
BEGIN

SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @LabelFormatID = 0 
	Begin
		INSERT
			dbo.LabelFormat
			(
				LabelID
				, MaterialTypeID
				, OrderNumber
				, IsBold
				, FontSize
				, CFCUserID
				, UpdateStamp
			)
		VALUES
			(	@LabelID
				, @MaterialTypeID
				, @OrderNumber
				, @IsBold
				, @FontSize
				, @CFCUserID
				, GetDate()
			)
		SET @RecordID = @@IDENTITY
	End
Else
	Begin
		UPDATE 
			dbo.LabelFormat
		SET 
			LabelID = ISNULL(@LabelID, LabelID)
			, MaterialTypeID = ISNULL(@MaterialTypeID, MaterialTypeID)
			, OrderNumber = ISNULL(@OrderNumber, OrderNumber)
			, IsBold = ISNULL(@IsBold, IsBold)
			, FontSize = ISNULL(@FontSize, FontSize)
			, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			, UpdateStamp = GetDate()
		
		WHERE 
			LabelFormatID = @LabelFormatID
	End

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END