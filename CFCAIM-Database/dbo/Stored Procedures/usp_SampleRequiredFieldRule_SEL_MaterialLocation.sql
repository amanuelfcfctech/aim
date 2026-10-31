CREATE PROCEDURE dbo.usp_SampleRequiredFieldRule_SEL_MaterialLocation
      @AIMMaterialID              INT
    , @AIMOwnerLocationID         INT
    , @SampleRequiredFieldGroupID INT        --8/5/26	Task1155 RequiredField FG
--exec usp_SampleRequiredFieldRule_SEL_MaterialLocation 5, 3, 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT RFD.RequiredControlID
         , C.ControlName
         , C.UserControlName
         , ISNULL(C.UserControlName, C.ControlName) AS FieldDisplayName
    FROM dbo.SampleRequiredFieldGroupDetail RFD WITH(NOLOCK)
    INNER JOIN dbo.SampleRequiredFieldGroup RFG WITH(NOLOCK)
        ON RFD.SampleRequiredFieldGroupID = RFG.SampleRequiredFieldGroupID
    INNER JOIN dbo.Control C WITH(NOLOCK)
        ON RFD.RequiredControlID = C.ControlID
    WHERE RFG.IsActive = 1
    AND RFD.IsActive = 1
    AND RFG.SampleRequiredFieldGroupID = @SampleRequiredFieldGroupID        --8/5/26	Task1155 RequiredField FG
    AND
    (
        (RFG.SpecificIngredientsGroupID IS NULL AND RFG.SpecificProductsGroupID IS NULL)
        OR EXISTS
        (
            SELECT 1
            FROM dbo.SpecificIngredients SI WITH(NOLOCK)
            WHERE SI.SpecificIngredientsGroupID = RFG.SpecificIngredientsGroupID
            AND SI.AIMMaterialID = @AIMMaterialID
            AND SI.IsActive = 1
        )
        OR EXISTS
        (
            SELECT 1
            FROM dbo.SpecificProducts SP WITH(NOLOCK)
            WHERE SP.SpecificProductsGroupID = RFG.SpecificProductsGroupID
            AND SP.AIMMaterialID = @AIMMaterialID
            AND SP.IsActive = 1
        )
    )
    AND
    (
        RFG.AIMOwnerLocationGroupID IS NULL
        OR EXISTS
        (
            SELECT 1
            FROM dbo.AIMOwnerLocationGroupDetail OLD WITH(NOLOCK)
            WHERE OLD.AIMOwnerLocationGroupID = RFG.AIMOwnerLocationGroupID
            AND OLD.AIMOwnerLocationID = @AIMOwnerLocationID
            AND OLD.IsActive = 1
        )
    )
END