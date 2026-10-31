create PROCEDURE [dbo].[usp_SampleColumnNames_SEL_TestSave]
			
AS
BEGIN
SET NOCOUNT ON;

--exec usp_SampleColumnNames_SEL

SELECT 'DO NOT USE' as MapColumnName, 1 as MapColumnID, 0 as IsDetail				, 1 as SortNo	
UNION SELECT 'AlternateCode' as MapColumnName, 2 as MapColumnID, 0 as IsDetail		, 2 as SortNo
UNION SELECT 'BillOfLading' as MapColumnName, 4 as MapColumnID, 0 as IsDetail		, 3 as SortNo
UNION SELECT 'ContractNumber' as MapColumnName, 5 as MapColumnID, 0 as IsDetail		, 4 as SortNo
UNION SELECT 'DataSource' as MapColumnName, 21 as MapColumnID, 1 as IsDetail		, 5 as SortNo
UNION SELECT 'InvoiceNumber' as MapColumnName, 6 as MapColumnID, 0 as IsDetail		, 6 as SortNo
UNION SELECT 'LoadNumber' as MapColumnName, 7 as MapColumnID, 0 as IsDetail			, 7 as SortNo
UNION SELECT 'LotNumber' as MapColumnName, 8 as MapColumnID, 0 as IsDetail			, 8 as SortNo
UNION SELECT 'Material' as MapColumnName, 9 as MapColumnID, 0 as IsDetail			, 9 as SortNo
UNION SELECT 'NutrientCode' as MapColumnName, 3 as MapColumnID, 1 as IsDetail		, 10 as SortNo
UNION SELECT 'Owner' as MapColumnName, 10 as MapColumnID, 0 as IsDetail				, 11 as SortNo
UNION SELECT 'OwnerLocation' as MapColumnName, 11 as MapColumnID, 0 as IsDetail		, 12 as SortNo
UNION SELECT 'PONumber' as MapColumnName, 12 as MapColumnID, 0 as IsDetail			, 13 as SortNo
UNION SELECT 'ResultValue' as MapColumnName, 13 as MapColumnID, 1 as IsDetail		, 14 as SortNo
UNION SELECT 'SampleCode' as MapColumnName, 14 as MapColumnID, 0 as IsDetail		, 15 as SortNo
UNION SELECT 'SampleDate' as MapColumnName, 15 as MapColumnID, 0 as IsDetail		, 16 as SortNo	
UNION SELECT 'Supplier' as MapColumnName, 16 as MapColumnID, 0 as IsDetail			, 17 as SortNo
UNION SELECT 'SupplierLocation' as MapColumnName, 17 as MapColumnID, 0 as IsDetail	, 18 as SortNo
UNION SELECT 'TicketNumber' as MapColumnName, 18 as MapColumnID, 0 as IsDetail		, 19 as SortNo
UNION SELECT 'UnitType' as MapColumnName, 19 as MapColumnID, 1 as IsDetail			, 20 as SortNo
UNION SELECT 'VehicleID' as MapColumnName, 20 as MapColumnID, 0 as IsDetail			, 21 as SortNo


ORDER BY SortNo	




END