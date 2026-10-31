CREATE TABLE [dbo].[ExportPlant] (
    [ExportPlantID]      INT      IDENTITY (1, 1) NOT NULL,
    [ExportPlantGroupID] INT      NULL,
    [CFCPlantID]         INT      NULL,
    [IsActive]           BIT      CONSTRAINT [DF_ExportPlant_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]        DATETIME CONSTRAINT [DF_ExportPlant_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]          INT      NULL,
    CONSTRAINT [PK_ExportPlant] PRIMARY KEY CLUSTERED ([ExportPlantID] ASC)
);

