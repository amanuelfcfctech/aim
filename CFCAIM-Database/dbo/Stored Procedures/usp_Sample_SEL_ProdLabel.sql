CREATE PROCEDURE [dbo].[usp_Sample_SEL_ProdLabel]
		@SampleID		    INT
		, @PrintNameFirst	BIT

--exec usp_Sample_SEL_ProdLabel 369, 0

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''


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

------------------------------------------------------------------------------------------------------------------------
SET @SQL = @SQL + '	  
	  
	  , 0 as AIMSourceID
	  , '''' as SourceName
	  , 0 as AIMSourceLocationID
	  , '''' AS SourceLocationName
	  , SA.SampleCode '

------------------------------------------------------------------------------------------------------------------------
-- The next 5 lines are the CAPTIONS for the 5 labels columns

SET @SQL = @SQL + '

	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelProdFieldLine1 = 1) AS LabelForLoadNumber	  
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelProdFieldLine2 = 1) AS LabelForLabelLine2
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelProdFieldLine3 = 1) AS LabelForLabelLine3
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelProdFieldLine4 = 1) AS LabelForLabelLine4
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelProdFieldLine5 = 1) AS LabelForTicketNumber	  
	  ' 

	  
------------------------------------------------------------------------------------------------------------------------	  
-- The next 5 lines indicate whether or not the line should be bold

SET @SQL = @SQL + '

	  , (Select ProdLine1Bold From dbo.Labels L WHERE L.LabelProdFieldLine1 = 1) AS Line1Bold
	  , (Select ProdLine2Bold From dbo.Labels L WHERE L.LabelProdFieldLine2 = 1) AS Line2Bold
	  , (Select ProdLine3Bold From dbo.Labels L WHERE L.LabelProdFieldLine3 = 1) AS Line3Bold
	  , (Select ProdLine4Bold From dbo.Labels L WHERE L.LabelProdFieldLine4 = 1) AS Line4Bold
	  , (Select ProdLine5Bold From dbo.Labels L WHERE L.LabelProdFieldLine5 = 1) AS Line5Bold
	  , ' 	
	  
	  	   
	  +
	  --SET @SQL = @SQL +


------------------------------------------------------------------------------------------------------------------------	  
-- The next 5 sections get the actual field name to be used to get the DATA for the 5 label columns


		(Select CASE WHEN
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine1 = 1) IS NULL
			THEN ''' '''
			ELSE
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine1 = 1)
			END)
			+ ' as LoadNumber

	  , ' +
			(Select CASE WHEN
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine2 = 1) IS NULL 
			THEN ''' '''
			ELSE
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine2 = 1) 
			END) 
			+ ' as LabelFieldLine2  

	  , ' +
			(Select CASE WHEN
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine3 = 1) IS NULL
			THEN ''' '''
			ELSE
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine3 = 1)
			END) 
			+ ' as LabelFieldLine3

	 , ' +
			(Select CASE WHEN
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine4 = 1) IS NULL 
			THEN ''' '''
			ELSE
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine4 = 1)
			END) 
			+ ' as LabelFieldLine4

	 , ' +
			(Select CASE WHEN
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine5 = 1) IS NULL
			THEN ''' '''
			ELSE
				(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
				case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
				case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
				FROM dbo.SampleFilter S 			
				INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
				INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
				WHERE L.LabelProdFieldLine5 = 1)
			END) 
			+ ' as TicketNumber
	 


					  
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