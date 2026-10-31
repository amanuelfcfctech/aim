CREATE TABLE [dbo].[SampleTabControlPage] (
    [SampleTabControlPageID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleTabControlPageName] NVARCHAR (50) NULL,
    [TabControlName]           NVARCHAR (50) NULL,
    [SortOrder]                INT           NULL,
    [IsHidden]                 BIT           NULL,
    [CFCUserID]                INT           NULL,
    [UpdateStamp]              DATETIME      NULL,
    CONSTRAINT [PK_SampleTabControlPage] PRIMARY KEY CLUSTERED ([SampleTabControlPageID] ASC)
);

