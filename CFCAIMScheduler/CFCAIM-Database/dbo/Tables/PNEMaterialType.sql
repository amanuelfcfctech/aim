CREATE TABLE [dbo].[PNEMaterialType] (
    [PNEMaterialTypeID]       INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerMaterialID]      INT      NULL,
    [PNEMaterialSampleTypeID] INT      NULL,
    [IsActive]                BIT      CONSTRAINT [DF_PNEMaterialType_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]             DATETIME CONSTRAINT [DF_PNEMaterialType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT      NULL,
    CONSTRAINT [PK_PNEMaterialType] PRIMARY KEY CLUSTERED ([PNEMaterialTypeID] ASC)
);

