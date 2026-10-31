CREATE TABLE [dbo].[UpdateIngredientHeader] (
    [UpdateIngredientHeaderID] INT           IDENTITY (1, 1) NOT NULL,
    [UpdateName]               NVARCHAR (30) NULL,
    [CreateDate]               DATETIME      CONSTRAINT [DF_UpdateIngredientHeader_CreateDate] DEFAULT (getdate()) NULL,
    [CFCUserID]                INT           CONSTRAINT [DF_UpdateIngredientHeader_CFCUserID] DEFAULT ((0)) NULL,
    [Status]                   INT           NULL,
    [AIMDatabaseID]            INT           NULL,
    [UpdateStamp]              DATETIME      NULL,
    [UpdateCFCUserID]          INT           NULL,
    [ExportTargetID]           INT           NULL,
    [CFCPlantID]               INT           NULL,
    [ShortName]                NVARCHAR (20) NULL,
    [ExportExpandID]           INT           NULL,
    [ExportExpandName]         NVARCHAR (40) NULL,
    [ExportPlantGroupID]       INT           NULL,
    [ExportPlantGroupName]     NVARCHAR (40) NULL,
    CONSTRAINT [PK_UpdateIngredientHeader] PRIMARY KEY CLUSTERED ([UpdateIngredientHeaderID] ASC)
);





