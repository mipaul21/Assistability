-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Script for the creation of the UserGroup table
-- and related stored procedures
-- </summary>
-- <remarks>
-- Ryan Taylor
-- Updated: 2021/03/26
-- added the sp_insert_new_user
-- in order to use the usergroup table
-- </remarks>
print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/02/18
--
-- Definition of the UserGroup table
-- </summary>
-- <remarks>
-- William Clark
-- Updated: 2021/02/23
-- Updated summary description
-- </remarks>
print '' print '*** creating UserGroup table ***'
GO
CREATE TABLE [dbo].[UserGroup]
(
	[UserID_Owner]		[INT]							NOT NULL,
	[GroupID]		[INT]			IDENTITY(1,1)			NOT NULL
	CONSTRAINT	[pk_GroupID]	PRIMARY KEY([GroupID] ASC),
	CONSTRAINT	[fk_UserGroup_UserID]		FOREIGN KEY([UserID_owner])
	REFERENCES [dbo].[UserAccount]([UserID])
)
GO

-- <summary>
-- William Clark
-- Created: 2021/02/18
--
-- Selects all UserGroups by UserAccountID
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_usergroups_by_useraccountid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_usergroups_by_useraccountid]
	(
		@UserAccountID			[INT]
	)
AS
	BEGIN
		SELECT [GroupID]
		FROM UserGroup
		WHERE [UserID_Owner] = @UserAccountID
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/02/18
--
-- Creates sp_insert_new_user stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_insert_new_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user]
(
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@UserName		[nvarchar](50),
	@Email			[nvarchar](250),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		DECLARE @userId [INT]
		DECLARE @groupId [INT]
		INSERT INTO [dbo].[UserAccount]
			([FirstName], [LastName], [UserName], [Email], [PasswordHash])
		VALUES
			(@FirstName, @LastName, @UserName, @Email, @PasswordHash)
		SELECT SCOPE_IDENTITY()
		SET @userId = SCOPE_IDENTITY()
		INSERT INTO [dbo].[UserGroup]
			([UserID_Owner])
		VALUES
			(@userId)
		SELECT SCOPE_IDENTITY()
		SET @groupID = SCOPE_IDENTITY()
		INSERT INTO [dbo].[Membership]
			([GroupID], [UserID], [Active])
		VALUES
			(@groupID, @userId, 1)
		INSERT INTO [dbo].[MembershipRole]
			([GroupID], [UserID], [RoleID], [RoleName])
		VALUES
			(@groupID, @userId, 1, "Admin")
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/04/15
--
-- Creates sp_insert_new_user_non_admin stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_insert_new_user_non_admin ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user_non_admin]
(
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@UserName		[nvarchar](50),
	@Email			[nvarchar](250),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		DECLARE @userId [INT]
		INSERT INTO [dbo].[UserAccount]
			([FirstName], [LastName], [UserName], [Email], [PasswordHash])
		VALUES
			(@FirstName, @LastName, @UserName, @Email, @PasswordHash)
		SELECT SCOPE_IDENTITY()
		SET @userId = SCOPE_IDENTITY()
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/04/24
--
-- Selects all UserGroups by GroupID
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_usergroups_by_groupid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_usergroups_by_groupid]
	(
		@GroupID			[INT]
	)
AS
	BEGIN
		SELECT [UserID_Owner]
		FROM UserGroup
		WHERE [GroupID] = @GroupID
	END
GO