CREATE PROCEDURE [dbo].[usp_AIMSource_SEL_SourceCode]
		  @SupplierCode		NVARCHAR(20)


--exec usp_AIMSource_SEL_SourceCode 'ADM'

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  S.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.AIMSource S WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON S.CFCUserID = U.CFCUserID

			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--LEFT OUTER JOIN
			--	dbo.aspnet_Users AU WITH(NOLOCK)
			--		ON AU.UserId = U.UserID

WHERE 

	S.AIMSupplierCode = @SupplierCode	
	AND S.IsActive = 1
END