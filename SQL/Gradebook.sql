CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(60) UNIQUE NOT NULL,
  [Email] nvarchar(128) UNIQUE NOT NULL,
  [DateCreated] datetime NOT NULL,
  [FirebaseId] nvarchar(28) NOT NULL
)
GO

CREATE TABLE [LegalGuardian] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [GuardianId] int NOT NULL,
  [DependentId] int NOT NULL
)
GO

CREATE TABLE [District] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(80) NOT NULL
)
GO

CREATE TABLE [School] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(75) NOT NULL,
  [DistrictId] int NOT NULL
)
GO

CREATE TABLE [Group] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(75) NOT NULL,
  [Description] nvarchar(255),
  [SchoolId] int NOT NULL,
  [StartDate] datetime NOT NULL,
  [EndDate] datetime NOT NULL,
  [GroupType] int NOT NULL
)
GO

CREATE TABLE [GroupType] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL
)
GO

CREATE TABLE [GroupDistrictUser] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [DistrictUserId] int NOT NULL,
  [GroupId] int NOT NULL,
  [GroupRoleId] int NOT NULL
)
GO

CREATE TABLE [Assignment] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(100) NOT NULL,
  [Description] nvarchar(2000) NOT NULL,
  [GroupId] int NOT NULL,
  [CreatedOn] datetime NOT NULL,
  [DueBy] datetime NOT NULL
)
GO

CREATE TABLE [GroupUserAssignment] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [GroupUserId] int NOT NULL,
  [AssignmentId] int NOT NULL,
  [Grade] int
)
GO

CREATE TABLE [GroupRole] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
  [DistrictId] int
)
GO

CREATE TABLE [DistrictRole] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
  [DistrictId] int
)
GO

CREATE TABLE [DistrictUser] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [DistrictId] int NOT NULL,
  [DistrictRoleId] int NOT NULL
)
GO

ALTER TABLE [DistrictUser] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [DistrictUser] ADD FOREIGN KEY ([DistrictId]) REFERENCES [District] ([Id])
GO

ALTER TABLE [DistrictRole] ADD FOREIGN KEY ([DistrictId]) REFERENCES [District] ([Id])
GO

ALTER TABLE [DistrictUser] ADD FOREIGN KEY ([DistrictRoleId]) REFERENCES [DistrictRole] ([Id])
GO

ALTER TABLE [School] ADD FOREIGN KEY ([DistrictId]) REFERENCES [District] ([Id])
GO

ALTER TABLE [LegalGuardian] ADD FOREIGN KEY ([GuardianId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [LegalGuardian] ADD FOREIGN KEY ([DependentId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Group] ADD FOREIGN KEY ([SchoolId]) REFERENCES [School] ([Id])
GO

ALTER TABLE [Group] ADD FOREIGN KEY ([GroupType]) REFERENCES [GroupType] ([Id])
GO

ALTER TABLE [GroupDistrictUser] ADD FOREIGN KEY ([DistrictUserId]) REFERENCES [DistrictUser] ([Id])
GO

ALTER TABLE [GroupDistrictUser] ADD FOREIGN KEY ([GroupId]) REFERENCES [Group] ([Id])
GO

ALTER TABLE [GroupDistrictUser] ADD FOREIGN KEY ([GroupRoleId]) REFERENCES [GroupRole] ([Id])
GO

ALTER TABLE [GroupUserAssignment] ADD FOREIGN KEY ([GroupUserId]) REFERENCES [GroupDistrictUser] ([Id])
GO

ALTER TABLE [GroupUserAssignment] ADD FOREIGN KEY ([AssignmentId]) REFERENCES [Assignment] ([Id])
GO

ALTER TABLE [Assignment] ADD FOREIGN KEY ([GroupId]) REFERENCES [Group] ([Id])
GO
