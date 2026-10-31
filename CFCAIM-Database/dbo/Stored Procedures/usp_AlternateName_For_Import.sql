CREATE PROCEDURE [dbo].[usp_AlternateName_For_Import]


AS
BEGIN

SET NOCOUNT ON;

SELECT 
	C.ControlName,
	AN.AlternateName 
FROM 
	dbo.[Control] C 
INNER JOIN
	dbo.AlternateName AN 
		on C.ControlID = AN.ControlID
WHERE 
	C.ControlName 
		IN ('lblBOL'
		, 'lblContractNumber'
		, 'lblInvoiceNumber'
		, 'lblLoadNumber'
		, 'lblLotNumber'
		, 'lblPONumber'
		, 'lblTicketNumber'
		, 'lblVehicleID'
		
		, 'lblAlternateCode'
     	, 'lblDataSource'
     	, 'lblExpirationDate'
     	, 'lblFurtherAnalysis'
     	, 'lblMaterial'
     	, 'lblNotes'
     	, 'lblOwnerLocation'
     	, 'lblOwner'
     	, 'lblID'
     	, 'lblDate'
     	, 'lblSupplierLocation'
     	, 'lblSupplier'
)
		

END