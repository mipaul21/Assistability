USE [assistability_db]
GO

print '' print '*** creating usergroup test records ***'
GO
INSERT INTO [dbo].[UserGroup]
	([UserID_Owner])
VALUES
	(1)
GO