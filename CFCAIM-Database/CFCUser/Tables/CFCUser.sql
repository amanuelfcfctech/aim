CREATE TABLE [CFCUser].[CFCUser] (
    [CFCUserID]                  INT              IDENTITY (0, 1) NOT NULL,
    [UserID]                     UNIQUEIDENTIFIER NOT NULL,
    [FirstName]                  VARCHAR (50)     NULL,
    [LastName]                   VARCHAR (50)     NULL,
    [IsActive]                   BIT              NULL,
    [EmailAddress]               NVARCHAR (50)    NULL,
    [LastActiveDataMapID]        INT              NULL,
    [LoggedInStatus]             BIT              CONSTRAINT [DF_CFCUser_LoggedInStatus] DEFAULT ((0)) NULL,
    [UserName]                   NVARCHAR (50)    NULL,
    [SpecificIngredientsGroupID] INT              NULL,
    [AIMOwnerLocationGroupID]    INT              NULL,
    [SpecificProductsGroupID]    INT              NULL,
    [AIMSupplierGroupID]         INT              NULL,
    [AIMSupplierLocationGroupID] INT              NULL,
    [C5UserName]                 NVARCHAR (8)     NULL,
    [SampleTestTypeGroupID]      INT              NULL,
    [TabOrderGroupID]            INT              NULL,
    [SampleScreenDisplayGroupID] INT              NULL,
    [SampleRequiredFieldGroupID] INT              NULL,
    CONSTRAINT [PK_CFCUser] PRIMARY KEY CLUSTERED ([CFCUserID] ASC)
);

