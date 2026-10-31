CREATE TABLE [Vitapro].[LotNumber] (
    [LotNumberID] INT      IDENTITY (1, 1) NOT NULL,
    [LotNumber]   INT      NULL,
    [UpdateStamp] DATETIME NULL,
    [CFCUserID]   INT      NULL,
    CONSTRAINT [PK_LotNumber] PRIMARY KEY CLUSTERED ([LotNumberID] ASC)
);

