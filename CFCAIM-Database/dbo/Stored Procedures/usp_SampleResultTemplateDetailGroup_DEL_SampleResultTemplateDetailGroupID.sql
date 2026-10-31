
CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetailGroup_DEL_SampleResultTemplateDetailGroupID]
			@SampleResultTemplateDetailGroupID		INT
AS

DELETE FROM
	dbo.SampleResultTemplateDetailGroup 
WHERE
	SampleResultTemplateDetailGroupID = @SampleResultTemplateDetailGroupID