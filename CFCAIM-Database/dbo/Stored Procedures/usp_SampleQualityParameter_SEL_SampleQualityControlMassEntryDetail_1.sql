CREATE PROCEDURE [dbo].[usp_SampleQualityParameter_SEL_SampleQualityControlMassEntryDetail]
		  @DataSourceTypeID					INT
		, @SampleQualityControlMassEntryID	INT
		, @IsActive							BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
	  SQP.*
	, SQP.UpdateStamp
	, DT.Name as DataSourceUserName	
	, X.*
	, UT.UnitTypeDescription

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.SampleQualityParameter SQP WITH(NOLOCK)

LEFT OUTER JOIN
	dbo.DataSourceType DT with (nolock)
		ON SQP.DataSourceTypeID = DT.DataSourceTypeID

LEFT OUTER JOIN
	(SELECT * 
	FROM dbo.SampleQualityControlMassEntryDetail MED with (nolock)
	where SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID) X
		ON X.SampleQualityParameterID = SQP.SampleQualityParameterID

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
	SQP.IsActive = 1  
	and X.SampleQualityParameterID IS NULL
	AND SQP.DataSourceTypeID = @DataSourceTypeID


ORDER BY
	SQP.SampleQualityParameterName
END