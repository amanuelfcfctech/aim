CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationGroup_SEL_TOP1_SupplierGroupID_IsActive]

--EXEC usp_AIMSupplierLocationGroup_SEL_TOP1_SupplierGroupID_IsActive 5, 1

	@IsActive			BIT
	, @SupplierGroupID	INT
	

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	  ASLG.AIMSupplierLocationGroupID	
	, ASLG.AIMSupplierGroupID
	, ASLG.AIMSupplierID
	, ASLG.AIMSupplierLocationID
	, ASLG.IsActive
	, ASLG.UpdateStamp
	, ASLG.CFCUserID	
	, U.UserName
	, ASL.AIMSupplierLocationCode
	, ASL.SourceLocationName	
	, AimS.AIMSupplierCode
	, ASG.AIMSupplierGroupCode
	

FROM 	
	dbo.AIMSupplierLocationGroup ASLG	

INNER JOIN	
	dbo.AIMSupplierGroup ASG 
		ON ASLG.AIMSupplierGroupID = ASG.AIMSupplierGroupID

LEFT OUTER JOIN
	dbo.AIMSourceLocation ASL
		on ASLG.AIMSupplierLocationID = ASL.AIMSourceLocationID

LEFT OUTER JOIN
	dbo.AIMSource AimS
		on ASL.AIMSourceID = AimS.AIMSourceID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ASLG.CFCUserID = U.CFCUserID
WHERE
	ASLG.IsActive = @IsActive AND
	ASLG.AIMSupplierGroupID = @SupplierGroupID
	
END