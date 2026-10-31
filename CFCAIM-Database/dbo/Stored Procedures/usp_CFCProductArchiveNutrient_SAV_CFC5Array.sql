create PROCEDURE [dbo].[usp_CFCProductArchiveNutrient_SAV_CFC5Array]
			  @CFCProductArchiveID			INT				= NULL
			, @RowData						VARCHAR(6000)
			, @CFCUserID					INT				= NULL
			, @AIMDatabaseID				INT				= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @SplitOn			VARCHAR(2) = '~'
DECLARE @SplitRecord		VARCHAR(2) = '~~'
DECLARE @CFCNutrientCode	VARCHAR(8)
DECLARE @NutrientValue		NUMERIC(18,9)
DECLARE @RecordID			INT = 0
DECLARE @CFCNutrientID		INT	= 0
DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''

WHILE (CHARINDEX(@SplitOn, @RowData) > 0 ) 
    BEGIN  
		SET @CFCNutrientCode = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitOn, @RowData) + 1, LEN(@RowData)) 
		SET @NutrientValue = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitRecord, @RowData) + 2, LEN(@RowData)) 
		
		SELECT 
			@CFCNutrientID = CFCNutrientID
		FROM 
			dbo.CFCNutrient CN
		WHERE
			CN.CFCNutrientCode = @CFCNutrientCode
			AND CN.AIMDatabaseID = @AIMDatabaseID


		IF EXISTS(SELECT
						CFCProductArchiveID
					FROM 
						dbo.CFCProductArchiveNutrient
					WHERE
						CFCProductArchiveID = @CFCProductArchiveID
						AND CFCNutrientID = @CFCNutrientID)
			BEGIN
				UPDATE
					dbo.CFCProductArchiveNutrient
				SET			
					  CFCProductArchiveID = ISNULL(@CFCProductArchiveID, CFCProductArchiveID) 
					, CFCNutrientID = ISNULL(@CFCNutrientID, CFCNutrientID)
					, NutrientValue = ISNULL(@NutrientValue, NutrientValue)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
					CFCProductArchiveID = @CFCProductArchiveID
					AND CFCNutrientID = @CFCNutrientID 

				SET @RecordID = 0
			END
		ELSE
			BEGIN
				INSERT
					dbo.CFCProductArchiveNutrient
						(
							  CFCProductArchiveID
							, CFCNutrientID
							, NutrientValue
							, UpdateStamp
							, CFCUserID
						)
				VALUES
						(
							  @CFCProductArchiveID
							, @CFCNutrientID
							, @NutrientValue
							, GETDATE()
							, @CFCUserID
						)
				SET @RecordID = @@IDENTITY
			END	
	END
			
SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END