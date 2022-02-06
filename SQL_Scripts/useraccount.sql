-- <summary>
-- Nathaniel Webber
-- Created: 2021/02/18
--
--
-- Script for the creation of the UserAccount table
-- and related stored procedures
-- </summary>
-- <remarks>
-- William Clark
-- Updated: 2021/02/23
-- Added a comment as previously absent
-- </remarks>
-- <remarks>
-- Ryan Taylor
-- Updated: 2021/03/05
-- Added the insert account stored procedure
-- </remarks>
-- <remarks>
-- Ryan Taylor
-- Updated: 2021/03/26
-- had to move the sp_insert_new_user to usergroup
-- in order to use the usergroup table
-- </remarks>
-- <remarks>
-- Ryan Taylor
-- Updated: 2021/03/25
-- added sp_reset_passwordhash
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
print '' print '*** creating UserAccount table ***'
GO
CREATE TABLE [dbo].[UserAccount]
(
	[UserID]		[INT]				IDENTITY(1,1)	NOT NULL,
	[FirstName]		[NVARCHAR](50)						NOT NULL,
	[LastName]		[NVARCHAR](50)						NOT NULL,
	[UserName]		[NVARCHAR](50)						NOT NULL,
	[Email]			[NVARCHAR](250)					NOT NULL,
	[PasswordHash]	     [NVARCHAR](100)					NOT NULL
                         /* Hash Code of 'newuser' */
					DEFAULT '9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]		     [BIT]							NOT NULL
					DEFAULT(1)
	CONSTRAINT [pk_UserID]		PRIMARY KEY([UserID] ASC),
	CONSTRAINT [ak_UserName]	     UNIQUE([UserName] ASC),
	CONSTRAINT [ak_Email]		UNIQUE([Email] ASC)
)
GO



print '' print '' print '*** USERACCOUNT PROCEDURES ***'
GO

print '' print '*** Creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@UserName		[NVARCHAR](50),
	@PasswordHash	[NVARCHAR](100)
)
AS
	BEGIN
		SELECT COUNT (UserName)
		FROM 	UserAccount
		WHERE 	UserName = @UserName
		AND 	PasswordHash = @PasswordHash
		AND 	Active = 1
	END
GO

print '' print '*** Creating sp_select_user_by_username ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_username]
(
	@UserName	[NVARCHAR](50)
)
AS
	BEGIN
		SELECT UserID, FirstName, LastName, UserName, Email, Active
		FROM UserAccount
		WHERE UserName = @UserName
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/02/18
--
-- Selects a UserAccount by it's ID
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_useraccount_by_useraccountid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_useraccount_by_useraccountid]
	(
		@UserID			[INT]
	)
AS
	BEGIN
		SELECT [FirstName], [LastName],
          [UserName],[Email], [Active]
		FROM UserAccount
		WHERE [UserID] = @UserID
	END
GO

-- <summary>
-- Mitchell Paul
-- Created: 2021/18/02
--
-- Creates sp_update_UserAccount stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_update_UserAccount ***'
GO
CREATE PROCEDURE [dbo].[sp_update_UserAccount]
	(
		@UserID			    [int],
	    @OldFirstName		[nvarchar](50),
		@OldLastName		[nvarchar](50),
		@OldUserName		[nvarchar](50),
		@OldEmail			[nvarchar](250),
        @NewFirstName		[nvarchar](50),
        @NewLastName		[nvarchar](50),
		@NewUserName		[nvarchar](50),
		@NewEmail			[nvarchar](250)

	)
AS
	BEGIN
		UPDATE UserAccount
			SET FirstName = @NewFirstName,
				LastName = @NewLastName,
				UserName = @NewUserName,
				Email = @NewEmail
			WHERE UserID = @UserID
			AND Email = @OldEmail
			AND	FirstName = @OldFirstName
			AND	LastName = @OldLastName
			AND UserName = @OldUserName
		RETURN @@ROWCOUNT
	END
GO



-- <summary>
-- Mitchell Paul
-- Created: 2021/18/02
--
-- Creates sp_safely_deactivate_UserAccount stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_safely_deactivate_UserAccount ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_deactivate_UserAccount]
	(
		@UserID			[int]
	)
AS
	BEGIN
		DECLARE @Admins int

		SELECT @Admins = COUNT(RoleID)
		FROM MembershipRole
		WHERE RoleID = 'Admin'
		  AND MembershipRole.UserID = @UserID

		IF @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				UPDATE UserAccount
					SET Active = 0
					WHERE UserAccount.UserID = @UserID
				RETURN @@ROWCOUNT
			END
	END
GO



-- <summary>
-- Mitchell Paul
-- Created: 2021/18/02
--
-- Creates sp_safely_reactivate_UserAccount stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_safely_reactivate_UserAccount ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_reactivate_UserAccount]
	(
		@UserID			[int]
	)
AS
	BEGIN
		UPDATE UserAccount
			SET Active = 1
			WHERE UserID = @UserID
		RETURN @@ROWCOUNT
	END
GO



-- <summary>
-- Mitchell Paul
-- Created: 2021/09/03
--
-- Creates sp_select_all_UserAccounts
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_all_UserAccounts ***'
GO
CREATE PROCEDURE [dbo].[sp_select_UserAccounts]
AS
	BEGIN
		SELECT		UserID, Email, FirstName, LastName, UserName, Active
		FROM		UserAccount
		ORDER BY 	LastName ASC
	END
GO


-- <summary>
-- Mitchell Paul
-- Created: 2021/09/03
--
-- Creates sp_safely_remove_UserAccountRole
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_safely_delete_UserAccount ***'


GO
CREATE PROCEDURE [dbo].[sp_safely_delete_UserAccount]
	(
		@UserID			[int]
	)
AS
		BEGIN
			DELETE FROM UserAccount
			WHERE UserAccount.UserID = @UserID
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/25
--
-- Creates sp_update_passwordhash
-- </summary>
print '' print '*** creating sp_reset_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_reset_passwordhash]
	(
	@Email				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE UserAccount
			SET PasswordHash = '9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E'
			WHERE Email = @Email
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/04/22
--
-- Selects a UserAccount by it's email
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_useraccount_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_useraccount_by_email]
	(
		@Email			[NVARCHAR](250)
	)
AS
	BEGIN
		SELECT [UserID], [FirstName], [LastName],
          [UserName], [Active]
		FROM UserAccount
		WHERE [Email] = @Email
	END
GO
