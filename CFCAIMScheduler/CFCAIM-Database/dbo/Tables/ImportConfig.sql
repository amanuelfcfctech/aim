CREATE TABLE [dbo].[ImportConfig] (
    [ImportConfigID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR (50)  NULL,
    [ProcessDirectory]        NVARCHAR (250) NULL,
    [BackUpDirectory]         NVARCHAR (250) NULL,
    [UpdateStamp]             DATETIME       NULL,
    [CFCUserID]               INT            NULL,
    [FileTypeID]              INT            NULL,
    [DataSourceTypeID]        INT            CONSTRAINT [DF_ImportConfig_DataSourceTypeID] DEFAULT ((0)) NULL,
    [IsCreateSample]          BIT            CONSTRAINT [DF_ImportConfig_IsCreateSample] DEFAULT ((0)) NULL,
    [AIMDatabaseID]           INT            NULL,
    [DelimiterTypeID]         INT            NULL,
    [ExcelSheetNumber]        INT            CONSTRAINT [DF_ImportConfig_ExcelSheetNumber] DEFAULT ((1)) NULL,
    [DatabaseName]            NVARCHAR (25)  NULL,
    [SelectRequiredAimFields] BIT            NULL,
    [ConnectName]             NVARCHAR (50)  NULL,
    [ServerName]              NVARCHAR (50)  NULL,
    [SqlUserName]             NVARCHAR (25)  NULL,
    [SqlPassword]             NVARCHAR (25)  NULL,
    [IsSqlAuthentication]     BIT            NULL,
    [WebServiceID]            INT            NULL,
    CONSTRAINT [PK_ImportConfigID] PRIMARY KEY CLUSTERED ([ImportConfigID] ASC)
);





















