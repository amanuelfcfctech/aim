CREATE TABLE [dbo].[AIMSourceLocationGuarantee] (
    [AIMSourceLocationGuaranteeID] INT             IDENTITY (1, 1) NOT NULL,
    [AIMSourceID]                  INT             NULL,
    [AIMSourceLocationID]          INT             NULL,
    [AIMNutrientID]                INT             NULL,
    [AIMMaterialID]                INT             NULL,
    [Min]                          NUMERIC (18, 8) CONSTRAINT [DF_AIMSourceLocationGuarantee_Min] DEFAULT ((0)) NULL,
    [Max]                          NUMERIC (18, 8) CONSTRAINT [DF_AIMSourceLocationGuarantee_Max] DEFAULT ((0)) NULL,
    [IsActive]                     BIT             CONSTRAINT [DF_AIMSourceLocationGuarantee_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                  DATETIME        CONSTRAINT [DF_AIMSourceLocationGuarantee_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                    INT             NULL,
    [Target]                       NUMERIC (18, 8) NULL,
    [Moisture]                     NUMERIC (18, 8) NULL,
    CONSTRAINT [PK_AIMSourceLocationGuarantee] PRIMARY KEY CLUSTERED ([AIMSourceLocationGuaranteeID] ASC)
);







