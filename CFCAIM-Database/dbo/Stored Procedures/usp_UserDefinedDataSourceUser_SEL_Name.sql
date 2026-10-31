
CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceUser_SEL_Name]
				@Name NVARCHAR(30) 
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  DST.*
	
FROM 
	dbo.DataSourceType DST WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DST.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID
WHERE 
	DST.IsUserDefined = 1 and 
	DST.Name = @Name


END