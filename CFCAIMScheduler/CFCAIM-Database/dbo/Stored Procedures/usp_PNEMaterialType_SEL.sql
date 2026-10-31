create PROCEDURE [dbo].[usp_PNEMaterialType_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  PMT.PNEMaterialTypeID
	, PMT.AnalyzerMaterialID
	, PMT.PNEMaterialSampleTypeID
	, PMT.IsActive
	, PMT.UpdateStamp
	, PMT.CFCUserID
	, U.UserName 
FROM 
	dbo.PNEMaterialType PMT
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON U.CFCUserID = PMT.CFCUserID

END