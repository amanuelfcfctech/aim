CREATE FUNCTION [dbo].[AnalysisReports_CFCProductType_WhereStatement]
(
	  @QueryTemplateID			INT
	  
	 
)
RETURNS VARCHAR(MAX)
AS
BEGIN	

-- exec dbo.AnalysisReports_CFCProductType_WhereStatement 20

	DECLARE @ID					INT = 0
	DECLARE @IsAnd				BIT = 1
	DECLARE @ParenOpen			VARCHAR(10)
	DECLARE @ParenClose			VARCHAR(10)	
	Declare @PrevID				INT = 0		
	DECLARE @Where				VARCHAR(MAX) = ''
	DECLARE @WhereValue			VARCHAR(MAX) = ''
	
	
						
	DECLARE WhereCur scroll cursor for
		SELECT QTD.QueryFieldID, QTD.IsAnd, '' as ParenOpen, '' as ParenClose, QF.FieldName + ' ' +  
						CASE WHEN QTD.Value > 0 THEN REPLACE(QO.Command, '@VALUE', QTD.Value) 
						ELSE CASE WHEN QTD.DateValue IS NULL THEN QO.Command
						ELSE CASE WHEN QTD.SecondDate IS NULL THEN REPLACE(QO.Command, '@VALUE', '''' + CAST(QTD.DateValue AS VARCHAR) + '''')
						ELSE REPLACE(REPLACE(QO.Command, '@DateValue', '''' + CAST(QTD.DateValue AS VARCHAR) + '''') , '@SecondDate', '''' + CAST(QTD.SecondDate AS VARCHAR) + '''')						
						END END END AS WhereValue		
						
		FROM	
			dbo.QueryTemplateDetail QTD 
			INNER JOIN dbo.QueryField QF ON QF.QueryFieldID = QTD.QueryFieldID 
			INNER JOIN dbo.QueryOperation QO ON QO.QueryOperationID = QTD.QueryOperationID 	
		WHERE QTD.QueryTemplateID = @QueryTemplateID AND QF.QueryFieldID = 14
		ORDER BY QTD.QueryFieldID
						
		OPEN WhereCur
		FETCH NEXT FROM WhereCur INTO @ID, @IsAnd, @ParenOpen, @ParenClose, @WhereValue
			WHILE @@FETCH_STATUS = 0		
				BEGIN									
						
					if @PrevID <> @ID and @PrevID <> 0
						SET @Where += ')'										
					if @PrevID <> @ID	
						SET @Where +=  ' AND ('
					else											
						Begin
							if @IsAnd = 1
								SET @Where +=  ' AND '	
							else	
								SET @Where +=  ' OR '
						End					
					SET @Where += @WhereValue	
					SET @PrevID = @ID			
						
					FETCH NEXT FROM WhereCur INTO @ID, @IsAnd, @ParenOpen, @ParenClose, @WhereValue				
				END
			if LEN(@Where) > 0
				SET @Where += ')'
			
		CLOSE WhereCur
		DEALLOCATE WhereCur

		---Print @Where
	
	RETURN @Where

END