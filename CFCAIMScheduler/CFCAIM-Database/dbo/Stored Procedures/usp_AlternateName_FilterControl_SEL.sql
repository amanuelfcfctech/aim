CREATE PROCEDURE [dbo].[usp_AlternateName_FilterControl_SEL]

	@FormID	Int

AS
BEGIN
SET NOCOUNT ON;

--EXEC [usp_AlternateName_FilterControl_SEL] 1

--11/15/22
--SELECT C.ControlID, AN.AlternateName
SELECT C.ControlID, case when C.ControlID = 62 then 'Material Name' else AN.AlternateName end as AlternateName

FROM 
	dbo.AlternateName AN with (nolock)
INNER JOIN 
	dbo.[Control] C with (nolock)		
		ON AN.ControlID = C.ControlID and AN.FormID = C.FormID

WHERE AN.FormID = @FormID and C.AllowFilter = 1

	--11/15/22
	and len(AN.AlternateName) > 0

--11/15/22 ---------------------------  (added for Versova)
union
SELECT C.ControlID, 'Material Code' as AlternateName
FROM dbo.[Control] C with (nolock) WHERE  C.ControlID = 11 

union
SELECT C.ControlID, 'DataSource Name' as AlternateName
FROM dbo.[Control] C with (nolock) WHERE  C.ControlID = 37 

union
SELECT 999, 'Analyzer Material Code' as AlternateName
--------------------------------------

ORDER BY AlternateName

END