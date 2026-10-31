CREATE PROCEDURE [dbo].[usp_ProgressBar_SAV]
				@ProgressBarID			INT
			,	@Description			NVARCHAR(50)	= NULL
			,	@CFCUserID				INT				= NULL
			,   @ProgressBarType		INT				= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber	INT = 0
DECLARE @RecordID		INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

IF @ProgressBarID = 0
	BEGIN
		INSERT 
			dbo.ProgressBar 
				(
					ProgressBarValue
				,	ProgressBarDescription
				,	UpdateStamp 
				,	CFCUserID
				,	ProgressBarType
				)
		VALUES
				(
					0
				,	@Description
				,	GETDATE()
				,	@CFCUserID
				,   @ProgressBarType
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN			
		UPDATE 
			dbo.ProgressBar WITH(ROWLOCK) 
		SET 
				ProgressBarValue = CASE WHEN ProgressBarValue + 1 > 100 THEN
											1
										ELSE
											ProgressBarValue + 1
										END
			,	ProgressBarDescription = ISNULL(@Description, ProgressBarDescription)
			,	UpdateStamp = GETDATE() 
			,	CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
			,	ProgressBarType = ISNULL(@ProgressBarType, ProgressBarType)

		WHERE
			ProgressBarID = @ProgressBarID
		SET @RecordID = @ProgressBarID
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END
