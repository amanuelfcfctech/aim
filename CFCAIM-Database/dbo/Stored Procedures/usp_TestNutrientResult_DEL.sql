
CREATE PROCEDURE dbo.usp_TestNutrientResult_DEL
    @TestNutrientResultID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM dbo.TestNutrientResult
    WHERE
        TestNutrientResultID = @TestNutrientResultID
END