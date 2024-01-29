CREATE DATABASE db_wcfwebservicesoap;
USE db_wcfwebservicesoap;

CREATE TABLE tb_Students(
	idStudent INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(40),
    roll VARCHAR(20)
);

DELIMITER $$

CREATE PROCEDURE proc_InsertStudent(IN varName VARCHAR(40), IN varRoll VARCHAR(20))
BEGIN
	INSERT INTO tb_Students (`name`, roll)
    VALUES (varName, varRoll);
END$$

CREATE PROCEDURE proc_SelectAllStudents()
BEGIN
	SELECT *
    FROM tb_Students;
END$$

CREATE PROCEDURE proc_SelectStudent(IN varIdStudent INT, OUT Name VARCHAR(40), OUT Roll VARCHAR(20))
BEGIN
	SELECT name, roll
    INTO Name, Roll
    FROM tb_Students
    WHERE idStudent = varIdStudent;
END$$ 

CREATE PROCEDURE proc_UpdateStudent(IN varIdStudent INT, IN varName VARCHAR(40), IN varRoll VARCHAR(20))
BEGIN
	UPDATE tb_Students
    SET name = varName, roll = varRoll
    WHERE idStudent = varIdStudent;
END$$

CREATE PROCEDURE proc_DeleteStudent(IN varIdStudent INT)
BEGIN
	DELETE FROM tb_Students
    WHERE idStudent = varIdStudent;
END$$

DELIMITER ;

