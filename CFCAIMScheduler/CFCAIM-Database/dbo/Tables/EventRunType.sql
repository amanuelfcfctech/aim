CREATE TABLE [dbo].[EventRunType] (
    [EventRunTypeID]   INT           NOT NULL,
    [EventRunTypeName] NVARCHAR (50) NULL,
    [UpdateStamp]      DATETIME      CONSTRAINT [DF_EventRunType_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventRunType] PRIMARY KEY CLUSTERED ([EventRunTypeID] ASC)
);

