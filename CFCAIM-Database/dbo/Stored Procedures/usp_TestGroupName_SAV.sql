CREATE PROCEDURE dbo.usp_TestGroupName_SAV
      @TestGroupNameID  INT
    , @TestGroupName    NVARCHAR(50) = NULL
    , @CFCUserID           INT          = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorNumber INT = 0
    DECLARE @RecordID INT = 0
    DECLARE @ErrorMessage VARCHAR(200) = ''

    IF @TestGroupNameID = 0
    BEGIN
        INSERT INTO dbo.TestGroupName
        (
            TestGroupName,
            UpdateStamp,
            CFCUserID
        )
        VALUES
        (
            @TestGroupName,
            GETDATE(),
            @CFCUserID
        )

        SET @RecordID = @@IDENTITY
    END
    ELSE
    BEGIN
        UPDATE dbo.TestGroupName WITH (ROWLOCK)
        SET
            TestGroupName = ISNULL(@TestGroupName, TestGroupName),
            UpdateStamp   = GETDATE(),
            CFCUserID        = ISNULL(@CFCUserID, CFCUserID)
        WHERE
            TestGroupNameID = @TestGroupNameID

        SET @RecordID = @@IDENTITY
    END

    SELECT
          @RecordID AS RecordID
        , @ErrorNumber AS ErrorNumber
        , @ErrorMessage AS ErrorMessage
END