CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntryDetail_SEL_SampleQualityControlMassEntryID_SortOrder]

	@SampleQualityControlMassEntryID        INT
	, @SortOrder							INT

AS
BEGIN

SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF EXISTS(

	SELECT DISTINCT
		MED.SampleQualityControlMassEntryDetailID
		, MED.SampleQualityControlMassEntryID
		, MED.SampleQualityParameterID
		, MED.SortOrder	
		, MED.ResultValue
		, MED.ResultValueString
		, MED.UpdateStamp
		, MED.CFCUserID
		, U.UserName
	
	FROM 
		dbo.SampleQualityControlMassEntryDetail MED with (nolock)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON MED.CFCUserID = U.CFCUserID

	WHERE MED.SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID
		and MED.SortOrder = @SortOrder)

		SET @ErrorNumber = 1
	
SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END