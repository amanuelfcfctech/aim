CREATE TABLE [dbo].[SampleResultTemplateDetailGroup] (
    [SampleResultTemplateDetailGroupID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleResultTemplateID]            INT      NULL,
    [NutrientGroupID]                   INT      NULL,
    [TestFrequency]                     INT      NULL,
    [StartDate]                         DATETIME NULL,
    [IsDateFrequency]                   BIT      NULL,
    [UpdateStamp]                       DATETIME NULL,
    [CFCUserID]                         INT      NULL
);

