CREATE PROCEDURE [dbo].[usp_PrinterDefault_SEL_PrinterTypeID]

@PrinterTypeID		INT

AS
BEGIN
SET NOCOUNT ON;

SELECT
	    PD.*
	  , PT.PrinterTypeName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.PrinterDefault PD WITH(NOLOCK)
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
	PD.PrinterTypeID = @PrinterTypeID

END