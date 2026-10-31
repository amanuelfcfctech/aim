CREATE PROCEDURE [dbo].[usp_MapColumnNames_SEL_MapColumnID]
		    @MapColumnID			INT
		 
-- exec usp_MapColumnNames_SEL_MapColumnID 11
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
	MCN.MapColumnID = @MapColumnID


END