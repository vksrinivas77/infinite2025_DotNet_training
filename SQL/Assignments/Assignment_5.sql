
--1. Write a T-Sql based procedure to generate complete payslip of a given employee with respect to the following condition

--   a) HRA as 10% of Salary
--   b) DA as 20% of Salary
--   c) PF as 8% of Salary
--   d) IT as 5% of Salary
--   e) Deductions as sum of PF and IT
--   f) Gross Salary as sum of Salary, HRA, DA
--   g) Net Salary as Gross Salary - Deductions

--print the payslip neatly with all details

create or alter proc Pro_PaySlip (@empID int,@sal decimal(10, 2))
as 
begin
    declare @HRA decimal(10, 2) = @sal * 0.10;
    declare @DA decimal(10, 2) = @sal * 0.20;
    declare @PF decimal(10, 2) = @sal * 0.08;
    declare @IT decimal(10, 2) = @sal * 0.05;
    declare @Deductions decimal(10, 2) = @PF + @IT;
    declare @GrossSalary decimal(10, 2) = @sal + @HRA + @DA;
    declare @NetSalary decimal(10, 2) = @GrossSalary - @Deductions;

    print 'Employee ID: ' + cast(@empID as varchar);
    print 'Basic Salary: ' + cast(@sal as varchar);
    print 'HRA (10%): ' + cast(@HRA as varchar);
    print 'DA (20%): ' + cast(@DA as varchar);
    print 'PF (8%): ' + cast(@PF as varchar);
    print 'IT (5%): ' + cast(@IT as varchar);
    print 'Deductions: ' + cast(@Deductions as varchar);
    print 'Gross Salary: ' + cast(@GrossSalary as varchar);
    print 'Net Salary: ' + cast(@NetSalary as varchar);
end;

execute pdPaySlip  @sal = 50000,@empID = 123



--2.  create a trigger to restrict data manipulation on EMP table during General holidays.
--Display the error message like �Due to Independence day you cannot manipulate data� or
--"Due To Diwali", you cannot manipulate" etc

--Note: create holiday table with (holiday_date,Holiday_name). 
--Store at least 4 holiday details. try to match and stop manipulation 


create table Genera_lHolidays (
    HoliDate date primary key,
    HoliName varchar(50)
);


insert into General_Holidays values 
(cast('2025-08-15' AS DATE), 'Independence Day'),
(cast('2025-12-25' AS DATE), 'Christmas'),
(cast('2025-10-02' AS DATE), 'Gandhi Jayanti'),
(cast('2025-01-26' AS DATE), 'Republic Day');


select * from General_Holidays;

create or alter trigger Restrict_HolidayDM
on EMP
after insert, update, delete
as
begin
    declare @TestDate date = '2025-12-26';
    declare @HolidayName varchar(50);
    declare @ErrMsg varchar(150);
    select @HolidayName = HoliName from General_Holidays where HoliDate = @TestDate;
   
	if @HolidayName is not null
		begin
			 begin transaction
			 set @ErrMsg = 'Due to ' + @HolidayName + ', you cannot manipulate data today. Try it again tomorrow.';
			 raiserror (@ErrMsg, 16, 1);
			 rollback transaction;
		end  
end;


