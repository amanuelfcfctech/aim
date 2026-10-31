CREATE TABLE [dbo].[NutrientGroup] (
    [NutrientGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [NutrientGroupName] NVARCHAR (50) NULL,
    [IsActive]          BIT           NULL,
    [UpdateStamp]       DATETIME      NULL,
    [CFCUserID]         INT           NULL
);

