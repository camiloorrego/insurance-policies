CREATE TABLE [PolicyTypes] (
  [PolicyTypeId] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [Coverage] decimal(18,2)
)
GO

CREATE TABLE [Polices] (
  [PoliceId] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(100),
  [Description] varchar(max),
  [PolicyType] int,
  [EffectiveDate] datetime,
  [CreateDate] datetime DEFAULT (GETDATE()),
  [Terms] int,
  [Cost] decimal(18,2),
  [RiskType] int,
  [UserId] int
)
GO

CREATE TABLE [RiskTypes] (
  [RiskTypeId] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(100)
)
GO

CREATE TABLE [Clients] (
  [ClientId] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(100)
)
GO

CREATE TABLE [ClientsPolices] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ClientId] int,
  [PoliceId] int,
  [CreateDate] datetime DEFAULT (GETDATE()),
  [UserId] int
)
GO

CREATE TABLE [Users] (
  [UserId] int PRIMARY KEY IDENTITY(1, 1),
  [Username] varchar(50),
  [Password] varchar(50),
  [CreateDate] datetime DEFAULT (GETDATE())
)
GO

ALTER TABLE [Polices] ADD FOREIGN KEY ([PolicyType]) REFERENCES [PolicyTypes] ([PolicyTypeId])
GO

ALTER TABLE [Polices] ADD FOREIGN KEY ([RiskType]) REFERENCES [RiskTypes] ([RiskTypeId])
GO

ALTER TABLE [Polices] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [ClientsPolices] ADD FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([ClientId])
GO

ALTER TABLE [ClientsPolices] ADD FOREIGN KEY ([PoliceId]) REFERENCES [Polices] ([PoliceId])
GO

ALTER TABLE [ClientsPolices] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO
