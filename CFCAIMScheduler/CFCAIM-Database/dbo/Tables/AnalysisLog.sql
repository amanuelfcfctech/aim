CREATE TABLE [dbo].[AnalysisLog] (
    [AnalysisLogID]  INT            IDENTITY (1, 1) NOT NULL,
    [UpdateStamp]    DATETIME       CONSTRAINT [DF_AnalysisLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]      INT            NULL,
    [Activity]       NVARCHAR (200) NULL,
    [IsActive]       BIT            CONSTRAINT [DF_AnalysisLog_IsActive] DEFAULT ((1)) NULL,
    [SampleResultID] INT            NULL,
    CONSTRAINT [PK_AnalysisLog] PRIMARY KEY CLUSTERED ([AnalysisLogID] ASC)
);

