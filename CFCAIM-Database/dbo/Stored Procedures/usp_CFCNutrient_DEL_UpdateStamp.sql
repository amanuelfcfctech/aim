CREATE PROCEDURE [dbo].[usp_CFCNutrient_DEL_UpdateStamp]
					@UpdateStamp		DATETIME
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.CFCNutrient 
WHERE 
	UpdateStamp < @UpdateStamp

INSERT INTO 
	dbo.CFCNutrientRefAIMNutrientLog 
		(
		  CFCNutrientID
		, AIMNutrientID
		, LogStatus
		, UpdateStamp
		, CFCUserID
		)
SELECT 
	  CNRAN.[CFCNutrientID]
    , CNRAN.AIMNutrientID
	, 'D'
	, GETDATE()
	, 0
FROM 
	dbo.CFCNutrientRefAIMNutrient CNRAN
	LEFT OUTER JOIN 
		dbo.CFCNutrient CN
			ON CN.CFCNutrientID = CNRAN.CFCNutrientID 
WHERE 
	CN.CFCNutrientID IS NULL

DELETE 
	dbo.CFCNutrientRefAIMNutrient
FROM 
	dbo.CFCNutrientRefAIMNutrient CNRAN
	LEFT OUTER JOIN 
		dbo.CFCNutrient CN
			ON CN.CFCNutrientID = CNRAN.CFCNutrientID 
WHERE 
	CN.CFCNutrientID IS NULL

END