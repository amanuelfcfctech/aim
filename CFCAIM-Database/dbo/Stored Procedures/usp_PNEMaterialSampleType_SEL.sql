CREATE PROCEDURE [dbo].[usp_PNEMaterialSampleType_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  PMST.PNEMaterialSampleTypeID
	, PMST.PNEMaterialSampleTypeCode
	, PMST.IsActive
	, PMST.UpdateStamp
	, PMST.CFCUserID
	, U.UserName  
FROM 
	dbo.PNEMaterialSampleType PMST
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON U.CFCUserID = PMST.CFCUserID

END