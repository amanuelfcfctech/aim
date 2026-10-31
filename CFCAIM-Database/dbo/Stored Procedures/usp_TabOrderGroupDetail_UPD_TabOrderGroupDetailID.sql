CREATE PROCEDURE [dbo].[usp_TabOrderGroupDetail_UPD_TabOrderGroupDetailID]
			  @TabOrderGroupDetailID			INT
			, @TabOrder							INT
			, @CFCUserID						INT
			

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @TempTable TABLE(TID INT, TOrder INT);
DECLARE @DuplicateFound BIT = 1;



IF EXISTS(SELECT TOP 1
			T.TabOrderGroupDetailID 
		FROM
			dbo.TabOrderGroupDetail T
		WHERE 
			T.TabOrderGroupDetailID = @TabOrderGroupDetailID)
	BEGIN
		UPDATE
			dbo.TabOrderGroupDetail
				SET
					  TabOrder = @TabOrder
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
					TabOrderGroupDetailID = @TabOrderGroupDetailID

	END



SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END