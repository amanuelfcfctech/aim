CREATE procedure [dbo].[usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID_AIMMaterialID_AIMNutrientID]
				@QueryTemplateID		INT
				, @AIMMaterialID			INT
				, @AIMNutrientID			INT

--exec usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID_AIMMaterialID_AIMNutrientID 21, 255, 1

AS
BEGIN
SET NOCOUNT ON;

SELECT	
	  QTDN.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.QueryTemplateDetail QTD
	INNER JOIN 
		dbo.QueryTemplateDetailNutrient QTDN 
			ON QTD.QueryTemplateDetailID = QTDN.QueryTemplateDetailID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON QTDN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	QTD.QueryTemplateID = @QueryTemplateID
	--and QTD.QueryFieldID = 9 
	and QTD.Value = @AIMMaterialID 
	and QTDN.AIMNutrientID = @AIMNutrientID

END