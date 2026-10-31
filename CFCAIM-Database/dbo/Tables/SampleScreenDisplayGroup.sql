CREATE TABLE [dbo].[SampleScreenDisplayGroup] (
    [SampleScreenDisplayGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleScreenDisplayGroupCode] NVARCHAR (50) NULL,
    [UpdateStamp]                  DATETIME      CONSTRAINT [DF_SampleScreenDisplay_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                    INT           NULL,
    CONSTRAINT [PK_SampleScreenDisplayID] PRIMARY KEY CLUSTERED ([SampleScreenDisplayGroupID] ASC)
);

