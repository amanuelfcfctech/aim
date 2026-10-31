CREATE PROCEDURE [dbo].[usp_ReportUserDefaultSetting_SEL_CFCUserID]
    @CFCUserID INT
AS
BEGIN
    SELECT
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
    FROM 
	ReportUserDefaultSetting
    WHERE 
	CFCUserID = @CFCUserID;
END;