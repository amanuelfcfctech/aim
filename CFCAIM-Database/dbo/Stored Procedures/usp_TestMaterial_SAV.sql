
CREATE PROCEDURE [dbo].[usp_TestMaterial_SAV]
      @TestMaterialID    INT
    , @TestID            INT           = NULL
    , @AIMMaterialID     INT           = NULL
    , @TestMaterialCurrentUpcharge  NUMERIC(18,8) = NULL
    , @TestMaterialPreviousUpcharge  NUMERIC(18,8) = NULL
    , @CFCUserID            INT           = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorNumber INT = 0
    DECLARE @RecordID INT = 0
    DECLARE @ErrorMessage VARCHAR(200) = ''

    IF @TestMaterialID = 0
    BEGIN
        INSERT INTO dbo.TestMaterial
        (
            TestID,
            AIMMaterialID,
            TestMaterialCurrentUpcharge,
	    TestMaterialPreviousUpcharge,
            UpdateStamp,
            CFCUserID
        )
        VALUES
        (
            @TestID,
            @AIMMaterialID,
            @TestMaterialCurrentUpcharge,
	    @TestMaterialPreviousUpcharge,
            GETDATE(),
            @CFCUserID
        )

        SET @RecordID = @@IDENTITY
    END
    ELSE
    BEGIN
        UPDATE dbo.TestMaterial WITH (ROWLOCK)
        SET
            TestID           = ISNULL(@TestID, TestID),
            AIMMaterialID    = ISNULL(@AIMMaterialID, AIMMaterialID),
            TestMaterialCurrentUpcharge = ISNULL(@TestMaterialCurrentUpcharge, TestMaterialCurrentUpcharge),
	    TestMaterialPreviousUpcharge = ISNULL(@TestMaterialPreviousUpcharge, TestMaterialPreviousUpcharge),
            UpdateStamp      = GETDATE(),
            CFCUserID           = ISNULL(@CFCUserID, CFCUserID)
        WHERE
            TestMaterialID = @TestMaterialID

        SET @RecordID = @@IDENTITY
    END

    SELECT
          @RecordID AS RecordID
        , @ErrorNumber AS ErrorNumber
        , @ErrorMessage AS ErrorMessage
END