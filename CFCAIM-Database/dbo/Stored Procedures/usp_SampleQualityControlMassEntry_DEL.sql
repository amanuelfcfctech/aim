CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntry_DEL]
				@SampleQualityControlMassEntryID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleQualityControlMassEntryDetail WITH(ROWLOCK)
WHERE
	SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID


DELETE
FROM 
	dbo.SampleQualityControlMassEntry WITH(ROWLOCK)
WHERE
	SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID

END