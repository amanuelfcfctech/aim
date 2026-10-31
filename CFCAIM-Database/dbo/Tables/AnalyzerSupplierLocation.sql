CREATE TABLE [dbo].[AnalyzerSupplierLocation] (
    [AnalyzerSupplierLocationID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnalyzerSupplierID]           INT           NULL,
    [AnalyzerSupplierLocationCode] NVARCHAR (20) NULL,
    [AnalyzerSupplierLocationName] NVARCHAR (50) NULL,
    [DataSourceTypeID]             INT           NULL,
    [IsActive]                     BIT           CONSTRAINT [DF_AnalyzerSupplierLocation_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                  DATETIME      CONSTRAINT [DF_AnalyzerSupplierLocation_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                    INT           NULL,
    CONSTRAINT [PK_AnalyzerSupplierLocation] PRIMARY KEY CLUSTERED ([AnalyzerSupplierLocationID] ASC)
);



