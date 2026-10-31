CREATE TABLE [dbo].[LabelFormat] (
    [LabelFormatID]  INT      IDENTITY (1, 1) NOT NULL,
    [LabelID]        INT      NULL,
    [MaterialTypeID] INT      NULL,
    [OrderNumber]    INT      NULL,
    [IsBold]         BIT      NULL,
    [UpdateStamp]    DATETIME CONSTRAINT [DF_LabelFormat_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]      INT      NULL,
    [FontSize]       INT      NULL,
    CONSTRAINT [PK_LabelFormat] PRIMARY KEY CLUSTERED ([LabelFormatID] ASC)
);

