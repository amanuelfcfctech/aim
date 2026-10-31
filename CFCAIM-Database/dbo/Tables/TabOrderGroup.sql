CREATE TABLE [dbo].[TabOrderGroup] (
    [TabOrderGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [TabOrderGroupCode] NVARCHAR (50) NULL,
    [UpdateStamp]       DATETIME      CONSTRAINT [DF_TabOrdreGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]         INT           NULL,
    CONSTRAINT [PK_TabOrdreGroupID] PRIMARY KEY CLUSTERED ([TabOrderGroupID] ASC)
);

