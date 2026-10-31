CREATE TABLE [dbo].[CFCProductArchive] (
    [CFCProductArchiveID] INT          IDENTITY (1, 1) NOT NULL,
    [CFCProductID]        INT          NULL,
    [ArchiveVersion]      VARCHAR (10) NULL,
    [ArchiveDate]         DATETIME     NULL,
    [ArchiveType]         VARCHAR (1)  NULL,
    [ArchiveUser]         VARCHAR (8)  NULL,
    [ProductionDate]      DATETIME     NULL,
    [ProductionVersion]   VARCHAR (8)  NULL,
    [ProductionUser]      VARCHAR (8)  NULL,
    [TrialDate]           DATETIME     NULL,
    [TrialVersion]        VARCHAR (8)  NULL,
    [TrialUser]           VARCHAR (8)  NULL,
    [UpdateStamp]         DATETIME     CONSTRAINT [DF_CFCProductArchive_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]           INT          NULL,
    [IsActive]            BIT          DEFAULT ((1)) NULL,
    CONSTRAINT [PK_CFCProductArchive] PRIMARY KEY CLUSTERED ([CFCProductArchiveID] ASC)
);



