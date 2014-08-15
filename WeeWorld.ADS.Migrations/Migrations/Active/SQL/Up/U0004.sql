-- 14th Apr, 2014
-- Adam Galloway
-- Create Builds Table

CREATE TABLE [Builds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationId] [int] NOT NULL,
	[Type] [int] NOT NULL DEFAULT ((0)),
	[VersionNumber] [varchar](50) NOT NULL,
	[BuildNumber] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[PackageUrl] [varchar](255) NOT NULL,
	[ReleaseNotes] [varchar](max) NULL,
	[SubmissionState] [int] NOT NULL,
	[SubmissionNotes] [varchar](max) NULL,
	[SubmissionDate] [datetime] NULL,
 CONSTRAINT [PK_Builds] PRIMARY KEY CLUSTERED ( [Id] ASC)
)

ALTER TABLE [Builds]  WITH CHECK ADD  CONSTRAINT [FK_Builds_Applications] FOREIGN KEY([ApplicationId])
REFERENCES [Applications] ([Id]) ON DELETE CASCADE

