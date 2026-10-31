CREATE PROCEDURE [dbo].[usp_Report_AlternateNames]


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
		, 'lblVehicleID')
		

END