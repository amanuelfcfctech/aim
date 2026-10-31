CREATE TABLE [dbo].[SampleTestType] (
    [SampleTestTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleTestTypeName] NVARCHAR (50) NULL,
    [UpdateStamp]        DATETIME      NULL,
    [CFCUserID]          INT           NULL,
    CONSTRAINT [PK_SampleTestType] PRIMARY KEY CLUSTERED ([SampleTestTypeID] ASC)
);

