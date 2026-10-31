
CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_Messages_Inside]
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

-- exec [usp_SampleResult_SEL_Messages_Inside] 3, 4183, NULL, NULL, 1

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
						ON SR.SampleID = S.SampleID
				LEFT OUTER JOIN 
					dbo.SampleResultPredict SRP with (nolock)
						ON SRP.SampleResultID = SR.SampleResultID 
				INNER JOIN 
					dbo.AnalyzerNutrient AN with (nolock)
						ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID '
	IF @IsActive IS NOT NULL
		Set @From += ' AND SR.IsActive = ' + Convert(varchar, @IsActive) + ''				
						 						
Set @From += '	LEFT JOIN
					dbo.AIMNutrientRefAnalyzerNutrient ANRAN with (nolock)
						ON SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID				
				LEFT JOIN 
					dbo.WarningLimits WL with (nolock)
						ON S.AimMaterialID = WL.AimMaterialID AND
						   ANRAN.AimNutrientID = WL.AimNutrientID	
			'

-- Select ------------------------------------------------------------------------------------
SET @Select = 'SELECT SampleResultPredictID, AN.AnalyzerNutrientID, ''MinMax Warning'' as Message, 1 as Counter '
						+ @From + @Where
						+ ' AND (ResultValue < WL.Min OR ResultValue > WL.Max) 
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
					SELECT SampleResultPredictID, AN.AnalyzerNutrientID, '''' as Message, 0 as Counter ' + @From + @Where
						+ ' AND (WL.WarningLimitsID IS NULL)  
				) X
				GROUP BY X.SampleResultPredictID '	

ELSE IF @Type = 3		
	SET @SQL = @Select + ' UNION 
				SELECT SampleResultPredictID,  AN.AnalyzerNutrientID, '''' as Message, 0 as Counter ' + @From + @Where
					+ ' AND (WL.WarningLimitsID IS NULL) '	

ELSE IF @Type = 4
	SET @SQL = 'SELECT AnalyzerNutrientID, Sum(Counter) as Message FROM (' + @Select + '
				UNION 
					SELECT SampleResultPredictID, AN.AnalyzerNutrientID, '''' as Message, 0 as Counter  ' + @From + @Where
						+ ' AND WL.WarningLimitsID IS NULL										
				) X
				GROUP BY X.AnalyzerNutrientID '	

--PRINT @sql
EXEC (@SQL)
	
END