CREATE PROCEDURE [dbo].[usp_SampleQualityParameter_SEL_IsActive]
			
	@IsActive INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SQP.*
	, DT.Name as DataSourceUserName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	, UT.UnitTypeDescription

FROM 
	dbo.SampleQualityParameter SQP WITH(NOLOCK)

	LEFT OUTER JOIN
	dbo.DataSourceType DT with (nolock)
		ON SQP.DataSourceTypeID = DT.DataSourceTypeID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SQP.CFCUserID = U.CFCUserID

	--4/25/22 - added for Butterball
	LEFT OUTER JOIN 
		dbo.UnitType UT 
			ON SQP.UnitTypeID = UT.UnitTypeID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	SQP.IsActive = @IsActive
END