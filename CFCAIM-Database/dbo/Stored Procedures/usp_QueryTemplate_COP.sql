CREATE PROCEDURE [dbo].[usp_QueryTemplate_COP]	
			  @QueryTemplateID					INT
			, @CopyFromQueryTemplateID			INT
			, @CFCUserID						INT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

INSERT INTO QueryTemplateDetail
  (		[QueryTemplateID]
       ,[QueryFieldID]
       ,[QueryOperationID]
       ,[Value]
       ,[UpdateStamp]
       ,[CFCUserID]
       ,[IsAnd]
       ,[DateValue]
	   ,[SecondDate]
	   ,[SecondValue])

Select @QueryTemplateID
       ,QueryFieldID
       ,QueryOperationID
       ,Value
       ,GetDate()
       ,@CFCUserID
       ,IsAnd
       ,[DateValue]
	   ,[SecondDate]
	   ,[SecondValue]

From QueryTemplateDetail WHERE QueryTemplateID = @CopyFromQueryTemplateID
		   
END