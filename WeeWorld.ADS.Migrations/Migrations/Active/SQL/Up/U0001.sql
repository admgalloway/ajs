-- 14th Apr, 2014
-- Adam Galloway
-- Create Users Table

CREATE TABLE [Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[Salt] [binary](8) NOT NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ( [Id] ASC ),
	 CONSTRAINT [IX_Users_EmailAddress] UNIQUE NONCLUSTERED ( [EmailAddress] ASC)
)