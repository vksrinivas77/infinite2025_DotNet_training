create Database Sql_Assessment_2
use Assignment_2
select * from sys.tables
select * from Emp

-----Q1----?Write a query to display your birthday( day of week)------
SELECT DATENAME(WEEKDAY,'2001-08-26') as MyBirthday


----Q2---------Write a query to display your age in days--------
SELECT DATEDIFF(DAY,'2001-06-26',getdate()) as 'Age_In_Days'

----Q3---------Write a query to display all employees information those who joined before 5 years in the current month
UPDATE emp SET hiredate = '2017-08-17' WHERE empno = 7499;

SELECT *
FROM emp
WHERE 
    DATEDIFF(YEAR, hiredate, GETDATE()) >= 5
    AND MONTH(hiredate) = MONTH(GETDATE());

 


----Q4---------Create table Employee with empno, ename, sal, doj columns or use your emp table and perform the following operations in a single transaction
---------a. First insert 3 rows 
---------b. Update the second row sal with 15% increment  
---------c. Delete first row.
---------After completing above all actions, recall the deleted row without losing increment of second row.

BEGIN TRANSACTION;
Select * from  Emp;

--  Update the second row's salary with a 15% increment
WITH RankedEmployees AS (
    SELECT empno, ROW_NUMBER() OVER (ORDER BY empno) AS Row_num
    FROM EMP
)
UPDATE EMP
SET sal = sal * 1.15
WHERE empno IN (
    SELECT empno FROM RankedEmployees WHERE Row_num = 2
);

-- -- Save transaction point before deletion
SAVE TRANSACTION t1;

----- Delete the first row
WITH RankedEmployees AS (
    SELECT empno, ROW_NUMBER() OVER (ORDER BY empno) AS Row_num
    FROM EMP
)
DELETE FROM EMP
WHERE empno IN (
    SELECT empno FROM RankedEmployees WHERE Row_num = 1
);

ROLLBACK TRANSACTION t1;
COMMIT;


----Q5-----    Create a user defined function calculate Bonus for all employees of a 
-------------given dept using 	following conditions

CREATE OR ALTER FUNCTION Calculate_Bonus (
    @deptno INT,
    @sal DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS 
BEGIN
    DECLARE @Bonus DECIMAL(10,2);

    IF @deptno = 10
        SET @Bonus = @sal * 0.15;
    ELSE IF @deptno = 20
        SET @Bonus = @sal * 0.20;
    ELSE
        SET @Bonus = @sal * 0.05;

    RETURN @Bonus;
END;

select *,dbo.Calculate_Bonus(deptno,sal) as bonus from emp

------Q6----6. Create a procedure to update the salary of employee by 500 whose dept name
-----------is Sales and current salary is below 1500 (use emp table)

CREATE OR ALTER PROCEDURE update_sales_salary
AS
BEGIN
    UPDATE emp
    SET sal = sal + 500
    WHERE sal < 1500
      AND deptno = (
          SELECT deptno FROM dept WHERE UPPER(dname) = 'SALES'
      );
END;

EXEC update_sales_salary;
select * from emp
