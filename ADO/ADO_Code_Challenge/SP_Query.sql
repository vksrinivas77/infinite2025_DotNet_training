CREATE DATABASE ADO_CC

USE ADO_CC
 drop table  Employee_Details
---1. Create a stored procedure
CREATE TABLE Employee_Details (
    Empid INT IDENTITY(1, 1) PRIMARY KEY,
    EMPName VARCHAR(100) NOT NULL,
    EMPSalary DECIMAL(10, 2) NOT NULL,
    Gender CHAR(1)
);

 
CREATE PROCEDURE Insert_Employee_Details
    @Name NVARCHAR(100),
    @GivenSalary DECIMAL(10,2),
    @Gender CHAR(1),
    @GeneratedEmpId INT OUTPUT,
    @CalculatedSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Calculate final salary
    SET @CalculatedSalary = @GivenSalary * 0.90;

    -- Insert employee record
    INSERT INTO Employee_Details (EMPName, EMPSalary, Gender)
    VALUES (@Name, @CalculatedSalary, @Gender);

    -- Get last inserted EmpId 
    SELECT @GeneratedEmpId = MAX(Empid) FROM Employee_Details;
END;
INSERT INTO Employee_Details (EMPName, EMPSalary, Gender)
VALUES ('Shrinivas', 5000 * 0.90, 'M');  
select * from Employee_Details

CREATE PROCEDURE UpdateSalaryByEmpId
    @EmpId INT,
    @UpdatedSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Update salary by adding 100
    UPDATE Employee_Details
    SET empsalary = empsalary + 100
    WHERE empid = @EmpId;

    -- Return the updated salary
    SELECT @UpdatedSalary = empsalary
    FROM Employee_Details
    WHERE empid = @EmpId;
END;
