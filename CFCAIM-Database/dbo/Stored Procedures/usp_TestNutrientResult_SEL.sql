CREATE PROCEDURE [dbo].[usp_TestNutrientResult_SEL]
AS
BEGIN
    SELECT
        TNR.TestNutrientResultID,
        TNR.TestID,
		T.TestCode,
		T.TestName,
        TNR.AIMNutrientID,
		N.AIMNutrientCode,
		N.AIMNutrientName,
        TNR.TestGroupNameID,
		TGN.TestGroupName,
        TNR.TestNutrientResultCurrentUpcharge,
		TNR.TestNutrientResultPreviousUpcharge,
		T.TestCurrentCost,
	    T.TestPreviousCost,
		TNR.TestNutrientResultCurrentUpcharge + T.TestCurrentCost AS TestNutrientResultCurrentCost,
		TNR.TestNutrientResultPreviousUpcharge + T.TestPreviousCost AS TestNutrientResultPreviousCost,
        TNR.UpdateStamp,
        TNR.CFCUserID,
		U.UserName
    FROM dbo.TestNutrientResult TNR
	INNER JOIN
		CFCUser.CFCUser U
		ON U.CFCUserID = TNR.CFCUserID
	INNER JOIN
		dbo.Test T 
		ON T.TestID = TNR.TestID
	INNER JOIN
		dbo.AIMNutrient N
		ON N.AIMNutrientID = TNR.AIMNutrientID
	LEFT OUTER JOIN
		dbo.TestGroupName TGN
		ON TGN.TestGroupNameID = TNR.TestGroupNameID
END;