CREATE PROCEDURE [dbo].[usp_AIMOwner_SEL_IsActive_GroupID_IsActive]
		 @AIMOwnerLocationGroupID	INT
		,@IsActive					BIT


--exec usp_AIMOwner_SEL_IsActive_GroupID_IsActive 6, 5
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(MAX) = ''

SET @SQL = 
'SELECT
	  AO.AIMOwnerID
	, AO.OwnerCode
	, AO.OwnerName
	, AO.IsActive
	, AO.UpdateStamp
	, U.UserName
	

FROM 
	dbo.AIMOwner AO WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AO.CFCUserID = U.CFCUserID '

IF @AIMOwnerLocationGroupID > 0 
SET @SQL += '
	LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL
			on AO.AIMOwnerID = AOL.AIMOwnerID
	LEFT OUTER JOIN
		dbo.AIMOwnerLocationGroupDetail ALGD
			on AOL.AIMOwnerLocationID = ALGD.AIMOwnerLocationID '

SET @SQL += '		
WHERE 	
	AO.IsActive = ' + CAST(@IsActive AS VARCHAR)

IF @AIMOwnerLocationGroupID > 0 
	SET @SQL += ' and ALGD.AIMOwnerLocationGroupID = ' + CAST(@AIMOwnerLocationGroupID AS VARCHAR)


SET @SQL += 
'GROUP BY
	AO.AIMOwnerID
	, AO.OwnerCode
	, AO.OwnerName
	, AO.IsActive
	, AO.UpdateStamp
	, U.UserName 
	'

EXEC (@SQL)

END