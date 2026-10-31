CREATE TABLE [dbo].[AIMSupplierRefAnalyzerSupplierLog] (
    [AIMSupplierRefAnalyzerSupplierLogID] INT           IDENTITY (1, 1) NOT NULL,
    [AIMSupplierID]                       INT           NULL,
    [AnalyzerSupplierID]                  INT           NULL,
    [LogStatus]                           CHAR (1)      NULL,
    [UpdateStamp]                         DATETIME      CONSTRAINT [DF_AIMSupplierRefAnalyzerSupplierLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                           INT           NULL,
    [OLDaimSupplierID]                    INT           NULL,
    [Comment]                             VARCHAR (250) NULL,
    CONSTRAINT [PK_AIMSupplierRefAnalyzerSupplierLog] PRIMARY KEY CLUSTERED ([AIMSupplierRefAnalyzerSupplierLogID] ASC)
);



