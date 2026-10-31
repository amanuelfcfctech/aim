CREATE TABLE [dbo].[AlternateName] (
    [AlternateNameID]      INT          IDENTITY (1, 1) NOT NULL,
    [FormID]               INT          NULL,
    [ControlID]            INT          NULL,
    [AlternateName]        VARCHAR (50) NULL,
    [UpdateStamp]          DATETIME     CONSTRAINT [DF_AlternateName_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT          NULL,
    [Visible]              BIT          NULL,
    [LabelFieldFirst]      BIT          CONSTRAINT [DF_AlternateName_LabelField1] DEFAULT ((0)) NULL,
    [LabelFieldLast]       BIT          CONSTRAINT [DF_AlternateName_LabelField2] DEFAULT ((0)) NULL,
    [ShowCodeNameOrBothID] INT          NULL,
    CONSTRAINT [PK_AlternateName] PRIMARY KEY CLUSTERED ([AlternateNameID] ASC)
);









