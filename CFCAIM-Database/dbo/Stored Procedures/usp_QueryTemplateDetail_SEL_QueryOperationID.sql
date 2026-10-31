CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_SEL_QueryOperationID]
		@QueryOperationID	INT

--exec usp_QueryTemplateDetail_SEL_QueryOperationID 27

AS
BEGIN
SET NOCOUNT ON;
  
-- QueryOperationID 1:  Past N Days to Date:				Value					(S.SampleDate >= CONVERT(DATE, DATEADD(dd, -14, GETDATE())))
-- QueryOperationID 2:  Month To Date:												(S.SampleDate Between CONVERT(date, DATEADD(MONTH, -1, GETDATE()) ) and CONVERT(DATE, getdate() ))
-- QueryOperationID 3:  Year To Date:												(S.SampleDate Between CONVERT(date, DATEADD(MONTH, -12, GETDATE()) ) and CONVERT(DATE, getdate() ))
-- QueryOperationID 4:  =									DateValue				S.SampleDate = '2021-06-01'
-- QueryOperationID 5:  >								    DateValue				S.SampleDate > '2021-06-01'
-- QueryOperationID 6:  <									DateValue				S.SampleDate < '2021-06-01'
-- QueryOperationID 7:  >=									DateValue				S.SampleDate >= '2021-06-01'
-- QueryOperationID 8:  <=									DateValue				S.SampleDate <= '2021-06-01'
-- QueryOperationID 9:  Not Equal							DateValue				S.SampleDate <> '2021-06-01'
-- QueryOperationID 10: Between								DateValue, SecondDate	S.SampleDate BETWEEN '2021-06-01' and '2021-06-14'


SELECT 
	QTD.QueryOperationID,
	case when QTD.QueryOperationID = 1 then CONVERT(DATE, DATEADD(dd, -QTD.Value, GETDATE())) 
	else case when QTD.QueryOperationID = 2 then CONVERT(date, DATEADD(MONTH, -1, GETDATE())) 
	else case when QTD.QueryOperationID = 3 then CONVERT(date, DATEADD(MONTH, -12, GETDATE()))
	else case when QTD.QueryOperationID = 4 then  QTD.DateValue
	else case when QTD.QueryOperationID = 5 then CONVERT(DATE, DATEADD(day, 1, QTD.DateValue)) 
	else case when QTD.QueryOperationID = 6 then Convert(DateTime, '1/1/2000')
	else case when QTD.QueryOperationID = 7 then QTD.DateValue
	else case when QTD.QueryOperationID = 8 then Convert(DateTime, '1/1/2000')	
	else case when QTD.QueryOperationID = 10 then QTD.DateValue
	else getdate() end end end end end end end end end as DateValue

	, case when QTD.QueryOperationID = 1 then GETDATE()
	else case when QTD.QueryOperationID = 2 then GETDATE()
	else case when QTD.QueryOperationID = 3 then GetDate()
	else case when QTD.QueryOperationID = 4 then QTD.DateValue
	else case when QTD.QueryOperationID = 5 then GetDate()
	else case when QTD.QueryOperationID = 6 then CONVERT(DATE, DATEADD(day, -1, QTD.DateValue))
	else case when QTD.QueryOperationID = 7 then GetDate()
	else case when QTD.QueryOperationID = 8 then QTD.DateValue
	else case when QTD.QueryOperationID = 10 then QTD.SecondDate
	
	else getdate() end end end end end end end end end as SecondDateValue

FROM
	dbo.QueryTemplateDetail QTD 
INNER JOIN
	dbo.QueryOperation QO 
		ON QTD.QueryOperationID = QO.QueryOperationID
WHERE QTD.QueryTemplateID = @QueryOperationID and QTD.QueryFieldID = 1

END