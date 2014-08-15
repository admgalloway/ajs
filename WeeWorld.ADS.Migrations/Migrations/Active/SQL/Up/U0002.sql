-- 14th Apr, 2014
-- Adam Galloway
-- Create Tokens Table

CREATE TABLE [Tokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[ExpiryDate] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	CONSTRAINT [PK_Tokens] PRIMARY KEY CLUSTERED ( [Id] ASC)
)

ALTER TABLE [Tokens]  WITH CHECK ADD CONSTRAINT [FK_Tokens_Users] FOREIGN KEY([UserId])
REFERENCES [Users] ([Id]) ON DELETE CASCADE