CREATE TABLE [dbo].[SampleQualityEquationBuild] (
    [SampleQualityEquationBuildID] INT          IDENTITY (1, 1) NOT NULL,
    [SampleQualityEquationID]      INT          NULL,
    [ValueString]                  VARCHAR (50) NULL,
    [CFCUserID]                    INT          NULL,
    [UpdateStamp]                  DATETIME     CONSTRAINT [DF_SampleQualityEquationBuild_UpdateStamp] DEFAULT (getdate()) NULL,
    [Status]                       BIT          NULL,
    [SampleQualityParameterID]     INT          NULL,
    CONSTRAINT [PK_SampleEquationBuild] PRIMARY KEY CLUSTERED ([SampleQualityEquationBuildID] ASC)
);

