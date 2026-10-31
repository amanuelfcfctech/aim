CREATE TABLE [dbo].[ColumnDescriptionOverride] (
    [ColumnDescriptionOverrideID] INT           IDENTITY (1, 1) NOT NULL,
    [TableName]                   NVARCHAR (50) NULL,
    [ColumnName]                  NVARCHAR (50) NULL,
    [OverrideName]                NVARCHAR (20) NULL,
    [UpdateStamp]                 DATETIME      CONSTRAINT [DF_ColumnDescriptionOverride_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                   INT           NULL,
    CONSTRAINT [PK_ColumnDescriptionOverride] PRIMARY KEY CLUSTERED ([ColumnDescriptionOverrideID] ASC)
);

