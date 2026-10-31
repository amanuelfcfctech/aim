CREATE PROCEDURE dbo.usp_SampleResultTemplateDetailGroup_DEL
			@SampleResultTemplateID		INT
AS

DELETE FROM
	dbo.SampleResultTemplateDetailGroup 
WHERE
	SampleResultTemplateID = @SampleResultTemplateID