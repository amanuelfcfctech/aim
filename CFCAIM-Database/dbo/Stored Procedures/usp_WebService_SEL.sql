CREATE PROCEDURE [dbo].[usp_WebService_SEL]
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  W.WebServiceID 
	, W.WebAddress
	, W.Token				
	, W.Name
	, W.CFCUserID
	, W.UpdateStamp				
	, W.DataSourceTypeID		
	, U.UserName
FROM 
	dbo.WebService W 
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON W.CFCUserID = U.CFCUserID

END

