CREATE TABLE [dbo].[Form] (
    [FormID]      INT          IDENTITY (1, 1) NOT NULL,
    [FormName]    VARCHAR (50) NULL,
    [CFCUserID]   INT          NULL,
    [UpdateStamp] DATETIME     CONSTRAINT [DF_Form_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED ([FormID] ASC)
);

