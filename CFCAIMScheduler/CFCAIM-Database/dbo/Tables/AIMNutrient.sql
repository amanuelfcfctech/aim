CREATE TABLE [dbo].[AIMNutrient] (
    [AIMNutrientID]        INT           IDENTITY (1, 1) NOT NULL,
    [AIMNutrientCode]      NVARCHAR (20) NULL,
    [AIMNutrientName]      VARCHAR (30)  NULL,
    [IsActive]             BIT           CONSTRAINT [DF_AIMNutrient_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_AIMNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [UnitTypeID]           INT           NULL,
    [CFCUserID]            INT           NULL,
    [MathTypeID]           INT           NULL,
    [PriorityAIMUsedValue] INT           NULL,
    [DataSourceTypeID]     INT           CONSTRAINT [DF_AIMNutrient_DataSourceTypeID] DEFAULT ((0)) NULL,
    [AIMNutrientShortName] VARCHAR (30)  NULL,
    CONSTRAINT [PK_AIMNutrient] PRIMARY KEY CLUSTERED ([AIMNutrientID] ASC)
);










GO
CREATE NONCLUSTERED INDEX [AIMNutrient_AIMNutrientID]
    ON [dbo].[AIMNutrient]([AIMNutrientID] ASC);

