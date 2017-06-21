

SET SQLCMD="C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE"

SET PATH=%1
SET SERVER=%2
SET DB=%3

SET "Database=DatabaseScripts\CreateDatabase"
SET "Tables=DatabaseScripts\Tables"
SET "StoredProcedures=DatabaseScripts\StoredProcedures"

SET "PATHCreateDb=%PATH%%Database%"
SET "PATHCreateTables=%PATH%%Tables%"
SET "PATHCreateStoredProcedures=%PATH%%StoredProcedures%"

ECHO.%PATHCreateDb%
ECHO.%PATHCreateTables%
ECHO.%PATHCreateStoredProcedures%
ECHO.%SERVER%
ECHO.%DB%

CD %PATHCreateDb%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -E -i %%~f 
IF %ERRORLEVEL% EQU 0 ECHO File %%~f successfully executed
IF %ERRORLEVEL% NEQ 0 ECHO Error while executing %%~f
)

CD %PATHCreateTables%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -d %DB% -E -i %%~f 
IF %ERRORLEVEL% EQU 0 ECHO File %%~f successfully executed
IF %ERRORLEVEL% NEQ 0 ECHO Error while executing %%~f
)

CD %PATHCreateStoredProcedures%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -d %DB% -E -i %%~f 
IF %ERRORLEVEL% EQU 0 ECHO File %%~f successfully executed
IF %ERRORLEVEL% NEQ 0 ECHO Error while executing %%~f
)
