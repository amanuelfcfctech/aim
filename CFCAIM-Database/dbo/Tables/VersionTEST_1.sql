CREATE TABLE [dbo].[VersionTEST] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Version]     NVARCHAR (15) NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF_VersionTEST_CreatedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_VersionTEST] PRIMARY KEY CLUSTERED ([ID] ASC)
);

