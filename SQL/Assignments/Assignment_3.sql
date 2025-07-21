use Assignment_2

-- 1. Retrieve a list of MANAGERS.
SELECT ename
FROM EMP
WHERE job = 'MANAGER';

--------------------------------------------------------------------------------

-- 2. Find out the names and salaries of all employees earning more than 1000 per month.
SELECT ename, sal
FROM EMP
WHERE sal > 1000;

--------------------------------------------------------------------------------

-- 3. Display the names and salaries of all employees except JAMES.
SELECT ename, sal
FROM EMP
WHERE ename <> 'JAMES';

--------------------------------------------------------------------------------

-- 4. Find out the details of employees whose names begin with ‘S’.
SELECT *
FROM EMP
WHERE ename LIKE 'S%';

--------------------------------------------------------------------------------

-- 5. Find out the names of all employees that have ‘A’ anywhere in their name.
SELECT ename
FROM EMP
WHERE ename LIKE '%A%';

--------------------------------------------------------------------------------

-- 6. Find out the names of all employees that have ‘L’ as their third character in their name.
SELECT ename
FROM EMP
WHERE ename LIKE '__L%';

--------------------------------------------------------------------------------

-- 7. Compute daily salary of JONES.
SELECT ename, sal / 30 AS daily_salary -- Assuming 30 days in a month for daily salary calculation
FROM EMP
WHERE ename = 'JONES';

--------------------------------------------------------------------------------

-- 8. Calculate the total monthly salary of all employees.
SELECT SUM(sal) AS total_monthly_salary
FROM EMP;

--------------------------------------------------------------------------------

-- 9. Print the average annual salary.
SELECT AVG(sal) * 12 AS average_annual_salary
FROM EMP;

--------------------------------------------------------------------------------

-- 10. Select the name, job, salary, department number of all employees except SALESMAN from department number 30.
SELECT ename, job, sal, deptno
FROM EMP
WHERE job <> 'SALESMAN' AND deptno = 30;

--------------------------------------------------------------------------------

-- 11. List unique departments of the EMP table.
SELECT DISTINCT deptno
FROM EMP;

--------------------------------------------------------------------------------

-- 12. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. Label the columns Employee and Monthly Salary respectively.
SELECT ename AS Employee, sal AS "Monthly Salary"
FROM EMP
WHERE sal > 1500 AND (deptno = 10 OR deptno = 30);

--------------------------------------------------------------------------------

-- 13. Display the name, job, and salary of all the employees whose job is MANAGER or ANALYST and their salary is not equal to 1000, 3000, or 5000.
SELECT ename, job, sal
FROM EMP
WHERE (job = 'MANAGER' OR job = 'ANALYST')
  AND sal NOT IN (1000, 3000, 5000);

--------------------------------------------------------------------------------

-- 14. Display the name, salary and commission for all employees whose commission amount is greater than their salary increased by 10%.
SELECT ename, sal, comm
FROM EMP
WHERE comm > (sal * 1.10);

--------------------------------------------------------------------------------

-- 15. Display the name of all employees who have two Ls in their name and are in department 30 or their manager is 7782.
SELECT ename
FROM EMP
WHERE ename LIKE '%L%L%' AND (deptno = 30 OR mgr_id = 7782);

--------------------------------------------------------------------------------

-- 16. Display the names of employees with experience of over 30 years and under 40 yrs.
SELECT ename
FROM EMP
WHERE DATEDIFF(year, hiredate, GETDATE()) > 30
  AND DATEDIFF(year, hiredate, GETDATE()) < 40;

-- Count the total number of employees.
SELECT COUNT(*) AS total_employees
FROM EMP;

--------------------------------------------------------------------------------

-- 17. Retrieve the names of departments in ascending order and their employees in descending order.
SELECT d.dname, e.ename
FROM DEPT d
JOIN EMP e ON d.deptno = e.deptno
ORDER BY d.dname ASC, e.ename DESC;

--------------------------------------------------------------------------------

-- 18. Find out experience of MILLER.
SELECT ename, DATEDIFF(year, hiredate, GETDATE()) AS years_of_experience
FROM EMP
WHERE ename = 'MILLER';