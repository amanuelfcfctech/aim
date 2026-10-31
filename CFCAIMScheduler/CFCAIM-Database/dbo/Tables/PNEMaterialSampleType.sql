CREATE TABLE [dbo].[PNEMaterialSampleType] (
    [PNEMaterialSampleTypeID]   INT          IDENTITY (1, 1) NOT NULL,
    [PNEMaterialSampleTypeCode] VARCHAR (20) NULL,
    [IsActive]                  BIT          CONSTRAINT [DF_PNEMaterialSampleType_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]               DATETIME     CONSTRAINT [DF_PNEMaterialSampleType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                 INT          CONSTRAINT [DF_PNEMaterialSampleType_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_PNEMaterialSampleType] PRIMARY KEY CLUSTERED ([PNEMaterialSampleTypeID] ASC)
);

