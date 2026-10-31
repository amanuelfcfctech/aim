CREATE TABLE [Vitapro].[ProductionStatus] (
    [ProductionStatusID]   INT           IDENTITY (1, 1) NOT NULL,
    [ProductionStatusName] NVARCHAR (20) NULL,
    CONSTRAINT [PK_ProductionStatus] PRIMARY KEY CLUSTERED ([ProductionStatusID] ASC)
);



