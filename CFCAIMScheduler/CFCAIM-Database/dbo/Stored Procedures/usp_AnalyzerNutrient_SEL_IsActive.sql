CREATE PROCEDURE [dbo].[usp_AnalyzerNutrient_SEL_IsActive]
		@IsActive		BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  NN.*
	, NN.UpdateStamp	
	, DST.Name as DataSourceTypeName	

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerNutrient NN WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.DataSourceType DST with (nolock)
			ON NN.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON NN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID


WHERE 
	NN.IsActive = @IsActive
END
