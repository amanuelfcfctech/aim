CREATE PROCEDURE [dbo].[usp_Chart_Trend]
		@AIMMaterialID INT = NULL
		, @AIMNutrientID INT = NULL			

		, @AIMSourceID INT = NULL
		, @AIMSourceLocationID INT = NULL

		, @AIMSourceID2 INT = NULL
		, @AIMSourceLocationID2 INT = NULL

		, @AIMSourceID3 INT = NULL
		, @AIMSourceLocationID3 INT = NULL
		, @AIMSourceID4 INT = NULL
		, @AIMSourceLocationID4 INT = NULL		
		, @SampleCodeFrom NVARCHAR(20) = NULL
		, @SampleCodeTo NVARCHAR(20) = NULL
		, @StartDate Date = NULL
		, @EndDate Date = NULL

		, @AIMOwnerID INT = NULL
		, @AIMOwnerLocationID INT = NULL

		, @AIMOwnerID2 INT = NULL
		, @AIMOwnerLocationID2 INT = NULL

		, @AIMOwnerID3 INT = NULL
		, @AIMOwnerLocationID3 INT = NULL

		, @AIMOwnerID4 INT = NULL
		, @AIMOwnerLocationID4 INT = NULL

		, @SupplierOwnerOption INT = NULL		-- 1=Supplier only;  2=Owner only;  3=Both Supplier and Owner
	
	--exec usp_Chart_Trend 1, 2,  2,4,  2,2,  3,6,  4,5, '', '', '2020-01-13', '2020-02-13'
	--exec usp_Chart_Trend 1, 2,  0,0,  0,0,  0,0,  0,0, '', '', '2020-01-13', '2020-01-16'
	--exec usp_Chart_Trend 1, 8,  1,1,  1,6,  0,0,  0,0, '', '', '2015-01-01', '2021-06-28', 1, 3
	--exec usp_Chart_Trend 1, 8,  1,6,  1,1,  0,0,  0,0, '', '', '2015-01-01', '2021-06-28', 1, 1
	--exec usp_Chart_Trend 96, 8,  0,0,  0,0,  0,0,  0,0, '', '', '2021-06-28', '2021-07-30', 1,11, 1,46, 0,0, 0,0, 2
	
	
	AS
	BEGIN
	SET NOCOUNT ON;

	DECLARE @SQL			VARCHAR(6000) = ''
	DECLARE @Where			VARCHAR(1000) = ''
	DECLARE @WhereValue		VARCHAR(1000) = ''	
	DECLARE @AndOr			Int = 0

	------------------------------------------------------------------
	--7/9/21 - Line1 deals with @AIMSourceID, @AIMSourceLocationID, @AIMOwnerID and @AIMOwnerLocationID
	--		   Line2 deals with @AIMSourceID2, @AIMSourceLocationID2, @AIMOwnerID2 and @AIMOwnerLocationID2
	--		   Etc.
	Declare @Line1			varchar(1000) = ''
	Declare @Line2			varchar(1000) = ''
	Declare @Line3			varchar(1000) = ''
	Declare @Line4			varchar(1000) = ''
	------------------------------------------------------------------

	IF @AIMMaterialID = 0
		BEGIN
			SET @AIMMaterialID = NULL
		END
	IF @AIMNutrientID = 0
		BEGIN
			SET @AIMNutrientID = NULL
		END
	
	IF @AIMSourceID = 0
		BEGIN
			SET @AIMSourceID = NULL
		END
	IF @AIMSourceLocationID = 0
		BEGIN
			SET @AIMSourceLocationID = NULL
		END

	IF @AIMSourceID2 = 0
		BEGIN
			SET @AIMSourceID2 = NULL
		END
	IF @AIMSourceLocationID2 = 0
		BEGIN
			SET @AIMSourceLocationID2 = NULL
		END

	IF @AIMSourceID3 = 0
		BEGIN
			SET @AIMSourceID3 = NULL
		END
	IF @AIMSourceLocationID3 = 0
		BEGIN
			SET @AIMSourceLocationID3 = NULL
		END

	IF @AIMSourceID4 = 0
		BEGIN
			SET @AIMSourceID4 = NULL
		END
	IF @AIMSourceLocationID4 = 0
		BEGIN
			SET @AIMSourceLocationID4 = NULL
		END

	-- 6/28/21 -------------------------------
	IF @AIMOwnerID = 0
		BEGIN
			SET @AIMOwnerID = NULL
		END
	IF @AIMOwnerLocationID = 0
		BEGIN
			SET @AIMOwnerLocationID = NULL
		END
	
	-- 7/9/21 --------------------------------
	IF @AIMOwnerID2 = 0
		BEGIN
			SET @AIMOwnerID2 = NULL
		END
	IF @AIMOwnerLocationID2 = 0
		BEGIN
			SET @AIMOwnerLocationID2 = NULL
		END
	IF @AIMOwnerID3 = 0
		BEGIN
			SET @AIMOwnerID3 = NULL
		END
	IF @AIMOwnerLocationID3 = 0
		BEGIN
			SET @AIMOwnerLocationID3 = NULL
		END
	IF @AIMOwnerID4 = 0
		BEGIN
			SET @AIMOwnerID4 = NULL
		END
	IF @AIMOwnerLocationID4 = 0
		BEGIN
			SET @AIMOwnerLocationID4 = NULL
		END
	------------------------------------------
	
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
		SET @Where += ' and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) + ' '
				
	if (@SampleCodeFrom IS NOT NULL)
		SET @Where += ' and S.SampleCode >= ''' + Cast(@SampleCodeFrom as varchar) + ' '''
	if (@SampleCodeTo IS NOT NULL)
		SET @Where += ' and S.SampleCode <= ''' + Cast(@SampleCodeTo as varchar) + ' '''			
	
	If @StartDate IS NOT NULL
		SET @Where += ' and S.SampleDate >= ''' + convert(varchar,@StartDate) + ''' '
	If @EndDate IS NOT NULL
		SET @Where += ' and S.SampleDate <= ''' + convert(varchar,@EndDate) + ''' '

	
	-- 7/9/21 --------------------------------	
	if @SupplierOwnerOption = 2 or @SupplierOwnerOption = 3		--OwnerOnly or SupplierAndOwner
		Begin
			if @AIMOwnerID IS NOT NULL		
				Set @Line1 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID as varchar) + ' '	
			if @AIMOwnerLocationID IS NOT NULL	
				Set @Line1 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '	
			if @Line1 <> ''
				Set @Line1 = '(' + @Line1 + ')'	
			if @AIMOwnerID2 IS NOT NULL		
				Set @Line2 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID2 as varchar) + ' '	
			if @AIMOwnerLocationID2 IS NOT NULL	
				Set @Line2 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID2 as varchar) + ' '	
			if @Line2 <> ''
				Set @Line2 = '(' + @Line2 + ')'
			if @AIMOwnerID3 IS NOT NULL		
				Set @Line3 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID3 as varchar) + ' '	
			if @AIMOwnerLocationID3 IS NOT NULL	
				Set @Line3 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID3 as varchar) + ' '	
			if @Line3 <> ''
				Set @Line3 = '(' + @Line3 + ')'	
			if @AIMOwnerID4 IS NOT NULL		
				Set @Line4 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID4 as varchar) + ' '	
			if @AIMOwnerLocationID4 IS NOT NULL	
				Set @Line4 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID4 as varchar) + ' '	
			if @Line4 <> ''
				Set @Line4 = '(' + @Line4 + ')'
		End 

	------------------------------------------
	SET @SQL = 'SELECT '

	If @StartDate IS NOT NULL
		SET @Sql += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
	If @EndDate is NOT NULL
		SET @Sql += '''' + convert(varchar,@EndDate) + ''' as ToDate, '

	SET @SQL += ' 
		   S.SampleCode, S.AlternateCode,  S.SampleID, S.SampleDate, S.AIMMaterialID, x.AimNutrientID, AN.AimNutrientName, S.AIMSourceID, S.AimSourceLocationID '
		   
		    if @AIMSourceID IS NULL and @AIMSourceID2 IS NULL and @AIMSourceID3 IS NULL and @AIMSourceID4 IS NULL
				SET @SQL += ', ResultValue '
			if @AIMSourceID IS NOT NULL
				SET @SQL += ', case when s1.ResultValue IS NULL THEN 0 else s1.ResultValue end as ResultValue '
			if @AIMSourceID2 IS NOT NULL
				SET @SQL += ', case when s2.ResultValue IS NULL THEN 0 else s2.ResultValue end as ResultValue2 '
			if @AIMSourceID3 IS NOT NULL
				SET @SQL += ', case when s3.ResultValue IS NULL THEN 0 else s3.ResultValue end as ResultValue3 '
			if @AIMSourceID4 IS NOT NULL
				SET @SQL += ', case when s4.ResultValue IS NULL THEN 0 else s4.ResultValue end as ResultValue4 '

	SET @SQL += '
	FROM 		dbo.Sample S with(nolock)
	INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
	INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
	INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
	INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
	INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID 	'

	if @AIMSourceID IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and S.AIMSourceID = ' + cast(@AIMSourceID as varchar)

					if @AIMSourceLocationID IS NOT NULL						
						SET @Sql += ' and S.AIMSourceLocationID = ' + cast(@AIMSourceLocationID as varchar)						

					--7/9/21
					if @Line1 <> ''
						SET @Sql += ' and ' + @Line1

			SET @SQL += ') s1 on S.SampleID = s1.SampleID '
		End

	if @AIMSourceID2 IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and S.AIMSourceID = ' + cast(@AIMSourceID2 as varchar)

					if @AIMSourceLocationID2 IS NOT NULL					
						SET @Sql += ' and S.AIMSourceLocationID = ' + cast(@AIMSourceLocationID2 as varchar)

					--7/9/21
					if @Line2 <> ''
						SET @Sql += ' and ' + @Line2

			SET @SQL += ') s2 on S.SampleID = s2.SampleID '
		End

	if @AIMSourceID3 IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and S.AIMSourceID = ' + cast(@AIMSourceID3 as varchar)

					if @AIMSourceLocationID3 IS NOT NULL						
						SET @Sql += ' and S.AIMSourceLocationID = ' + cast(@AIMSourceLocationID3 as varchar)

					--7/9/21
					if @Line3 <> ''
						SET @Sql += ' and ' + @Line3
						
			SET @SQL += ') s3 on S.SampleID = s3.SampleID '
		End

	if @AIMSourceID4 IS NOT NULL
		Begin
			SET @SQL += '	left outer join (
					SELECT 	   	S.SampleID, ResultValue
					FROM 		dbo.Sample S with(nolock)
					INNER JOIN 		dbo.SampleResult sr with(nolock)			ON S.SampleID = sr.SampleID
					INNER JOIN 		dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)			ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
					INNER JOIN 		dbo.AnalyzerNutrient ANAL			ON x.AnalyzerNutrientID = ANAL.AnalyzerNutrientID
					INNER JOIN 		dbo.aimnutrient AN with(nolock)			ON x.AIMNutrientID = AN.AIMNutrientID	
					INNER JOIN 		dbo.AIMMaterial AM with(nolock)			ON S.AIMMaterialID = AM.AIMMaterialID ' + @Where + ' 
					and S.AIMSourceID = ' + cast(@AIMSourceID4 as varchar)

					if @AIMSourceLocationID IS NOT NULL					
						SET @Sql += ' and S.AIMSourceLocationID = ' + cast(@AIMSourceLocationID4 as varchar)
					
					--7/9/21
					if @Line4 <> ''
						SET @Sql += ' and ' + @Line4
					
			SET @SQL += ') s4 on S.SampleID = s4.SampleID '
		End
	

	SET @SQL += @Where 



	if @AIMSourceID IS NOT NULL or @AIMSourceID2 IS NOT NULL or @AIMSourceID3 IS NOT NULL or @AIMSourceID4 IS NOT NULL
		Begin

			SET @SQL += ' and ('

			if @AIMSourceID IS NOT NULL 
				Begin
					SET @SQL += 's1.ResultValue IS NOT NULL '
					SET @AndOr = 1
				End
			if @AIMSourceID2 IS NOT NULL
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @SQL += 's2.ResultValue IS NOT NULL '
					SET @AndOr = 1
				End
			if @AIMSourceID3 IS NOT NULL
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @SQL += 's3.ResultValue IS NOT NULL '
					SET @AndOr = 1
				End
			if @AIMSourceID4 IS NOT NULL
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @SQL += 's4.ResultValue IS NOT NULL '
				End
		
			SET @SQL += ') ' 

		End
	
	------------------------------------------------------------------------------
	-- When run by Owner only
	if @SupplierOwnerOption = 2
	Begin
	
		SET @AndOr = 0
		If @Line1 <> '' or @Line2 <> '' or @Line3 <> '' or @Line4 <> ''
		Begin				
			Set @Sql += ' and ('
			if @Line1 <> ''	
				Begin
					SET @Sql += @Line1
					SET @AndOr = 1
				End
			if @Line2 <> ''
				Begin
					if @AndOr = 1					
						SET @SQL += ' or '
					SET @Sql += @Line2
					SET @AndOr = 1
				End
			if @Line3 <> ''
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @Sql += @Line3
					SET @AndOr = 1
				End
			if @Line4 <> ''
				Begin
					if @AndOr = 1
						SET @SQL += ' or '
					SET @Sql += @Line4
				End
			Set @Sql += ') '
		End 
	End
	------------------------------------------------------------------------------	
	
	SET @SQL += 
	' ORDER BY S.CREATEDSTAMP '

	--print @Sql
	EXEC (@SQL)

	END