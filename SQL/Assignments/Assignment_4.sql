-- -------------------------------------------
-- 1. Factorial of a Given Number
----------------------------------------

DECLARE @Number INT = 5; -- Input number here
DECLARE @Factorial BIGINT = 1;
DECLARE @i INT = 1;

PRINT '--- Factorial Calculation ---';

WHILE @i <= @Number
BEGIN
    SET @Factorial = @Factorial * @i;
    SET @i = @i + 1;
END

PRINT 'Factorial of ' + CAST(@Number AS VARCHAR) + ' is ' + CAST(@Factorial AS VARCHAR);
PRINT '';


-----------------------------------------------
-- 2. Stored Procedure: Multiplication Table


IF OBJECT_ID('sp_GenerateMultiplicationTable', 'P') IS NOT NULL
    DROP PROCEDURE sp_GenerateMultiplicationTable;
GO

CREATE PROCEDURE sp_GenerateMultiplicationTable
    @Number INT,
    @Limit INT
AS
BEGIN
    DECLARE @i INT = 1;
    PRINT '--- Multiplication Table of ' + CAST(@Number AS VARCHAR) + ' ---';
    WHILE @i <= @Limit
    BEGIN
        PRINT CAST(@Number AS VARCHAR) + ' x ' + CAST(@i AS VARCHAR) + ' = ' + CAST(@Number * @i AS VARCHAR);
        SET @i = @i + 1;
    END
END;
GO


EXEC sp_GenerateMultiplicationTable @Number = 7, @Limit = 10;
PRINT '';

-----------------------------------------
-- 3. Student and Marks Data with Status


-- Drop existing tables if they exist
IF OBJECT_ID('Marks', 'U') IS NOT NULL DROP TABLE Marks;
IF OBJECT_ID('Student', 'U') IS NOT NULL DROP TABLE Student;

-- Create Student table
CREATE TABLE Student (
    Sid INT PRIMARY KEY,
    Sname VARCHAR(50)
);

-- Insert sample data
INSERT INTO Student (Sid, Sname) VALUES 
(1, 'Jack'),
(2, 'Rithvik'),
(3, 'Jaspreeth'),
(4, 'Praveen'),
(5, 'Bisa'),
(6, 'Suraj');

-- Create Marks table
CREATE TABLE Marks (
    Mid INT PRIMARY KEY,
    Sid INT FOREIGN KEY REFERENCES Student(Sid),
    Score INT
);

-- Insert sample marks
INSERT INTO Marks (Mid, Sid, Score) VALUES 
(1, 1, 23),
(2, 6, 95),
(3, 4, 98),
(4, 2, 17),
(5, 3, 53),
(6, 5, 13);

----------------------------------------------
-- Create Function to Determine Status


IF OBJECT_ID('fn_GetStatus', 'FN') IS NOT NULL
    DROP FUNCTION fn_GetStatus;
GO

CREATE FUNCTION fn_GetStatus(@Score INT)
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @Result VARCHAR(10);
    SET @Result = CASE WHEN @Score >= 50 THEN 'Pass' ELSE 'Fail' END;
    RETURN @Result;
END;
GO


PRINT '';
PRINT '--- Student Status Report ---';

SELECT 
    s.Sid,
    s.Sname,
    m.Score,
    dbo.fn_GetStatus(m.Score) AS Status
FROM 
    Student s
JOIN 
    Marks m ON s.Sid = m.Sid
ORDER BY 
    s.Sid;


