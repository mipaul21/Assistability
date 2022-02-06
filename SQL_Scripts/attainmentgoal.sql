print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO
-- <summary>
-- Becky Baenziger
-- Created: 2021/03/28
--
-- Creates the Attainment Goal table
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating attainment goal table ***'
GO
CREATE TABLE [dbo].[AttainmentGoal](
	[UserID_client]					[int]			NOT NULL,
	[UserID_admin]					[int]			NOT NULL,
	[AttGoalName]					[nvarchar](50)	NOT NULL,
	[AttGoalDescription]			[nvarchar](500)	NOT NULL,
	[AttGoalTargetDate]				[datetime],
	[AttGoalEntryDate]				[datetime]		NOT NULL,
	[AttGoalEditDate]				[datetime],
	[AttGoalRemovalDate]			[datetime],
	[Active]						[bit]			NOT NULL	DEFAULT 1,
	[PerformanceFrequency]			[int]			NOT NULL	DEFAULT 1,
	[AwardName]						[nvarchar](50)	NOT NULL,
	[PerformanceName]				[nvarchar](50)	NOT NULL,
	CONSTRAINT [pk_AttGoalName_UserID_client_AttGoalEntryDate] PRIMARY KEY([UserID_client], [AttGoalName], [AttGoalEntryDate]),
	CONSTRAINT [fk_UserID_client_AttGoal] FOREIGN KEY ([UserID_client])
		REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT [fk_UserID_admin_AttGoal] FOREIGN KEY ([UserID_admin])
		REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT [fk_AwardName_AttGoal] FOREIGN KEY ([AwardName])
		REFERENCES [dbo].[award]([AwardName]),
	CONSTRAINT [fk_PeformanceName_AttGoal] FOREIGN KEY ([UserID_client], [PerformanceName])
		REFERENCES [dbo].[Performance]([UserID_client], [PerformanceName])
	
)
GO

-- <summary>
-- Becky Baenziger
-- 2021/03/28
--
-- Creates the stored procedure sp_create_attainment_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_create_attainment_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_create_attainment_goal]
	(
		@UserID_client			[int],
		@UserID_admin			[int],
		@AttGoalName			[nvarchar](50),
		@AttGoalDescription		[nvarchar](500),
		@AttGoalTargetDate		[datetime],
		@AttGoalEntryDate		[datetime],
		@PerformanceFrequency	[int],
		@AwardName				[nvarchar](50),
		@PerformanceName		[nvarchar](50)
	)

AS
	BEGIN
		INSERT INTO [dbo].[AttainmentGoal]
			([UserID_client], [UserID_admin], [AttGoalName], [AttGoalDescription], [AttGoalTargetDate],
			[AttGoalEntryDate], [PerformanceFrequency], [AwardName], [PerformanceName])
		VALUES
			(@UserID_client, @UserID_admin, @AttGoalName, @AttGoalDescription, @AttGoalTargetDate, 
			@AttGoalEntryDate, @PerformanceFrequency, @AwardName, @PerformanceName)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Becky Baenziger
-- Created: 2021/03/28
--
-- Creates the stored procedure sp_update_attainment_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_update_attainment_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_update_attainment_goal]
	(
		@UserID_client				[int],
		@AttGoalName				[nvarchar](50),
		@AttGoalEntryDate			[datetime],
		@NewAttGoalDescription		[nvarchar](500),
		@NewAttGoalTargetDate		[datetime],
		@NewAttGoalEditDate			[datetime],
		@NewPerformanceFrequency	[int],
		@NewAwardName				[nvarchar](50)
	)
AS
	BEGIN
		UPDATE AttainmentGoal
			SET AttGoalDescription = @NewAttGoalDescription,
				AttGoalEditDate = @NewAttGoalEditDate,
				PerformanceFrequency = @NewPerformanceFrequency,
				AwardName = @NewAwardName
			WHERE UserID_client = @UserID_client
				AND AttGoalName = @AttGoalName
				AND AttGoalEntryDate = @AttGoalEntryDate
		RETURN @@ROWCOUNT
	END
GO
			
-- <summary>
-- Becky Baenziger
-- 2021/03/28
--
-- Creates the stored procedure sp_deactivate_attainment_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_deactivate_attainment_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_attainment_goal]
	(
		@AttGoalName		[nvarchar](50),
		@AttGoalEntryDate	[datetime],
		@UserID_client		[int]
	)
AS
	BEGIN
		UPDATE AttainmentGoal
			SET Active = 0,
				AttGoalRemovalDate = GETDATE()
			WHERE AttGoalName = @AttGoalName
				AND AttGoalEntryDate = @AttGoalEntryDate
				AND UserID_client = @UserID_client
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/02/18
--
-- Creates the stored procedure sp_reactivate_attainment_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print'*** creating sp_reactivate_attainment_goal ***'
GO
CREATE PROCEDURE[dbo].[sp_reactivate_attainment_goal]
	(
		@AttGoalName		[nvarchar](50),
		@AttGoalEntryDate	[datetime],
		@UserID_client		[int]
	)
AS
	BEGIN
		UPDATE AttainmentGoal
			SET Active = 1
			WHERE AttGoalName = @AttGoalName
				AND AttGoalEntryDate = @AttGoalEntryDate
				AND UserID_client = @UserID_client
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/03/28
--
-- Creates the stored procedure sp_select_attainment_goal_by_active
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_select_attainment_goal_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_attainment_goal_by_active]
	(
		@UserID_client	[int],
		@Active			[bit]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, AttGoalName, AttGoalDescription, AttGoalTargetDate, AttGoalEntryDate, PerformanceFrequency, AwardName, PerformanceName, Active
		FROM 		AttainmentGoal
		WHERE Active = @Active
			AND UserID_client = @UserID_client
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/03/28
-- 
-- Creates the stored procedure sp_select_attainment_goal_by_target_date
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>

print '' print '*** creating sp_select_attainment_goal_by_taget_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_attainment_goal_by_target_date]
	(
	
		@AttGoalTargetDate		[datetime]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, AttGoalName, AttGoalDescription, AttGoalTargetDate, AttGoalEntryDate, PerformanceFrequency, AwardName, PerformanceName, Active
		FROM 		AttainmentGoal
		WHERE 		AttGoalTargetDate = @AttGoalTargetDate
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/03/28
--
-- Creates the stored procedure sp_select_attainment_goal_by_userID_client
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_select_attainment_goal_by_userID_client ***'
GO
CREATE PROCEDURE [dbo].[sp_select_attainment_goal_by_userID_client]
	(
		@userID_client		[int]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, AttGoalName, AttGoalDescription, AttGoalTargetDate, AttGoalEntryDate, PerformanceFrequency, AwardName, PerformanceName, Active
		FROM 		AttainmentGoal
		WHERE 		userID_client = @userID_client
	END
GO