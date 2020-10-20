CREATE DATABASE  IF NOT EXISTS `edscversion` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `edscversion`;
-- MySQL dump 10.13  Distrib 8.0.20, for Win64 (x86_64)
--
-- Host: localhost    Database: edscversion
-- ------------------------------------------------------
-- Server version	8.0.20

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
-- Table structure for table `academicgrading`
--

DROP TABLE IF EXISTS `academicgrading`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `academicgrading` (
  `id` int NOT NULL AUTO_INCREMENT,
  `GradingSystemName` varchar(45) NOT NULL,
  `gradeName` varchar(10) NOT NULL DEFAULT '',
  `percentageFrom` float NOT NULL,
  `percentageTo` float NOT NULL,
  `institution_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `academicgrading`
--

LOCK TABLES `academicgrading` WRITE;
/*!40000 ALTER TABLE `academicgrading` DISABLE KEYS */;
/*!40000 ALTER TABLE `academicgrading` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `batch`
--

DROP TABLE IF EXISTS `batch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `batch` (
  `id` int NOT NULL AUTO_INCREMENT,
  `batchName` varchar(30) NOT NULL DEFAULT '',
  `institution_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `batch`
--

LOCK TABLES `batch` WRITE;
/*!40000 ALTER TABLE `batch` DISABLE KEYS */;
INSERT INTO `batch` VALUES (1,'Batch 1',2),(2,'Batch 2',2),(3,'Batch 3',2),(4,'Batch 4',2),(6,'Batch 1',3);
/*!40000 ALTER TABLE `batch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `class`
--

DROP TABLE IF EXISTS `class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `class` (
  `id` int NOT NULL AUTO_INCREMENT,
  `className` varchar(30) NOT NULL DEFAULT '',
  `institution_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `class`
--

LOCK TABLES `class` WRITE;
/*!40000 ALTER TABLE `class` DISABLE KEYS */;
INSERT INTO `class` VALUES (1,'Class 1',2),(2,'Class 2',2),(3,'Class 3',2),(6,'Class 4',2),(7,'Class 1',3);
/*!40000 ALTER TABLE `class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `classroutine`
--

DROP TABLE IF EXISTS `classroutine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `classroutine` (
  `id` int NOT NULL AUTO_INCREMENT,
  `staff_id` int NOT NULL,
  `subjectManager_id` int NOT NULL,
  `day` varchar(10) NOT NULL DEFAULT '',
  `timeStart` time NOT NULL,
  `timeEnd` time NOT NULL,
  `roomNumber` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `classroutine`
--

LOCK TABLES `classroutine` WRITE;
/*!40000 ALTER TABLE `classroutine` DISABLE KEYS */;
INSERT INTO `classroutine` VALUES (1,5,1,'Sunday','10:00:00','11:00:00',1),(2,5,2,'Sunday','11:00:00','12:00:00',1);
/*!40000 ALTER TABLE `classroutine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `designations`
--

DROP TABLE IF EXISTS `designations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `designations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `designationName` varchar(60) NOT NULL,
  `institution_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `designations`
--

LOCK TABLES `designations` WRITE;
/*!40000 ALTER TABLE `designations` DISABLE KEYS */;
INSERT INTO `designations` VALUES (1,'Owner',1),(2,'Teacher',2),(3,'Head Teacher',2),(4,'Principle',2);
/*!40000 ALTER TABLE `designations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entryinformation`
--

DROP TABLE IF EXISTS `entryinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `entryinformation` (
  `id` int NOT NULL,
  `entryBy_id` int NOT NULL,
  `entryDate` datetime NOT NULL,
  `isActive` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entryinformation`
--

LOCK TABLES `entryinformation` WRITE;
/*!40000 ALTER TABLE `entryinformation` DISABLE KEYS */;
INSERT INTO `entryinformation` VALUES (1,1,'2020-05-10 00:00:00',1),(2,1,'2020-05-10 00:00:00',1),(3,1,'2020-05-11 11:27:17',1),(5,1,'2020-05-12 16:58:55',1),(6,1,'2020-05-13 17:52:44',1),(7,2,'2020-05-13 17:54:31',1),(10,1,'2020-05-14 17:38:23',1);
/*!40000 ALTER TABLE `entryinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `examinformation`
--

DROP TABLE IF EXISTS `examinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `examinformation` (
  `id` int NOT NULL,
  `name` varchar(60) NOT NULL DEFAULT '',
  `subjectManager_id` int NOT NULL,
  `date` date NOT NULL,
  `time` time NOT NULL,
  `fullMarks` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `examinformation`
--

LOCK TABLES `examinformation` WRITE;
/*!40000 ALTER TABLE `examinformation` DISABLE KEYS */;
/*!40000 ALTER TABLE `examinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `feescollection`
--

DROP TABLE IF EXISTS `feescollection`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `feescollection` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `month` varchar(9) NOT NULL DEFAULT '',
  `year` int NOT NULL,
  `fees` double NOT NULL,
  `discount` double NOT NULL,
  `invoiceNumber` varchar(13) NOT NULL DEFAULT '',
  `date` date NOT NULL,
  `entryBy_id` int NOT NULL,
  `status` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feescollection`
--

LOCK TABLES `feescollection` WRITE;
/*!40000 ALTER TABLE `feescollection` DISABLE KEYS */;
/*!40000 ALTER TABLE `feescollection` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `institutionprofile`
--

DROP TABLE IF EXISTS `institutionprofile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `institutionprofile` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL DEFAULT '',
  `logo` varchar(300) NOT NULL DEFAULT '',
  `userContactInformation_id` int NOT NULL,
  `registrationNumber` varchar(100) DEFAULT '',
  `registrationDate` date NOT NULL,
  `isActive` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `institutionprofile`
--

LOCK TABLES `institutionprofile` WRITE;
/*!40000 ALTER TABLE `institutionprofile` DISABLE KEYS */;
INSERT INTO `institutionprofile` VALUES (1,'Match Wheel Limited','mwl.jpg',1,'00000000','2020-05-11',1),(2,'KGF Coaching','sdsd',2,'121345','2020-05-11',1),(3,'RHC Coaching Center','rhc.jpg',17,'22225555','2020-05-29',1),(4,'UCC Coaching Center','ucc.jpg',18,'22225555','2020-06-01',1);
/*!40000 ALTER TABLE `institutionprofile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `login`
--

DROP TABLE IF EXISTS `login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `login` (
  `username` varchar(60) NOT NULL DEFAULT '',
  `password` varchar(200) NOT NULL DEFAULT '',
  `userRole` varchar(20) NOT NULL DEFAULT '',
  `lastLoginDate` datetime DEFAULT NULL,
  `isLoginActive` int NOT NULL,
  `institutionProfile_id` int NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login`
--

LOCK TABLES `login` WRITE;
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` VALUES ('admin','qYPrQYvXAZrM7cgFo/C3FA==','Admin','2020-06-01 18:53:34',1,2),('mwl','qYPrQYvXAZrM7cgFo/C3FA==','Superadmin','2020-06-01 17:33:41',1,1),('staff1','DIa+MBqh9zJgAgqmQRjaSw==','Teacher','2020-05-14 15:18:46',1,2),('student1','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-06-01 18:45:27',1,3),('student2','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-06-01 19:20:24',1,2),('student3','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-05-12 17:45:07',1,2),('student4','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-05-13 00:00:00',1,2),('student5','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-05-13 18:01:45',1,2);
/*!40000 ALTER TABLE `login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payrollinformation`
--

DROP TABLE IF EXISTS `payrollinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payrollinformation` (
  `id` int NOT NULL,
  `staff_id` int NOT NULL,
  `staffType_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payrollinformation`
--

LOCK TABLES `payrollinformation` WRITE;
/*!40000 ALTER TABLE `payrollinformation` DISABLE KEYS */;
/*!40000 ALTER TABLE `payrollinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payrollmanager`
--

DROP TABLE IF EXISTS `payrollmanager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payrollmanager` (
  `id` int NOT NULL,
  `payrollInformation_id` int NOT NULL,
  `dateOfPayment` date NOT NULL,
  `month` varchar(9) NOT NULL DEFAULT '',
  `paidAmount` double NOT NULL,
  `salaryDeduction_id` int NOT NULL,
  `entryInformation_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payrollmanager`
--

LOCK TABLES `payrollmanager` WRITE;
/*!40000 ALTER TABLE `payrollmanager` DISABLE KEYS */;
/*!40000 ALTER TABLE `payrollmanager` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `result`
--

DROP TABLE IF EXISTS `result`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `result` (
  `id` int NOT NULL,
  `examInformation_id` int NOT NULL,
  `student_id` int NOT NULL,
  `marks` double NOT NULL,
  `comments` varchar(200) NOT NULL DEFAULT '',
  `entryInformation_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `result`
--

LOCK TABLES `result` WRITE;
/*!40000 ALTER TABLE `result` DISABLE KEYS */;
/*!40000 ALTER TABLE `result` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `salarydeduction`
--

DROP TABLE IF EXISTS `salarydeduction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `salarydeduction` (
  `id` int NOT NULL,
  `reason` varchar(100) NOT NULL DEFAULT '',
  `amount` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `salarydeduction`
--

LOCK TABLES `salarydeduction` WRITE;
/*!40000 ALTER TABLE `salarydeduction` DISABLE KEYS */;
/*!40000 ALTER TABLE `salarydeduction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staffattendance`
--

DROP TABLE IF EXISTS `staffattendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `staffattendance` (
  `id` int NOT NULL AUTO_INCREMENT,
  `staff_id` int NOT NULL,
  `timeIn` datetime NOT NULL,
  `timeOut` datetime DEFAULT NULL,
  `inputDevice` varchar(20) NOT NULL DEFAULT '',
  `entryBy_id` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staffattendance`
--

LOCK TABLES `staffattendance` WRITE;
/*!40000 ALTER TABLE `staffattendance` DISABLE KEYS */;
INSERT INTO `staffattendance` VALUES (1,2,'2020-05-23 06:24:09','2020-05-23 06:24:09','1',2),(2,2,'2020-05-23 06:24:09','2020-05-23 06:24:09','1',2);
/*!40000 ALTER TABLE `staffattendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staffs`
--

DROP TABLE IF EXISTS `staffs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `staffs` (
  `id` int NOT NULL,
  `username` varchar(60) NOT NULL,
  `NIDNumber` varchar(17) NOT NULL,
  `joiningDate` datetime NOT NULL,
  `designation_id` int NOT NULL,
  `entryInformation_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staffs`
--

LOCK TABLES `staffs` WRITE;
/*!40000 ALTER TABLE `staffs` DISABLE KEYS */;
INSERT INTO `staffs` VALUES (1,'mwl','000000','2020-05-14 00:00:00',1,1),(2,'admin','11110000','2020-05-01 00:00:00',2,1),(5,'staff1','199814545','2020-05-14 00:00:00',1,10);
/*!40000 ALTER TABLE `staffs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stafftype`
--

DROP TABLE IF EXISTS `stafftype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stafftype` (
  `id` int NOT NULL,
  `type` varchar(20) NOT NULL DEFAULT '',
  `salary` double DEFAULT NULL,
  `perClassRate` double DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stafftype`
--

LOCK TABLES `stafftype` WRITE;
/*!40000 ALTER TABLE `stafftype` DISABLE KEYS */;
/*!40000 ALTER TABLE `stafftype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `id` int NOT NULL,
  `username` varchar(60) NOT NULL DEFAULT '',
  `entryInformation_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `UQ__Student__F3DBC572BFA2F339` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (1,'student1',5),(2,'student2',1),(3,'student3',2),(4,'student4',6),(5,'student5',7);
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentadmission`
--

DROP TABLE IF EXISTS `studentadmission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentadmission` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `subjectManager_id` int NOT NULL,
  `monthlyFees` double NOT NULL,
  `admissionDate` date NOT NULL,
  `entryBy_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `Fk_student_idx` (`student_id`),
  CONSTRAINT `Fk_student` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentadmission`
--

LOCK TABLES `studentadmission` WRITE;
/*!40000 ALTER TABLE `studentadmission` DISABLE KEYS */;
INSERT INTO `studentadmission` VALUES (3,2,1,500,'2020-05-20',2),(4,2,2,500,'2020-05-20',2),(5,3,1,650,'2020-05-20',2),(6,3,2,650,'2020-05-20',2),(14,4,1,650,'2020-05-20',2),(15,5,5,650,'2020-05-20',2),(16,5,1,600,'2020-05-20',2);
/*!40000 ALTER TABLE `studentadmission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentattendance`
--

DROP TABLE IF EXISTS `studentattendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentattendance` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `timeIn` datetime NOT NULL,
  `timeOut` datetime DEFAULT NULL,
  `inputDevice` varchar(20) NOT NULL DEFAULT '',
  `entryBy_id` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentattendance`
--

LOCK TABLES `studentattendance` WRITE;
/*!40000 ALTER TABLE `studentattendance` DISABLE KEYS */;
INSERT INTO `studentattendance` VALUES (1,1,'2020-05-22 00:00:00',NULL,'1',1),(2,2,'2020-05-22 00:00:00',NULL,'2',2);
/*!40000 ALTER TABLE `studentattendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentsguardianinformation`
--

DROP TABLE IF EXISTS `studentsguardianinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentsguardianinformation` (
  `student_id` int NOT NULL,
  `fathersContact` varchar(13) NOT NULL DEFAULT '',
  `fathersImage` varchar(200) DEFAULT '',
  `mothersContact` varchar(13) DEFAULT '',
  `mothersImage` varchar(200) DEFAULT '',
  `guardianName` varchar(60) NOT NULL DEFAULT '',
  `guardianContact` varchar(13) NOT NULL DEFAULT '',
  `guardianImage` varchar(200) DEFAULT '',
  `guardianAddress` varchar(200) NOT NULL DEFAULT '',
  PRIMARY KEY (`student_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentsguardianinformation`
--

LOCK TABLES `studentsguardianinformation` WRITE;
/*!40000 ALTER TABLE `studentsguardianinformation` DISABLE KEYS */;
INSERT INTO `studentsguardianinformation` VALUES (1,'014741124741','string1.jpg','01452145214','sadasd.png','Anwer','12457896325','string.jpg','Dhaka, Bangladesh'),(2,'sdfs','sdfsdf','sdfsd','fsdfsd','sdfsd','sdfsd','sdf','sdff'),(3,'sdfs','sdfsdf','sdfsd','fsdfsd','sdfsd','sdfsd','sdf','sdff'),(4,'01478523698','','','','Mizan Khan','0147852369','string.jpg','Dhaka, Bangladeh'),(5,'01478523698','','01447785522','','Labib Khan','0147852369','string.jpg','Dhaka, Bangladeh');
/*!40000 ALTER TABLE `studentsguardianinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subject`
--

DROP TABLE IF EXISTS `subject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subject` (
  `id` int NOT NULL AUTO_INCREMENT,
  `subjectName` varchar(60) NOT NULL DEFAULT '',
  `subjectCode` varchar(60) DEFAULT '',
  `institution_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subject`
--

LOCK TABLES `subject` WRITE;
/*!40000 ALTER TABLE `subject` DISABLE KEYS */;
INSERT INTO `subject` VALUES (1,'Bangla','BAN001',2),(2,'English','ENG001',2),(3,'Math','MT001',2),(4,'ICT','IC001',2),(8,'Bangla','BAN001',3);
/*!40000 ALTER TABLE `subject` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subjectmanager`
--

DROP TABLE IF EXISTS `subjectmanager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subjectmanager` (
  `id` int NOT NULL AUTO_INCREMENT,
  `subject_id` int NOT NULL,
  `class_id` int NOT NULL,
  `batch_id` int NOT NULL,
  `fees` double NOT NULL,
  `session` varchar(9) DEFAULT '',
  PRIMARY KEY (`id`),
  KEY `fk_subjectManager_subject_idx` (`subject_id`),
  KEY `fk_subjectManager_class_idx` (`class_id`),
  KEY `fk_subjectManager_batch_idx` (`batch_id`),
  CONSTRAINT `fk_subjectManager_batch` FOREIGN KEY (`batch_id`) REFERENCES `batch` (`id`),
  CONSTRAINT `fk_subjectManager_class` FOREIGN KEY (`class_id`) REFERENCES `class` (`id`),
  CONSTRAINT `fk_subjectManager_subject` FOREIGN KEY (`subject_id`) REFERENCES `subject` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subjectmanager`
--

LOCK TABLES `subjectmanager` WRITE;
/*!40000 ALTER TABLE `subjectmanager` DISABLE KEYS */;
INSERT INTO `subjectmanager` VALUES (1,1,1,1,700,'2020'),(2,1,1,2,700,'2020'),(3,1,1,3,700,'2020'),(4,2,1,1,700,'2020'),(5,2,1,2,700,'2020'),(6,1,2,1,700,'2020'),(7,8,7,6,700,'2020'),(8,8,7,6,700,'2020');
/*!40000 ALTER TABLE `subjectmanager` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tracer`
--

DROP TABLE IF EXISTS `tracer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tracer` (
  `actor_id` int NOT NULL,
  `actionName` varchar(100) NOT NULL DEFAULT 'Update/Delete',
  `tableName` varchar(100) DEFAULT NULL,
  `actionApplied_id` int NOT NULL,
  `actionTime` datetime NOT NULL,
  `institution_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tracer`
--

LOCK TABLES `tracer` WRITE;
/*!40000 ALTER TABLE `tracer` DISABLE KEYS */;
INSERT INTO `tracer` VALUES (2,'Update','Class',4,'2020-05-17 13:50:51',2),(2,'Update','Class',4,'2020-05-17 13:51:02',2),(2,'Update','Class',5,'2020-05-17 13:55:41',2),(2,'Delete','Class',4,'2020-05-17 14:04:58',2),(1,'Update','Batch',5,'2020-05-17 16:18:49',2),(1,'Delete','Batch',5,'2020-05-17 16:18:57',2),(1,'Update','Subject',5,'2020-05-17 16:56:34',0),(1,'Delete','Subject',5,'2020-05-17 16:56:51',0),(2,'Update','Class',7,'2020-05-18 18:41:08',2),(2,'Update','Subject',6,'2020-05-18 19:17:09',2),(2,'Delete','Subject',6,'2020-05-18 19:17:37',2),(2,'Update','SubjectManager',7,'2020-05-18 19:24:04',2),(2,'Update','SubjectManager',7,'2020-05-18 19:24:15',2),(2,'Delete','SubjectManager',7,'2020-05-18 19:24:44',2),(2,'Update','StudentAdmission',16,'2020-05-20 16:06:50',2),(2,'Update','StudentAdmission',16,'2020-05-20 16:09:39',2),(2,'Update','StudentAdmission',16,'2020-05-20 16:11:01',2),(2,'Update','StudentAdmission',1,'2020-05-20 16:11:29',2),(2,'Update','StudentAdmission',1,'2020-05-20 16:12:05',2),(2,'Update','StudentAdmission',1,'2020-05-20 16:12:11',2),(2,'Update','StudentAdmission',2,'2020-05-20 16:12:20',2),(2,'Update','StudentAdmission',2,'2020-05-20 16:12:23',2),(2,'Update','StudentAdmission',4,'2020-05-20 16:37:20',2),(2,'Update','StudentAdmission',3,'2020-05-20 16:38:38',2),(2,'Update','StudentAttendance',3,'2020-05-22 17:58:42',2),(2,'Delete','StudentAttendance',3,'2020-05-22 18:02:56',2),(1,'Update','Class',8,'2020-05-29 16:14:21',3),(1,'Update','Subject',7,'2020-05-29 16:16:28',3),(1,'Delete','SubjectManager',19,'2020-05-29 17:37:40',3),(2,'Delete','SubjectManager',27,'2020-05-29 17:42:51',2),(1,'Delete','SubjectManager',28,'2020-05-29 17:45:55',3);
/*!40000 ALTER TABLE `tracer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usercontactinformation`
--

DROP TABLE IF EXISTS `usercontactinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usercontactinformation` (
  `id` int NOT NULL AUTO_INCREMENT,
  `contact1` varchar(13) DEFAULT NULL,
  `contact2` varchar(13) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  `address` varchar(300) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usercontactinformation`
--

LOCK TABLES `usercontactinformation` WRITE;
/*!40000 ALTER TABLE `usercontactinformation` DISABLE KEYS */;
INSERT INTO `usercontactinformation` VALUES (1,'00000000000','0000000000','info@matchwheelltd.com','Dhaka'),(2,'01111111111','0000000000',NULL,'dhakaa'),(11,'01478523698','02113213112','rakib@gmail.com','Dhaka, Bangladesh'),(12,'01474125874','','mim@gmail.com','Dhaka, Bangladesh'),(13,'01474125874','','mim@gmail.com','Dhaka, Bangladesh'),(16,'01642090363','00001111222','hridoy@gmail.com','Dhaka, Bangladesh'),(17,'01478523698','14785236987','rhc@gmail.com','Dhaka, Bangladesh'),(18,'01478523698','14785236987','ucc@gmail.com','Dhaka, Bangladesh');
/*!40000 ALTER TABLE `usercontactinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userpersonalinformation`
--

DROP TABLE IF EXISTS `userpersonalinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userpersonalinformation` (
  `username` varchar(60) NOT NULL,
  `fullName` varchar(60) NOT NULL DEFAULT '',
  `fathersName` varchar(60) NOT NULL DEFAULT '',
  `mothersName` varchar(60) DEFAULT '',
  `gender` varchar(6) NOT NULL DEFAULT '',
  `dateOfBirth` date NOT NULL,
  `image` varchar(300) DEFAULT NULL,
  `userContactInformation_id` int NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userpersonalinformation`
--

LOCK TABLES `userpersonalinformation` WRITE;
/*!40000 ALTER TABLE `userpersonalinformation` DISABLE KEYS */;
INSERT INTO `userpersonalinformation` VALUES ('admin','Md Admin Islam','Md Hossain','Mst Kanta ','male','1998-05-14',NULL,2),('mwl','Md Rakibul Hasan','Md Anwer Hossen','Mst Ronjona Begum','male','1998-05-22',NULL,1),('staff1','Hridoy Khan ','Anwer Hossain','Ronjona Begum','male','2010-05-11','string.jpg',16),('student1','Rakibul Hasan ','Anwer Hossain','Ronjona Begum','male','2010-05-11','string.jpg',11),('student2','Raisul Islam','Robiul Islam','Morzina ','male','2020-05-10','vcbcvb',1),('student3','Kamrul','Kabir','Mohona','male','2020-05-10','vcbcvb',2),('student4','Mim Nur ','Mizan Khan','Nupur Hasan','female','2000-05-13','string.jpg',12),('student5','Liza Akter','Labib Khan','Lamia Hasan','female','2000-05-13','string.jpg',13);
/*!40000 ALTER TABLE `userpersonalinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `vw_academicgrading`
--

DROP TABLE IF EXISTS `vw_academicgrading`;
/*!50001 DROP VIEW IF EXISTS `vw_academicgrading`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_academicgrading` AS SELECT 
 1 AS `id`,
 1 AS `GradingSystemName`,
 1 AS `gradeName`,
 1 AS `percentageFrom`,
 1 AS `percentageTo`,
 1 AS `institution_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_batch`
--

DROP TABLE IF EXISTS `vw_batch`;
/*!50001 DROP VIEW IF EXISTS `vw_batch`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_batch` AS SELECT 
 1 AS `id`,
 1 AS `batchName`,
 1 AS `institution_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_class`
--

DROP TABLE IF EXISTS `vw_class`;
/*!50001 DROP VIEW IF EXISTS `vw_class`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_class` AS SELECT 
 1 AS `id`,
 1 AS `className`,
 1 AS `institution_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_classroutine`
--

DROP TABLE IF EXISTS `vw_classroutine`;
/*!50001 DROP VIEW IF EXISTS `vw_classroutine`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_classroutine` AS SELECT 
 1 AS `id`,
 1 AS `staff_id`,
 1 AS `subjectManager_id`,
 1 AS `day`,
 1 AS `timeStart`,
 1 AS `timeEnd`,
 1 AS `roomNumber`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_feescollection`
--

DROP TABLE IF EXISTS `vw_feescollection`;
/*!50001 DROP VIEW IF EXISTS `vw_feescollection`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_feescollection` AS SELECT 
 1 AS `id`,
 1 AS `student_id`,
 1 AS `month`,
 1 AS `year`,
 1 AS `fees`,
 1 AS `discount`,
 1 AS `invoiceNumber`,
 1 AS `date`,
 1 AS `entryBy_id`,
 1 AS `status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_staffattendance`
--

DROP TABLE IF EXISTS `vw_staffattendance`;
/*!50001 DROP VIEW IF EXISTS `vw_staffattendance`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_staffattendance` AS SELECT 
 1 AS `id`,
 1 AS `staff_id`,
 1 AS `timeIn`,
 1 AS `timeOut`,
 1 AS `inputDevice`,
 1 AS `entryBy_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_studentadmission`
--

DROP TABLE IF EXISTS `vw_studentadmission`;
/*!50001 DROP VIEW IF EXISTS `vw_studentadmission`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_studentadmission` AS SELECT 
 1 AS `id`,
 1 AS `student_id`,
 1 AS `subjectManager_id`,
 1 AS `monthlyFees`,
 1 AS `admissionDate`,
 1 AS `entryBy_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_studentattendance`
--

DROP TABLE IF EXISTS `vw_studentattendance`;
/*!50001 DROP VIEW IF EXISTS `vw_studentattendance`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_studentattendance` AS SELECT 
 1 AS `id`,
 1 AS `student_id`,
 1 AS `timeIn`,
 1 AS `timeOut`,
 1 AS `inputDevice`,
 1 AS `entryBy_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_subject`
--

DROP TABLE IF EXISTS `vw_subject`;
/*!50001 DROP VIEW IF EXISTS `vw_subject`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_subject` AS SELECT 
 1 AS `id`,
 1 AS `subjectName`,
 1 AS `subjectCode`,
 1 AS `institution_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_subjectmanager`
--

DROP TABLE IF EXISTS `vw_subjectmanager`;
/*!50001 DROP VIEW IF EXISTS `vw_subjectmanager`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_subjectmanager` AS SELECT 
 1 AS `id`,
 1 AS `subject_id`,
 1 AS `class_id`,
 1 AS `batch_id`,
 1 AS `subjectName`,
 1 AS `SubjectCode`,
 1 AS `className`,
 1 AS `batchName`,
 1 AS `fees`,
 1 AS `session`,
 1 AS `institution_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_tracer`
--

DROP TABLE IF EXISTS `vw_tracer`;
/*!50001 DROP VIEW IF EXISTS `vw_tracer`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_tracer` AS SELECT 
 1 AS `actor_id`,
 1 AS `actionName`,
 1 AS `tableName`,
 1 AS `actionApplied_id`,
 1 AS `actionTime`,
 1 AS `institution_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `vw_academicgrading`
--

/*!50001 DROP VIEW IF EXISTS `vw_academicgrading`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_academicgrading` AS select `academicgrading`.`id` AS `id`,`academicgrading`.`GradingSystemName` AS `GradingSystemName`,`academicgrading`.`gradeName` AS `gradeName`,`academicgrading`.`percentageFrom` AS `percentageFrom`,`academicgrading`.`percentageTo` AS `percentageTo`,`academicgrading`.`institution_id` AS `institution_id` from `academicgrading` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_batch`
--

/*!50001 DROP VIEW IF EXISTS `vw_batch`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_batch` AS select `batch`.`id` AS `id`,`batch`.`batchName` AS `batchName`,`batch`.`institution_id` AS `institution_id` from `batch` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_class`
--

/*!50001 DROP VIEW IF EXISTS `vw_class`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_class` AS select `class`.`id` AS `id`,`class`.`className` AS `className`,`class`.`institution_id` AS `institution_id` from `class` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_classroutine`
--

/*!50001 DROP VIEW IF EXISTS `vw_classroutine`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_classroutine` AS select `classroutine`.`id` AS `id`,`classroutine`.`staff_id` AS `staff_id`,`classroutine`.`subjectManager_id` AS `subjectManager_id`,`classroutine`.`day` AS `day`,`classroutine`.`timeStart` AS `timeStart`,`classroutine`.`timeEnd` AS `timeEnd`,`classroutine`.`roomNumber` AS `roomNumber` from (`classroutine` join `subjectmanager`) where (`classroutine`.`subjectManager_id` = `subjectmanager`.`id`) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_feescollection`
--

/*!50001 DROP VIEW IF EXISTS `vw_feescollection`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_feescollection` AS select `feescollection`.`id` AS `id`,`feescollection`.`student_id` AS `student_id`,`feescollection`.`month` AS `month`,`feescollection`.`year` AS `year`,`feescollection`.`fees` AS `fees`,`feescollection`.`discount` AS `discount`,`feescollection`.`invoiceNumber` AS `invoiceNumber`,`feescollection`.`date` AS `date`,`feescollection`.`entryBy_id` AS `entryBy_id`,`feescollection`.`status` AS `status` from `feescollection` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_staffattendance`
--

/*!50001 DROP VIEW IF EXISTS `vw_staffattendance`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_staffattendance` AS select `staffattendance`.`id` AS `id`,`staffattendance`.`staff_id` AS `staff_id`,`staffattendance`.`timeIn` AS `timeIn`,`staffattendance`.`timeOut` AS `timeOut`,`staffattendance`.`inputDevice` AS `inputDevice`,`staffattendance`.`entryBy_id` AS `entryBy_id` from `staffattendance` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_studentadmission`
--

/*!50001 DROP VIEW IF EXISTS `vw_studentadmission`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_studentadmission` AS select `studentadmission`.`id` AS `id`,`studentadmission`.`student_id` AS `student_id`,`studentadmission`.`subjectManager_id` AS `subjectManager_id`,`studentadmission`.`monthlyFees` AS `monthlyFees`,`studentadmission`.`admissionDate` AS `admissionDate`,`studentadmission`.`entryBy_id` AS `entryBy_id` from `studentadmission` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_studentattendance`
--

/*!50001 DROP VIEW IF EXISTS `vw_studentattendance`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_studentattendance` AS select `studentattendance`.`id` AS `id`,`studentattendance`.`student_id` AS `student_id`,`studentattendance`.`timeIn` AS `timeIn`,`studentattendance`.`timeOut` AS `timeOut`,`studentattendance`.`inputDevice` AS `inputDevice`,`studentattendance`.`entryBy_id` AS `entryBy_id` from `studentattendance` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_subject`
--

/*!50001 DROP VIEW IF EXISTS `vw_subject`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_subject` AS select `subject`.`id` AS `id`,`subject`.`subjectName` AS `subjectName`,`subject`.`subjectCode` AS `subjectCode`,`subject`.`institution_id` AS `institution_id` from `subject` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_subjectmanager`
--

/*!50001 DROP VIEW IF EXISTS `vw_subjectmanager`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_subjectmanager` AS select `subjectmanager`.`id` AS `id`,`subjectmanager`.`subject_id` AS `subject_id`,`subjectmanager`.`class_id` AS `class_id`,`subjectmanager`.`batch_id` AS `batch_id`,`subject`.`subjectName` AS `subjectName`,`subject`.`subjectCode` AS `SubjectCode`,`class`.`className` AS `className`,`batch`.`batchName` AS `batchName`,`subjectmanager`.`fees` AS `fees`,`subjectmanager`.`session` AS `session`,`subject`.`institution_id` AS `institution_id` from (((`subjectmanager` join `subject`) join `class`) join `batch`) where ((`subjectmanager`.`subject_id` = `subject`.`id`) and (`subjectmanager`.`class_id` = `class`.`id`) and (`subjectmanager`.`batch_id` = `batch`.`id`) and (`subject`.`institution_id` = `class`.`institution_id`) and (`subject`.`institution_id` = `batch`.`institution_id`) and (`class`.`institution_id` = `batch`.`institution_id`)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_tracer`
--

/*!50001 DROP VIEW IF EXISTS `vw_tracer`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_tracer` AS select `tracer`.`actor_id` AS `actor_id`,`tracer`.`actionName` AS `actionName`,`tracer`.`tableName` AS `tableName`,`tracer`.`actionApplied_id` AS `actionApplied_id`,`tracer`.`actionTime` AS `actionTime`,`tracer`.`institution_id` AS `institution_id` from `tracer` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-06-06 17:55:49
