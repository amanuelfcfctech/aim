CREATE TABLE [dbo].[MathType] (
    [MathTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [MathTypeName] NVARCHAR (50) NULL,
    [IsActive]     BIT           CONSTRAINT [DF_MathType_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]  DATETIME      CONSTRAINT [DF_MathType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]    INT           CONSTRAINT [DF_MathType_CFCUserID] DEFAULT ((0)) NULL,
    [IsSystem]     BIT           NULL,
    [Equation]     VARCHAR (200) NULL,
    CONSTRAINT [PK_MathTypeID] PRIMARY KEY CLUSTERED ([MathTypeID] ASC)
);





