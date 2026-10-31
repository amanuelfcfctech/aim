CREATE PROCEDURE [dbo].[usp_SampleResultPredict_INS]
			  @SampleResultID			INT
			, @IsInPredictionRange		BIT
			, @GlobalH					NUMERIC(18,8)	
			, @Limit					NUMERIC(18,8)	
			, @IsInMaterialRange		BIT
			, @MaxMaterialRange			NUMERIC(18,8)	
			, @MinMaterialRange			NUMERIC(18,8)			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
						
INSERT
	dbo.SampleResultPredict WITH(ROWLOCK)
		(
		  SampleResultID
		, IsInPredictionRange
		, GlobalH
		, Limit
		, IsInMaterialRange
		, MaxMaterialRange
		, MinMaterialRange
		)
VALUES
		(
		  @SampleResultID
		, @IsInPredictionRange
		, @GlobalH
		, @Limit
		, @IsInMaterialRange
		, @MaxMaterialRange
		, @MinMaterialRange
		)
	
SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
