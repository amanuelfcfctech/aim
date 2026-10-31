CREATE PROCEDURE [dbo].[usp_GuaranteeRefundDetail_SEL]

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	DET.GuaranteeRefundDetailID,
	DET.GuaranteeRefundHeaderID,
	HDR.BatchID,
	HDR.BatchSentDate,		
	HDR.AIMSupplierID,
	HDR.AIMSupplierLocationID,
	HDR.AIMMaterialID,
	DET.AIMNutrientID,
	DET.SampleID,
	DET.SampleCode,
	DET.LoadNumber,
	DET.SampleDate,
	DET.ResultValue,
	DET.GuaranteeMin,
	DET.GuaranteeMax,	
	DET.CalculatedValue,
	DET.Status,	
	HDR.CFCUserID,	
	AM.AIMMaterialName,
	AN.AIMNutrientName,
	AMS.SourceName,
	ASL.SourceLocationName,

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--AU.UserName
	U.UserName

FROM 
	dbo.GuaranteeRefundHeader HDR 
	INNER JOIN
		dbo.GuaranteeRefundDetail DET
			ON HDR.GuaranteeRefundHeaderID = DET.GuaranteeRefundHeaderID	
	LEFT OUTER JOIN
		dbo.AIMMaterial AM
			ON HDR.AIMMaterialID = AM.AIMMaterialID
	LEFT OUTER JOIN
		dbo.AIMSource AMS
			ON HDR.AIMSupplierID = AMS.AIMSourceID
	LEFT OUTER JOIN
		dbo.AIMSourceLocation ASL
			ON HDR.AIMSupplierLocationID = ASL.AIMSourceLocationID
	LEFT OUTER JOIN
		dbo.AIMNutrient AN
			ON DET.AIMNutrientID = AN.AIMNutrientID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON HDR.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID	

END