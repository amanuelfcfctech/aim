CREATE TABLE [dbo].[Days] (
    [DayID]       INT       IDENTITY (1, 1) NOT NULL,
    [DayNumber]   NCHAR (4) NULL,
    [UpdateStamp] DATETIME  CONSTRAINT [DF_Days_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Days] PRIMARY KEY CLUSTERED ([DayID] ASC)
);

