CREATE PROCEDURE [dbo].[usp_CFCProductArchive_INS_CFC5]
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

SELECT 
	@CFCProductArchiveID = CPA.CFCProductArchiveID 
FROM 
	dbo.CFCProductArchive CPA
	INNER JOIN 
		dbo.CFCProduct CP
			ON CP.CFCProductID = CPA.CFCProductID 
	INNER JOIN
		dbo.CFCPlant CPL
			ON CPL.CFCPlantID = CP.CFCPlantID 
WHERE 
	CP.AIMDatabaseID = @AIMDatabaseID
	AND CPL.CFCPlantCode = @PlantCode
	AND CP.CFCProductCode = @ProductCode
	AND CPA.ArchiveVersion = @ArchiveVersion
	AND CPA.ArchiveDate = @ArchiveDate

IF @CFCProductArchiveID IS NULL
	BEGIN
		SET @CFCProductArchiveID = 0
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
		SET @ErrorNumber = 1
		SET @ErrorMessage = 'Already exists'
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END