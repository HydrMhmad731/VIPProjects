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
-- Table structure for table `patients`
--

DROP TABLE IF EXISTS `patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patients` (
  `Patientid` int NOT NULL AUTO_INCREMENT,
  `Firstname` varchar(45) NOT NULL,
  `Lastname` varchar(45) NOT NULL,
  `Age` varchar(45) NOT NULL,
  `DateOfBirth` varchar(45) NOT NULL,
  `Sex` varchar(10) NOT NULL,
  `Address` varchar(50) NOT NULL,
  `Email` varchar(45) NOT NULL,
  PRIMARY KEY (`Patientid`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (2,'Ali','Kassem','25','1999-5-25','Male','Beirut','ali_KM@gmail.com'),(4,'Nour','Shamas','22','2002-2-24','Female','Beirut','NShamas313@gmail.com'),(5,'Fatima','Jammal','26','1998-12-2','Female','Baalbeck','JamalFatima@gmail.com'),(6,'Mohamad','Yazbeck','19','2005-2-13','Male','Tyre','MhmdYazbeck12@gmail.com'),(7,'Kassem','Haidar','32','1992-4-8','Male','Ghazieh','Kassem_H77@gmail.com'),(15,'Hadi','Ghazal','23','2000-06-16','Male','Beirut','Hadi12Ghazal@gmail.com'),(16,'Mohamad','Farhat','21','2003-02-12','Male','Kfarsir','farhat_mhmd@gmail.com'),(17,'Mahdi','Hamdar','22','2002-01-03','Male','Jwaya','Mahdi_Hr21@gmail,com'),(18,'Hussein','Zaarour','21','2003-02-15','Male','Al Kheyam','zaarour_HZ@gmail.com');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-18 12:48:51
