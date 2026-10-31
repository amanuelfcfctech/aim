CREATE TABLE [dbo].[AnalyzerSupplier] (
    [AnalyzerSupplierID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnalyzerSupplierCode] NVARCHAR (20) NULL,
    [AnalyzerSupplierName] NVARCHAR (50) NULL,
    [DataSourceTypeID]     INT           NULL,
    [IsActive]             BIT           CONSTRAINT [DF_AnalyzerSupplier_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_AnalyzerSupplier_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_AnalyzerSupplier] PRIMARY KEY CLUSTERED ([AnalyzerSupplierID] ASC)
);



