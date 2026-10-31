CREATE PROCEDURE [dbo].[usp_ReportQueryDetail_UPD_ReportQueryDetailID]
			  @ReportQueryDetailID					INT
			, @ReportQueryID						INT
			, @ReportQueryDetailAndOr				NVARCHAR(10)
			, @ReportQueryDetailField				NVARCHAR(20)
			, @ReportQueryDetailOperator			NVARCHAR(10)
			, @ReportQueryDetailValue				NVARCHAR(30)
			, @CFCUserID							INT
			, @IsActive								BIT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @ReportQueryDetailID > 0
BEGIN
	UPDATE
		dbo.ReportQueryDetail WITH(ROWLOCK)
			SET 
				  ReportQueryDetailAndOr = @ReportQueryDetailAndOr
				, ReportQueryDetailField = @ReportQueryDetailField
				, ReportQueryDetailOperator = @ReportQueryDetailOperator
				, ReportQueryDetailValue = @ReportQueryDetailValue
				, CFCUserID = @CFCUserID
				, IsActive = @IsActive
				, UpdateStamp = GETDATE()
			WHERE 
				ReportQueryDetailID = @ReportQueryDetailID
END
ELSE
BEGIN
	INSERT
		dbo.ReportQueryDetail WITH(ROWLOCK)
		(
		  ReportQueryID
		, CFCUserID
		, UpdateStamp
		, IsActive
		, ReportQueryDetailAndOr
		, ReportQueryDetailField
		, ReportQueryDetailOperator
		, ReportQueryDetailValue
		)
		values
		(
		  @ReportQueryID
		, @CFCUserID
		, GETDATE()
		, 1
		, @ReportQueryDetailAndOr
		, @ReportQueryDetailField
		, @ReportQueryDetailOperator
		, @ReportQueryDetailValue
		)
END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
