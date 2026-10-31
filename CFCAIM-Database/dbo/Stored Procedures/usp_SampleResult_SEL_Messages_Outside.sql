CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_Messages_Outside]
				  @Type					INT 				
				, @SampleID				INT			= NULL
				, @StartDate			DATE		= NULL
				, @EndDate				DATE		= NULL
				, @IsActive				BIT			= NULL
				
AS
BEGIN

-- Type:
-- 1 = total count of messages
-- 2 = count grouped by SampleResultPredictID	(for Warning grid)
-- 3 = detail of messages						(for Message-Detail grid)
-- 4 = count grouped by AnalyzerNutrientID		(for Result grid)

-- exec usp_SampleResult_SEL_Messages_Outside 1, 5575, NULL, NULL, TRUE

SET NOCOUNT ON;
DECLARE @SQL		VARCHAR(5000)
DECLARE @Where		VARCHAR(500)
DECLARE @From		VARCHAR(2000)
DECLARE @Select		VARCHAR(5000)

-- Where -------------------------------------------------------------------------------------
Set @Where = ''
If @SampleID IS NOT NULL
	Set @Where += ' SR.SampleID = ' + Cast(@SampleID as varchar)		
IF @StartDate IS NOT NULL 
	Begin
		IF LEN(@Where) > 0
			Begin
				Set @Where += ' and '
			End
		Set @Where += ' SR.UpdateStamp >= ''' + Convert(varchar,@StartDate,112) + ' 00:00:00.000'' '		
	End
IF @EndDate IS NOT NULL
	Begin
		IF LEN(@Where) > 0
			Begin
				Set @Where += ' and '
			End
		Set @Where += ' SR.UpdateStamp <= ''' + Convert(varchar,@EndDate,112) + ' 23:59:59.999'' '		
	End
IF LEN(@Where) > 0
	Set @Where = ' WHERE ' + @Where

-- From --------------------------------------------------------------------------------------
Set @From = ' FROM dbo.Sample S with (nolock)
				INNER JOIN	
					dbo.SampleResult SR with (nolock)
						ON SR.SampleID = S.SampleID '
	IF @IsActive IS NOT NULL
		Set @From += ' AND SR.IsActive = ' + Convert(varchar, @IsActive) + '' 

Set @From += 'INNER JOIN 
					dbo.SampleResultPredict SRP with (nolock)
						ON SRP.SampleResultID = SR.SampleResultID 
				INNER JOIN 
					dbo.AnalyzerNutrient AN with (nolock)
						ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
						AND AN.IsActive = 1 				
			'

-- Select ------------------------------------------------------------------------------------
SET @Select = 'SELECT SampleResultPredictID, AN.AnalyzerNutrientID, ''Result Value < Minimum'' as Message, 1 as Counter ' + @From + @Where
						+ ' AND (ResultValue < MinMaterialRange AND MinMaterialRange <> 0)
				UNION 
				SELECT SampleResultPredictID, AN.AnalyzerNutrientID, ''Result Value > Maximum'' as Message, 1 as Counter ' + @From + @Where
						+ ' AND (ResultValue > MaxMaterialRange AND MaxMaterialRange <> 0)
				UNION 
				SELECT SampleResultPredictID, AN.AnalyzerNutrientID, ''GlobalH > Limit'' as Message, 1 as Counter ' + @From + @Where
						+ ' AND GlobalH > Limit 							
			  '

----------------------------------------------------------------------------------------------
-- Type:
-- 1 = total count of messages
-- 2 = count grouped by SampleResultPredictID	(for Warning grid)
-- 3 = detail of messages						(for Message-Detail grid)
-- 4 = count grouped by AnalyzerNutrientID		(for Result grid)


IF @Type = 1  
	SET @SQL = 'SELECT Count(*) as Message FROM (' + @Select + ') X '

ELSE IF @Type = 2
	SET @SQL = 'SELECT SampleResultPredictID, Sum(Counter) as Message FROM (' + @Select + '
				UNION 
					SELECT SampleResultPredictID, AN.AnalyzerNutrientID, '''' as Message, 0 as Counter ' + @From +  @Where
						+ ' AND (ResultValue >= MinMaterialRange AND MinMaterialRange <> 0) 
							AND (ResultValue <= MaxMaterialRange AND MaxMaterialRange <> 0)
							AND (GlobalH <= Limit) 							
							) X
					GROUP BY X.SampleResultPredictID '	
ELSE IF @Type = 3	
	Begin		
		SET @SQL = @Select 
		SET @SQL += ' UNION 
					SELECT SampleResultPredictID,  AN.AnalyzerNutrientID, '''' as Message, 0 as Counter ' + @From + @Where
						+ ' AND (ResultValue >= MinMaterialRange AND MinMaterialRange <> 0)
							AND (ResultValue <= MaxMaterialRange AND MaxMaterialRange <> 0)
							AND (GlobalH <= Limit) 							
						  '
	End
ELSE IF @Type = 4
	SET @SQL = 'SELECT AnalyzerNutrientID, Sum(Counter) as Message FROM (' + @Select + '
				UNION 
					SELECT SampleResultPredictID, AN.AnalyzerNutrientID, '''' as Message, 0 as Counter ' + @From + @Where
						+ ' AND (ResultValue >= MinMaterialRange AND MinMaterialRange <> 0) 
							AND (ResultValue <= MaxMaterialRange AND MaxMaterialRange <> 0) 
							AND (GlobalH <= Limit) 
				UNION 
					SELECT 0, AN.AnalyzerNutrientID, '''' as Message, 0 as Counter 
					FROM dbo.SampleResult SR WITH (NOLOCK)
					LEFT JOIN dbo.SampleResultPredict SRP WITH (NOLOCK)	ON SRP.SampleResultID = SR.SampleResultID 
					INNER JOIN 	dbo.AnalyzerNutrient AN WITH(NOLOCK) ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID AND AN.IsActive = 1 '
						+ @Where + ' AND SRP.SampleResultID IS NULL 
				
				
				) X
				GROUP BY X.AnalyzerNutrientID '	


--PRINT @sql
EXEC (@SQL)
	
END