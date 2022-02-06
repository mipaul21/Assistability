-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Script for the creation of the MembershipRole table
-- and related stored procedures
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/02/18
--
-- Definition of the UserAccount table
-- </summary>
-- <remarks>
-- William Clark
-- Updated: 2021/02/23
-- Updated summary description
-- </remarks>
print '' print '*** creating MembershipRole table ***'
GO
CREATE TABLE [dbo].[MembershipRole]
(
	[GroupID]		[INT]							NOT NULL,
	[UserID]		[INT]							NOT NULL,
	[RoleID]		[INT]							NULL,
	[RoleName]		[VARCHAR](50)					NOT NULL
	CONSTRAINT	[fk_MembershipRole_GroupID] FOREIGN KEY([GroupID])
	REFERENCES [dbo].[UserGroup]([GroupID]),
	CONSTRAINT	[fk_MembershipRole_UserID] FOREIGN KEY([UserID])
	REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT	[fk_MembershipRole_RoleID]	   FOREIGN KEY([RoleID])
	REFERENCES [dbo].[Role]([RoleID]),
		CONSTRAINT	[fk_MembershipRole_RoleName]	   FOREIGN KEY([RoleName])
	REFERENCES [dbo].[Role]([RoleName])
)
GO

-- <summary>
-- William Clark
-- Created: 2021/02/25
--
-- Selects all MembershipRoles by Membership
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_membershiproles_by_membership ***'
GO
CREATE PROCEDURE [dbo].[sp_select_membershiproles_by_membership]
	(
		@GroupID			[INT],
		@UserID				[INT]
	)
AS
	BEGIN
		SELECT MembershipRole.[RoleID], MembershipRole.[RoleName], [RoleDescription]
		FROM MembershipRole
		INNER JOIN Role
			ON MembershipRole.RoleID = Role.RoleID
			AND MembershipRole.RoleName = Role.RoleName
		WHERE [GroupID] = @GroupID AND
			[UserID] = @UserID
	END
GO



-- <summary>
-- Mitchell Paul
-- Created: 2021/04/01
--
-- Inserts a membership role using the GroupID and UserID
-- </summary>
-- <remarks>
-- William Clark
-- Updated: 2021/04/15
-- Changed to accurately insert RoleID
-- </remarks>
print '' print '*** creating sp_insert_membershipRole ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_membershipRole]
	(
	@GroupID			[INT],
	@UserID				[INT],
	@RoleName			[NVARCHAR](50)
	)
AS
	BEGIN

		INSERT INTO [dbo].[MembershipRole]
		(
		[GroupID],
		[UserID],
		[RoleID],
		[RoleName]
		)
		VALUES
		(
		@GroupID,
		@UserID,
		(SELECT RoleID
		FROM Role
		WHERE Role.RoleName = @RoleName),
		@RoleName
		)
END
GO


-- <summary>
-- Mitchell Paul
-- Created: 2021/04/01
--
-- sp_select_all_membershiproles
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_all_membershiproles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_roles]
AS
	BEGIN
		SELECT 	RoleID,
		Rolename,
		RoleDescription
		FROM 	Role
		ORDER BY RoleID ASC
	END
GO



-- <summary>
-- Mitchell Paul
-- Created: 2021/04/01
--
-- sp_safely_remove_userrole
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_safely_remove_userrole ***'

GO
CREATE PROCEDURE [dbo].[sp_safely_remove_userrole]
	(
	@GroupID			[INT],
	@UserID				[INT],
	@RoleName			[NVARCHAR](50)
	)
AS
	BEGIN
			DELETE FROM MembershipRole
			WHERE RoleName = @RoleName
				AND UserID = @UserID
				AND GroupID = @GroupID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** creating sp_select_membershipnames_by_membership ***'
GO
CREATE PROCEDURE [dbo].[sp_select_membershipnames_by_membership]
	(
		@GroupID			[INT],
		@UserID				[INT]
	)
AS
	BEGIN
		SELECT MembershipRole.[RoleName], [RoleDescription]
		FROM MembershipRole
		INNER JOIN Role
			ON MembershipRole.RoleName = Role.RoleName
		WHERE [GroupID] = @GroupID AND
			[UserID] = @UserID
	END
GO
