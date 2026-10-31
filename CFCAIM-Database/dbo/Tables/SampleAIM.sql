CREATE TABLE [dbo].[SampleAIM] (
    [SampleAIMID]    INT             IDENTITY (1, 1) NOT NULL,
    [SampleID]       INT             NULL,
    [SampleResultID] INT             NULL,
    [AIMNutrientID]  INT             NULL,
    [AIMUsedValue]   NUMERIC (18, 9) NULL,
    [UpdateStamp]    DATETIME        NULL,
    [CFCUserID]      INT             NULL,
    [IsActive]       BIT             CONSTRAINT [DF_SampleAIM_IsActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SampleAIM] PRIMARY KEY CLUSTERED ([SampleAIMID] ASC)
);






GO
CREATE NONCLUSTERED INDEX [SampleAIM_SampleID]
    ON [dbo].[SampleAIM]([SampleID] ASC);


GO
CREATE NONCLUSTERED INDEX [SampleAIM_SampleIDAIMNutrientID]
    ON [dbo].[SampleAIM]([SampleID] ASC, [AIMNutrientID] ASC);

