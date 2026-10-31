CREATE PROCEDURE [dbo].[usp_AIMSource_SEL_IsActive_For_Utility]
		@IsActive		BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  S.AIMSourceID
	  , CASE WHEN S.AIMSupplierCode IS NULL THEN LEFT(S.SourceName,20) ELSE S.AIMSupplierCode end as AIMSupplierCode	  
	  , CASE WHEN S.SourceName IS NULL THEN S.AIMSupplierCode ELSE S.SourceName end as SourceName	  
	  , S.IsActive
	  , S.UpdateStamp
	  , S.CFCUserID

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.AIMSource S WITH(NOLOCK)
LEFT OUTER JOIN  	
	dbo.Sample SAMP  		
		ON S.AIMSourceID = SAMP.AIMSourceID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON S.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

WHERE 
	S.IsActive = @IsActive
	AND SAMP.SampleID IS NOT NULL

GROUP BY
	  CASE WHEN S.SourceName IS NULL THEN S.AIMSupplierCode ELSE S.SourceName end
	  , S.AIMSourceID
	  , CASE WHEN S.AIMSupplierCode IS NULL THEN LEFT(S.SourceName,20) ELSE S.AIMSupplierCode end	  
	  , S.IsActive
	  , S.UpdateStamp
	  , S.CFCUserID

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

END