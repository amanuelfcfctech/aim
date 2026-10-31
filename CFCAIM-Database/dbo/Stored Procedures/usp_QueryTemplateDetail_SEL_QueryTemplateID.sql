CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_SEL_QueryTemplateID]
				@QueryTemplateID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT	
	  QTD.QueryTemplateDetailID
    , QTD.QueryTemplateID
    , QTD.QueryFieldID
	, QTD.IsAnd
    , QTD.QueryOperationID
    , QTD.Value
	, QTD.SecondValue
    , QTD.DateValue
	, QTD.SecondDate
    , QTD.UpdateStamp
    , QTD.CFCUserID

	--6/1/22
	, QTD.OrderBy

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.QueryTemplateDetail QTD WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON QTD.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	QTD.QueryTemplateID = @QueryTemplateID

END
