
create PROCEDURE [dbo].usp_APISprectrumLog_SEL_SampleIDAPISpectrumID
		  @SampleID			INT
		, @APISpectrumID	INT
		
		
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SET XACT_ABORT ON;
SET NOCOUNT ON;

------------------------------------------
--	 PROCEDURE CODE		--
------------------------------------------


SELECT 
	  ASL.APISprectrumLogID
	, ASL.SampleID		
	, ASL.APISpectrumID
	, ASL.APINIRType
	, ASL.CreateStamp

FROM 
	dbo.APISprectrumLog ASL
WHERE
		ASL.SampleID = @SampleID 
	AND ASL.APISpectrumID = @APISpectrumID