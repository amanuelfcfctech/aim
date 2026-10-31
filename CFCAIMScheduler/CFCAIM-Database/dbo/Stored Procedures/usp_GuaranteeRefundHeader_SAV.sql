CREATE PROCEDURE [dbo].[usp_GuaranteeRefundHeader_SAV]
			  @GuaranteeRefundHeaderID	INT
			, @BatchID					INT	= NULL
			, @BatchSentDate			DATETIME		= NULL
			, @AIMSupplierID			INT	= NULL
			, @AIMSupplierLocationID	INT	= NULL
			, @AIMMaterialID			INT	= NULL			
			, @CFCUserID				INT				= NULL
					
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @GuaranteeRefundHeaderID = 0 
	BEGIN			
		INSERT
			dbo.GuaranteeRefundHeader
				(
				  BatchID
				, BatchSentDate
				, AIMSupplierID
				, AIMSupplierLocationID
				, AIMMaterialID				
				, UpdateStamp
				, CFCUserID								
				)
		VALUES
				(
				  @BatchID
				, @BatchSentDate
				, @AIMSupplierID
				, @AIMSupplierLocationID
				, @AIMMaterialID				
				, GETDATE()
				, @CFCUserID			
				)
		SET @RecordID = @@IDENTITY
		
	END
ELSE
	BEGIN	
		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.GuaranteeRefundHeader WITH(ROWLOCK)
						SET 							  
							  BatchID = ISNULL(@BatchID, BatchID)
							, BatchSentDate = ISNULL(@BatchSentDate, BatchSentDate)
							, AIMSupplierID = ISNULL(@AIMSupplierID, AIMSupplierID)
							, AIMSupplierLocationID = ISNULL(@AIMSupplierLocationID, AIMSupplierLocationID)
							, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
							, UpdateStamp = GETDATE()
							, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
							
						WHERE 
							GuaranteeRefundHeaderID = @GuaranteeRefundHeaderID
				SET @RecordID = @GuaranteeRefundHeaderID
			END
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END