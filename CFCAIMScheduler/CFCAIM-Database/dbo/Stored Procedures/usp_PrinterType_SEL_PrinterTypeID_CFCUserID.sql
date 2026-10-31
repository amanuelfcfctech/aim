
CREATE PROCEDURE [dbo].[usp_PrinterType_SEL_PrinterTypeID_CFCUserID]
		@PrinterTypeID		INT
		, @CFCUserID		INT
AS
BEGIN
SET NOCOUNT ON;

--exec dbo.usp_PrinterType_SEL_PrinterTypeID_CFCUserID 2, 3

SELECT 
	Case When X.PrinterTypeID is not null then X.UserPrinter Else PD.PrinterName End as PrinterName	
	, X.PrinterTypeID
	, X.UserPrinter
	, PD.CFCUserID
	, PT.IsActive
	, PT.UpdateStamp
	, PT.PrinterTypeName	

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.PrinterDefault PD 
INNER JOIN
	dbo.PrinterType PT
		ON PD.PrinterTypeID = PT.PrinterTypeID
LEFT OUTER JOIN 
	(SELECT PrinterTypeID, PrinterName as UserPrinter 
		FROM dbo.PrinterUserDefault PUD 
		WHERE PUD.CFCUserID = @CFCUserID) X 
			on PD.PrinterTypeID = X.PrinterTypeID
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