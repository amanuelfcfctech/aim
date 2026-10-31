
CREATE PROCEDURE [dbo].[usp_Test_DEL]
    @TestID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM dbo.Test 
    WHERE
        TestID = @TestID

	DELETE
	FROM dbo.TestMaterial
	WHERE
		TestID = @TestID
	DELETE
	FROM
		dbo.TestNutrientResult
	WHERE
		TestID = @TestID
END