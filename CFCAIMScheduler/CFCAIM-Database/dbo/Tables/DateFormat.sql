CREATE TABLE [dbo].[DateFormat] (
    [DateFormatID]    INT           IDENTITY (1, 1) NOT NULL,
    [DateFormat]      NVARCHAR (10) NULL,
    [SortOrder]       INT           NULL,
    [FormatParameter] CHAR (1)      NULL,
    [Culture]         NVARCHAR (20) NULL,
    [UpdateStamp]     DATETIME      CONSTRAINT [DF_DateFormat_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]       INT           NULL,
    CONSTRAINT [PK_DateFormat] PRIMARY KEY CLUSTERED ([DateFormatID] ASC)
);

