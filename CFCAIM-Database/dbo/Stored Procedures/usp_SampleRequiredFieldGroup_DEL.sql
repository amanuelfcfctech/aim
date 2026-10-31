CREATE PROCEDURE dbo.usp_SampleRequiredFieldGroup_DEL
      @SampleRequiredFieldGroupID INT
--exec usp_SampleRequiredFieldGroup_DEL 1
AS
BEGIN
    SET NOCOUNT ON;
    DELETE dbo.SampleRequiredFieldGroupDetail
    WHERE SampleRequiredFieldGroupID = @SampleRequiredFieldGroupID
    DELETE dbo.SampleRequiredFieldGroup
    WHERE SampleRequiredFieldGroupID = @SampleRequiredFieldGroupID
END