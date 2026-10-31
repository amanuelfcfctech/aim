CREATE PROCEDURE [dbo].[usp_TabOrderGroup_UPD_TabOrderGroupID]
			  @TabOrderGroupID			INT
			, @TabOrderGroupCode		NVARCHAR(50)
			, @CFCUserID				INT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @TempTable TABLE(TID INT, TOrder INT);
DECLARE @DuplicateFound BIT = 1;



IF EXISTS(SELECT TOP 1
			T.TabOrderGroupID 
		FROM
			dbo.TabOrderGroup T
		WHERE 
			T.TabOrderGroupID = @TabOrderGroupID)
	BEGIN
		UPDATE
			dbo.TabOrderGroup
				SET
					  TabOrderGroupCode = @TabOrderGroupCode
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
					TabOrderGroupID = @TabOrderGroupID

	END
ELSE
	INSERT INTO
		dbo.TabOrderGroup
		(
			 TabOrderGroupCode
			,CFCUserID
			,UpdateStamp
		)
	VALUES
		(
			 @TabOrderGroupCode
			,@CFCUserID
			,GETDATE()
		)


SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END