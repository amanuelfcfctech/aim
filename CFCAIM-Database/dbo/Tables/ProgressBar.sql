CREATE TABLE [dbo].[ProgressBar] (
    [ProgressBarID]           INT             IDENTITY (1, 1) NOT NULL,
    [ProgressBarValue]        INT             NULL,
    [ProgressBarDescription]  NVARCHAR (50)   NULL,
    [UpdateStamp]             DATETIME        CONSTRAINT [DF_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT             NULL,
    [ProgressBarValueDecimal] NUMERIC (18, 8) NULL,
    [ProgressBarType]         INT             CONSTRAINT [DF_ProgressBar_ProgressBarType] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_ProgressID] PRIMARY KEY CLUSTERED ([ProgressBarID] ASC)
);





