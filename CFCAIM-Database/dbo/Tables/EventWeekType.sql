CREATE TABLE [dbo].[EventWeekType] (
    [EventWeekTypeID]   INT        IDENTITY (1, 1) NOT NULL,
    [EventWeekTypeName] NCHAR (10) NULL,
    [UpdateStamp]       DATETIME   CONSTRAINT [DF_EventWeekType_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventWeekType] PRIMARY KEY CLUSTERED ([EventWeekTypeID] ASC)
);

