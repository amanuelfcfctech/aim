CREATE PROCEDURE [dbo].[usp_GuaranteeRefund_SEL_BatchID_SampleCode]

	  @BatchID		INT 
	, @SampleCode	NVARCHAR(20)


--exec usp_GuaranteeRefund_SEL_BatchID_SampleCode 1, ''


-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''
DECLARE @Where				VARCHAR(1000) = ''

Set @Where = ' WHERE '

IF @BatchID > 0 and Len(@SampleCode) > 0	
	SET @Where = @Where + ' BatchID = ' + Cast(@BatchID as varchar) + ' and SampleCode = ''' + @SampleCode + ''' '	
ELSE IF @BatchID > 0 
	SET @Where = @Where + ' BatchID = ' + Cast(@BatchID as varchar) 
ELSE
	SET @Where = @Where + ' SampleCode = ''' + @SampleCode + ''' '

SET @SQL = ' 
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
	U.UserName,
	AM.AIMMaterialName,
	AN.AIMNutrientName,
	AMS.SourceName,
	ASL.SourceLocationName

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
	

'
+ @WHERE 

EXEC (@SQL)
--print @sql


END