CREATE TABLE [dbo].[AIMOwnerLocationRefAnalyzerOwnerLocation] (
    [AIMOwnerLocationRefAnalyzerOwnerLocationID] INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerOwnerID]                            INT      NULL,
    [AnalyzerOwnerLocationID]                    INT      NULL,
    [AIMOwnerID]                                 INT      NULL,
    [AIMOwnerLocationID]                         INT      NULL,
    [UpdateStamp]                                DATETIME CONSTRAINT [DF_AIMOwnerLocationRefAnalyzerOwnerLocation_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]                                   BIT      CONSTRAINT [DF_AIMOwnerLocationRefAnalyzerOwnerLocation_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                                  INT      NULL,
    CONSTRAINT [PK_AIMOwnerLocationRefAnalyzerOwnerLocation] PRIMARY KEY CLUSTERED ([AIMOwnerLocationRefAnalyzerOwnerLocationID] ASC)
);

