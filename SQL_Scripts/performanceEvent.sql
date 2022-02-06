-- <summary>
-- Whitney Vinson
-- Created: 2021/03/29
--
-- Script for the creation of the PerformanceEvent table
-- and related stored procedures
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/03/29
--
-- Definition of the PerformanceEvent table.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating Performance Event table ***'
GO
CREATE TABLE [dbo].[PerformanceEvent](
	[PerformanceEventID]		[int] IDENTITY(1, 1) 	NOT NULL	
	, [PerformanceName]			[nvarchar](50)			NOT NULL
	, [DateOfOccurance]			[datetime]				NOT NULL
	, [EventDescription]		[nvarchar](500)			NOT NULL	
	, [EventResult]				[nvarchar](250)			NOT NULL
	, [EventEditDate]			[datetime]				NULL		
	, [UserIDClient]			[int]					NOT NULL  
	, [UserIDReporter]			[int]					NOT NULL
	, CONSTRAINT [pk_performanceEvent] PRIMARY KEY([PerformanceEventID] ASC)
	, CONSTRAINT [fk_PerformanceName] FOREIGN KEY([UserIDClient],[PerformanceName])
		REFERENCES [dbo].[Performance]([UserID_client],[PerformanceName])
	, CONSTRAINT [fk_UserIDClient] FOREIGN KEY([UserIDClient])
		REFERENCES [dbo].[UserAccount]([UserID])
	, CONSTRAINT [fk_UserIDReporter] FOREIGN KEY([UserIDReporter])
		REFERENCES [dbo].[UserAccount]([UserID])
)
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/03/29
--
-- The stored procedure for 
-- inserting a PerformanceEvent.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_insert_performance_event ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_performance_event]
	(
		@PerformanceName			[nvarchar](50)
		, @DateOfOccurance			[datetime]
		, @EventDescription			[nvarchar](500)
		, @EventResult				[nvarchar](250)
		, @UserIDClient				[int]
		, @UserIDReporter			[int]
		
	)
AS
	BEGIN
		INSERT INTO [dbo].[PerformanceEvent] (
				[PerformanceName],
				[DateOfOccurance], 
				[EventDescription], 
				[EventResult],
				[UserIDClient],
				[UserIDReporter]
				)
		VALUES (
				@PerformanceName,
				@DateOfOccurance,
				@EventDescription, 
				@EventResult,
				@UserIDClient,
				@UserIDReporter
				)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/03/29
--
-- The stored procedure for 
-- updating a PerformanceEvent.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_update_performance_event ***'
GO
CREATE PROCEDURE [dbo].[sp_update_performance_event]
	(
	  @PerformanceEventID		[int]
	, @NewEventDescription		[nvarchar](500)
	, @NewEventResult			[nvarchar](250)	
	, @NewEventEditDate			[datetime]
	, @OldEventDescription		[nvarchar](500)
	, @OldEventResult			[nvarchar](250)	
	, @OldEventEditDate			[datetime]
	)
AS
	BEGIN
		UPDATE PerformanceEvent
			SET EventDescription = @NewEventDescription,
				EventResult = @NewEventResult,
				EventEditDate = @NewEventEditDate
			WHERE PerformanceEventID = @PerformanceEventID
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/03/29
--
-- The stored procedure for 
-- selecting all PerformanceEvents
-- by user.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_select_all_performance_events_by_UserIDClient ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_performance_events_by_UserIDClient]
(
	@UserIDClient [int]
)
AS
	BEGIN
		SELECT
			PerformanceEventID
			, PerformanceName
			, DateOfOccurance
			, EventDescription
			, EventResult
			, EventEditDate
			, UserIDClient
			, UserIDReporter
		FROM PerformanceEvent
		WHERE UserIDClient = @UserIDClient
		ORDER BY PerformanceEventID ASC
	END
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/03/29
--
-- The stored procedure for 
-- selecting all PerformanceEvents
-- by PerformanceName.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_select_all_performance_events_by_performance_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_performance_events_by_performance_name]
(
	@PerformanceName [nvarchar](50)
)
AS
	BEGIN
		SELECT
			PerformanceEventID
			, PerformanceName
			, DateOfOccurance
			, EventDescription
			, EventResult
			, EventEditDate
			, UserIDClient
			, UserIDReporter
		FROM PerformanceEvent
		WHERE PerformanceName = @PerformanceName
		ORDER BY PerformanceEventID ASC
	END
GO