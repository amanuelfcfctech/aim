CREATE TABLE [dbo].[QualityControlProductVersion] (
    [QualityControlProductVersionID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCProductID]                   INT      NOT NULL,
    [AIMNutrientID]                  INT      NOT NULL,
    [ProductVersionBegin]            DATETIME NULL,
    [ProductVersionEnd]              DATETIME NULL,
    [CFCUserID]                      INT      NULL,
    [UpdateStamp]                    DATETIME CONSTRAINT [DF_QualityControlProductVersion_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_QualityControlProductVersion] PRIMARY KEY CLUSTERED ([QualityControlProductVersionID] ASC)
);

