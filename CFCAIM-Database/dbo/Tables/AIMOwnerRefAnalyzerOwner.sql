CREATE TABLE [dbo].[AIMOwnerRefAnalyzerOwner] (
    [AIMOwnerRefAnalyzerOwnerID] INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerOwnerID]            INT      NULL,
    [AIMOwnerID]                 INT      NULL,
    [UpdateStamp]                DATETIME CONSTRAINT [DF_AIMOwnerRefAnalyzerOwner_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]                   BIT      CONSTRAINT [DF_AIMOwnerRefAnalyzerOwner_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                  INT      NULL,
    CONSTRAINT [PK_AIMOwnerRefAnalyzerOwner] PRIMARY KEY CLUSTERED ([AIMOwnerRefAnalyzerOwnerID] ASC)
);

