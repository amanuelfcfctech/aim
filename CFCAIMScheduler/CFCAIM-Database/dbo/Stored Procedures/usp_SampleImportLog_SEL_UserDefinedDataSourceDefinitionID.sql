CREATE PROCEDURE [dbo].[usp_SampleImportLog_SEL_UserDefinedDataSourceDefinitionID]
			 @UserDefinedDataSourceDefinitionID INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  L.*
	, S.SampleCode
	, S.SampleDate

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.SampleImportLog L WITH(NOLOCK)

	INNER JOIN
		dbo.Sample S with (nolock) on L.SampleID = S.SampleID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON L.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID

END