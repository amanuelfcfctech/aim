CREATE TABLE [dbo].[Weeks] (
    [WeekID]      INT        NOT NULL,
    [WeekName]    NCHAR (10) NULL,
    [UpdateStamp] DATETIME   CONSTRAINT [DF_Week_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Week] PRIMARY KEY CLUSTERED ([WeekID] ASC)
);

