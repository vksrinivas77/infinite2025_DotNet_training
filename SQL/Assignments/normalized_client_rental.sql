
-- Client Table
CREATE TABLE Client (
    ClientNo VARCHAR(10) PRIMARY KEY,
    cName VARCHAR(100) NOT NULL
);

-- Owner Table
CREATE TABLE Owner (
    ownerNo VARCHAR(10) PRIMARY KEY,
    oName VARCHAR(100) NOT NULL
);

-- Property Table
CREATE TABLE Property (
    propertyNo VARCHAR(10) PRIMARY KEY,
    pAddress VARCHAR(200) NOT NULL,
    ownerNo VARCHAR(10) NOT NULL,
    FOREIGN KEY (ownerNo) REFERENCES Owner(ownerNo)
);

-- Rental Table
CREATE TABLE Rental (
    ClientNo VARCHAR(10),
    propertyNo VARCHAR(10),
    rentStart DATE,
    rentFinish DATE,
    rent DECIMAL(10, 2),
    PRIMARY KEY (ClientNo, propertyNo, rentStart),
    FOREIGN KEY (ClientNo) REFERENCES Client(ClientNo),
    FOREIGN KEY (propertyNo) REFERENCES Property(propertyNo)
);

-- Insert data into Client
INSERT INTO Client VALUES 
('CR76', 'John Kay'),
('CR56', 'Aline Stewart');

-- Insert data into Owner
INSERT INTO Owner VALUES 
('CO93', 'Tony Shaw'),
('CO40', 'Tina Murphy');

-- Insert data into Property
INSERT INTO Property VALUES 
('PG16', '5 Nova Dr, Glasgow', 'CO93'),
('PG1', '6 Lawrence St, Glasgow', 'CO40'),
('PG36', '2 Maitland Rd, Glasgow', 'CO93');

-- Insert data into Rental
INSERT INTO Rental VALUES 
('CR76', 'PG1', '2000-07-01', '2001-08-31', 350),
('CR76', 'PG16', '2002-09-01', '2002-10-01', 450),
('CR56', 'PG16', '2002-09-01', '2002-10-01', 450),
('CR56', 'PG36', '2001-10-01', '2001-12-01', 300),
('CR56', 'PG1', '2000-09-01', '2001-09-01', 350);
