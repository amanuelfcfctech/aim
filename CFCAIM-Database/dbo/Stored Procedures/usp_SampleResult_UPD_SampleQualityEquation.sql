CREATE PROCEDURE [dbo].[usp_SampleResult_UPD_SampleQualityEquation]

	@StartDate			DATETIME
	, @EndDate			DATETIME
	, @CFCUser			INT
	, @AIMDatabaseID	INT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SampleID		INT = 0
DECLARE @Cnt			INT = 0

DECLARE tempcur scroll cursor for
SELECT 
	S.SampleID 
FROM	
	dbo.Sample S
WHERE
	S.SampleDate >= @StartDate and S.SampleDate <= @EndDate
ORDER BY 
	S.SampleID

Open tempcur
Fetch Next from tempcur into @SampleID
While @@FETCH_STATUS = 0
Begin
	
	-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
	--Exec dbo.usp_SampleResult_INS_SampleQualityEquation @SampleID, @CFCUser, @AIMDatabaseID	
	Exec dbo.usp_SampleResult_INS_SampleQualityEquation @SampleID, @CFCUser, @AIMDatabaseID,0	

	--print @SampleID
	--SET @Cnt = @Cnt + 1
	Fetch Next from tempcur into @SampleID	
	
	
End	

print @Cnt
Close tempcur
Deallocate tempcur

END