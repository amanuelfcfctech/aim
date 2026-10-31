CREATE PROCEDURE [dbo].[usp_Chart_DataSourceCompare]
		@AIMMaterialID INT = NULL
		, @AIMNutrientID INT = NULL			
		, @DataSourceTypeID INT = NULL		
		, @DataSourceTypeID2 INT = NULL		
		, @DataSourceTypeID3 INT = NULL	
		, @DataSourceTypeID4 INT = NULL		
		, @SampleCodeFrom NVARCHAR(20) = NULL
		, @SampleCodeTo NVARCHAR(20) = NULL
		, @StartDate Date = NULL
		, @EndDate Date = NULL
	
	--exec usp_Chart_DataSourceCompare 96, 72, 10012, 1, 0, 0, '', '', '2018-06-01', '2018-06-30'


	AS
	BEGIN
	SET NOCOUNT ON;

	DECLARE @SQL			VARCHAR(6000) = ''
	DECLARE @Where			VARCHAR(1000) = ''
	DECLARE @WhereValue		VARCHAR(1000) = ''	
	DECLARE @AndOr			Int = 0

	IF @AIMMaterialID = 0
		BEGIN
			SET @AIMMaterialID = NULL
		END
	IF @AIMNutrientID = 0
		BEGIN
			SET @AIMNutrientID = NULL
		END
	
	IF @DataSourceTypeID = 0
		BEGIN
			SET @DataSourceTypeID = NULL
		END	
	IF @DataSourceTypeID2 = 0
		BEGIN
			SET @DataSourceTypeID2 = NULL
		END
	IF @DataSourceTypeID3 = 0
		BEGIN
			SET @DataSourceTypeID3 = NULL
		END
	IF @DataSourceTypeID4 = 0
		BEGIN
			SET @DataSourceTypeID4 = NULL
		END
	
	IF Len(@SampleCodeFrom) = 0
		BEGIN
			SET @SampleCodeFrom = NULL
		END
	IF Len(@SampleCodeTo) = 0
		BEGIN
			SET @SampleCodeTo = NULL
		END


	SET @Where = 'WHERE AN.IsActive = 1 and SR.IsActive = 1 '

	if @AIMMaterialID IS NOT NULL
		SET @Where += ' and S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' '		
	if @AIMNutrientID IS NOT NULL
		SET @Where += ' and X.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) + ' '
				
	if (@SampleCodeFrom IS NOT NULL)
		SET @Where += ' and S.SampleCode >= ''' + Cast(@SampleCodeFrom as varchar) + ' '''
	if (@SampleCodeTo IS NOT NULL)
		SET @Where += ' and S.SampleCode <= ''' + Cast(@SampleCodeTo as varchar) + ' '''			
	
	If @StartDate IS NOT NULL
		SET @Where += ' and S.SampleDate >= ''' + convert(varchar,@StartDate) + ''' '
	If @EndDate IS NOT NULL
		SET @Where += ' and S.SampleDate <= ''' + convert(varchar,@EndDate) + ''' '
			

	SET @SQL = 'SELECT '

	If @StartDate IS NOT NULL
		SET @Sql += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
	If @EndDate is NOT NULL
		SET @Sql += '''' + convert(varchar,@EndDate) + ''' as ToDate, '

	SET @SQL += ' 
		   S.SampleCode, S.AlternateCode,  S.SampleID, S.SampleDate, S.AIMMaterialID, x.AimNutrientID, AN.AimNutrientName, S.AIMSourceID, S.AimSourceLocationID '
		   
		    if @DataSourceTypeID IS NULL and @DataSourceTypeID2 IS NULL and @DataSourceTypeID3 IS NULL and @DataSourceTypeID4 IS NULL
				SET @SQL += ', ResultValue '
			if @DataSourceTypeID IS NOT NULL
				SET @SQL += ', case when s1.ResultValue IS NULL THEN 0 else s1.ResultValue end as ResultValue '
			if @DataSourceTypeID2 IS NOT NULL
				SET @SQL += ', case when s2.ResultValue IS NULL THEN 0 else s2.ResultValue end as ResultValue2 '
			if @DataSourceTypeID3 IS NOT NULL
				SET @SQL += ', case when s3.ResultValue IS NULL THEN 0 else s3.ResultValue end as ResultValue3 '
			if @DataSourceTypeID4 IS NOT NULL
				SET @SQL += ', case when s4.ResultValue IS NULL THEN 0 else s4.ResultValue end as ResultValue4 '

	SET @SQL += '
	FROM 		dbo.Sample S with(nolock)
	INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
	INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
	INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
	INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
	INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID 	'

	if @DataSourceTypeID IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and ANAL.DataSourceTypeID = ' + cast(@DataSourceTypeID as varchar)										
			SET @SQL += ') s1 on S.SampleID = s1.SampleID '
		End

	if @DataSourceTypeID2 IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and ANAL.DataSourceTypeID = ' + cast(@DataSourceTypeID2 as varchar)										
			SET @SQL += ') s2 on S.SampleID = s2.SampleID '
		End

	if @DataSourceTypeID3 IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and ANAL.DataSourceTypeID = ' + cast(@DataSourceTypeID3 as varchar)
			SET @SQL += ') s3 on S.SampleID = s3.SampleID '
		End

	if @DataSourceTypeID4 IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and ANAL.DataSourceTypeID = ' + cast(@DataSourceTypeID4 as varchar)
			SET @SQL += ') s4 on S.SampleID = s4.SampleID '
		End
	

	SET @SQL += @Where 



	if @DataSourceTypeID IS NOT NULL or @DataSourceTypeID2 IS NOT NULL or @DataSourceTypeID3 IS NOT NULL or @DataSourceTypeID4 IS NOT NULL
		Begin

			SET @SQL += ' and ('

			if @DataSourceTypeID IS NOT NULL 
				Begin
					SET @SQL += 's1.ResultValue IS NOT NULL '
					SET @AndOr = 1
				End
			if @DataSourceTypeID2 IS NOT NULL
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @SQL += 's2.ResultValue IS NOT NULL '
					SET @AndOr = 1
				End
			if @DataSourceTypeID3 IS NOT NULL
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @SQL += 's3.ResultValue IS NOT NULL '
					SET @AndOr = 1
				End
			if @DataSourceTypeID4 IS NOT NULL
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @SQL += 's4.ResultValue IS NOT NULL '
				End
		
			SET @SQL += ') ' 

		End
		
	SET @SQL += 
	'ORDER BY S.CREATEDSTAMP '

	--print @Sql
	EXEC (@SQL)

	END