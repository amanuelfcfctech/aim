CREATE PROCEDURE [Vitapro].[usp_ProductionStatus_SEL_ForDepleted]
AS
BEGIN
SET NOCOUNT ON;



SELECT
	    PS.ProductionStatusID
	  , PS.ProductionStatusName
	 
FROM 
	Vitapro.ProductionStatus PS 

WHERE
	PS.ProductionStatusID IN (2,4)	--Available and Depleted
	
END