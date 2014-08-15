-- 14th Apr, 2014
-- Adam Galloway
-- Create GroupUsers Table

CREATE TABLE [GroupUsers](
	[GroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	CONSTRAINT [PK_GroupUsers] PRIMARY KEY CLUSTERED ( [GroupId] ASC, [UserId] ASC )
)

ALTER TABLE [GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_Groups] FOREIGN KEY([GroupId])
REFERENCES [Groups] ([Id]) ON DELETE CASCADE

ALTER TABLE [GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_Users] FOREIGN KEY([UserId])
REFERENCES [Users] ([Id]) ON DELETE CASCADE

