CREATE PROCEDURE [Vitapro].[usp_ProductionStatus_SEL]
AS
BEGIN
SET NOCOUNT ON;

--exec usp_ProductionStatus_SEL_All

SELECT
	    PS.ProductionStatusID
	  , PS.ProductionStatusName
	 
FROM 
	Vitapro.ProductionStatus PS 

	
END