CREATE PROCEDURE [dbo].[usp_AIMOwner_SEL_AIMOwnerLocationGroupID]  

		@AIMOwnerLocationGroupID	INT
AS   

--exec usp_AIMOwner_SEL_GroupID 5

BEGIN   
SET NOCOUNT ON;   

DECLARE @SQL				VARCHAR(MAX) = ''

SET @SQL = '
SELECT       
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
			on AOL.AIMOwnerLocationID = ALGD.AIMOwnerLocationID	
			and ALGD.AIMOwnerLocationGroupID = ' + CAST(@AIMOwnerLocationGroupID AS VARCHAR)
			
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