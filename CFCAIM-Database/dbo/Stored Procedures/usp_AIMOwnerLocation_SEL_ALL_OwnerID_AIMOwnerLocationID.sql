CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_ALL_OwnerID_AIMOwnerLocationID]
		@AIMOwnerID						INT
		, @AIMOwnerLocationGroupID		INT

AS
BEGIN

-- exec [usp_AIMOwnerLocation_SEL_ALL_OwnerID_AIMOwnerLocationID] 1, 0
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(MAX) = ''

SET @SQL = 
'SELECT
	  OL.*
	, U.UserName

FROM 
	dbo.AIMOwnerLocation OL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON OL.CFCUserID = U.CFCUserID '
	IF @AIMOwnerLocationGroupID > 0 
	BEGIN
	SET @SQL += '
	LEFT OUTER JOIN
		dbo.AIMOwnerLocationGroupDetail ALGD
			on OL.AIMOwnerLocationID = ALGD.AIMOwnerLocationID	'
	END 

SET @SQL += '
WHERE
		OL.AIMOwnerID = ' + CAST(@AIMOwnerID AS VARCHAR)

IF @AIMOwnerLocationGroupID > 0 
	SET @SQL += ' and ALGD.AIMOwnerLocationGroupID = ' + CAST(@AIMOwnerLocationGroupID AS VARCHAR)

SET @SQL += '

ORDER BY
		OL.LocationName '

EXEC (@SQL)
	
END