CREATE TABLE [dbo].[UserOptionCrossReference] (
    [CFCUserID]    INT      NOT NULL,
    [UserOptionID] INT      NOT NULL,
    [UpdateStamp]  DATETIME CONSTRAINT [DF_UserOptionCrossReference_UpdateStamp] DEFAULT (getdate()) NULL,
    [UpdateUserID] INT      NULL,
    CONSTRAINT [PK__UserOpti__37B5616B3B2F13B1] PRIMARY KEY CLUSTERED ([CFCUserID] ASC, [UserOptionID] ASC)
);


GO


GO