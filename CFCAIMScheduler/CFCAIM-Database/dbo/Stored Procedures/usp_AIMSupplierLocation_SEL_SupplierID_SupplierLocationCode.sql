CREATE PROCEDURE [dbo].[usp_AIMSupplierLocation_SEL_SupplierID_SupplierLocationCode]
		@SupplierID					INT
		, @SupplierLocationCode		NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	    S.AIMSourceLocationID
	  , S.AIMSourceID
	  , S.SourceLocationName
	  , S.AIMSupplierLocationCode
	  , S.UpdateStamp
	  , S.IsActive
	  , AU.UserName
	 
FROM 

	dbo.AIMSourceLocation S WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON S.CFCUserID = U.CFCUserID
		LEFT OUTER JOIN
			dbo.aspnet_Users AU WITH(NOLOCK)
				ON AU.UserId = U.UserID
WHERE
		S.AIMSourceID = @SupplierID 
	AND S.AIMSupplierLocationCode = @SupplierLocationCode
	AND S.IsActive = 1

ORDER BY
	S.SourceLocationName

END