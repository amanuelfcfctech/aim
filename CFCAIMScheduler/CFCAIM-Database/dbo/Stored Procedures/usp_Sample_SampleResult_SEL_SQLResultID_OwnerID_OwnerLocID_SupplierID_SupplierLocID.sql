CREATE PROCEDURE [dbo].[usp_Sample_SampleResult_SEL_SQLResultID_OwnerID_OwnerLocID_SupplierID_SupplierLocID]
	@SQLResultID	    NVARCHAR(300)
	, @OwnerID			INT
	, @OwnerLocID		INT
	, @SupplierID		INT
	, @SupplierLocID	INT

-- exec dbo.usp_Sample_SampleResult_SEL_SQLResultID_OwnerID_OwnerLocID_SupplierID_SupplierLocID '18_3301_20-49805', 1, 3, 3, 5
	

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	    S.*
	  , CPA.ProductionVersion
	  , CPA.ProductionDate
	  , U.UserName		  
	 
FROM 	
	dbo.Sample S WITH(NOLOCK)

INNER JOIN 
	dbo.SampleResult SR 
		ON S.SampleID = SR.SampleID

LEFT OUTER JOIN
	dbo.CFCProductArchive CPA
		ON S.CFCProductArchiveID = CPA.CFCProductArchiveID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON S.CFCUserID = U.CFCUserID
WHERE 
	SR.SQLResultID = @SQLResultID 
	and S.AIMOwnerID = @OwnerID
	and S.AIMOwnerLocationID = @OwnerLocID
	and S.AIMSourceID = @SupplierID
	and S.AIMSourceLocationID = @SupplierLocID

ORDER BY CreatedStamp desc

END