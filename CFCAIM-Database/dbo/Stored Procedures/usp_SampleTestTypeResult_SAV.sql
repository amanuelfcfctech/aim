CREATE PROCEDURE [dbo].[usp_SampleTestTypeResult_SAV]
			  @SampleTestTypeResultID	INT
			, @SampleID					INT
			, @SampleTestTypeDetailID	INT			
			, @IsIncluded				BIT
			, @IsActive				BIT
			, @CFCUserID				INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleTestTypeResultID = 0	
	BEGIN	
		INSERT
			dbo.SampleTestTypeResult
			(
				SampleID
			,   SampleTestTypeDetailID			
			,   IsIncluded
			,	IsActive
			,	UpdateStamp
			,	CFCUserID
			)
		VALUES
			(
			  @SampleID
			, @SampleTestTypeDetailID			
			, @IsIncluded
			, @IsActive
			, getdate()
			, @CFCUserID
			)
		SET @RecordID = @@IDENTITY
	END
	

ELSE
	BEGIN		
		UPDATE
			dbo.SampleTestTypeResult
				SET
					  SampleID = ISNULL(@SampleID, SampleID)
					, SampleTestTypeDetailID = ISNULL(@SampleTestTypeDetailID, SampleTestTypeDetailID)					
					, IsIncluded = ISNULL(@IsIncluded, IsIncluded)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID

		WHERE 
			SampleTestTypeResultID = @SampleTestTypeResultID
		SET @RecordID = @@IDENTITY
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END