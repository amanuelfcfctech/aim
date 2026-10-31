CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_OwnerID_AIMOwnerLocationGroupID]
		  @AIMOwnerID					INT
		, @AIMOwnerLocationGroupID		INT
AS
BEGIN

-- EXEC usp_AIMOwnerLocation_SEL_OwnerID_AIMOwnerLocationGroupID 1, 0

SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(MAX) = ''

SET @SQL = 
'SELECT
	  OL.*
	, O.OwnerName
	, U.UserName	

FROM 
	dbo.AIMOwnerLocation OL WITH(NOLOCK)
	LEFT JOIN dbo.AIMOwner O with (nolock)
		ON OL.AIMOwnerID = O.AIMOwnerID
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
		OL.AIMOwnerID = ' + CAST(@AIMOwnerID AS VARCHAR) + '
	AND OL.IsActive = 1 '

IF @AIMOwnerLocationGroupID > 0 
	SET @SQL += ' and ALGD.AIMOwnerLocationGroupID = ' + CAST(@AIMOwnerLocationGroupID AS VARCHAR)

SET @SQL += '

ORDER BY
	OL.LocationName
	'

EXEC (@SQL)
--PRINT @SQL

END