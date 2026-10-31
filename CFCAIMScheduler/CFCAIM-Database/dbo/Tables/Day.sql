CREATE TABLE [dbo].[Day] (
    [DayID]       INT       IDENTITY (1, 1) NOT NULL,
    [Day]         NCHAR (4) NULL,
    [UpdateStamp] DATETIME  CONSTRAINT [DF_Day_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Day] PRIMARY KEY CLUSTERED ([DayID] ASC)
);

