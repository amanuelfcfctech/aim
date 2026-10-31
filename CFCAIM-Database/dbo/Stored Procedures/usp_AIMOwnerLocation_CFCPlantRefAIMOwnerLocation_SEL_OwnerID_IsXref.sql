CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_CFCPlantRefAIMOwnerLocation_SEL_OwnerID_IsXref]
		  @AIMOwnerID		INT
		, @IsXref			BIT


--exec usp_AIMOwnerLocation_CFCPlantRefAIMOwnerLocation_SEL_OwnerID_IsXref 1, 1

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''


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

IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCPlantRefAIMOwnerLocation x 
				ON OL.AimOwnerLocationID = x.AIMOwnerLocationID 

WHERE
	OL.AIMOwnerID = ' + cast(@AIMOwnerID as VARCHAR) + ' AND OL.IsActive = 1 '

If @IsXref = 0
	SET @SQL += ' and x.CFCPlantRefAIMOwnerLocationID is null '

SET @SQL += ' ORDER BY
	OL.LocationName '


--print @SQL
Exec (@SQL)

END