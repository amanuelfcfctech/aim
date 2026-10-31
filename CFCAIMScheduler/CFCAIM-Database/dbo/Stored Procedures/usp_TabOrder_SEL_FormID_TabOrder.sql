CREATE PROCEDURE [dbo].[usp_TabOrder_SEL_FormID_TabOrder]
		    @FormID			INT
		  , @UserID			INT
		  , @TabOrder		INT

-- exec usp_TabOrder_SEL_FormID_TabOrder 1, 1, 1

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  T.TabOrderID  
	, T.TabOrder
	, T.UserID
	, T.UpdateStamp
	, F.FormID
	, F.FormName
	, C.ControlID
	, C.ControlName
	, C.UserControlName	
	, case when X.AlternateName is not null then X.AlternateName else C.UserControlName End as AlternateName
	, U.UserName
	, X.ShowCodeNameOrBothID
	

FROM 
	dbo.Form F WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.Control C WITH(NOLOCK)
			ON C.FormID = F.FormID	
	LEFT OUTER JOIN
		dbo.TabOrder T WITH(NOLOCK)
			ON T.ControlID = C.ControlID
	LEFT OUTER JOIN 
		(SELECT T.UserControlName, AN.AlternateName

			-- 9/30/22
			, AN.ShowCodeNameOrBothID

			FROM dbo.Control T WITH (NOLOCK) 
			INNER JOIN dbo.AlternateName AN with (nolock) on T.ControlID = AN.ControlID
		    WHERE T.AllowAlternateName = 1) X
		ON C.UserControlName = X.UserControlName
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON T.CFCUserID = U.CFCUserID

WHERE 
		    F.FormID = @FormID
		AND T.UserID = @UserID
		and T.TabOrder = @TabOrder
		and C.AllowTabOrder = 1
		

ORDER BY T.TabOrder

END