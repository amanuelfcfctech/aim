CREATE TABLE [dbo].[ExportPlantGroup] (
    [ExportPlantGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [ExportPlantGroupCode] NVARCHAR (50) NULL,
    [IsActive]             BIT           CONSTRAINT [DF_ExportPlantGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_ExportPlantGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_ExportPlantGroup] PRIMARY KEY CLUSTERED ([ExportPlantGroupID] ASC)
);



