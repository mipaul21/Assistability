print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/18
--
-- Create test data for reward table
-- </summary>
print '' print '*** creating reward test data ***'
GO
INSERT INTO [dbo].[reward]
	(RewardName, RewardDescription, UserID)
VALUES
	("Test Reward 1", "Test Description 1", 1),
	("Test Reward 2", "Test Description 2", 1),
	("Test Reward 3", "Test Description 3", 1)
GO