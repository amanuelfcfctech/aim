CREATE TABLE [dbo].[DelimiterType] (
    [DelimiterTypeID] INT           NOT NULL,
    [Name]            NVARCHAR (5)  NULL,
    [Description]     NVARCHAR (25) NULL,
    [IsActive]        BIT           NULL,
    [UpdateStamp]     DATETIME      CONSTRAINT [DF_DelimiterType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]       INT           CONSTRAINT [DF_DelimiterType_CFCUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_DelimiterType] PRIMARY KEY CLUSTERED ([DelimiterTypeID] ASC)
);

