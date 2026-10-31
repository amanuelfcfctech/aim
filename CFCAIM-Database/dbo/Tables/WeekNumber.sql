CREATE TABLE [dbo].[WeekNumber] (
    [WeekNumberID]   INT        NOT NULL,
    [WeekNumberName] NCHAR (10) NULL,
    [UpdateStamp]    DATETIME   CONSTRAINT [DF_WeekNumber_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_WeekNumber] PRIMARY KEY CLUSTERED ([WeekNumberID] ASC)
);

