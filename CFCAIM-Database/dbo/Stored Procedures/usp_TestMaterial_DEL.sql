CREATE PROCEDURE dbo.usp_TestMaterial_DEL
    @TestMaterialID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM dbo.TestMaterial 
    WHERE
        TestMaterialID = @TestMaterialID
END