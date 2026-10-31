CREATE PROCEDURE [dbo].[usp_AIMMaterial_INS_GenerateFromCFCProduct]
					
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

INSERT INTO 
	dbo.AIMMaterial
		(
		  MaterialTypeID
        , AIMMaterialCode
        , AIMMaterialName
        , IsActive
        , UpdateStamp
        , CFCUserID
        , AIMOwnerLocationID
		)
SELECT 
	2 AS MaterialTypeID
	, CProd.CFCProductCode
	, LEFT(CProd.CFCProductName, 30)
	, 1 AS IsActive
	, GETDATE()
	, 0
	, CProd.AIMOwnerLocationID
FROM 
	(SELECT
		 P.CFCProductCode 
		 , P.CFCPlantID 
		 , P.CFCProductName 
		 , CP.CFCPlantCode 
		 , CPRAOL.AIMOwnerLocationID 
	FROM 
		dbo.CFCProduct P
		INNER JOIN 
			dbo.CFCPlant CP
				ON CP.CFCPlantID = P.CFCPlantID 
		INNER JOIN 
			dbo.CFCPlantRefAIMOwnerLocation CPRAOL 
				ON CPRAOL.CFCPlantID = CP.CFCPlantID) CProd	
	LEFT OUTER JOIN 
		(SELECT 
			  AM.AIMMaterialID
			, AM.AIMMaterialCode 
			, CPRAOL.CFCPlantID 
		FROM	
			dbo.AIMMaterial AM
			INNER JOIN 
				dbo.AIMOwnerLocation AOL
					ON AOL.AIMOwnerLocationID = AM.AIMOwnerLocationID 
			INNER JOIN
				dbo.CFCPlantRefAIMOwnerLocation CPRAOL 
					ON CPRAOL.AIMOwnerLocationID = AOL.AIMOwnerLocationID
		WHERE 
			AM.MaterialTypeID = 2) AProd
			ON AProd.AIMMaterialCode = CProd.CFCProductCode 
			AND AProd.CFCPlantID = CProd.CFCPlantID 
WHERE 
	AProd.AIMMaterialID IS NULL


SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END