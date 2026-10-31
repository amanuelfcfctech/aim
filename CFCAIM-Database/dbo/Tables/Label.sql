CREATE TABLE [dbo].[Label] (
    [LabelID]                 INT           IDENTITY (1, 1) NOT NULL,
    [ControlID]               INT           NULL,
    [IsVisible]               BIT           NULL,
    [UpdateStamp]             DATETIME      CONSTRAINT [DF_Label_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT           NULL,
    [TableColumnName]         NVARCHAR (50) NULL,
    [IsVisibleToPrinterLabel] BIT           NULL,
    CONSTRAINT [PK_Label] PRIMARY KEY CLUSTERED ([LabelID] ASC)
);

