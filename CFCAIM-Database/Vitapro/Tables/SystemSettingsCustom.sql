CREATE TABLE [Vitapro].[SystemSettingsCustom] (
    [SystemSettingsCustomID]    INT      IDENTITY (1, 1) NOT NULL,
    [AIMNutrientID1_ForDisplay] INT      NULL,
    [AIMNutrientID2_ForDisplay] INT      NULL,
    [AIMNutrientID3_ForDisplay] INT      NULL,
    [AIMNutrientID4_ForDisplay] INT      NULL,
    [CFCUserID]                 INT      NULL,
    [UpdateStamp]               DATETIME NULL,
    CONSTRAINT [PK_SystemSettingsCustom] PRIMARY KEY CLUSTERED ([SystemSettingsCustomID] ASC)
);



