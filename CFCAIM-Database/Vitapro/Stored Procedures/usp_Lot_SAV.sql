CREATE PROCEDURE [Vitapro].[usp_Lot_SAV]
			  @LotID						INT
			, @SampleID						INT
			, @LotStatusID					INT = NULL
			, @LotStatusUpdate				DATETIME = NULL			
			, @ProductionStatusLine1		INT = NULL
			, @ProductionStatusLine2		INT = NULL
			, @ProductionStatusLine3		INT = NULL
			, @ProductionStatusLine4		INT = NULL
			, @ExportStatus					INT = NULL
			
			--, @AmountAvailable				NUMERIC(18,8) = NULL
			, @AmountAvailable			INT = NULL

			, @CFCUserID					INT
			, @DepletedDate					DATETIME = NULL		

			-- 9/2/20
			, @Concept5ExportStatus			INT = NULL
			
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @LotID = 0 
	BEGIN
		INSERT		
			Vitapro.Lot
				(
				  SampleID
				, LotStatusID
				, LotStatusUpdate				
				, ProductionStatusLine1
				, ProductionStatusLine2
				, ProductionStatusLine3
				, ProductionStatusLine4
				, ExportStatus
				, AmountAvailable
				, UpdateStamp
				, CFCUserID
				, DepletedDate
				, Concept5ExportStatus
						
				)
		VALUES
				(			
				@SampleID	
				, @LotStatusID
				, @LotStatusUpdate				
				, @ProductionStatusLine1
				, @ProductionStatusLine2
				, @ProductionStatusLine3
				, @ProductionStatusLine4
				, @ExportStatus
				, @AmountAvailable
				, GETDATE()
				, @CFCUserID
				, @DepletedDate
				, @Concept5ExportStatus
						
				)
		SET @RecordID = @@IDENTITY 
	END	
ELSE
	BEGIN
		UPDATE
			Vitapro.Lot WITH(ROWLOCK)
				SET 
					  SampleID = ISNULL(@SampleID, SampleID)
					, LotStatusID = ISNULL(@LotStatusID, LotStatusID)
					, LotStatusUpdate = ISNULL(@LotStatusUpdate, LotStatusUpdate)					
					, ProductionStatusLine1 = ISNULL(@ProductionStatusLine1, ProductionStatusLine1)
					, ProductionStatusLine2 = ISNULL(@ProductionStatusLine2, ProductionStatusLine2)
					, ProductionStatusLine3 = ISNULL(@ProductionStatusLine3, ProductionStatusLine3)
					, ProductionStatusLine4 = ISNULL(@ProductionStatusLine4, ProductionStatusLine4)
					, ExportStatus = ISNULL(@ExportStatus, ExportStatus)
					, AmountAvailable = ISNULL(@AmountAvailable, AmountAvailable)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					, DepletedDate = ISNULL(@DepletedDate, DepletedDate)
					, Concept5ExportStatus = ISNULL(@Concept5ExportStatus, Concept5ExportStatus)
					
				WHERE 
					LotID = @LotID
		SET @RecordID = @LotID
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END