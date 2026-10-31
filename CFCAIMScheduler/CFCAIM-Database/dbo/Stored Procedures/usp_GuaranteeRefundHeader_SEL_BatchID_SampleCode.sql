CREATE PROCEDURE [dbo].[usp_GuaranteeRefundHeader_SEL_BatchID_SampleCode]

	@BatchID		INT 
	, @SampleCode	NVARCHAR(20)


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
	GRF.*
	, U.UserName

FROM 
	dbo.GuaranteeRefundHeader GRF WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON GRF.CFCUserID = U.CFCUserID		
'
+ @WHERE 

EXEC (@SQL)


END