CREATE TABLE [dbo].[SystemLog] (
    [SystemLogID]      INT            IDENTITY (1, 1) NOT NULL,
    [TypeID]           INT            NULL,
    [Message]          NVARCHAR (250) NULL,
    [MethodName]       NVARCHAR (50)  NULL,
    [SourceLineNumber] INT            NULL,
    [UpdateStamp]      DATETIME       CONSTRAINT [DF_SystemLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]        INT            NULL,
    CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED ([SystemLogID] ASC)
);

