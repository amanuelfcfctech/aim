CREATE PROCEDURE [dbo].[usp_UpdateIngredientHeader_SAV]
			  @UpdateIngredientHeaderID	INT
			, @UpdateName				NVARCHAR(30)	= NULL
			, @CFCUserID				INT				= NULL
			, @Status					INT				= NULL
			, @ExportTargetID			INT				= NULL
			, @AIMDatabaseID			INT				= NULL
			, @UpdateStamp				DATETIME		= NULL
			, @UpdateCFCUserID			INT				= NULL
			, @StartDate				DATETIME		= NULL
			, @EndDate					DATETIME		= NULL
			, @CFCPlantID				INT				= NULL
			, @ShortName				NVARCHAR(20)	= NULL
			, @ExportExpandID			INT				= NULL
			, @ExportExpandName			NVARCHAR(20)	= NULL
			, @ExportPlantGroupID		INT				= NULL
			, @ExportPlantGroupName		NVARCHAR(20)	= NULL
			
			
AS
BEGIN
SET NOCOUNT ON;

--exec [usp_UpdateIngredientHeader_SAV] 30, 'demo 1', 2, 1, false, 0, '2015-01-21', 2

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @UpdateIngredientHeaderID = 0 
	BEGIN			
		INSERT
			dbo.UpdateIngredientHeader
				(
				  UpdateName
				, [Status]
				, ExportTargetID
				, CreateDate
				, CFCUserID					
				, AIMDatabaseID
				, CFCPlantID
				, ShortName
				, UpdateCFCUserID
				, ExportExpandID
				, ExportExpandName
				, ExportPlantGroupID
				, ExportPlantGroupName
				--, UpdateStamp					--2/7/24 -->   commented 6/24/24 per Mark
				)
		VALUES
				(
				  @UpdateName
				, @Status
				, @ExportTargetID
				, GETDATE()
				, @CFCUserID
				, @AIMDatabaseID	
				, @CFCPlantID	
				, @ShortName
				, @CFCUserID
				, @ExportExpandID
				, @ExportExpandName
				, @ExportPlantGroupID
				, @ExportPlantGroupName
				--, GetDate()					--2/7/24 -->   commented 6/24/24 per Mark
				)
		SET @RecordID = @@IDENTITY
		
	END
ELSE
	BEGIN	
		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.UpdateIngredientHeader WITH(ROWLOCK)
						SET 							  
							  UpdateName = ISNULL(@UpdateName, UpdateName)
							, [Status] = ISNULL(@Status, [Status])
							, ExportTargetID = ISNULL(@ExportTargetID, ExportTargetID)
							, CreateDate = GETDATE()
							, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
							, AIMDatabaseID = ISNULL(@AIMDatabaseID, AIMDatabaseID)
							, UpdateStamp = ISNULL(@UpdateStamp, UpdateStamp)
							, UpdateCFCUserID = ISNULL(@UpdateCFCUserID, UpdateCFCUserID)
							, CFCPlantID = ISNULL(@CFCPlantID, CFCPlantID)
							, ShortName = ISNULL(@ShortName, ShortName)
							, ExportExpandID = ISNULL(@ExportExpandID, ExportExpandID)
							, ExportExpandName = ISNULL(@ExportExpandName, ExportExpandName)
							, ExportPlantGroupID = ISNULL(@ExportPlantGroupID, ExportPlantGroupID)
							, ExportPlantGroupName = ISNULL(@ExportPlantGroupName, ExportPlantGroupName)

						WHERE 
							UpdateIngredientHeaderID = @UpdateIngredientHeaderID
				SET @RecordID = @UpdateIngredientHeaderID
			END
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
