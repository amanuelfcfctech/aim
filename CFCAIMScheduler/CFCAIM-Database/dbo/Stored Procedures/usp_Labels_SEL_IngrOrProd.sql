CREATE PROCEDURE [dbo].[usp_Labels_SEL_IngrOrProd]

	@IngrOrProd		NVARCHAR(1)
	
AS
BEGIN

DECLARE @SQL				VARCHAR(MAX) = ''
DECLARE @Where				VARCHAR(MAX) = ''


--exec usp_Labels_SEL_IngrOrProd 'P'

SET NOCOUNT ON;

SET @SQL = '
SELECT
	
	  L.LabelsID	
	, L.ControlID
	, C.ControlName	
	, AT.AlternateNameID	
	, L.LabelFieldFirst
	, L.LabelFieldLast	
	, C.UserControlName		
	, AT.AlternateName		
	, L.UpdateStamp
	, U.UserName	
	, L.LabelFieldLine2
	, L.LabelFieldLine3
	, L.LabelFieldLine4
	, Line1Bold
	, Line2Bold
	, Line3Bold
	, Line4Bold
	, Line5Bold

	, LabelProdFieldLine1
	, LabelProdFieldLine2
	, LabelProdFieldLine3
	, LabelProdFieldLine4
	, LabelProdFieldLine5
	,ProdLine1Bold
	,ProdLine2Bold
	,ProdLine3Bold
	,ProdLine4Bold
	,ProdLine5Bold

	
FROM 
	dbo.Labels L
INNER JOIN dbo.Control C 
	ON	L.ControlID = C.ControlID
INNER JOIN 	dbo.AlternateName AT
	ON L.AlternateNameID = AT.AlternateNameID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON C.CFCUserID = U.CFCUserID '

if @IngrOrProd = 'P' 
SET @SQL = @SQL + 
	'WHERE C.ControlID NOT IN (43, 44, 113, 114) '

SET @SQL = @SQL + 
'ORDER BY C.UserControlName'

EXEC (@SQL)


END