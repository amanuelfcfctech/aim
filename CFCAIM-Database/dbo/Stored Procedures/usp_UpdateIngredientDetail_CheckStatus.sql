CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_CheckStatus]
			@UpdateIngredientHeaderID	INT

			-- 1/25/24 - commented, not needed
			--, @UpdateName				NVARCHAR(30)	= NULL
			--, @CFCUserID				INT				= NULL
			--, @Status					INT				= NULL
			--, @IsOverlay				BIT				= NULL
			--, @AIMDatabaseID			INT				= NULL
			--, @UpdateStamp				DATETIME		= NULL
			--, @UpdateCFCUserID			INT				= NULL
			--, @StartDate				DATETIME		= NULL
			--, @EndDate					DATETIME		= NULL
			--, @ExportTargetID			INT				= NULL
			--, @AIMOwnerLocationID		INT				= NULL
			--, @CFCPlantID				INT				= NULL
			--, @ShortName				NVARCHAR(20)	= NULL

AS
BEGIN


SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0
Declare @RecordCount INT = 0

Declare @TotCnt INT = 0
Declare @RejectCnt INT = 0

SELECT 
	@TotCnt = COUNT(*) 
FROM 
	dbo.UpdateIngredientDetail 
WHERE 
	UpdateIngredientHeaderID = @UpdateIngredientHeaderID

SELECT 
	@RejectCnt = COUNT(*)  
FROM 
	dbo.UpdateIngredientDetail 
WHERE 
	UpdateIngredientHeaderID = @UpdateIngredientHeaderID 
	AND (Status = 1 
	OR Status = 3)

if @TotCnt = @RejectCnt
	Begin		
		SET @ErrorNumber = 2		
		SET @RecordID = @UpdateIngredientHeaderID		
	END
ELSE
	BEGIN
		SELECT 
			@RecordCount = Count(*)
		FROM 
			dbo.UpdateIngredientDetail 
		WHERE 
			UpdateIngredientHeaderID = @UpdateIngredientHeaderID
			AND	(Status IS NULL 
			OR Status = 0) 

		SET @RecordID = @UpdateIngredientHeaderID
	End

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @RecordCount AS RecordCount

END
