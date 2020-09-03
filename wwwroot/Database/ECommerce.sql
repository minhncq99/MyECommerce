USE master
GO

--- Create Database ---
IF NOT EXISTS (SELECT * FROM sys.databases Where name = 'MyECommerce')
BEGIN
	CREATE DATABASE	MyECommerce
END
ELSE
BEGIN
	DROP DATABASE MyECommerce

	Create DATABASE MyECommerce

END
Go

--- Create Table ---
USE MyECommerce
GO

-- Table Type User --
CREATE TABLE Roles (
	Id tinyint IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(10)  NOT NULL
);
Go

--- Table Admin ---
Create TABLE Admins(
	AdminId nvarchar(30) PRIMARY KEY,
	Password nvarchar(50) NOT NULL,
	Name nvarchar(50) NOT NULL,
	Email nvarchar(50) NOT NULL UNIQUE,
	BirthDate Date ,
	PhoneNumber nvarchar (20) NOT NULL,
	Address nvarchar(200) NOT NULL,
	FromAdmin nvarchar(30),
	RoleId tinyint NOT NULL,
	FOREIGN KEY (FromAdmin) REFERENCES Admins(AdminId),
	FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);
GO

--- Table Shops ---
CREATE TABLE Shops(
	ShopId nvarchar(30) PRIMARY KEY,
	Password nvarchar(50) NOT NULL,
	Name nvarchar(50) NOT NULL,
	ShopName nvarchar(50) NOT NULL UNIQUE,
	Email nvarchar(50) NOT NULL UNIQUE,
	CreatedDate Date NOT NULL,
	PhoneNumber nvarchar(20) NOT NULL,
	Address nvarchar(200) NOT NULL,
	TaxCode nvarchar(20) NOT NULL,
	AccountNumber nvarchar(20) NOT NULL,
	Balance money,
	ContractCode nvarchar(20) NOT NULL,
	RoleId tinyint NOT NULL,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id)
)
GO

--- Coupon ---
Create TABLE Coupons(
	CouponId int IDENTITY(1,1) PRIMARY KEY,
	Code nvarchar(20) NOT NULL UNIQUE,
	Discount int NOT NULL,
	Minimize int,
	ShopId nvarchar(30) NOT NULL,
	FOREIGN KEY (ShopId) REFERENCES Shops(ShopId)
)
GO

--- Customer ---
Create TABLE Customers(
	CustomerId nvarchar(30) PRIMARY KEY,
	Password nvarchar(50) NOT NULL,
	Name nvarchar(50) NOT NULL,
	Email nvarchar(50) NOT NULL UNIQUE,
	BirthDate Date,
	PhoneNumber nvarchar(20) NOT NULL,
	Address nvarchar(200) NOT NULL,
	AccountNumber nvarchar(20),
	RoleId tinyint NOT NULL,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id)
)
GO

--- Shipper ---
CREATE TABLE Shippers(
	ShipperId nvarchar(30) PRIMARY KEY,
	Password nvarchar(50) NOT NULL,
	Name nvarchar(50) NOT NULL,
	Email nvarchar(50) NOT NULL UNIQUE,
	BirthDate Date,
	PhoneNumber nvarchar(20) NOT NULL,
	Address nvarchar(200) NOT NULL,
	TaxCode nvarchar(20) NOT NULL,
	AccountNumber nvarchar(20) NOT NULL,
	Balance money,
	ContractCode nvarchar(20) NOT NULL,
	RoleId tinyint NOT NULL,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id)
)
GO

--- Business(Ngành kinh doanh) ---
CREATE TABLE Business(
	BusinessId int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(100) NOT NULL
)
GO

--- ProductGroups(Nhóm mặt hàng) ---
CREATE TABLE ProductGroups(
	ProductGroupId int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(100) NOT NULL,
	BusinessId int NOT NULL,
	FOREIGN KEY (BusinessId) REFERENCES Business(BusinessId)
)
GO

--- Products ---
CREATE TABLE Products(
	ProductId bigint IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(200) NOT NULL,
	Price bigint NOT NULL,
	Discount bigint NOT NULL,
	Unit nvarchar (20) NOT NULL,
	Weight int Not NULL,
	Brand nvarchar (50) NOT NULL,
	Origin nvarchar(50) NOT NULL,
	Size nvarchar(100) NOT NULL,
	Picture text,
	Description text NOT NULL,
	ShopId nvarchar(30) NOT NULL,
	Status bit,
	ProductGroupId int NOT NULL,
	FOREIGN KEY(ShopId) REFERENCES Shops(ShopId),
	FOREIGN KEY(ProductGroupId) REFERENCES ProductGroups(ProductGroupId) 
)
GO

--- Evaluate ---
CREATE TABLE Evaluates(
	EvaluateId bigint IDENTITY(1,1) PRIMARY KEY,
	NumberStar tinyint NOT NULL,
	ProductId bigint NOT NULL,
	CustomerId nvarchar(30) NOT NULL,
	FOREIGN KEY(ProductId) REFERENCES Products(ProductId), 
	FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId)
)
GO

--- Comments ---
CREATE TABLE Comments(
	CommentId bigint IDENTITY(1,1) PRIMARY KEY,
	Content text NOT NULL,
	ProductId bigint NOT NULL,
	ShopId nvarchar(30),
	CustomerId nvarchar(30),
	FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
	FOREIGN KEY (ShopId) REFERENCES Shops(ShopId),
	FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
)
GO

--- Reply ---
CREATE TABLE Replies(
	ReplyId bigint IDENTITY(1,1) PRIMARY KEY,
	Content text NOT NULL,
	CommentId bigint NOT NULL,
	ShopId nvarchar(30),
	CustomerId nvarchar(30),
	FOREIGN KEY (CommentId) REFERENCES Comments(CommentId),
	FOREIGN KEY (ShopId) REFERENCES Shops(ShopId),
	FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
)
GO

--- Orders ---
CREATE TABLE Orders(
	OrderId bigint IDENTITY(1,1) PRIMARY KEY,
	TimeOrder datetime NOT NULL,
	TimeRecived datetime,
	TemporarySum bigint,
	ShippingFee int,
	TotalPrice bigint,
	Status nvarchar(50),
	Comment text,
	CustomerId nvarchar(30) NOT NULL,
	FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId),
)
GO

--- Order Detail ---
CREATE TABLE OrderDetails(
	OrderDetailId bigint IDENTITY(1,1) PRIMARY KEY,
	Amount int NOT NULL,
	OrderId bigint NOT NULL,
	ProductId bigint NOT NULL,
	CouponId int, 
	FOREIGN KEY(OrderId) REFERENCES Orders(OrderId),
	FOREIGN KEY(ProductId) REFERENCES Products(ProductId),
	FOREIGN KEY(CouponId) REFERENCES Coupons(CouponId)

)
GO

--- Warehose(Chuyển hàng vào kho) ---
CREATE TABLE Warehose(
	WarehoseId bigint IDENTITY(1,1) PRIMARY KEY,
	Time datetime NOT NULL,
	OrderDetailId bigint NOT NULL,
	ShipperId nvarchar(30) NOT NULL,
	FOREIGN KEY(OrderDetailId) REFERENCES OrderDetails(OrderDetailId),
	FOREIGN KEY(ShipperId) REFERENCES Shippers(ShipperId)
)
GO

--- Deliveries(Giao hàng) ---
Create TABLE Deliveries(
	DeliveryId bigint IDENTITY(1,1) PRIMARY KEY,
	Time datetime NOT NULL,
	OrderId bigint NOT NULL,
	ShipperId nvarchar(30) NOT NULL,
	FOREIGN KEY(OrderId) REFERENCES Orders(OrderId),
	FOREIGN KEY(ShipperId) REFERENCES Shippers(ShipperId)
)
GO

--- Chat(Trò chuyện) ---
Create TABLE Chats(
	ChatId bigint IDENTITY(1,1) PRIMARY KEY,
	CustomerId nvarchar(30),
	ShopId nvarchar(30),
	FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId),
	FOREIGN KEY(ShopId) REFERENCES Shops(ShopId)
)
GO

--- ChatDetails ---
Create TABLE ChatDetails(
	ChatDetailId bigint IDENTITY(1,1) PRIMARY KEY,
	Content text NOT NULL,
	Time Datetime  NOT NULL,
	ChatId bigint  NOT NULL,
	CustomerId nvarchar(30),
	ShopId nvarchar(30),
	FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId),
	FOREIGN KEY(ShopId) REFERENCES Shops(ShopId),
	FOREIGN KEY(ChatId) REFERENCES Chats(ChatId)
)
GO

--- Notifications ---
Create Table Notifications(
	NotificationId bigint IDENTITY(1,1) PRIMARY KEY,
	Content text  NOT NULL,
	Date datetime  NOT NULL,
	AdminId nvarchar(30) NOT NULL,
	TypeUserId tinyint NOT NULL,
	FOREIGN KEY (AdminId) REFERENCES Admins(AdminId),
	FOREIGN KEY (TypeUserId) REFERENCES Roles(Id)
)
GO