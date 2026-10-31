CREATE TABLE [dbo].[SampleRequiredFieldGroup] (
    [SampleRequiredFieldGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleRequiredFieldGroupCode] NVARCHAR (50) NOT NULL,
    [SpecificIngredientsGroupID]   INT           NULL,
    [SpecificProductsGroupID]      INT           NULL,
    [AIMOwnerLocationGroupID]      INT           NULL,
    [IsActive]                     BIT           CONSTRAINT [DF_SampleRequiredFieldGroup_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                    INT           NULL,
    [UpdateStamp]                  DATETIME      CONSTRAINT [DF_SampleRequiredFieldGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_SampleRequiredFieldGroup] PRIMARY KEY CLUSTERED ([SampleRequiredFieldGroupID] ASC),
    CONSTRAINT [UQ_SampleRequiredFieldGroup_Code] UNIQUE NONCLUSTERED ([SampleRequiredFieldGroupCode] ASC)
);

