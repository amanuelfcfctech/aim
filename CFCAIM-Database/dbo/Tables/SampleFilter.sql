CREATE TABLE [dbo].[SampleFilter] (
    [SampleFilterID]       INT           IDENTITY (1, 1) NOT NULL,
    [ControlID]            INT           NULL,
    [SampleTableFieldName] VARCHAR (50)  NULL,
    [SearchTable]          NVARCHAR (25) NULL,
    [SearchField]          NVARCHAR (25) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_SampleFilter_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_SampleFilter] PRIMARY KEY CLUSTERED ([SampleFilterID] ASC)
);

