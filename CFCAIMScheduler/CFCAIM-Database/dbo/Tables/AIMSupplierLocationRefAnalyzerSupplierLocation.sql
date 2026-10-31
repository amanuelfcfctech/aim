CREATE TABLE [dbo].[AIMSupplierLocationRefAnalyzerSupplierLocation] (
    [AIMSupplierLocationRefAnalyzerSupplierLocationID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMSupplierLocationID]                            INT      NULL,
    [AnalyzerSupplierLocationID]                       INT      NULL,
    [AIMSupplierID]                                    INT      NULL,
    [AnalyzerSupplierID]                               INT      NULL,
    [UpdateStamp]                                      DATETIME CONSTRAINT [DF_AIMSupplierLocationRefAnalyzerSupplierLocation_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                                        INT      NULL,
    CONSTRAINT [PK_AIMSupplierLocationRefAnalyzerSupplierLocation] PRIMARY KEY CLUSTERED ([AIMSupplierLocationRefAnalyzerSupplierLocationID] ASC)
);

