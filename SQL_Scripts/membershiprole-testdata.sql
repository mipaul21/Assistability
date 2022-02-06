USE [assistability_db]
GO

print '' print '*** creating membershiprole test records ***'
GO
INSERT INTO [dbo].[MembershipRole]
	([GroupID], [UserID], [RoleID], [RoleName])
VALUES
	(1, 1, 1, 'Admin'),
	(1, 2, 2, 'Caregiver'),
	(1, 3, 3, 'Client')
GO