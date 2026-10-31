CREATE TABLE [Vitapro].[LotStatus] (
    [LotStatusID]   INT           IDENTITY (1, 1) NOT NULL,
    [LotStatusName] NVARCHAR (20) NULL,
    [UpdateStamp]   DATETIME      NULL,
    [CFCUserID]     INT           NULL,
    CONSTRAINT [PK_LotStatus] PRIMARY KEY CLUSTERED ([LotStatusID] ASC)
);

