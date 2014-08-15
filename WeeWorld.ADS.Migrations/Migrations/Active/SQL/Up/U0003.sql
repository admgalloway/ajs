-- 14th Apr, 2014
-- Adam Galloway
-- Create Applications Table

CREATE TABLE [Applications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[IconUrl] [varchar](250) NULL,
	[Deleted] [bit] NOT NULL DEFAULT ((0)),
	[Platform] [varchar](50) NOT NULL DEFAULT ('iOS'),
	 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ( [Id] ASC),
	 CONSTRAINT [IX_Applications] UNIQUE NONCLUSTERED ( [Name] ASC)
)