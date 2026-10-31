CREATE TABLE [dbo].[SampleLog] (
    [SampleLogID]  INT            NULL,
    [SampleID]     INT            NULL,
    [LogMessage]   NVARCHAR (500) NULL,
    [CreatedStamp] DATETIME       NULL,
    [CFCUserID]    INT            NULL,
    [IsActive]     BIT            NULL
);




GO
CREATE NONCLUSTERED INDEX [SampleLog_SampleID]
    ON [dbo].[SampleLog]([SampleID] ASC);

