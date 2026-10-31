CREATE TABLE [dbo].[NutrientGroupDetail] (
    [NutrientGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [NutrientGroupID]       INT      NULL,
    [AIMNutrientID]         INT      NULL,
    [SortOrder]             INT      NULL,
    [UpdateStamp]           DATETIME NULL,
    [CFCUserID]             INT      NULL
);

