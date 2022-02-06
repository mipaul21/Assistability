GO
USE [assistability_db]
GO

-- <remarks>
-- Updater Name: Jory A. Wernette
-- Update Date: 2021/04/29
-- added sp_get_every_award so the admin can see awards active and not
-- </remarks>

-- <remarks>
-- Updater Name: Jory A. Wernette
-- Update Date: 2021/04/29
-- added sp_safely_reactivate_award so the admin can reactivate awards
-- </remarks>


-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Creates the award table
-- </summary>
print ''  print '' print '*** creating award table ***'
GO
CREATE TABLE [dbo].[award] 
(
	[AwardName]			[NVARCHAR](50)						NOT NULL,
	[AwardDescription]	[NVARCHAR](255)						NOT NULL,
	[Active]			[BIT]				DEFAULT(1)		NULL
	
	CONSTRAINT [pk_AwardName]			PRIMARY KEY([AwardName] ASC)
)
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create test data for award table
-- </summary>
-- print '' print '*** creating award test data ***'
-- GO
-- INSERT INTO [dbo].[award]
-- 	(UserID_Award, AwardName, AwardDescription, GoalID, GoalTypeID)
-- VALUES
-- 	(1, "Award number 1", "A short description", 1, 1),
-- 	(1, "Award number 2", "A longer description", 1, 2),
-- 	(1, "Award number 3", "The longest description", 2, 1),
-- 	(1, "Award number 4", "The shortest Description", 2, 2)
-- GO

print '' print '*** CREATING STORED PROCEDURES FOR AWARD ***'
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create stored procedure sp_create_new_award
-- </summary>
print '' print '*** Creating sp_create_new_award ***'
GO
CREATE PROCEDURE [dbo].[sp_create_new_award]
	(
		@AwardName				[NVARCHAR](50),
		@AwardDescription		[NVARCHAR](255)
	)
AS
	BEGIN
		INSERT INTO [dbo].[award]
			([AwardName], [AwardDescription])
		VALUES
			(@AwardName, @AwardDescription)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create stored procedure sp_delete_award
-- </summary>
print '' print '*** Creating sp_delete_award ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_award]
	(
		@AwardName		[NVARCHAR](50)
	)
AS
	BEGIN
		DELETE FROM award
			WHERE AwardName = @AwardName
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create stored procedure sp_safely_deactivate_award
-- </summary>
print '' print '*** Creating sp_safely_deactivate_award ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_deactivate_award]
	(
		@AwardName		[NVARCHAR](50)
	)
AS
	BEGIN
		UPDATE award
			SET Active = 0
			WHERE AwardName = @AwardName
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/04/29
--
-- Create stored procedure sp_safely_reactivate_award
-- </summary>
print '' print '*** Creating sp_safely_reactivate_award ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_reactivate_award]
	(
		@AwardName		[NVARCHAR](50)
	)
AS
	BEGIN
		UPDATE award
			SET Active = 1
			WHERE AwardName = @AwardName
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/04/29
--
-- Create stored procedure sp_select_every_award
-- </summary>
print '' print '*** Creating sp_select_every_award ***'
GO
CREATE PROCEDURE [dbo].[sp_select_every_award]
AS
	BEGIN
		SELECT AwardName, AwardDescription, Active
		FROM award
		ORDER BY AwardName ASC
	END
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create stored procedure sp_select_all_awards
-- </summary>
print '' print '*** Creating sp_select_all_awards ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_awards]
	(	
		@Active		[BIT]
	)
AS
	BEGIN
		SELECT AwardName, AwardDescription, Active
		FROM award
		WHERE Active = @Active
		ORDER BY AwardName ASC
	END
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create stored procedure sp_select_award_by_awardID
-- </summary>
print '' print '*** Creating sp_select_award_by_award_name ***'
GO
CREATE PROCEDURE[dbo].[sp_select_award_by_award_name]
	(
		@AwardName		[NVARCHAR](50)
	)
AS
	BEGIN
		SELECT AwardName, AwardDescription, Active
		FROM award
		WHERE AwardName = @AwardName
	END
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create stored procedure sp_update_award
-- </summary>
print '' print '*** Creating sp_update_award ***'
GO
CREATE PROCEDURE [dbo].[sp_update_award]
	(
		@AwardName				[NVARCHAR](50),
		@NewAwardDescription	[NVARCHAR](255),
		@OldAwardDescription	[NVARCHAR](255)
	)
AS
	BEGIN
		UPDATE award
			SET AwardDescription = @NewAwardDescription
			WHERE AwardName = @AwardName
			AND AwardDescription = @OldAwardDescription
		RETURN @@ROWCOUNT
	END
GO

print ''

















