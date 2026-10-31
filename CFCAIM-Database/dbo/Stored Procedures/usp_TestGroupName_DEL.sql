CREATE PROCEDURE dbo.usp_TestGroupName_DEL
    @TestGroupNameID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM dbo.TestGroupName
    WHERE
        TestGroupNameID = @TestGroupNameID
END