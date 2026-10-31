CREATE PROCEDURE [dbo].[usp_TestMaterial_SEL]
AS
BEGIN
    SELECT
        TM.TestMaterialID,
        TM.TestID,
		T.TestCode,
        TM.AIMMaterialID,
		M.AIMMaterialCode,
		M.AIMMaterialName,
        TM.TestMaterialCurrentUpcharge,
		TM.TestMaterialPreviousUpcharge,
        T.TestCurrentCost,
	    T.TestPreviousCost,
		TM.TestMaterialCurrentUpcharge + T.TestCurrentCost AS TestMaterialCurrentCost,
		TM.TestMaterialPreviousUpcharge + T.TestPreviousCost AS TestMaterialPreviousCost,
        TM.UpdateStamp,
		TM.CFCUserID,
        U.UserName
    FROM dbo.TestMaterial TM
	INNER JOIN
		dbo.AIMMaterial M
		ON M.AIMMaterialID = TM.AIMMaterialID
	INNER JOIN
		dbo.Test T
		ON T.TestID = TM.TestID
	INNER JOIN
		CFCUser.CFCUser U
		ON U.CFCUserID = TM.CFCUserID
END;