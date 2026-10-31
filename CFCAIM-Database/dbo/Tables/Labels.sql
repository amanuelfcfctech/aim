CREATE TABLE [dbo].[Labels] (
    [LabelsID]            INT      IDENTITY (1, 1) NOT NULL,
    [ControlID]           INT      NULL,
    [AlternateNameID]     INT      NULL,
    [LabelFieldFirst]     BIT      CONSTRAINT [DF_Labels_LabelField1] DEFAULT ((0)) NULL,
    [LabelFieldLast]      BIT      CONSTRAINT [DF_Labels_LabelField2] DEFAULT ((0)) NULL,
    [UpdateStamp]         DATETIME CONSTRAINT [DF_Labels_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]           INT      NULL,
    [LabelFieldLine2]     BIT      NULL,
    [LabelFieldLine3]     BIT      NULL,
    [LabelFieldLine4]     BIT      NULL,
    [Line1Bold]           BIT      NULL,
    [Line2Bold]           BIT      NULL,
    [Line3Bold]           BIT      NULL,
    [Line4Bold]           BIT      NULL,
    [Line5Bold]           BIT      NULL,
    [LabelProdFieldLine1] BIT      NULL,
    [LabelProdFieldLine2] BIT      NULL,
    [LabelProdFieldLine3] BIT      NULL,
    [LabelProdFieldLine4] BIT      NULL,
    [LabelProdFieldLine5] BIT      NULL,
    [ProdLine1Bold]       BIT      NULL,
    [ProdLine2Bold]       BIT      NULL,
    [ProdLine3Bold]       BIT      NULL,
    [ProdLine4Bold]       BIT      NULL,
    [ProdLine5Bold]       BIT      NULL,
    CONSTRAINT [PK_Labels] PRIMARY KEY CLUSTERED ([LabelsID] ASC)
);





