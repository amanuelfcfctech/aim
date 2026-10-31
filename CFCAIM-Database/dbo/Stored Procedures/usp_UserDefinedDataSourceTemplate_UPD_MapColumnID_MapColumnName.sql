CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_UPD_MapColumnID_MapColumnName]

(		  @UserDefinedDataSourceDefinitionID	AS INT
		, @MapColumnID							AS INT
		, @MapColumnName						AS NVARCHAR(30)
		, @CFCUserID							AS INT
)

AS
BEGIN

SET NOCOUNT ON;


DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0


	Begin		
		UPDATE dbo.UserDefinedDataSourceTemplate 
		SET	MapColumnID = @MapColumnID, 
			MapColumnName = @MapColumnName,
			CFCUserID = @CFCUserID
		WHERE 
			UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID
	End

SELECT 
	  @RecordID AS RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END