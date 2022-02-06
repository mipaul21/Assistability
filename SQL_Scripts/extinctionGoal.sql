print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- </summary>
-- <remarks>
-- Updater Name: Becky Baenziger
-- Update Date: 2021/04/02
-- changed userID names, added incident completion count and changed constraints,
-- changed description length to 500
-- </remarks>
print '' print '*** creating extinction goal table ***'
GO
CREATE TABLE [dbo].[ExtinctionGoal] (
    [ExtGoalName]        		NVARCHAR (50) 	NOT NULL,
    [ExtGoalDescription] 		NVARCHAR (500) 	NOT NULL,
    [IncidentName]       		NVARCHAR (50)  	NOT NULL,
    [ExtGoalTargetDate]  		DATETIME       	NOT NULL,
    [ExtGoalEntryDate]   		DATETIME       	NOT NULL,
    [ExtGoalEditDate]    		DATETIME       	NULL,
    [ExtGoalRemovalDate] 		DATETIME       	NULL,
    [Active]             		BIT            	NOT NULL	DEFAULT 1,
    [IncidentFrequency]  		INT            	NOT NULL,
    [UserID_client]       		INT            	NOT NULL,
    [UserID_admin]      		INT            	NOT NULL,
    [AwardName]            		NVARCHAR (50)  	NOT NULL,
	CONSTRAINT [pk_ExtGoalName_UserID_client_ExtGoalEntryDate] PRIMARY KEY ([UserID_client], [ExtGoalName], [ExtGoalEntryDate]),
	CONSTRAINT [fk_UserID_client_ExtGoal] FOREIGN KEY ([UserID_client])
		REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT [fk_UserID_admin_ExtGoal] FOREIGN KEY ([UserID_admin])
		REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT [fk_AwardName_Ext] FOREIGN KEY ([AwardName])
		REFERENCES [dbo].[award]([AwardName]),
	CONSTRAINT [fk_IncidentName_ExtGoal] FOREIGN KEY ([IncidentName])
		REFERENCES [dbo].[Incident]([IncidentName])
	
	
     --CONSTRAINT [PK_ExtinctionGoal] PRIMARY KEY ([ExtGoalName]),
	 --CONSTRAINT [FK_Incident_IncidentName] FOREIGN KEY([IncidentName]) 
	 --REFERENCES [INCIDENT](IncidentName) ON UPDATE CASCADE,
	 --CONSTRAINT [FK_Incident_UserIDClient] FOREIGN KEY([UserIDClient]) 
	 --REFERENCES [INCIDENT](UserIDClient) ON UPDATE CASCADE,
	 --CONSTRAINT [FK_Incident_UserIDCreator] FOREIGN KEY([UserIDCreator]) 
	 --REFERENCES [INCIDENT](UserIDCreator) ON UPDATE CASCADE,
	 --CONSTRAINT [FK_Award_AwardID] FOREIGN KEY([AwardID]) 
	 --REFERENCES [dbo].[Award](AwardID) ON UPDATE CASCADE
		----!>
)

	GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash]
	(
	@UserName				[nvarchar](100),
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE UserAccount
			SET PasswordHash = @NewPasswordHash
			WHERE UserName = @UserName
			AND PasswordHash = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO
print '' print '*** EXTINCTION GOAL STORED PROCEDURES ***'


--<summary>
--</summary>
--<remarks>
-- Becky Baenziger
-- 2021/04/02
-- added old and new distinction for extinction goal update
--</remarks>
print '' print '*** creating sp_update_ext_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_update_ext_goal]
	(
		@UserID_client			[int],
		@ExtGoalName			[nvarchar](50),
		@ExtGoalEntryDate		[datetime],
		@NewExtGoalDescription	[nvarchar](500),
		@NewExtGoalEditDate		[datetime],
		@NewIncidentFrequency	[int],
		@NewAwardName			[nvarchar](50)
	)
AS
	BEGIN
		UPDATE ExtinctionGoal
			SET ExtGoalDescription = @NewExtGoalDescription,
				ExtGoalEditDate = @NewExtGoalEditDate,
				IncidentFrequency = @NewIncidentFrequency,
				AwardName = @NewAwardName
			WHERE UserID_client = @UserID_client
				AND ExtGoalName = @ExtGoalName
				AND ExtGoalEntryDate = @ExtGoalEntryDate
		RETURN @@ROWCOUNT
	END
GO

---<summary>
---</summary>
---<remarks>
--- Updater Name: Becky Baenziger
--- Update Date: 2021/04/02
--- removed the edit, removal, and active from the creation of extinction goal
--- changed decription length to 500
---</remarks>
print '' print '*** creating sp_insert_new_extinction_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_ext_goal]
(
	@ExtGoalName			[nvarchar](50),
	@ExtGoalDescription		[nvarchar](500),
	@IncidentName			[nvarchar](50),
	@ExtGoalTargetDate		[DateTime],
	@ExtGoalEntryDate		[DateTime],
	@IncidentFrequency		[nvarchar](15),
	@UserID_client		    [int],
    @UserID_admin			[int],
    @AwardName				[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[ExtinctionGoal]
		  ([ExtGoalName], [ExtGoalDescription],[IncidentName],[ExtGoalTargetDate],
		  [ExtGoalEntryDate],[IncidentFrequency],[UserID_client],[UserID_admin],[AwardName])
		
		VALUES
		(@ExtGoalName,
		@ExtGoalDescription,
		@IncidentName,
		@ExtGoalTargetDate,
		@ExtGoalEntryDate,
		@IncidentFrequency,
		@UserID_client,
		@UserID_admin,
		@AwardName)
	END
GO

print '' print '*** creating sp_select_all_extinction_goals ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_extinction_goals]
AS
	BEGIN
		SELECT ExtGoalName,	ExtGoalDescription,	IncidentName, ExtGoalTargetDate, ExtGoalEntryDate, 
		ExtGoalEditDate, ExtGoalRemovalDate, Active, IncidentFrequency, UserID_client, UserID_admin, AwardName
		FROM ExtinctionGoal
		ORDER BY ExtGoalName ASC
	END
GO


-- <summary>
-- Becky Baenziger
-- 2021/04/02
--
-- Creates the stored procedure sp_deactivate_extinction_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_deactivate_extinction_goal ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_extinction_goal]
	(
		@ExtGoalName		[nvarchar](50),
		@ExtGoalEntryDate	[datetime],
		@UserID_client		[int]
	)
AS
	BEGIN
		UPDATE ExtinctionGoal
			SET Active = 0,
				ExtGoalRemovalDate = GETDATE()
			WHERE ExtGoalName = @ExtGoalName
				AND ExtGoalEntryDate = @ExtGoalEntryDate
				AND UserID_client = @UserID_client
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/04/02
--
-- Creates the stored procedure sp_reactivate_extinction_goal
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print'*** creating sp_reactivate_extinction_goal ***'
GO
CREATE PROCEDURE[dbo].[sp_reactivate_extinction_goal]
	(
		@ExtGoalName		[nvarchar](50),
		@ExtGoalEntryDate	[datetime],
		@UserID_client		[int]
	)
AS
	BEGIN
		UPDATE ExtinctionGoal
			SET Active = 1
			WHERE ExtGoalName = @ExtGoalName
				AND ExtGoalEntryDate = @ExtGoalEntryDate
				AND UserID_client = @UserID_client
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/04/02
--
-- Creates the stored procedure sp_select_extinction_goal_by_active
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_select_extinction_goal_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_extinction_goal_by_active]
	(
		@UserID_client 	[int],
		@Active			[bit]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, ExtGoalName, ExtGoalDescription, ExtGoalTargetDate, ExtGoalEntryDate, IncidentFrequency, AwardName, IncidentName, Active
		FROM 		ExtinctionGoal
		WHERE Active = @Active
			AND UserID_client = @UserID_client
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/04/02
-- 
-- Creates the stored procedure sp_select_extinction_goal_by_taget_date
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>

print '' print '*** creating sp_select_extinction_goal_by_target_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_extinction_goal_by_target_date]
	(
		@ExtGoalTargetDate		[datetime]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, ExtGoalName, ExtGoalDescription, ExtGoalTargetDate, ExtGoalEntryDate, IncidentFrequency, AwardName, IncidentName, Active
		FROM 		ExtinctionGoal
		WHERE 		ExtGoalTargetDate = @ExtGoalTargetDate
	END
GO

-- <summary>
-- Becky Baenziger
-- 2021/04/02
--
-- Creates the stored procedure sp_select_extinction_goal_by_userID_client
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating sp_select_extinction_goal_by_userID_client ***'
GO
CREATE PROCEDURE [dbo].[sp_select_extinction_goal_by_userID_client]
	(
		@UserID_client		[int]
	)
AS
	BEGIN
		SELECT		UserID_client, UserID_admin, ExtGoalName, ExtGoalDescription, ExtGoalTargetDate, ExtGoalEntryDate, IncidentFrequency, AwardName, IncidentName, Active
		FROM 		ExtinctionGoal
		WHERE 		UserID_client = @UserID_client
	END
GO
