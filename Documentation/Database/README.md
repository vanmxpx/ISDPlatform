# Oracle DB tuning guide

This guide helps to configure Oracle DB for Cooper project.

## Installation Oracle Database 18c and creation of connection to a database

1. Download [Oracle Database 18c](https://www.oracle.com/technetwork/database/enterprise-edition/downloads/index.html).
2. Install Oracle Database 18c by [this video](https://www.youtube.com/watch?v=CrTo_XoDQwI).
3. Download [SQL Developer](https://www.oracle.com/technetwork/developer-tools/sql-developer/downloads/index.html) and run it.
4. Click on green "+" to create a new connection.
![New connection](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/ScreenshotsForGuide/new_connection.png)
5. In the window for creating a new connection, fill in all the fields as in the screenshot.
<br/>**Note: In the password field enter the password that you specified during installation Oracle Database 18c.**
![Create connection](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/ScreenshotsForGuide/create_connection.png)
6. Click on the connect button at the bottom of the window.

**Congratulations, you have created the database connection:)**

## Creating tables and adding data in these table

1. Click on the "+" on the left of the newly created database connection.
2. After connecting to the database you will have a worksheet. You must insert [this query](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/Cooper.Sql) into this worksheet and press Cltr + Enter to run script.
<br/>**Note: If you have a error *"The ORA-01858 error "a non-numeric character was located where a digit was expected."*, go to *Tools>Preferences* and enter "nls" in the search. After that, configure everything as in the screenshot below.**
![Change NLS](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/ScreenshotsForGuide/change_nls.png)
**After configuring the NLS, delete all tables with the query below and execute [this query](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/Cooper.Sql) one more time.**
```sql
DROP TABLE USERSGAMES;
DROP TABLE CREATORSGAMES;
DROP TABLE USERSCONNECTIONS;
DROP TABLE GAMESSTATISTICS;
DROP TABLE GAMESREVIEWS;
DROP TABLE USERSREVIEWS;
DROP TABLE USERSCHATS;
DROP TABLE GAMES;
DROP TABLE MESSAGES;
DROP TABLE CHATS;
DROP TABLE USERS;
```
3. You have created the necessary tables, you are well done. But now you need to fill them with data. To do this, simply run [this query](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/InsertBasicInfo.Sql).

**Congratulations, you have created the tables and added some data to them:)**

## Adding an environment variable

For specify a password in the connection string, we use environment variables.

1. Open the Start Search, type in "env", and choose "Edit the system environment variables".
2. Click the "Environment Variables…" button.
3. Now you can add a user environment variable to Windows with the name "Coop" and the password that you specified during installation Oracle Database 18c.
![New connection](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/ScreenshotsForGuide/add_environment_variable.png)
