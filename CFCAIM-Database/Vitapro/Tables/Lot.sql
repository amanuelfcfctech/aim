CREATE TABLE [Vitapro].[Lot] (
    [LotID]                 INT          IDENTITY (1, 1) NOT NULL,
    [SampleID]              INT          NULL,
    [LotStatusID]           INT          NULL,
    [LotStatusUpdate]       DATETIME     NULL,
    [ProductionStatusLine1] INT          NULL,
    [ProductionStatusLine2] INT          NULL,
    [ProductionStatusLine3] INT          NULL,
    [ProductionStatusLine4] INT          NULL,
    [UpdateStamp]           DATETIME     NULL,
    [CFCUserID]             INT          NULL,
    [ExportStatus]          BIT          CONSTRAINT [DF_Lot_ExportStatus] DEFAULT ((0)) NULL,
    [AmountAvailable]       INT          NULL,
    [DepletedDate]          DATETIME     NULL,
    [AmtAvailable]          VARCHAR (20) NULL,
    [Concept5ExportStatus]  BIT          NULL,
    CONSTRAINT [PK_Lot] PRIMARY KEY CLUSTERED ([LotID] ASC)
);







