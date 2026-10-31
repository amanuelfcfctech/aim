CREATE TABLE [dbo].[TabOrder] (
    [TabOrderID]  INT      IDENTITY (1, 1) NOT NULL,
    [UserID]      INT      NULL,
    [ControlID]   INT      NULL,
    [TabOrder]    INT      NULL,
    [UpdateStamp] DATETIME CONSTRAINT [DF_TabOrder_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]   INT      NULL,
    CONSTRAINT [PK_TabOrder] PRIMARY KEY CLUSTERED ([TabOrderID] ASC)
);

