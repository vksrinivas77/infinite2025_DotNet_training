----creating a database---
Create Database Sql_Assessment

use Sql_Assessment

create table Book(
id int primary key,
title varchar(50),
 author VARCHAR(50),
 isbn VARCHAR(20) UNIQUE,
 published_date DATETIME
 )
 ---inserting data into book table----
 insert into Book values(1,'My first sql book ','Mary parker','9837487487','2012-02-22 11:08:17'),
 (2,'My Second  sql book ','Jhon mayer','9834783487','1972-07-03 12:08:45'),
 (3,'My third sql book ','cary fint','52374857874','2015-10-18 02:08:44')


 -----Q1---Write a query to fetch the details of the books written by author whose name ends with er----
 select * from Book where author like '%er'


 ----Creating reviews table----
 create table reviews(
 id int primary key,
 book_id int,
 reviewer_name varchar(50),
 content varchar(50),
 rating int,
 published_date DATETIME,
  FOREIGN KEY (book_id) REFERENCES book(id))

  ---inserting data into reviews tabel--------
  Insert into reviews values(1,1,'John Smith','My first review',4,'2017-12-10 02:09:34'),
  (2,2,'John Smith','My second review',5,'2017-10-10 21:09:34'),
  (3,2,'alice Walker','Another review',1,'2017-10-10 21:09:34')

  select * from reviews

  ------Q1---Display the Title ,Author and ReviewerName for all the books from the above table-------
  Select Book.title,Book.author,reviews.reviewer_name from Book,reviews where book.id=reviews.book_id



 ----- Q2---- Display the reviewer name who reviewed more than one book.-------------
 select reviewer_name from reviews group by reviewer_name having COUNT(*)>1

 ----creating customer table-------
 create table customer(id int primary key,
 name varchar(20),
 age int,
 Address varchar(50),
 salary float)

 ---inserting data into Customer table-----
INSERT INTO Customer
VALUES 
    (1, 'RAMESH',32, 'AHMEDABAD',2000.00),
    (2, 'KHILAN',25, 'DELHI', 1500.00),
    (3, 'KAUSHIK',23, 'KOTA', 2000.00),
	(4,'CHAITALI',25,'MUMBAI',6500.00),
	(5,'HARDIK',27,'BHOPAL',8500.00),
	(6,'KOMAL',22,'MP',4500),
	(7,'MUFFY',24,'INDORE',10000)

	select * from Customer


	----Q3---Display the Name for the customer from above customer table 
	---who live in same address which has character o anywhere in address---

	select name,Address from customer where Address like '%o%'


	---creating orders table---
	CREATE TABLE ORDERS (
    O_ID INT PRIMARY KEY,
    O_DATE DATETIME,
    CUSTOMER_ID INT,
    AMOUNT FLOAT,
   )
----inserting data into ORDERS table-----
INSERT INTO ORDERS 
VALUES (102, '2009-10-08 00:00:00', 3, 3000),
       (100, '2009-10-08 00:00:00', 3, 1500),
       (101, '2008-05-20 00:00:00',2, 1560),
	   (103, '2008-05-20 00:00:00',4, 2060)


select * from ORDERS

------Write a query to display the   Date,Total no of customer  placed order on same Date
select o_date, COUNT(DISTINCT customer_id) AS TotalNoCustomers
FROM ORDERS
GROUP BY o_date 


--------creating employe table-------
CREATE TABLE EMPLOYEE (
    id int primary KEY,
    name varchar(50),
    address varchar(100),
    age int,
    salary float
)


INSERT INTO EMPLOYEE
VALUES 
    (1, 'RAMESH', 'AHMEDABAD',  32, 2000.00),
    (2, 'KHILAN', 'DELHI', 25, 1500.00),
    (3, 'KAUSHIK', 'KOTA', 23, 2000.00),
	(4,'CHAITALI','MUMBAI',25,6500.00),
	(5,'HARDIK','BHOPAL',27,8500.00),
	(6,'KOMAL','MP',22,NULL),
	(7,'MUFFY','INDORE',24,NULL)


-----------Display the Names of the Employee in lower case, whose salary is null 
SELECT LOWER(name) AS LowercaseName
FROM Employee
WHERE salary IS NULL






-----creating students table-----
CREATE TABLE Students (
    reg INT PRIMARY KEY,
    name VARCHAR(50),
    age INT,
    qualification VARCHAR(50),
    mobile_no VARCHAR(15),
    mail_id VARCHAR(50),
    location VARCHAR(100),
    gender CHAR(1)
)
-----inserting data into student table------
INSERT INTO Students 
VALUES 
(2, 'SAI', 22, 'BE', '9952836777', 'SAI@GMAIL.COM', 'CHENNAI', 'M'),
(3, 'KUMAR', 20, 'BSC', '7890125648', 'KUMAR@GMAIL.COM', 'MADURAI', 'M'),
(4, 'SELVI',  22, 'B  TECH', '8904567342', 'SELVI@GMAIL.COM', 'SELAM', 'F'),
(5, 'NISHA',  25, 'ME', '7834672310', 'NISHA@GMAIL.COM', 'THENI', 'F'),
(6, 'SAISARAN',  21, 'BA', '7890345678', 'SARAN@GMAIL.COM', 'MADURAI', 'F'),
(7, 'TOM',  23, 'BCA', '8901234675', 'TOM@GMAIL.COM', 'PUNE', 'M')
 
--Write a sql server query to display the Gender,Total no of male and female from the above relation    .
 
SELECT gender, COUNT(*) AS Totale_No_mployees
FROM Students
GROUP BY gender