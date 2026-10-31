CREATE PROCEDURE [dbo].[usp_QueryOperation_SEL_QueryFieldID]
				@QueryFieldID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT DISTINCT
	QO.QueryOperationID
	, QO.Operation
	, QO.IsActive
	, QO.UpdateStamp
	, QO.CFCUserID
	, U.UserName


FROM 
	dbo.QueryOperation QO 
	
	INNER JOIN 	
		dbo.QueryFieldRefQueryOperation QFRQO 
			ON QFRQO.QueryOperationID = QO.QueryOperationID
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON QO.CFCUserID = U.CFCUserID

WHERE 
		QO.IsActive = 1
	AND QFRQO.QueryFieldID = @QueryFieldID

ORDER BY  QO.QueryOperationID

END
