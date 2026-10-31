CREATE PROCEDURE dbo.usp_SampleRequiredFieldGroupDetail_SEL_GroupID
      @SampleRequiredFieldGroupID INT
--exec usp_SampleRequiredFieldGroupDetail_SEL_GroupID 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RFD.SampleRequiredFieldGroupDetailID
         , RFD.SampleRequiredFieldGroupID
         , RFD.RequiredControlID
         , C.ControlName
         , C.UserControlName
         , ISNULL(C.UserControlName, C.ControlName) AS FieldDisplayName
         , RFD.IsActive
         , U.UserName
         , RFD.UpdateStamp
    FROM dbo.SampleRequiredFieldGroupDetail RFD WITH(NOLOCK)
    INNER JOIN dbo.Control C WITH(NOLOCK)
        ON RFD.RequiredControlID = C.ControlID
    LEFT OUTER JOIN CFCUser.CFCUser U WITH(NOLOCK)
        ON RFD.CFCUserID = U.CFCUserID
    WHERE RFD.SampleRequiredFieldGroupID = @SampleRequiredFieldGroupID
    AND RFD.IsActive = 1
    ORDER BY ISNULL(C.UserControlName, C.ControlName)
END