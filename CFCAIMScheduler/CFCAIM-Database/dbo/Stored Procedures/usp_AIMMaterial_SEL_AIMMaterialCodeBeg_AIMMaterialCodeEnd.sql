CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_AIMMaterialCodeBeg_AIMMaterialCodeEnd]
		@AIMMaterialCodeBeg		NVARCHAR(20)
		, @AIMMaterialCodeEnd	NVARCHAR(20)

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  AM.AIMMaterialID
	, AM.MaterialTypeID	
	, AM.AIMMaterialName
	, AM.AIMMaterialCode
	, AM.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID
	, U.UserName

FROM 
	dbo.AIMMaterial AM WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID
WHERE 
	AM.AIMMaterialCode >= @AIMMaterialCodeBeg and
	AM.AIMMaterialCode <= @AIMMaterialCodeEnd

END