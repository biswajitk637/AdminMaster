CREATE TABLE Author
(Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(100),
Email VARCHAR(100),
Mobile VARCHAR(20)
)

CREATE TABLE Books
(Id INT PRIMARY KEY IDENTITY,
Title VARCHAR(100),
Price MONEY, 
Quantity INT,
Published_On VARCHAR(100),
Author_Id INT FOREIGN KEY REFERENCES Author(Id)
)

INSERT INTO Author (Name,Email,Mobile)
VALUES
('Aman','amam23@gmail.com','924829322'),
('Rakesh','rakesh23@gmail.com','774829322'),
('Subham Kumar','subham3@gmail.com','884829322');

INSERT INTO Books (Title,Price,Quantity,Published_On,Author_Id)VALUES
('Angular 11',1200,100,getDate(),1),
('React js',2600,100,getDate(),1),
('C sharp',540,300,getDate(),2),
('Node js',1300,600,getDate(),2),
('Javascript',1600,700,getDate(),3);

select* from Tbl_Users;

