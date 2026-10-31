CREATE PROCEDURE [dbo].[usp_SampleTabControlPage_SEL_SampleTabControlPageName]

@SampleTabControlPageName	NVARCHAR(50)

--exec usp_SampleTabControlPage_SEL_SampleTabControlPageName 'Sample' 

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
	STCP.SampleTabControlPageName = @SampleTabControlPageName

ORDER BY STCP.SortOrder

END