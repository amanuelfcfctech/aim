CREATE PROCEDURE [dbo].[usp_PrinterUserDefault_SEL_All]

AS
BEGIN
SET NOCOUNT ON;

SELECT	
	    PUD.*
	  , PT.PrinterTypeName	  

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName 
	  , U.UserName 
	  	  
FROM
	dbo.PrinterUserDefault PUD
INNER JOIN 
	dbo.PrinterDefault PD WITH(NOLOCK)
		ON PD.PrinterTypeID = PUD.PrinterTypeID
LEFT OUTER JOIN
	dbo.PrinterType PT with (nolock)
		ON	PD.PrinterTypeID = PT.PrinterTypeID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON PUD.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

END