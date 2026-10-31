CREATE TABLE [dbo].[Months] (
    [MonthID]     INT        NOT NULL,
    [MonthName]   NCHAR (10) NULL,
    [SortOrder]   INT        NULL,
    [UpdateStamp] DATETIME   CONSTRAINT [DF_Months_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Months] PRIMARY KEY CLUSTERED ([MonthID] ASC)
);

