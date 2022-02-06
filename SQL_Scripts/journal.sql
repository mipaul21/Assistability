print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO
-- <summary>
-- Jory A. Wernette
-- Created: 2021/02/23
--
-- Creates the journal table
-- </summary>
-- <remarks>
--	Ryan Taylor 
-- Updated: 2021/03/12
--	Added my own test journal
-- </remarks>
print '' print '*** creating journal table ***'
GO
CREATE TABLE [dbo].[journal](
	[UserID_Client]			[int] NOT NULL,
	[JournalName]			[nvarchar](50) NOT NULL,
	[JournalDescription]	[nvarchar](255) NULL
	CONSTRAINT [pk_JournalName] PRIMARY KEY([UserID_client] ASC,[JournalName] ASC),
	CONSTRAINT [fk_UserID_Client] FOREIGN KEY([UserID_Client])
		REFERENCES [dbo].[UserAccount]([UserID])
)
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/02/23
--
-- Creates test data for the journal table
-- </summary>
print '' print '*** creating journal test data ***'
GO
INSERT INTO [dbo].[journal]
	([UserID_Client], [JournalName], [JournalDescription])
VALUES
	(1, 'Halloween Journal', 'My Journal all about halloween throughout October while I plan my costume and candy route!'),
	(1, 'Capstone', 'MY weekly Capstone Journal'),
	(1, 'Data Structures'   , 'A Data Structures Journal'),
	(1, 'Java 3' , 'A Java 3 Journal'),
	(1, '.NET 3' , 'A .NET 3 Journal')
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/02/22
--
-- Creates test data for journal
-- </summary>
print '' print '*** adding sample journal records ***'
GO
INSERT INTO [dbo].[Journal]
		([JournalName], [JournalDescription], [UserID_client])
	VALUES
		("Test Journal", "Nothing more than a test", 1)
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/02/23
--
-- Creates sp_insert_new_journal_by_userID stored procedure
-- </summary>
print '' print '*** creating sp_insert_new_journal_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_journal_by_userID]
	(
	@UserID_Client		  		[int],
	@JournalName		[nvarchar](50),
	@JournalDescription	[nvarchar](255)
	)
AS 
	BEGIN
		INSERT INTO [dbo].[journal]
			([UserID_Client], [JournalName], [JournalDescription])
		VALUES
			(@UserID_Client, @JournalName, @JournalDescription)
	END
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/03/03
--
-- Creates sp_delete_journal_by_userID_and_journalName stored procedure
-- </summary>
print '' print '*** creating sp_delete_journal_by_userID_and_journalName ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_journal_by_userID_and_journalName]
	(
	@UserID_Client		  		[int],
	@JournalName				[nvarchar](50)
	)
AS 
	BEGIN
		DELETE FROM Journal
		WHERE UserID_Client = @UserID_Client
		AND JournalName = @JournalName
	END
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/03/03
--
-- Creates sp_select_journal_by_userID_and_journalName stored procedure
-- </summary>
print '' print '*** creating sp_select_journal_by_userID_and_journalName ***'
GO
CREATE PROCEDURE [dbo].[sp_select_journal_by_userID_and_journalName]
	(
	@UserID_Client		  		[int],
	@JournalName				[nvarchar](50)
	)
AS 
	BEGIN
		SELECT JournalName, JournalDescription
		FROM Journal
		WHERE UserID_Client = @UserID_Client
		AND JournalName = @JournalName
	END
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/02/23
--
-- Creates sp_select_journals_by_userID stored procedure
-- </summary>
print '' print '*** creating sp_select_journals_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_journals_by_userID]
	(
	@UserID_Client		  		[int]
	)
AS 
	BEGIN
		SELECT JournalName, JournalDescription
		FROM Journal
		WHERE UserID_Client = @UserID_Client
	END
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/02/23
--
-- Creates sp_select_all_journals stored procedure
-- </summary>
print '' print '*** creating sp_select_all_journals ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_journals]
AS 
	BEGIN
		SELECT JournalName, JournalDescription, UserID_Client
		FROM Journal
	END
GO

-- <summary>
-- Jory A. Wernette
-- Created: 2021/02/23
--
-- Creates sp_update_journal stored procedure
-- </summary>
print '' print '*** creating sp_update_journal ***'
GO
CREATE PROCEDURE [dbo].[sp_update_journal]
	(
	@UserID						[int],
	@NewJournalName				[nvarchar](50),
	@NewJournalDescription		[nvarchar](255),
	@OldJournalName				[nvarchar](50),
	@OldJournalDescription		[nvarchar](255)
	)
AS
	BEGIN
		UPDATE Journal
			SET JournalName = @NewJournalName,
				JournalDescription = @NewJournalDescription
			WHERE UserID_Client = @UserID
			AND JournalName = @OldJournalName
			AND JournalDescription = @OldJournalDescription
		RETURN @@ROWCOUNT
	END
GO



