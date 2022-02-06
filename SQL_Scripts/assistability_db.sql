/* Check whether database exists and drop it if it does */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE NAME = 'assistability_db')

BEGIN
	DROP DATABASE assistability_db
	print '' print '*** dropping database assistability_db ***'
END
GO

print '' print '*** creating assisstability_db ***'
GO
CREATE DATABASE assistability_db
GO
