USE [master]
GO
/****** Object:  Database [McAutomation]    Script Date: 06-10-2018 17:13:47 ******/
CREATE DATABASE [McAutomation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'McAutomation', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\McAutomation.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'McAutomation_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\McAutomation_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [McAutomation] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [McAutomation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [McAutomation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [McAutomation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [McAutomation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [McAutomation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [McAutomation] SET ARITHABORT OFF 
GO
ALTER DATABASE [McAutomation] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [McAutomation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [McAutomation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [McAutomation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [McAutomation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [McAutomation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [McAutomation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [McAutomation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [McAutomation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [McAutomation] SET  ENABLE_BROKER 
GO
ALTER DATABASE [McAutomation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [McAutomation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [McAutomation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [McAutomation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [McAutomation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [McAutomation] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [McAutomation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [McAutomation] SET RECOVERY FULL 
GO
ALTER DATABASE [McAutomation] SET  MULTI_USER 
GO
ALTER DATABASE [McAutomation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [McAutomation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [McAutomation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [McAutomation] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [McAutomation] SET DELAYED_DURABILITY = DISABLED 
GO
USE [McAutomation]
GO
/****** Object:  Table [dbo].[AllowanceMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllowanceMaster](
	[AllowanceId] [int] IDENTITY(1,1) NOT NULL,
	[AllowanceType] [nvarchar](50) NULL,
	[ValueMethod] [nvarchar](20) NULL,
	[AllowanceHead] [nvarchar](50) NULL,
	[AllowanceAmount] [decimal](18, 0) NULL,
	[IsActive] [bit] NULL,
	[IsCreated] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_AllowanceMaster] PRIMARY KEY CLUSTERED 
(
	[AllowanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AnnualLeave]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnnualLeave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[OtherNames] [nvarchar](250) NOT NULL,
	[Faculty] [nvarchar](100) NULL,
	[Department] [nvarchar](max) NULL,
	[Maritalstatus] [nvarchar](50) NULL,
	[Nationality] [nvarchar](100) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[Fileno] [nvarchar](20) NULL,
	[PresentStatus] [nvarchar](100) NULL,
	[Salaryperannum] [nvarchar](50) NULL,
	[Proposedannualleave] [nvarchar](50) NULL,
	[LeavefromDate] [date] NULL,
	[LeavetoDate] [date] NULL,
	[Totalworkingday] [nvarchar](10) NULL,
	[IsLeave] [nvarchar](10) NULL,
	[IsLeavefromDate] [date] NULL,
	[IsLeavetoDate] [date] NULL,
	[OutstandingLeaveDays] [nvarchar](10) NULL,
	[IsPublicService] [nvarchar](10) NULL,
	[IsHOD] [nvarchar](10) NULL,
	[ActOfficer] [nvarchar](250) NULL,
	[IApprove] [nvarchar](10) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_AnnualLeave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankMaster](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [nvarchar](60) NOT NULL,
	[BankTypeId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_BankMaster] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankTypeMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankTypeMaster](
	[BankTypeId] [int] IDENTITY(1,1) NOT NULL,
	[BankTypeName] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_BankTypeMaster] PRIMARY KEY CLUSTERED 
(
	[BankTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CadreMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CadreMaster](
	[CadreId] [int] IDENTITY(1,1) NOT NULL,
	[CadreName] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_CadreMaster] PRIMARY KEY CLUSTERED 
(
	[CadreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CasualLeave]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CasualLeave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Department] [nvarchar](250) NULL,
	[Post] [nvarchar](100) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[Reason] [nvarchar](max) NULL,
	[ResponsiblePerson] [nvarchar](250) NULL,
	[HodComment] [nvarchar](max) NULL,
	[AnyLeaveDays] [nvarchar](10) NULL,
	[OfficeInChargeName] [nvarchar](250) NULL,
	[ApprovedDays] [nvarchar](10) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_CasualLeave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CentralStores]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentralStores](
	[Id] [int] NOT NULL,
	[Item] [nvarchar](max) NOT NULL,
	[Class] [nvarchar](50) NOT NULL,
	[Price] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_CentralStores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChemicalStores]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChemicalStores](
	[Id] [int] NOT NULL,
	[Item] [nvarchar](max) NOT NULL,
	[Class] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_ChemicalStores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CityMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](50) NULL,
	[StateId] [int] NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_CityMaster_IsDeleted]  DEFAULT ((0)),
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_CityMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClassMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassMaster](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[ClassNumber] [int] NOT NULL,
	[ClassName] [nvarchar](100) NULL,
	[ClassStatus] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_ClassMaster] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[COUNTRYLIST]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[COUNTRYLIST](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ISO] [char](2) NOT NULL,
	[NAME] [varchar](80) NOT NULL,
	[NICENAME] [varchar](80) NOT NULL,
	[ISO3] [char](3) NULL,
	[NUMCODE] [int] NULL,
	[PHONECODE] [int] NOT NULL,
	[CustomerId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CrivVoucher]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrivVoucher](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[CRIVVoucherId] [int] NULL,
	[DepartmentId] [nvarchar](500) NULL,
	[Store] [nvarchar](500) NULL,
	[Item] [nvarchar](500) NULL,
	[Class] [nvarchar](500) NULL,
	[Uom] [nvarchar](500) NULL,
	[Qunatity] [nvarchar](500) NULL,
	[Rate] [nvarchar](500) NULL,
	[TotalValue] [nvarchar](500) NULL,
	[BatchDetail] [nvarchar](500) NULL,
	[RequisitionOfficer] [nvarchar](500) NULL,
	[RecivingPerson] [nvarchar](500) NULL,
	[Remarks] [nvarchar](500) NULL,
	[TotalCost] [nvarchar](500) NULL,
	[AmtWord] [nvarchar](500) NULL,
	[Name1] [nvarchar](500) NULL,
	[Sign1] [nvarchar](500) NULL,
	[Fac1] [nvarchar](500) NULL,
	[Date1] [datetime] NULL,
	[Sign2] [nvarchar](500) NULL,
	[Date2] [datetime] NULL,
	[Name3] [nvarchar](500) NULL,
	[Fac3] [nvarchar](500) NULL,
	[Date3] [datetime] NULL,
	[DateIns] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_CrivVoucher] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerMaster](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeGIId] [int] NOT NULL,
	[LoginUserId] [int] NOT NULL,
	[OrgName] [nvarchar](250) NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[ContactPerson] [nvarchar](250) NULL,
	[OrgLogoUrl] [nvarchar](max) NULL,
	[CountryLogoIrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_CustomerMaster] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeductionsMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeductionsMaster](
	[DeductionId] [int] IDENTITY(1,1) NOT NULL,
	[DeductionType] [nvarchar](50) NULL,
	[ValueMethod] [nvarchar](20) NULL,
	[DeductionHead] [nvarchar](50) NULL,
	[DeductionAmount] [decimal](18, 0) NULL,
	[IsActive] [bit] NULL,
	[IsCreated] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_DeductionsMaster] PRIMARY KEY CLUSTERED 
(
	[DeductionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmpAIAssociation]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpAIAssociation](
	[AssociationsId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[IDnumber] [nvarchar](20) NULL,
	[AttendedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_EmpAIAssociation] PRIMARY KEY CLUSTERED 
(
	[AssociationsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmpAIConference]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpAIConference](
	[ConferenceId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[AttendedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_EmpAIConference] PRIMARY KEY CLUSTERED 
(
	[ConferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeAI]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeAI](
	[EmployeeAIId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[InstitutionAttended1] [nvarchar](max) NULL,
	[InstitutionAttended2] [nvarchar](max) NULL,
	[InstitutionAttended3] [nvarchar](max) NULL,
	[Qualification1] [nvarchar](10) NULL,
	[Qualification2] [nvarchar](10) NULL,
	[Qualification3] [nvarchar](10) NULL,
	[YearOfGraduation1] [nvarchar](10) NULL,
	[YearOfGraduation2] [nvarchar](10) NULL,
	[YearOfGraduation3] [nvarchar](10) NULL,
	[ProfessionalAssociationsTitle] [nvarchar](max) NULL,
	[ProfessionalAssociationsIdNo] [nvarchar](50) NULL,
	[ProfessionalAssociationsDate] [date] NULL,
	[ConferenceAttendedName] [nvarchar](max) NULL,
	[ConferenceAttendedTitle] [nvarchar](max) NULL,
	[ConferenceAttendedDate] [date] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_EmployeeAI] PRIMARY KEY CLUSTERED 
(
	[EmployeeAIId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeAttendance]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeAttendance](
	[EmployeeCode] [int] NOT NULL,
	[OnDate] [date] NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[LogoutTime] [datetime] NOT NULL,
	[Status] [nvarchar](20) NULL,
	[CustomerId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeGI]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeGI](
	[EmployeeGIId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[Rank] [nvarchar](100) NULL,
	[File_No] [nvarchar](50) NULL,
	[Grade_Level] [int] NULL,
	[Step] [int] NULL,
	[Cadre] [nvarchar](150) NULL,
	[Title] [nvarchar](50) NULL,
	[First_Name] [nvarchar](250) NULL,
	[Middle_Name] [nvarchar](250) NULL,
	[Surname] [nvarchar](250) NULL,
	[Sex] [nvarchar](10) NULL,
	[DateOfBirth] [datetime] NULL,
	[PlaceOfBirth] [nvarchar](max) NULL,
	[Marital_Status] [nvarchar](50) NULL,
	[Maiden_Name] [nvarchar](max) NULL,
	[Spouse_Name] [nvarchar](250) NULL,
	[StateOfOrigin] [nvarchar](100) NULL,
	[LGA] [nvarchar](100) NULL,
	[Home_Town] [nvarchar](100) NULL,
	[Religion] [nvarchar](20) NULL,
	[ContactHomeAddress] [nvarchar](max) NULL,
	[FirstAppointmentDate] [datetime] NULL,
	[FirstAppointmentLocation] [nvarchar](250) NULL,
	[ConfirmationDate] [datetime] NULL,
	[DateOfRetirement] [datetime] NOT NULL,
	[LastPromotionDate] [datetime] NULL,
	[Unit_Research] [nvarchar](100) NULL,
	[Section] [nvarchar](50) NULL,
	[LeaveDays] [int] NULL,
	[Leave_fromDate] [datetime] NULL,
	[Leave_ToDate] [datetime] NULL,
	[EmployeePhoto] [nvarchar](max) NULL,
	[StationOfDeployment] [nvarchar](50) NULL,
	[Programmes] [nvarchar](100) NULL,
	[Unit_Services] [nvarchar](100) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_EmployeeGI] PRIMARY KEY CLUSTERED 
(
	[EmployeeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeMI]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeMI](
	[EmployeeMIId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[NhisNo] [nvarchar](50) NULL,
	[NhisProvider] [nvarchar](max) NULL,
	[BloodGroup] [nvarchar](10) NULL,
	[BloodGenotype] [nvarchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_EmployeeMI] PRIMARY KEY CLUSTERED 
(
	[EmployeeMIId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeePI]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePI](
	[EmployeePIId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[EmpEmailId] [nvarchar](50) NULL,
	[PermanentAddress] [nvarchar](150) NULL,
	[MobileNo] [nvarchar](10) NULL,
	[EmailIdKin] [nvarchar](50) NULL,
	[KinName] [nvarchar](30) NULL,
	[AddressNextOfKin] [nvarchar](150) NULL,
	[StateNextOfKin] [nvarchar](30) NULL,
	[LGAextOfKin] [nvarchar](30) NULL,
	[Relation] [nvarchar](30) NULL,
	[PhoneNoNextOfKin] [nvarchar](10) NULL,
	[NameOfStaffBenificiary] [nvarchar](50) NULL,
	[PhoneOfStaffBenificiary] [nvarchar](10) NULL,
	[AddressOfStaffBenificiary] [nvarchar](150) NULL,
	[EmployeeStatus] [nvarchar](30) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_EmployeePI] PRIMARY KEY CLUSTERED 
(
	[EmployeePIId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeSI]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSI](
	[EmployeeSIId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[CurrentPosting] [nvarchar](100) NULL,
	[BankType] [nvarchar](50) NULL,
	[NameOfBanks] [nvarchar](50) NULL,
	[BankBranch] [nvarchar](200) NULL,
	[AccountType] [nvarchar](10) NULL,
	[AccountNumber] [nvarchar](30) NULL,
	[AccountName] [nvarchar](100) NULL,
	[PFA] [nvarchar](100) NULL,
	[RSAPinNo] [nvarchar](50) NULL,
	[SalaryScale] [nvarchar](20) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_EmployeeSI] PRIMARY KEY CLUSTERED 
(
	[EmployeeSIId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FertilizerStores]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FertilizerStores](
	[Id] [int] NOT NULL,
	[Item] [nvarchar](max) NULL,
	[Class] [nvarchar](50) NULL,
	[Price] [decimal](18, 18) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_FertilizerStores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GraduateAttachmentForm]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GraduateAttachmentForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NULL,
	[OurRef] [nvarchar](50) NULL,
	[YourRef] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[Name] [nvarchar](250) NULL,
	[LetterDated] [nvarchar](20) NULL,
	[FromDate] [nvarchar](20) NULL,
	[ToDate] [nvarchar](20) NULL,
	[OfficerInCharge] [nvarchar](250) NULL,
	[PrincipalAccountant] [nvarchar](250) NULL,
	[ReinstatePayment] [nvarchar](50) NULL,
	[PaymentToDate] [nvarchar](20) NULL,
	[PaymentFromDate] [nvarchar](20) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_GraduateAttachmentForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMaster](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [int] NULL,
	[ClassId] [int] NULL,
	[ItemName] [nvarchar](500) NULL,
	[ItemCat] [nvarchar](500) NULL,
	[UomId] [int] NULL,
	[VendorId] [int] NULL,
	[ItemRate] [nvarchar](500) NULL,
	[ItemTax] [nvarchar](500) NULL,
	[ItemImage] [nvarchar](500) NULL,
	[StatusId] [int] NULL,
	[ItemDesc] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveApplication]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveApplication](
	[LeaveAccId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[LeaveTypeName] [nvarchar](20) NOT NULL,
	[LeaveFromDate] [date] NOT NULL,
	[LeaveToDate] [date] NOT NULL,
	[NoOfDays] [decimal](18, 0) NOT NULL,
	[AppDate] [date] NULL,
	[Status] [nvarchar](15) NULL,
	[IsApproved] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_LeaveApplication] PRIMARY KEY CLUSTERED 
(
	[LeaveAccId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveLedger]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveLedger](
	[LeaveLogId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[LeaveType] [nvarchar](20) NOT NULL,
	[ConsumedLeaves] [decimal](18, 0) NOT NULL,
	[BalanceLeaves] [decimal](18, 0) NOT NULL,
	[FiscalYear] [nvarchar](15) NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_LeaveLedger] PRIMARY KEY CLUSTERED 
(
	[LeaveLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveMaster](
	[LeaveId] [int] IDENTITY(1,1) NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[LeaveCount] [int] NOT NULL,
	[CalenderYear] [nvarchar](10) NOT NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
	[IsCreated] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_LeaveMaster] PRIMARY KEY CLUSTERED 
(
	[LeaveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveRoasterJuniorStaff]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveRoasterJuniorStaff](
	[Id] [int] NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[SNo] [int] NULL,
	[Name] [nvarchar](250) NULL,
	[NoOfDay] [int] NULL,
	[FromDate] [nvarchar](20) NULL,
	[ToDate] [nvarchar](20) NULL,
	[FileNo] [nvarchar](50) NULL,
	[Contiss] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_LeaveRoasterJuniorStaff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveTypeMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveTypeMaster](
	[LeaveTypeId] [int] IDENTITY(1,1) NOT NULL,
	[LeaveTypeName] [nvarchar](30) NOT NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_LeaveTypeMaster] PRIMARY KEY CLUSTERED 
(
	[LeaveTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Login]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Userid] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Usertype] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[LoginDate] [datetime] NULL,
	[SystemIp] [nvarchar](100) NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NyscFinalClearance]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NyscFinalClearance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NULL,
	[OurRef] [nvarchar](50) NULL,
	[YourRef] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[Name] [nvarchar](50) NULL,
	[NYSC_Code] [nvarchar](50) NULL,
	[EffectDate] [date] NULL,
	[BankAccountNo] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_NyscFinalClearance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NyscMonthlyClearance]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NyscMonthlyClearance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NULL,
	[OurRef] [nvarchar](50) NULL,
	[YourRef] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[Name] [nvarchar](250) NULL,
	[NYSC_Code] [nvarchar](50) NULL,
	[SatisfactoryMonth] [nvarchar](50) NULL,
	[AllowanceMonth] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_NyscMonthlyClearance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PFAMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PFAMaster](
	[PFAId] [int] IDENTITY(1,1) NOT NULL,
	[PFAName] [nvarchar](150) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_PFAMaster] PRIMARY KEY CLUSTERED 
(
	[PFAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrequalificationScoring]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrequalificationScoring](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	[ProjectTitle] [nvarchar](max) NULL,
	[LotNo] [nvarchar](50) NOT NULL,
	[ContractorName] [nvarchar](max) NULL,
	[EvidofReg_Cac] [nvarchar](10) NULL,
	[TaxClearanceCertificate] [nvarchar](10) NULL,
	[EvidofReg_Bureau] [nvarchar](10) NULL,
	[AudittedAccount] [nvarchar](50) NULL,
	[ClearanceCert_Itf] [nvarchar](50) NULL,
	[ClearanceCert_Pencom] [nvarchar](50) NULL,
	[ClearanceCert_Nsitf] [nvarchar](50) NULL,
	[StaffStrength] [nvarchar](50) NULL,
	[CurrentFinStatus] [nvarchar](50) NULL,
	[EquipmentList] [nvarchar](max) NULL,
	[EvidPreSimJob] [nvarchar](max) NULL,
	[ExpCompt] [nvarchar](max) NULL,
	[FinalScore] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_PrequalificationScoring] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrintForms]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintForms](
	[EmployeeCode] [nvarchar](50) NOT NULL,
	[FormType] [nvarchar](50) NULL,
	[IsApproved] [bit] NULL,
	[IsIssued] [bit] NULL,
	[AppDate] [date] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_PrintForms] PRIMARY KEY CLUSTERED 
(
	[EmployeeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProgrammeMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgrammeMaster](
	[ProgrammeId] [int] IDENTITY(1,1) NOT NULL,
	[ProgrammeName] [nvarchar](80) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_ProgrammeMaster] PRIMARY KEY CLUSTERED 
(
	[ProgrammeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [nvarchar](500) NULL,
	[VendorId] [int] NULL,
	[ItemId] [int] NULL,
	[ItemQunt] [nvarchar](500) NULL,
	[ItemTax] [nvarchar](500) NULL,
	[DeliLoc] [nvarchar](500) NULL,
	[Terms] [nvarchar](500) NULL,
	[StatusId] [int] NULL,
	[ItemDesc] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QualificationMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QualificationMaster](
	[QualificationId] [int] IDENTITY(1,1) NOT NULL,
	[QualificationName] [nvarchar](50) NOT NULL,
	[Duration] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_QualificationMaster] PRIMARY KEY CLUSTERED 
(
	[QualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RankMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RankMaster](
	[RankId] [int] IDENTITY(1,1) NOT NULL,
	[RankName] [nvarchar](50) NOT NULL,
	[RankDescription] [nvarchar](150) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_RankMaster] PRIMARY KEY CLUSTERED 
(
	[RankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMaster](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_RoleMaster] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalaryPayment]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryPayment](
	[SalaryPaymId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[SalaryAmount] [decimal](18, 0) NOT NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[DeductionAmount] [decimal](18, 0) NULL,
	[AllowanceAmount] [decimal](18, 0) NULL,
	[OtherDeduction] [decimal](18, 0) NULL,
	[OtherAllowance] [decimal](18, 0) NULL,
	[PaymentDate] [datetime] NULL,
	[IsPaid] [bit] NULL,
	[IsSalarySlipPrint] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_SalaryPayment] PRIMARY KEY CLUSTERED 
(
	[SalaryPaymId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalaryStructureMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalaryStructureMaster](
	[SalaryScaleId] [int] IDENTITY(1,1) NOT NULL,
	[SalaryScale] [nvarchar](50) NULL,
	[GradeLevel] [int] NULL,
	[Step] [int] NULL,
	[SalaryAmount] [decimal](18, 0) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[ScaleYear] [varchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_SalaryStructure] PRIMARY KEY CLUSTERED 
(
	[SalaryScaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SectionMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionMaster](
	[SectionId] [int] IDENTITY(1,1) NOT NULL,
	[SectionName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_SectionMaster] PRIMARY KEY CLUSTERED 
(
	[SectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[State] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_StateMaster_IsDeleted]  DEFAULT ((0)),
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_StateMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StationaryStores]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationaryStores](
	[Id] [int] NOT NULL,
	[Item] [nvarchar](max) NOT NULL,
	[Class] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_StationaryStores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StationMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationMaster](
	[StationId] [int] IDENTITY(1,1) NOT NULL,
	[StationName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_StationMaster] PRIMARY KEY CLUSTERED 
(
	[StationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusMaster](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_StatusMaster] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StockMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockMaster](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [int] NULL,
	[ClassId] [int] NULL,
	[ItemId] [int] NULL,
	[BatchNo] [nvarchar](500) NULL,
	[Quantity] [nvarchar](500) NULL,
	[UomId] [int] NULL,
	[VendorId] [int] NULL,
	[PInvoiceNo] [nvarchar](500) NULL,
	[ReciveDate] [datetime] NULL,
	[TotalPrice] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_StockMaster] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreCreditVoucher]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreCreditVoucher](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[CVoucherId] [nvarchar](500) NULL,
	[RVoucherId] [nvarchar](500) NULL,
	[DepartmentId] [nvarchar](500) NULL,
	[Store] [nvarchar](500) NULL,
	[DateIns] [datetime] NULL,
	[Item] [nvarchar](500) NULL,
	[Vendor] [nvarchar](500) NULL,
	[Uom] [nvarchar](500) NULL,
	[Qunatity] [nvarchar](500) NULL,
	[Rate] [nvarchar](500) NULL,
	[TotalValue] [nvarchar](500) NULL,
	[BatchDetail] [nvarchar](500) NULL,
	[StoreOfficer] [nvarchar](500) NULL,
	[RecivingPerson] [nvarchar](500) NULL,
	[Sign1] [nvarchar](500) NULL,
	[Date1] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[Remark] [nvarchar](500) NULL,
	[int] [int] NULL,
 CONSTRAINT [PK_StoreCreditVoucher] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreGateVoucher]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreGateVoucher](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherTo] [nvarchar](500) NULL,
	[LPONo] [nvarchar](500) NULL,
	[VoucherFrom] [nvarchar](500) NULL,
	[VoucherId] [nvarchar](500) NULL,
	[Store] [nvarchar](500) NULL,
	[DateIns] [datetime] NULL,
	[Deliver] [nvarchar](500) NULL,
	[vehcileNo] [nvarchar](500) NULL,
	[Item] [nvarchar](500) NULL,
	[Vendor] [nvarchar](500) NULL,
	[Uom] [nvarchar](500) NULL,
	[Qunatity] [nvarchar](500) NULL,
	[Rate] [nvarchar](500) NULL,
	[TotalValue] [nvarchar](500) NULL,
	[Sign1] [nvarchar](500) NULL,
	[OutOfStock] [nvarchar](500) NULL,
	[LowStock] [nvarchar](500) NULL,
	[Description] [nvarchar](500) NULL,
	[Fund] [nvarchar](500) NULL,
	[ShortSupply] [nvarchar](500) NULL,
	[Reciving] [nvarchar](500) NULL,
	[Fac] [nvarchar](500) NULL,
	[Depart] [nvarchar](500) NULL,
	[Date1] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_StoreGateVoucher] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreMaster](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[StoreNumber] [int] NOT NULL,
	[StoreName] [nvarchar](100) NULL,
	[StoreDesc] [nvarchar](500) NULL,
	[StoreImgName] [nvarchar](100) NULL,
	[StoreStatus] [int] NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_StoreMaster_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreReciptVoucher]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreReciptVoucher](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [nvarchar](500) NULL,
	[DepartmentId] [nvarchar](500) NULL,
	[Store] [nvarchar](500) NULL,
	[DateIns] [datetime] NULL,
	[ReceviedFrom] [nvarchar](500) NULL,
	[Item] [nvarchar](500) NULL,
	[Vendor] [nvarchar](500) NULL,
	[Uom] [nvarchar](500) NULL,
	[Qunatity] [nvarchar](500) NULL,
	[Rate] [nvarchar](500) NULL,
	[TotalValue] [nvarchar](500) NULL,
	[BatchDetail] [nvarchar](500) NULL,
	[StoreOfficer] [nvarchar](500) NULL,
	[RecivingPerson] [nvarchar](500) NULL,
	[Sign1] [nvarchar](500) NULL,
	[Date1] [datetime] NULL,
	[Sign2] [nvarchar](500) NULL,
	[Date2] [datetime] NULL,
	[Sign3] [nvarchar](500) NULL,
	[Date3] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[Remark] [nvarchar](500) NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_StoreReciptVoucher] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreTallyVoucher]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreTallyVoucher](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](500) NULL,
	[LEDGERFOLIO] [nvarchar](500) NULL,
	[TVoucherId] [nvarchar](500) NULL,
	[RVoucherId] [nvarchar](500) NULL,
	[Store] [nvarchar](500) NULL,
	[DateIns] [datetime] NULL,
	[Item] [nvarchar](500) NULL,
	[Vendor] [nvarchar](500) NULL,
	[Uom] [nvarchar](500) NULL,
	[Qunatity] [nvarchar](500) NULL,
	[Rate] [nvarchar](500) NULL,
	[TotalValue] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_StoreTallyVoucher] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SuperAdmin]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuperAdmin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
 CONSTRAINT [PK_SuperAdmin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TendererInformation]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TendererInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	[LotNo] [nvarchar](50) NOT NULL,
	[ProjectTitle] [nvarchar](max) NULL,
	[RepresentativeName] [nvarchar](250) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[SubmissionDate] [date] NULL,
	[YearofProject] [nvarchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_TendererInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TenderOpening]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TenderOpening](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RepresentativeName] [nvarchar](250) NULL,
	[CompanyName] [nvarchar](max) NULL,
	[AmountQuoted] [decimal](18, 0) NULL,
	[CompletionPeriodFrom] [date] NULL,
	[CompletionPeriodTo] [date] NULL,
	[Remarks] [nvarchar](max) NULL,
	[LotNo] [nvarchar](50) NULL,
	[ProjectTitle] [nvarchar](max) NULL,
	[YearofProject] [nvarchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_TenderOpening] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UnitResearchMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitResearchMaster](
	[UnitResearchId] [int] IDENTITY(1,1) NOT NULL,
	[UnitResearchName] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_UnitResearchMaster] PRIMARY KEY CLUSTERED 
(
	[UnitResearchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UnitServicesMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitServicesMaster](
	[UnitServicesId] [int] IDENTITY(1,1) NOT NULL,
	[UnitServicesName] [nvarchar](60) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_UnitServicesMaster] PRIMARY KEY CLUSTERED 
(
	[UnitServicesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UomMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UomMaster](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[UOMCode] [nvarchar](100) NULL,
	[UOMName] [nvarchar](100) NULL,
	[UOMDesc] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_UomMaster_CreatedDate]  DEFAULT (getdate()),
	[IsDeleted] [bit] NOT NULL,
	[UOMStatus] [int] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_UomMaster] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [int] NOT NULL,
	[EmailId] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](250) NULL,
	[UserKeyId] [nvarchar](30) NULL,
	[OrganizationName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[LastLoginDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VendorMaster]    Script Date: 06-10-2018 17:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorMaster](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [nvarchar](100) NULL,
	[VendorOrg] [nvarchar](500) NULL,
	[VendorMob] [nvarchar](500) NULL,
	[VendorAMob] [nvarchar](500) NULL,
	[VendorEmail] [nvarchar](500) NULL,
	[VendorRepDesc] [nvarchar](500) NULL,
	[VendorAdd1] [nvarchar](500) NULL,
	[VendorAdd2] [nvarchar](500) NULL,
	[VendorCity] [int] NULL,
	[VendorState] [int] NULL,
	[VendorCountry] [int] NULL,
	[VendorZipCode] [nvarchar](500) NULL,
	[VendorTaxDet] [nvarchar](500) NULL,
	[VendorDesc] [nvarchar](100) NULL,
	[VendorStatus] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AnnualLeave] ADD  CONSTRAINT [DF_AnnualLeave_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AnnualLeave] ADD  CONSTRAINT [DF_AnnualLeave_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CasualLeave] ADD  CONSTRAINT [DF_CasualLeave_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CasualLeave] ADD  CONSTRAINT [DF_CasualLeave_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CentralStores] ADD  CONSTRAINT [DF_CentralStores_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CentralStores] ADD  CONSTRAINT [DF_CentralStores_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ChemicalStores] ADD  CONSTRAINT [DF_ChemicalStores_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ChemicalStores] ADD  CONSTRAINT [DF_ChemicalStores_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ClassMaster] ADD  CONSTRAINT [DF_ClassMaster_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ClassMaster] ADD  CONSTRAINT [DF_ClassMaster_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[COUNTRYLIST] ADD  DEFAULT (NULL) FOR [ISO3]
GO
ALTER TABLE [dbo].[COUNTRYLIST] ADD  DEFAULT (NULL) FOR [NUMCODE]
GO
ALTER TABLE [dbo].[CrivVoucher] ADD  CONSTRAINT [DF_CrivVoucher_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[FertilizerStores] ADD  CONSTRAINT [DF_FertilizerStores_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[FertilizerStores] ADD  CONSTRAINT [DF_FertilizerStores_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[GraduateAttachmentForm] ADD  CONSTRAINT [DF_GraduateAttachmentForm_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[GraduateAttachmentForm] ADD  CONSTRAINT [DF_GraduateAttachmentForm_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[LeaveRoasterJuniorStaff] ADD  CONSTRAINT [DF_LeaveRoasterJuniorStaff_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[LeaveRoasterJuniorStaff] ADD  CONSTRAINT [DF_LeaveRoasterJuniorStaff_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Table_1_CreatedDate]  DEFAULT (getdate()) FOR [LoginDate]
GO
ALTER TABLE [dbo].[NyscFinalClearance] ADD  CONSTRAINT [DF_NyscFinalClearance_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[NyscFinalClearance] ADD  CONSTRAINT [DF_NyscFinalClearance_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[NyscMonthlyClearance] ADD  CONSTRAINT [DF_NyscClearance_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[NyscMonthlyClearance] ADD  CONSTRAINT [DF_NyscClearance_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PrequalificationScoring] ADD  CONSTRAINT [DF_PrequalificationScoring_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PrequalificationScoring] ADD  CONSTRAINT [DF_PrequalificationScoring_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[StationaryStores] ADD  CONSTRAINT [DF_StationaryStores_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[StationaryStores] ADD  CONSTRAINT [DF_StationaryStores_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[StoreCreditVoucher] ADD  CONSTRAINT [DF_StoreCreditVoucher_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[StoreReciptVoucher] ADD  CONSTRAINT [DF_StoreReciptVoucher_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[StoreTallyVoucher] ADD  CONSTRAINT [DF_StoreTallyVoucher_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TendererInformation] ADD  CONSTRAINT [DF_TendererInformation_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TendererInformation] ADD  CONSTRAINT [DF_TendererInformation_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AnnualLeave]  WITH CHECK ADD  CONSTRAINT [FK__AnnualLea__Emplo__4F7CD00D] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[AnnualLeave] CHECK CONSTRAINT [FK__AnnualLea__Emplo__4F7CD00D]
GO
ALTER TABLE [dbo].[AnnualLeave]  WITH CHECK ADD  CONSTRAINT [FK__AnnualLea__Emplo__5812160E] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[AnnualLeave] CHECK CONSTRAINT [FK__AnnualLea__Emplo__5812160E]
GO
ALTER TABLE [dbo].[CasualLeave]  WITH CHECK ADD  CONSTRAINT [FK__CasualLea__Emplo__5070F446] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[CasualLeave] CHECK CONSTRAINT [FK__CasualLea__Emplo__5070F446]
GO
ALTER TABLE [dbo].[CasualLeave]  WITH CHECK ADD  CONSTRAINT [FK__CasualLea__Emplo__59063A47] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[CasualLeave] CHECK CONSTRAINT [FK__CasualLea__Emplo__59063A47]
GO
ALTER TABLE [dbo].[CustomerMaster]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMaster_UserMaster] FOREIGN KEY([LoginUserId])
REFERENCES [dbo].[UserMaster] ([UserId])
GO
ALTER TABLE [dbo].[CustomerMaster] CHECK CONSTRAINT [FK_CustomerMaster_UserMaster]
GO
ALTER TABLE [dbo].[EmployeeAI]  WITH CHECK ADD  CONSTRAINT [FK__EmployeeA__Emplo__07020F21] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[EmployeeAI] CHECK CONSTRAINT [FK__EmployeeA__Emplo__07020F21]
GO
ALTER TABLE [dbo].[EmployeeGI]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeGI_CustomerMaster] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerMaster] ([CustomerId])
GO
ALTER TABLE [dbo].[EmployeeGI] CHECK CONSTRAINT [FK_EmployeeGI_CustomerMaster]
GO
ALTER TABLE [dbo].[EmployeeMI]  WITH CHECK ADD  CONSTRAINT [FK__EmployeeM__Emplo__08EA5793] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[EmployeeMI] CHECK CONSTRAINT [FK__EmployeeM__Emplo__08EA5793]
GO
ALTER TABLE [dbo].[EmployeePI]  WITH CHECK ADD  CONSTRAINT [FK__EmployeeP__Emplo__07F6335A] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[EmployeePI] CHECK CONSTRAINT [FK__EmployeeP__Emplo__07F6335A]
GO
ALTER TABLE [dbo].[EmployeeSI]  WITH CHECK ADD  CONSTRAINT [FK__EmployeeS__Emplo__09DE7BCC] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[EmployeeSI] CHECK CONSTRAINT [FK__EmployeeS__Emplo__09DE7BCC]
GO
ALTER TABLE [dbo].[GraduateAttachmentForm]  WITH CHECK ADD  CONSTRAINT [FK__GraduateA__Emplo__4CA06362] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[GraduateAttachmentForm] CHECK CONSTRAINT [FK__GraduateA__Emplo__4CA06362]
GO
ALTER TABLE [dbo].[GraduateAttachmentForm]  WITH CHECK ADD  CONSTRAINT [FK__GraduateA__Emplo__5441852A] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[GraduateAttachmentForm] CHECK CONSTRAINT [FK__GraduateA__Emplo__5441852A]
GO
ALTER TABLE [dbo].[LeaveRoasterJuniorStaff]  WITH CHECK ADD  CONSTRAINT [FK__LeaveRoas__Emplo__5535A963] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[LeaveRoasterJuniorStaff] CHECK CONSTRAINT [FK__LeaveRoas__Emplo__5535A963]
GO
ALTER TABLE [dbo].[NyscFinalClearance]  WITH CHECK ADD  CONSTRAINT [FK__NyscFinal__Emplo__4D94879B] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[NyscFinalClearance] CHECK CONSTRAINT [FK__NyscFinal__Emplo__4D94879B]
GO
ALTER TABLE [dbo].[NyscFinalClearance]  WITH CHECK ADD  CONSTRAINT [FK__NyscFinal__Emplo__5629CD9C] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[NyscFinalClearance] CHECK CONSTRAINT [FK__NyscFinal__Emplo__5629CD9C]
GO
ALTER TABLE [dbo].[NyscMonthlyClearance]  WITH CHECK ADD  CONSTRAINT [FK__NyscMonth__Emplo__4E88ABD4] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[NyscMonthlyClearance] CHECK CONSTRAINT [FK__NyscMonth__Emplo__4E88ABD4]
GO
ALTER TABLE [dbo].[NyscMonthlyClearance]  WITH CHECK ADD  CONSTRAINT [FK__NyscMonth__Emplo__571DF1D5] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[EmployeeGI] ([EmployeeCode])
GO
ALTER TABLE [dbo].[NyscMonthlyClearance] CHECK CONSTRAINT [FK__NyscMonth__Emplo__571DF1D5]
GO
USE [master]
GO
ALTER DATABASE [McAutomation] SET  READ_WRITE 
GO
