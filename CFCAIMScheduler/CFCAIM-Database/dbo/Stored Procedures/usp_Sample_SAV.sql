
CREATE PROCEDURE [dbo].[usp_Sample_SAV]
			  @SampleID						INT				
			, @SampleCode					NVARCHAR(20)	
			, @AlternateCode				NVARCHAR(30)	= NULL
			, @SampleDate					DATE  			= NULL
			, @AIMOwnerID					INT				= NULL
			, @AIMSourceID					INT				= NULL
			, @AIMSourceLocationID			INT				= NULL			
			, @ContractNumber				NVARCHAR(20)	= NULL
			, @PoNumber						NVARCHAR(20)	= NULL	
			, @InvoiceNumber				NVARCHAR(20)	= NULL
			, @TicketNumber					NVARCHAR(20)	= NULL	
			, @LoadNumber					NVARCHAR(20)	= NULL
			, @BillOfLoad					NVARCHAR(20)	= NULL
			, @VehicleNumber				NVARCHAR(20)	= NULL
			, @LotNumber					NVARCHAR(20)	= NULL				
			, @IsActive						BIT				= NULL	
			, @AIMMaterialID				INT				= NULL									
			, @AnalyzerMaterialCode			NVARCHAR(20)	= NULL --this one is being provided from file read
			, @AnalysisDate					DATETIME		= NULL			
			, @AIMOwnerLocationID			INT				= NULL
			, @FurtherAnalysisID			INT				= NULL
			, @CFCUserID					INT
			, @DataSourceTypeID				INT				= NULL
			, @AnalyzerSupplierCode 		NVARCHAR(20)	= NULL
			, @AnalyzerSupplierLocationCode NVARCHAR(20)	= NULL
			, @AnalyzerSupplierName 		NVARCHAR(30)	= NULL
			, @AnalyzerSupplierLocationName NVARCHAR(30)	= NULL
			, @CFCProductArchiveID			INT				= NULL

			-- 5/4/17 		
			, @AnalyzerMaterialID			INT				= NULL
			, @AnalyzerSupplierID			INT				= NULL
			, @AnalyzerSupplierLocationID	INT				= NULL
						
			, @Notes						VARCHAR(250)	= NULL			--2/12/18			
			, @ExpirationDate				DATETIME		= NULL			--5/29/18			
			, @SupportingDocumentPath		VARCHAR(200)	= NULL			--9/8/20			
			, @SampleTypeID					INT				= NULL			--1/22/21			
			, @IsGround						BIT				= NULL			--9/30/22			
			, @SampleTestTypeID				INT				= NULL			--8/22/23
			, @GroundUnGroundID				INT				= NULL			--1/19/24
			
 
--exec usp_Sample_SAV 0, '', 'test this again #10', '2015/06/09', 1, 8, 9, '', '', '', '', '', '', '', '', 1, 1, 1, null, '', null, 1, 0,2  			
--exec usp_Sample_SAV 1, 'UGA Tyson Corn', '', '2015/02/16', 1, 1, 1, '', '', '', '', '', '', '', '', 1, 1, null, null, 1, null, 2, 0, null, null, null, null, 'test2'		
--select * from sample where sampleid = 1

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

--5/4/17 - comment these and declared above.
--DECLARE @AnalyzerMaterialID	INT	= 0
--DECLARE @AnalyzerSupplierID	INT	= 0
--DECLARE @AnalyzerSupplierLocationID	INT	= 0


--6/19/18 - Added 'TOP 1' to select stmt below (Marilyn and Nickki have 2 recs in System Settings table!?)
--5/22/18 - Check SystemsSettings for auto-generated LotNumber and increment
DECLARE @AutoLotNumber INT = 0
SET @AutoLotNumber = (SELECT TOP 1 Case When AutoGenerateLotNumber = 1 then NextLotNumber else 0 end FROM dbo.SystemSettings SS)

IF @AutoLotNumber <> 0 and (@LotNumber IS NULL or @LotNumber = '')
	BEGIN
		SET @LotNumber = @AutoLotNumber
		UPDATE dbo.SystemSettings SET NextLotNumber = NextLotNumber + 1
	END


-- INSERT ANALYZER MATERIAL ------------------------------------------------------
IF ((@AnalyzerMaterialID = 0 OR @AnalyzerMaterialID = NULL) AND (Len(@AnalyzerMaterialCode) != 0 or @AnalyzerMaterialCode != NULL))
	BEGIN
	IF EXISTS(
			SELECT 
				AnalyzerMaterialID 
			FROM 
				dbo.AnalyzerMaterial 
			WHERE 
				AnalyzerMaterialCode = @AnalyzerMaterialCode 
					AND DataSourceTypeID = @DataSourceTypeID
			)
		BEGIN
			SET @AnalyzerMaterialID = (SELECT TOP 1 AnalyzerMaterialID 
										FROM dbo.AnalyzerMaterial 
										WHERE AnalyzerMaterialCode = @AnalyzerMaterialCode
												AND DataSourceTypeID = @DataSourceTypeID
												AND IsActive = 1)
		END
	ELSE
		BEGIN
			INSERT
				dbo.AnalyzerMaterial
					(
					  AnalyzerMaterialCode
					, AnalyzerMaterialName
					, DataSourceTypeID
					, CFCUserID
					)
					VALUES
					(
					  @AnalyzerMaterialCode
					, @AnalyzerMaterialCode
					, @DataSourceTypeID
					, @CFCUserID
					)
			SET @AnalyzerMaterialID = (SELECT TOP 1 AnalyzerMaterialID FROM dbo.AnalyzerMaterial 
										WHERE AnalyzerMaterialCode = @AnalyzerMaterialCode
											   AND DataSourceTypeID = @DataSourceTypeID
											   AND IsActive = 1)
		END
	END	
-- INSERT ANALYZER MATERIAL CROSS-REFERENCE --------------------------------------
IF NOT EXISTS(
			SELECT 
				AIMMaterialRefAnalyzerMaterialID 
			FROM 
				dbo.AIMMaterialRefAnalyzerMaterial XREF
			WHERE
				AIMMaterialID = @AIMMaterialID and AnalyzerMaterialID = @AnalyzerMaterialID)
		BEGIN
			INSERT
				dbo.AIMMaterialRefAnalyzerMaterial
					(
					  AIMMaterialID
					, AnalyzerMaterialID
					, UpdateStamp
					, CFCUserID
					)
					VALUES
					(
					  @AIMMaterialID
					, @AnalyzerMaterialID
					, GetDate()
					, @CFCUserID
					)
		END


-- INSERT ANALYZER SUPPLIER ------------------------------------------------------
IF ((@AnalyzerSupplierID = 0 OR @AnalyzerSupplierID = NULL) AND (Len(@AnalyzerSupplierCode) != 0 or @AnalyzerSupplierCode != NULL))
	BEGIN
	IF EXISTS(
			SELECT 
				AnalyzerSupplierID 
			FROM 
				dbo.AnalyzerSupplier 
			WHERE 
				AnalyzerSupplierCode = @AnalyzerSupplierCode 
					AND DataSourceTypeID = @DataSourceTypeID
			)
		BEGIN
			SET @AnalyzerSupplierID = (SELECT TOP 1 AnalyzerSupplierID 
										FROM dbo.AnalyzerSupplier 
										WHERE AnalyzerSupplierCode = @AnalyzerSupplierCode
												AND DataSourceTypeID = @DataSourceTypeID
												AND IsActive = 1)
		END
	ELSE
		BEGIN
			INSERT
				dbo.AnalyzerSupplier
					(
					  AnalyzerSupplierCode
					, AnalyzerSupplierName
					, DataSourceTypeID
					, CFCUserID
					)
					VALUES
					(
					  @AnalyzerSupplierCode
					, @AnalyzerSupplierName
					, @DataSourceTypeID
					, @CFCUserID
					)
			SET @AnalyzerSupplierID = (SELECT TOP 1 AnalyzerSupplierID FROM dbo.AnalyzerSupplier 
										WHERE AnalyzerSupplierCode = @AnalyzerSupplierCode
											   AND DataSourceTypeID = @DataSourceTypeID
											   AND IsActive = 1)
		END
	END	
-- INSERT ANALYZER SUPPLIER CROSS-REFERENCE --------------------------------------
IF NOT EXISTS(
			SELECT 
				AIMSupplierRefAnalyzerSupplierID 
			FROM 
				dbo.AIMSupplierRefAnalyzerSupplier XREF
			WHERE
				AIMSupplierID = @AIMSourceID and AnalyzerSupplierID = @AnalyzerSupplierID)
		BEGIN
			INSERT
				dbo.AIMSupplierRefAnalyzerSupplier
					(
					  AIMSupplierID
					, AnalyzerSupplierID
					, UpdateStamp
					, CFCUserID
					)
					VALUES
					(
					  @AIMSourceID
					, @AnalyzerSupplierID
					, GetDate()
					, @CFCUserID
					)
		END


-- INSERT ANALYZER SUPPLIER LOCATION ---------------------------------------------
IF ((@AnalyzerSupplierLocationID = 0 OR @AnalyzerSupplierLocationID = NULL) AND (Len(@AnalyzerSupplierLocationCode) != 0 or @AnalyzerSupplierLocationCode != NULL))
	BEGIN
	IF EXISTS(
			SELECT 
				AnalyzerSupplierLocationID 
			FROM 
				dbo.AnalyzerSupplierLocation 
			WHERE 
				AnalyzerSupplierLocationCode = @AnalyzerSupplierLocationCode 
				    AND AnalyzerSupplierID = @AnalyzerSupplierID
					AND DataSourceTypeID = @DataSourceTypeID
			)
		BEGIN
			SET @AnalyzerSupplierLocationID = (SELECT TOP 1 AnalyzerSupplierLocationID 
										FROM dbo.AnalyzerSupplierLocation 
										WHERE AnalyzerSupplierLocationCode = @AnalyzerSupplierLocationCode
												AND AnalyzerSupplierID = @AnalyzerSupplierID
												AND DataSourceTypeID = @DataSourceTypeID
												AND IsActive = 1)
		END
	ELSE
		BEGIN
			INSERT
				dbo.AnalyzerSupplierLocation
					(
					  AnalyzerSupplierID
					, AnalyzerSupplierLocationCode
					, AnalyzerSupplierLocationName
					, DataSourceTypeID
					, CFCUserID
					)
					VALUES
					(
					  @AnalyzerSupplierID
					, @AnalyzerSupplierLocationCode
					, @AnalyzerSupplierLocationName
					, @DataSourceTypeID
					, @CFCUserID
					)
			SET @AnalyzerSupplierLocationID = (SELECT TOP 1 AnalyzerSupplierLocationID FROM dbo.AnalyzerSupplierLocation 
										WHERE AnalyzerSupplierLocationCode = @AnalyzerSupplierLocationCode
											   AND AnalyzerSupplierID = @AnalyzerSupplierID
											   AND DataSourceTypeID = @DataSourceTypeID
											   AND IsActive = 1)
		END
	END	
-- INSERT ANALYZER SupplierLocation LOCATION CROSS-REFERENCE -----------------------------
IF NOT EXISTS(
			SELECT 
				AIMSupplierLocationRefAnalyzerSupplierLocationID 
			FROM 
				dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF
			WHERE
				AIMSupplierLocationID = @AIMSourceLocationID and 
				AIMSupplierID = @AIMSourceID and
				AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID and
				AnalyzerSupplierID = @AnalyzerSupplierID)
		BEGIN
			INSERT
				dbo.AIMSupplierLocationRefAnalyzerSupplierLocation
					(
					  AIMSupplierLocationID
					, AnalyzerSupplierLocationID
					, AIMSupplierID
					, AnalyzerSupplierID
					, UpdateStamp
					, CFCUserID
					)
					VALUES
					(
					  @AIMSourceLocationID
					, @AnalyzerSupplierLocationID
					, @AIMSourceID
					, @AnalyzerSupplierID
					, GetDate()
					, @CFCUserID
					)
		END


-- Update Sample --------------------------------------------------------------
IF @SampleID > 0
	BEGIN
		UPDATE
			dbo.Sample WITH(ROWLOCK)
				SET
					  SampleCode = ISNULL(@SampleCode, SampleCode)
					, SampleDate = ISNULL(@SampleDate, SampleDate)
					, AlternateCode = ISNULL(@AlternateCode, AlternateCode)
					, AIMOwnerID = ISNULL(@AIMOwnerID, AIMOwnerID)
					, AIMSourceID = ISNULL(@AIMSourceID, AIMSourceID)
					, AIMSourceLocationID = ISNULL(@AIMSourceLocationID, AIMSourceLocationID)
					, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
					, AnalyzerMaterialID = ISNULL(@AnalyzerMaterialID, AnalyzerMaterialID)
					, ContractNumber = ISNULL(@ContractNumber, ContractNumber)
					, PoNumber = ISNULL(@PoNumber, PoNumber)
					, InvoiceNumber = ISNULL(@InvoiceNumber, InvoiceNumber)
					, TicketNumber = ISNULL(@TicketNumber, TicketNumber)
					, LoadNumber = ISNULL(@LoadNumber, LoadNumber)
					, BillOfLoad = ISNULL(@BillOfLoad, BillOfLoad)
					, VehicleNumber = ISNULL(@VehicleNumber, VehicleNumber)
					, IsActive = ISNULL(@IsActive, @IsActive)
					, LotNumber = ISNULL(@LotNumber, @LotNumber)
					, AnalysisDate = ISNULL(@AnalysisDate, @AnalysisDate)					
					, AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, @AIMOwnerLocationID)
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, UpdateStamp = GETDATE()
					, FurtherAnalysisID = ISNULL(@FurtherAnalysisID, FurtherAnalysisID)					
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)		
					, CFCProductArchiveID = ISNULL(@CFCProductArchiveID, CFCProductArchiveID)		
					, Notes = ISNULL(@Notes, Notes)		
					, ExpirationDate = ISNULL(@ExpirationDate, ExpirationDate)		
					, SupportingDocumentPath = ISNULL(@SupportingDocumentPath, SupportingDocumentPath)
					, SampleTypeID = ISNULL(@SampleTypeID, SampleTypeID)
					, IsGround = ISNULL(@IsGround, IsGround)
					, SampleTestTypeID = ISNULL(@SampleTestTypeID, SampleTestTypeID)
					, GroundUnGroundID = ISNULL(@GroundUnGroundID, GroundUnGroundID)
					, @ErrorMessage = 'Successfully Saved'
			WHERE
				SampleID = @SampleID
	
	END


-- Insert Sample --------------------------------------------------------------
ELSE
	BEGIN

		INSERT
			dbo.Sample WITH(ROWLOCK)
				(
				  SampleCode
				, AlternateCode
				, SampleDate
				, CreatedStamp
				, AIMOwnerID
				, AIMSourceID
				, AIMSourceLocationID
				, AIMMaterialID
				, AnalyzerMaterialID
				, ContractNumber
				, PoNumber
				, InvoiceNumber
				, TicketNumber
				, LoadNumber
				, BillOfLoad
				, VehicleNumber
				, LotNumber
				, AnalysisDate 				
				, AIMOwnerLocationID
				, CFCUserID
				, FurtherAnalysisID	
				, DataSourceTypeID	
				, CFCProductArchiveID	
				, Notes	
				, ExpirationDate
				, SupportingDocumentPath
				, SampleTypeID
				, IsGround
				, SampleTestTypeID
				, GroundUnGroundID
				)
		VALUES
				(
				  @SampleCode
				, @AlternateCode
				, @SampleDate
				, GETDATE()
				, @AIMOwnerID
				, @AIMSourceID
				, @AIMSourceLocationID
				, @AIMMaterialID
				, @AnalyzerMaterialID
				, @ContractNumber
				, @PoNumber
				, @InvoiceNumber
				, @TicketNumber
				, @LoadNumber
				, @BillOfLoad
				, @VehicleNumber
				, @LotNumber
				, @AnalysisDate				
				, @AIMOwnerLocationID
				, @CFCUserID
				, @FurtherAnalysisID
				, @DataSourceTypeID
				, @CFCProductArchiveID
				, @Notes
				, @ExpirationDate
				, @SupportingDocumentPath
				, @SampleTypeID
				, @IsGround
				, @SampleTestTypeID
				, @GroundUnGroundID
				)

		IF @SampleCode IS NULL OR len(@SampleCode) = 0
		Begin
			UPDATE dbo.SAMPLE with (rowlock)
			SET SampleCode = SampleID
			WHERE SampleID = @@IDENTITY
		End
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

