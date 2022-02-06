GO
USE [assistability_db]
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/18
--
-- Creates the reward table
-- </summary>
print ''  print '' print '*** creating reward table ***'
GO
CREATE TABLE [dbo].[reward]
(
	[RewardID]				[INT]				IDENTITY(1,1)	NOT NULL,
	[RewardName]			[NVARCHAR](50)						NOT NULL,
	[RewardDescription]		[NVARCHAR](255)						NOT NULL,
	[UserID]				[INT]								NOT NULL,
	[Active]				[BIT]				DEFAULT(1)		NULL
	
	CONSTRAINT [pk_RewardID]		PRIMARY KEY([RewardID] ASC),
	CONSTRAINT [fk_UserID_Reward]	FOREIGN KEY([UserID])			REFERENCES[dbo].[UserAccount]([UserID])
)
GO

print '' print '*** CREATING STORED PROCEDURES FOR REWARD ***'
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/18
--
-- Create stored procedure sp_select_all_rewards
-- </summary>
print '' print '*** Creating sp_select_all_rewards ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_rewards]
	(
		@UserID		[INT]
	)
AS
	BEGIN
		SELECT RewardID, RewardName, RewardDescription, UserID, Active
		FROM reward
		WHERE UserID = @UserID
		ORDER BY RewardName ASC
	END
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/25
--
-- Create stored procedure sp_create_new_reward
-- </summary>
print '' print '*** Creating sp_create_new_reward ***'
GO
CREATE PROCEDURE [dbo].[sp_create_new_reward]
	(
		@UserID				[INT],
		@RewardName			[NVARCHAR](50),
		@RewardDescription	[NVARCHAR](255)
	)
AS
	BEGIN
		INSERT INTO [reward]
			([UserID], [RewardName], [RewardDescription])
		VALUES
			(@UserID, @Rewardname, @RewardDescription)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/01/04
--
-- Creates sp_update_reward stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_update_reward ***'
GO
CREATE PROCEDURE [dbo].[sp_update_reward]
	(
		@RewardID        		     		[INT],
		@OldUserID							[INT],
	    @NewRewardName						[NVARCHAR](50),	
		@NewRewardDescription				[NVARCHAR](255)
	)
AS
	BEGIN
		UPDATE reward
			SET 
				RewardName = @NewRewardName,
				RewardDescription = @NewRewardDescription
			WHERE RewardID = @RewardID
				AND UserID = @OldUserID
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/04/08
--
-- Creates sp_deactivate_reward stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_deactivate_reward ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_reward]
	(
		@UserID					[INT],
		@RewardName				[NVARCHAR](50),
		@RewardID				[INT]
	)
AS
	BEGIN
		UPDATE reward
			SET Active = 0
			WHERE @UserID = UserID
			AND		@RewardName = RewardName
			AND		@RewardID = RewardID
			RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/04/08
--
-- Creates sp_reactivate_reward stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_reactivate_reward ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_reward]
	(
		@UserID					[INT],
		@RewardName				[NVARCHAR](50),
		@RewardID				[INT]
	)
AS
	BEGIN
		UPDATE reward
			SET Active = 1
			WHERE @UserID = UserID
			AND		@RewardName = RewardName
			AND		@RewardID = RewardID
			RETURN @@ROWCOUNT
	END
GO
