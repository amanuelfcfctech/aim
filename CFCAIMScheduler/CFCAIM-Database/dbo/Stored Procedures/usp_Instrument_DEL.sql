create PROCEDURE [dbo].[usp_Instrument_DEL]
				@InstrumentID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.Instrument WITH(ROWLOCK)
WHERE
	InstrumentID = @InstrumentID

END
