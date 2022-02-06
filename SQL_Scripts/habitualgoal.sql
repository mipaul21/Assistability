print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO
-- <summary>
-- Becky Baenziger
-- Created: 2021/02/18
--
-- Creates the Habiutal Goal table
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating habitual goal table ***'
GO
CREATE TABLE [dbo].[HabitualGoal](
	[UserID_client]			[int]			NOT NULL,
	[UserID_admin]			[int]			NOT NULL,
	[HabGoalName]			[nvarchar](50)	NOT NULL,
	[HabGoalDescription]	[nvarchar](500)	NOT NULL,
	[HabGoalTargetDate]		[datetime],
	[HabGoalEntryDate]		[datetime]		NOT NULL,
	[HabGoalEditDate]		[datetime],
	[HabGoalRemovalDate]	[datetime],
	[RoutineFrequency]		[int]			NOT NULL	DEFAULT 1,
	[Active]				[bit]			NOT NULL	DEFAULT 1,
	[AwardName]				[nvarchar](50)	NOT NULL,
	[RoutineName]			[nvarchar](50)	NOT NULL,
	CONSTRAINT [pk_HabGoalName_UserID_client_HabGoalEntryDate] PRIMARY KEY([UserID_client], [HabGoalName], [HabGoalEntryDate]),
	CONSTRAINT [fk_UserID_client_HabGoal] FOREIGN KEY ([UserID_client])
		REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT [fk_UserID_admin_HabGoal] FOREIGN KEY ([UserID_admin])
		REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT [fk_AwardName_HabGoal] FOREIGN KEY ([AwardName])
		REFERENCES [dbo].[award]([AwardName]),
	CONSTRAINT [fk_RoutineName_HabGoal] FOREIGN KEY ([RoutineName])
		REFERENCES [dbo].[Routines]([RoutineName])
)
GO

-- <summary>
-- Becky Baenziger
-- 2021/02/18
--
-- Creates the stored procedure sp_create_habitual_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_create_habitual_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_create_habitual_goal]
	(
		@UserID_client			[int],
		@UserID_admin			[int],
		@HabGoalName			[nvarchar](50),
		@HabGoalDescription		[nvarchar](500),
		@HabGoalTargetDate		[datetime],
		@HabGoalEntryDate		[datetime],
		@RoutineFrequency		[int],	
		@AwardName				[nvarchar](50),
		@RoutineName			[nvarchar](50)
	)

AS
	BEGIN
		INSERT INTO [dbo].[HabitualGoal]
			([UserID_client], [UserID_admin], [HabGoalName], [HabGoalDescription], [HabGoalTargetDate],
			[HabGoalEntryDate], [RoutineFrequency], [AwardName], [RoutineName])
		VALUES
			(@UserID_client, @UserID_admin, @HabGoalName, @HabGoalDescription, @HabGoalTargetDate, 
			@HabGoalEntryDate, @RoutineFrequency, @AwardName, @RoutineName)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Becky Baenziger
-- Created: 2021/03/12
--
-- Creates the stored procedure sp_update_habitual_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_update_habitual_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_update_habitual_goal]
	(
		@UserID_client			[int],
		@HabGoalName			[nvarchar](50),
		@HabGoalEntryDate		[datetime],
		@NewHabGoalDescription	[nvarchar](500),
		@NewHabGoalEditDate		[datetime],
		@NewRoutineFrequency	[int],
		@NewAwardName			[nvarchar](50)
	)
AS
	BEGIN
		UPDATE HabitualGoal
			SET HabGoalDescription = @NewHabGoalDescription,
				HabGoalEditDate = @NewHabGoalEditDate,
				RoutineFrequency = @NewRoutineFrequency,
				AwardName = @NewAwardName
			WHERE UserID_client = @UserID_client
				AND HabGoalName = @HabGoalName
				AND HabGoalEntryDate = @HabGoalEntryDate
		RETURN @@ROWCOUNT
	END
GO
			
-- <summary>
-- Becky Baenziger
-- 2021/02/18
--
-- Creates the stored procedure sp_deactivate_habitual_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_deactivate_habitual_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_habitual_goal]
	(
		@HabGoalName		[nvarchar](50),
		@HabGoalEntryDate	[datetime],
		@UserID_client		[int]
	)
AS
	BEGIN
		UPDATE HabitualGoal
			SET Active = 0,
				HabGoalRemovalDate = GETDATE()
			WHERE HabGoalName = @HabGoalName
				AND HabGoalEntryDate = @HabGoalEntryDate
				AND UserID_client = @UserID_client
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/02/18
--
-- Creates the stored procedure sp_reactivate_habitual_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print'*** creating sp_reactivate_habitual_goal ***'
GO
CREATE PROCEDURE[dbo].[sp_reactivate_habitual_goal]
	(
		@HabGoalName		[nvarchar](50),
		@HabGoalEntryDate	[datetime],
		@UserID_client		[int]
	)
AS
	BEGIN
		UPDATE HabitualGoal
			SET Active = 1
			WHERE HabGoalName = @HabGoalName
				AND HabGoalEntryDate = @HabGoalEntryDate
				AND UserID_client = @UserID_client
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/02/18
--
-- Creates the stored procedure sp_select_habitual_goal_by_active
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_select_habitual_goal_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_habitual_goal_by_active]
	(
		@UserID_client	[int],	
		@Active			[bit]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, HabGoalName, HabGoalDescription, HabGoalTargetDate, HabGoalEntryDate, RoutineFrequency, AwardName, RoutineName, Active
		FROM 		HabitualGoal
		WHERE Active = @Active
			AND UserID_client = @UserID_client
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/02/18
-- 
-- Creates the stored procedure sp_select_habitual_goal_by_taget_date
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>

print '' print '*** creating sp_select_habitual_goal_by_taget_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_habitual_goal_by_target_date]
	(
		@HabGoalTargetDate		[datetime]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, HabGoalName, HabGoalDescription, HabGoalTargetDate, HabGoalEntryDate, RoutineFrequency, AwardName, RoutineName, Active
		FROM 		HabitualGoal
		WHERE 		HabGoalTargetDate = @HabGoalTargetDate
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/02/18
--
-- Creates the stored procedure sp_select_habitual_goal_by_userID_client
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_select_habitual_goal_by_userID_client ***'
GO
CREATE PROCEDURE [dbo].[sp_select_habitual_goal_by_userID_client]
	(
		@userID_client		[int]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, HabGoalName, HabGoalDescription, HabGoalTargetDate, HabGoalEntryDate, RoutineFrequency, AwardName, RoutineName, Active
		FROM 		HabitualGoal
		WHERE 		userID_client = @userID_client
	END
GO