CREATE PROCEDURE [dbo].[usp_CFCUser_SEL]
	
AS

BEGIN
SET NOCOUNT ON;

SELECT 
	  U.CFCUserID	
	, U.FirstName
	, U.LastName
	, U.EmailAddress 
	, U.LastActiveDataMapID
	, U.UserName
		
FROM	
	CFCUser.CFCUser U WITH(NOLOCK)				

WHERE
	U.CFCUserID <> 0

END