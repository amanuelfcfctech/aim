CREATE PROCEDURE [dbo].[usp_SystemLog_SEL]
				
AS
BEGIN
SET NOCOUNT ON;

SELECT

SL.SystemLogID
, SL.TypeID
, SL.[Message]
, SL.MethodName
, SL.SourceLineNumber
, SL.UpdateStamp
, SL.CFCUserID
, U.UserName
	  	
FROM 
	dbo.SystemLog SL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SL.CFCUserID = U.CFCUserID

END