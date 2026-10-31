CREATE PROCEDURE [dbo].[usp_SampleTestTypeGroup_SEL_GroupID]

	@SampleTestTypeGroupID		INT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(MAX) = ''
SET @SQL = 

'SELECT 
	  STTG.SampleTestTypeGroupID
	, STTG.SampleTestTypeGroupCode	
	, STTG.IsActive
	, STTG.UpdateStamp
	, STTG.CFCUserID	
	, U.UserName

FROM 
	dbo.SampleTestTypeGroup STTG

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON STTG.CFCUserID = U.CFCUserID '


if @SampleTestTypeGroupID > 0

	SET @SQL += ' WHERE	STTG.SampleTestTypeGroupID = ' + CAST(@SampleTestTypeGroupID as varchar) 

EXEC (@SQL)

	
END