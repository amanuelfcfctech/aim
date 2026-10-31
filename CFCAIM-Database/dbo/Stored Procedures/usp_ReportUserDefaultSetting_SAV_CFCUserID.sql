CREATE PROCEDURE [dbo].[usp_ReportUserDefaultSetting_SAV_CFCUserID]
    @CFCUserID INT,
    @ReportType INT = NULL,
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL,
    @MaterialType INT = NULL,
    @ExtremeOutlier INT = NULL,
    @MaterialGroup INT = NULL,
    @MildOutlier INT = NULL,
    @SortOnNutrientCode INT = NULL,
    @SampleResult INT = NULL,
    @AnalyzerOrFormulation INT = NULL,
    @QualityControl INT = NULL,
    @DateSampleOrCreated INT = NULL,
    @OwnerID INT = NULL,
    @OwnerLocationID INT = NULL,
    @SupplierID INT = NULL,
    @SupplierLocationID INT = NULL,
    @MaterialID INT = NULL,
    @MaterialNameID INT = NULL,
    @TestTypeID INT = NULL,
	@TicketNumber NVARCHAR(100) = NULL,
	@TemplateID INT = NULL,
	@CFCUserFormID INT = NULL,
	@OutOfRangeOnly BIT = NULL,
	@ShowViolations BIT = NULL

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


    IF EXISTS (SELECT 1 FROM ReportUserDefaultSetting WHERE CFCUserID = @CFCUserID)
    BEGIN
        UPDATE ReportUserDefaultSetting
        SET
            ReportType = @ReportType,
            FromDate = @FromDate,
            ToDate = @ToDate,
            MaterialType = @MaterialType,
            ExtremeOutlier = @ExtremeOutlier,
            MaterialGroup = @MaterialGroup,
            MildOutlier = @MildOutlier,
            SortOnNutrientCode = @SortOnNutrientCode,
            SampleResult = @SampleResult,
            AnalyzerOrFormulation = @AnalyzerOrFormulation,
            QualityControl = @QualityControl,
            DateSampleOrCreated = @DateSampleOrCreated,
            OwnerID = @OwnerID,
            OwnerLocationID = @OwnerLocationID,
            SupplierID = @SupplierID,
            SupplierLocationID = @SupplierLocationID,
            MaterialID = @MaterialID,
            MaterialNameID = @MaterialNameID,
            TestTypeID = @TestTypeID,
			TicketNumber = @TicketNumber,
			TemplateID = @TemplateID,
			CFCUserFormID = @CFCUserFormID,
			OutOfRangeOnly = @OutOfRangeOnly,
			ShowViolations = @ShowViolations
        WHERE CFCUserID = @CFCUserID;
    END
    ELSE
    BEGIN
        INSERT INTO ReportUserDefaultSetting (
            ReportType,
            FromDate,
            ToDate,
            MaterialType,
            ExtremeOutlier,
            MaterialGroup,
            MildOutlier,
            SortOnNutrientCode,
            SampleResult,
            AnalyzerOrFormulation,
            QualityControl,
            DateSampleOrCreated,
            CFCUserID,
            OwnerID,
            OwnerLocationID,
            SupplierID,
            SupplierLocationID,
            MaterialID,
            MaterialNameID,
            TestTypeID,
			TicketNumber,
			TemplateID,
			CFCUserFormID,
			OutOfRangeOnly,
			ShowViolations

        )
        VALUES (
            @ReportType,
            @FromDate,
            @ToDate,
            @MaterialType,
            @ExtremeOutlier,
            @MaterialGroup,
            @MildOutlier,
            @SortOnNutrientCode,
            @SampleResult,
            @AnalyzerOrFormulation,
            @QualityControl,
            @DateSampleOrCreated,
            @CFCUserID,
            @OwnerID,
            @OwnerLocationID,
            @SupplierID,
            @SupplierLocationID,
            @MaterialID,
            @MaterialNameID,
            @TestTypeID,
			@TicketNumber,
			@TemplateID,
			@CFCUserFormID,
			@OutOfRangeOnly,
			@ShowViolations

        );
    END

SET @ErrorNumber = @@ERROR

SELECT
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END;