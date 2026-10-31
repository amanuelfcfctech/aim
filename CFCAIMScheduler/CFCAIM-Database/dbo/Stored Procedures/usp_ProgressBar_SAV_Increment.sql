CREATE PROCEDURE [dbo].[usp_ProgressBar_SAV_Increment]
				@ProgressBarID			INT
			,	@Description			NVARCHAR(50)	= NULL
			,	@CFCUserID				INT				= NULL
			,   @Increment				NUMERIC(18,8)	= NULL
			,   @ProgressBarType		INT				= NULL

			-- 6/25/19 - added because of memory issues at Smithfield
			,   @ReturnMessage	BIT
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
				,	@ProgressBarType
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN			
		UPDATE 
			dbo.ProgressBar WITH(ROWLOCK) 
		SET 						
				ProgressBarValueDecimal = CASE WHEN ISNULL(ProgressBarValueDecimal,0) + @Increment > 100 THEN
											1
										ELSE
											ISNULL(ProgressBarValueDecimal,0) + @Increment
										END	
			,   ProgressBarValue = CASE WHEN ISNULL(ProgressBarValueDecimal,0) + @Increment + 1 > 100 THEN
											1
										ELSE
											ISNULL(Floor(ProgressBarValueDecimal),0)
										END										 
			,	ProgressBarDescription = ISNULL(@Description, ProgressBarDescription)
			,	UpdateStamp = GETDATE() 
			,	CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
			,	ProgressBarType = ISNULL(@ProgressBarType, ProgressBarType)

		WHERE
			ProgressBarID = @ProgressBarID
		SET @RecordID = @ProgressBarID
	END

-- 6/25/19 - added because of memory issues at Smithfield
IF @ReturnMessage = 1
	BEGIN

		SELECT 
			  @RecordID AS RecordID
			, @ErrorNumber AS ErrorNumber
			, @ErrorMessage AS ErrorMessage
	END
END