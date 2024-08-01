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
-- Table structure for table `patientvitals`
--

DROP TABLE IF EXISTS `patientvitals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patientvitals` (
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientvitals`
--

LOCK TABLES `patientvitals` WRITE;
/*!40000 ALTER TABLE `patientvitals` DISABLE KEYS */;
INSERT INTO `patientvitals` VALUES (1,2,70.50,175.00,'120/80',70,36.50,180.00,90.00,16,'2024-02-01'),(2,4,65.00,168.00,'110/70',72,37.00,160.00,85.00,18,'2024-02-05'),(3,5,75.00,180.00,'130/85/130/85',70,36.80,190.00,95.00,15,'2024-01-15'),(4,6,80.00,185.00,'125/80',75,36.70,170.00,88.00,17,'2024-02-10'),(5,7,90.00,178.00,'135/85',70,37.20,200.00,100.00,14,'2024-03-01'),(9,15,74.00,159.00,'120/80',120,37.00,120.00,90.00,20,'2024-05-17'),(10,17,77.00,174.00,'110/75',75,37.00,120.00,80.00,24,'2024-05-17'),(11,18,78.00,174.00,'110/80',70,37.00,140.00,90.00,20,'2024-05-18');
/*!40000 ALTER TABLE `patientvitals` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-18 12:48:54
