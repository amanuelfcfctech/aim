create PROCEDURE [dbo].[usp_AnalysisLog_UPD_AnalysisLogID]
			  @AnalysisLogID					INT
			, @Activity							VARCHAR(200)
			, @CFCUserID						INT
			, @IsActive							BIT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalysisLogID > 0
BEGIN
	UPDATE
		dbo.AnalysisLog WITH(ROWLOCK)
			SET 
				  Activity = @Activity
				, CFCUserID = @CFCUserID
				, IsActive = @IsActive
				, UpdateStamp = GETDATE()
			WHERE 
				AnalysisLogID = @AnalysisLogID
END
ELSE
BEGIN
	INSERT INTO
		dbo.AnalysisLog
			(
			  Activity
			, CFCUserID
			, IsActive
			, UpdateStamp
			)
		VALUES
			(
			  @Activity
			, @CFCUserID
			, 1
			, GETDATE()
			)
END

SELECT 
	  @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
