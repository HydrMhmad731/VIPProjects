-- MySqlBackup.NET 2.3.8.0
-- Dump Time: 2024-04-04 16:56:56
-- --------------------------------------
-- Server version 8.0.36 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of patients
-- 

DROP TABLE IF EXISTS `patients`;
CREATE TABLE IF NOT EXISTS `patients` (
  `Patientid` int NOT NULL AUTO_INCREMENT,
  `Firstname` varchar(45) NOT NULL,
  `Lastname` varchar(45) NOT NULL,
  `Age` varchar(45) NOT NULL,
  `DateOfBirth` varchar(45) NOT NULL,
  `Sex` varchar(10) NOT NULL,
  `Address` varchar(50) NOT NULL,
  `ContactNo` varchar(45) NOT NULL,
  PRIMARY KEY (`Patientid`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table patients
-- 

/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients`(`Patientid`,`Firstname`,`Lastname`,`Age`,`DateOfBirth`,`Sex`,`Address`,`ContactNo`) VALUES(2,'Ali','Kassem','25','1999-5-25','Male','Beirut','03922196'),(4,'Nour','Shamas','22','2002-2-24','Female','Beirut','76310564'),(5,'Fatima','Jammal','26','1998-12-2','Female','Baalbeck','81580213'),(6,'Mohamad','Yazbeck','19','2005-2-13','Male','Tyre','76987754'),(7,'Kassem','Haidar','32','1992-4-8','Male','Ghazieh','03877343'),(8,'Zahraa','Mezher','23','2001-6-12','Female','Nabatieh','81365375'),(9,'Hadi','Hamadani','22','2002-4-13','Male','Majadel','81677759'),(10,'Zahraa','El Shami','30','1994-3-21','Female','Jwaya','76566344'),(11,'Celine','Sabra','23','2001-11-3','Female','Nabatieh','03980960'),(13,'Rabi','Tarraf','43','1981-08-20','Male','Beirut','76544455');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;

-- 
-- Definition of nutritionplans
-- 

DROP TABLE IF EXISTS `nutritionplans`;
CREATE TABLE IF NOT EXISTS `nutritionplans` (
  `PlanID` int NOT NULL AUTO_INCREMENT,
  `Patientid` int NOT NULL,
  `StartDate` date NOT NULL,
  `PlanName` varchar(255) NOT NULL,
  `EndDate` date NOT NULL,
  `NutritionalDetails` varchar(150) NOT NULL,
  PRIMARY KEY (`PlanID`),
  KEY `Patientid` (`Patientid`),
  CONSTRAINT `nutritionplans_ibfk_1` FOREIGN KEY (`Patientid`) REFERENCES `patients` (`Patientid`)
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table nutritionplans
-- 

/*!40000 ALTER TABLE `nutritionplans` DISABLE KEYS */;
INSERT INTO `nutritionplans`(`PlanID`,`Patientid`,`StartDate`,`PlanName`,`EndDate`,`NutritionalDetails`) VALUES(101,2,'2024-02-01 00:00:00','KetoGenic Diet','2024-03-01 00:00:00','High-fat, low-carbohydrate diet for weight loss and health benefits'),(102,4,'2024-02-05 00:00:00','Plant-Based Nutrition Program','2024-03-15 00:00:00','Emphasizes whole, plant-based foods for health and sustainability'),(103,5,'2024-01-15 00:00:00','Low Carb Diet','2024-02-28 00:00:00','Reduced carbohydrate intake for weight managemen'),(104,6,'2024-02-10 00:00:00','Low Carb Diet','2024-03-10 00:00:00','Reduced carbohydrate intake for weight management'),(105,7,'2024-03-01 00:00:00','Mediterranean Diet','2024-04-30 00:00:00','Emphasizes whole grains, fruits, vegetables, and healthy fats');
/*!40000 ALTER TABLE `nutritionplans` ENABLE KEYS */;

-- 
-- Definition of patientregistration
-- 

DROP TABLE IF EXISTS `patientregistration`;
CREATE TABLE IF NOT EXISTS `patientregistration` (
  `RegistrationID` int NOT NULL,
  `PatientID` int NOT NULL,
  `RegistrationDate` date NOT NULL,
  `PaymentDate` date NOT NULL,
  PRIMARY KEY (`RegistrationID`),
  KEY `patientregistration_ibfk_1` (`PatientID`),
  CONSTRAINT `patientregistration_ibfk_1` FOREIGN KEY (`PatientID`) REFERENCES `patients` (`Patientid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table patientregistration
-- 

/*!40000 ALTER TABLE `patientregistration` DISABLE KEYS */;
INSERT INTO `patientregistration`(`RegistrationID`,`PatientID`,`RegistrationDate`,`PaymentDate`) VALUES(1,2,'2022-12-01 00:00:00','2023-01-01 00:00:00'),(2,4,'2022-12-03 00:00:00','2023-01-03 00:00:00'),(3,5,'2023-01-10 00:00:00','2023-02-10 00:00:00'),(4,6,'2022-11-25 00:00:00','2022-12-25 00:00:00'),(5,7,'2023-01-12 00:00:00','2023-02-12 00:00:00'),(6,8,'2022-11-20 00:00:00','2023-12-20 00:00:00'),(7,9,'2023-02-01 00:00:00','2023-03-01 00:00:00'),(8,10,'2022-10-15 00:00:00','2022-11-15 00:00:00'),(9,11,'2023-01-21 00:00:00','2023-02-21 00:00:00');
/*!40000 ALTER TABLE `patientregistration` ENABLE KEYS */;

-- 
-- Definition of patientvitals
-- 

DROP TABLE IF EXISTS `patientvitals`;
CREATE TABLE IF NOT EXISTS `patientvitals` (
  `Vitalid` int NOT NULL AUTO_INCREMENT,
  `PatientId` int NOT NULL,
  `Weight` decimal(5,2) NOT NULL,
  `Height` decimal(5,2) NOT NULL,
  `BloodPressure` varchar(20) NOT NULL,
  `HeartRate` int NOT NULL,
  `BodyTemperature` decimal(5,2) NOT NULL,
  `Cholesterol` decimal(5,2) NOT NULL,
  `Glucoselevel` decimal(5,2) NOT NULL,
  `RespiratoryRate` int NOT NULL,
  `DateRecorded` date NOT NULL,
  PRIMARY KEY (`Vitalid`),
  KEY `patientvitals_ibfk_1` (`PatientId`),
  CONSTRAINT `patientvitals_ibfk_1` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Patientid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table patientvitals
-- 

/*!40000 ALTER TABLE `patientvitals` DISABLE KEYS */;
INSERT INTO `patientvitals`(`Vitalid`,`PatientId`,`Weight`,`Height`,`BloodPressure`,`HeartRate`,`BodyTemperature`,`Cholesterol`,`Glucoselevel`,`RespiratoryRate`,`DateRecorded`) VALUES(1,2,70.50,175.00,'120/80',70,36.50,180.00,90.00,16,'2024-02-01 00:00:00'),(2,4,65.00,168.00,'110/70',72,37.00,160.00,85.00,18,'2024-02-05 00:00:00'),(3,5,75.00,180.00,'130/85',68,36.80,190.00,95.00,15,'2024-01-15 00:00:00'),(4,6,80.00,185.00,'125/80',75,36.70,170.00,88.00,17,'2024-02-10 00:00:00'),(5,7,90.00,178.00,'135/85',70,37.20,200.00,100.00,14,'2024-03-01 00:00:00'),(6,8,70.00,170.00,'115/75',68,36.60,175.00,92.00,16,'2024-01-10 00:00:00'),(7,9,72.50,172.00,'125/80',65,36.90,185.00,88.00,15,'2024-02-15 00:00:00');
/*!40000 ALTER TABLE `patientvitals` ENABLE KEYS */;

-- 
-- Definition of suppliers
-- 

DROP TABLE IF EXISTS `suppliers`;
CREATE TABLE IF NOT EXISTS `suppliers` (
  `SupplierID` int NOT NULL,
  `SupplierName` varchar(25) NOT NULL,
  `ContactPerson` varchar(25) NOT NULL,
  `ContactNumber` int NOT NULL,
  `Email` varchar(25) NOT NULL,
  `Address` varchar(75) NOT NULL,
  PRIMARY KEY (`SupplierID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table suppliers
-- 

/*!40000 ALTER TABLE `suppliers` DISABLE KEYS */;
INSERT INTO `suppliers`(`SupplierID`,`SupplierName`,`ContactPerson`,`ContactNumber`,`Email`,`Address`) VALUES(1,'Fitness Supplements Co.','Sarah Johnson',76234678,'sarah313@gmail.com','321 Fitness Road, Beirut, Lebanon'),(2,'Nutrition Express Ltd.','Mark Davis',81345789,'mark_abenz@hotmail.com','456 Nutrition Avenue, Riyadh, Saudi Arabia'),(3,'Health Emporium Inc.','Emily Wilson',75456890,'emily@gmail.com.com','789 Health Street, Tyre, Lebanon'),(4,'Protein Plus LLC','Chris Brown',76567901,'chris@gmail.com','987 Protein Avenue, Sidon, Lebanon'),(5,'Supplement Solutions Ltd.','Laura Taylor',75678012,'lauraS4@gmail.com','654 Supplement Road, Dubai, UAE'),(6,'livgood Lebanon','Samih Smeha',81678012,'Shsh12@gmail.com','13 Dekweneh,beirut,lebanon');
/*!40000 ALTER TABLE `suppliers` ENABLE KEYS */;

-- 
-- Definition of products
-- 

DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `ProductID` int NOT NULL,
  `ProductName` varchar(255) NOT NULL,
  `Category` varchar(50) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `StockQuantity` int NOT NULL,
  `SID` int NOT NULL,
  PRIMARY KEY (`ProductID`),
  KEY `products_ibfk_1` (`SID`),
  CONSTRAINT `products_ibfk_1` FOREIGN KEY (`SID`) REFERENCES `suppliers` (`SupplierID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table products
-- 

/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products`(`ProductID`,`ProductName`,`Category`,`Price`,`StockQuantity`,`SID`) VALUES(1,'Micellar Casein Protein Powder','Supplements',19.99,27,5),(4000,'Fish Oil Capsules','Supplements',19.99,180,3),(4002,'Protein Bars','Snacks',19.99,150,2),(4004,'Casein Protein','Protein',39.99,70,4),(4019,'Protein Pancake Mix','Snacks',15.99,120,6),(4020,'ACup','Snacks',4.99,120,6),(4032,'Creatine Monohydrate','Supplements',24.99,80,3),(4056,'Glutamine Powder','Supplements',27.99,100,5),(4060,'Multivitamin Tablets','Vitamins',14.99,200,1),(4074,'BCAA Powder','Supplements',34.99,120,5),(4077,'Whey Protein Powder','Protein',29.99,100,5),(4086,'Pre-workout Supplement','Supplements',44.99,90,3);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;

-- 
-- Definition of supplierpayments
-- 

DROP TABLE IF EXISTS `supplierpayments`;
CREATE TABLE IF NOT EXISTS `supplierpayments` (
  `PaymentID` int NOT NULL,
  `SupplierID` int NOT NULL,
  `ProductId` int NOT NULL,
  `PaymentDate` date NOT NULL,
  `Amount` decimal(10,2) NOT NULL,
  PRIMARY KEY (`PaymentID`),
  KEY `payments_ibfk_1` (`SupplierID`),
  KEY `payments_ibfk_2` (`ProductId`),
  CONSTRAINT `supplierpayments_ibfk_1` FOREIGN KEY (`SupplierID`) REFERENCES `suppliers` (`SupplierID`),
  CONSTRAINT `supplierpayments_ibfk_2` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table supplierpayments
-- 

/*!40000 ALTER TABLE `supplierpayments` DISABLE KEYS */;
INSERT INTO `supplierpayments`(`PaymentID`,`SupplierID`,`ProductId`,`PaymentDate`,`Amount`) VALUES(1,1,4060,'2024-03-01 00:00:00',70.00),(2,2,4002,'2024-03-05 00:00:00',100.00),(3,4,4004,'2024-03-08 00:00:00',150.00),(4,4,4004,'2024-03-12 00:00:00',150.00),(5,5,4077,'2024-03-15 00:00:00',85.00),(6,6,4020,'2024-03-18 00:00:00',90.00),(7,2,4002,'2024-03-22 00:00:00',100.00),(8,3,4086,'2024-03-25 00:00:00',65.00),(9,4,4004,'2024-03-28 00:00:00',150.00),(10,1,4060,'2024-03-30 00:00:00',70.00);
/*!40000 ALTER TABLE `supplierpayments` ENABLE KEYS */;

-- 
-- Definition of users
-- 

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `UID` int NOT NULL AUTO_INCREMENT,
  `Firstname` varchar(50) NOT NULL,
  `Lastname` varchar(45) NOT NULL,
  `Age` varchar(45) NOT NULL,
  `Sex` varchar(45) NOT NULL,
  `Username` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Role` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  PRIMARY KEY (`UID`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table users
-- 

/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users`(`UID`,`Firstname`,`Lastname`,`Age`,`Sex`,`Username`,`Password`,`Role`,`Email`) VALUES(12,'Haydar','Mhamad','20','Male','Messi','#TheGoatMessi','-Healthcare Provider','10121673@gmail.com'),(13,'Hadi','Mhmd','18','Male','ronaldo','#TheCatRonaldo','-Healthcare Provider','hadimhm@gmail.com'),(14,'Marwa','Abdallah','22','Female','Marousha','M24-M14','Dietitian/Nutritionist','Marwa_ab@gmail.com'),(16,'jawad','Tarhini','24','Male','tarhinio','@tarhini4ever','-Healthcare Provider','tarhini76@gmail.com');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- 
-- Definition of appointments
-- 

DROP TABLE IF EXISTS `appointments`;
CREATE TABLE IF NOT EXISTS `appointments` (
  `AppointmentId` int NOT NULL AUTO_INCREMENT,
  `Patientid` int NOT NULL,
  `Doctorid` int NOT NULL,
  `AppointmentDate` date NOT NULL,
  `AppointmentType` varchar(50) NOT NULL,
  `Notes` text NOT NULL,
  PRIMARY KEY (`AppointmentId`),
  KEY `appointments_ibfk_1` (`Patientid`),
  KEY `appointments_ibfk_2` (`Doctorid`),
  CONSTRAINT `appointments_ibfk_1` FOREIGN KEY (`Patientid`) REFERENCES `patients` (`Patientid`),
  CONSTRAINT `appointments_ibfk_2` FOREIGN KEY (`Doctorid`) REFERENCES `users` (`UID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table appointments
-- 

/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
INSERT INTO `appointments`(`AppointmentId`,`Patientid`,`Doctorid`,`AppointmentDate`,`AppointmentType`,`Notes`) VALUES(1,2,12,'2024-05-10 00:00:00','Checkup','Blood pressure check'),(2,4,13,'2024-05-15 00:00:00','Follow-up','Discuss medication'),(3,7,12,'2024-05-20 00:00:00','Consultation','Diabetes management');
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;

-- 
-- Definition of exerciseplans
-- 

DROP TABLE IF EXISTS `exerciseplans`;
CREATE TABLE IF NOT EXISTS `exerciseplans` (
  `ExPlanId` int NOT NULL AUTO_INCREMENT,
  `Patientid` int NOT NULL,
  `Userid` int NOT NULL,
  `Planname` varchar(255) NOT NULL,
  `Duration` varchar(50) NOT NULL,
  `Intensity` varchar(50) NOT NULL,
  PRIMARY KEY (`ExPlanId`),
  KEY `exerciseplans_ibfk_1` (`Patientid`),
  KEY `exerciseplans_ibfk_2` (`Userid`),
  CONSTRAINT `exerciseplans_ibfk_1` FOREIGN KEY (`Patientid`) REFERENCES `patients` (`Patientid`),
  CONSTRAINT `exerciseplans_ibfk_2` FOREIGN KEY (`Userid`) REFERENCES `users` (`UID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table exerciseplans
-- 

/*!40000 ALTER TABLE `exerciseplans` DISABLE KEYS */;
INSERT INTO `exerciseplans`(`ExPlanId`,`Patientid`,`Userid`,`Planname`,`Duration`,`Intensity`) VALUES(1,2,12,'Cardiovascular Fitness Program','12 weeks','Moderate'),(2,4,12,'Strength Training Routine','30 days','High'),(3,5,13,'Flexibility Training','4 weeks','Low'),(4,6,14,'High-Intensity Interval Training (HIIT)','6 weeks','High'),(5,7,13,'Outdoor Cycling Challenge','30 days','Moderate'),(6,8,12,'Core Strengthening Routine','8 weeks','Moderate'),(7,9,14,'Yoga for Stress Relief','12 weeks','Low');
/*!40000 ALTER TABLE `exerciseplans` ENABLE KEYS */;

-- 
-- Definition of foodintake
-- 

DROP TABLE IF EXISTS `foodintake`;
CREATE TABLE IF NOT EXISTS `foodintake` (
  `intakeId` int NOT NULL AUTO_INCREMENT,
  `Patientid` int NOT NULL,
  `Userid` int NOT NULL,
  `IntakeDate` varchar(45) NOT NULL,
  `MealType` varchar(50) NOT NULL,
  `FoodItem` varchar(255) NOT NULL,
  `PortionSize` varchar(50) NOT NULL,
  `Calories` int NOT NULL,
  `FoodCat.` varchar(45) NOT NULL,
  PRIMARY KEY (`intakeId`),
  KEY `foodintake_ibfk_1` (`Patientid`),
  KEY `foodintake_ibfk_2` (`Userid`),
  CONSTRAINT `foodintake_ibfk_1` FOREIGN KEY (`Patientid`) REFERENCES `patients` (`Patientid`),
  CONSTRAINT `foodintake_ibfk_2` FOREIGN KEY (`Userid`) REFERENCES `users` (`UID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table foodintake
-- 

/*!40000 ALTER TABLE `foodintake` DISABLE KEYS */;
INSERT INTO `foodintake`(`intakeId`,`Patientid`,`Userid`,`IntakeDate`,`MealType`,`FoodItem`,`PortionSize`,`Calories`,`FoodCat.`) VALUES(1,2,14,'Morning','Breakfast','Scrambled Eggs','2 eggs',140,'Protein'),(2,2,12,'Afternoon','Lunch','Grilled Chicken Salad','1 serving',300,'Protein'),(3,5,13,'Evening','Dinner','Salmon with Steamed Vegetables','1 fillet',250,'Protein'),(4,5,14,'Between Meals','Snack','Greek Yogurt with Berries','1 cup',150,'Dairy'),(5,4,12,'Morning','Breakfast','Oatmeal with Mixed Berries','1 cup',150,'Grains'),(6,7,13,'Afternoon','Lunch','Quinoa Salad with Avocado','1 serving',200,'Grains'),(7,9,12,'Evening','Dinner','Grilled Salmon with Asparagus','1 fillet',300,'Protein'),(8,4,14,'Between Meals','Snack','Almonds','1 oz',160,'Nuts');
/*!40000 ALTER TABLE `foodintake` ENABLE KEYS */;

-- 
-- Definition of healthcarerecords
-- 

DROP TABLE IF EXISTS `healthcarerecords`;
CREATE TABLE IF NOT EXISTS `healthcarerecords` (
  `RecordID` int NOT NULL AUTO_INCREMENT,
  `Patientid` int NOT NULL,
  `Userrid` int NOT NULL,
  `Date` date NOT NULL,
  `Discription` varchar(255) NOT NULL,
  `Diagnosis` varchar(55) NOT NULL,
  `Treatment` varchar(55) NOT NULL,
  `TestResults` varchar(75) NOT NULL,
  PRIMARY KEY (`RecordID`),
  KEY `Patientid` (`Patientid`),
  KEY `healthcarerecords_ibfk_2` (`Userrid`),
  CONSTRAINT `healthcarerecords_ibfk_1` FOREIGN KEY (`Patientid`) REFERENCES `patients` (`Patientid`),
  CONSTRAINT `healthcarerecords_ibfk_2` FOREIGN KEY (`Userrid`) REFERENCES `users` (`UID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table healthcarerecords
-- 

/*!40000 ALTER TABLE `healthcarerecords` DISABLE KEYS */;
INSERT INTO `healthcarerecords`(`RecordID`,`Patientid`,`Userrid`,`Date`,`Discription`,`Diagnosis`,`Treatment`,`TestResults`) VALUES(1,2,12,'2024-02-15 00:00:00','Physical examination','High blood pressure','Medication','Blood pressure: 140/90 mmHg'),(2,4,13,'2024-03-05 00:00:00','Annual checkup','None','None','Cholesterol: 200 mg/dL'),(3,7,12,'2024-01-10 00:00:00','Follow-up visit','Diabetes','Insulin therapy','Blood glucose: 180 mg/dL');
/*!40000 ALTER TABLE `healthcarerecords` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2024-04-04 16:56:56
-- Total time: 0:0:0:0:496 (d:h:m:s:ms)
