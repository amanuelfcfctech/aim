CREATE PROCEDURE [dbo].[usp_SampleTabControlPage_SAV_SampleTabControlPageID_SortOrder]
			  @SampleTabControlPageID			INT			
			, @SortOrder						INT				= NULL
			, @CFCUserID						INT				= NULL


AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		dbo.SampleTabControlPage WITH(ROWLOCK)
			SET 
				SortOrder = ISNULL(@SortOrder, SortOrder)
				, UpdateStamp = GETDATE()
				, CFCUserID = @CFCUserID				
							
			WHERE 
				SampleTabControlPageID = @SampleTabControlPageID
END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
SELECT * FROM SAMPLETABCONTROLPAGE