--/// <summary>
--/// Coded by       : Kim Dave Torres
--/// Activity Title : User Registration
--/// Subject        : DBMS
--/// </summary>

USE UserRegistration
GO

--insert user
CREATE PROCEDURE sp_insert
	@ProfileImage Image,
	@Firstname varchar(50),
	@Lastname varchar(50),
	@email varchar(50),
	@Username varchar(50),
	@Password varchar(50)
AS
	INSERT INTO Users VALUES (
		@ProfileImage,
		@Firstname,
		@Lastname,
		@email,
		@Username,
		@Password
	)
GO --end

--update user
CREATE PROCEDURE sp_update
	@UserID int,
	@ProfileImage Image,
	@Firstname varchar(50),
	@Lastname varchar(50),
	@email varchar(50),
	@Username varchar(50),
	@Password varchar(50)
AS
	UPDATE Users 
	SET
		ProfileImage=@ProfileImage,
		Firstname=@Firstname,
		Lastname=@Lastname,
		email=@email,
		Username=@Username,
		Password=@Password
	WHERE UserID=@UserID
GO --end

--user delete
CREATE PROCEDURE sp_delete
	@UserID int
AS
	DELETE FROM Users WHERE UserID=@UserID
GO --end

--user search
CREATE PROCEDURE sp_search
	@keywords varchar(max)
AS
	SELECT * FROM Users WHERE Firstname LIKE '%'+@keywords+'%' OR Lastname LIKE '%'+@keywords+'%'
GO --end

--view user
CREATE PROCEDURE sp_user_view 
AS
	SELECT * FROM Users
GO--end

