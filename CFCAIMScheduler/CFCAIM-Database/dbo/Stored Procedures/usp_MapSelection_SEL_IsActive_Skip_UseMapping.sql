CREATE PROCEDURE [dbo].[usp_MapSelection_SEL_IsActive_Skip_UseMapping]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SS.*
	, U.UserName
FROM 
	dbo.SampleImportUserDefinedMapSelection SS WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SS.CFCUserID = U.CFCUserID
WHERE 
	SS.IsActive = @IsActive and 
	SS.Description <> 'Use Mapping'
END