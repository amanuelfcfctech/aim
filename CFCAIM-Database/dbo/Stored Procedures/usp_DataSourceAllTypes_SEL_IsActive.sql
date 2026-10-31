CREATE PROCEDURE [dbo].[usp_DataSourceAllTypes_SEL_IsActive]
		@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_DataSourceAllTypes_SEL_IsActive true

SELECT 
	ROW_NUMBER() OVER(ORDER BY DataSourceTypeID DESC) AS RowNumber
	, *
FROM 
(


SELECT 
		  DST.DataSourceTypeID
		, DST.Name
		, DST.IsActive
		, DST.UpdateStamp
		, DST.CFCUserID				
		, case when DST.IsUserDefined = 1 then 'User-Defined' else 'System' end as IsUserDefined
		, DST.IsFurtherAnalyses
		, DST.IsSQLDatabase

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--, AU.UserName
		, U.UserName

	FROM 
		dbo.DataSourceType DST WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON DST.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
	WHERE
		DST.IsActive = @IsActive




) x

--ORDER BY RowNumber
ORDER BY isUserDefined, name

END