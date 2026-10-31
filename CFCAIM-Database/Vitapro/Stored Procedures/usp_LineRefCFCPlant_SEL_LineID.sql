CREATE PROCEDURE [Vitapro].[usp_LineRefCFCPlant_SEL_LineID]

	@LineID		INt

AS
BEGIN
SET NOCOUNT ON;

SELECT
	 L.LineRefCFCPlantID
	 , L.LineID	 
	 , L.LineName
	 , L.UpdateStamp
	 , L.UpdateStamp
	 , L.IsActive
	 , L.CFCUserID
	 , P.CFCPlantID
	 , P.CFCPlantCode
	 , P.CFCPlantName
	 
	 

FROM 
	Vitapro.LineRefCFCPlant L 

LEFT OUTER JOIN
	dbo.CFCPlant P
		on L.CFCPlantID = P.CFCPlantID

WHERE
	LineID = @LineID

END