# Oracle DB tuning guide

This guide helps to configure Oracle DB for Cooper project.

1. Download [Oracle Database 18c](https://www.oracle.com/technetwork/database/enterprise-edition/downloads/index.html).
2. Install Oracle Database 18c by [this video](https://www.youtube.com/watch?v=CrTo_XoDQwI).
3. Download [SQL Developer](https://www.oracle.com/technetwork/developer-tools/sql-developer/downloads/index.html).
4. Click on green "+" to create a new connection.
<br/><br/>![New connection](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/ScreenshotsForGuide/new_connection.png)<br/>
5. In the window for creating a new connection, fill in all the fields as in the screenshot.
**Note: In the password field enter the password that you specified during installation Oracle Database 18c.**
<br/><br/>![Create connection](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/ScreenshotsForGuide/create_connection.png)<br/>
6. Click on the connect button at the bottom of the window.

## Congratulations, you have created a database connection:)

For specify a password in the connection string, we use environment variables. Now you can add a user environment variable to Windows with the name "Coop" and the password that you specified during installation Oracle Database 18c.
<br/><br/>![New connection](https://github.com/vanmxpx/ISDPlatform/blob/feature/OracleDBTuningGuide/Documentation/Database/ScreenshotsForGuide/add_environment_variable.png)<br/>
