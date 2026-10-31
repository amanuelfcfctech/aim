CREATE PROCEDURE [dbo].[usp_Sample_SEL_SampleTypeID]
	@SampleTypeID	INT

AS
BEGIN
SET NOCOUNT ON;

--exec usp_Sample_SEL_SampleTypeID 1


SELECT TOP 1
	    S.*	  	  	  
	  , U.UserName		  
	 
FROM 
	dbo.Sample S WITH(NOLOCK)	
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON S.CFCUserID = U.CFCUserID

WHERE 
	S.SampleTypeID = @SampleTypeID 


END