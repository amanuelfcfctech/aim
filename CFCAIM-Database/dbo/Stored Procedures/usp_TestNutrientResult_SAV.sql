CREATE PROCEDURE [dbo].[usp_TestNutrientResult_SAV]
      @TestNutrientResultID   INT
    , @TestID                 INT           = NULL
    , @AIMNutrientID             INT           = NULL
    , @TestGroupNameID        INT           = NULL
    , @TestNutrientResultCurrentUpcharge NUMERIC(18,8) = NULL
    , @TestNutrientResultPreviousUpcharge NUMERIC(18,8) = NULL
    , @CFCUserID                 INT           = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorNumber INT = 0
    DECLARE @RecordID INT = 0
    DECLARE @ErrorMessage VARCHAR(200) = ''

    IF @TestNutrientResultID = 0
    BEGIN
        INSERT INTO dbo.TestNutrientResult
        (
            TestID,
            AIMNutrientID,
            TestGroupNameID,
            TestNutrientResultCurrentUpcharge,
            TestNutrientResultPreviousUpcharge,
            UpdateStamp,
            CFCUserID
        )
        VALUES
        (
            @TestID,
            @AIMNutrientID,
            @TestGroupNameID,
            @TestNutrientResultCurrentUpcharge,
            @TestNutrientResultPreviousUpcharge,
            GETDATE(),
            @CFCUserID
        )

        SET @RecordID = @@IDENTITY
    END
    ELSE
    BEGIN
        UPDATE dbo.TestNutrientResult WITH (ROWLOCK)
        SET
            TestID                 = ISNULL(@TestID, TestID),
            AIMNutrientID             = ISNULL(@AIMNutrientID, AIMNutrientID),
            TestGroupNameID        = ISNULL(@TestGroupNameID, TestGroupNameID),
            TestNutrientResultCurrentUpcharge = ISNULL(@TestNutrientResultCurrentUpcharge, TestNutrientResultCurrentUpcharge),
	    TestNutrientResultPreviousUpcharge = ISNULL(@TestNutrientResultPreviousUpcharge, TestNutrientResultPreviousUpcharge),
            UpdateStamp            = GETDATE(),
            CFCUserID                 = ISNULL(@CFCUserID, CFCUserID)
        WHERE
            TestNutrientResultID = @TestNutrientResultID

        SET @RecordID = @@IDENTITY
    END

    SELECT
          @RecordID AS RecordID
        , @ErrorNumber AS ErrorNumber
        , @ErrorMessage AS ErrorMessage
END