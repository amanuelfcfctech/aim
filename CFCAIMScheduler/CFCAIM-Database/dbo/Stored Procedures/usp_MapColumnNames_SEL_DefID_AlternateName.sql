CREATE PROCEDURE [dbo].[usp_MapColumnNames_SEL_DefID_AlternateName]
		    @UserDefinedDataSourceDefinitionID			INT
		  , @AlternateName								VARCHAR(50)

--exec [usp_MapColumnNames_SEL_DefID_AlternateName] 8, "Sample Code"


AS
BEGIN
SET NOCOUNT ON;

Select TOP 1
	MCN.*
From 
	dbo.MapColumnNames MCN
Inner Join
	dbo.UserDefinedDataSourceTemplate UDST
		on
			MCN.MapColumnID = UDST.MapColumnID
Inner Join
	dbo.AlternateName AN
		on 
			MCN.AlternateNameID = AN.AlternateNameID

Where 
	UDST.UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID
	and AN.AlternateName = @AlternateName


END