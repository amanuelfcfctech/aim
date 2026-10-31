create PROCEDURE [dbo].[usp_Sample_SEL_SampleCode_DataSourceTypeID]
	@SampleCode			VARCHAR(50)
	, @DataSourceTypeID	INT
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	    S.*
	 
FROM 
	dbo.Sample S WITH(NOLOCK)

WHERE 
		S.SampleCode = @SampleCode
	and S.DataSourceTypeID = @DataSourceTypeID

ORDER BY CreatedStamp desc

END