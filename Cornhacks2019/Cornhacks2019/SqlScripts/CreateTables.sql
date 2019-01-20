-- Uncomment the following line the first time you run this script: 
-- CREATE DATABASE Cornhacks; 

USE Cornhacks; 

CREATE TABLE User (
UserId INT auto_increment primary key,
Email nvarchar(255),
Password nvarchar(255),
IsBeginner bool
);

CREATE TABLE Language (
LanguageId INT auto_increment primary key,
LanguageName nvarchar(255)
);

CREATE TABLE Topic (
TopicId INT auto_increment primary key,
TopicName nvarchar(255)
);

CREATE TABLE Size (
SizeId INT auto_increment primary key,
SizeName nvarchar(255)
);

CREATE TABLE UserLanguage (
UserId INT,
LanguageId INT,
FOREIGN KEY (UserId) REFERENCES User (UserId),
FOREIGN KEY (LanguageId) REFERENCES Language (LanguageId),
PRIMARY KEY (UserId, LanguageId)
);

CREATE TABLE UserTopic (
UserId INT,
TopicId INT,
FOREIGN KEY (UserId) REFERENCES User (UserId),
FOREIGN KEY (TopicId) REFERENCES Topic (TopicId),
PRIMARY KEY (UserId, TopicId)
);

CREATE TABLE UserSize (
UserId INT,
SizeId INT,
FOREIGN KEY (UserId) REFERENCES User (UserId),
FOREIGN KEY (SizeId) REFERENCES Size (SizeId),
PRIMARY KEY (UserId, SizeId)
);
