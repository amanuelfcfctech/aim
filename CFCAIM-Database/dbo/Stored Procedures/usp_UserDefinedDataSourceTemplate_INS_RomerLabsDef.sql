CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_INS_RomerLabsDef]
	
		(@DefID as Integer
		,@IsComposite as Bit)	
AS
BEGIN
SET NOCOUNT ON;

-- for Composite imports, need to set UseAsSampleCode = false 
If @IsComposite = 1 
	INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Sample', 1, 2, 'AlternateCode', 0, getdate(), 1, 0, 0
else
	INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Sample', 1, 2, 'AlternateCode', 0, getdate(), 1, 0, 0

INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Operator', 2, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Date/Time', 3, 15, 'SampleDate', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Test', 4, 3, 'AnalyzerNutrientCode', 0, getdate(), 1, 1, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Lot', 5, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Strip', 6, 1, 'DO NOT USE', 0, getdate(), 1, 1, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Commodity', 7, 9, 'Material', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Range', 8, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Method', 9, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Curve', 10, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Result 1', 11, 13, 'ResultValue', 0, getdate(), 1, 1, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Result 2', 12, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Result 3', 13, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0

--5/1/23
--INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Reader name', 14, 1, 'DO NOT USE', 0, getdate(), 1, 0, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Reader name', 14, 1, 'OwnerLocation', 0, getdate(), 1, 0, 0

INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'Reader serial number', 15, 1, 'DO NOT USE', 0, getdate(), 1, 1, 0
INSERT INTO dbo.UserDefinedDataSourceTemplate (UserDefinedDataSourceDefinitionID, SourceColumnName, SourceColumnNumber, MapColumnID, MapColumnName, IsManualEntry, UpdateStamp, CFCUserID, IsDetail, UseAsSampleCode) SELECT @DefID, 'UniqueSampleID', 16, 1, 'UniqueSampleID', 0, getdate(), 1, 1, 1

END