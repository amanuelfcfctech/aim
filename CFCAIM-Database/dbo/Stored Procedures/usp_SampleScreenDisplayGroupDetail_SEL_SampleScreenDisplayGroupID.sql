CREATE PROCEDURE [dbo].[usp_SampleScreenDisplayGroupDetail_SEL_SampleScreenDisplayGroupID]
                @SampleScreenDisplayGroupID    INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
      SSDGD.SampleScreenDisplayGroupDetailID
    , SSDGD.SampleScreenDisplayGroupID
    , SSDGD.ControlID
    , C.ControlName
    , C.UserControlName
    , SSDGD.UpdateStamp
    , SSDGD.CFCUserID
    , U.UserName
FROM
    dbo.SampleScreenDisplayGroupDetail SSDGD
    INNER JOIN
        dbo.Control C
            ON C.ControlID = SSDGD.ControlID
    LEFT OUTER JOIN
        CFCUser.CFCUser U
            ON U.CFCUserID = SSDGD.CFCUserID
WHERE
    SSDGD.SampleScreenDisplayGroupID = @SampleScreenDisplayGroupID
ORDER BY
    C.UserControlName
END