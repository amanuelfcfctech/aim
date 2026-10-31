
CREATE PROCEDURE [dbo].[usp_APILog_INS_SampleID_APISampleID_DataSourceTypeID]
		@SampleID			INT
		, @APISampleID	    INT	
		, @DataSourceTypeID	INT
		
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SET XACT_ABORT ON;
SET NOCOUNT ON;


INSERT INTO 
	dbo.APILog
		(
		  SampleID        
		, APISampleID
		, DataSourceTypeID
        , CreateStamp
		)
VALUES
		(
		  @SampleID        
		, @APISampleID
		, @DataSourceTypeID
        , GETDATE()
		)

SELECT 
	  @@IDENTITY AS RecordID
	, @@Error AS ErrorNumber
	, ERROR_MESSAGE()  AS ErrorMessage