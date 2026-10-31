CREATE PROCEDURE [dbo].[usp_SampleScreenDisplayGroupDetail_INS]
              @SampleScreenDisplayGroupID    INT
            , @ControlID                     INT
            , @CFCUserID                     INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber  INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF EXISTS(SELECT TOP 1
            SSDGD.SampleScreenDisplayGroupDetailID
          FROM
            dbo.SampleScreenDisplayGroupDetail SSDGD
          WHERE
            SSDGD.SampleScreenDisplayGroupID = @SampleScreenDisplayGroupID
            AND SSDGD.ControlID = @ControlID)
    BEGIN
        SET @ErrorMessage = 'That column is already in this group.'
    END
ELSE
    BEGIN
        INSERT INTO
            dbo.SampleScreenDisplayGroupDetail
            (
                 SampleScreenDisplayGroupID
                ,ControlID
                ,UpdateStamp
                ,CFCUserID
            )
        VALUES
            (
                 @SampleScreenDisplayGroupID
                ,@ControlID
                ,GETDATE()
                ,@CFCUserID
            )
    END

SELECT
      @@IDENTITY AS RecordID
    , @ErrorNumber AS ErrorNumber
    , @ErrorMessage AS ErrorMessage
END