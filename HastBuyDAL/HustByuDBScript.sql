-- DATABASE: Enterprise eCommerce Platform
USE [master]
GO
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'HastBuyDB'OR name = N'HastBuyDB')))
DROP DATABASE HastBuyDB

CREATE DATABASE HastBuyDB
GO

USE HastBuyDB
GO

-- 1. USERS TABLE
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    Phone NVARCHAR(20) UNIQUE,
    PasswordHash NVARCHAR(255),
    Role NVARCHAR(50), -- 'Customer', 'Admin', 'DeliveryAgent', etc.
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- 2. ADDRESSES TABLE
CREATE TABLE Addresses (
    AddressID INT PRIMARY KEY IDENTITY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Street NVARCHAR(200),
    City NVARCHAR(100),
    State NVARCHAR(100),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(100),
    IsDefault BIT DEFAULT 0
);

-- 3. PRODUCTS TABLE
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(200),
    Description TEXT,
    Price DECIMAL(10,2),
    Stock INT,
    CategoryID INT,
    Brand NVARCHAR(100),
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- 4. PRODUCT CATEGORIES
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100),
    ParentCategoryID INT NULL REFERENCES Categories(CategoryID)
);

-- 5. REVIEWS
CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY IDENTITY,
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(1000),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 6. WISHLIST
CREATE TABLE Wishlist (
    WishlistID INT PRIMARY KEY IDENTITY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 7. CART ITEMS
CREATE TABLE CartItems (
    CartItemID INT PRIMARY KEY IDENTITY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT,
    AddedAt DATETIME DEFAULT GETDATE()
);

-- 8. ORDERS
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    AddressID INT FOREIGN KEY REFERENCES Addresses(AddressID),
    OrderDate DATETIME DEFAULT GETDATE(),
    OrderStatus NVARCHAR(50), -- Pending, Shipped, Delivered, Returned, etc.
    PaymentStatus NVARCHAR(50), -- Paid, Failed, Pending
    TotalAmount DECIMAL(10,2)
);

-- 9. ORDER ITEMS
CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY IDENTITY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT,
    Price DECIMAL(10,2)
);

-- 10. PAYMENTS
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    PaymentMethod NVARCHAR(50),
    PaymentDate DATETIME,
    Amount DECIMAL(10,2),
    TransactionID NVARCHAR(100)
);

-- 11. RETURNS / REFUNDS
CREATE TABLE Returns (
    ReturnID INT PRIMARY KEY IDENTITY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    Reason NVARCHAR(255),
    RequestDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(50), -- Requested, Approved, Rejected, Completed
    RefundAmount DECIMAL(10,2)
);

-- 12. PROMO CODES
CREATE TABLE PromoCodes (
    PromoID INT PRIMARY KEY IDENTITY,
    Code NVARCHAR(50),
    DiscountPercent INT,
    ExpiryDate DATE,
    MaxUsage INT,
    UsageCount INT DEFAULT 0
);

-- 13. PROMO USAGE
CREATE TABLE PromoUsage (
    UsageID INT PRIMARY KEY IDENTITY,
    PromoID INT FOREIGN KEY REFERENCES PromoCodes(PromoID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    UsedOn DATETIME DEFAULT GETDATE(),
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID)
);

-- 14. DELIVERIES
CREATE TABLE Deliveries (
    DeliveryID INT PRIMARY KEY IDENTITY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    DeliveryAgentID INT FOREIGN KEY REFERENCES Users(UserID),
    AssignedAt DATETIME DEFAULT GETDATE(),
    DeliveredAt DATETIME NULL,
    DeliveryStatus NVARCHAR(50) -- 'Assigned', 'Out for Delivery', 'Delivered', 'Failed'
);

-- 15. INVENTORY LOGS
CREATE TABLE InventoryLogs (
    LogID INT PRIMARY KEY IDENTITY,
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    ChangeType NVARCHAR(50), -- 'Order', 'Restock', 'Return'
    QuantityChange INT,
    ChangedAt DATETIME DEFAULT GETDATE(),
    ReferenceID INT NULL
);

-- 16. AUDIT LOGS
CREATE TABLE AuditLogs (
    LogID INT PRIMARY KEY IDENTITY,
    UserID INT,
    Action NVARCHAR(255),
    ActionTime DATETIME DEFAULT GETDATE(),
    IPAddress NVARCHAR(50),
    Module NVARCHAR(100)
);

-- 17. NOTIFICATIONS
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Message NVARCHAR(255),
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
)
GO

CREATE FUNCTION dbo.fn_GetStock(@ProductID INT)
RETURNS INT
AS
BEGIN
    DECLARE @Stock INT;
    SELECT @Stock = Stock FROM Products WHERE ProductID = @ProductID;
    RETURN @Stock;
END;
GO

CREATE FUNCTION dbo.fn_UserOrderSummary(@UserID INT)
RETURNS TABLE
AS
RETURN (
    SELECT o.OrderID, o.OrderDate, o.TotalAmount, o.OrderStatus
    FROM Orders o
    WHERE o.UserID = @UserID
);
GO

CREATE PROCEDURE dbo.sp_PlaceOrder
    @UserID INT,
    @AddressID INT,
    @PromoCode NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @OrderID INT, @TotalAmount DECIMAL(10,2) = 0;

    INSERT INTO Orders(UserID, AddressID, OrderStatus, PaymentStatus, TotalAmount)
    VALUES (@UserID, @AddressID, 'Pending', 'Pending', @TotalAmount);

    SET @OrderID = SCOPE_IDENTITY();

    UPDATE Orders SET TotalAmount = @TotalAmount WHERE OrderID = @OrderID;
    SELECT @OrderID AS NewOrderID;
END;

