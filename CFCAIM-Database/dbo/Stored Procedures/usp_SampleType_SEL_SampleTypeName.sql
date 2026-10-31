CREATE PROCEDURE [dbo].[usp_SampleType_SEL_SampleTypeName]
				@SampleTypeName NVARCHAR(30) 



--exec usp_SampleType_SEL_SampleTypeName 'NON GMO'
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ST.*
	
FROM 
	dbo.SampleType ST WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ST.CFCUserID = U.CFCUserID
				

WHERE 	
	ST.SampleTypeName = @SampleTypeName


END