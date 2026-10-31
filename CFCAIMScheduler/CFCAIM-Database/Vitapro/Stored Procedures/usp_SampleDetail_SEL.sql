CREATE PROCEDURE [Vitapro].[usp_SampleDetail_SEL]

AS
BEGIN
SET NOCOUNT ON;

SELECT
	L.SampleID,
	SR.SampleResultID,	
	SR.AnalyzerNutrientCode,
	SR.ResultValue,
	SR.Unit as UnitType
		

FROM 
	Vitapro.Lot L 

INNER JOIN
	dbo.SampleResult SR 
		ON L.SampleID = SR.SampleID

WHERE SR.IsActive = 1


END