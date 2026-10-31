CREATE PROCEDURE [dbo].[usp_Sample_UPD_StampDate]
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SampleID		INT
DECLARE @SampleDate		DATE
DECLARE @Days			INT

IF DB_NAME() = 'CFCAIMDemo'
	BEGIN
	DECLARE Samples CURSOR FOR	
		SELECT 
			  S.SampleID
			, S.SampleDate 
		FROM 
			[CFCAIMDemo].dbo.Sample S
		ORDER BY
			S.SampleDate DESC

	OPEN Samples
	FETCH NEXT 
	FROM 
		Samples 
	INTO 
		@SampleID, @SampleDate

	SET @Days = DATEDIFF(d, @SampleDate, GETDATE())

	WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE 
				dbo.Sample
			SET
				SampleDate = DATEADD(d, @Days, SampleDate)
			WHERE
				SampleID = @SampleID 

			FETCH NEXT 
			FROM
				Samples 
			INTO 
				@SampleID, @SampleDate
		END

		CLOSE Samples
		DEALLOCATE Samples
	END
END