CREATE PROCEDURE [dbo].[usp_GuaranteeRefundDetail_SAV]
			  @GuaranteeRefundDetailID		INT				
			, @GuaranteeRefundHeaderID		INT				= NULL
			, @SampleID						INT				= NULL			
			, @AIMNutrientID				INT				= NULL			
			, @ResultValue					NUMERIC(18, 8)	= NULL
			, @GuaranteeMin					NUMERIC(18, 8)	= NULL
			, @GuaranteeMax					NUMERIC(18, 8)	= NULL
			, @SampleCode					NVARCHAR(20)	= NULL
			, @LoadNumber					NVARCHAR(20)	= NULL
			, @SampleDate					DATE			= NULL
			, @CalculatedValue				NUMERIC(18, 8)	= NULL
			, @Status						INT				= NULL
			, @CFCUserID					INT				= NULL
			, @UpdateStamp					DATETIME		= NULL			
			
AS
BEGIN


SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @GuaranteeRefundDetailID = 0
	BEGIN
		INSERT INTO
			dbo.GuaranteeRefundDetail 
				(
				  GuaranteeRefundHeaderID 
				, SampleID				
				, AIMNutrientID 				
				, ResultValue
				, GuaranteeMin 
				, GuaranteeMax
				, SampleCode
				, LoadNumber
				, SampleDate
				, CalculatedValue
				, CFCUserID 
				, UpdateStamp 				
				)
		VALUES
				(
				  @GuaranteeRefundHeaderID 
				, @SampleID				
				, @AIMNutrientID 				
				, @ResultValue
				, @GuaranteeMin 
				, @GuaranteeMax
				, @SampleCode
				, @LoadNumber
				, @SampleDate
				, @CalculatedValue
				, @CFCUserID 
				, GETDATE() 
				)
		SET @RecordID = @@IDENTITY
	END
ELSE			
	BEGIN
		UPDATE 
			dbo.GuaranteeRefundDetail
		SET
			  GuaranteeRefundHeaderID = ISNULL(@GuaranteeRefundDetailID, GuaranteeRefundHeaderID)
			, SampleID = ISNULL(@SampleID, SampleID)			
			, AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)			
			, ResultValue = ISNULL(@ResultValue, ResultValue)
			, GuaranteeMin = ISNULL(@GuaranteeMin, GuaranteeMin)
			, GuaranteeMax = ISNULL(@GuaranteeMax, GuaranteeMax)
			, SampleCode = ISNULL(@SampleCode, SampleCode)
			, LoadNumber = ISNULL(@LoadNumber, LoadNumber)
			, SampleDate = ISNULL(@SampleDate, SampleDate)
			, CalculatedValue = ISNULL(@CalculatedValue, CalculatedValue)
			, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			, UpdateStamp = ISNULL(@UpdateStamp, UpdateStamp)
			
			

		WHERE
			GuaranteeRefundDetailID = @GuaranteeRefundDetailID
			
		SET @RecordID = @GuaranteeRefundDetailID
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END