CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_UPD_SampleResultTemplateID]
			  @SampleResultTemplateID	INT				
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = '';

WITH MaxIterations AS (
	SELECT 
		SampleResultTemplateID
		,CAST(ROUND(EXP(SUM(LOG(TestFrequency))),0) AS INT) AS MaxIteration
	FROM
		dbo.SampleResultTemplateDetailGroup STG
	WHERE
		SampleResultTemplateID = @SampleResultTemplateID
		AND STG.IsDateFrequency = 0
	GROUP BY
		SampleResultTemplateID
) 

		UPDATE ST						
			SET ST.CurrentIteration =
				CASE 
					WHEN CurrentIteration >= M.MaxIteration THEN 1
					ELSE CurrentIteration + 1
				END
		FROM	
			dbo.SampleResultTemplate ST
		INNER JOIN
			MaxIterations M
			ON M.SampleResultTemplateID = M.SampleResultTemplateID
			WHERE
				ST.SampleResultTemplateID = @SampleResultTemplateID

	UPDATE STG
		SET STG.StartDate = GETDATE()
		FROM dbo.SampleResultTemplate ST
			inner join
				dbo.SampleResultTemplateDetailGroup STG
				ON STG.SampleResultTemplateID = ST.SampleResultTemplateID
		WHERE
			STG.IsDateFrequency = 1
			AND ST.SampleResultTemplateID = @SampleResultTemplateID
			AND GETDATE() > DATEADD(day, STG.TestFrequency, StartDate)

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END