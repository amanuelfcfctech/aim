CREATE PROCEDURE [dbo].[usp_Sample_DEL_ID]
	  @SampleID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE FROM dbo.Sample with (rowlock) where SampleID = @SampleID

DELETE FROM dbo.SampleAIM with (rowlock) where SampleID = @SampleID

DELETE
	dbo.SampleResultPredict 
FROM dbo.SampleResult s with (rowlock) 
INNER JOIN
		dbo.SampleResultPredict p
		on p.SampleResultID = s.SampleResultID 
WHERE SampleID = @SampleID

DELETE FROM dbo.SampleResult with (rowlock) where SampleID = @SampleID

END