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
-- Table structure for table `nutritionsinfo`
--

DROP TABLE IF EXISTS `nutritionsinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nutritionsinfo` (
  `FoodId` int NOT NULL AUTO_INCREMENT,
  `PlanId` int DEFAULT NULL,
  `FoodName` varchar(255) DEFAULT NULL,
  `Sugar` decimal(8,2) DEFAULT NULL,
  `Calories` decimal(8,2) DEFAULT NULL,
  `Protein` decimal(8,2) DEFAULT NULL,
  `Carbohydrates` decimal(8,2) DEFAULT NULL,
  `Fat` decimal(8,2) DEFAULT NULL,
  `Fiber` decimal(8,2) DEFAULT NULL,
  `ServingSize` varchar(50) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`FoodId`),
  KEY `PlanId` (`PlanId`),
  CONSTRAINT `nutritionsinfo_ibfk_1` FOREIGN KEY (`PlanId`) REFERENCES `nutritionplans` (`PlanID`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nutritionsinfo`
--

LOCK TABLES `nutritionsinfo` WRITE;
/*!40000 ALTER TABLE `nutritionsinfo` DISABLE KEYS */;
INSERT INTO `nutritionsinfo` VALUES (17,101,'Avocado',0.00,160.00,2.00,9.00,15.00,7.00,'1 whole (201g)','Rich in healthy fats and low in carbohydrates'),(18,101,'Salmon',0.00,208.00,22.00,0.00,13.00,0.00,'3 ounces (85g)','High in omega-3 fatty acids and protein'),(19,101,'Eggs',1.00,78.00,6.00,1.00,5.00,0.00,'1 large egg (50g)','Excellent source of protein and healthy fats'),(20,101,'Coconut Oil',0.00,117.00,0.00,0.00,13.60,0.00,'1 tablespoon (13.6g)','Rich in medium-chain triglycerides (MCTs)'),(21,102,'Spinach',0.10,7.00,0.90,1.10,0.10,0.70,'1 cup (30g)','High in vitamins and minerals, low in calories'),(22,102,'Quinoa',0.90,222.00,8.10,39.40,3.60,5.20,'1 cup (185g) cooked','Complete protein source with essential amino acids'),(23,102,'Lentils',3.00,230.00,18.00,40.00,1.00,16.00,'1 cup (198g) cooked','High in protein, fiber, and iron'),(24,102,'Almonds',1.40,207.00,7.60,7.60,18.60,4.00,'1 ounce (28g)','Rich in healthy fats, protein, and fiber'),(25,103,'Chicken Breast',0.00,165.00,31.00,0.00,3.60,0.00,'3 ounces (85g)','Lean protein source with minimal carbohydrates'),(26,103,'Broccoli',1.50,31.00,2.60,6.00,0.30,2.40,'1 cup (91g) chopped','High in fiber and vitamins, low in calories'),(27,103,'Salmon',0.00,208.00,22.00,0.00,13.00,0.00,'3 ounces (85g)','High in omega-3 fatty acids and protein'),(28,103,'Cauliflower',2.00,25.00,2.00,5.00,0.10,2.00,'1 cup (107g) chopped','Low-carb alternative to grains and starches'),(29,106,'Olive Oil',0.00,119.00,0.00,0.00,13.50,0.00,'1 tablespoon (13.5g)','Heart-healthy fat source with antioxidants'),(30,106,'Whole Grain Bread',2.00,128.00,4.50,22.50,2.00,2.00,'1 slice (25g)','Fiber-rich carbohydrate source for sustained energy'),(31,106,'Tomatoes',2.60,18.00,0.90,3.90,0.20,1.20,'1 medium tomato (123g)','Rich in vitamins, minerals, and antioxidants'),(32,106,'Feta Cheese',1.10,74.00,4.00,1.10,6.00,0.00,'1 ounce (28g)','Creamy cheese with a tangy flavor, often used in Mediterranean cuisine'),(33,109,'Grilled Chicken Breast',0.00,120.00,30.00,0.00,2.00,0.00,'3 ounces (85g)','lean source of protien for lunch and dinner');
/*!40000 ALTER TABLE `nutritionsinfo` ENABLE KEYS */;
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
