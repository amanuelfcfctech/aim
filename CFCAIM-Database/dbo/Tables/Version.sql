CREATE TABLE [dbo].[Version] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Version]     NVARCHAR (15) NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF_Version_CreatedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED ([ID] ASC)
);



