CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SAV_CFC5]
			  @CFCProductArchiveID	INT			
			, @PlantCode			VARCHAR(8)	= NULL
			, @ProductCode			VARCHAR(8)	= NULL
			, @ArchiveVersion		VARCHAR(8)	= NULL
			, @ArchiveDate			DATETIME	= NULL
			, @ArchiveType			VARCHAR(1)	= NULL
			, @ArchiveUser			VARCHAR(8)	= NULL
			, @ProductionDate		DATETIME	= NULL
			, @ProductionVersion	VARCHAR(8)	= NULL
			, @ProductionUser		VARCHAR(8)	= NULL
			, @TrialDate			DATETIME	= NULL
			, @TrialVersion			VARCHAR(8)	= NULL
			, @TrialUser			VARCHAR(8)	= NULL
			, @CFCUserID			INT			= NULL
			, @AIMDatabaseID		INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @CFCProductID	INT
DECLARE @RecordID		INT
DECLARE @ReturnValue	INT
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

IF @CFCProductArchiveID = 0
	BEGIN
		SELECT
			@CFCProductArchiveID = CPA.CFCProductArchiveID 
		FROM
			dbo.CFCProduct CP
			INNER JOIN 
				dbo.CFCPlant CPT
					ON CPT.CFCPlantID = CP.CFCPlantID 
					AND CPT.AIMDatabaseID = @AIMDatabaseID
			INNER JOIN 
				dbo.CFCProductArchive CPA
					ON CP.CFCProductID = CPA.CFCProductID 
		WHERE
			CP.AIMDatabaseID = @AIMDatabaseID
			AND CPT.CFCPlantCode = @PlantCode
			AND CP.CFCProductCode = @ProductCode
			AND CPA.ArchiveVersion = @ArchiveVersion
	END

IF @CFCProductArchiveID = 0
	BEGIN
		INSERT
			dbo.CFCProductArchive
				(
					CFCProductID 
				, ArchiveVersion		
				, ArchiveDate			
				, ArchiveType			
				, ArchiveUser			
				, ProductionDate		
				, ProductionVersion	
				, ProductionUser		
				, TrialDate			
				, TrialVersion			
				, TrialUser			
				, UpdateStamp			
				, CFCUserID	
				)
		SELECT
			  CP.CFCProductID 
			, @ArchiveVersion		
			, @ArchiveDate			
			, @ArchiveType			
			, @ArchiveUser			
			, @ProductionDate		
			, @ProductionVersion	
			, @ProductionUser		
			, @TrialDate			
			, @TrialVersion			
			, @TrialUser			
			, GETDATE()			
			, @CFCUserID								
		FROM									
			dbo.CFCProduct CP
			INNER JOIN 
				dbo.CFCPlant CPT
					ON CPT.CFCPlantID = CP.CFCPlantID 
					AND CPT.AIMDatabaseID = @AIMDatabaseID
			WHERE
				CP.AIMDatabaseID = @AIMDatabaseID
				AND CPT.CFCPlantCode = @PlantCode
				AND CP.CFCProductCode = @ProductCode
		SET @RecordID = @@IDENTITY
	END
ELSE
	BEGIN
		SELECT
			@CFCProductID = CP.CFCProductID 
		FROM
			dbo.CFCProduct CP
			INNER JOIN 
				dbo.CFCPlant CPT
					ON CPT.CFCPlantID = CP.CFCPlantID 
					AND CPT.AIMDatabaseID = @AIMDatabaseID
		WHERE
			CP.AIMDatabaseID = @AIMDatabaseID
			AND CPT.CFCPlantCode = @PlantCode
			AND CP.CFCProductCode = @ProductCode

		UPDATE
			dbo.CFCProductArchive
				SET			
					  CFCProductID = ISNULL(@CFCProductID, CFCProductID)
					, ArchiveVersion = ISNULL(@ArchiveVersion, ArchiveVersion)
					, ArchiveDate = ISNULL(@ArchiveDate, ArchiveDate)
					, ArchiveType = ISNULL(@ArchiveType, ArchiveType)			
					, ArchiveUser = ISNULL(@ArchiveUser, ArchiveUser)			
					, ProductionDate = ISNULL(@ProductionDate, ProductionDate)		
					, ProductionVersion = ISNULL(@ProductionVersion, ProductionVersion)	
					, ProductionUser = ISNULL(@ProductionUser, ProductionUser)		
					, TrialDate = ISNULL(@TrialDate, TrialDate)			
					, TrialVersion = ISNULL(@TrialVersion, TrialVersion)			
					, TrialUser = ISNULL(@TrialUser, TrialUser)			
					, UpdateStamp = GETDATE()		
					, CFCUserID	= @CFCUserID 
				WHERE
					CFCProductArchiveID = @CFCProductArchiveID
		SET @RecordID = @CFCProductArchiveID
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END