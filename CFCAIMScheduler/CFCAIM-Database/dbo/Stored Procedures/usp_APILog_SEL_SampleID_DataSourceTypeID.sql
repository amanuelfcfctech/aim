CREATE PROCEDURE [dbo].[usp_APILog_SEL_SampleID_DataSourceTypeID]
		@SampleID			INT
		, @DataSourceTypeID	INT
		
		
AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SET XACT_ABORT ON;
SET NOCOUNT ON;

------------------------------------------
--	 PROCEDURE CODE		--
------------------------------------------


SELECT TOP 1
	  APL.APILogID
	, APL.SampleID		
	, APL.APISampleID
	, APL.DataSourceTypeID
	, APL.CreateStamp

FROM 
	dbo.APILog APL
	
WHERE
	APL.SampleID = @SampleID AND
	APL.DataSourceTypeID = @DataSourceTypeID
	

ORDER BY
	APL.CreateStamp DESC