CREATE PROCEDURE dbo.usp_Test_SAV
      @TestID            INT
    , @TestCode          NVARCHAR(20) = NULL
    , @TestName          NVARCHAR(50) = NULL
    , @DataSourceTypeID  INT          = NULL
    , @TestCurrentCost          NUMERIC(18,8)= NULL
    , @TestPreviousCost          NUMERIC(18,8)= NULL
    , @CFCUserID            INT          = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorNumber INT = 0
    DECLARE @RecordID INT = 0
    DECLARE @ErrorMessage VARCHAR(200) = ''

    IF @TestID = 0
    BEGIN
        INSERT INTO dbo.Test
        (
            TestCode,
            TestName,
            DataSourceTypeID,
            TestCurrentCost,
	    TestPreviousCost,
            UpdateStamp,
            CFCUserID
        )
        VALUES
        (
            @TestCode,
            @TestName,
            @DataSourceTypeID,
            @TestCurrentCost,
	    @TestPreviousCost,
            GETDATE(),
            @CFCUserID
        )

        SET @RecordID = @@IDENTITY
    END
    ELSE
    BEGIN
        UPDATE dbo.Test WITH (ROWLOCK)
        SET
            TestCode         = ISNULL(@TestCode, TestCode),
            TestName         = ISNULL(@TestName, TestName),
            DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID),
            TestCurrentCost         = ISNULL(@TestCurrentCost, TestCurrentCost),
	    TestPreviousCost         = ISNULL(@TestPreviousCost, TestPreviousCost),
            UpdateStamp      = GETDATE(),
            CFCUserID           = ISNULL(@CFCUserID, CFCUserID)
        WHERE
            TestID = @TestID

        SET @RecordID = @@IDENTITY
    END

    SELECT
          @RecordID AS RecordID
        , @ErrorNumber AS ErrorNumber
        , @ErrorMessage AS ErrorMessage
END