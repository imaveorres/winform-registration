--/// <summary>
--/// Coded by       : Kim Dave Torres
--/// Activity Title : User Registration
--/// Subject        : DBMS
--/// </summary>

CREATE DATABASE UserRegistration
USE UserRegistration

CREATE TABLE Users (
	UserID int IDENTITY(1,1) NOT NULL,
	ProfileImage Image NOT NULL,
	Firstname varchar(50) NOT NULL,
	Lastname varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	Username varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	PRIMARY KEY (UserID)
);


