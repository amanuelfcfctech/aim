CREATE PROCEDURE [dbo].[usp_ProductToleranceHeader_SEL_ProductToleranceHeaderID]

@ProductToleranceHeaderID	INT

AS
BEGIN

--exec usp_ProductToleranceHeader_SEL_ProductToleranceHeaderID 57

DECLARE @SQL				VARCHAR(MAX) = ''


Declare @SpecFrom as Int 
SET @SpecFrom = (SELECT SpecFrom FROM dbo.ProductToleranceHeader PTH WHERE PTH.ProductToleranceHeaderID = @ProductToleranceHeaderID)


SET NOCOUNT ON;

SET @SQL = 
'SELECT

	  PTH.ProductToleranceHeaderID
	, PTH.ProductToleranceHeaderName	
	, PTH.AIMMaterialID
	, PTH.AIMMaterialCode
	, PTH.UpdateStamp
	, PTH.CFCUserID	
	, PTH.SpecFrom
	, U.UserName
	, AM.AIMMaterialCode
	, AM.AIMMaterialName '

If @SpecFrom = 1
	Begin
		SET @SQL = @SQL + ', CPA.ArchiveVersion '
		SET @SQL = @SQL + ', CPA.ProductionVersion '
	End 

SET @SQL = @SQL + '
FROM 
	dbo.ProductToleranceHeader PTH WITH(NOLOCK) 

LEFT OUTER JOIN 
	dbo.CFCProductRefAIMMaterial x
		ON PTH.AIMMaterialID = x.AIMMaterialID '

-- If ProductTolerance is Using Spec from Formulation, add the CFCProductArchive table
If @SpecFrom = 1
	Begin

	SET @SQL = @SQL + '
		LEFT OUTER JOIN
			dbo.CFCProductArchive CPA
				ON x.CFCProductID = CPA.CFCProductID '
	End

SET @SQL = @SQL + '
LEFT OUTER JOIN 
	dbo.AIMMaterial AM
		ON PTH.AIMMaterialID = AM.AIMMaterialID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON PTH.CFCUserID = U.CFCUserID

WHERE
	PTH.ProductToleranceHeaderID = ' + CAST(@ProductToleranceHeaderID AS VARCHAR) 
	
EXEC (@SQL)
--print @SQL	

END