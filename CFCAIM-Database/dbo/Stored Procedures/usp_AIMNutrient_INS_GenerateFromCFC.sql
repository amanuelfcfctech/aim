CREATE PROCEDURE [dbo].[usp_AIMNutrient_INS_GenerateFromCFC]
			  @CFCNutrientID					INT
			, @CFCUserID						INT = NULL
						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @UnitTypeID			INT
DECLARE @AIMNutrientID		INT	

SELECT 
	@UnitTypeID = UT.UnitTypeID 
FROM 
	dbo.CFCNutrient N
	INNER JOIN 
		dbo.UnitType UT
			ON N.Unit = UT.UnitTypeCode 
WHERE 
	N.CFCNutrientID = @CFCNutrientID

IF @UnitTypeID IS NULL
	BEGIN
		INSERT INTO 
			dbo.UnitType 
				(
					  UnitTypeCode
					, UnitTypeDescription
					, UpdateStamp
					, CFCUserID 
					, IsActive
				)
		SELECT 
			  Unit
			, Unit
			, GETDATE()
			, @CFCUserID
			, 1
		FROM 
			dbo.CFCNutrient N
		WHERE 
			N.CFCNutrientID = @CFCNutrientID

		SET @UnitTypeID = @@IDENTITY 
	END

IF EXISTS(SELECT 
				AIMNutrientID 	
			FROM 
				dbo.CFCNutrient N
				INNER JOIN
					dbo.AIMNutrient AN
						ON AN.AIMNutrientCode = N.CFCNutrientCode 
			WHERE 
				N.CFCNutrientID = @CFCNutrientID)
	BEGIN
		SET @ErrorMessage = 'Nutrient Code ready exists'
		SET @ErrorNumber = 1
	END
ELSE
	BEGIN
		INSERT INTO 
			dbo.AIMNutrient 
				(
					  AIMNutrientCode 
					, AIMNutrientName 
					, AIMNutrientShortName
					, UnitTypeID
					, MathTypeID 			 
					, PriorityAIMUsedValue
					, DataSourceTypeID
					, IsActive 
					, UpdateStamp 
					, CFCUserID 
				)
		SELECT 
			  CFCNutrientCode
			, CFCNutrientName
			, CFCNutrientName
			, @UnitTypeID
			, 0
			, 1
			, 0
			, 1
			, GETDATE()
			, @CFCUserID
		FROM 
			dbo.CFCNutrient N
		WHERE 
			N.CFCNutrientID = @CFCNutrientID

		SET @AIMNutrientID = @@IDENTITY

		INSERT INTO	
			dbo.CFCNutrientRefAIMNutrient 
				(
					  CFCNutrientID
					, AIMNutrientID 
					, UpdateStamp
					, CFCUserID
				)
		VALUES
			(
				  @CFCNutrientID
				, @AIMNutrientID
				, GETDATE()
				, @CFCUserID
			)
	END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END