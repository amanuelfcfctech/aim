CREATE PROCEDURE [dbo].[usp_CFCUserLoginLog_SEL]
AS
SELECT
	   CL.CFCUserLoginLog 
	 , CL.CFCUserID 
	 , CL.DatabaseConnection
	 , CL.LoginDate 
	 , CU.UserName AS CFCUserName

FROM
	dbo.CFCUserLoginLog CL
INNER JOIN
	CFCUser.CFCUser CU
	ON CL.CFCUserID = CU.CFCUserID
ORDER BY
	CL.LoginDate DESC