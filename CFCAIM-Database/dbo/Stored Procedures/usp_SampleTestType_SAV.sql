CREATE PROCEDURE [dbo].[usp_SampleTestType_SAV]
			  @SampleTestTypeID	INT			
			, @SampleTestTypeName NVARCHAR(50)	= NULL						
			, @CFCUserID						 INT			= NULL		
			, @UpdateStamp						 DATETIME		= NULL
			

			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleTestTypeID = 0
	BEGIN
		INSERT
			dbo.SampleTestType
				(
				  SampleTestTypeName								
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @SampleTestTypeName															
				, @CFCUserID
				, GetDate()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleTestType WITH(ROWLOCK)
				SET 
					  SampleTestTypeName = ISNULL(@SampleTestTypeName, SampleTestTypeName)											
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)						
					
				WHERE 
					SampleTestTypeID = @SampleTestTypeID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END