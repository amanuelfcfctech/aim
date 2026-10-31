CREATE PROCEDURE [dbo].[usp_PrinterUserDefault_SEL_CFCUserID_PrinterTypeID]

	@CFCUserID			INT
	, @PrinterTypeID	INT

AS
BEGIN
SET NOCOUNT ON;

SELECT

	    PUD.*	  

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM
	dbo.PrinterUserDefault PUD
INNER JOIN 
	dbo.PrinterDefault PD WITH(NOLOCK)
		ON PD.PrinterTypeID = PUD.PrinterTypeID
INNER JOIN
	dbo.PrinterType PT with (nolock)
		ON	PD.PrinterTypeID = PT.PrinterTypeID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON PD.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

WHERE
	PUD.CFCUserID = @CFCUserID and
	PUD.PrinterTypeID = @PrinterTypeID

END