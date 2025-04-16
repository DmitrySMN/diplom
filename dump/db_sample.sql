CREATE DATABASE  IF NOT EXISTS `db17` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db17`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: db17
-- ------------------------------------------------------
-- Server version	8.0.39

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
-- Table structure for table `client_request`
--

DROP TABLE IF EXISTS `client_request`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client_request` (
  `requestId` int NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  `request_type_id` int NOT NULL,
  PRIMARY KEY (`requestId`,`request_type_id`),
  KEY `fk_client_request_request_type1_idx` (`request_type_id`),
  CONSTRAINT `fk_client_request_request_type1` FOREIGN KEY (`request_type_id`) REFERENCES `request_type` (`request_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client_request`
--

LOCK TABLES `client_request` WRITE;
/*!40000 ALTER TABLE `client_request` DISABLE KEYS */;
/*!40000 ALTER TABLE `client_request` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clients` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Surname` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Patronymic` varchar(45) NOT NULL,
  `passport` varchar(20) NOT NULL,
  `address` varchar(75) NOT NULL,
  `birth` date NOT NULL,
  `phone_number` varchar(22) NOT NULL,
  `type` varchar(120) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

LOCK TABLES `clients` WRITE;
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `deal_status`
--

DROP TABLE IF EXISTS `deal_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `deal_status` (
  `id` int NOT NULL,
  `status` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deal_status`
--

LOCK TABLES `deal_status` WRITE;
/*!40000 ALTER TABLE `deal_status` DISABLE KEYS */;
/*!40000 ALTER TABLE `deal_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `deals`
--

DROP TABLE IF EXISTS `deals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `deals` (
  `id` int NOT NULL AUTO_INCREMENT,
  `client` int NOT NULL,
  `estate` int NOT NULL,
  `employees` int NOT NULL,
  `type` varchar(10) NOT NULL,
  `transaction_date` date NOT NULL,
  `status` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_deals_clients1_idx` (`client`),
  KEY `fk_deals_estate1_idx` (`estate`),
  KEY `fk_deals_employees1_idx` (`employees`),
  KEY `fk_deals_dealtypes1_idx` (`type`),
  CONSTRAINT `fk_deals_clients1` FOREIGN KEY (`client`) REFERENCES `clients` (`id`),
  CONSTRAINT `fk_deals_employees1` FOREIGN KEY (`employees`) REFERENCES `employees` (`id`),
  CONSTRAINT `fk_deals_estate1` FOREIGN KEY (`estate`) REFERENCES `estate` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deals`
--

LOCK TABLES `deals` WRITE;
/*!40000 ALTER TABLE `deals` DISABLE KEYS */;
/*!40000 ALTER TABLE `deals` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employees` (
  `id` int NOT NULL AUTO_INCREMENT,
  `login` varchar(45) NOT NULL,
  `password` varchar(100) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Patronymic` varchar(45) NOT NULL,
  `passport` varchar(45) NOT NULL,
  `birth` date NOT NULL,
  `phone_number` varchar(22) NOT NULL,
  `address` varchar(45) NOT NULL,
  `post` int NOT NULL,
  `photo` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `login_UNIQUE` (`login`),
  KEY `fk_employees_posts1_idx` (`post`),
  CONSTRAINT `fk_employees_posts1` FOREIGN KEY (`post`) REFERENCES `posts` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` VALUES (53,'login1','785f3ec7eb32f30b90cd0fcf3657d388b5ff4297f2f9716ff66e9b69c05ddd09','asdf','asdf','asd','2222 222222','2007-04-16','+7 (222) 222 22 22','asdf',1,'');
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estate`
--

DROP TABLE IF EXISTS `estate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estate` (
  `id` int NOT NULL AUTO_INCREMENT,
  `estate_type` int NOT NULL,
  `owner_id` int NOT NULL,
  `address` varchar(45) NOT NULL,
  `square` int NOT NULL,
  `cadastral` varchar(45) NOT NULL,
  `rooms` int NOT NULL,
  `price` int NOT NULL,
  `photo` varchar(45) DEFAULT NULL,
  `status` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_estate_estate_type_idx` (`estate_type`),
  CONSTRAINT `fk_estate_estate_type` FOREIGN KEY (`estate_type`) REFERENCES `estate_type` (`id`),
  CONSTRAINT `fk_estate_owner1` FOREIGN KEY (`id`) REFERENCES `clients` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estate`
--

LOCK TABLES `estate` WRITE;
/*!40000 ALTER TABLE `estate` DISABLE KEYS */;
/*!40000 ALTER TABLE `estate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estate_type`
--

DROP TABLE IF EXISTS `estate_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estate_type` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estate_type`
--

LOCK TABLES `estate_type` WRITE;
/*!40000 ALTER TABLE `estate_type` DISABLE KEYS */;
INSERT INTO `estate_type` VALUES (1,'Квартира'),(2,'Дом'),(3,'Котедж'),(4,'Склад'),(5,'Офис');
/*!40000 ALTER TABLE `estate_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `posts`
--

DROP TABLE IF EXISTS `posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `posts` (
  `id` int NOT NULL AUTO_INCREMENT,
  `post` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idposts_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `posts`
--

LOCK TABLES `posts` WRITE;
/*!40000 ALTER TABLE `posts` DISABLE KEYS */;
INSERT INTO `posts` VALUES (1,'Администратор'),(2,'Риелтор');
/*!40000 ALTER TABLE `posts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_type`
--

DROP TABLE IF EXISTS `request_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `request_type` (
  `request_type_id` int NOT NULL,
  `request_type` varchar(45) NOT NULL,
  PRIMARY KEY (`request_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_type`
--

LOCK TABLES `request_type` WRITE;
/*!40000 ALTER TABLE `request_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `request_type` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-16 10:43:55
