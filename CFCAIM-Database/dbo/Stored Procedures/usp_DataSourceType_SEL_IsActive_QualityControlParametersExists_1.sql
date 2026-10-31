CREATE PROCEDURE [dbo].[usp_DataSourceType_SEL_IsActive_QualityControlParametersExists]
		@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

--usp_DataSourceType_SEL_IsActive_QualityControlParametersExists 1

SELECT 
		  DST.DataSourceTypeID
		, DST.Name
		, DST.IsActive
		, DST.UpdateStamp
		, DST.CFCUserID
		, DST.IsFurtherAnalyses
		, DST.IsUserDefined
		, DST.IsSQLDatabase
		, DST.DataSourceTypeCode

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--, AU.UserName
		, U.UserName
	FROM 
		dbo.DataSourceType DST WITH(NOLOCK)
	INNER JOIN
		
		dbo.SampleQualityParameter SQP with (nolock)
			ON DST.DataSourceTypeID = SQP.DataSourceTypeID
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON DST.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID

	WHERE
		DST.IsActive = @IsActive 

GROUP BY

  DST.DataSourceTypeID
		, DST.Name
		, DST.IsActive
		, DST.UpdateStamp
		, DST.CFCUserID
		, DST.IsFurtherAnalyses
		, DST.IsUserDefined
		, DST.IsSQLDatabase
		, DST.DataSourceTypeCode

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--, AU.UserName
		, U.UserName
		

ORDER BY IsUserDefined, Name

END