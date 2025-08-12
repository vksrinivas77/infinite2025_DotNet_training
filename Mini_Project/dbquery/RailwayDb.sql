Create Database RailwayDB

GO
USE RailwayDB;
GO

-- ========================
-- 1. Users Table
-- ========================
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Phone NVARCHAR(15),
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('admin','user')),
    Status NVARCHAR(10) NOT NULL DEFAULT 'active' CHECK (Status IN ('active','inactive')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- ========================
-- 2. Stations Table
-- ========================
CREATE TABLE Stations (
    StationID INT IDENTITY(1,1) PRIMARY KEY,
    StationCode NVARCHAR(10) NOT NULL UNIQUE,
    StationName NVARCHAR(100) NOT NULL
);

-- ========================
-- 3. Trains Table
-- ========================
CREATE TABLE Trains (
    TrainID INT IDENTITY(1,1) PRIMARY KEY,
    TrainNo NVARCHAR(10) NOT NULL UNIQUE,
    TrainName NVARCHAR(100) NOT NULL,
    SourceStationID INT NOT NULL,
    DestinationStationID INT NOT NULL,
    DepartureTime TIME NOT NULL,
    ArrivalTime TIME NOT NULL,
    Status NVARCHAR(10) NOT NULL DEFAULT 'running' CHECK (Status IN ('running','cancelled','maintenance')),
    AvailableDays NVARCHAR(50) NOT NULL, -- e.g., 'Mon,Tue,Wed'
    AvailableSeats_1AC INT NOT NULL,
    AvailableSeats_2AC INT NOT NULL,
    AvailableSeats_3AC INT NOT NULL,
    AvailableSeats_Sleeper INT NOT NULL,
    TotalSeats AS (AvailableSeats_1AC + AvailableSeats_2AC + AvailableSeats_3AC + AvailableSeats_Sleeper) PERSISTED, -- computed column
    AvailableSeats AS (AvailableSeats_1AC + AvailableSeats_2AC + AvailableSeats_3AC + AvailableSeats_Sleeper) PERSISTED, -- same as total initially
    FOREIGN KEY (SourceStationID) REFERENCES Stations(StationID),
    FOREIGN KEY (DestinationStationID) REFERENCES Stations(StationID)
);

-- ========================
-- 4. Bookings Table
-- ========================
CREATE TABLE Bookings (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    TrainID INT NOT NULL,
    SourceStationName NVARCHAR(100) NOT NULL,
    DestinationStationName NVARCHAR(100) NOT NULL,
    JourneyDate DATE NOT NULL,
    PassengerName NVARCHAR(100) NOT NULL,
    PassengerAge INT NOT NULL,
    PassengerGender NVARCHAR(10),
    TravelClass NVARCHAR(20) NOT NULL CHECK (TravelClass IN ('1AC','2AC','3AC','Sleeper')),
    SeatNumber NVARCHAR(10) NOT NULL,
    PNR NVARCHAR(20) NOT NULL UNIQUE,
    BookingDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (TrainID) REFERENCES Trains(TrainID)
);

-- ========================
-- 5. Cancellations Table
-- ========================
CREATE TABLE Cancellations (
    CancellationID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL,
    CancelledAt DATETIME DEFAULT GETDATE(),
    RefundAmount DECIMAL(10,2) NULL,
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)
);

-- ========================
-- Seed Data
-- ========================

-- Admin user
INSERT INTO Users (Username, Password, FullName, Email, Phone, Role, Status)
VALUES ('admin', 'admin123', 'System Admin', 'admin@railway.com', '9999999999', 'admin', 'active');

-- Stations
INSERT INTO Stations (StationCode, StationName) VALUES
('SBC', 'Krantivira Sangolli Rayanna Bengaluru'),
('MAS', 'Chennai Central'),
('HYB', 'Hyderabad Deccan'),
('PUNE', 'Pune Junction');

-- Sample Train
INSERT INTO Trains (
    TrainNo, TrainName, SourceStationID, DestinationStationID, DepartureTime, ArrivalTime, Status, AvailableDays,
    AvailableSeats_1AC, AvailableSeats_2AC, AvailableSeats_3AC, AvailableSeats_Sleeper
) VALUES (
    '12627', 'Karnataka Express', 1, 2, '19:00', '07:00', 'running', 'Mon,Tue,Wed,Thu,Fri,Sat,Sun',
    10, 20, 40, 100
);

select * from Users
CREATE PROCEDURE sp_BookTicket
    @UserID INT,
    @TrainID INT,
    @JourneyDate DATE,
    @PassengerName NVARCHAR(100),
    @PassengerAge INT,
    @PassengerGender NVARCHAR(10),
    @TravelClass NVARCHAR(20),
    @SeatNumber NVARCHAR(20) OUTPUT,
    @PNR NVARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @avail INT;
        IF @TravelClass='1AC'
            SELECT @avail = AvailableSeats_1AC FROM Trains WHERE TrainID=@TrainID;
        ELSE IF @TravelClass='2AC'
            SELECT @avail = AvailableSeats_2AC FROM Trains WHERE TrainID=@TrainID;
        ELSE IF @TravelClass='3AC'
            SELECT @avail = AvailableSeats_3AC FROM Trains WHERE TrainID=@TrainID;
        ELSE
            SELECT @avail = AvailableSeats_Sleeper FROM Trains WHERE TrainID=@TrainID;

        IF @avail IS NULL OR @avail <= 0
        BEGIN
            RAISERROR('No seats available in this class',16,1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        SET @SeatNumber = @TravelClass + '-' + CONVERT(NVARCHAR(10), (ABS(CHECKSUM(NEWID())) % 1000) + 1);
        SET @PNR = 'PNR' + CONVERT(NVARCHAR(14), GETDATE(), 112) + RIGHT('000' + CAST(ABS(CHECKSUM(NEWID()) % 1000) AS VARCHAR(3)),3);

        INSERT INTO Bookings (UserID, TrainID, SourceStationName, DestinationStationName, JourneyDate, PassengerName, PassengerAge, PassengerGender, TravelClass, SeatNumber, PNR)
        VALUES (@UserID, @TrainID,
                (SELECT s1.StationName FROM Stations s1 JOIN Trains t ON t.SourceStationID=s1.StationID WHERE t.TrainID=@TrainID),
                (SELECT s2.StationName FROM Stations s2 JOIN Trains t2 ON t2.DestinationStationID=s2.StationID WHERE t2.TrainID=@TrainID),
                @JourneyDate, @PassengerName, @PassengerAge, @PassengerGender, @TravelClass, @SeatNumber, @PNR);

        IF @TravelClass='1AC'
            UPDATE Trains SET AvailableSeats_1AC = AvailableSeats_1AC - 1 WHERE TrainID=@TrainID;
        ELSE IF @TravelClass='2AC'
            UPDATE Trains SET AvailableSeats_2AC = AvailableSeats_2AC - 1 WHERE TrainID=@TrainID;
        ELSE IF @TravelClass='3AC'
            UPDATE Trains SET AvailableSeats_3AC = AvailableSeats_3AC - 1 WHERE TrainID=@TrainID;
        ELSE
            UPDATE Trains SET AvailableSeats_Sleeper = AvailableSeats_Sleeper - 1 WHERE TrainID=@TrainID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

CREATE PROCEDURE sp_CancelBooking
    @BookingID INT,
    @RefundAmount DECIMAL(10,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @trainId INT, @cls NVARCHAR(20);
        SELECT @trainId = TrainID, @cls = TravelClass FROM Bookings WHERE BookingID=@BookingID;

        INSERT INTO Cancellations (BookingID, CancelledAt, RefundAmount) VALUES (@BookingID, GETDATE(), 0.00);

        IF @cls='1AC' UPDATE Trains SET AvailableSeats_1AC = AvailableSeats_1AC + 1 WHERE TrainID=@trainId;
        ELSE IF @cls='2AC' UPDATE Trains SET AvailableSeats_2AC = AvailableSeats_2AC + 1 WHERE TrainID=@trainId;
        ELSE IF @cls='3AC' UPDATE Trains SET AvailableSeats_3AC = AvailableSeats_3AC + 1 WHERE TrainID=@trainId;
        ELSE UPDATE Trains SET AvailableSeats_Sleeper = AvailableSeats_Sleeper + 1 WHERE TrainID=@trainId;

        SET @RefundAmount = 0.00;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
select * from Trains