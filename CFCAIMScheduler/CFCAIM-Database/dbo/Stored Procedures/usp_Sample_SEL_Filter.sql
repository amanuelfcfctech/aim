CREATE PROCEDURE [dbo].[usp_Sample_SEL_Filter]	
	
	@FilterControlID	INT			 = NULL
	, @Value			NVARCHAR(50) = NULL
	, @DateBeg			NVARCHAR(23) = NULL
	, @DateEnd			NVARCHAR(23) = NULL

-- 1/11/21 - removed link to aspnet_User table
	
AS
BEGIN
SET NOCOUNT ON;

--exec usp_Sample_SEL_Filter 44, 'Salisbury'
--exec usp_Sample_SEL_Filter 5, 'ddddd', '2015-08-19', '2015-08-19'
--exec usp_Sample_SEL_Filter 0, '', '2015-08-19', '2015-08-19'
--exec usp_Sample_SEL_Filter 42, 'Test', '', ''
--exec usp_Sample_SEL_Filter 5, 'Test', '6/12/2015', '6/12/2015'

DECLARE @SQL	VARCHAR(2000) = ''
DECLARE @Where	VARCHAR(500) = '' 
DECLARE @InnerJoin VARCHAR(500) = ''
DECLARE @WhereDate VARCHAR(500) = ''
DECLARE @Fieldname VARCHAR(50) = ''
DECLARE @SearchTable VARCHAR(25) = ''
DECLARE @SearchField VARCHAR(25) = ''

IF @FilterControlID <> 0 OR @DateBeg IS NOT NULL OR @DateEnd IS NULL
Begin

	If @FilterControlID <> 0
		Begin
			SET @Fieldname = (Select SampleTableFieldName From dbo.SampleFilter with (nolock)
								Where ControlID = @FilterControlID)

			IF @FieldName IN ('SampleCode', 'ContractNumber', 'PONumber', 'InvoiceNumber','TicketNumber', 
								'LoadNumber', 'BillOfLoad', 'VehicleNumber', 'LotNumber', 'AlternateCode')				
				SET @Where = '(' + @FieldName + ' LIKE ''%' + @Value + '%'')'		
			
			ELSE
				Begin
				IF @FieldName IN ('AnalyzerMaterialID','AIMOwnerID', 'AIMOwnerLocationID',
								'AIMSourceID', 'AIMSourceLocationID',
								'DataSourceTypeID', 'FurtherAnalysisID', 'AIMMaterialID')
					Begin
				
						SET @SearchTable = (Select SearchTable From dbo.SampleFilter with (nolock)
							Where ControlID = @FilterControlID) 
						SET @SearchField = (Select SearchField From dbo.SampleFilter with (nolock)
							Where ControlID = @FilterControlID) 

						IF Len(@SearchTable) > 0 and Len(@SearchField) > 0
							Begin								
								SET @InnerJoin = ' Inner join ' + @SearchTable + ' T on S.' + @FieldName + ' = T.' + @FieldName
								SET @Where = '(T.' + @SearchField + ' LIKE ''%' + @Value + '%'')'		
												
							End
					End			 
				End								
		End
		
		
	If Len(@DateBeg) > 0 AND len(@DateEnd) > 0	
		SET @WhereDate = '(SampleDate >= Cast(''' + @DateBeg + ''' as date) AND SampleDate <= Cast(''' + @DateEnd + ''' as date) )'	
	Else if Len(@DateBeg) > 0
		SET @WhereDate = '(SampleDate >= Cast(''' + @DateBeg + ''' as date) )'	
	Else if Len(@DateEnd) > 0
		SET @WhereDate = '(SampleDate <= Cast(''' + @DateEnd + ''' as date) )'				

	if Len(@Where) > 0 
		SET @Where = ' WHERE ' + @Where 

	if Len(@Where) > 0 and Len(@WhereDate) > 0
		SET @Where = @Where + ' AND ' + @WhereDate

	if Len(@Where) = 0 and Len(@WhereDate) > 0
		SET @Where = ' WHERE ' + @WhereDate 
		
SET @SQL = '
	SELECT S.SampleID
		, CASE WHEN S.SampleCode IS NULL or len(S.SampleCode) = 0 then	
			convert(varchar, S.SampleID) else S.SampleCode End as SampleCode      
		,S.CreatedStamp
		,S.AIMOwnerID
		,S.AIMSourceID
		,S.AIMSourceLocationID
		,S.ContractNumber
		,S.PoNumber
		,S.InvoiceNumber
		,S.TicketNumber
		,S.LoadNumber
		,S.BillOfLoad
		,S.VehicleNumber
		,S.LotNumber
		,S.IsActive
		,S.AnalyzerMaterialID
		,S.SampleDate
		,S.AnalysisDate
		,S.AIMOwnerLocationID		
		,S.CFCUserID
		,S.UpdateStamp
		,S.FurtherAnalysisID
		,S.AIMMaterialID
		,S.AlternateCode
		,S.DataSourceTypeID 
		,S.SupportingDocumentPath
		
		  
	FROM 
		dbo.Sample S WITH(NOLOCK) '

	if len(@InnerJoin) > 0
		SET @SQL += @InnerJoin
	
	+ '		
		
		LEFT OUTER JOIN
				CFCUser.CFCUser U WITH(NOLOCK)
					ON S.CFCUserID = U.CFCUserID	'

	if len(@Where) > 0
		SET @SQL += @Where
	
	EXEC(@SQL)
	--print @SQL
		
	END
	
END