CREATE TABLE [dbo].[CFCNutrient] (
    [CFCNutrientID]   INT           IDENTITY (1, 1) NOT NULL,
    [CFCNutrientCode] NVARCHAR (20) NULL,
    [CFCNutrientName] VARCHAR (30)  NULL,
    [IsActive]        BIT           CONSTRAINT [DF_CFCNutrient_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]     DATETIME      NULL,
    [Unit]            NVARCHAR (15) NULL,
    [AIMDatabaseID]   INT           NULL,
    [CFCUserID]       INT           NULL,
    [MathTypeID]      INT           NULL,
    [IsComputed]      BIT           NULL,
    CONSTRAINT [PK_CFCNutrient] PRIMARY KEY CLUSTERED ([CFCNutrientID] ASC)
);








GO
CREATE NONCLUSTERED INDEX [CFCNutrient_AIMDatabaseIDCFCNutrientCode]
    ON [dbo].[CFCNutrient]([AIMDatabaseID] ASC, [CFCNutrientCode] ASC);


GO
CREATE NONCLUSTERED INDEX [CFCNutrient_CFCNutrientID]
    ON [dbo].[CFCNutrient]([CFCNutrientID] ASC);

