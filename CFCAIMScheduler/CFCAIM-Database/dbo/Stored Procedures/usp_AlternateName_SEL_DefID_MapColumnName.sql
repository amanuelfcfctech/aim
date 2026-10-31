CREATE PROCEDURE [dbo].[usp_AlternateName_SEL_DefID_MapColumnName]
		    @UserDefinedDataSourceDefinitionID			INT
		  , @MapColumnName								VARCHAR(50)

--exec [usp_AlternateName_SEL_DefID_MapColumnName] 8, "SampleCode"


AS
BEGIN
SET NOCOUNT ON;

Select TOP 1
	AN.AlternateName 
From 
	dbo.AlternateName AN
Inner Join
	dbo.MapColumnNames MCN
		ON
			AN.AlternateNameID = MCN.AlternateNameID
Inner Join
	dbo.UserDefinedDataSourceTemplate UDST
		on
			MCN.MapColumnID = UDST.MapColumnID

where UDST.UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID
	and MCN.MapColumnName = @MapColumnName

END