CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_INS_GenerateFromCFCProduct]

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

INSERT INTO 
	dbo.CFCProductRefAIMMaterial
		(
		  CFCProductID
        , AIMMaterialID
        , UpdateStamp
        , CFCUserID
		)
SELECT
	  P.CFCProductID
	, AM.AIMMaterialID  
	, GETDATE()
	, 0
FROM 
	dbo.CFCProduct P
	INNER JOIN 
		dbo.CFCPlant CP
			ON CP.CFCPlantID = P.CFCPlantID 
	INNER JOIN 
		dbo.CFCPlantRefAIMOwnerLocation CPRAOL 
			ON CPRAOL.CFCPlantID = CP.CFCPlantID
	INNER JOIN 
		dbo.AIMMaterial AM
			ON AM.AIMMaterialCode = P.CFCProductCode
			AND AM.AIMOwnerLocationID = CPRAOL.AIMOwnerLocationID
	LEFT OUTER JOIN
		dbo.CFCProductRefAIMMaterial x 
			ON x.AIMMaterialID = AM.AIMMaterialID and
			   x.CFCProductID = P.CFCProductID

WHERE x.CFCProductRefAIMMaterialID IS NULL

GROUP BY 
	P.CFCProductID
	, AM.AIMMaterialID  


--4/17/18 ----------------------------------------
INSERT INTO CFCProductRefAIMMaterialLog (CFCProductID, AIMMaterialID, LogStatus, UpdateStamp, CFCUserID)
SELECT	    P.CFCProductID, AM.AIMMaterialID, 'A', Getdate(), 0
FROM 
	dbo.CFCProduct P
	INNER JOIN 
		dbo.CFCPlant CP
			ON CP.CFCPlantID = P.CFCPlantID 
	INNER JOIN 
		dbo.CFCPlantRefAIMOwnerLocation CPRAOL 
			ON CPRAOL.CFCPlantID = CP.CFCPlantID
	INNER JOIN 
		dbo.AIMMaterial AM
			ON AM.AIMMaterialCode = P.CFCProductCode
			AND AM.AIMOwnerLocationID = CPRAOL.AIMOwnerLocationID
GROUP BY 
	P.CFCProductID
	, AM.AIMMaterialID  
  
--------------------------------------------------


SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END