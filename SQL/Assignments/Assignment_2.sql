Create database Assignment_2
use Assignment_2

-- DEPT Table
CREATE TABLE DEPT (
    DEPTNO INT PRIMARY KEY,
    DNAME VARCHAR(20),
    LOC VARCHAR(20)
);

-- EMP Table
CREATE TABLE EMP (
    EMPNO INT PRIMARY KEY,
    ENAME VARCHAR(20),
    JOB VARCHAR(20),
    MGR_ID INT,
    HIREDATE DATE,
    SAL DECIMAL(10, 2),
    COMM DECIMAL(10, 2),
    DEPTNO INT FOREIGN KEY REFERENCES DEPT(DEPTNO)
);

-- Insert into DEPT
INSERT INTO DEPT VALUES
(10, 'ACCOUNTING', 'NEW YORK'),
(20, 'RESEARCH', 'DALLAS'),
(30, 'SALES', 'CHICAGO'),
(40, 'OPERATIONS', 'BOSTON');

-- Insert into EMP
INSERT INTO EMP VALUES
(7369, 'SMITH', 'CLERK', 7902, '1980-12-17', 800, NULL, 20),
(7499, 'ALLEN', 'SALESMAN', 7698, '1981-02-20', 1600, 300, 30),
(7521, 'WARD', 'SALESMAN', 7698, '1981-02-22', 1250, 500, 30),
(7566, 'JONES', 'MANAGER', 7839, '1981-04-02', 2975, NULL, 20),
(7654, 'MARTIN', 'SALESMAN', 7698, '1981-09-28', 1250, 1400, 30),
(7698, 'BLAKE', 'MANAGER', 7839, '1981-05-01', 2850, NULL, 30),
(7782, 'CLARK', 'MANAGER', 7839, '1981-06-09', 2450, NULL, 10),
(7788, 'SCOTT', 'ANALYST', 7566, '1987-04-19', 3000, NULL, 20),
(7839, 'KING', 'PRESIDENT', NULL, '1981-11-17', 5000, NULL, 10),
(7844, 'TURNER', 'SALESMAN', 7698, '1981-09-08', 1500, 0, 30),
(7876, 'ADAMS', 'CLERK', 7788, '1987-05-23', 1100, NULL, 20),
(7900, 'JAMES', 'CLERK', 7698, '1981-12-03', 950, NULL, 30),
(7902, 'FORD', 'ANALYST', 7566, '1981-12-03', 3000, NULL, 20),
(7934, 'MILLER', 'CLERK', 7782, '1982-01-23', 1300, NULL, 10);

--==============================
-- QUERY 1: Employees name starts with 'A'
-- ==============================
SELECT * FROM EMP WHERE ENAME LIKE 'A%';

-- ==============================
-- QUERY 2: Employees without a manager
-- ==============================
SELECT * FROM EMP WHERE MGR_ID IS NULL;

-- ==============================
-- QUERY 3: Employees earning between 1200 and 1400
-- ==============================
SELECT ENAME, EMPNO, SAL 
FROM EMP 
WHERE SAL BETWEEN 1200 AND 1400;

-- ==============================
-- QUERY 4: Give RESEARCH dept employees 10% raise
-- ==============================

-- Before Update
SELECT * FROM EMP WHERE DEPTNO = 20;

-- Update salaries
UPDATE EMP SET SAL = SAL * 1.10 WHERE DEPTNO = 20;

-- After Update
SELECT * FROM EMP WHERE DEPTNO = 20;

-- ==============================
-- QUERY 5: Number of CLERKS
-- ==============================
SELECT COUNT(*) AS [Number of Clerks] 
FROM EMP 
WHERE JOB = 'CLERK';

-- ==============================
-- QUERY 6: Average salary and employee count per job
-- ==============================
SELECT JOB, AVG(SAL) AS [Average Salary], COUNT(*) AS [Number of Employees]
FROM EMP
GROUP BY JOB;

-- ==============================
-- QUERY 7: Employees with lowest and highest salary
-- ==============================
-- Lowest Salary
SELECT * FROM EMP 
WHERE SAL = (SELECT MIN(SAL) FROM EMP);

-- Highest Salary
SELECT * FROM EMP 
WHERE SAL = (SELECT MAX(SAL) FROM EMP);

-- ==============================
-- QUERY 8: Departments with no employees
-- ==============================
SELECT * FROM DEPT 
WHERE DEPTNO NOT IN (SELECT DISTINCT DEPTNO FROM EMP);

-- ==============================
-- QUERY 9: Analysts in dept 20 earning > 1200 sorted by name
-- ==============================
SELECT ENAME, SAL 
FROM EMP 
WHERE JOB = 'ANALYST' AND DEPTNO = 20 AND SAL > 1200 
ORDER BY ENAME;

-- ==============================
-- QUERY 10: Total salary by department
-- ==============================
SELECT D.DEPTNO, D.DNAME, SUM(E.SAL) AS Total_Salary
FROM DEPT D
LEFT JOIN EMP E ON D.DEPTNO = E.DEPTNO
GROUP BY D.DEPTNO, D.DNAME;

-- ==============================
-- QUERY 11: Salary of MILLER and SMITH
-- ==============================
SELECT ENAME, SAL 
FROM EMP 
WHERE ENAME IN ('MILLER', 'SMITH');

-- ==============================
-- QUERY 12: Employees whose names begin with 'A' or 'M'
-- ==============================
SELECT * FROM EMP 
WHERE ENAME LIKE 'A%' OR ENAME LIKE 'M%';

-- ==============================
-- QUERY 13: Yearly salary of SMITH
-- ==============================
SELECT ENAME, SAL * 12 AS [Yearly Salary] 
FROM EMP 
WHERE ENAME = 'SMITH';

-- ==============================
-- QUERY 14: Employees with salary NOT between 1500 and 2850
-- ==============================
SELECT ENAME, SAL 
FROM EMP 
WHERE SAL NOT BETWEEN 1500 AND 2850;


-- QUERY 15: Managers with more than 2 direct reports
-- ==============================
SELECT MGR_ID AS Manager_EMPNO, COUNT(*) AS Reportee_Count
FROM EMP
WHERE MGR_ID IS NOT NULL
GROUP BY MGR_ID
HAVING COUNT(*) > 2;



