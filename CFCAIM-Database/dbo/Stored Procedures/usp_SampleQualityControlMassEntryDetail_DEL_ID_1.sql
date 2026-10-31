CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntryDetail_DEL_ID]
	  @SampleQualityControlMassEntryDetailID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.SampleQualityControlMassEntryDetail	WITH(ROWLOCK)
WHERE 
	SampleQualityControlMassEntryDetailID = @SampleQualityControlMassEntryDetailID
END