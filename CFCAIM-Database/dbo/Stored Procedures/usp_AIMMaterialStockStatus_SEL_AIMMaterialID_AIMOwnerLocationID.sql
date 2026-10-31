CREATE PROCEDURE [dbo].[usp_AIMMaterialStockStatus_SEL_AIMMaterialID_AIMOwnerLocationID]
		  @AIMMaterialID			INT
		  , @AIMOwnerLocationID		INT

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 	  
	  AML.AIMMaterialStockStatusID
	, AML.AIMMaterialID	
	, AML.AIMOwnerLocationID				
	, AM.AIMMaterialName
	, AM.AIMMaterialCode	
	, AML.UpdateStamp
	, AML.CFCUserID	
	, AOL.LocationCode
	, IsNull(AOL.LocationName, '') as AIMOwnerLocationName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMMaterialStockStatus AML WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.AIMMaterial AM with (nolock)
			ON AML.AIMMaterialID = AM.AIMMaterialID	
	LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL with (nolock)
			ON AML.AIMOwnerLocationID = AOL.AIMOwnerLocationID	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 

	AML.AIMMaterialID = @AIMMaterialID
	AND AML.AIMOwnerLocationID = @AIMOwnerLocationID
END