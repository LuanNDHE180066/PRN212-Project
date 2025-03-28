create database PRN_FinalProject

create table DeviceType(
	dtID int primary key identity(1,1),
	dtName nvarchar(255) null,
	Detail nvarchar(255) null,
	Status int null check (Status in (1,0))
)

create table Device(
	did int primary key identity(1,1),
	typeid int null,
	Hours int null,
	Running int null check (Running in (1,0)),
	Status int null check (Status in (1,0)),
	PricePerHour decimal null,
	foreign key (typeid) references DeviceType(dtID)
)

create table GoodType(
	gtid int primary key identity(1,1),
	gtName nvarchar(255) null
)

create table Goods (
	gid int primary key identity (1,1),
	gName nvarchar(255) null, 
	typeid int null,
	Quantity int null,
	UnitPrice decimal null,
	Unit nvarchar(255) null,
	Status int null check (Status in (1,0)),
	Img nvarchar(255) null,
	foreign key (typeid) references GoodType(gtid)
)

create table Role(
	rid int primary key identity (1,1),
	rName nvarchar(255) null,
	Description nvarchar(255) null
)

create table Staff(
	sid int primary key identity (1,1),
	roleid int,
	Phone varchar(255) null,
	sName nvarchar(255) null,
	StartDate date null,
	EndDate date null,
	Address nvarchar(255) null,
	Username varchar(255) null,
	Password varchar(255) null,
	Status int null check (Status in (1,0)),
	foreign key (roleid) references Role(rid)
)

create table Customer (
	cid int primary key identity (1,1),
	cName nvarchar(255) null,
	Hours int null, 
	Phone varchar(255) null,
	Email nvarchar(255) null, 
	Username varchar(255) null,
	Password varchar(255) null,
	Status int null check (Status in (1,0))
)

create table Expenditure (
	exID int primary key identity (1,1),
	goodsID int null,
	Quantity int null,
	Total decimal null,
	ExDate date null,
	Supplier nvarchar(255) null,
	StaffID int null,
	foreign key (goodsID) references Goods(gid),
	foreign key (StaffID) references Staff(sid)
)

create table Invoice(
	iID int primary key identity(1,1),
	InvoiceDate date null,
	Total decimal null,
	CustomerID int null,
	StaffID int null,
	foreign key (CustomerID) references Customer(cid),
	foreign key (StaffID) references Staff(sid)
)
create table HistoryUsedDevice(
	hudID int primary key identity(1,1),
	InvoiceID int null,
	DeviceID int  null,
	Date date null,
	Start time null,
	[End] time null,
	Amount decimal,
	foreign key (InvoiceID) references Invoice(iID),
	foreign key (DeviceID) references Device(did)
)
create table HistoryBuyGoods(
	hbgID int primary key identity(1,1),
	InvoiceID int null,
	GoodsID int null,
	Date date null,
	Quantity int null,
	Amount decimal,
	foreign key (InvoiceID) references Invoice(iID),
	foreign key (GoodsID) references Goods(gid)
)
create table WorkingHistory(
	StaffID int null,
	Date date null,
	StartTime Time null,
	EndTime Time null,
	foreign key (StaffID) references Staff(sid)
)

INSERT INTO DeviceType (dtName, Detail, Status)
VALUES 
(N'Máy đơn', N'Phổ biến nhất, dành cho game thủ cá nhân, CPU: Intel Core i5-12400F / Ryzen 5 5600, GPU: GTX 1660 Super / RTX 3050, RAM: 16GB DDR4, Màn hình: 24 inch, 75Hz hoặc 144Hz', 1),
(N'Máy đôi', N'Hai người chơi chung một bàn, có thể dùng chung một màn hình lớn hoặc 2 màn hình riêng, CPU: Intel Core i5-13400F / Ryzen 5 7600, GPU: RTX 3060 / RX 6700XT, RAM: 16GB DDR4 hoặc DDR5, Màn hình: 2x 27 inch, 144Hz / 165Hz', 1),
(N'Máy vip', N'Dành cho game thủ có nhu cầu trải nghiệm mượt mà với cấu hình mạnh, CPU: Intel Core i7-13700K / Ryzen 7 7800X3D, GPU: RTX 4070 / RX 7900XT, RAM: 32GB DDR5, Màn hình: 27-32 inch, 240Hz hoặc 360Hz',1),
(N'Máy Stream',N'Dành cho người chơi kết hợp livestream, yêu cầu hiệu suất cao, CPU: Intel Core i9-13900K / Ryzen 9 7950X, GPU: RTX 4080 / RTX 4090, RAM: 32GB / 64GB DDR5, Màn hình: 2x 27 inch, 240Hz',1),
(N'Máy Thi đấu',N'Dành riêng cho game thủ chuyên nghiệp, dùng trong các giải đấu lớn, CPU: Intel Core i9-14900K / Ryzen 9 7950X3D, GPU: RTX 4090, RAM: 64GB DDR5, Màn hình: 32 inch, 360Hz ',1);

select * from DeviceType

INSERT INTO Device (typeid, Hours, Running, Status, PricePerHour)
VALUES 
(1, 40, 1, 1, 7500),
(1, 45, 1, 1, 7500),
(1, 42, 1, 1, 7500),
(1, 67, 1, 1, 7500),
(2, 56, 1, 1, 10000),
(2, 70, 1, 1, 10000),
(3, 80, 1, 1, 15000),
(3, 75, 1, 1, 15000),
(4, 64, 0, 1, 22000),
(4, 74, 1, 1, 22000),
(5, 90, 0, 1, 30000),
(5, 85, 0, 1, 30000);

select * from Device

INSERT INTO GoodType (gtName)
VALUES 
(N'Đồ Ăn Vặt'),
(N'Đồ Uống'),
(N'Đồ Ăn Nhanh'),
(N'Khác');
select * from GoodType

INSERT INTO Goods( gName, typeid, Quantity, UnitPrice, Unit, Status, Img)
VALUES 
(N'Coca Cola', 2, 30, 12000, N'Lon', 1, ''),
(N'Nước Suối', 2, 32, 7000, N'Chai',1, ''),
(N'Pepsi', 2, 30, 15000, N'Lon', 1, ''),
(N'Sting', 2, 30, 10000, N'Chai', 1, ''),
(N'Number One', 2, 30, 10000, N'Chai', 1, ''),
(N'RedBull', 2, 30, 17000, N'Lon', 1, ''),
(N'Nước ép táo', 2, 30, 13000, N'Chai',1,''),
(N'Cà phê', 2, 30, 20000, N'Lon', 1, ''),
(N'Nước ép lê', 2, 30, 17000, N'Chai', 1, ''),
(N'Sữa hộp', 2, 30, 8000, N'Hộp', 1, ''),
(N'Mỳ tôm', 3, 50, 13000, N'Bát', 1, ''),
(N'Bánh mì', 3, 50, 20000, N'Cái', 1, ''),
(N'Xúc xích', 3, 100, 10000, N'Cái', 1, ''),
(N'Cơm hộp', 3, 20, 40000, N'Suất', 1, ''),
(N'Gà rán', 3, 50, 20000, N'Miếng', 1, ''),
(N'Bánh bao', 3, 100, 15000, N'Cái', 1, ''),
(N'Bim Bim', 1, 200, 7000, N'Gói', 1, ''),
(N'Khô Gà', 1, 200, 20000, N'Đĩa', 1, ''),
(N'Khô Bò', 1, 200, 25000, N'Gói', 1, ''),
(N'Khô Heo', 1, 200, 20000, N'Gói', 1, ''),
(N'Rong biển sấy', 1, 200, 7000, N'Gói', 1, ''),
(N'Tai nghe', 4, 50, 100000, N'Cái', 1, ''),
(N'Chuột Gaming', 4, 200, 400000, N'Cái', 1, ''),
(N'Lót chuột', 4, 70, 80000, N'Cái', 1, '');

select * from Goods

INSERT INTO Role (rName, Description)
VALUES 
(N'Quản lý', N'Quản lý toàn bộ hoạt động quán'),
(N'Thu ngân', N'Thu tiền, hỗ trợ khách đăng nhập'),
(N'Phục vụ', N'Order đồ ăn, nước uống, phục vụ khách');
select * from Role
INSERT INTO Staff (roleid, Phone, sName, StartDate, EndDate, Address, Username, Password, Status)
VALUES 
(1, N'0979208204', N'Phùng Nhật Quang', '2025-01-28', null, N'Hà Nội', 'admin', '123', 1),
(2, N'0874747283', N'Nguyễn Văn A', '2025-01-30', null, N'Hà Nội', 'thungan1', '123', 1),
(2, N'0874747434', N'Trần Thị F', '2025-01-30', null, N'Hà Nội', 'thungan2', '123', 1),
(3, N'0979205555', N'Trần Thị B', '2025-01-31', '2025-02-02', N'Phú Thọ', 'phucvu1', '123', 1),
(3, N'0979204546', N'Lê Văn C', '2025-02-01', null, N'Hà Nội', 'phucvu2', '123', 1),
(3, N'0979204546', N'Ngô Văn P', '2025-02-01', '2025-02-03', N'Hà Nội', 'phucvu3', '123', 1),
(3, N'0979204546', N'Phan Thị G', '2025-02-04', null, N'Hà Nội', 'phucvu4', '123', 1);

select * from Staff

INSERT INTO Customer ( cName, Hours, Phone, Email, Username, Password, Status)
VALUES 
(N'Nguyễn Văn A', 6, N'0785847674', N'nguyenvana@gmail.com', N'nguyenvana', '1', 1),
(N'Trần Văn B', 10, N'0785844355', N'tranvanb@gmail.com', N'tranvanb', '2', 1),
(N'Lê Thị C', 12, N'0785857464', N'lethic@gmail.com', N'lethic', '3', 1),
(N'Phùng Nhật D', 5, N'0785846373', N'phungnhatd@gmail.com', N'phungnhatd', '4', 1),
(N'Ngô Văn E', 9, N'0785847894', N'ngovane@gmail.com', N'ngovane', '5', 1),
(N'Nguyễn Quang G', 10, N'07858342', N'nguyenquangg@gmail.com', N'nguyenquangg', '6', 1),
(N'Ma Văn H', 14, N'0785848889', N'mavanh@gmail.com', N'mavanh', '7', 1),
(N'Lê Đức I', 15, N'0785844356', N'leduci@gmail.com', N'leduci', '8', 1),
(N'Vũ Đình J', 18, N'0785844357', N'vudinhj@gmail.com', N'vudinhj', '9', 1),
(N'Triệu Hoàng S', 20, N'0785844358', N'trieuhoangs@gmail.com', N'trieuhoangs', '10', 1);
select * from Customer

INSERT INTO Expenditure(goodsID, Quantity, Total, ExDate, Supplier, StaffID)
VALUES 
(1, 30, 300000, '2025-02-01', N'Cửa hàng bách hóa', 1),
(2, 30, 150000, '2025-02-01', N'Cửa hàng bách hóa', 1),
(11, 30, 450000, '2025-02-01', N'Cửa hàng bách hóa', 1),
(13, 30, 150000, '2025-02-06', N'Cửa hàng bách hóa', 1),
(20, 30, 790000, '2025-02-07', N'Cửa hàng bách hóa', 1),
(21, 30, 210000, '2025-02-10', N'Cửa hàng bách hóa', 1),
(22, 10, 900000, '2025-02-10', N'Hệ thống cung cấp đồ công nghệ', 1),
(23, 5, 3500000, '2025-02-10', N'Hệ thống cung cấp đồ công nghệ', 1),
(24, 30, 300000, '2025-02-10', N'Hệ thống cung cấp đồ công nghệ', 1),
(14, 10, 300000, '2025-02-12', N'Quán cơm bình dân', 1);

select * from Expenditure
select * from Customer
INSERT INTO Invoice (InvoiceDate, Total, CustomerID, StaffID)
VALUES 
('2025-02-01', 84000, 1, 2),
('2025-02-01', 61750,2,3),
('2025-02-02', 115000 ,3,2),
('2025-02-02', 130000 ,4, 3),
('2025-02-03', 380000 ,5,2),
('2025-02-04', 161000, 6, 3),
('2025-02-04',123000 ,7,2),
('2025-02-05', 71000 ,8,3),
('2025-02-05', 109000 ,9,2),
('2025-02-06', 559000 ,10,3);









INSERT INTO HistoryBuyGoods( InvoiceID, GoodsID, Date, Quantity, Amount)
VALUES 
(1, 1, '2025-02-01', 2, 24000),
(2, 3, '2025-02-01', 2, 30000),
(2, 11, '2025-02-01', 1, 13000),
(3, 8, '2025-02-02', 3, 60000),
(3, 12, '2025-02-02', 2, 40000),
(4, 16, '2025-02-02', 5, 75000),
(4, 13, '2025-02-02', 4, 40000),
(5, 18, '2025-02-03', 7, 140000),
(5, 14, '2025-02-03', 3, 120000),
(6, 17, '2025-02-04', 3, 21000),
(6, 18, '2025-02-04', 2, 40000),
(6, 14, '2025-02-04', 2, 80000),
(7, 15, '2025-02-04', 2, 40000),
(7, 10, '2025-02-04', 1, 8000),
(8, 2, '2025-02-05', 5, 35000),
(8, 7, '2025-02-05', 2, 26000),
(9, 9, '2025-02-05', 2, 34000),
(10, 10, '2025-02-06', 2, 16000),
(10, 23, '2025-02-06', 1, 400000);






INSERT INTO HistoryUsedDevice(InvoiceID, DeviceID, Date, Start, [End] , Amount)
VALUES 
(1, 12, '2025-02-01', '08:30:00', '10:30:00', 60000),
(2, 1, '2025-02-01', '09:00:00', '11:30:00', 18750),
(3, 2, '2025-02-02', '09:00:00', '11:00:00', 15000),
(4, 3, '2025-02-02', '12:00:00', '14:00:00', 15000),
(5, 11, '2025-02-03', '12:00:00','16:00:00', 120000),
(6, 5, '2025-02-04', '15:00:00', '17:00:00', 20000),
(7, 12, '2025-02-04', '07:00:00', '09:30:00', 75000),
(8, 6, '2025-02-05', '08:00:00', '09:00:00', 10000),
(9, 8, '2025-02-05', '13:00:00', '18:00:00', 75000),
(10, 9, '2025-02-06', '12:00:00', '18:30:00', 143000);


select * from Staff
INSERT INTO WorkingHistory (StaffID, Date, StartTime, EndTime)
VALUES 
(2, '2025-02-01', '07:00:00', '17:00:00'),
(3, '2025-02-02', '07:00:00', '17:00:00'),
(2, '2025-02-03', '07:00:00', '16:30:00'),
(3, '2025-02-04', '07:00:00', '17:00:00'),
(2, '2025-02-05', '07:00:00', '17:00:00'),
(2, '2025-02-06', '07:00:00', '17:00:00'),
(4, '2025-02-01', '07:00:00', '17:00:00'),
(4, '2025-02-02', '07:00:00', '12:00:00'),
(5, '2025-02-02', '07:00:00', '17:00:00'),
(5, '2025-02-03', '07:00:00', '17:00:00'),
(5, '2025-02-04', '07:00:00', '17:00:00'),
(5, '2025-02-05', '07:00:00', '17:00:00'),
(5, '2025-02-06', '07:00:00', '17:00:00'),
(6, '2025-02-02', '07:00:00', '17:00:00'),
(6, '2025-02-03', '07:00:00', '11:00:00'),
(7, '2025-02-04', '07:00:00', '17:00:00'),
(7, '2025-02-05', '07:00:00', '17:00:00'),
(7, '2025-02-06', '07:00:00', '17:00:00');

select * from WorkingHistory

 select * from Invoice
select * from Expenditure
select * from HistoryUsedDevice
select * from Invoice
select * from Customer
select * from Device
select * from Staff

SELECT * FROM HistoryBuyGoods 

















