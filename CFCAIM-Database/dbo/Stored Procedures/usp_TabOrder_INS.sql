CREATE PROCEDURE [dbo].[usp_TabOrder_INS]
			    @FormID			INT
			  , @UserID			INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @TempTable TABLE(TID INT, TOrder INT);
DECLARE @DuplicateFound BIT = 1;


INSERT
	INTO dbo.TabOrder
	(
	  UserID
	, ControlID
	, TabOrder
	)
SELECT
	  @UserID
	, ControlID 
	, 0
FROM 
	dbo.Control 
WHERE 
	FormID = @FormID		
END

