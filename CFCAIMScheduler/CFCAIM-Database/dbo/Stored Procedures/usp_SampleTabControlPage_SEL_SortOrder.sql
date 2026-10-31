CREATE PROCEDURE [dbo].[usp_SampleTabControlPage_SEL_SortOrder]

@SortOrder	INT

--exec usp_SampleTabControlPage_SEL_SortOrder 2 

AS
BEGIN
SET NOCOUNT ON;

SELECT
	STCP.SampleTabControlPageID
	, STCP.SampleTabControlPageName
	, STCP.TabControlName
	, STCP.SortOrder
	, STCP.IsHidden
	, STCP.CFCUserID
	, STCP.UpdateStamp
	, U.UserName

FROM 
	dbo.SampleTabControlPage STCP

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON STCP.CFCUserID = U.CFCUserID

WHERE	
	STCP.SortOrder = @SortOrder

ORDER BY STCP.SortOrder

END