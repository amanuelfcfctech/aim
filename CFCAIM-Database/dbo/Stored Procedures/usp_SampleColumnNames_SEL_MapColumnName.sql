CREATE PROCEDURE [dbo].[usp_SampleColumnNames_SEL_MapColumnName]
	@MapColumnName NVarChar(50) 

--exec [usp_SampleColumnNames_SEL_MapColumnName] 'BillOfLoad'
			
AS
BEGIN
SET NOCOUNT ON;

/*
//TODO - 4/3/18 - this stp is not used anymore -- can be deleted when ImportTextFile.cs is removed.
*/

SELECT X.* 
FROM (
	SELECT 'SampleCode' as MapColumnName, 1 as MapColumnID, 0 as IsDetail	
	UNION SELECT 'AlternateCode' as MapColumnName, 2 as MapColumnID, 0 as IsDetail
	UNION SELECT 'Owner' as MapColumnName, 3 as MapColumnID, 0 as IsDetail
	UNION SELECT 'OwnerLocation' as MapColumnName, 4 as MapColumnID, 0 as IsDetail
	UNION SELECT 'Supplier' as MapColumnName, 5 as MapColumnID, 0 as IsDetail
	UNION SELECT 'SupplierLocation' as MapColumnName, 6 as MapColumnID, 0 as IsDetail
	UNION SELECT 'ContractNumber' as MapColumnName, 7 as MapColumnID, 0 as IsDetail
	UNION SELECT 'PONumber' as MapColumnName, 8 as MapColumnID, 0 as IsDetail
	UNION SELECT 'InvoiceNumber' as MapColumnName, 9 as MapColumnID, 0 as IsDetail
	UNION SELECT 'TicketNumber' as MapColumnName, 10 as MapColumnID, 0 as IsDetail
	UNION SELECT 'LoadNumber' as MapColumnName, 11 as MapColumnID, 0 as IsDetail
	UNION SELECT 'Material' as MapColumnName, 12 as MapColumnID, 0 as IsDetail
	UNION SELECT 'BillOfLading' as MapColumnName, 13 as MapColumnID, 0 as IsDetail
	UNION SELECT 'VehicleID' as MapColumnName, 14 as MapColumnID, 0 as IsDetail
	UNION SELECT 'LotNumber' as MapColumnName, 15 as MapColumnID, 0 as IsDetail
	UNION SELECT 'SampleDate' as MapColumnName, 16 as MapColumnID, 0 as IsDetail
	UNION SELECT 'AnalyzerNutrientCode' as MapColumnName, 17 as MapColumnID, 1 as IsDetail
	UNION SELECT 'ResultValue' as MapColumnName, 18 as MapColumnID, 1 as IsDetail
	UNION SELECT 'UnitType' as MapColumnName, 19 as MapColumnID, 1 as IsDetail 
	UNION SELECT 'VehicleID' as MapColumnName, 20 as MapColumnID, 0 as IsDetail 
	UNION SELECT 'DataSource' as MapColumnName, 21 as MapColumnID, 1 as IsDetail 
	UNION SELECT 'Notes' as MapColumnName, 22 as MapColumnID, 0 as IsDetail 
	) X

WHERE X.MapColumnName = @MapColumnName

END