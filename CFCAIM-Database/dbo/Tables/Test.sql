CREATE TABLE [dbo].[Test] (
    [TestID]           INT             IDENTITY (1, 1) NOT NULL,
    [TestCode]         NVARCHAR (20)   NULL,
    [TestName]         NVARCHAR (50)   NULL,
    [DataSourceTypeID] INT             NULL,
    [TestCurrentCost]  NUMERIC (18, 8) NULL,
    [TestPreviousCost] NUMERIC (18, 8) NULL,
    [UpdateStamp]      DATETIME        NULL,
    [CFCUserID]        INT             NULL
);

