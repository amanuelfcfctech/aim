CREATE TABLE [dbo].[AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedLog] (
    [AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedLogID] INT           IDENTITY (1, 1) NOT NULL,
    [AIMSupplierLocationID]                                               INT           NULL,
    [AIMSupplierID]                                                       INT           NULL,
    [AnalyzerSupplierLocationCombinedID]                                  INT           NULL,
    [LogStatus]                                                           CHAR (1)      NULL,
    [UpdateStamp]                                                         DATETIME      CONSTRAINT [DF_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                                                           INT           NULL,
    [OLDaimSupplierLocationID]                                            INT           NULL,
    [OLDaimSupplierID]                                                    INT           NULL,
    [OLDanalyzerSupplierLocationCombinedID]                               INT           NULL,
    [Comment]                                                             VARCHAR (250) NULL,
    CONSTRAINT [PK_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedLogLog] PRIMARY KEY CLUSTERED ([AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedLogID] ASC)
);

