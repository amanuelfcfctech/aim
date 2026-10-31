CREATE TABLE [dbo].[DecimalSeparator] (
    [DecimalSeparatorID] INT           NOT NULL,
    [Name]               NVARCHAR (5)  NULL,
    [Description]        NVARCHAR (25) NULL,
    [IsActive]           BIT           NULL,
    [UpdateStamp]        DATETIME      CONSTRAINT [DF_DecimalSeparator_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]          INT           CONSTRAINT [DF_DecimalSeparator_CFCUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_DecimalSeparator] PRIMARY KEY CLUSTERED ([DecimalSeparatorID] ASC)
);

