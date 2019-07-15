DELETE FROM GAMES WHERE ID=1;

INSERT INTO GAMES(NAME, DESCRIPTION, GENRE, LINK, LOGOURL, COVERURL, ISVERIFIED)
VALUES('WebTanks', 'Popular worldwide strategy game that conquered the hearts of many gamers.', 'Action', '/game/tanks/', 'logoURL', 'coverURL', 'y');

INSERT INTO GAMES(NAME, DESCRIPTION, GENRE, LINK, LOGOURL, COVERURL, ISVERIFIED)
VALUES('Extreme Island', 'Really extremal and survival game that makes you spend a lot of time in order to become a winner', 'Action', '/game/islands/', 'logoURL', 'coverURL', 'y');
