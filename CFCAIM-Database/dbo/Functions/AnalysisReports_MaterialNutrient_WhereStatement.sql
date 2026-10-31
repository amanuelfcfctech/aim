CREATE FUNCTION [dbo].[AnalysisReports_MaterialNutrient_WhereStatement]
(
	   @QueryTemplateID			INT
	 
)
RETURNS VARCHAR(MAX)
AS
BEGIN	

-- exec dbo.Analysis_MaterialNutrient_WhereStatement 21

	DECLARE @ID					INT = 0
	DECLARE @ParenOpen			VARCHAR(10)
	DECLARE @ParenClose			VARCHAR(10)	
	Declare @PrevID				INT = 0
	DECLARE @PrevCFCID			INT = 0
	DECLARE @WhereNutrient		VARCHAR(MAX) = ''
	DECLARE @MatNutWhere		VARCHAR(MAX) = ''
	DECLARE @WhereValue			VARCHAR(MAX) = ''
	
						
	DECLARE MatNutCursor scroll cursor for
	SELECT QTD.Value, '' as ParenOpen, '' as ParenClose, QF.FieldName + ' ' + 
	CASE WHEN QTD.Value > 0 THEN REPLACE(QO.Command, '@VALUE', QTD.Value) ELSE QO.Command END AS WhereValue, 
						' ' + QF.FieldName2 + ' = ' + CAST(QTDN.AIMNutrientID as varchar) +  ' ' AS WhereNutrient
		FROM
		dbo.QueryTemplateDetail QTD 
		INNER JOIN dbo.QueryField QF ON QF.QueryFieldID = QTD.QueryFieldID 
		INNER JOIN dbo.QueryOperation QO ON QO.QueryOperationID = QTD.QueryOperationID 
		LEFT OUTER JOIN dbo.QueryTemplateDetailNutrient QTDN on QTD.QueryTemplateDetailID = QTDN.QueryTemplateDetailID	
	WHERE QTD.QueryTemplateID = @QueryTemplateID AND QF.QueryFieldID = 9		
	ORDER BY QTD.Value
	OPEN MatNutCursor
	FETCH NEXT FROM MatNutCursor INTO @ID, @ParenOpen, @ParenClose, @WhereValue, @WhereNutrient
		WHILE @@FETCH_STATUS = 0		
			BEGIN	
							
				if @PrevID <> @ID and @PrevID <> 0
					SET @MatNutWhere += ')'	
						
				if @PrevID <> @ID	
					Begin
						Begin
							if @PrevID = 0
								set @MatNutWhere += ' AND ('
							else
								set @MatNutWhere += ') OR '
						End
						SET @MatNutWhere +=  '(' + @WhereValue + ' AND (' + @WhereNutrient 
					End
				else
					SET @MatNutWhere +=  ' OR ' + @WhereNutrient 																

				SET @PrevID = @ID										
				FETCH NEXT FROM MatNutCursor INTO @ID, @ParenOpen, @ParenClose, @WhereValue, @WhereNutrient
				
			END
							
			if LEN(@MatNutWhere) > 0									
				SET @MatNutWhere += ')) )'				

	CLOSE MatNutCursor
	DEALLOCATE MatNutCursor
					


	
	RETURN @MatNutWhere

END