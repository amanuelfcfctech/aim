create PROCEDURE [dbo].[usp_SampleAIM_SAV_AnalyzerNutrientID_AimNutrientID_SAV2]
				@AnalyzerNutrientID	INT				
			  , @AIMNutrientID		INT			  
			  , @CFCUserID			INT	= NULL	
			  , @AimDatabaseID		INT 		  

--exec usp_SampleAIM_SAV_AnalyzerNutrientID_AimNutrientID 1, 2, 2, 1016
			
AS
BEGIN

SET NOCOUNT ON;

DECLARE @DataSourceTypeID	INT = 0
DECLARE @ResultValue		NUMERIC(18,8) = 0
DECLARE @SQL			    VARCHAR(6000) = ''

-----------------------------------------------------------
SET @SQL = ' 
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
		AN.AnalyzerNutrientID = ' + Cast(@AnalyzerNutrientID as varchar) + ' AND
		dbo.CheckPriority(SR.SampleID, ' + 
				Cast(@AnalyzerNutrientID as varchar) + ', ' + 
				Cast(@DataSourceTypeID as varchar) + ', ' + 
				Cast(@AimDatabaseID as varchar) + ') 
			= 1 '	
----EXEC(@SQL)
PRINT (@SQL)

	
-----------------------------------------------------------
SET @SQL = ' 
	DELETE SA
	FROM
		dbo.SampleAim SA 
	WHERE EXISTS (Select *	
					FROM ##TempTable
					WHERE	SA.SampleID = ##TempTable.SampleID and
							SA.SampleResultID = ##TempTable.SampleResultID and 
							SA.AIMNutrientID = ' + CAST(@AIMNutrientID as varchar)
----EXEC(@SQL)
PRINT (@SQL)

-----------------------------------------------------------
SET @SQL = ' 
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
		, ' + Cast(@AimNutrientID as varchar) + '
		, T.ResultValue
		, GETDATE() 
		, ' + Cast(@CFCUserID as varchar) + '
	FROM
		##TempTable T '
		
----EXEC (@SQL)
PRINT (@SQL)
	
-----------------------------------------------------------
SET @SQL = ' DROP ##TempTable '
----EXEC (@SQL)
PRINT (@SQL)

END