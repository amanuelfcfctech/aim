CREATE PROCEDURE [dbo].[usp_UserDefaults_SEL_CFCUserID]
	  @CFCUserID			INT
	
	
	
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  UD.UserDefaultsID
	, UD.CFCUserID
	, UD.FormID
	, UD.ControlID 
	, UD.ValueOnOff
	, UD.UpdateStamp
	, UD.UpdateUserID
	, AU.UserName
	, C.ControlName 
FROM 
	dbo.UserDefaults UD WITH(NOLOCK)
	INNER JOIN
		dbo.Control C with (nolock)
			ON UD.ControlID = C.ControlID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON UD.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID

WHERE
	UD.CFCUserID = @CFCUserID and
	C.AllowUserDefault = 1

END