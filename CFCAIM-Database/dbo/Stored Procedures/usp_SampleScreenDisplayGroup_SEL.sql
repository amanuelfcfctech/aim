CREATE PROCEDURE [dbo].[usp_SampleScreenDisplayGroup_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT
      SSDG.SampleScreenDisplayGroupID
    , SSDG.SampleScreenDisplayGroupCode
    , SSDG.UpdateStamp
    , SSDG.CFCUserID
    , U.UserName
FROM
    dbo.SampleScreenDisplayGroup SSDG
    LEFT OUTER JOIN
        CFCUser.CFCUser U
            ON U.CFCUserID = SSDG.CFCUserID
ORDER BY
    SSDG.SampleScreenDisplayGroupCode
END