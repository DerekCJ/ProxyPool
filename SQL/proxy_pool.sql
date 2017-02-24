/*
Navicat SQL Server Data Transfer

Source Server         : 
Source Server Version : 130000
Source Host           : 
Source Database       : proxy_pool
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 130000
File Encoding         : 65001

Date: 2017-02-07 00:02:19
*/
USE [master]
GO
DROP DATABASE if exists [proxy_pool]
GO

CREATE DATABASE [proxy_pool]
GO

USE [proxy_pool]
GO
-- ----------------------------
-- Table structure for tb_log
-- ----------------------------
DROP TABLE [dbo].[tb_log]
GO
CREATE TABLE [dbo].[tb_log] (
[log_id] int NOT NULL IDENTITY(1,1) ,
[log_time] datetime2(0) NULL ,
[pool_id] int NULL ,
[pxy_src_id] int NULL ,
[pxy_id] int NULL ,
[vld_id] int NULL ,
[log_description] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of tb_log
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tb_log] ON
GO
SET IDENTITY_INSERT [dbo].[tb_log] OFF
GO

-- ----------------------------
-- Table structure for tb_pool
-- ----------------------------
DROP TABLE [dbo].[tb_pool]
GO
CREATE TABLE [dbo].[tb_pool] (
[pool_id] int NOT NULL IDENTITY(1,1) ,
[pool_name] nvarchar(255) NULL ,
[vld_id] int NULL ,
[pool_validate_timespan] int NULL ,
[pool_validate_thread] int NULL ,
[pool_source] nvarchar(MAX) NULL ,
[pool_cathe_size] int NULL ,
[pool_status] tinyint NULL ,
[pool_create_time] datetime2(0) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[tb_pool]', RESEED, 2)
GO

-- ----------------------------
-- Records of tb_pool
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tb_pool] ON
GO
INSERT INTO [dbo].[tb_pool] ([pool_id], [pool_name], [vld_id], [pool_validate_timespan], [pool_validate_thread], [pool_source], [pool_cathe_size], [pool_status], [pool_create_time]) VALUES (N'1', N'基础池-包含所有源', N'1', N'180000', N'100', N'1,2,3,4,5,6', N'10000', N'1', N'2017-02-09 00:00:00')
GO
SET IDENTITY_INSERT [dbo].[tb_pool] OFF
GO

-- ----------------------------
-- Table structure for tb_proxy
-- ----------------------------
DROP TABLE [dbo].[tb_proxy]
GO
CREATE TABLE [dbo].[tb_proxy] (
[pxy_id] int NOT NULL IDENTITY(1,1) ,
[pxy_src_id] int NULL ,
[pxy_ip_add] nvarchar(15) NULL ,
[pxy_port] int NULL ,
[pxy_protocal] nvarchar(255) NULL ,
[pxy_request_method] nvarchar(255) NULL ,
[pxy_location] nvarchar(255) NULL ,
[pxy_type] nvarchar(255) NULL ,
[pxy_user] nvarchar(255) NULL ,
[pxy_pass] nvarchar(255) NULL ,
[pxy_domain] nvarchar(255) NULL ,
[pxy_status] tinyint NULL ,
[pxy_create_time] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[tb_proxy]', RESEED, 899)
GO

-- ----------------------------
-- Records of tb_proxy
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tb_proxy] ON
GO
SET IDENTITY_INSERT [dbo].[tb_proxy] OFF
GO

-- ----------------------------
-- Table structure for tb_proxy_source
-- ----------------------------
DROP TABLE [dbo].[tb_proxy_source]
GO
CREATE TABLE [dbo].[tb_proxy_source] (
[pxy_src_id] int NOT NULL IDENTITY(1,1) ,
[pxy_src_name] varchar(255) NULL ,
[pxy_src_cathe_size] int NULL ,
[pxy_src_url] nvarchar(MAX) NULL ,
[pxy_src_url_para] nvarchar(MAX) NULL ,
[pxy_src_request_timespan] int NULL ,
[pxy_src_refresh_timespan] int NULL ,
[pxy_src_charset] nvarchar(255) NULL ,
[pxy_src_request_method] nvarchar(255) NULL ,
[pxy_src_doc_type] nvarchar(255) NULL ,
[pxy_src_srch_type] nvarchar(255) NULL ,
[pxy_src_ip_add_srch] nvarchar(255) NULL ,
[pxy_src_port_srch] nvarchar(255) NULL ,
[pxy_src_protocal_srch] nvarchar(255) NULL ,
[pxy_src_request_method_srch] nvarchar(255) NULL ,
[pxy_src_location_srch] nvarchar(255) NULL ,
[pxy_src_type_srch] nvarchar(255) NULL ,
[pxy_src_user_srch] nvarchar(255) NULL ,
[pxy_src_pass_srch] nvarchar(255) NULL ,
[pxy_src_domain_srch] nvarchar(255) NULL ,
[pxy_src_create_time] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[tb_proxy_source]', RESEED, 6)
GO

-- ----------------------------
-- Records of tb_proxy_source
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tb_proxy_source] ON
GO
INSERT INTO [dbo].[tb_proxy_source] ([pxy_src_id], [pxy_src_name], [pxy_src_cathe_size], [pxy_src_url], [pxy_src_url_para], [pxy_src_request_timespan], [pxy_src_refresh_timespan], [pxy_src_charset], [pxy_src_request_method], [pxy_src_doc_type], [pxy_src_srch_type], [pxy_src_ip_add_srch], [pxy_src_port_srch], [pxy_src_protocal_srch], [pxy_src_request_method_srch], [pxy_src_location_srch], [pxy_src_type_srch], [pxy_src_user_srch], [pxy_src_pass_srch], [pxy_src_domain_srch], [pxy_src_create_time]) VALUES (N'1', N'西刺代理', N'10000', N'http://www.xicidaili.com/nn/[*]', N'1,5,1', N'1000', N'300000', N'UTF-8', N'Get', N'HTML', N'XPATH', N'//table[@id=''ip_list'']/tr/td[2]', N'//table[@id=''ip_list'']/tr/td[3]', N'//table[@id=''ip_list'']/tr/td[6]', null, N'//table[@id=''ip_list'']/tr/td[4]', N'//table[@id=''ip_list'']/tr/td[5]', null, null, null, N'2017-02-01 15:14:19.000')
GO
SET IDENTITY_INSERT [dbo].[tb_proxy_source] OFF
GO

-- ----------------------------
-- Table structure for tb_validation
-- ----------------------------
DROP TABLE [dbo].[tb_validation]
GO
CREATE TABLE [dbo].[tb_validation] (
[vld_id] int NOT NULL IDENTITY(1,1) ,
[vld_name] nvarchar(255) NULL ,
[vld_url] nvarchar(MAX) NOT NULL ,
[vld_request_method] nvarchar(255) NULL ,
[vld_pass_regex] nvarchar(255) NULL ,
[vld_timeout] int NULL ,
[vld_attemps] int NULL ,
[vld_status] int NULL ,
[vld_create_time] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[tb_validation]', RESEED, 1001)
GO

-- ----------------------------
-- Records of tb_validation
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tb_validation] ON
GO
INSERT INTO [dbo].[tb_validation] ([vld_id], [vld_name], [vld_url], [vld_request_method], [vld_pass_regex], [vld_timeout], [vld_attemps], [vld_status], [vld_create_time]) VALUES (N'1', N'百度一下，你就知道666', N'https://www.baidu.com/', N'Get', N'百度一下，你就知道', N'300', N'3', N'1', N'2017-02-01 13:21:33.000')
GO
SET IDENTITY_INSERT [dbo].[tb_validation] OFF
GO

-- ----------------------------
-- Indexes structure for table tb_log
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_log
-- ----------------------------
ALTER TABLE [dbo].[tb_log] ADD PRIMARY KEY ([log_id])
GO

-- ----------------------------
-- Indexes structure for table tb_pool
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_pool
-- ----------------------------
ALTER TABLE [dbo].[tb_pool] ADD PRIMARY KEY ([pool_id])
GO

-- ----------------------------
-- Indexes structure for table tb_proxy
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_proxy
-- ----------------------------
ALTER TABLE [dbo].[tb_proxy] ADD PRIMARY KEY ([pxy_id])
GO

-- ----------------------------
-- Indexes structure for table tb_validation
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tb_validation
-- ----------------------------
ALTER TABLE [dbo].[tb_validation] ADD PRIMARY KEY ([vld_id])
GO
