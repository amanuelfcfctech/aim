CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_AIMMaterialCode]
		@AIMMaterialCode	NVARCHAR(20)

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  AM.AIMMaterialID
	, AM.MaterialTypeID	
	, AM.AIMMaterialName
	, AM.AIMMaterialCode
	, AM.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID
	, AU.UserName
FROM 
	dbo.AIMMaterial AM WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID
WHERE 
	AM.AIMMaterialCode = @AIMMaterialCode 

END