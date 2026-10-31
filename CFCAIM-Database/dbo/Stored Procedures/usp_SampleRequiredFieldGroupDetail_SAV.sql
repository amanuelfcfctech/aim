CREATE PROCEDURE dbo.usp_SampleRequiredFieldGroupDetail_SAV
      @SampleRequiredFieldGroupDetailID INT
    , @SampleRequiredFieldGroupID       INT = NULL
    , @RequiredControlID                INT = NULL
    , @IsActive                         BIT = NULL
    , @CFCUserID                        INT = NULL
--exec usp_SampleRequiredFieldGroupDetail_SAV 0, 1, 123, 1, 1
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorNumber INT = 0
    DECLARE @RecordID INT = 0
    DECLARE @ErrorMessage VARCHAR(200) = ''
    IF @SampleRequiredFieldGroupDetailID = 0
        AND EXISTS
        (
            SELECT 1
            FROM dbo.SampleRequiredFieldGroupDetail WITH(NOLOCK)
            WHERE SampleRequiredFieldGroupID = @SampleRequiredFieldGroupID
            AND RequiredControlID = @RequiredControlID
            AND IsActive = 1
        )
    BEGIN
        SET @ErrorNumber = 1
        SET @ErrorMessage = 'This field is already in the group.'
    END
    ELSE IF @SampleRequiredFieldGroupDetailID = 0
    BEGIN
        INSERT dbo.SampleRequiredFieldGroupDetail
        (
              SampleRequiredFieldGroupID
            , RequiredControlID
            , IsActive
            , CFCUserID
            , UpdateStamp
        )
        VALUES
        (
              @SampleRequiredFieldGroupID
            , @RequiredControlID
            , @IsActive
            , @CFCUserID
            , GETDATE()
        )
        SET @RecordID = @@IDENTITY
    END
    ELSE
    BEGIN
        UPDATE dbo.SampleRequiredFieldGroupDetail WITH(ROWLOCK)
        SET   SampleRequiredFieldGroupID = ISNULL(@SampleRequiredFieldGroupID, SampleRequiredFieldGroupID)
            , RequiredControlID          = ISNULL(@RequiredControlID, RequiredControlID)
            , IsActive                   = ISNULL(@IsActive, IsActive)
            , CFCUserID                  = ISNULL(@CFCUserID, CFCUserID)
            , UpdateStamp                = GETDATE()
        WHERE SampleRequiredFieldGroupDetailID = @SampleRequiredFieldGroupDetailID
        SET @RecordID = @SampleRequiredFieldGroupDetailID
    END
    SELECT @RecordID AS RecordID
         , @ErrorNumber AS ErrorNumber
         , @ErrorMessage AS ErrorMessage
END