CREATE TABLE [dbo].[UserOption] (
    [UserOptionID] INT           NOT NULL,
    [Description]  NVARCHAR (50) NULL,
    [UpdateStamp]  DATE          CONSTRAINT [DF_UserOption_UpdateStamp] DEFAULT (getdate()) NULL,
    [UpdateUserID] INT           NULL,
    CONSTRAINT [PK__UserOpti__CB1B6ECE4B449270] PRIMARY KEY CLUSTERED ([UserOptionID] ASC)
);



GO


GO