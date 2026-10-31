CREATE PROCEDURE [dbo].[usp_AnalyzerAnalyse_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  AA.AnalyzerAnalyseID
    , AA.DataSourceTypeID
    , AA.Code
    , AA.Name
    , AA.Description
    , AA.UpdateStamp
    , AA.CFCUserID
    , AA.IsActive
	, AU.UserName	

FROM 
	dbo.AnalyzerAnalyse AA WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AA.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID
END
