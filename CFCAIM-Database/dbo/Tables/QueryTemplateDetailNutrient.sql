CREATE TABLE [dbo].[QueryTemplateDetailNutrient] (
    [QueryTemplateDetailNutrientID] INT      IDENTITY (1, 1) NOT NULL,
    [QueryTemplateDetailID]         INT      NULL,
    [AIMNutrientID]                 INT      NULL,
    [UpdateStamp]                   DATETIME CONSTRAINT [DF_QueryTemplateDetailNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                     INT      CONSTRAINT [DF_QueryTemplateDetailNutrient_CFCUserID] DEFAULT ((0)) NULL,
    [DataSourceTypeID]              INT      NULL,
    [DriverAIMNutrientID]           INT      NULL,
    CONSTRAINT [PK_QueryTemplateDetailNutrient] PRIMARY KEY CLUSTERED ([QueryTemplateDetailNutrientID] ASC)
);





