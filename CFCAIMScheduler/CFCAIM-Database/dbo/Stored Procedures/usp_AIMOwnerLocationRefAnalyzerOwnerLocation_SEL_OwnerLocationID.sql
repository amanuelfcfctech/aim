CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL_OwnerLocationID]
	@AnalyzerOwnerLocationID		INT

AS
BEGIN
SET NOCOUNT ON;



SELECT
	XREF.*
	FROM 

	dbo.AIMOwnerLocationRefAnalyzerOwnerLocation XREF WITH(NOLOCK)

WHERE
	AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID

END