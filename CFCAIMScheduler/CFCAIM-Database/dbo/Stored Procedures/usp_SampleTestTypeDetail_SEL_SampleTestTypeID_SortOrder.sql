CREATE PROCEDURE [dbo].[usp_SampleTestTypeDetail_SEL_SampleTestTypeID_SortOrder]

	@SampleTestTypeID        INT
	, @SortOrder			INT

AS
BEGIN

SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF EXISTS(

	SELECT DISTINCT
		STTD.SampleTestTypeDetailID
		, STTD.SampleTestTypeID
		, STTD.DataSourceTypeID
		, STTD.SortOrder	
		, STTD.UpdateStamp
		, STTD.CFCUserID
		, STTD.IsActive
		, U.UserName
	
	FROM 
		dbo.SampleTestTypeDetail STTD with (nolock)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON STTD.CFCUserID = U.CFCUserID

	WHERE STTD.SampleTestTypeID = @SampleTestTypeID
		and STTD.SortOrder = @SortOrder)

		SET @ErrorNumber = 1
	
SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END