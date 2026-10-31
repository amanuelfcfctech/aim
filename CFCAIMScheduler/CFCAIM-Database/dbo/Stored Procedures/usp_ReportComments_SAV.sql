CREATE PROCEDURE [dbo].[usp_ReportComments_SAV]
			  @ReportCommentsID					INT
			, @ReportID							INT = NULL			
			, @ReportComment1					NVARCHAR(200) = NULL
			, @ReportComment2					NVARCHAR(200) = NULL
			, @ReportComment3					NVARCHAR(200) = NULL
			, @ReportComment4					NVARCHAR(200) = NULL
			, @ReportComment5					NVARCHAR(200) = NULL
			, @CFCUserID						INT	= NULL
			
--exec usp_ReportComments_SAV 0, 29, 'test', '', 'testtesttest', '', '', 1
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''
Declare @InsertOrUpdate nvarchar(10)

BEGIN

IF NOT EXISTS(SELECT TOP 1	
						RC.ReportCommentsID
					FROM
						dbo.ReportComments RC
					WHERE
						RC.ReportID = @ReportID)	

	
	Begin			

			INSERT
				dbo.ReportComments
					(
					  ReportID								
					, ReportComment1
					, ReportComment2
					, ReportComment3
					, ReportComment4
					, ReportComment5
					, CFCUserID
					, UpdateStamp				
					
					)
			VALUES
					(
					  @ReportID
					, @ReportComment1
					, @ReportComment2
					, @ReportComment3
					, @ReportComment4
					, @ReportComment5
					, @CFCUserID
					, GETDATE()					
					)
			SET @RecordID = @@IDENTITY 
		End

ELSE

	Begin
		SET @ReportCommentsID = (SELECT TOP 1	
							RC.ReportCommentsID
						FROM
							dbo.ReportComments RC
						WHERE
							RC.ReportID = @ReportID)

		UPDATE
			dbo.ReportComments 
				SET 
					  ReportID = ISNULL(@ReportID, ReportID)															
					, ReportComment1 = ISNULL(@ReportComment1, ReportComment1)
					, ReportComment2 = ISNULL(@ReportComment2, ReportComment2)
					, ReportComment3 = ISNULL(@ReportComment3, ReportComment3)
					, ReportComment4 = ISNULL(@ReportComment4, ReportComment4)
					, ReportComment5 = ISNULL(@ReportComment5, ReportComment5)
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					
					
				WHERE 
					ReportCommentsID = @ReportCommentsID

			SET @RecordID = @@IDENTITY 
	End

END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END