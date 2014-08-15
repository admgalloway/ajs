-- 14th Apr, 2014
-- Adam Galloway
-- Create Groups Table

CREATE TABLE [Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL  DEFAULT ((1)),
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[AlertStatus] [int] NOT NULL DEFAULT ((0)),
	 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ( [Id] ASC ),
	 CONSTRAINT [IX_Groups] UNIQUE NONCLUSTERED ( [Name] ASC)
)
