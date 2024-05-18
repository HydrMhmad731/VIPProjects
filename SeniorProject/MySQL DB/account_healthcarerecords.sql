CREATE DATABASE  IF NOT EXISTS `account` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `account`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: account
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `healthcarerecords`
--

DROP TABLE IF EXISTS `healthcarerecords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `healthcarerecords` (
  `RecordID` int NOT NULL AUTO_INCREMENT,
  `Patientid` int NOT NULL,
  `RecordedDate` date NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Diagnosis` varchar(55) NOT NULL,
  `Treatment` varchar(55) NOT NULL,
  `TestResults` varchar(75) NOT NULL,
  PRIMARY KEY (`RecordID`),
  KEY `Patientid` (`Patientid`),
  CONSTRAINT `healthcarerecords_ibfk_1` FOREIGN KEY (`Patientid`) REFERENCES `patients` (`Patientid`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `healthcarerecords`
--

LOCK TABLES `healthcarerecords` WRITE;
/*!40000 ALTER TABLE `healthcarerecords` DISABLE KEYS */;
INSERT INTO `healthcarerecords` VALUES (1,2,'2024-02-15','Physical examination','Hypertension (High Blood Pressure)','Medications (e.g., ACE inhibitors, diuretics)','Blood pressure: 140/90 mmHg'),(2,4,'2024-03-05','Annual checkup','None','None','Cholesterol: 200 mg/dL'),(5,5,'2024-04-10','Follow-up visit','Diabetes','Insulin therapy','Blood glucose: 180 mg/dL'),(6,6,'2024-05-20','Routine checkup','Hypertension','Lifestyle modification','Blood pressure: 130/80 mmHg'),(8,7,'2024-05-05','Annual Checkup','Obesity','Physical activity','Blood pressure: 140/90 mmHg'),(9,15,'2024-05-15','Annual Checkup','Asthma','Inhaled corticosteroids','Blood pressure : 200mg'),(10,17,'2024-05-17','Headache Examination','Anxiety Disorder','Psychotherapy','High level of migrane');
/*!40000 ALTER TABLE `healthcarerecords` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-18 12:48:52
