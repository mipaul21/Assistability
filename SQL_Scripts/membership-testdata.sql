USE [assistability_db]
GO

print '' print '*** creating membership test records ***'
GO
INSERT INTO [dbo].[Membership]
	([GroupID], [UserID], [Active])
VALUES
	(1, 1, 1),
	(1, 2, 1),
	(1, 3, 1)
GO
