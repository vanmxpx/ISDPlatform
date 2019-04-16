/*=================================================*/
/*            ADDING BASIC INFO                  */
/*=================================================*/



/*============= USERS ================*/

INSERT into users (name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
('Andrew', 'mew_boy', 'pugach@dlit.dp.ua', 'qwertynya', 'photoURL', 'y', 'n', 'n', '13-Jan-2000', 'English', 'light');

INSERT into users (name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
('Nikita Tsyhankov', 'vanmxpx', 'nnn@dlit.dp.ua', 'qwertynya2', 'photoURL', 'y', 'y', 'n', '13-Jan-2000', 'English', 'Dark');

INSERT into users (name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
('Anton Soroka', 'hunroll', 'aaa@dlit.dp.ua', 'qwertynya3', 'photoURL', 'y', 'y', 'n', '13-Jan-2000', 'English', 'Light');


INSERT into users (name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
('Nikita Borovinsky', 'NikBororvinsS', 'nik@dlit.dp.ua', 'qwertynya3', 'photoURL', 'n', 'n', 'n', '13-Jan-2000', 'English', 'Dark');

INSERT into users (name, nickname, email, password, photoURL, isVerified, isCreator, isBanned, endBanDate, PlatformLanguage, PlatformTheme)
VALUES
('Misha Maslov', 'maslowind', 'maslov@dlit.dp.ua', 'qwertynya3', 'photoURL', 'n', 'n', 'n', '13-Jan-2000', 'English', 'Dark');



/*============= GAMES ================*/
INSERT INTO GAMES(NAME, DESCRIPTION, GENRE, LINK, LOGOURL, COVERURL, ISVERIFIED)
VALUES('Heroes of Might and Magic 3', 'Popular worldwide strategy game that conquered the hearts of many gamers.', 'Strategy', 'link', 'logoURL', 'coverURL', 'y');