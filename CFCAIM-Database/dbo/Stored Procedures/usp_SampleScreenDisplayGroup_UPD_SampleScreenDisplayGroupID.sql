CREATE PROCEDURE [dbo].[usp_SampleScreenDisplayGroup_UPD_SampleScreenDisplayGroupID]
              @SampleScreenDisplayGroupID    INT
            , @SampleScreenDisplayGroupCode  NVARCHAR(50)
            , @CFCUserID                     INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber  INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF EXISTS(SELECT TOP 1
            S.SampleScreenDisplayGroupID
          FROM
            dbo.SampleScreenDisplayGroup S
          WHERE
            S.SampleScreenDisplayGroupID = @SampleScreenDisplayGroupID)
    BEGIN
        UPDATE
            dbo.SampleScreenDisplayGroup
                SET
                      SampleScreenDisplayGroupCode = @SampleScreenDisplayGroupCode
                    , UpdateStamp = GETDATE()
                    , CFCUserID = @CFCUserID
                WHERE
                    SampleScreenDisplayGroupID = @SampleScreenDisplayGroupID
    END
ELSE
    INSERT INTO
        dbo.SampleScreenDisplayGroup
        (
             SampleScreenDisplayGroupCode
            ,CFCUserID
            ,UpdateStamp
        )
    VALUES
        (
             @SampleScreenDisplayGroupCode
            ,@CFCUserID
            ,GETDATE()
        )

SELECT
      @@IDENTITY AS RecordID
    , @ErrorNumber AS ErrorNumber
    , @ErrorMessage AS ErrorMessage
END