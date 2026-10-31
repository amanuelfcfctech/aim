CREATE PROCEDURE [Vitapro].[usp_Lot_INS]

	(@CFCUserID		INT)

AS

-- truncate table vitapro.lot
-- exec Vitapro.usp_Lot_INS 1

BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''
DECLARE @ReturnID		        INT = 0

	BEGIN
		INSERT INTO Vitapro.Lot (SampleID, LotStatusID, 
			ProductionStatusLine1, ProductionStatusLine2, ProductionStatusLine3, ProductionStatusLine4, 
			ExportStatus, UpdateStamp, CFCUserID,
			Concept5ExportStatus							--9/2/20
			)
		SELECT 
			S.SampleID, 1, 1, 1, 1, 1, 0, GetDate(), @CFCUserID,0
		
		FROM
			dbo.Sample S
		INNER JOIN 
			dbo.SampleResult SR 
				ON S.SampleID = SR.SampleID
		INNER JOIN
			dbo.AIMMaterial am
				ON S.AIMMaterialID = am.AIMMaterialID
		LEFT OUTER JOIN 
			Vitapro.Lot L 
				ON S.SampleID = L.SampleID
		WHERE L.SampleID IS NULL

		--------and S.SampleDate >= '2018-01-01' and S.SampleID NOT IN (24853, 25279, 1908)
		
		
	GROUP BY S.SampleID
	END
	
SELECT 
	@ReturnID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END