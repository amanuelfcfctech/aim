CREATE PROCEDURE [dbo].[usp_AIMOwnerRefAnalyzerOwner_SEL_OwnerID]
	@AnalyzerOwnerID INT

AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMOwnerRefAnalyzerOwner_SEL_OwnerID 18


SELECT
	    XREF.*
	  
FROM 
	dbo.AIMOwnerRefAnalyzerOwner XREF WITH(NOLOCK)
	
WHERE XREF.AnalyzerOwnerID = @AnalyzerOwnerID

END