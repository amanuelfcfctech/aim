CREATE PROCEDURE [dbo].[usp_AIMSource_SEL_SourceName]
		  @SupplierName		NVARCHAR(50)


--exec usp_AIMSource_SEL_SourceName 'Valero Renewable Fuels - Albert City - IA'

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

	S.SourceName = @SupplierName	
	AND S.IsActive = 1
END