CREATE PROCEDURE [dbo].[usp_PrinterDefault_PrinterType_SEL]

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	PD.PrinterTypeID
	, PT.PrinterTypeName 
	, PD.PrinterName
	, PT.IsActive
	, PT.UpdateStamp
	, PT.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	PrinterDefault PD
INNER JOIN 
	PrinterType PT
		ON PD.PrinterTypeID = PT.PrinterTypeID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON PD.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

END