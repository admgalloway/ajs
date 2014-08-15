-- 14th Apr, 2014
-- Adam Galloway
-- Create seed data

INSERT INTO [Groups] ([Name], [AlertStatus]) VALUES ('Administrators', 0)

INSERT INTO [Users] ([EmailAddress], [Password], [Salt]) VALUES ('admin@weeworld.com', 'x8xRVALxhqBkzTzyL9JZJlDlbZuBHhw6jvkq7Ar+K7k/0tV7L1QChS+Tx51v7v/X', 0x21EA42E218DF76B0)

DECLARE @userId AS INT 
SET @userId = (SELECT TOP 1 [Id] FROM [users] WHERE [EmailAddress] = 'admin@weeworld.com')

DECLARE @groupId AS INT 
SET @groupId = (SELECT TOP 1 [Id] FROM [Groups] WHERE [name] = 'Administrators')


INSERT INTO [GroupUsers] ([GroupId] ,[UserId]) VALUES (@groupId,@userId)

