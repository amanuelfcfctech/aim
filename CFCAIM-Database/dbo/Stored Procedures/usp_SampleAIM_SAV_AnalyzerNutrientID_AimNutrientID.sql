CREATE PROCEDURE [dbo].[usp_SampleAIM_SAV_AnalyzerNutrientID_AimNutrientID]
				@AnalyzerNutrientID	INT				
			  , @AIMNutrientID		INT			  
			  , @CFCUserID			INT	= NULL	
			  , @AimDatabaseID		INT
			  , @DataSourceTypeID	INT 		  


--exec usp_SampleAIM_SAV_AnalyzerNutrientID_AimNutrientID 1, 2, 2, 1016, 13008

			
AS
BEGIN

SET NOCOUNT ON;

DECLARE @RecordID			INT = 0
DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''

-----------------------------------------------------------
SELECT 
		  SR.SampleID
		, SR.SampleResultID
		, IsNull(S.DataSourceTypeID, 0) as DataSourceTypeID
		, SR.ResultValue 	
	INTO 
		##TempTable
	FROM 
		dbo.AnalyzerNutrient AN with (nolock)
	LEFT JOIN 
		dbo.SampleResult SR with (nolock) ON AN.AnalyzerNutrientID = SR.AnalyzerNutrientID
	LEFT JOIN 
		dbo.Sample S ON SR.SampleID = S.SampleID
	WHERE 
		AN.AnalyzerNutrientID = Cast(@AnalyzerNutrientID as varchar) AND
		dbo.CheckPriority(SR.SampleID, 
				Cast(@AnalyzerNutrientID as varchar),  
				Cast(@DataSourceTypeID as varchar),  
				Cast(@AimDatabaseID as varchar),
				Cast(@AnalyzerNutrientID as varchar)) = 1			--2/7/24

	
-----------------------------------------------------------
DELETE 
	dbo.SampleAIM
FROM
	dbo.SampleAim SA 
	INNER JOIN
		##TempTable TT
			ON  SA.SampleID = TT.SampleID 
			AND SA.SampleResultID = TT.SampleResultID 
WHERE	
	SA.AIMNutrientID =  CAST(@AIMNutrientID as varchar)


-------------------------------------------------------------
INSERT INTO dbo.SampleAIM with (rowlock)(
		  SampleID
		, SampleResultID
		, AIMNutrientID
		, AIMUsedValue
		, UpdateStamp
		, CFCUserID) 
	SELECT 
		  T.SampleID
		, T.SampleResultID
		, Cast(@AimNutrientID as varchar)
		, T.ResultValue
		, GETDATE() 
		, @CFCUserID
	FROM
		##TempTable T 
	
-----------------------------------------------------------
DROP TABLE ##TempTable 

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage


END