CREATE PROCEDURE [dbo].[usp_SampleColumnNames_SEL_HeaderOnly]
	
	-- exec usp_SampleColumnNames_SEL_HeaderOnly
			
AS
BEGIN
SET NOCOUNT ON;


SELECT 

	--1/7/20
	--MapColumnName
	Case when M.AlternateNameID is not null then A.AlternateName else M.MapColumnName end as MapColumnName

	, MapColumnID
	, IsDetail
	, SortNumber as SortNo
	, CanAlter

FROM 
	dbo.MapColumnNames M

	--1/7/20
	LEFT OUTER JOIN dbo.AlternateName A on M.AlternateNameID = A.AlternateNameID

WHERE 
	IsDetail = 0 
	and CanAlter = 1 
		
	--1/13/23 - added to remove blank lines from field list (for Versova) 
	and (Case when M.AlternateNameID is not null then A.AlternateName else M.MapColumnName end <> '') 

ORDER BY 
	SortNo

END