CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierGroupID_AIMSupplierLocationGroupID]
		@AIMSourceID					INT
		, @AIMSupplierGroupID			INT
		, @AIMSupplierLocationGroupID	INT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierGroupID_AIMSupplierLocationGroupID 1, 1, 2   --supplier ADM
--exec usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierGroupID_AIMSupplierLocationGroupID 2, 1, 2   --supplier Cargill

DECLARE @Count INT
DECLARE @SQL	VARCHAR(MAX) = ''
SET @SQL = 

'SELECT
	    ASL.AIMSourceLocationID
	  , ASL.AIMSourceID
	  , ASL.AIMSupplierLocationCode
	  , ASL.SourceLocationName
	  , ASL.UpdateStamp
	  , ASL.IsActive
	  , U.UserName
FROM 
	dbo.AIMSourceLocation ASL

	LEFT OUTER JOIN
		dbo.AIMSupplierLocationGroup ASLG
			ON ASL.AIMSourceLocationID = ASLG.AIMSupplierLocationID

	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON ASL.CFCUserID = U.CFCUserID

WHERE
		ASL.AIMSourceID = ' + CAST(@AIMSourceID as varchar) + ' AND ASL.IsActive = 1 '

SET @Count = 0
IF @AIMSupplierGroupID > 0 and @AIMSupplierLocationGroupID > 0
	BEGIN
		SET @Count = (SELECT count(*) from dbo.AimSupplierLocationGroup ASLG 
			Where ASLG.AimSupplierGroupID = @AIMSupplierGroupID and AIMSupplierID = @AIMSourceID)
	END
IF @Count > 0 --(if Count = 0, ALL the supplier-locations are returned for the supplier; otherwise, only specific supplier-locs are returned)
	BEGIN
		IF @AIMSupplierGroupID > 0 and @AIMSupplierLocationGroupID > 0 
			SET @SQL += ' and ASLG.AIMSupplierGroupID = ' + CAST(@AIMSupplierGroupID as VARCHAR)
	END
	
SET @SQL +=

' ORDER BY
	ASL.SourceLocationName '

EXEC (@SQL)
--print @SQL

END