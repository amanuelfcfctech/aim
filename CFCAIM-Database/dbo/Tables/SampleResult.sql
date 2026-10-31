CREATE TABLE [dbo].[SampleResult] (
    [SampleResultID]       INT             IDENTITY (1, 1) NOT NULL,
    [SampleID]             INT             NULL,
    [AnalyzerNutrientID]   INT             NULL,
    [ResultValue]          NUMERIC (18, 8) NULL,
    [UpdateStamp]          DATETIME        NULL,
    [IsActive]             BIT             CONSTRAINT [DF_SampleResult_IsActive] DEFAULT ((1)) NULL,
    [AnalysisDate]         DATETIME        NULL,
    [AnalyzerNutrientCode] NVARCHAR (100)  NULL,
    [CFCUserID]            INT             NULL,
    [IsCalculate]          BIT             CONSTRAINT [DF_SampleResult_IsCalculate] DEFAULT ((1)) NULL,
    [Unit]                 NVARCHAR (15)   NULL,
    [InstrumentID]         INT             NULL,
    [SQLResultID]          NVARCHAR (300)  NULL,
    CONSTRAINT [PK_SampleResult] PRIMARY KEY CLUSTERED ([SampleResultID] ASC)
);


















GO
create TRIGGER [dbo].[AnalysisLog_UPD_IsCalc]
ON [dbo].[SampleResult]
AFTER UPDATE
AS
BEGIN

		--DECLARE @Activity NVARCHAR(200);
		--IF((SELECT IsCalculate FROM inserted) = 1) 
		--	SET @Activity = 'Is Calculate changed from False to True';
		--ELSE
		--	SET @Activity = 'Is Calculate changed from True to False';
	IF (UPDATE(IsCalculate))
	BEGIN
		INSERT INTO
			dbo.AnalysisLog
			(
			   Activity
			 , SampleResultID
			 , IsActive
			 , CFCUserID
			 , UpdateStamp
			)
			SELECT 
			   CASE WHEN I.IsCalculate = 1  
					THEN 'Is Calculate changed from False to True' 
					ELSE 'Is Calculate changed from True to False'
					END
			 , I.SampleResultID
			 , 1
			 , I.CFCUserID
			 , GETDATE()

			FROM
				inserted I
	END
END

GO
CREATE NONCLUSTERED INDEX [SampleResult_SampleID]
    ON [dbo].[SampleResult]([SampleID] ASC);


GO
CREATE NONCLUSTERED INDEX [SampleResult_IsActive]
    ON [dbo].[SampleResult]([IsActive] ASC);


GO
CREATE NONCLUSTERED INDEX [SampleResult_SampleIDIsActive]
    ON [dbo].[SampleResult]([SampleID] ASC, [IsActive] ASC);


GO
CREATE NONCLUSTERED INDEX [SampleResult_SampleID_AnalyzerNutrientID_IsActive]
    ON [dbo].[SampleResult]([SampleID] ASC, [AnalyzerNutrientID] ASC, [IsActive] ASC);

