CREATE PROCEDURE [dbo].[usp_SampleTestType_SEL_SampleTestTypeGroupID]

		@SampleTestTypeGroupID		INT		

--exec usp_SampleTestType_SEL_SampleTestTypeGroupID 0

AS
BEGIN


SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(MAX) = ''
SET @SQL = 


'SELECT 
	  STT.SampleTestTypeID
	, STT.SampleTestTypeName
	, STT.UpdateStamp
	, STT.CFCUserID
	, U.UserName

FROM 
	dbo.SampleTestType STT WITH(NOLOCK)
			
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON STT.CFCUserID = U.CFCUserID

	LEFT OUTER JOIN
		dbo.SampleTestTypeGroupDetail STTGD
			on STT.SampleTestTypeID = STTGD.SampleTestTypeID
	LEFT OUTER JOIN	
		dbo.SampleTestTypeGroup STTG
			on STTGD.SampleTestTypeGroupID = STTG.SampleTestTypeGroupID '

if @SampleTestTypeGroupID > 0

	SET @SQL += ' WHERE STTG.SampleTestTypeGroupID = ' + CAST(@SampleTestTypeGroupID as varchar) + ' and STTG.IsActive = 1 '

SET @SQL += ' GROUP BY 
	STT.SampleTestTypeID
	, STT.SampleTestTypeName
	, STT.UpdateStamp
	, STT.CFCUserID
	, U.UserName '


EXEC (@SQL)
--PRINT @SQL

END