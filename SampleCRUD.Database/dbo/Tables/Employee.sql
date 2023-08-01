CREATE TABLE [dbo].[Employee] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (50) NOT NULL,
    [LastName]     VARCHAR (50) NOT NULL,
    [EmployeeCode] VARCHAR (20) NOT NULL,
    [DepartmentId] INT          NOT NULL,
    [CreatedBy]    INT          NOT NULL,
    [CreatedOn]    DATETIME     NOT NULL,
    [UpdatedBy]    INT          NOT NULL,
    [UpdatedOn]    DATETIME     NOT NULL,
    [IsDeleted]    BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_ApplicationUser_Created] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Employee_ApplicationUser_Updated] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Employee_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id])
);

