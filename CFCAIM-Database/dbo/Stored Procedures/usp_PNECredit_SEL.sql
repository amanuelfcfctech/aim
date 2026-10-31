CREATE PROCEDURE [dbo].[usp_PNECredit_SEL]
AS
BEGIN

SELECT 
	  PN.PNECreditID
	, PN.Credit
	, PN.PNEKey
	, PN.PNEDescription
	, PN.UpdateStamp
	, PN.CFCUserID
	, C.UserName
FROM 
	dbo.PNECredit PN
	LEFT OUTER JOIN
		CFCUser.CFCUser C 
			ON C.CFCUserID = PN.CFCUserID

END