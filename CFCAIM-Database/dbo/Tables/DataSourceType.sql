CREATE TABLE [dbo].[DataSourceType] (
    [DataSourceTypeID]   INT           IDENTITY (10000, 1) NOT NULL,
    [Name]               NVARCHAR (50) NULL,
    [IsActive]           BIT           NULL,
    [UpdateStamp]        DATETIME      CONSTRAINT [DF_DataSourceType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]          INT           CONSTRAINT [DF_DataSourceType_CFCUserID] DEFAULT ((1)) NULL,
    [IsFurtherAnalyses]  BIT           CONSTRAINT [DF_DataSourceType_IsFurtherAnalyses] DEFAULT ((0)) NULL,
    [IsUserDefined]      BIT           NULL,
    [IsSQLDatabase]      BIT           NULL,
    [DataSourceTypeCode] NVARCHAR (20) NULL,
    CONSTRAINT [PK_DataSourceType] PRIMARY KEY CLUSTERED ([DataSourceTypeID] ASC)
);













