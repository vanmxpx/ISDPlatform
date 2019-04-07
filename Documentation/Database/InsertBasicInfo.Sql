
/*=================================================*/
/*            ADDING BASIC INFO                  */
/*=================================================*/



/*============= USERS ================*/

INSERT into users (id, name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
(1, 'Andrew', 'mew_boy', 'pugach@dlit.dp.ua', 'qwertynya', 'photoURL', 'y', 'n', 'n', '13/01/2000', 'English', 'light');

INSERT into users (id, name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
(2, 'Nikita Tsyhankov', 'vanmxpx', 'nnn@dlit.dp.ua', 'qwertynya2', 'photoURL', 'y', 'y', 'n', '13/01/2000', 'English', 'Dark');

INSERT into users (id, name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
(3, 'Anton Soroka', 'hunroll', 'aaa@dlit.dp.ua', 'qwertynya3', 'photoURL', 'y', 'y', 'n', '13/01/2000', 'English', 'Light');


INSERT into users (id, name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
(4, 'Nikita Borovinsky', 'NikBororvinsS', 'nik@dlit.dp.ua', 'qwertynya3', 'photoURL', 'n', 'n', 'n', '13/01/2000', 'English', 'Dark');

INSERT into users (id, name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
(5, 'Misha Maslov', 'maslowind', 'maslov@dlit.dp.ua', 'qwertynya3', 'photoURL', 'n', 'n', 'n', '13/01/2000', 'English', 'Dark');



/*============= GAMES ================*/
INSERT INTO GAMES(ID, NAME, DESCRIPTION, GENRE, LINK, LOGOURL, COVERURL, ISVERIFIED)
VALUES(1, 'Heroes of Might and Magic 3', 'Popular worldwide strategy game that conquered the hearts of many gamers.', 'Strategy', 'link', 'logoURL', 'coverURL', 'y');