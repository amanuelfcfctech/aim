CREATE PROCEDURE [dbo].[usp_Sample_COUNT_By_AimID]
		@ID			INT	  
	  , @Name		NVARCHAR(20)
	  , @IDSecond	INT	= NULL			
	  

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @Val INT = 0
DECLARE @UnitDesc NVARCHAR(30)

IF @Name = 'Material'
	SET @Val = (SELECT TOP 1 AIMMaterialID FROM dbo.Sample S WHERE S.AIMMaterialID = @ID)	
	
IF @Name = 'Nutrient'
	SET @Val = (SELECT TOP 1 AIMNutrientID FROM dbo.Sample S 
			INNER JOIN dbo.SampleResult SR on S.SampleID = SR.SampleID
			INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient X on SR.AnalyzerNutrientID = X.AnalyzerNutrientID
			WHERE X.AIMNutrientID = @ID)	

IF @Name = 'Owner'
	SET @Val = (SELECT TOP 1 AIMOwnerID FROM dbo.Sample S WHERE S.AIMOwnerID = @ID)	

IF @Name = 'OwnerLocation'
	SET @Val = (SELECT TOP 1 AIMOwnerLocationID FROM dbo.Sample S WHERE S.AIMOwnerLocationID = @ID and S.AIMOwnerID = @IDSecond)
	
IF @Name = 'Supplier'
	SET @Val = (SELECT TOP 1 AIMSourceID FROM dbo.Sample S WHERE S.AIMSourceID = @ID)

IF @Name = 'SupplierLocation'
	SET @Val = (SELECT TOP 1 AIMSourceLocationID FROM dbo.Sample S WHERE S.AIMSourceLocationID = @ID and S.AIMSourceID = @IDSecond)
	
IF @Name = 'Unit'
	Begin		
		SET @UnitDesc = (SELECT TOP 1 UnitTypeDescription FROM UnitType WHERE UnitTypeID = @ID)
		SET @Val = (SELECT TOP 1 SampleID FROM SampleResult WHERE Unit = @UnitDesc)
		IF @Val = 0					
			SET @Val = (SELECT TOP 1 UnitTypeID FROM AIMNutrient WHERE UnitTypeID = @ID)		
	End						


SELECT 
	  @Val as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	
END