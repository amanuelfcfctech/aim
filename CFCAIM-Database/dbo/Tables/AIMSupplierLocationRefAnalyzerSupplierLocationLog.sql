CREATE TABLE [dbo].[AIMSupplierLocationRefAnalyzerSupplierLocationLog] (
    [AIMSupplierLocationRefAnalyzerSupplierLocationLogID] INT           IDENTITY (1, 1) NOT NULL,
    [AIMSupplierLocationID]                               INT           NULL,
    [AnalyzerSupplierLocationID]                          INT           NULL,
    [AIMSupplierID]                                       INT           NULL,
    [AnalyzerSupplierID]                                  INT           NULL,
    [LogStatus]                                           CHAR (1)      NULL,
    [UpdateStamp]                                         DATETIME      CONSTRAINT [DF_AIMSupplierLocationRefAnalyzerSupplierLocationLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                                           INT           NULL,
    [OLDaimSupplierLocationID]                            INT           NULL,
    [OLDaimSupplierID]                                    INT           NULL,
    [OLDanalyzerSupplierLocationID]                       INT           NULL,
    [OLDanalyzerSupplierID]                               INT           NULL,
    [Comment]                                             VARCHAR (250) NULL,
    CONSTRAINT [PK_AIMSupplierLocationRefAnalyzerSupplierLocationLog] PRIMARY KEY CLUSTERED ([AIMSupplierLocationRefAnalyzerSupplierLocationLogID] ASC)
);



