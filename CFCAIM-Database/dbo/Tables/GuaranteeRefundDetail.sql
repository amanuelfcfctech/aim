CREATE TABLE [dbo].[GuaranteeRefundDetail] (
    [GuaranteeRefundDetailID] INT             IDENTITY (1, 1) NOT NULL,
    [GuaranteeRefundHeaderID] INT             NULL,
    [SampleID]                INT             NULL,
    [AIMNutrientID]           INT             NULL,
    [ResultValue]             NUMERIC (18, 8) NULL,
    [GuaranteeMin]            NUMERIC (18, 8) NULL,
    [GuaranteeMax]            NUMERIC (18, 8) NULL,
    [GuaranteeTarget]         NUMERIC (18, 8) NULL,
    [SampleCode]              NVARCHAR (20)   NULL,
    [SampleDate]              DATE            NULL,
    [LoadNumber]              NVARCHAR (20)   NULL,
    [CalculatedValue]         NUMERIC (18, 8) NULL,
    [Status]                  INT             NULL,
    [CFCUserID]               INT             NULL,
    [UpdateStamp]             DATETIME        NULL,
    CONSTRAINT [PK_GuaranteeRefundDetail] PRIMARY KEY CLUSTERED ([GuaranteeRefundDetailID] ASC)
);

