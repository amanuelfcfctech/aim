CREATE TABLE [dbo].[TestNutrientResult] (
    [TestNutrientResultID]               INT             IDENTITY (1, 1) NOT NULL,
    [TestID]                             INT             NULL,
    [AIMNutrientID]                      INT             NULL,
    [TestGroupNameID]                    INT             NULL,
    [TestNutrientResultCurrentUpcharge]  NUMERIC (18, 8) NULL,
    [TestNutrientResultPreviousUpcharge] NUMERIC (18, 8) NULL,
    [UpdateStamp]                        DATETIME        NULL,
    [CFCUserID]                          INT             NULL
);

