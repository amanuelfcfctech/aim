CREATE PROCEDURE [dbo].[usp_Sample_SEL_Label]
		@SampleID		    INT
		, @PrintNameFirst	BIT

--exec usp_Sample_SEL_Label 264758, 0

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

--  ***************************
--  *** the label fields below are truncated to 13 characters; any more and the label runs on to 2 pages ***
--  ***************************

-- 3/26/18 - changed LoadNumber and TicketNumber lines below because of "ID" fields -- the codes/names for those ID's aren't on the
--				sample table, so added left joins to get the location name, supplier name, etc.

------------------------------------------------------------------------------------------------------------------------
SET @SQL = @SQL + '	  
	  
	  , SO.AIMSourceID
	  , SO.SourceName
	  , SL.AIMSourceLocationID
	  , LEFT(SL.SourceLocationName, 20) AS SourceLocationName
	  , SA.SampleCode '

------------------------------------------------------------------------------------------------------------------------
-- 6/29/21 - removed join on AlternateNameID
--, (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID and A.AlternateNameID = L.AlternateNameID WHERE L.LabelFieldFirst = 1) AS LabelForLoadNumber
--	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID and A.AlternateNameID = L.AlternateNameID WHERE L.LabelFieldLast = 1) AS LabelForTicketNumber
--	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID and A.AlternateNameID = L.AlternateNameID WHERE L.LabelFieldLine2 = 1) AS LabelForLabelLine2
--	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID and A.AlternateNameID = L.AlternateNameID WHERE L.LabelFieldLine3 = 1) AS LabelForLabelLine3
--	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID and A.AlternateNameID = L.AlternateNameID WHERE L.LabelFieldLine4 = 1) AS LabelForLabelLine4	  
--	  , ' +

------------------------------------------------------------------------------------------------------------------------
-- The next 5 lines are the CAPTIONS for the 5 labels columns

SET @SQL = @SQL + '

	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelFieldFirst = 1) AS LabelForLoadNumber	  
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelFieldLine2 = 1) AS LabelForLabelLine2
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelFieldLine3 = 1) AS LabelForLabelLine3
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelFieldLine4 = 1) AS LabelForLabelLine4
	  , (Select TOP 1 Left(A.AlternateName,13) From dbo.AlternateName A INNER JOIN dbo.Labels L ON A.ControlID = L.ControlID  WHERE L.LabelFieldLast = 1) AS LabelForTicketNumber	  
	  ' 

------------------------------------------------------------------------------------------------------------------------	  
-- The next 5 lines indicate whether or not the line should be bold

SET @SQL = @SQL + '

	  , (Select Line1Bold From dbo.Labels L WHERE L.LabelFieldFirst = 1) AS Line1Bold
	  , (Select Line2Bold From dbo.Labels L WHERE L.LabelFieldLine2 = 1) AS Line2Bold
	  , (Select Line3Bold From dbo.Labels L WHERE L.LabelFieldLine3 = 1) AS Line3Bold
	  , (Select Line4Bold From dbo.Labels L WHERE L.LabelFieldLine4 = 1) AS Line4Bold
	  , (Select Line5Bold From dbo.Labels L WHERE L.LabelFieldLast = 1) AS Line5Bold
	  , ' 	  	    	  
	  +


------------------------------------------------------------------------------------------------------------------------	  
-- The next 5 sections get the actual field name to be used to get the DATA for the 5 label columns

-- 7/2/21 - Added another case for AIMMaterialName because there is only one controlID for material	       

			(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
			case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
			FROM dbo.SampleFilter S 			
			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
			INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
			WHERE L.LabelFieldFirst = 1) + ' as LoadNumber

	  , ' +
			(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
			case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
			FROM dbo.SampleFilter S 			
			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
			INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
			WHERE L.LabelFieldLast = 1) + ' as TicketNumber

	  , ' +
			(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
			case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
			FROM dbo.SampleFilter S 			
			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
			INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
			WHERE L.LabelFieldLine2 = 1) + ' as LabelFieldLine2

	 , ' +
			(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
			case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
			FROM dbo.SampleFilter S 			
			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
			INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
			WHERE L.LabelFieldLine3 = 1) + ' as LabelFieldLine3

	 , ' +
			(Select TOP 1 case when S.SearchField IS NULL or S.SearchField = 'Null' then 'SA.' + S.SampleTableFieldName Else    
			case when S.SearchField = 'AnalyzerMaterialName' then 'AIMMaterialName' else
			case when S.SearchField = 'AIMMaterialName' and @PrintNameFirst = 0 then 'AIMMaterialCode' else S.SearchField  end end end
			FROM dbo.SampleFilter S 			
			INNER JOIN dbo.AlternateName A on S.ControlID = A.ControlID
			INNER JOIN dbo.Labels L on A.AlternateNameID = L.AlternateNameID
			WHERE L.LabelFieldLine4 = 1) + ' as LabelFieldLine4
	 


					  
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
