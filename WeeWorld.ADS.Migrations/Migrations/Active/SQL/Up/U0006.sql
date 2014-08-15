-- 14th Apr, 2014
-- Adam Galloway
-- Create GroupApplications Table

CREATE TABLE [GroupApplications](
	[GroupId] [int] NOT NULL,
	[ApplicationId] [int] NOT NULL,
	CONSTRAINT [PK_GroupApplications] PRIMARY KEY CLUSTERED ( [GroupId] ASC, [ApplicationId] ASC )
)

ALTER TABLE [GroupApplications]  WITH CHECK ADD  CONSTRAINT [FK_GroupApplications_Applications] FOREIGN KEY([ApplicationId])
REFERENCES [Applications] ([Id]) ON DELETE CASCADE

ALTER TABLE [GroupApplications]  WITH CHECK ADD  CONSTRAINT [FK_GroupApplications_Groups] FOREIGN KEY([GroupId])
REFERENCES [Groups] ([Id]) ON DELETE CASCADE

