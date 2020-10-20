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
  `gradingSystem_id` int NOT NULL,
  `gradeName` varchar(10) NOT NULL DEFAULT '',
  `percentageFrom` float NOT NULL,
  `percentageTo` float NOT NULL,
  `gpa` float NOT NULL,
  `EntryBy_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_AcademicGradingtoGradingSystem_idx` (`gradingSystem_id`),
  CONSTRAINT `FK_AcademicGradingtoGradingSystem` FOREIGN KEY (`gradingSystem_id`) REFERENCES `gradingsystem` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `academicgrading`
--

LOCK TABLES `academicgrading` WRITE;
/*!40000 ALTER TABLE `academicgrading` DISABLE KEYS */;
INSERT INTO `academicgrading` VALUES (1,1,'A+',80,100,5,2),(2,1,'A',70,79,4,2),(3,1,'A-',60,69,3.5,2),(4,1,'B',50,59,3,2),(5,1,'B-',40,49,2.5,2),(6,1,'F',0,39,0,2);
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `classroutine`
--

LOCK TABLES `classroutine` WRITE;
/*!40000 ALTER TABLE `classroutine` DISABLE KEYS */;
INSERT INTO `classroutine` VALUES (3,5,1,'Saturday','10:30:00','11:30:00',101),(4,5,2,'Monday','11:30:30','12:30:00',102);
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
  `id` int NOT NULL AUTO_INCREMENT,
  `examName` varchar(60) NOT NULL DEFAULT '',
  `institution_id` int NOT NULL,
  `EntryBy_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `examinformation`
--

LOCK TABLES `examinformation` WRITE;
/*!40000 ALTER TABLE `examinformation` DISABLE KEYS */;
INSERT INTO `examinformation` VALUES (1,'CLass Test 1',2,2),(2,'CLass Test 2',2,2),(3,'Mid Term',2,2),(4,'Final Exam',2,2),(5,'Class Test 3',2,2),(6,'Class Test 4',2,2);
/*!40000 ALTER TABLE `examinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exammanager`
--

DROP TABLE IF EXISTS `exammanager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exammanager` (
  `id` int NOT NULL AUTO_INCREMENT,
  `examInformation_id` int NOT NULL,
  `subjectManager_id` int NOT NULL,
  `gradingSystem_id` int NOT NULL,
  `fullMarks` float NOT NULL,
  `date` date NOT NULL,
  `timeStart` time NOT NULL,
  `timeEnd` time NOT NULL,
  `roomNumber` varchar(50) NOT NULL,
  `year` int NOT NULL,
  `EntryBy_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exammanager`
--

LOCK TABLES `exammanager` WRITE;
/*!40000 ALTER TABLE `exammanager` DISABLE KEYS */;
INSERT INTO `exammanager` VALUES (12,1,2,1,100,'2020-06-10','11:00:00','12:00:00','102',2020,2),(13,1,1,1,100,'2020-06-10','13:00:00','15:00:00','102',2020,2),(14,2,3,1,100,'2020-06-10','00:00:00','00:00:00','102',2020,2),(15,2,4,1,100,'2020-06-15','13:00:00','14:00:00','102',2020,2),(16,1,2,1,100,'2019-06-10','11:00:00','12:00:00','102',2019,2);
/*!40000 ALTER TABLE `exammanager` ENABLE KEYS */;
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
  `remain` float NOT NULL,
  `invoiceNumber` varchar(13) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  `entryBy_id` int NOT NULL,
  `status` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feescollection`
--

LOCK TABLES `feescollection` WRITE;
/*!40000 ALTER TABLE `feescollection` DISABLE KEYS */;
INSERT INTO `feescollection` VALUES (41,2,'may',2020,400,100,0,'INV000000001','2020-06-10 12:27:46',2,'Paid'),(58,2,'June',2020,0,0,1000,'INV000000042','2020-06-10 13:40:01',2,'Unpaid'),(59,3,'June',2020,0,0,1300,'INV000000059','2020-06-10 13:40:02',2,'Unpaid'),(60,4,'June',2020,0,0,650,'INV000000060','2020-06-10 13:40:03',2,'Unpaid'),(61,5,'June',2020,0,0,1250,'INV000000061','2020-06-10 13:40:03',2,'Unpaid');
/*!40000 ALTER TABLE `feescollection` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gradingsystem`
--

DROP TABLE IF EXISTS `gradingsystem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `gradingsystem` (
  `id` int NOT NULL AUTO_INCREMENT,
  `systemName` varchar(45) NOT NULL,
  `institution_id` int NOT NULL,
  `EntryBy_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gradingsystem`
--

LOCK TABLES `gradingsystem` WRITE;
/*!40000 ALTER TABLE `gradingsystem` DISABLE KEYS */;
INSERT INTO `gradingsystem` VALUES (1,'Gpa Based Grading',2,2),(3,'Gpa-4 Based Grading',2,2);
/*!40000 ALTER TABLE `gradingsystem` ENABLE KEYS */;
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
INSERT INTO `login` VALUES ('admin','qYPrQYvXAZrM7cgFo/C3FA==','Admin','2020-06-15 11:55:52',1,2),('mwl','qYPrQYvXAZrM7cgFo/C3FA==','Superadmin','2020-06-08 14:30:01',1,1),('staff1','DIa+MBqh9zJgAgqmQRjaSw==','Teacher','2020-05-14 15:18:46',1,2),('student1','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-06-01 18:45:27',1,3),('student2','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-06-07 17:00:03',1,2),('student3','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-05-12 17:45:07',1,2),('student4','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-05-13 00:00:00',1,2),('student5','qYPrQYvXAZrM7cgFo/C3FA==','Student','2020-05-13 18:01:45',1,2);
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
  `id` int NOT NULL AUTO_INCREMENT,
  `examManager_id` int NOT NULL,
  `student_id` int NOT NULL,
  `marks` double NOT NULL,
  `comments` varchar(200) DEFAULT NULL,
  `date` datetime NOT NULL,
  `EntryBy_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `result`
--

LOCK TABLES `result` WRITE;
/*!40000 ALTER TABLE `result` DISABLE KEYS */;
INSERT INTO `result` VALUES (3,12,2,76,NULL,'2020-06-13 14:29:37',2),(4,12,3,76,NULL,'2020-06-13 14:29:37',2),(5,13,2,96,NULL,'2020-06-13 14:30:05',2),(6,13,3,55,NULL,'2020-06-13 14:30:05',2),(7,13,4,88,NULL,'2020-06-13 14:30:06',2),(8,13,5,20,NULL,'2020-06-13 14:30:06',2),(9,16,2,0,NULL,'2020-06-15 16:42:17',2),(10,16,3,0,NULL,'2020-06-15 16:42:22',2);
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
INSERT INTO `studentadmission` VALUES (3,2,1,500,'2020-04-20',2),(4,2,2,500,'2020-05-20',2),(5,3,1,650,'2020-05-20',2),(6,3,2,650,'2020-05-20',2),(14,4,1,650,'2020-05-20',2),(15,5,5,650,'2020-05-20',2),(16,5,1,600,'2020-05-20',2);
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
INSERT INTO `studentsguardianinformation` VALUES (1,'014741124741','string1.jpg','01452145214','sadasd.png','Anwer Hossain','12457896325','string.jpg','Dhaka, Bangladesh'),(2,'014741124742','string2.jpg','01452145215','sadasd1.png','Hasan Kabir','12457896325','string.jpg','Dhaka, Bangladesh'),(3,'014741124743','string3.jpg','01452145216','sadasd2.png','Mariful Hasan','12457896326','string.jpg','Dhaka, Bangladesh'),(4,'01478523698','string4.jpg','01452145217','sadasd3.png','Mizan Khan','0147852369','string.jpg','Dhaka, Bangladeh'),(5,'01478523698','string5.jpg','01452145218','sadasd4.png','Labib Khan','0147852369','string.jpg','Dhaka, Bangladeh');
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
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
  `id` int NOT NULL AUTO_INCREMENT,
  `actor_id` int NOT NULL,
  `actionName` varchar(100) NOT NULL DEFAULT 'Update/Delete',
  `tableName` varchar(100) DEFAULT NULL,
  `actionApplied_id` int NOT NULL,
  `actionTime` datetime NOT NULL,
  `institution_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tracer`
--

LOCK TABLES `tracer` WRITE;
/*!40000 ALTER TABLE `tracer` DISABLE KEYS */;
INSERT INTO `tracer` VALUES (1,2,'Update','Class',4,'2020-05-17 13:50:51',2),(2,2,'Update','Class',4,'2020-05-17 13:51:02',2),(3,2,'Update','Class',5,'2020-05-17 13:55:41',2),(4,2,'Delete','Class',4,'2020-05-17 14:04:58',2),(5,1,'Update','Batch',5,'2020-05-17 16:18:49',2),(6,1,'Delete','Batch',5,'2020-05-17 16:18:57',2),(7,1,'Update','Subject',5,'2020-05-17 16:56:34',0),(8,1,'Delete','Subject',5,'2020-05-17 16:56:51',0),(9,2,'Update','Class',7,'2020-05-18 18:41:08',2),(10,2,'Update','Subject',6,'2020-05-18 19:17:09',2),(11,2,'Delete','Subject',6,'2020-05-18 19:17:37',2),(12,2,'Update','SubjectManager',7,'2020-05-18 19:24:04',2),(13,2,'Update','SubjectManager',7,'2020-05-18 19:24:15',2),(14,2,'Delete','SubjectManager',7,'2020-05-18 19:24:44',2),(15,2,'Update','StudentAdmission',16,'2020-05-20 16:06:50',2),(16,2,'Update','StudentAdmission',16,'2020-05-20 16:09:39',2),(17,2,'Update','StudentAdmission',16,'2020-05-20 16:11:01',2),(18,2,'Update','StudentAdmission',1,'2020-05-20 16:11:29',2),(19,2,'Update','StudentAdmission',1,'2020-05-20 16:12:05',2),(20,2,'Update','StudentAdmission',1,'2020-05-20 16:12:11',2),(21,2,'Update','StudentAdmission',2,'2020-05-20 16:12:20',2),(22,2,'Update','StudentAdmission',2,'2020-05-20 16:12:23',2),(23,2,'Update','StudentAdmission',4,'2020-05-20 16:37:20',2),(24,2,'Update','StudentAdmission',3,'2020-05-20 16:38:38',2),(25,2,'Update','StudentAttendance',3,'2020-05-22 17:58:42',2),(26,2,'Delete','StudentAttendance',3,'2020-05-22 18:02:56',2),(27,1,'Update','Class',8,'2020-05-29 16:14:21',3),(28,1,'Update','Subject',7,'2020-05-29 16:16:28',3),(29,1,'Delete','SubjectManager',19,'2020-05-29 17:37:40',3),(30,2,'Delete','SubjectManager',27,'2020-05-29 17:42:51',2),(31,1,'Delete','SubjectManager',28,'2020-05-29 17:45:55',3),(32,2,'Update','AcademicGrading',5,'2020-06-07 12:05:38',2),(33,2,'Update','FeesCollection',2,'2020-06-07 14:24:17',2),(34,2,'Update','FeesCollection',2,'2020-06-07 15:53:30',2),(35,2,'Update','FeesCollection',2,'2020-06-07 15:53:56',2),(36,2,'Update','FeesCollection',2,'2020-06-07 15:55:29',2),(37,2,'Update','Batch',7,'2020-06-08 12:27:01',2),(38,2,'Update','Batch',7,'2020-06-08 12:27:10',2),(39,2,'Update','Batch',7,'2020-06-08 12:27:15',2),(40,2,'Delete','Batch',7,'2020-06-08 12:27:34',2),(41,2,'Update','Class',9,'2020-06-08 12:36:46',2),(42,2,'Delete','Class',9,'2020-06-08 12:36:51',2),(43,2,'Update','Subject',1,'2020-06-08 12:38:05',2),(44,2,'Update','Subject',1,'2020-06-08 12:38:17',2),(45,2,'Delete','Subject',9,'2020-06-08 12:38:40',2),(46,2,'Update','GradingSystem',3,'2020-06-08 13:56:12',2),(47,2,'Update','ExamInformation',1,'2020-06-09 10:08:55',2),(48,2,'Update','ExamInformation',6,'2020-06-09 10:12:45',2),(49,2,'Update','FeesCollection',41,'2020-06-10 12:27:50',2),(50,2,'Delete','FeesCollection',53,'2020-06-10 13:36:55',2),(51,2,'Update','ExamManager',4,'2020-06-10 16:31:33',2),(52,2,'Update','ExamManager',4,'2020-06-10 16:34:07',2),(53,2,'Update','ExamManager',4,'2020-06-10 16:41:20',2),(54,2,'Update','ExamManager',4,'2020-06-10 16:42:42',2),(55,2,'Update','ExamManager',4,'2020-06-10 16:52:46',2),(56,2,'Update','ExamManager',4,'2020-06-10 16:55:07',2),(57,2,'Update','ExamManager',4,'2020-06-10 16:59:22',2),(58,2,'Update','ExamManager',4,'2020-06-10 17:33:35',2),(59,2,'Update','ExamManager',4,'2020-06-10 17:34:40',2),(60,2,'Update','ExamManager',4,'2020-06-10 17:35:29',2),(61,2,'Update','ExamManager',4,'2020-06-10 17:50:10',2),(62,2,'Update','ExamManager',15,'2020-06-15 12:29:30',2);
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
 1 AS `gradingsystem_id`,
 1 AS `systemName`,
 1 AS `gradeName`,
 1 AS `percentageFrom`,
 1 AS `percentageTo`,
 1 AS `gpa`,
 1 AS `EntryBy_id`,
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
-- Temporary view structure for view `vw_examinformation`
--

DROP TABLE IF EXISTS `vw_examinformation`;
/*!50001 DROP VIEW IF EXISTS `vw_examinformation`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_examinformation` AS SELECT 
 1 AS `id`,
 1 AS `examName`,
 1 AS `institution_id`,
 1 AS `EntryBy_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_exammanager`
--

DROP TABLE IF EXISTS `vw_exammanager`;
/*!50001 DROP VIEW IF EXISTS `vw_exammanager`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_exammanager` AS SELECT 
 1 AS `id`,
 1 AS `examInformation_id`,
 1 AS `subjectManager_id`,
 1 AS `gradingSystem_id`,
 1 AS `fullMarks`,
 1 AS `date`,
 1 AS `timeStart`,
 1 AS `timeEnd`,
 1 AS `roomNumber`,
 1 AS `EntryBy_id`,
 1 AS `year`*/;
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
 1 AS `remain`,
 1 AS `invoiceNumber`,
 1 AS `date`,
 1 AS `entryBy_id`,
 1 AS `status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_gradingsystem`
--

DROP TABLE IF EXISTS `vw_gradingsystem`;
/*!50001 DROP VIEW IF EXISTS `vw_gradingsystem`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_gradingsystem` AS SELECT 
 1 AS `id`,
 1 AS `systemName`,
 1 AS `institution_id`,
 1 AS `EntryBy_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_result`
--

DROP TABLE IF EXISTS `vw_result`;
/*!50001 DROP VIEW IF EXISTS `vw_result`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_result` AS SELECT 
 1 AS `id`,
 1 AS `examManager_id`,
 1 AS `student_id`,
 1 AS `marks`,
 1 AS `comments`,
 1 AS `date`,
 1 AS `EntryBy_id`*/;
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
 1 AS `id`,
 1 AS `actor_id`,
 1 AS `actionName`,
 1 AS `tableName`,
 1 AS `actionApplied_id`,
 1 AS `actionTime`,
 1 AS `institution_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping routines for database 'edscversion'
--
/*!50003 DROP PROCEDURE IF EXISTS `new_procedure` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `new_procedure`(
	IN Id INT
)
BEGIN
	DELETE FROM result 
    WHERE result.id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteAcademicGrading` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteAcademicGrading`(
	IN id INT
)
BEGIN
	DELETE FROM academicgrading
    WHERE academicgrading.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteBatch` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteBatch`(
	IN id INT
)
BEGIN
	DELETE FROM batch
    WHERE batch.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteClass` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteClass`(
	IN id INT
)
BEGIN
	DELETE FROM class
    WHERE class.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteClassRoutine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteClassRoutine`(
	IN id INT
)
BEGIN
	DELETE FROM classroutine
    WHERE classroutine.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteDesignation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteDesignation`(IN `id` INT)
    NO SQL
BEGIN
	
    DELETE FROM designations
    WHERE
    designations.id = id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteExamInformation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteExamInformation`(
	IN Id INT
)
BEGIN
	DELETE FROM examinformation
    WHERE examinformation.id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteExamManager` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteExamManager`(
	IN Id INT
)
BEGIN
	DELETE FROM exammanager 
    WHERE exammanager.Id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteFeesCollection` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteFeesCollection`(
	IN id INT
)
BEGIN
	DELETE FROM feescollection
    WHERE feescollection.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteGradingSystem` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteGradingSystem`(
	IN Id INT
)
BEGIN
	DELETE FROM academicgrading
    WHERE academicgrading.gradingSystem_id = Id;
    
    DELETE FROM gradingsystem
    WHERE gradingsystem.id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteStaffAttendance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteStaffAttendance`(
	IN id INT
)
BEGIN
	DELETE FROM staffattendance
    WHERE staffattendance.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteStudentAdmission` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteStudentAdmission`(
	IN id INT
)
BEGIN
	DELETE FROM studnetadmission 
    WHERE studentadmission.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteStudentAttendance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteStudentAttendance`(
	IN id INT
)
BEGIN
	DELETE FROM studentattendance
    WHERE studentattendance.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteSubject` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteSubject`(
	IN id INT
)
BEGIN
	DELETE FROM subject
    WHERE subject.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_deleteSubjectManager` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteSubjectManager`(
	IN id INT
)
BEGIN
	DELETE FROM subjectmanager
    WHERE subjectmanager.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_getAllDesignation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllDesignation`()
    NO SQL
BEGIN
	SELECT * FROM designations;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_getAllInstitution` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllInstitution`()
    NO SQL
BEGIN
	SELECT * FROM 
    institutionprofile , usercontactinformation 
	
    WHERE 
    	institutionprofile.userContactInformation_id =
     	usercontactinformation.id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_getAllLogin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllLogin`()
    NO SQL
BEGIN
	SELECT * FROM login;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_getAllStaff` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllStaff`()
    NO SQL
BEGIN
	SELECT * FROM staffs, userpersonalinformation, usercontactinformation, 		designations, entryinformation, login
    WHERE
    staffs.username 			= userpersonalinformation.username AND 
    staffs.designation_id 		= designations.id AND
    staffs.entryInformation_id 	= entryinformation.id AND
    userpersonalinformation.
    userContactInformation_id 	= usercontactinformation.id AND
    staffs.username 			= login.username;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_getAllStudent` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllStudent`()
    NO SQL
BEGIN
	SELECT * FROM student, userpersonalinformation, usercontactinformation, 	studentsguardianinformation, entryinformation, login
    WHERE
    student.username = userpersonalinformation.username AND 
    student.id = studentsguardianinformation.student_id AND
    student.entryInformation_id = entryinformation.id AND
    userpersonalinformation.userContactInformation_id =
    usercontactinformation.id AND
    student.username = login.username;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_getAllTracer` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllTracer`()
    NO SQL
BEGIN
	SELECT * FROM tracer ;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveAcademicGrading` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveAcademicGrading`(
    IN GradingSystemId 		INT,
    IN GradeName 			VARCHAR(10),
    IN PercentageFrom		FLOAT,
    IN PercentageTo			FLOAT,
    IN Gpa					FLOAT,
    IN EntryBy_id		INT 
)
BEGIN
	INSERT INTO academicgrading
    (
		academicgrading.gradingSystem_id	,
        academicgrading.gradeName 			,
        academicgrading.percentageFrom		,
        academicgrading.percentageto		,
        academicgrading.gpa					,
        academicgrading.EntryBy_id
    )
    VALUES
    (
		GradingSystemId	,
        GradeName 			,
        PercentageFrom		,
        PercentageTo		,
        Gpa					,
        EntryBy_id
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveBatch` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveBatch`(
	IN name VARCHAR(60),
    IN institution_id INT
)
BEGIN
	INSERT INTO batch 
    (
		batch.batchName,
        batch.institution_id
    )
    VALUES
    (
		name,
        institution_id
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveClass` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveClass`(
	IN name VARCHAR(60),
    IN institution_id INT
)
BEGIN
	INSERT INTO class 
    (
		class.className,
        class.institution_id
    )
    VALUES
    (
		name,
        institution_id
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveClassRoutine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveClassRoutine`(
	IN staff_id 		INT ,
    IN subjectManger_id INT,
    IN day 				VARCHAR(10),
    IN timeStart 		TIME,
    IN timeEnd 			TIME,
    IN roomNumber 		INT
)
BEGIN
	INSERT INTO classRoutine 
    (
		staff_id, 		
		subjectManger_id, 
		day, 				
        timeStart, 		
        timeEnd, 			
        roomNumber
	)
    VALUES
    (
		staff_id, 		
        subjectManger_id, 
        day, 				
        timeStart, 		
        timeEnd, 			
        roomNumber 		
    );
    END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveDesignation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveDesignation`(IN `name` VARCHAR(60), IN `institution_id` INT)
    NO SQL
BEGIN
 	
    INSERT INTO designations
    (
        designations.designationName,
        designations.institution_id
    )
    VALUES
    (
        name,
        institution_id
    );
 
 END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveExamInformation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveExamInformation`(
	IN ExamName 		VARCHAR(60),
	IN InstitutionId 	INT,
	IN EntryBy_id 		INT
)
BEGIN
	INSERT INTO examinformation
    (
        examinformation.ExamName, 	
        examinformation.Institution_Id,
		examinformation.EntryBy_id 	
    )
    VALUES
    (
		ExamName, 	
        InstitutionId,
		EntryBy_id
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveExamManager` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveExamManager`(
	IN ExamInformationId 	INT,
    IN SubjectManagerId 	INT,
    IN GradingSystemId 		INT,
    IN FullMarks 			FLOAT,
    IN ExamDate 			DATE,
    IN TimeStart 			TIME,
    IN TimeEnd  			TIME,
    IN RoomNumber 			VARCHAR(50),
    IN ExamYear 			INT,
    IN EntryBy_id 			INT
)
BEGIN
	INSERT INTO exammanager
    (
		exammanager.ExamInformation_Id	,
		exammanager.SubjectManager_Id 	,
		exammanager.GradingSystem_Id 	,
		exammanager.FullMarks 			,
		exammanager.Date 				,
		exammanager.TimeStart			,
		exammanager.TimeEnd				,
		exammanager.RoomNumber 			,
		exammanager.Year 				,
		exammanager.EntryBy_id 		
    )
    VALUES
    (
		ExamInformationId	,
		SubjectManagerId 	,
		GradingSystemId 	,
		FullMarks 			,
		ExamDate 			,
		TimeStart 			,
		TimeEnd 			,
		RoomNumber 			,
		ExamYear 			,
		EntryBy_id 		
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_SaveFeesCollection` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SaveFeesCollection`(
	IN student_id 		INT,
    IN month 			VARCHAR(9),
    IN year 			INT,
    IN fees 			FLOAT,
    IN discount 		FLOAT,
    IN remain	 		FLOAT,
    IN invoiceNumber 	VARCHAR(13),
    IN date 			DATETIME,
    IN EntryBy_id 		INT,
    IN status 			VARCHAR(20)
)
BEGIN
	INSERT INTO feescollection
    (
		feescollection.student_id 	,
        feescollection.month 		,
        feescollection.year 		,
        feescollection.fees 		,
        feescollection.discount 	,
        feescollection.remain	 	,
        feescollection.invoiceNumber,
        feescollection.date			,
        feescollection.EntryBy_id 	,
        feescollection.status 			
    )
    VALUES
    (
		student_id 	,
        month 		,
        year 		,
        fees 		,
        discount 	,
        remain	 	,
        invoiceNumber,
        date		,
        EntryBy_id 	,
        status 		
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveGradingSystem` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveGradingSystem`(
	IN SystemName 		VARCHAR(45),
    IN InstitutionId 	INT,
    IN EntryBy_id 		INT
)
BEGIN
	INSERT INTO gradingSystem
    (
		gradingSystem.SystemName,
		gradingSystem.Institution_id,
        gradingSystem.EntryBy_id 
    )VALUES
    (
		SystemName,
        InstitutionId,
        EntryBy_id
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveInstitution` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveInstitution`(
	IN `name` VARCHAR(100), 
    IN `logo` VARCHAR(300), 
    IN `registrationDate` DATETIME, 
    IN `isActive` INT(1), 
    IN `contact1` VARCHAR(13),
    IN `contact2` VARCHAR(13), 
    IN `email` VARCHAR(150), 
    IN `address` VARCHAR(300), 
    IN `registrationNumber` VARCHAR(100)
)
    NO SQL
BEGIN

INSERT INTO usercontactinformation 
        (
            contact1,
            contact2,
         	email,
         	address
        )
        VALUES
        (
            contact1,
            contact2,
         	email,
         	address
        );  
        
        INSERT INTO institutionprofile 
        (
            name,
            logo,
         	userContactInformation_id,
         	registrationNumber,
         	registrationDate,
         	isActive
        )VALUES
        (
            name,
            logo,
         	LAST_INSERT_ID(),
         	registrationNumber,
         	registrationDate,
         	isActive
        );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveResult` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveResult`(
	IN ExamManagerId INT,
    IN StudentId 	 INT,
    IN Marks 		 FLOAT,
    IN Date 		 DATETIME,
    IN EntryBy_id 	 INT
)
BEGIN
	INSERT INTO result
    (
		result.exammanager_id,
        result.student_id,
        result.marks,
        result.date,
        result.EntryBy_id
    )
    VALUES
    (
		ExamManagerId, 
        StudentId 	 ,
        Marks 		 ,
        Date 		 ,
		EntryBy_id 	 
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveStaff` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveStaff`(IN `username` VARCHAR(60), IN `NIDNumber` VARCHAR(17), IN `JoiningDate` DATETIME, IN `designation_id` INT, IN `fullName` VARCHAR(60), IN `fathersName` VARCHAR(60), IN `mothersName` VARCHAR(60), IN `dateOfBirth` DATE, IN `gender` VARCHAR(6), IN `image` VARCHAR(300), IN `contact1` VARCHAR(13), IN `contact2` VARCHAR(13), IN `email` VARCHAR(150), IN `address` VARCHAR(300), IN `password` VARCHAR(200), IN `userRole` VARCHAR(20), IN `lastLoginDate` DATETIME, IN `isLoginActive` INT(1), IN `institutionProfile_id` INT, IN `entryBy_id` INT, IN `entryDate` DATETIME, IN `isActive` INT(1))
    NO SQL
BEGIN
START TRANSACTION;
 		
        INSERT INTO entryinformation
        (
            entryinformation.entryBy_id,
            entryinformation.entryDate,
            entryinformation.isActive
        )
        VALUES
        (
			 entryBy_id,
			 entryDate,
			 isActive
        );

		SET @entry_last_id = LAST_INSERT_ID();
        
        INSERT INTO staffs
        (
            staffs.username,
            staffs.designation_id,
            staffs.NIDNumber,
            staffs.joiningDate,
            staffs.entryInformation_id
        )
        VALUES
        (
            username,
            designation_id,
            NIDNumber,
            joiningDate,
			@entry_last_id
        );
		
		SET @student_id = LAST_INSERT_ID();
		INSERT INTO usercontactinformation
        (
            usercontactinformation.contact1,
            usercontactinformation.contact2,
            usercontactinformation.email,
            usercontactinformation.address
        )
        VALUES
        (
            contact1,
            contact2,
            email,
            address
        );

		SET @contact_last_id = LAST_INSERT_ID();
        INSERT INTO userpersonalinformation
        (
            userpersonalinformation.username,
            userpersonalinformation.fullName,
            userpersonalinformation.fathersName,
            userpersonalinformation.mothersName,
            userpersonalinformation.gender,
            userpersonalinformation.dateOfBirth,
            userpersonalinformation.image,
            userpersonalinformation.userContactInformation_id
        )
        VALUES
        (
            username,
            fullName,
            fathersName,
            mothersName,
            gender,
            dateOfBirth,
            image,
            @contact_last_id
        );

        INSERT INTO login
        (
            login.username,
            login.password,
            login.userRole,
            login.lastLoginDate,
            login.isLoginActive,
            login.institutionProfile_id
        )
        VALUES
        (
            username,
            password,
            userRole,
            lastLoginDate,
            isLoginActive,
            institutionProfile_id 
        );
		
	COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveStaffAttendance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveStaffAttendance`(
	IN user_id INT,
    IN timeIn DATETIME,
    IN timeOut DATETIME,
    IN inputDevice VARCHAR(50),
    IN entryBy_id INT
)
BEGIN
	INSERT INTO staffattendance 
    (
		staff_id,
        timeIn,
        timeOut,
        inputDevice,
        entryBy_id
    )
    VALUES
    (
		user_id,
        timeIn,
        timeOut,
        inputDevice,
        entryBy_id   
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveStudent` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveStudent`(IN `username` VARCHAR(60), IN `fullName` VARCHAR(60), IN `fathersName` VARCHAR(60), IN `mothersName` VARCHAR(60), IN `dateOfBirth` DATE, IN `gender` VARCHAR(6), IN `image` VARCHAR(300), IN `contact1` VARCHAR(13), IN `contact2` VARCHAR(13), IN `email` VARCHAR(150), IN `address` VARCHAR(300), IN `fathersContact` VARCHAR(13), IN `fathersImage` VARCHAR(200), IN `mothersContact` VARCHAR(13), IN `mothersImage` VARCHAR(200), IN `guardianName` VARCHAR(200), IN `guardianContact` VARCHAR(13), IN `guardianImage` VARCHAR(200), IN `guardianAddress` VARCHAR(200), IN `password` VARCHAR(200), IN `userRole` VARCHAR(20), IN `lastLoginDate` DATETIME, IN `isLoginActive` INT(1), IN `institutionProfile_id` INT, IN `entryBy_id` INT, IN `entryDate` DATETIME, IN `isActive` INT(1))
    NO SQL
BEGIN
START TRANSACTION;
 
        INSERT INTO entryinformation
        (
            entryinformation.entryBy_id,
            entryinformation.entryDate,
            entryinformation.isActive
        )
        VALUES
        (
			 entryBy_id,
			 entryDate,
			 isActive
        );

		SET @entry_last_id = LAST_INSERT_ID();
        INSERT INTO student
        (
            student.username,
            student.entryInformation_id
        )
        VALUES
        (
             username,
			 @entry_last_id
        );
		
		SET @student_id = LAST_INSERT_ID();
		INSERT INTO usercontactinformation
        (
            usercontactinformation.contact1,
            usercontactinformation.contact2,
            usercontactinformation.email,
            usercontactinformation.address
        )
        VALUES
        (
            contact1,
            contact2,
            email,
            address
        );

		SET @contact_last_id = LAST_INSERT_ID();
        INSERT INTO userpersonalinformation
        (
            userpersonalinformation.username,
            userpersonalinformation.fullName,
            userpersonalinformation.fathersName,
            userpersonalinformation.mothersName,
            userpersonalinformation.gender,
            userpersonalinformation.dateOfBirth,
            userpersonalinformation.image,
            userpersonalinformation.userContactInformation_id
        )
        VALUES
        (
            username,
            fullName,
            fathersName,
            mothersName,
            gender,
            dateOfBirth,
            image,
            @contact_last_id
        );

        INSERT INTO studentsguardianinformation 
        (
            studentsguardianinformation.student_id,
            studentsguardianinformation.fathersContact,
            studentsguardianinformation.fathersImage,
            studentsguardianinformation.mothersContact,
            studentsguardianinformation.mothersImage,
            studentsguardianinformation.guardianName,
            studentsguardianinformation.guardianContact,
            studentsguardianinformation.guardianImage,
            studentsguardianinformation.guardianAddress
        )
        VALUES 
        (
            @student_id,
            fathersContact,
            fathersImage,
            mothersContact,
            mothersImage,
            guardianName,
            guardianContact,
            guardianImage,
            guardianAddress
        );


        INSERT INTO login
        (
            login.username,
            login.password,
            login.userRole,
            login.lastLoginDate,
            login.isLoginActive,
            login.institutionProfile_id
        )
        VALUES
        (
            username,
            password,
            userRole,
            lastLoginDate,
            isLoginActive,
            institutionProfile_id 
        );
		
	COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveStudentAdmission` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveStudentAdmission`(
	IN student_id 		INT,
    IN subjectManager_id INT,
    IN admissionDate 	DATETIME,
    IN monthlyFees 		FLOAT,
    IN entryBy_id 		INT
)
BEGIN
	INSERT INTO studentadmission
    (
		student_id ,		
		subjectManager_id, 
		admissionDate, 	
        monthlyFees, 		
        entryBy_id 		
	)
    VALUES 
    (
		student_id ,		
        subjectManager_id, 
        admissionDate, 	
        monthlyFees, 		
        entryBy_id 		
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveStudentAttendance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveStudentAttendance`(
	IN user_id INT,
    IN timeIn DATETIME,
    IN timeOut DATETIME,
    IN inputDevice VARCHAR(50),
    IN entryBy_id INT
)
BEGIN
	INSERT INTO studentattendance 
    (
		student_id,
        timeIn,
        timeOut,
        inputDevice,
        entryBy_id
    )
    VALUES
    (
		user_id,
        timeIn,
        timeOut,
        inputDevice,
        entryBy_id   
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveSubject` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveSubject`(
	IN name VARCHAR(60),
	IN code VARCHAR(60),
    IN institution_id INT
)
BEGIN
	INSERT INTO subject
    (
		subject.subjectName,
		subject.subjectCode,
        subject.institution_id
    )
    VALUES
    (
		name,
		code,
        institution_id
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveSubjectManager` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveSubjectManager`(
	IN subject_id INT,
    IN class_id INT,
	IN batch_id INT,
	IN fees FLOAT,
	IN session INT
)
BEGIN
	INSERT INTO subjectmanager
    (
		subjectmanager.subject_id,
        subjectmanager.class_id,
        subjectmanager.batch_id,
        subjectmanager.fees,
        subjectmanager.session
    )
    VALUES
    (
		subject_id,
        class_id,
        batch_id,
        fees,
        session
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveTracer` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveTracer`(
	IN `actor_id` INT,
    IN `actionName` VARCHAR(100),
    IN `tableName` VARCHAR(100),
    IN `actionApplied_id` INT,
    IN `actionTime` DATETIME,
    IN `institution_id` INT
)
    NO SQL
BEGIN
	
    INSERT INTO tracer 
    (
        tracer.actor_id,
        tracer.actionName,
        tracer.tableName,
        tracer.actionApplied_id,
        tracer.actionTime,
        tracer.institution_id
    )
    VALUES
    (
        actor_id,
        actionName,
        tableName,
        actionApplied_id,
        actionTime,
        institution_id
    );
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_saveTracker` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_saveTracker`(
	IN actor_id 		INT,
	IN actionName 		VARCHAR(200),
	IN tableName 		VARCHAR(200),
	IN actionApplied_id INT,
	IN actionTime 		DATETIME,
	IN institution_id 	INT
)
BEGIN
	INSERT INTO tracer
    (
		tracer.actor_id, 		
        tracer.actionName, 		
        tracer.tableName, 		
        tracer.actionApplied_id, 
        tracer.actionTime, 		
        tracer.institution_id 	
    )
    VALUES
    (
		actor_id, 		
        actionName, 		
        tableName, 		
        actionApplied_id,
        actionTime, 		
		institution_id
	);  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateAcademicGrading` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateAcademicGrading`(
	IN id 					INT,
    IN GradingSystemId 	VARCHAR(45),
    IN GradeName 			VARCHAR(10),
    IN PercentageFrom		FLOAT,
    IN PercentageTo			FLOAT,
    IN Gpa					FLOAT,
    IN EntryBy_id			INT 
)
BEGIN
	UPDATE academicgrading 
    SET 
    academicgrading.gradingSystem_id	= GradingSystemId,
    academicgrading.gradeName 			= GradeName,
    academicgrading.percentageFrom		= PercentageFrom,
    academicgrading.percentageTo		= PercentageTo,
    academicgrading.gpa					= Gpa,
    academicgrading.EntryBy_id			= EntryBy_id
    WHERE academicgrading.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateBatch` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateBatch`(
	IN id INT,
	IN name VARCHAR(60),
    IN institution_id INT
)
BEGIN
	UPDATE batch 
    SET
		batch.batchName = name,
        batch.institution_id = institution_id
    WHERE
    batch.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateClass` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateClass`(
	IN id INT,
	IN name VARCHAR(60),
    IN institution_id INT
)
BEGIN
	UPDATE class 
    SET
		class.className = name,
        class.institution_id = institution_id
    WHERE
    class.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateClassRoutine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateClassRoutine`(
	IN id 				INT ,
	IN staff_id 		INT ,
    IN subjectManger_id INT,
    IN day 				VARCHAR(10),
    IN timeStart 		TIME,
    IN timeEnd 			TIME,
    IN roomNumber 		INT
)
BEGIN
	UPDATE classroutine 
    SET 
		classroutine.staff_id 			= staff_id ,		
        classroutine.subjectManger_id	= subjectManger_id,  
        classroutine.day 				= day, 					
        classroutine.timeStart 			= timeStart, 		
        classroutine.timeEnd 			= timeEnd, 				
        classroutine.roomNumber 		= roomNumber
	WHERE 
    classroutine.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateDesignation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateDesignation`(IN `id` INT, IN `name` VARCHAR(60), IN `institution_id` INT)
    NO SQL
BEGIN
	UPDATE designations
    SET
    designations.designationName = name,
    designations.institution_id  = institution_id
    
    WHERE 
    designations.id = id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateExamInformation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateExamInformation`(
	IN Id		 		INT,
	IN ExamName 		VARCHAR(60),
	IN InstitutionId 	INT,
	IN EntryBy_id 		INT
)
BEGIN
	UPDATE examinformation 
    SET 
    examinformation.ExamName 		= ExamName, 	
	examinformation.Institution_Id 	= InstitutionId,
	examinformation.EntryBy_id 		= EntryBy_id
    WHERE examinformation.id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateExamManager` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateExamManager`(
	IN Id 					INT,
	IN ExamInformationId 	INT,
    IN SubjectManagerId 	INT,
    IN GradingSystemId 		INT,
    IN FullMarks 			FLOAT,
    IN ExamDate 			DATE,
    IN TimeStart 			TIME,
    IN TimeEnd  			TIME,
    IN RoomNumber 			VARCHAR(50),
    IN ExamYear 			INT,
    IN EntryBy_id 			INT
)
BEGIN
	UPDATE exammanager
    SET 
    exammanager.ExamInformation_Id	= ExamInformationId,
    exammanager.SubjectManager_Id 	= SubjectManagerId,
    exammanager.GradingSystem_Id 	= GradingSystemId,
    exammanager.FullMarks 			= FullMarks,
    exammanager.Date 				= ExamDate,
    exammanager.TimeStart 			= TimeStart,
    exammanager.TimeEnd 			= TimeEnd,
    exammanager.RoomNumber 			= RoomNumber,
    exammanager.Year 				= ExamYear,
    exammanager.EntryBy_id 		    = EntryBy_id
    WHERE exammanager.Id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateFeesCollection` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateFeesCollection`(
	IN id 				INT,
	IN student_id 		INT,
    IN month 			VARCHAR(9),
    IN year 			INT,
    IN fees 			FLOAT,
    IN discount 		FLOAT,
    IN remain	 		FLOAT,
    IN invoiceNumber 	VARCHAR(13),
    IN date			 	DATETIME,
    IN EntryBy_id 		INT,
    IN status 			VARCHAR(20)
)
BEGIN
	UPDATE feescollection
    SET
    feescollection.student_id 	= student_id,
    feescollection.month 		= month,
    feescollection.year 		= year,
    feescollection.fees 		= fees,
    feescollection.discount 	= discount,
    feescollection.remain	 	= remain,
    feescollection.invoiceNumber= invoiceNumber,
    feescollection.date			= date,
    feescollection.EntryBy_id 	= EntryBy_id,
    feescollection.status 	    = status
    WHERE feescollection.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateGradingSystem` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateGradingSystem`(
	IN Id 				INT,
	IN SystemName 		VARCHAR(45),
    IN InstitutionId 	INT,
    IN EntryBy_id 		INT
)
BEGIN
	UPDATE gradingsystem
    SET
    gradingsystem.systemName 		= SystemName,
    gradingsystem.institution_id 	= InstitutionId,
    gradingsystem.EntryBy_id 		= EntryBy_id
    WHERE gradingsystem.id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateInstitution` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateInstitution`(IN `id` INT, IN `name` VARCHAR(100), IN `logo` VARCHAR(300), IN `registrationNumber` VARCHAR(100), IN `registrationDate` DATETIME, IN `contactinformation_id` INT, IN `contact1` VARCHAR(13), IN `contact2` VARCHAR(13), IN `email` VARCHAR(150), IN `address` VARCHAR(300), IN `isActive` INT(1))
    NO SQL
BEGIN
    
    UPDATE institutionprofile 
    SET
    institutionprofile.name	= name,
    institutionprofile.logo	= logo,
    institutionprofile.userContactInformation_id=
    userContactInformation_id,
    institutionprofile.registrationNumber=
    registrationNumber,
    institutionprofile.registrationDate			= registrationDate,
    institutionprofile.isActive					= isActive
    WHERE
    	institutionprofile.id = id;
        
    UPDATE usercontactinformation 
    SET 
        usercontactinformation.contact1 	= contact1,
        usercontactinformation.contact2 	= contact2,
        usercontactinformation.email 		= email,
        usercontactinformation.address		= address
    WHERE
    	 usercontactinformation.id = contactInformation_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateLogin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateLogin`(IN `username` VARCHAR(60), IN `password` VARCHAR(100), IN `userRole` VARCHAR(10), IN `isLoginActive` VARCHAR(1), IN `lastLoginDate` DATETIME, IN `institutionProfile_id` INT(11))
    NO SQL
BEGIN
	UPDATE login SET 
        login.password = password,
        login.userRole = userRole,
        login.isLoginActive = isLoginActive,
        login.lastLoginDate = lastloginDate,
        login.institutionProfile_id = institutionProfile_id
    WHERE login.username = username;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateResult` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateResult`(
	IN Id INT,
	IN ExamManagerId INT,
    IN StudentId 	 INT,
    IN Marks 		 FLOAT,
    IN Date 		 DATETIME,
    IN EntryBy_id 	 INT
)
BEGIN
	UPDATE result 
    SET
		result.exammanager_id = ExamManagerId,
        result.student_id	= StudentId,
        result.marks		= Marks,
        result.date			= Date,
        result.EntryBy_id 	= EntryBy_id
	WHERE 
    result.id = Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateStaff` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateStaff`(IN `id` INT, IN `username` VARCHAR(60), IN `designation_id` INT, IN `NIDNumber` VARCHAR(17), IN `fullName` VARCHAR(60), IN `fathersName` VARCHAR(60), IN `mothersName` VARCHAR(60), IN `dateOfBirth` DATE, IN `gender` VARCHAR(6), IN `image` VARCHAR(300), IN `contact1` VARCHAR(13), IN `contact2` VARCHAR(13), IN `email` VARCHAR(150), IN `address` VARCHAR(300), IN `password` VARCHAR(200), IN `userRole` VARCHAR(20), IN `lastLoginDate` DATETIME, IN `isLoginActive` INT(1), IN `institutionProfile_id` INT, IN `entryBy_id` INT(11), IN `entryDate` DATETIME, IN `isActive` INT(1), IN `contactInformation_id` INT, IN `entryInformation_id` INT)
    NO SQL
BEGIN
	START TRANSACTION;
        UPDATE usercontactinformation 
        SET
            usercontactinformation.contact1 = contact1,
            usercontactinformation.contact2 = contact2,
            usercontactinformation.email = email,
            usercontactinformation.address = address
        WHERE
        usercontactinformation.id = contactInformation_id;

        UPDATE userpersonalinformation
        SET
            userpersonalinformation.fullName =fullName,
            userpersonalinformation.fathersName = fathersName,
            userpersonalinformation.mothersName = mothersName,
            userpersonalinformation.gender = gender,
            userpersonalinformation.dateOfBirth = dateOfBirth,
            userpersonalinformation.image = image,
            userpersonalinformation.userContactInformation_id = 
            userContactInformation_id
		WHERE
        userpersonalinformation.username = username;

        UPDATE login
        SET
            login.username = username,
            login.password = password,
            login.userRole = userRole,
            login.lastLoginDate = lastLoginDate,
            login.isLoginActive = isLoginActive,
            login.institutionProfile_id = institutionProfile_id
		WHERE
        login.username = username;

        
        UPDATE entryinformation
		SET
			entryinformation.entryBy_id = entryBy_id,
            entryinformation.entryDate = entryDate ,
            entryinformation.isActive = isActive
		WHERE 
        entryinformation.id = entryInformation_id;
        

        UPDATE staffs
        SET
            staffs.username 			= username,
            staffs.designation_id 		= designation_id,
            staffs.NIDNumber 			= NIDNumber,
            staffs.joiningDate 			= joiningDate,
            staffs.entryInformation_id 	= entryInformation_id
        WHERE
        staffs.id = id;

	COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateStaffAttendance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateStaffAttendance`(
	IN id INT,
	IN user_id INT,
    IN timeIn DATETIME,
    IN timeOut DATETIME,
    IN inputDevice VARCHAR(50),
    IN entryBy_id INT
)
BEGIN
	UPDATE staffattendance
    SET
    staffattendance.student_id 	= user_id,
    staffattendance.timeIn  	= timeIn,
    staffattendance.timeOut 	= timeOut,
    staffattendance.inputDevice = inputDevice,
    staffattendance.entryBy_id 	= entryBy_id
	WHERE
    staffattendance.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateStudent` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateStudent`(IN `id` INT, IN `username` VARCHAR(60), IN `fullName` VARCHAR(60), IN `fathersName` VARCHAR(60), IN `mothersName` VARCHAR(60), IN `dateOfBirth` DATE, IN `gender` VARCHAR(6), IN `image` VARCHAR(300), IN `contact1` VARCHAR(13), IN `contact2` VARCHAR(13), IN `email` VARCHAR(150), IN `address` VARCHAR(300), IN `fathersContact` VARCHAR(13), IN `fathersImage` VARCHAR(200), IN `mothersContact` VARCHAR(13), IN `mothersImage` VARCHAR(200), IN `guardianName` VARCHAR(200), IN `guardianContact` VARCHAR(13), IN `guardianImage` VARCHAR(200), IN `guardianAddress` VARCHAR(200), IN `password` VARCHAR(200), IN `userRole` VARCHAR(20), IN `lastLoginDate` DATETIME, IN `isLoginActive` INT(1), IN `institutionProfile_id` INT, IN `entryBy_id` INT(11), IN `entryDate` DATETIME, IN `isActive` INT(1), IN `contactInformation_id` INT, IN `entryInformation_id` INT)
    NO SQL
BEGIN
	START TRANSACTION;
        UPDATE usercontactinformation 
        SET
            usercontactinformation.contact1 = contact1,
            usercontactinformation.contact2 = contact2,
            usercontactinformation.email = email,
            usercontactinformation.address = address
        WHERE
        usercontactinformation.id = contactInformation_id;

        UPDATE userpersonalinformation
        SET
            userpersonalinformation.fullName =fullName,
            userpersonalinformation.fathersName = fathersName,
            userpersonalinformation.mothersName = mothersName,
            userpersonalinformation.gender = gender,
            userpersonalinformation.dateOfBirth = dateOfBirth,
            userpersonalinformation.image = image,
            userpersonalinformation.userContactInformation_id = 
            userContactInformation_id
		WHERE
        userpersonalinformation.username = username;

        UPDATE login
        SET
            login.username = username,
            login.password = password,
            login.userRole = userRole,
            login.lastLoginDate = lastLoginDate,
            login.isLoginActive = isLoginActive,
            login.institutionProfile_id = institutionProfile_id
		WHERE
        login.username = username;

        UPDATE studentsguardianinformation 
        SET
            studentsguardianinformation.fathersContact 	= fathersContact,
            studentsguardianinformation.fathersImage 	= fathersImage,
            studentsguardianinformation.mothersContact 	= mothersContact,
            studentsguardianinformation.mothersImage 	= mothersImage,
            studentsguardianinformation.guardianName 	= guardianName,
            studentsguardianinformation.guardianContact = guardianContact,
            studentsguardianinformation.guardianImage 	= guardianImage,
            studentsguardianinformation.guardianAddress = guardianAddress
		WHERE
        studentsguardianinformation.student_id = id;

        UPDATE entryinformation
		SET
			entryinformation.entryBy_id = entryBy_id,
            entryinformation.entryDate = entryDate ,
            entryinformation.isActive = isActive
		WHERE 
        entryinformation.id = entryInformation_id;
        

        UPDATE student
        SET
            student.username = username,
            student.entryInformation_id = entryInformation_id
        WHERE
        student.id = id;

	COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateStudentAdmission` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateStudentAdmission`(
	IN id 				INT,
	IN student_id 		INT,
    IN subjectManager_id INT,
    IN admissionDate 	DATETIME,
    IN monthlyFees 		FLOAT,
    IN entryBy_id 		INT
)
BEGIN
	UPDATE studentadmission
    SET
		studentadmission.student_id 		= student_id ,		
		studentadmission.subjectManager_id 	= subjectManager_id ,
		studentadmission.admissionDate 		= admissionDate, 	
        studentadmission.monthlyFees 		= monthlyFees, 		
        studentadmission.entryBy_id 		= entryBy_id 		
	WHERE 
    studentadmission.id = id ;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateStudentAttendance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateStudentAttendance`(
	IN id INT,
	IN user_id INT,
    IN timeIn DATETIME,
    IN timeOut DATETIME,
    IN inputDevice VARCHAR(50),
    IN entryBy_id INT
)
BEGIN
	UPDATE studentattendance
    SET
    studentattendance.student_id 	= user_id,
    studentattendance.timeIn  		= timeIn,
    studentattendance.timeOut 		= timeOut,
    studentattendance.inputDevice 	= inputDevice,
    studentattendance.entryBy_id 	= entryBy_id
	WHERE
    studentattendance.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateSubject` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateSubject`(
	IN id INT,
	IN name VARCHAR(60),
	IN code VARCHAR(60),
    IN institution_id INT
)
BEGIN
	UPDATE subject 
    SET
		subject.subjectName = name,
		subject.subjectCode = code,
        subject.institution_id = institution_id
    WHERE
    subject.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_updateSubjectManager` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updateSubjectManager`(
	IN id INT,
	IN subject_id INT,
    IN class_id INT,
	IN batch_id INT,
	IN fees FLOAT,
	IN session INT
)
BEGIN
	UPDATE subjectmanager
    SET
    	subjectmanager.subject_id 	= subject_id,
        subjectmanager.class_id 	= class_id,
        subjectmanager.batch_id 	= batch_id,
        subjectmanager.fees 		= fees,
        subjectmanager.session 		= session  
	WHERE 
    subjectmanager.id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

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
/*!50001 VIEW `vw_academicgrading` AS select `academicgrading`.`id` AS `id`,`academicgrading`.`gradingSystem_id` AS `gradingsystem_id`,`gradingsystem`.`systemName` AS `systemName`,`academicgrading`.`gradeName` AS `gradeName`,`academicgrading`.`percentageFrom` AS `percentageFrom`,`academicgrading`.`percentageTo` AS `percentageTo`,`academicgrading`.`gpa` AS `gpa`,`gradingsystem`.`EntryBy_id` AS `EntryBy_id`,`gradingsystem`.`institution_id` AS `institution_id` from (`academicgrading` join `gradingsystem`) where (`academicgrading`.`gradingSystem_id` = `gradingsystem`.`id`) */;
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
-- Final view structure for view `vw_examinformation`
--

/*!50001 DROP VIEW IF EXISTS `vw_examinformation`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_examinformation` AS select `examinformation`.`id` AS `id`,`examinformation`.`examName` AS `examName`,`examinformation`.`institution_id` AS `institution_id`,`examinformation`.`EntryBy_id` AS `EntryBy_id` from `examinformation` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_exammanager`
--

/*!50001 DROP VIEW IF EXISTS `vw_exammanager`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_exammanager` AS select `exammanager`.`id` AS `id`,`exammanager`.`examInformation_id` AS `examInformation_id`,`exammanager`.`subjectManager_id` AS `subjectManager_id`,`exammanager`.`gradingSystem_id` AS `gradingSystem_id`,`exammanager`.`fullMarks` AS `fullMarks`,`exammanager`.`date` AS `date`,`exammanager`.`timeStart` AS `timeStart`,`exammanager`.`timeEnd` AS `timeEnd`,`exammanager`.`roomNumber` AS `roomNumber`,`exammanager`.`EntryBy_id` AS `EntryBy_id`,`exammanager`.`year` AS `year` from `exammanager` */;
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
/*!50001 VIEW `vw_feescollection` AS select `feescollection`.`id` AS `id`,`feescollection`.`student_id` AS `student_id`,`feescollection`.`month` AS `month`,`feescollection`.`year` AS `year`,`feescollection`.`fees` AS `fees`,`feescollection`.`discount` AS `discount`,`feescollection`.`remain` AS `remain`,`feescollection`.`invoiceNumber` AS `invoiceNumber`,`feescollection`.`date` AS `date`,`feescollection`.`entryBy_id` AS `entryBy_id`,`feescollection`.`status` AS `status` from `feescollection` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_gradingsystem`
--

/*!50001 DROP VIEW IF EXISTS `vw_gradingsystem`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_gradingsystem` AS select `gradingsystem`.`id` AS `id`,`gradingsystem`.`systemName` AS `systemName`,`gradingsystem`.`institution_id` AS `institution_id`,`gradingsystem`.`EntryBy_id` AS `EntryBy_id` from `gradingsystem` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_result`
--

/*!50001 DROP VIEW IF EXISTS `vw_result`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_result` AS select `result`.`id` AS `id`,`result`.`examManager_id` AS `examManager_id`,`result`.`student_id` AS `student_id`,`result`.`marks` AS `marks`,`result`.`comments` AS `comments`,`result`.`date` AS `date`,`result`.`EntryBy_id` AS `EntryBy_id` from `result` */;
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
/*!50001 VIEW `vw_tracer` AS select `tracer`.`id` AS `id`,`tracer`.`actor_id` AS `actor_id`,`tracer`.`actionName` AS `actionName`,`tracer`.`tableName` AS `tableName`,`tracer`.`actionApplied_id` AS `actionApplied_id`,`tracer`.`actionTime` AS `actionTime`,`tracer`.`institution_id` AS `institution_id` from `tracer` */;
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

-- Dump completed on 2020-06-18 13:16:35
