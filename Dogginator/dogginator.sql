--
-- File generated with SQLiteStudio v3.2.1 on Di Jan 22 20:40:10 2019
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: appointment
DROP TABLE IF EXISTS appointment;
CREATE TABLE "appointment" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`date_from`	TEXT NOT NULL,
	`date_to`	TEXT,
	`active`	INTEGER
);

-- Table: characteristics
DROP TABLE IF EXISTS characteristics;
CREATE TABLE "characteristics" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`description`	TEXT,
	`active`	INTEGER
);
INSERT INTO characteristics (id, description, active) VALUES (2, 'neigt zu übergewicht', 1);

-- Table: consisted_book
DROP TABLE IF EXISTS consisted_book;
CREATE TABLE `consisted_book` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`dogId`	INTEGER,
	`appointmentId`	INTEGER,
	FOREIGN KEY(`appointmentId`) REFERENCES `appointment`(`id`),
	FOREIGN KEY(`dogId`) REFERENCES `dog`(`id`)
);

-- Table: customer
DROP TABLE IF EXISTS customer;
CREATE TABLE "customer" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`salution`	TEXT,
	`firstname`	TEXT NOT NULL,
	`lastname`	TEXT NOT NULL,
	`street`	TEXT NOT NULL,
	`housenumber`	TEXT NOT NULL,
	`zipcode`	TEXT NOT NULL,
	`city`	TEXT,
	`phonenumber`	TEXT,
	`mobilenumber`	TEXT,
	`email`	TEXT,
	`birthday`	TEXT,
	`create_date`	TEXT NOT NULL,
	`edit_date`	TEXT,
	`active`	INTEGER
);
INSERT INTO customer (id, salution, firstname, lastname, street, housenumber, zipcode, city, phonenumber, mobilenumber, email, birthday, create_date, edit_date, active) VALUES (1, 'Herr', 'Patrick', 'Robin', 'Walter-Gropius-Str.', '1', '72762', 'Reutlingen', '071211363381', '', '', '28.08.1983', '2019-01-18 11:58:40', '2019-01-19 23:52:24', 1);
INSERT INTO customer (id, salution, firstname, lastname, street, housenumber, zipcode, city, phonenumber, mobilenumber, email, birthday, create_date, edit_date, active) VALUES (2, 'Frau', 'Monika', 'Rietz', 'Walter-Gropius-Str.', '1', '72762', 'Reutlingen', '07121/1363381', '', 'monika.rietz@kabelbw.de', '27.05.1972', '2019-01-19 13:00:51', '2019-01-19 23:56:59', 1);

-- Table: customer_to_dog
DROP TABLE IF EXISTS customer_to_dog;
CREATE TABLE "customer_to_dog" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`customerId`	INTEGER,
	`dogId`	INTEGER,
	FOREIGN KEY(`customerId`) REFERENCES `customer`(`id`),
	FOREIGN KEY(`dogId`) REFERENCES `dog`(`id`)
);
INSERT INTO customer_to_dog (id, customerId, dogId) VALUES (2, 1, 1);
INSERT INTO customer_to_dog (id, customerId, dogId) VALUES (3, 2, 2);
INSERT INTO customer_to_dog (id, customerId, dogId) VALUES (5, 2, 1);

-- Table: diseases
DROP TABLE IF EXISTS diseases;
CREATE TABLE "diseases" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`name`	TEXT NOT NULL,
	`active`	INTEGER
);
INSERT INTO diseases (id, name, active) VALUES (2, 'keine bekannt', 1);

-- Table: dog
DROP TABLE IF EXISTS dog;
CREATE TABLE "dog" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`name`	TEXT NOT NULL,
	`breed`	TEXT,
	`color`	TEXT,
	`gender`	TEXT NOT NULL,
	`birthday`	TEXT,
	`tassoregistration`	TEXT,
	`chipped`	INTEGER,
	`whichpoint`	TEXT,
	`castrated`	INTEGER NOT NULL,
	`castratedsince`	TEXT,
	`castratemethod`	TEXT,
	`create_date`	TEXT NOT NULL,
	`edit_date`	TEXT,
	`active`	INTEGER
);
INSERT INTO dog (id, name, breed, color, gender, birthday, tassoregistration, chipped, whichpoint, castrated, castratedsince, castratemethod, create_date, edit_date, active) VALUES (1, 'Samy', 'Labrador', 'Blond', 'Rüde', '24.03.2012', '1212121212', 1, 'rechtes Schulterblatt', 1, '12.09.2016', 'Hoden entfernt', '2019-01-19 13:03:11', '2019-01-20 01:05:00', 1);
INSERT INTO dog (id, name, breed, color, gender, birthday, tassoregistration, chipped, whichpoint, castrated, castratedsince, castratemethod, create_date, edit_date, active) VALUES (2, 'Holly', 'Schäferhund', 'Braun/Schwarz', 'Weibchen', '12.04.2006', '332211123', 1, 'rechtes Ohr', 1, '17.05.2014', 'Gebärmutter komlplett entfernt', '2019-01-19 13:07:22', '2019-01-19 23:57:09', 1);

-- Table: dog_to_characteristics
DROP TABLE IF EXISTS dog_to_characteristics;
CREATE TABLE "dog_to_characteristics" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`dogId`	INTEGER,
	`characteristicsId`	INTEGER,
	FOREIGN KEY(`characteristicsId`) REFERENCES `characteristics`(`id`),
	FOREIGN KEY(`dogId`) REFERENCES `dog`(`id`)
);
INSERT INTO dog_to_characteristics (id, dogId, characteristicsId) VALUES (2, 1, 2);

-- Table: dog_to_diseases
DROP TABLE IF EXISTS dog_to_diseases;
CREATE TABLE "dog_to_diseases" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`dogId`	INTEGER,
	`diseasesId`	INTEGER,
	FOREIGN KEY(`diseasesId`) REFERENCES `diseases`(`id`),
	FOREIGN KEY(`dogId`) REFERENCES `dog`(`id`)
);
INSERT INTO dog_to_diseases (id, dogId, diseasesId) VALUES (2, 1, 2);

-- Table: invoice
DROP TABLE IF EXISTS invoice;
CREATE TABLE `invoice` (
	`invoicenumber`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`create_date`	TEXT,
	`paygoal`	TEXT
);

-- Table: note
DROP TABLE IF EXISTS note;
CREATE TABLE "note" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`description`	TEXT NOT NULL,
	`active`	INTEGER
);

-- Table: note_to_customer
DROP TABLE IF EXISTS note_to_customer;
CREATE TABLE "note_to_customer" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`customerId`	INTEGER,
	`noteId`	INTEGER,
	FOREIGN KEY(`noteId`) REFERENCES `note`(`id`),
	FOREIGN KEY(`customerId`) REFERENCES `customer`(`id`)
);

-- Table: products
DROP TABLE IF EXISTS products;
CREATE TABLE `products` (
	`itemnumber`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`shortdescription`	TEXT,
	`longdescription`	TEXT,
	`price`	REAL
);

-- Table: user
DROP TABLE IF EXISTS user;
CREATE TABLE "user" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`username`	TEXT NOT NULL,
	`password`	TEXT NOT NULL,
	`isadmin`	INTEGER,
	`isactive`	INTEGER,
	`create_date`	TEXT,
	`edit_date`	TEXT
);
INSERT INTO user (id, username, password, isadmin, isactive, create_date, edit_date) VALUES (1, 'paddy', '864f9a4fbb8df49f1f59068a7f9a94d4', 1, 1, '2019-01-18 18:22:33', NULL);
INSERT INTO user (id, username, password, isadmin, isactive, create_date, edit_date) VALUES (2, 'p', '9a7e387b9f13500d8e6f443175b9ac4a', 0, 0, '2019-01-18 18:20:33', '2019-01-18 23:05:49');
INSERT INTO user (id, username, password, isadmin, isactive, create_date, edit_date) VALUES (3, 'moni', '735735aa594d4a2eda923d866879d5f2', 0, 1, '2019-01-18 20:18:33', '2019-01-19 13:09:25');
INSERT INTO user (id, username, password, isadmin, isactive, create_date, edit_date) VALUES (4, 'peter.admin', '21232f297a57a5a743894a0e4a801fc3', 1, 1, '2019-01-18 20:20:58', NULL);
INSERT INTO user (id, username, password, isadmin, isactive, create_date, edit_date) VALUES (5, 'peter.user', '51dc30ddc473d43a6011e9ebba6ca770', 0, 1, '2019-01-18 20:21:51', '2019-01-18 22:42:20');

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
