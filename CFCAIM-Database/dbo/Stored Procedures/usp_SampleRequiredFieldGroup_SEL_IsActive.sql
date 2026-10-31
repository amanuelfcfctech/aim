CREATE PROCEDURE dbo.usp_SampleRequiredFieldGroup_SEL_IsActive
      @IsActive BIT
--exec usp_SampleRequiredFieldGroup_SEL_IsActive 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RFG.SampleRequiredFieldGroupID
         , RFG.SampleRequiredFieldGroupCode
         , ISNULL(RFG.SpecificIngredientsGroupID, 0) AS SpecificIngredientsGroupID
         , ISNULL(RFG.SpecificProductsGroupID, 0) AS SpecificProductsGroupID
         , ISNULL(RFG.AIMOwnerLocationGroupID, 0) AS AIMOwnerLocationGroupID
         , RFG.IsActive
         , U.UserName
         , RFG.UpdateStamp
    FROM dbo.SampleRequiredFieldGroup RFG WITH(NOLOCK)
    LEFT OUTER JOIN CFCUser.CFCUser U WITH(NOLOCK)
        ON RFG.CFCUserID = U.CFCUserID
    WHERE RFG.IsActive = @IsActive
    ORDER BY RFG.SampleRequiredFieldGroupCode
END