--USE [CFCAIMElmira]
--GO
--/****** Object:  StoredProcedure [dbo].[usp_Sample_SEL_LabelFormat]    Script Date: 04/30/2026 9:16:22 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO


-----------------------------------------------------
CREATE PROCEDURE [dbo].[usp_Sample_SEL_LabelFormat]
		@SampleID		    INT 
		, @PrintNameFirst	BIT 
		, @IngrOrProd		INT 

--exec usp_Sample_SEL_LabelFormat 522, 0, 1
--exec usp_Sample_SEL_LabelFormat 9791, 0, 2

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''
DECLARE @LabelLineData		VARCHAR(500) = ''			--Task1141 DB251215

--  ***************************
--  *** the label fields below are truncated to 13 characters; any more and the label runs on to 2 pages ***
--  ***************************

SET @SQL = '

SELECT
	    SA.SampleID	  
	  , SA.SampleDate
	  , SA.IsActive
	  , AM.AIMMaterialID '

If @PrintNameFirst = 1
	SET @SQL = @SQL + ' , LEFT(AM.AIMMaterialName, 20) as AIMMaterialCode '
Else
	SET @SQL = @SQL + ', AM.AIMMaterialCode '

SET @SQL = @SQL + '	  
	  
	  , SO.AIMSourceID
	  , SO.SourceName
	  , SL.AIMSourceLocationID
	  , LEFT(SL.SourceLocationName, 20) AS SourceLocationName
	  , SA.SampleCode '

------------------------------------------------------------------------------------------------------------------------
-- The next 5 lines are the CAPTIONS for the 5 labels columns

SET @SQL = @SQL + '

	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Control C ON A.ControlID = C.ControlID INNER JOIN dbo.Label L on C.ControlID = L.ControlID INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID   WHERE LF.OrderNumber = 1 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line1Caption
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Control C ON A.ControlID = C.ControlID INNER JOIN dbo.Label L on C.ControlID = L.ControlID INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID   WHERE LF.OrderNumber = 2 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line2Caption	  
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Control C ON A.ControlID = C.ControlID INNER JOIN dbo.Label L on C.ControlID = L.ControlID INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID   WHERE LF.OrderNumber = 3 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line3Caption	  
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Control C ON A.ControlID = C.ControlID INNER JOIN dbo.Label L on C.ControlID = L.ControlID INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID   WHERE LF.OrderNumber = 4 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line4Caption	  
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Control C ON A.ControlID = C.ControlID INNER JOIN dbo.Label L on C.ControlID = L.ControlID INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID   WHERE LF.OrderNumber = 5 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line5Caption	  
	  ' 
------------------------------------------------------------------------------------------------------------------------	  
-- The next 5 lines indicate whether or not the line should be bold

SET @SQL = @SQL + '

	  , (Select FontSize From dbo.LabelFormat LF WHERE LF.OrderNumber = 1 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line1Size
	  , (Select FontSize From dbo.LabelFormat LF WHERE LF.OrderNumber = 2 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line2Size
	  , (Select FontSize From dbo.LabelFormat LF WHERE LF.OrderNumber = 3 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line3Size
	  , (Select FontSize From dbo.LabelFormat LF WHERE LF.OrderNumber = 4 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line4Size
	  , (Select FontSize From dbo.LabelFormat LF WHERE LF.OrderNumber = 5 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line5Size ' 	 	  
------------------------------------------------------------------------------------------------------------------------	  
-- The next 5 lines indicate whether or not the line should be bold

SET @SQL = @SQL + '

	  , (Select IsBold From dbo.LabelFormat LF WHERE LF.OrderNumber = 1 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line1Bold
	  , (Select IsBold From dbo.LabelFormat LF WHERE LF.OrderNumber = 2 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line2Bold
	  , (Select IsBold From dbo.LabelFormat LF WHERE LF.OrderNumber = 3 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line3Bold
	  , (Select IsBold From dbo.LabelFormat LF WHERE LF.OrderNumber = 4 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line4Bold
	  , (Select IsBold From dbo.LabelFormat LF WHERE LF.OrderNumber = 5 and LF.MaterialTypeID = ' + Cast(@IngrOrProd as varchar) + ') AS Line5Bold
	  , ' 	  	    	  
	  
	-- Task1141 BD251215 Below is the querying the control table to set LineNData based on the Order Number
	SET @LabelLineData = ''
	SELECT @LabelLineData = TableColumnName FROM dbo.LabelFormat LF INNER JOIN dbo.Label L ON L.LabelID = LF.LabelID WHERE LF.MaterialTypeID = Cast(@IngrOrProd as varchar) AND LF.OrderNumber = 1
	SET @LabelLineData = CASE WHEN len(@LabelLineData) = 0 THEN '''''' ELSE @LabelLineData END + ' AS Line1DataUnformatted ,' + CHAR(13)
	SET @SQL = @SQL + @LabelLineData

	SET @LabelLineData = ''
	SELECT @LabelLineData = TableColumnName FROM dbo.LabelFormat LF INNER JOIN dbo.Label L ON L.LabelID = LF.LabelID WHERE LF.MaterialTypeID = Cast(@IngrOrProd as varchar) AND LF.OrderNumber = 2
	SET @LabelLineData = CASE WHEN len(@LabelLineData) = 0 THEN '''''' ELSE @LabelLineData END + ' AS Line2DataUnformatted ,' + CHAR(13)
	SET @SQL = @SQL + @LabelLineData

	SET @LabelLineData = ''
	SELECT @LabelLineData = TableColumnName FROM dbo.LabelFormat LF INNER JOIN dbo.Label L ON L.LabelID = LF.LabelID WHERE LF.MaterialTypeID = Cast(@IngrOrProd as varchar) AND LF.OrderNumber = 3
	SET @LabelLineData = CASE WHEN len(@LabelLineData) = 0 THEN '''''' ELSE @LabelLineData END + ' AS Line3DataUnformatted ,'  + CHAR(13)
	SET @SQL = @SQL + @LabelLineData

	SET @LabelLineData = ''
	SELECT @LabelLineData = TableColumnName FROM dbo.LabelFormat LF INNER JOIN dbo.Label L ON L.LabelID = LF.LabelID WHERE LF.MaterialTypeID = Cast(@IngrOrProd as varchar) AND LF.OrderNumber = 4
	SET @LabelLineData = CASE WHEN len(@LabelLineData) = 0 THEN '''''' ELSE @LabelLineData END + ' AS Line4DataUnformatted ,' + CHAR(13)
	SET @SQL = @SQL + @LabelLineData

	SET @LabelLineData = ''
	SELECT @LabelLineData = TableColumnName FROM dbo.LabelFormat LF INNER JOIN dbo.Label L ON L.LabelID = LF.LabelID WHERE LF.MaterialTypeID = Cast(@IngrOrProd as varchar) AND LF.OrderNumber = 5
	SET @LabelLineData = CASE WHEN len(@LabelLineData) = 0 THEN '''''' ELSE @LabelLineData END + ' AS Line5DataUnformatted ' 
	SET @SQL = @SQL + @LabelLineData
------------------------------------------------------------------------------------------------------------------------	  
-- The next 5 sections get the actual field name to be used to get the DATA for the 5 label columns
   

--	Task1141 DB2512115
--		Note: Changed the way we pull the label data. We added a column to the control table to identify which data column to pull from (LabelTableColumnName)
	--(Select case when (

	--		(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--		case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--		FROM dbo.SampleFilter S 			
	--		INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--		INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--		INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--		WHERE LF.OrderNumber = 1
	--		and MaterialTypeID = @IngrOrProd) ) IS NULL then '''''' else
			
	--			(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--			FROM dbo.SampleFilter S 			
	--			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--			INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--			INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--			WHERE LF.OrderNumber = 1
	--			and MaterialTypeID = @IngrOrProd)  end)		
			
	--		+ ' as Line1Data

	--  , ' +

	--  (Select case when (
	--		(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--		case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--		FROM dbo.SampleFilter S 			
	--		INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--		INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--		INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--		WHERE LF.OrderNumber = 2
	--		and MaterialTypeID = @IngrOrProd) ) IS NULL then '''''' else

	--			(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--			FROM dbo.SampleFilter S 			
	--			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--			INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--			INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--			WHERE LF.OrderNumber = 2
	--			and MaterialTypeID = @IngrOrProd) end)

	--		+ ' as Line2Data

	--  , ' +

	--  (Select case when (
	--		(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--		case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--		FROM dbo.SampleFilter S 			
	--		INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--		INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--		INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--		WHERE LF.OrderNumber = 3
	--		and MaterialTypeID = @IngrOrProd)) IS NULL then '''''' else

	--			(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--			FROM dbo.SampleFilter S 			
	--			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--			INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--			INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--			WHERE LF.OrderNumber = 3
	--			and MaterialTypeID = @IngrOrProd) end)

	--		+ ' as Line3Data

	-- , ' +

	-- (Select case when (
	--		(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--		case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--		FROM dbo.SampleFilter S 			
	--		INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--		INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--		INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--		WHERE LF.OrderNumber = 4
	--		and MaterialTypeID = @IngrOrProd) ) IS NULL then '''''' else 
			
	--			(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--			FROM dbo.SampleFilter S 			
	--			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--			INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--			INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--			WHERE LF.OrderNumber = 4
	--			and MaterialTypeID = @IngrOrProd) end)

	--		+ ' as Line4Data

	-- , ' +

	-- (Select case when (
	--		(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--		case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--		FROM dbo.SampleFilter S 			
	--		INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--		INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--		INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--		WHERE LF.OrderNumber = 5
	--		and MaterialTypeID = @IngrOrProd) ) IS NULL then '''''' else 
			
	--			(Select TOP 1  case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
	--			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else S.SearchField end end					
	--			FROM dbo.SampleFilter S 			
	--			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID			
	--			INNER JOIN dbo.Label L on A.ControlID = L.ControlID
	--			INNER JOIN dbo.LabelFormat LF on L.LabelID = LF.LabelID
	--			WHERE LF.OrderNumber = 5
	--			and MaterialTypeID = @IngrOrProd) end)

	--		+ ' as Line5Data

SET @SQL = @SQL + '	 
	 	 					  
FROM 
	dbo.Sample SA WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.AIMMaterial AM WITH(NOLOCK)
			ON AM.AIMMaterialID = SA.AIMMaterialID
	LEFT OUTER JOIN
		dbo.AIMOwner AO with (nolock)
			ON AO.AIMOwnerID = SA.AIMOwnerID
	LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL with (nolock)
			ON AOL.AIMOwnerLocationID = SA.AIMOwnerLocationID
	LEFT OUTER JOIN
		dbo.AIMSource SO WITH(NOLOCK)
			ON SO.AIMSourceID = SA.AIMSourceID
	LEFT OUTER JOIN
		dbo.AIMSourceLocation SL WITH(NOLOCK)
			ON SL.AIMSourceLocationID = SA.AIMSourceLocationID
	LEFT OUTER JOIN
		dbo.DataSourceType D with (nolock)
			ON SA.DataSourceTypeID = D.DataSourceTypeID
	LEFT OUTER JOIN
		dbo.AnalyzerMaterial AN 
			ON AN.AnalyzerMaterialID = SA.AnalyzerMaterialID
	
	
WHERE 
	SA.SampleID = ' + + Cast(@SampleID as varchar)



EXEC (@Sql)
--print (@Sql)


END