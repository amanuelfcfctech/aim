CREATE PROCEDURE [dbo].[usp_TestGroupName_SEL]
AS
BEGIN
    SELECT
        TGN.TestGroupNameID,
        TGN.TestGroupName,
        TGN.UpdateStamp,
        TGN.CFCUserID,
		U.UserName
    FROM dbo.TestGroupName TGN
	INNER JOIN
		CFCUser.CFCUser U
		ON U.CFCUserID = TGN.CFCUserID
END;