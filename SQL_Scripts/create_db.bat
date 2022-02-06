ECHO off

sqlcmd -S %1 -E -i assistability_db.sql
sqlcmd -S %1 -E -i useraccount.sql
sqlcmd -S %1 -E -i incident.sql
sqlcmd -S %1 -E -i incidentEvent.sql
sqlcmd -S %1 -E -i role.sql
sqlcmd -S %1 -E -i usergroup.sql
sqlcmd -S %1 -E -i membership.sql
sqlcmd -S %1 -E -i membershiprole.sql
sqlcmd -S %1 -E -i routines.sql
sqlcmd -S %1 -E -i routinecompletion.sql
sqlcmd -S %1 -E -i routinestep.sql
sqlcmd -S %1 -E -i routinestepcompletion.sql
sqlcmd -S %1 -E -i performance.sql
sqlcmd -S %1 -E -i performanceEvent.sql
sqlcmd -S %1 -E -i test.sql
sqlcmd -S %1 -E -i journal.sql
sqlcmd -S %1 -E -i journalEntry.sql
sqlcmd -S %1 -E -i usergroup-testdata.sql
sqlcmd -S %1 -E -i membership-testdata.sql
sqlcmd -S %1 -E -i membershiprole-testdata.sql
sqlcmd -S %1 -E -i award.sql
sqlcmd -S %1 -E -i award-testdata.sql
sqlcmd -S %1 -E -i reward.sql
sqlcmd -S %1 -E -i reward-testdata.sql
sqlcmd -S %1 -E -i habitualgoal.sql
sqlcmd -S %1 -E -i habitualgoaltestdata.sql
sqlcmd -S %1 -E -i attainmentgoal.sql
sqlcmd -S %1 -E -i attainmentgoaltestdata.sql
sqlcmd -S %1 -E -i extinctionGoal.sql
sqlcmd -S %1 -E -i extinctiongoaltestdata.sql

ECHO .
ECHO if no errors appear DB was created
PAUSE