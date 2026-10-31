
create PROCEDURE [dbo].[usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierGroupID_AIMSupplierLocationGroupID_ALL]
		@AIMSourceID					INT
		,@AIMSupplierGroupID			INT
		,@AIMSupplierLocationGroupID	INT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierGroupID_ALL 1,0

DECLARE @SQL				VARCHAR(MAX) = ''
SET @SQL = 
'SELECT
	    ASL.AIMSourceLocationID
	  , ASL.AIMSourceID
	  , ASL.SourceLocationName
	  , ASL.UpdateStamp
	  , ASL.IsActive
	  , U.UserName

FROM 
	dbo.AIMSourceLocation ASL WITH(NOLOCK)	

	LEFT OUTER JOIN
		dbo.AIMSupplierLocationGroup ASLG
		ON ASL.AIMSourceLocationID = ASLG.AIMSupplierLocationID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ASL.CFCUserID = U.CFCUserID
WHERE
	ASL.AIMSourceID = ' + CAST(@AIMSourceID as VARCHAR)

IF @AIMSupplierGroupID > 0 and @AIMSupplierLocationGroupID > 0
	SET @SQL += ' and ASLG.AIMSupplierGroupID = ' + CAST(@AIMSupplierGroupID as VARCHAR)


SET @SQL +=

' ORDER BY
	ASL.SourceLocationName '

EXEC (@SQL)

END