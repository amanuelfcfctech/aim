

CREATE PROCEDURE Vitapro.[usp_LotStatus_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  LS.LotStatusID
	, LS.LotStatusName
	, LS.UpdateStamp
	, LS.CFCUserID
FROM 
	Vitapro.LotStatus LS 
END