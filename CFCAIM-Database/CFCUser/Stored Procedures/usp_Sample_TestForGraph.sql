CREATE PROCEDURE [CFCUser].[usp_Sample_TestForGraph]
AS
BEGIN
SET NOCOUNT ON;
select  
S.SampleDate
,  round(avg(sa.AIMUsedValue), 3) as AIMUsedValue
,  n.AIMNutrientName
from  dbo.Sample  s
inner  join
dbo.SampleAIM SA
ON  S.SampleID =  SA.SampleID
inner join 
dbo.AIMNutrient n 
on n.AIMNutrientID = sa.AIMNutrientID 
WHERE  sa.AIMNutrientID <  5
and  s.SampleDate > '2014-12-05'
and s.SampleDate < '2014-12-30'
and s.AIMMaterialID = 4
--and sa.AIMNutrientID = 1
group  by  
S.SampleDate
,  n.AIMNutrientName

END
