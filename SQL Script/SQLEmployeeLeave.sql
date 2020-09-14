CREATE DATABASE EmployeeLeave;


/*creating table for wmployees*/
CREATE TABLE Employees (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	city VARCHAR(50),
	email VARCHAR(50)
);

/*insert data in table just for testing*/
INSERT INTO Employees (first_name,last_name,city,email) VALUES ('Jane','Tasevski','Bitola','janeadmiral@gmail.com');
INSERT INTO Employees (first_name,last_name,city,email) VALUES ('Mike','Stroun','NJ','mike@gmail.com');
INSERT INTO Employees (first_name,last_name,city,email) VALUES ('Cristin','Wein','Chicago','cwein@gmail.com');


/*Store procedures for Employees CRUD operations*/
CREATE PROCEDURE getAllEmployees
AS
BEGIN
	SELECT * FROM Employees ORDER BY last_name;
END;

CREATE PROCEDURE getEmployeeById (
	@emp_id AS INT
)
AS
BEGIN
	SELECT * FROM Employees WHERE id = @emp_id;
END;

CREATE PROCEDURE addEmployee(
	@first_name AS VARCHAR(50),
	@last_name AS VARCHAR(50),
	@city AS VARCHAR(50),
	@email AS VARCHAR(50)
)
AS
BEGIN
	INSERT INTO Employees (first_name,last_name,city,email) VALUES (@first_name, @last_name, @city, @email);
END;

CREATE PROCEDURE updateEmployee(
	@emp_id AS INT,
	@first_name AS VARCHAR(50),
	@last_name AS VARCHAR(50),
	@city AS VARCHAR(50),
	@email AS VARCHAR(50)
)
AS
BEGIN
	UPDATE Employees SET first_name = @first_name, last_name = @last_name, city = @city, email = @email WHERE id = @emp_id
END;

CREATE PROCEDURE deleteEmployee(
	@emp_id AS INT
)
AS
BEGIN
	DELETE FROM Employees WHERE id = @emp_id;
END;


/*Create table for vacations*/
CREATE TABLE Leaves (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	employee_id INT NOT NULL,
	days INT NOT NULL,
	start_date DATE NOT NULL,
	end_date DATE NOT NULL,
	FOREIGN KEY (employee_id) REFERENCES Employees(id)
);

/*Add some data in leaves table*/
INSERT INTO Leaves (employee_id, days, start_date, end_date) VALUES (9,5,'2020-02-01','2020-02-05');
INSERT INTO Leaves (employee_id, days, start_date, end_date) VALUES (10,5,'2020-03-01','2020-03-05');
INSERT INTO Leaves (employee_id, days, start_date, end_date) VALUES (11,5,'2020-03-01','2020-03-05');

/*Store procedure for add leave*/
CREATE PROCEDURE addLeave(
	@employee_id AS INT,
	@days AS INT,
	@start_date AS DATE,
	@end_date AS DATE
)
AS
BEGIN
	INSERT INTO Leaves (employee_id, days, start_date, end_date) VALUES (@employee_id, @days, @start_date, @end_date);
END;


/*Store procedure for get all leaves for employee*/
CREATE PROCEDURE getAllLeaves (
	@emp_id AS INT
)
AS
BEGIN
	SELECT * FROM Leaves WHERE employee_id = @emp_id ORDER BY start_date DESC;
END;

/*Store procedure for get leave details for practicular year for employee*/
CREATE PROCEDURE getTotalLeavesForYearByEmployeeId(
	@emp_id AS INT,
	@year AS INT
)
AS
BEGIN
	SELECT employee_id, (SELECT first_name FROM Employees WHERE id = @emp_id) AS first_name, (SELECT last_name FROM Employees WHERE id = @emp_id) AS last_name, SUM(days) AS spent_days, (SELECT value FROM Setup WHERE type = 'max_days') AS total_days, ((SELECT value FROM Setup WHERE type = 'max_days') - SUM(days)) AS left_days
	FROM Leaves 
	WHERE YEAR(start_date) = @year AND employee_id = @emp_id
	GROUP BY employee_id;
END;

EXEC getTotalLeavesForYearByEmployeeId 8, 2020

/*Creating table setup. There will be max number of leave days in year*/
CREATE TABLE Setup (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	type VARCHAR(50) NOT NULL,
	value INT NOT NULL
);

INSERT INTO Setup (type, value) VALUES ('max_days', 24);

CREATE PROCEDURE getMaxDays
AS
BEGIN
	SELECT value AS max_days FROM Setup WHERE type = 'max_days';
END;

CREATE PROCEDURE setMaxDays(
	@max_days AS INT
)
AS
BEGIN
	UPDATE Setup SET value = @max_days WHERE type = 'max_days';
END;

/*Table for Basic Authentication*/
CREATE TABLE UserLogin(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	UserName VARCHAR(50) NOT NULL,
	Password VARCHAR(50) NOT NULL,
	Email VARCHAR(50),
	Role VARCHAR(50)
);

INSERT INTO UserLogin (UserName, Password, Email, Role) VALUES ('jane','123','janeadmiral@gmail.com','admin');
INSERT INTO UserLogin (UserName, Password, Email, Role) VALUES ('testuser','321','test@email.com','user');

CREATE PROCEDURE getLoginUser(
	@username AS VARCHAR(50),
	@password AS VARCHAR(50)
)
AS
BEGIN
	SELECT * FROM UserLogin WHERE UserName = @username AND Password = @password;
END;
