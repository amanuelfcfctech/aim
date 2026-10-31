

create PROCEDURE [dbo].[usp_APISprectrumLog_INS]
		  @SampleID			INT
		, @APISpectrumID    INT	
		, @APINIRType		INT
		
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SET XACT_ABORT ON;
SET NOCOUNT ON;


INSERT INTO 
	dbo.APISprectrumLog
		(
		  SampleID        
		, APISpectrumID
		, APINIRType
        , CreateStamp
		)
VALUES
		(
		  @SampleID        
		, @APISpectrumID
		, @APINIRType
        , GETDATE()
		)

SELECT 
	  @@IDENTITY AS RecordID
	, @@Error AS ErrorNumber
	, ERROR_MESSAGE()  AS ErrorMessage