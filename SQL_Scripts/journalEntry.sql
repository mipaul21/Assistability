-- <summary>
-- Ryan Taylor
-- Created: 2021/03/05
--
-- Script for the creation of the JournalEntries table
-- and related stored procedures
-- </summary>
--
-- <remarks>
-- Ryan Taylor
-- Updated: 2021/03/10
-- added the delete journal entry stored procedure
-- </remarks>
--
-- <remarks>
-- Ryan Taylor
-- Updated: 2021/03/14
-- added the edit journal entry stored procedure
-- </remarks>
print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/02/22
--
-- Creates the table for storing journal entries
-- </summary>
print '' print '*** creating JournalEntry table ***'
GO
CREATE TABLE [dbo].[JournalEntry]
(
	[UserID_client]				[INT]								NOT NULL,
	[UserID_ClientJournal]		[INT]								NOT NULL,
	[JournalID]					[NVARCHAR](50)						NOT NULL,
	[JournalEntryBody]			[NVARCHAR](500)						NOT NULL,
	[JournalEntryDate]			[DATETIME]							NOT	NULL
						DEFAULT GETDATE(),
	[JournalEditDate]			[DATETIME]							NOT	NULL
						DEFAULT GETDATE(),
	
	CONSTRAINT	[pk_UserID_client_JournalEntryDate]		
		PRIMARY KEY([JournalEntryDate], [UserID_client] ASC),
	CONSTRAINT	[fk_UserID_client_JournalEntry] FOREIGN KEY([UserID_client])	
		REFERENCES [dbo].[UserAccount]([UserID])
	ON UPDATE CASCADE,
	CONSTRAINT	[fk_JournalID]	FOREIGN KEY ([UserID_ClientJournal], [JournalID])
		REFERENCES [dbo].[Journal]([UserID_client], [JournalName])
)
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/02/22
--
-- Creates the test data for the journal entries table
-- </summary>
print '' print '*** creating sample JournalEntry data ***'
INSERT INTO [dbo].[JournalEntry]
		([UserID_client], [UserID_ClientJournal], [JournalID], [JournalEntryBody])
	VALUES
		(1, 1, "Test Journal", "I just want to see what this will look like.")
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/02/23
--
-- Creates the stored procedure for creating a journal entry
-- </summary>
print '' print '*** creating sp_insert_journal_entry ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_journal_entry]
(
	@UserID_client				[int],
	@UserID_ClientJournal		[int],
	@JournalID					[nvarchar](50),
	@JournalEntryBody			[nvarchar](500)
)
AS
	BEGIN
		INSERT INTO [dbo].[JournalEntry]
			([UserID_client], [UserID_ClientJournal], [JournalID], [JournalEntryBody])
		VALUES
			(@UserID_client, @UserID_ClientJournal, @JournalID, @JournalEntryBody)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/02/23
--
-- Creates the stored procedure for retrieving all journal entries related to a journal
-- </summary>
print '' print '*** creating sp_retrieve_journals_journal_entries_ ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_journals_journal_entries_]
(
	@UserID_ClientJournal		[int],
	@JournalID					[nvarchar](50)
)
AS
	BEGIN
		SELECT UserID_client, UserID_ClientJournal, 
		JournalID, JournalEntryBody, JournalEntryDate, JournalEditDate
		FROM JournalEntry
		WHERE UserID_ClientJournal = @UserID_ClientJournal
			AND JournalID = @JournalID
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/10
--
-- Creates the stored procedure for delteting a journal entry
-- </summary>
print '' print '*** creating sp_delete_one_journal_entry ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_one_journal_entry]
	(
		@UserID_client				[int],
		@JournalID					[nvarchar](50),
		@JournalEntryBody			[nvarchar](500),
		@JournalEntryDate			[DATETIME]
	)
AS
	BEGIN
		DELETE JournalEntry
			WHERE UserID_client = @UserID_client
				AND JournalID = @JournalID
				AND JournalEntryBody = @JournalEntryBody
				AND JournalEntryDate = @JournalEntryDate
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/10
--
-- Creates the stored procedure for updating a journal entry
-- </summary>
print '' print '*** creating sp_update_journal_entry ***'
GO
CREATE PROCEDURE [dbo].[sp_update_journal_entry]
	(
		@UserID_client				[INT],
		@UserID_ClientJournal		[INT],
		@JournalID					[NVARCHAR](50),
		@JournalEntryDate			[DATETIME],
		@OldJournalEntryBody		[NVARCHAR](500),
		@JournalEditDate			[DATETIME],
		
		@NewJournalEntryBody		[NVARCHAR](500)
	)
AS
	BEGIN
		UPDATE JournalEntry
			SET JournalEntryBody = @NewJournalEntryBody,
				JournalEditDate = GETDATE()
			WHERE UserID_client = @UserID_client
					AND UserID_ClientJournal = @UserID_ClientJournal
					AND JournalID = @JournalID
					AND JournalEntryDate = @JournalEntryDate
					AND JournalEntryBody = @OldJournalEntryBody
					AND JournalEditDate = @JournalEditDate
		RETURN @@ROWCOUNT
	END
GO