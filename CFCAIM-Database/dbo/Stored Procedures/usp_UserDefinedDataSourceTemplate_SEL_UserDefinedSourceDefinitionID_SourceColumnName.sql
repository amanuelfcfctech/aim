CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_SEL_UserDefinedSourceDefinitionID_SourceColumnName]
	@UserDefinedDataSourceDefinitionID  INT		
	, @SourceColumnName NVARCHAR(30) 
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	    DU.UserDefinedDataSourceTemplateID
	  , DU.UserDefinedDataSourceDefinitionID
	  , DU.SourceColumnName
	  , DU.SourceColumnNumber
	  , case when DU.MapColumnID is null then 0 else DU.MapColumnID end as MapColumnID 
	  , case when Du.MapColumnName is null then '' else DU.MapColumnName end as MapColumnName 
	  , DU.IsManualEntry
	  , DU.IsDetail

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.UserDefinedDataSourceTemplate DU WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DU.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID and
	SourceColumnName = @SourceColumnName


END