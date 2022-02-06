print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Nathaniel Webber
-- Created: 2021/03/11
--
-- Create test data for award table
-- </summary>
print '' print '*** creating award test data ***'
GO
INSERT INTO [dbo].[award]
	(AwardName, AwardDescription, Active)
VALUES
	("Award number 1", "A short description", 1),
	("Award number 2", "A longer description", 1),
	("Award number 3", "The longest description", 1),
	("Award number 4", "The shortest Description", 1)
GO