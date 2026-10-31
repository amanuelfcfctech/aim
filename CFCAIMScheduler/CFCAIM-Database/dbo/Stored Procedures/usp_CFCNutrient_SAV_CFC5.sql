
CREATE PROCEDURE [dbo].[usp_CFCNutrient_SAV_CFC5]
			  @CFCNutrientID					INT
			, @CFCNutrientCode					VARCHAR(10)		= NULL
			, @CFCNutrientName					VARCHAR(30)		= NULL
			, @Unit								VARCHAR(15)		= NULL
			, @IsActive							BIT				= NULL
			, @CFCUserID						INT				= NULL	
			, @MathTypeID						INT				= NULL
			, @AimDatabaseID					INT				= NULL
			, @IsComputed						BIT				= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @CFCNutrientID = 0 
	BEGIN
		SELECT 
			@CFCNutrientID = CFCNutrientID 
		FROM 
			dbo.CFCNutrient CN
		WHERE 
			CN.CFCNutrientCode = @CFCNutrientCode 
			AND CN.AIMDatabaseID = @AimDatabaseID
	END

IF @CFCNutrientID = 0 
	BEGIN
		INSERT
			dbo.CFCNutrient
				(
				  CFCNutrientCode
				, CFCNutrientName
				, Unit 
				, UpdateStamp
				, CFCUserID
				, MathTypeID
				, AIMDatabaseID
				, IsComputed
				)
		VALUES
				(
				  @CFCNutrientCode
				, @CFCNutrientName
				, @Unit 
				, GETDATE()
				, @CFCUserID
				, @MathTypeID
				, @AimDatabaseID
				, @IsComputed
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.CFCNutrient WITH(ROWLOCK)
		SET 					  							
			  CFCNutrientName = ISNULL(@CFCNutrientName, CFCNutrientName)
			, Unit = ISNULL(@Unit, Unit)
			, IsActive = ISNULL(@IsActive, IsActive)
			, UpdateStamp = GETDATE()
			, CFCUserID = @CFCUserID
			, MathTypeID = ISNULL(@MathTypeID, MathTypeID)
			, AIMDatabaseID = ISNULL(@AimDatabaseID, AIMDatabaseID)
			, IsComputed = ISNULL(@IsComputed, IsComputed)
		WHERE 
			CFCNutrientID = @CFCNutrientID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END