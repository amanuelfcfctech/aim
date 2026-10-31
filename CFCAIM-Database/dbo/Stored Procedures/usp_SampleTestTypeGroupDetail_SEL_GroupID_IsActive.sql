CREATE PROCEDURE [dbo].[usp_SampleTestTypeGroupDetail_SEL_GroupID_IsActive]

	@GroupID		INT
	, @IsActive		BIT		

AS
BEGIN

SET NOCOUNT ON;
DECLARE @SQL				VARCHAR(MAX) = ''

--EXEC usp_SampleTestTypeGroupDetail_SEL_GroupID_IsActive 1, 1

SET @SQL = 

'SELECT 
	STTGD.SampleTestTypeGroupDetailID,
	STTGD.IsActive,
	STTGD.CFCUserID,
	STTGD.UpdateStamp,
	STTG.SampleTestTypeGroupID,
	STTG.SampleTestTypeGroupCode,
	STT.SampleTestTypeID,
	STT.SampleTestTypeName,
	STTD.SampleTestTypeDetailID,
	STTD.SortOrder,
	DST.Name as DataSourceTypeName,
	U.UserName

	
FROM 
	dbo.SampleTestTypeGroup STTG
INNER JOIN
	dbo.SampleTestTypeGroupDetail STTGD
		ON STTG.SampleTestTypeGroupID = STTGD.SampleTestTypeGroupID
INNER JOIN
	dbo.SampleTestType STT
		ON STTGD.SampleTestTypeID = STT.SampleTestTypeID
INNER JOIN
	dbo.SampleTestTypeDetail STTD
		ON STT.SampleTestTypeID = STTD.SampleTestTypeID
LEFT OUTER JOIN
	dbo.DataSourceType DST
		ON STTD.DataSourceTypeID = DST.DataSourceTypeID
LEFT OUTER JOIN
    CFCUser.CFCUser U WITH(NOLOCK)
		ON STTGD.CFCUserID = U.CFCUserID

WHERE
	STTG.IsActive = ' + CAST(@IsActive as varchar) 

if @GroupID > 0

	SET @SQL += ' and STTG.SampleTestTypeGroupID = ' + CAST(@GroupID as varchar)


EXEC (@SQL)
--PRINT @SQL

END