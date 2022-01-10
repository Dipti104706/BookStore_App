create database BookStore
use BookStore
---------------------------
CREATE TABLE Users(
	UserId int Identity(1,1) PRIMARY KEY,
	Name varchar (200),
	Email varchar (200),
	Password varchar(50),
	Phone bigint
	);

select * from Users 
-----------------------------------

Alter procedure Sp_AddCustomers    
(   
    @Name VARCHAR(200),    
    @Email VARCHAR(200),   
    @Password VARCHAR(50),   
    @Phone bigint   
)   
as    
Begin 
	IF (select UserId from Users where Email=@Email) is not null 
	begin
		select 1;
	end
	else
	begin   
		Insert into Users (Name,Email,Password, Phone)    
		Values (@Name,@Email,@Password, @Phone) 
	end   
End

-------------------Login--------------------
Alter procedure spGetAllDetails  (    
    @Email VARCHAR(200),
	@Password VARCHAR(50)
)   
as   
Begin   
    select * from Users WHERE Email = @Email and Password = @Password
End

--------------Forgot ps---------------------------
create procedure spForgotPS (
 @Email VARCHAR(200)
)   
as   
Begin  
	IF Exists(select * from Users where Email = @Email) 
	begin
		select 1
	end
	Else
	begin
		select 0
	end
End

-------------Reset----------------
alter procedure spResetPS (
 @Email VARCHAR(200),
 @NewPs VARCHAR(50)
)   
as   
Begin  
	IF Exists(select * from Users where Email = @Email) 
	begin
		update [Users] set [Password]= @NewPs where Email = @Email;
	end
	Else
	begin
		select 1
	end
End

--------Table book details----------------------

create TABLE BookDetails(
	BookId int Identity(1,1) PRIMARY KEY,
	BookName varchar(200) not null,
	AuthorName varchar(200) not null,
    DiscountPrice money not null,   
	OriginalPrice  money not null,            
    BookDescription nvarchar(max),
    Rating float default 0,
    Reviewer int default 0 ,
    Image varchar(max)not null,
	BookCount int not null
	);

select * from BookDetails 

ALTER TABLE BookDetails
ALTER COLUMN BookCount int

--------------Insert book------------------
create procedure Sp_AddBooks   
(   
    @BookName VARCHAR(200),    
    @AuthorName varchar(200),
    @DiscountPrice money ,   
	@OriginalPrice  money ,            
    @BookDescription nvarchar(max),
    @Rating float ,
    @Reviewer int  ,
    @Image varchar(max),
	@BookCount int    
)   
as    
Begin 
	Begin try   
		BEGIN TRANSACTION;
		Insert into BookDetails (BookName,AuthorName,DiscountPrice,OriginalPrice,BookDescription,Rating,Reviewer,Image,BookCount)    
		Values (@BookName,@AuthorName,@DiscountPrice,@OriginalPrice,@BookDescription,@Rating,@Reviewer,@Image,@BookCount) 
		COMMIT TRANSACTION; 
	End try
	Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;  
		ROLLBACK TRANSACTION;  
	End catch  
End

Exec Sp_AddBooks 'My life','Ramcharan',150,300,'Based on true events',0,0,'jujhujjh/url',5

----------------Get books-------------------------
alter PROCEDURE spGetBookDetails
  @BookId int
AS
BEGIN
 Begin try
     IF(EXISTS(SELECT * FROM BookDetails WHERE BookId=@BookId))
	 begin
	   SELECT * FROM BookDetails WHERE BookId=@BookId;
   	 end
 End try
 Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;    
 End catch 
End

-----------delete book-------------
create PROCEDURE spDeleteBookDetails
  @BookId int
AS
BEGIN
 Begin try
     IF(EXISTS(SELECT * FROM BookDetails WHERE BookId=@BookId))
	 begin
	   delete from BookDetails WHERE BookId=@BookId;
   	 end
	 else
	   Select 1;
 End try
 Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;    
 End catch 
End
-------------------Update book-------------------------
create procedure sp_UpdateBooks   
( 
	@BookId int,
    @BookName VARCHAR(200),    
    @AuthorName varchar(200),
    @DiscountPrice money ,   
	@OriginalPrice  money ,            
    @BookDescription nvarchar(max),
    @Rating float ,
    @Reviewer int  ,
    @Image varchar(max),
	@BookCount int    
)   
as    
Begin 
	Begin try   
		BEGIN TRANSACTION;
		IF Exists(select * from BookDetails where BookId = @BookId) 
		begin
			update BookDetails set 
			BookName= @BookName ,
			AuthorName=@AuthorName,
			DiscountPrice=@DiscountPrice,
			OriginalPrice=@OriginalPrice,
			BookDescription=@BookDescription,
			Rating=@Rating,
			Reviewer=@Reviewer,
			Image=@Image,
			BookCount=@BookCount			
			where BookId = @BookId;
		end
		else
		begin
			Select 1;
		end
		COMMIT TRANSACTION; 
	End try
	Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;  
		ROLLBACK TRANSACTION;  
	End catch  
End

----------Get all book---------------
create procedure spGetAllBook  
as   
Begin  
	select * from BookDetails
End

----------------------------------------
----------------cart table-------------
CREATE TABLE CartDetails
(
	CartID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES BookDetails(BookId),	
	OrderQuantity INT default 1
);

select * from CartDetails

------------Add b0ok to cart -----------
Alter PROCEDURE spAddingCart(
	@UserId INT,
	@BookId INT)
AS
BEGIN
	IF (EXISTS(SELECT * FROM BookDetails WHERE BookId=@BookId))		
	begin
		INSERT INTO CartDetails( UserId,BookId)
		VALUES (@UserId,@BookId)
	end
	else
	begin 
		select 2
	end
END

Exec spAddingCart 3,2
Exec spAddingCart 2,2

-------------Update Quantity in cart details-------
CREATE PROC spUpdateQuantity
	@CartID INT,
	@OrderQuantity INT
AS
BEGIN
	IF (EXISTS(SELECT * FROM CartDetails WHERE CartID = @CartID))
	BEGIN
			UPDATE CartDetails
			SET
				OrderQuantity = @OrderQuantity
			WHERE
				CartID = @CartID;
		END
		ELSE
		BEGIN
			Select 1;
		END
END

--------------get cart details------
CREATE PROCEDURE spGetCartDetails
	@UserId INT
AS
BEGIN
	SELECT
		CartDetails.CartID,
		CartDetails.UserId,
		CartDetails.BookId,
		CartDetails.OrderQuantity,	
		BookDetails.BookName,
		BookDetails.AuthorName,
		BookDetails.DiscountPrice,
		BookDetails.OriginalPrice  
	FROM CartDetails 
	Inner JOIN BookDetails ON CartDetails.BookId = BookDetails.BookId
	WHERE CartDetails.UserId = @UserId
END

--------------Delete cart---------
CREATE PROCEDURE spDeleteCartDetails
	@CartID INT
AS
BEGIN
	IF EXISTS(SELECT * FROM CartDetails WHERE CartID = @CartID)
	BEGIN
		DELETE FROM CartDetails WHERE CartID = @CartID
	END
	ELSE
	BEGIN
		select 1
	END
END

Exec spDeleteCartDetails 4

------------Address table------
create table AddressTable
(
    AddressId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
	Address varchar(max) not null,
	City varchar(100),
	State varchar(100),
	TypeId int
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId)
);

------------Address type table-------
create table AddressType
(
	TypeId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Type varchar(20)
);
INSERT INTO AddressType (Type) VALUES ('Home')
INSERT INTO AddressType (Type) VALUES ('Work')
INSERT INTO AddressType (Type) VALUES ('Other')

-----------Add address ------------

create procedure SpAddAddress(
		@UserId int,
        @Address varchar(600),
		@City varchar(50),
		@State varchar(50),
		@TypeId int	)		
As 
Begin
	IF (EXISTS(SELECT * FROM Users WHERE @UserId = @UserId))
	Begin
	Insert into AddressTable (UserId,Address,City,State,TypeId )
		values (@UserId,@Address,@City,@State,@TypeId);
	End
	Else
	Begin
		Select 1
	End
End

Exec SpAddAddress 3,'Jhirpani','Rou','Odisha',3
------------------Update address---------
create PROCEDURE UpdateAddress
(
@AddressId int,
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int	)

AS
BEGIN
       If (exists(Select * from AddressTable where AddressId=@AddressId))
		begin
			UPDATE AddressTable
			SET 
			Address= @Address, 
			City = @City,
			State=@State,
			TypeId=@TypeId 
				WHERE AddressId=@AddressID;
		 end
		 else
		 begin
		 select 1;
		 end
END 

Exec UpdateAddress 2,'Sector','Rour','Odisha',3

----------Get all adresses-----------
create PROCEDURE GetAllAddresses
AS
BEGIN
	 begin
	   SELECT * FROM AddressTable ;
   	 end
End

Exec GetAllAddresses
-----------Get adress by user id----------
Alter PROCEDURE GetAddressbyUserid
  @UserId int
AS
BEGIN

     IF(EXISTS(SELECT * FROM AddressTable WHERE UserId=@UserId))
	 begin
	   SELECT * FROM AddressTable WHERE UserId=@UserId;
   	 end
	 else
	 begin
		select 1
	end
End

Exec GetAddressbyUserid 3

-----------------------------------------------------------
-----------------Wishlist table---------------
create table WishlistTable
(
	WishlistId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES BookDetails(BookId)	
);
select * from WishlistTable

----------------Add wishlist------------
Alter PROCEDURE spCreateWishlist
	@UserId INT,
	@BookId INT
AS
BEGIN 
	IF EXISTS(SELECT * FROM WishlistTable WHERE BookId = @BookId AND UserId = @UserId)
		SELECT 1;
	ELSE
	BEGIN
		IF EXISTS(SELECT * FROM BookDetails WHERE BookId = @BookId)
		BEGIN
			INSERT INTO WishlistTable(UserId,BookId)
			VALUES ( @UserId,@BookId)
		END
		ELSE
			SELECT 2;
	END
END

EXEC spCreateWishlist 2,2
EXEC spCreateWishlist 2,3

------------------get wishlist-----------
Alter PROCEDURE ShowWishlistbyUserid
  @UserId int
AS
BEGIN
	   select 
		BookDetails.BookId,BookDetails.BookName,BookDetails.AuthorName,BookDetails.DiscountPrice,BookDetails.OriginalPrice,BookDetails.Image,WishlistTable.WishlistId,WishlistTable.UserId,WishlistTable.BookId
		FROM BookDetails
		inner join WishlistTable
		on WishlistTable.BookId=BookDetails.BookId where WishlistTable.UserId=@UserId
End
Exec ShowWishlistbyUserid 2

----------Remove wishlist-----------
CREATE PROCEDURE spDeleteWishlist
	@WishlistId INT
AS
BEGIN
		DELETE FROM WishlistTable WHERE WishlistId = @WishlistId
END

Exec spDeleteWishlist 2

----------------------OrderTable--------
create table OrderTable
(
         OrdersId int not null identity (1,1) primary key,
		 UserId INT NOT NULL
		 FOREIGN KEY (UserId) REFERENCES Users(UserId),
		 AddressId int
		 FOREIGN KEY (AddressId) REFERENCES AddressTable(AddressId),
	     BookId INT NOT NULL
		 FOREIGN KEY (BookId) REFERENCES BookDetails(BookId),
		 TotalPrice int,
		 BookQuantity int,
		 OrderDate Date
);

-------------add order---------------
Alter PROC spAddingOrders
	@UserId INT,
	@AddressId int,
	@BookId INT ,
	@BookQuantity int
AS
	Declare @TotPrice int
BEGIN
	Select @TotPrice=DiscountPrice from BookDetails where BookId = @BookId;
	IF (EXISTS(SELECT * FROM BookDetails WHERE BookId = @BookId))
	begin
		IF (EXISTS(SELECT * FROM Users WHERE UserId = @UserId))
		Begin
		Begin try
			Begin transaction			
				INSERT INTO OrderTable(UserId,AddressId,BookId,TotalPrice,BookQuantity,OrderDate)
				VALUES ( @UserId,@AddressId,@BookId,@BookQuantity*@TotPrice,@BookQuantity,GETDATE())
				Update BookDetails set BookCount=BookCount-@BookQuantity
				Delete from CartDetails where BookId = @BookId and UserId = @UserId
			commit Transaction
		End try
		Begin catch
			Rollback transaction
		End catch
		end
		Else
		begin
			Select 1
		end
	end 
	Else
	begin
			Select 2
	end	
END

------------get orders------------
Alter PROC spGetAllOrders
	@UserId INT
AS
BEGIN
	select 
		BookDetails.BookId,BookDetails.BookName,BookDetails.AuthorName,BookDetails.DiscountPrice,BookDetails.OriginalPrice,BookDetails.Image,OrderTable.OrdersId,OrderTable.OrderDate
		FROM BookDetails
		inner join OrderTable
		on OrderTable.BookId=BookDetails.BookId where OrderTable.UserId=@UserId
END
Exec spGetAllOrders 2

---------------Feedback table------------
create table FeedbackTable
(
         FeedbackId int not null identity (1,1) primary key,
		 UserId INT NOT NULL
		 FOREIGN KEY (UserId) REFERENCES Users(UserId),
	     BookId INT NOT NULL
		 FOREIGN KEY (BookId) REFERENCES BookDetails(BookId),
		 Comments Varchar(max),
		 Ratings int		 
);

-------------CreateFeedbacktable-------------------
ALter procedure SpAddFeedback(
	@UserId INT,
	@BookId INT,
	@Comments Varchar(max),
	@Ratings int)		
As 
Declare @AverageRating int;
Begin
	IF (EXISTS(SELECT * FROM FeedbackTable WHERE BookId = @BookId and UserId=@UserId))
		select 1; --already given feedback--
	Else
	Begin
		IF (EXISTS(SELECT * FROM BookDetails WHERE BookId = @BookId))
		Begin
			Begin try
				Begin transaction
					Insert into FeedbackTable (UserId,BookId,Comments,Ratings )
						values (@UserId,@BookId,@Comments,@Ratings);		
					select @AverageRating=AVG(Ratings) from FeedbackTable where BookId = @BookId;
					Update BookDetails set Rating=@AverageRating, Reviewer=Reviewer+1 where BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
End

-----------Get feedback-----------
Alter PROC spGetFeedbacks
	@BookId INT
AS
BEGIN
	select 
		FeedbackTable.UserId,FeedbackTable.Comments,FeedbackTable.Ratings,Users.Name
		FROM Users
		inner join FeedbackTable
		on FeedbackTable.UserId=Users.UserId
		where BookId=@BookId
END

Exec spGetFeedbacks 2



