CREATE PROCEDURE [dbo].[usp_ReportQuery_UPD_ReportQueryID]
			  @ReportQueryID					INT
			, @ReportID							INT
			, @ReportQueryCode					NVARCHAR(20)
			, @ReportQueryName					NVARCHAR(30)
			, @CFCUserID						INT
			, @IsActive							BIT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @ReportQueryID > 0
BEGIN
	UPDATE
		dbo.ReportQuery WITH(ROWLOCK)
			SET 
				  ReportQueryCode = ISNULL(@ReportQueryCode, ReportQueryCode)
				, ReportQueryName = ISNULL(@ReportQueryName, ReportQueryName)
				, ReportID = ReportID
				, CFCUserID = @CFCUserID
				, IsActive = @IsActive
				, UpdateStamp = GETDATE()
			WHERE 
				ReportQueryID = @ReportQueryID
END
ELSE
BEGIN
	INSERT
		dbo.ReportQuery WITH(ROWLOCK)
		(
		  ReportID
		, ReportQueryCode
		, ReportQueryName
		, CFCUserID
		, UpdateStamp
		, IsActive
		)
		values
		(
		  @ReportID
		, @ReportQueryCode
		, @ReportQueryName
		, @CFCUserID
		, GETDATE()
		, 1
		)
END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
