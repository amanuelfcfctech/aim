CREATE PROCEDURE [dbo].[usp_SampleScreenDisplayGroup_DEL_SampleScreenDisplayGroupID]
                @SampleScreenDisplayGroupID    INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber  INT = 0
DECLARE @ErrorMessage VARCHAR(50)

DELETE
    dbo.SampleScreenDisplayGroupDetail
WHERE
    SampleScreenDisplayGroupID = @SampleScreenDisplayGroupID

SET @ErrorNumber = @@ERROR

IF @ErrorNumber > 0
    BEGIN
        SET @ErrorMessage = 'Sample Screen Display Group Detail did not get deleted'
    END
ELSE
    BEGIN
        DELETE
            dbo.SampleScreenDisplayGroup
        WHERE
            SampleScreenDisplayGroupID = @SampleScreenDisplayGroupID

        SET @ErrorNumber = @@ERROR

        IF @ErrorNumber > 0
            BEGIN
                SET @ErrorMessage = 'Sample Screen Display Group did not get deleted'
            END
    END

SELECT
      0 AS RecordID
    , @ErrorNumber AS ErrorNumber
    , @ErrorMessage AS ErrorMessage
END