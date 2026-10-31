CREATE TABLE [dbo].[Sample] (
    [SampleID]               INT           IDENTITY (1, 1) NOT NULL,
    [SampleCode]             NVARCHAR (20) NULL,
    [CreatedStamp]           DATETIME      CONSTRAINT [DF_Sample_CreatedStamp] DEFAULT (getdate()) NULL,
    [AIMOwnerID]             INT           NULL,
    [AIMSourceID]            INT           NULL,
    [AIMSourceLocationID]    INT           NULL,
    [ContractNumber]         NVARCHAR (20) NULL,
    [PoNumber]               NVARCHAR (20) NULL,
    [InvoiceNumber]          NVARCHAR (50) NULL,
    [TicketNumber]           NVARCHAR (20) NULL,
    [LoadNumber]             NVARCHAR (20) NULL,
    [BillOfLoad]             NVARCHAR (20) NULL,
    [VehicleNumber]          NVARCHAR (20) NULL,
    [LotNumber]              NVARCHAR (20) NULL,
    [IsActive]               BIT           CONSTRAINT [DF_Sample_IsActive] DEFAULT ((1)) NULL,
    [AnalyzerMaterialID]     INT           NULL,
    [SampleDate]             DATE          NULL,
    [AnalysisDate]           DATETIME      NULL,
    [AIMOwnerLocationID]     INT           NULL,
    [CFCUserID]              INT           NULL,
    [UpdateStamp]            DATETIME      CONSTRAINT [DF_Sample_UpdateStamp] DEFAULT (getdate()) NULL,
    [FurtherAnalysisID]      INT           CONSTRAINT [DF_Sample_FurtherAnalysisID] DEFAULT ((0)) NULL,
    [AIMMaterialID]          INT           NULL,
    [AlternateCode]          NVARCHAR (30) NULL,
    [DataSourceTypeID]       INT           NULL,
    [CFCProductArchiveID]    INT           NULL,
    [Notes]                  VARCHAR (250) NULL,
    [ExpirationDate]         DATETIME      NULL,
    [SupportingDocumentPath] VARCHAR (200) NULL,
    [SampleTypeID]           INT           NULL,
    [IsGround]               BIT           NULL,
    [SampleTestTypeID]       INT           NULL,
    [isPNE]                  BIT           NULL,
    [GroundUnGroundID]       INT           NULL,
    CONSTRAINT [PK_Sample] PRIMARY KEY CLUSTERED ([SampleID] ASC)
);
































GO
CREATE NONCLUSTERED INDEX [Sample_SampleDate]
    ON [dbo].[Sample]([SampleDate] ASC)
    INCLUDE([IsActive]);


GO
CREATE NONCLUSTERED INDEX [Sample_SampleID]
    ON [dbo].[Sample]([SampleID] ASC);


GO
CREATE NONCLUSTERED INDEX [Sample_SampleCode]
    ON [dbo].[Sample]([SampleCode] ASC);


GO
CREATE NONCLUSTERED INDEX [Sample_SampleDateIsActive]
    ON [dbo].[Sample]([SampleDate] ASC, [IsActive] ASC);

