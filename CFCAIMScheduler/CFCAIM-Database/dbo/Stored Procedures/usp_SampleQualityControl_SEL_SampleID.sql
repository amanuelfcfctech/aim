CREATE PROCEDURE [dbo].[usp_SampleQualityControl_SEL_SampleID]
	@SampleID		INT

-- exec usp_SampleQualityControl_SEL_SampleID 4

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	SQC.* 
	, AM.AIMNutrientCode	
	, AM.AIMNutrientName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName	
	, U.UserName	

FROM dbo.SampleQualityControl SQC with (nolock)
LEFT JOIN dbo.AIMNutrient AM with (nolock)
		ON SQC.AIMNutrientID = AM.AIMNutrientID
LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SQC.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE SQC.SampleID = @SampleID


END