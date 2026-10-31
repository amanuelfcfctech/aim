CREATE PROCEDURE [dbo].[usp_TabOrderGroup_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  TOG.TabOrderGroupID
	, TOG.TabOrderGroupCode
	, TOG.UpdateStamp
	, TOG.CFCUserID
	, U.UserName
FROM 
	dbo.TabOrderGroup TOG
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON U.CFCUserID = TOG.CFCUserID
ORDER BY 
	TOG.TabOrderGroupCode

END