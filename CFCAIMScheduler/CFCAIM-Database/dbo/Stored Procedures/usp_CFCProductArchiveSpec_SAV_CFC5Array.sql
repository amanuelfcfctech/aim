create PROCEDURE [dbo].[usp_CFCProductArchiveSpec_SAV_CFC5Array]
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
DECLARE @NutrientMin		NUMERIC(18,9)
DECLARE @NutrientMax		NUMERIC(18,9)
DECLARE @RecordID			INT = 0
DECLARE @CFCNutrientID		INT	= 0
DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''


WHILE (CHARINDEX(@SplitOn, @RowData) > 0 ) 
    BEGIN  
		SET @CFCNutrientCode = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitOn, @RowData) + 1, LEN(@RowData)) 
		SET @NutrientMin = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitOn, @RowData) + 1, LEN(@RowData)) 
		SET @NutrientMax = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
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
					dbo.CFCProductArchiveSpec
				SET			
					  CFCProductArchiveID = ISNULL(@CFCProductArchiveID, CFCProductArchiveID) 
					, CFCNutrientID = ISNULL(@CFCNutrientID, CFCNutrientID)
					, NutrientMin = ISNULL(@NutrientMin, NutrientMin)
					, NutrientMax = ISNULL(@NutrientMax, NutrientMax)
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
					dbo.CFCProductArchiveSpec
						(
							  CFCProductArchiveID
							, CFCNutrientID
							, NutrientMin
							, NutrientMax
							, UpdateStamp
							, CFCUserID
						)
				VALUES
						(
								@CFCProductArchiveID
							, @CFCNutrientID
							, @NutrientMin
							, @NutrientMax
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