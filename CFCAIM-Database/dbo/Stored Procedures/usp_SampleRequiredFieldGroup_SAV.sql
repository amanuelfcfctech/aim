CREATE PROCEDURE dbo.usp_SampleRequiredFieldGroup_SAV
      @SampleRequiredFieldGroupID   INT
    , @SampleRequiredFieldGroupCode NVARCHAR(50) = NULL
    , @SpecificIngredientsGroupID   INT = NULL    -- 0 or NULL = not scoped by ingredient group
    , @SpecificProductsGroupID      INT = NULL    -- 0 or NULL = not scoped by product group
    , @AIMOwnerLocationGroupID      INT = NULL    -- 0 or NULL = all locations
    , @IsActive                     BIT = NULL
    , @CFCUserID                    INT = NULL
--exec usp_SampleRequiredFieldGroup_SAV 0, 'ELMIRA SOYBEANS', 0, 0, 0, 1, 1
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorNumber INT = 0
    DECLARE @RecordID INT = 0
    DECLARE @ErrorMessage VARCHAR(200) = ''
    IF NULLIF(@SpecificIngredientsGroupID, 0) IS NOT NULL AND NULLIF(@SpecificProductsGroupID, 0) IS NOT NULL
    BEGIN
        SET @ErrorNumber = 1
        SET @ErrorMessage = 'A group cannot use an ingredient group and a product group at the same time.'
    END
    ELSE IF EXISTS
    (
        SELECT 1
        FROM dbo.SampleRequiredFieldGroup WITH(NOLOCK)
        WHERE SampleRequiredFieldGroupCode = @SampleRequiredFieldGroupCode
        AND SampleRequiredFieldGroupID <> @SampleRequiredFieldGroupID
    )
    BEGIN
        SET @ErrorNumber = 2
        SET @ErrorMessage = 'Group code ''' + @SampleRequiredFieldGroupCode + ''' already exists.'
    END
    ELSE IF @SampleRequiredFieldGroupID = 0
    BEGIN
        INSERT dbo.SampleRequiredFieldGroup
        (
              SampleRequiredFieldGroupCode
            , SpecificIngredientsGroupID
            , SpecificProductsGroupID
            , AIMOwnerLocationGroupID
            , IsActive
            , CFCUserID
            , UpdateStamp
        )
        VALUES
        (
              @SampleRequiredFieldGroupCode
            , NULLIF(@SpecificIngredientsGroupID, 0)
            , NULLIF(@SpecificProductsGroupID, 0)
            , NULLIF(@AIMOwnerLocationGroupID, 0)
            , @IsActive
            , @CFCUserID
            , GETDATE()
        )
        SET @RecordID = @@IDENTITY
    END
    ELSE
    BEGIN
        UPDATE dbo.SampleRequiredFieldGroup WITH(ROWLOCK)
        SET   SampleRequiredFieldGroupCode = ISNULL(@SampleRequiredFieldGroupCode, SampleRequiredFieldGroupCode)
            , SpecificIngredientsGroupID   = NULLIF(@SpecificIngredientsGroupID, 0)
            , SpecificProductsGroupID      = NULLIF(@SpecificProductsGroupID, 0)
            , AIMOwnerLocationGroupID      = NULLIF(@AIMOwnerLocationGroupID, 0)
            , IsActive                     = ISNULL(@IsActive, IsActive)
            , CFCUserID                    = ISNULL(@CFCUserID, CFCUserID)
            , UpdateStamp                  = GETDATE()
        WHERE SampleRequiredFieldGroupID = @SampleRequiredFieldGroupID
        SET @RecordID = @SampleRequiredFieldGroupID
    END
    SELECT @RecordID AS RecordID
         , @ErrorNumber AS ErrorNumber
         , @ErrorMessage AS ErrorMessage
END