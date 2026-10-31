CREATE PROCEDURE dbo.usp_SampleRequiredFieldGroupDetail_DEL
      @SampleRequiredFieldGroupDetailID INT
--exec usp_SampleRequiredFieldGroupDetail_DEL 1
AS
BEGIN
    SET NOCOUNT ON;
    DELETE dbo.SampleRequiredFieldGroupDetail
    WHERE SampleRequiredFieldGroupDetailID = @SampleRequiredFieldGroupDetailID
END