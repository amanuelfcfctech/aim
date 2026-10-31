CREATE PROCEDURE [dbo].[usp_CFCProductRefCFCProductType_SAV_CFC5Array]
	  @CFCPlantCode							VARCHAR(8)
	, @CFCProductCode						VARCHAR(8)
	, @InType								VARCHAR(200)
	, @CFCUserID							INT
	, @AIMDatabaseID						INT				= NULL
AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID		INT = 0
DECLARE @ResultNumber	INT = 0
DECLARE @ResultMessage	VARCHAR(200) = ''
DECLARE @LogStatus		CHAR = ''
DECLARE @CFCProductID	INT

SELECT 
	@CFCProductID = ISNULL(CP.CFCProductID, 0 )
FROM
	dbo.CFCPlant CPL
	INNER JOIN 
		dbo.CFCProduct CP
			ON CP.CFCPlantID = CPL.CFCPlantID 
			AND CP.AIMDatabaseID = @AIMDatabaseID
WHERE
	CPL.CFCPlantCode = @CFCPlantCode
	AND CP.CFCProductCode = @CFCProductCode

IF @CFCProductID > 0 
	BEGIN
		CREATE TABLE #InType 
			(
			   ID			INT IDENTITY(1,1) PRIMARY KEY
			 , Item			NVARCHAR(1000)
			)

		INSERT INTO #InType 
			(
				Item 
			)
		SELECT 
			CASE WHEN Item = 'Y' THEN 'Y' ELSE 'N' END
		FROM
			dbo.SplitString(@InType, '~')


		DELETE FROM 
			dbo.CFCProductRefCFCProductType
		FROM
			dbo.CFCProductRefCFCProductType CPRCPT
			INNER JOIN 
				dbo.CFCProductType CPT
					ON CPT.CFCProductTypeID = CPRCPT.CFCProductTypeID 
			INNER JOIN	
				#InType IT
				ON IT.ID = CPT.ProductTypeNumber 
				AND IT.Item = 'N'
		WHERE
			CPRCPT.CFCProductID = @CFCProductID 
			AND CPRCPT.AIMDatabaseID = @AIMDatabaseID
			

		SELECT 
			  CPRCPT.CFCProductRefCFCProductTypeID
			, CPT.CFCProductTypeID
			, CPT.ProductTypeNumber
		INTO 
			#ProductType
		FROM
			dbo.CFCProductRefCFCProductType CPRCPT
			INNER JOIN 
				dbo.CFCProductType CPT
					ON CPT.CFCProductTypeID = CPRCPT.CFCProductTypeID 
		WHERE
			CPRCPT.CFCProductID = @CFCProductID 
			AND CPRCPT.AIMDatabaseID = @AIMDatabaseID			
		
		INSERT INTO 
			dbo.CFCProductRefCFCProductType
				(
					  CFCProductID
					, CFCProductTypeID
					, UpdateStamp
					, CFCUserID
					, AIMDatabaseID
				)
		SELECT 
			  @CFCProductID
			, CPT.CFCProductTypeID 
			, GETDATE()
			, @CFCUserID
			, @AIMDatabaseID 
		FROM
			#InType IT
			INNER JOIN 
				dbo.CFCProductType CPT
					ON CPT.ProductTypeNumber = IT.ID 
					AND CPT.AIMDatabaseID = @AIMDatabaseID 
			LEFT OUTER JOIN 
				dbo.#ProductType PT
					ON PT.ProductTypeNumber = IT.ID
		WHERE
			PT.CFCProductRefCFCProductTypeID IS NULL
			AND IT.Item <> 'N'

		DROP TABLE #InType
		DROP TABLE #ProductType
	END
END