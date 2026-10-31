CREATE TABLE [dbo].[TabOrderGroupDetail] (
    [TabOrderGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [TabOrderGroupID]       INT      NULL,
    [ControlID]             INT      NULL,
    [IsVisible]             BIT      NULL,
    [TabOrder]              INT      NULL,
    [UpdateStamp]           DATETIME CONSTRAINT [DF_TabOrdreGroupDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]             INT      NULL,
    CONSTRAINT [PK_TabOrdreGroupDetailID] PRIMARY KEY CLUSTERED ([TabOrderGroupDetailID] ASC)
);

