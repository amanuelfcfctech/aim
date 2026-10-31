CREATE PROCEDURE [dbo].[usp_SampleScreenDisplayGroupDetail_DEL_SampleScreenDisplayGroupDetailID]
                @SampleScreenDisplayGroupDetailID    INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber  INT = 0
DECLARE @ErrorMessage VARCHAR(50)

DELETE
    dbo.SampleScreenDisplayGroupDetail
WHERE
    SampleScreenDisplayGroupDetailID = @SampleScreenDisplayGroupDetailID

SET @ErrorNumber = @@ERROR

IF @ErrorNumber > 0
    BEGIN
        SET @ErrorMessage = 'Sample Screen Display Group Detail did not get deleted'
    END

SELECT
      0 AS RecordID
    , @ErrorNumber AS ErrorNumber
    , @ErrorMessage AS ErrorMessage
END