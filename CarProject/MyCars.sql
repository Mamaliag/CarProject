CREATE DATABASE MyCars;
GO

--in order to add tables, etc to the ArtGallery DB, I want to be using the ArtGallery DB 
USE MyCars;
GO
--let's start adding tables - all of tables should be created, or none of them should so let's use a transaction,too
BEGIN TRANSACTION;

--make the customer table
CREATE TABLE cars (
--column name - data type (maybe IDENTITY) - CONSTRAINT
car_id	INT IDENTITY(1,1), --IDENTITY is MS SQL Server auto-incrementing column usually used for primary keys 
car_make VARCHAR(50) NOT NULL,
car_model VARCHAR(100) NOT NULL,
year INT NOT NULL,
image VARCHAR(800),
car_details VARCHAR(200)

CONSTRAINT pk_cars PRIMARY KEY (car_id)
);

COMMIT;

INSERT INTO cars (car_make, car_model, year, image, car_details) VALUES ('Toyota', 'Rav4', 2020, 'https://www.cnet.com/a/img/resize/270a924c3405352038de6af652cda32d1a881ef4/hub/2019/01/18/3d48f6c4-ea31-4c50-85ed-3bb2bf1179ec/2019-toyota-rav4-hybrid-limited-54.jpg?auto=webp&fit=crop&height=675&width=1200', 'Great Reliable Car');
INSERT INTO cars (car_make, car_model, year, image, car_details) VALUES ('BMW', 'X5', 2022, 'https://hips.hearstapps.com/hmg-prod/images/2020-bmw-x5-m-140-1582911136.jpg?crop=0.784xw:0.882xh;0.0721xw,0.118xh&resize=640:*', 'Luxury at its Finest!');