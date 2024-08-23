INSERT INTO dbo.brand (name, description, image_url, createdDate, UpdatedDate)
VALUES ('Thuan', 'Thuan Thuan Thuan', 'data/image', '11/11/2003', '11/11/2003');

--
INSERT INTO dbo.category (name, description, image_url, createdDate, UpdatedDate)
VALUES ('Thuan category', 'Thuan Thuan Thuan category', 'data/image', '11/11/2003', '11/11/2003');

--
INSERT INTO dbo.color (colorName, HexValue, createdDate, UpdatedDate)
VALUES ('Cam', '', '11/11/2003', '11/11/2003');

--
INSERT INTO dbo.size (sizeName, description, createdDate, updatedDate)
VALUES ('large', 'To', '11/11/2003', '11/11/2003');

--
INSERT INTO dbo.AspNetUsers 
(Id, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, 
AccessFailedCount)
VALUES 
('1','Thuan', 'Thuan@gmail.com', 1, '0345492751', 1, 1, 1, 0);

--
SET IDENTITY_INSERT dbo.product ON;
INSERT INTO dbo.product 
(id, name, description, stock, price, status, brandId, categoryId, createdDate, updatedDate, userId, colorId, sizeId, isHidden)
VALUES 
(1,'Thuan', '', 0, 999999999, 1, 3, 1, '11/11/2003', '11/11/2003', 1, 1, 3, 0);
SET IDENTITY_INSERT dbo.product OFF;

