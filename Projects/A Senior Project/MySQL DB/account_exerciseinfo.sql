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
-- Table structure for table `exerciseinfo`
--

DROP TABLE IF EXISTS `exerciseinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exerciseinfo` (
  `exerciseId` int NOT NULL AUTO_INCREMENT,
  `ExPlanid` int DEFAULT NULL,
  `ExName` varchar(100) DEFAULT NULL,
  `MuscleGroup` varchar(100) DEFAULT NULL,
  `ReqEquipment` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`exerciseId`),
  KEY `ExPlanid` (`ExPlanid`),
  CONSTRAINT `exerciseinfo_ibfk_1` FOREIGN KEY (`ExPlanid`) REFERENCES `exerciseplans` (`ExPlanId`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exerciseinfo`
--

LOCK TABLES `exerciseinfo` WRITE;
/*!40000 ALTER TABLE `exerciseinfo` DISABLE KEYS */;
INSERT INTO `exerciseinfo` VALUES (1,1,'Running','Legs','Running Shoes'),(2,1,'Jumping Jacks','Full Body','None'),(3,1,'Cycling','Legs','Bicycle'),(4,2,'Bench Press','Chest, Arms','Barbell, Bench'),(5,2,'Deadlifts','Back, Legs','Barbell, Weight Plates'),(6,2,'Squats','Legs, Glutes','Barbell, Squat Rack'),(7,3,'Yoga Tree Pose','Core, Balance','Yoga Mat'),(8,3,'Forward Fold','Hamstrings, Lower Back','None'),(9,3,'Cobra Pose','Back, Chest','None'),(10,4,'Burpees','Full Body','None'),(11,4,'Mountain Climbers','Core','None'),(12,4,'Box Jumps','Legs','Plyo Box'),(13,6,'Planks','Core','None'),(14,6,'Russian Twists','Core, Obliques','Medicine Ball'),(15,6,'Leg Raises','Core, Lower Abs','None');
/*!40000 ALTER TABLE `exerciseinfo` ENABLE KEYS */;
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
