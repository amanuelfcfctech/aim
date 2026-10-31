CREATE TABLE [dbo].[TestMaterial] (
    [TestMaterialID]               INT             IDENTITY (1, 1) NOT NULL,
    [TestID]                       INT             NULL,
    [AIMMaterialID]                INT             NULL,
    [TestMaterialCurrentUpcharge]  NUMERIC (18, 8) NULL,
    [TestMaterialPreviousUpcharge] NUMERIC (18, 8) NULL,
    [UpdateStamp]                  DATETIME        NULL,
    [CFCUserID]                    INT             NULL
);

