CREATE TABLE [dbo].[AIMNutrientDriver] (
    [AIMNutrientDriverID] INT           IDENTITY (1, 1) NOT NULL,
    [QueryTemplateID]     INT           NOT NULL,
    [DataSourceTypeID]    INT           NOT NULL,
    [AIMNutrientID]       INT           NOT NULL,
    [AIMNutrientCode]     NVARCHAR (20) NULL,
    [AIMNutrientName]     VARCHAR (30)  NULL,
    CONSTRAINT [PK_AIMNutrientDriver] PRIMARY KEY CLUSTERED ([AIMNutrientDriverID] ASC)
);

