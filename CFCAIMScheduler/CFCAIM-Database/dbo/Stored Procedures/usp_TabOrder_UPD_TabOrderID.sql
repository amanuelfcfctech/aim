CREATE PROCEDURE [dbo].[usp_TabOrder_UPD_TabOrderID]
			  @TabOrderID				INT
			, @TabOrder					INT
			, @CFCUserID				INT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @TempTable TABLE(TID INT, TOrder INT);
DECLARE @DuplicateFound BIT = 1;


UPDATE
	dbo.TabOrder WITH(ROWLOCK)
		SET
			TabOrder = @TabOrder
		  , UpdateStamp = GETDATE()
		  , CFCUserID = @CFCUserID

		WHERE
			TabOrderID = @TabOrderID
IF EXISTS(SELECT TOP 1
			T.TabOrder 
		FROM
			dbo.TabOrder T WITH(NOLOCK)
		WHERE 
			T.TabOrder != 0
		GROUP BY
			T.TabOrder
		HAVING 
			COUNT(*) > 1)
	BEGIN
		UPDATE
			dbo.TabOrder WITH(ROWLOCK)
				SET
					  TabOrder = TabOrder + 1
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
						TabOrderID != @TabOrderID
					AND TabOrder >= @TabOrder
					AND TabOrder != 0
	END


SELECT 
--	  @@IDENTITY AS RecordID
	0 AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

