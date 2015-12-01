
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/06/2015 21:37:03
-- Generated from EDMX file: C:\Users\Amandeep Brar\Source\Repos\TechPirates_InterviewTube\CapModel\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CapstoneDB1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_JobSeekerLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locations] DROP CONSTRAINT [FK_JobSeekerLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerCertification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Certifications] DROP CONSTRAINT [FK_JobSeekerCertification];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerEducation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Educations] DROP CONSTRAINT [FK_JobSeekerEducation];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerWorkExperience]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkExperiences] DROP CONSTRAINT [FK_JobSeekerWorkExperience];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyRecruiter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Recruiters] DROP CONSTRAINT [FK_CompanyRecruiter];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyNewsPost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsPosts] DROP CONSTRAINT [FK_CompanyNewsPost];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyJobPosting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobPostings] DROP CONSTRAINT [FK_CompanyJobPosting];
GO
IF OBJECT_ID(N'[dbo].[FK_RecruiterJobPosting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobPostings] DROP CONSTRAINT [FK_RecruiterJobPosting];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerUserMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMedias] DROP CONSTRAINT [FK_JobSeekerUserMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_JobPostingUserMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMedias] DROP CONSTRAINT [FK_JobPostingUserMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyUserMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMedias] DROP CONSTRAINT [FK_CompanyUserMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_JobPostingJobApplied]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobApplieds] DROP CONSTRAINT [FK_JobPostingJobApplied];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerJobApplied]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobApplieds] DROP CONSTRAINT [FK_JobSeekerJobApplied];
GO
IF OBJECT_ID(N'[dbo].[FK_JobPostingLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locations] DROP CONSTRAINT [FK_JobPostingLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerSkillCollection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SkillCollections] DROP CONSTRAINT [FK_JobSeekerSkillCollection];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerUserProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProfiles] DROP CONSTRAINT [FK_JobSeekerUserProfile];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyLocation1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locations] DROP CONSTRAINT [FK_CompanyLocation1];
GO
IF OBJECT_ID(N'[dbo].[FK_RecruiterLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locations] DROP CONSTRAINT [FK_RecruiterLocation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Recruiters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recruiters];
GO
IF OBJECT_ID(N'[dbo].[JobSeekers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobSeekers];
GO
IF OBJECT_ID(N'[dbo].[SkillCollections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SkillCollections];
GO
IF OBJECT_ID(N'[dbo].[Certifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Certifications];
GO
IF OBJECT_ID(N'[dbo].[Educations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Educations];
GO
IF OBJECT_ID(N'[dbo].[WorkExperiences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkExperiences];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Followers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Followers];
GO
IF OBJECT_ID(N'[dbo].[NewsPosts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsPosts];
GO
IF OBJECT_ID(N'[dbo].[JobPostings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobPostings];
GO
IF OBJECT_ID(N'[dbo].[UserMedias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMedias];
GO
IF OBJECT_ID(N'[dbo].[UserProfiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfiles];
GO
IF OBJECT_ID(N'[dbo].[JobApplieds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobApplieds];
GO
IF OBJECT_ID(N'[dbo].[JobCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobCategories];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [LocationId] int IDENTITY(1,1) NOT NULL,
    [AptNum] nvarchar(max)  NULL,
    [StreetAdd] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Province] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [PostalCode] nvarchar(max)  NOT NULL,
    [JobSeeker_JSId] int  NULL,
    [JobPosting_JobPostId] int  NULL,
    [Company_CompId] int  NULL,
    [Recruiter_RecId] int  NULL
);
GO

-- Creating table 'Recruiters'
CREATE TABLE [dbo].[Recruiters] (
    [RecId] int IDENTITY(1,1) NOT NULL,
    [Department] nvarchar(max)  NOT NULL,
    [JobTitle] nvarchar(max)  NOT NULL,
    [HasAccess] nvarchar(max)  NOT NULL,
    [CompanyCompId] int  NOT NULL,
    [UserName] nvarchar(max)  NULL
);
GO

-- Creating table 'JobSeekers'
CREATE TABLE [dbo].[JobSeekers] (
    [JSId] int IDENTITY(1,1) NOT NULL,
    [SkillSummary] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [Visibility] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NULL
);
GO

-- Creating table 'SkillCollections'
CREATE TABLE [dbo].[SkillCollections] (
    [SkillId] int IDENTITY(1,1) NOT NULL,
    [SkillName] nvarchar(max)  NOT NULL,
    [JobSeekerJSId] int  NULL
);
GO

-- Creating table 'Certifications'
CREATE TABLE [dbo].[Certifications] (
    [CertId] int IDENTITY(1,1) NOT NULL,
    [CertName] nvarchar(max)  NOT NULL,
    [CertAuthority] nvarchar(max)  NOT NULL,
    [CertLicense] nvarchar(max)  NOT NULL,
    [CertDate] nvarchar(max)  NOT NULL,
    [JobSeekerJSId] int  NOT NULL
);
GO

-- Creating table 'Educations'
CREATE TABLE [dbo].[Educations] (
    [EduId] int IDENTITY(1,1) NOT NULL,
    [CourseName] nvarchar(max)  NOT NULL,
    [SchoolName] nvarchar(max)  NOT NULL,
    [EduLocation] nvarchar(max)  NOT NULL,
    [EduStartDate] nvarchar(max)  NOT NULL,
    [EduEndDate] nvarchar(max)  NULL,
    [EduDescription] nvarchar(max)  NULL,
    [EduGrade] nvarchar(max)  NULL,
    [JobSeekerJSId] int  NOT NULL
);
GO

-- Creating table 'WorkExperiences'
CREATE TABLE [dbo].[WorkExperiences] (
    [ExperienceId] int IDENTITY(1,1) NOT NULL,
    [ExpCompany] nvarchar(max)  NOT NULL,
    [ExpTitle] nvarchar(max)  NOT NULL,
    [ExpLocation] nvarchar(max)  NULL,
    [ExpStartDate] nvarchar(max)  NOT NULL,
    [ExpEndDate] nvarchar(max)  NULL,
    [CurrentPosition] nvarchar(max)  NULL,
    [ExpSummary] nvarchar(max)  NOT NULL,
    [JobSeekerJSId] int  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [CompId] int IDENTITY(1,1) NOT NULL,
    [CompName] nvarchar(max)  NOT NULL,
    [CompCode] nvarchar(max)  NOT NULL,
    [CompDescription] nvarchar(max)  NOT NULL,
    [NumFollowers] int  NOT NULL,
    [UserName] nvarchar(max)  NULL
);
GO

-- Creating table 'Followers'
CREATE TABLE [dbo].[Followers] (
    [FollowId] int IDENTITY(1,1) NOT NULL,
    [ViewNews] nvarchar(max)  NOT NULL,
    [ViewJobs] nvarchar(max)  NOT NULL,
    [PostSeen] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NewsPosts'
CREATE TABLE [dbo].[NewsPosts] (
    [NPId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [NewsDesc] nvarchar(max)  NOT NULL,
    [NewsDate] nvarchar(max)  NOT NULL,
    [IsVisible] nvarchar(max)  NOT NULL,
    [CompanyCompId] int  NOT NULL
);
GO

-- Creating table 'JobPostings'
CREATE TABLE [dbo].[JobPostings] (
    [JobPostId] int IDENTITY(1,1) NOT NULL,
    [JobTitle] nvarchar(max)  NOT NULL,
    [JobType] nvarchar(max)  NOT NULL,
    [PostStartDate] datetime  NOT NULL,
    [PostEndDate] datetime  NOT NULL,
    [NumPositions] nvarchar(max)  NOT NULL,
    [JobLevel] nvarchar(max)  NOT NULL,
    [JobCompensation] float  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [JobReq] nvarchar(max)  NOT NULL,
    [JobDuties] nvarchar(max)  NOT NULL,
    [CompanyCompId] int  NOT NULL,
    [RecruiterRecId] int  NOT NULL,
    [JobVisible] nvarchar(max)  NULL,
    [JobCategory] nvarchar(max)  NOT NULL,
    [JobSubcategory] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserMedias'
CREATE TABLE [dbo].[UserMedias] (
    [UMediaId] int IDENTITY(1,1) NOT NULL,
    [VidPath] nvarchar(max)  NOT NULL,
    [MediaDesc] nvarchar(max)  NOT NULL,
    [JobSeeker_JSId] int  NULL,
    [JobPosting_JobPostId] int  NULL,
    [Company_CompId] int  NULL
);
GO

-- Creating table 'UserProfiles'
CREATE TABLE [dbo].[UserProfiles] (
    [ProfileId] int IDENTITY(1,1) NOT NULL,
    [PicPath] nvarchar(max)  NOT NULL,
    [PicDesc] nvarchar(max)  NOT NULL,
    [JobSeeker_JSId] int  NULL
);
GO

-- Creating table 'JobApplieds'
CREATE TABLE [dbo].[JobApplieds] (
    [JobAppId] int IDENTITY(1,1) NOT NULL,
    [ApplicationDate] nvarchar(max)  NOT NULL,
    [AppliedMessage] nvarchar(max)  NULL,
    [JobPostingJobPostId] int  NOT NULL,
    [JobSeekerJSId] int  NOT NULL
);
GO

-- Creating table 'JobCategories'
CREATE TABLE [dbo].[JobCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CatName] nvarchar(max)  NOT NULL,
    [SubCategory] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Province] nvarchar(max)  NOT NULL,
    [CountryName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [LocationId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([LocationId] ASC);
GO

-- Creating primary key on [RecId] in table 'Recruiters'
ALTER TABLE [dbo].[Recruiters]
ADD CONSTRAINT [PK_Recruiters]
    PRIMARY KEY CLUSTERED ([RecId] ASC);
GO

-- Creating primary key on [JSId] in table 'JobSeekers'
ALTER TABLE [dbo].[JobSeekers]
ADD CONSTRAINT [PK_JobSeekers]
    PRIMARY KEY CLUSTERED ([JSId] ASC);
GO

-- Creating primary key on [SkillId] in table 'SkillCollections'
ALTER TABLE [dbo].[SkillCollections]
ADD CONSTRAINT [PK_SkillCollections]
    PRIMARY KEY CLUSTERED ([SkillId] ASC);
GO

-- Creating primary key on [CertId] in table 'Certifications'
ALTER TABLE [dbo].[Certifications]
ADD CONSTRAINT [PK_Certifications]
    PRIMARY KEY CLUSTERED ([CertId] ASC);
GO

-- Creating primary key on [EduId] in table 'Educations'
ALTER TABLE [dbo].[Educations]
ADD CONSTRAINT [PK_Educations]
    PRIMARY KEY CLUSTERED ([EduId] ASC);
GO

-- Creating primary key on [ExperienceId] in table 'WorkExperiences'
ALTER TABLE [dbo].[WorkExperiences]
ADD CONSTRAINT [PK_WorkExperiences]
    PRIMARY KEY CLUSTERED ([ExperienceId] ASC);
GO

-- Creating primary key on [CompId] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([CompId] ASC);
GO

-- Creating primary key on [FollowId] in table 'Followers'
ALTER TABLE [dbo].[Followers]
ADD CONSTRAINT [PK_Followers]
    PRIMARY KEY CLUSTERED ([FollowId] ASC);
GO

-- Creating primary key on [NPId] in table 'NewsPosts'
ALTER TABLE [dbo].[NewsPosts]
ADD CONSTRAINT [PK_NewsPosts]
    PRIMARY KEY CLUSTERED ([NPId] ASC);
GO

-- Creating primary key on [JobPostId] in table 'JobPostings'
ALTER TABLE [dbo].[JobPostings]
ADD CONSTRAINT [PK_JobPostings]
    PRIMARY KEY CLUSTERED ([JobPostId] ASC);
GO

-- Creating primary key on [UMediaId] in table 'UserMedias'
ALTER TABLE [dbo].[UserMedias]
ADD CONSTRAINT [PK_UserMedias]
    PRIMARY KEY CLUSTERED ([UMediaId] ASC);
GO

-- Creating primary key on [ProfileId] in table 'UserProfiles'
ALTER TABLE [dbo].[UserProfiles]
ADD CONSTRAINT [PK_UserProfiles]
    PRIMARY KEY CLUSTERED ([ProfileId] ASC);
GO

-- Creating primary key on [JobAppId] in table 'JobApplieds'
ALTER TABLE [dbo].[JobApplieds]
ADD CONSTRAINT [PK_JobApplieds]
    PRIMARY KEY CLUSTERED ([JobAppId] ASC);
GO

-- Creating primary key on [Id] in table 'JobCategories'
ALTER TABLE [dbo].[JobCategories]
ADD CONSTRAINT [PK_JobCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [JobSeeker_JSId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [FK_JobSeekerLocation]
    FOREIGN KEY ([JobSeeker_JSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerLocation'
CREATE INDEX [IX_FK_JobSeekerLocation]
ON [dbo].[Locations]
    ([JobSeeker_JSId]);
GO

-- Creating foreign key on [JobSeekerJSId] in table 'Certifications'
ALTER TABLE [dbo].[Certifications]
ADD CONSTRAINT [FK_JobSeekerCertification]
    FOREIGN KEY ([JobSeekerJSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerCertification'
CREATE INDEX [IX_FK_JobSeekerCertification]
ON [dbo].[Certifications]
    ([JobSeekerJSId]);
GO

-- Creating foreign key on [JobSeekerJSId] in table 'Educations'
ALTER TABLE [dbo].[Educations]
ADD CONSTRAINT [FK_JobSeekerEducation]
    FOREIGN KEY ([JobSeekerJSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerEducation'
CREATE INDEX [IX_FK_JobSeekerEducation]
ON [dbo].[Educations]
    ([JobSeekerJSId]);
GO

-- Creating foreign key on [JobSeekerJSId] in table 'WorkExperiences'
ALTER TABLE [dbo].[WorkExperiences]
ADD CONSTRAINT [FK_JobSeekerWorkExperience]
    FOREIGN KEY ([JobSeekerJSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerWorkExperience'
CREATE INDEX [IX_FK_JobSeekerWorkExperience]
ON [dbo].[WorkExperiences]
    ([JobSeekerJSId]);
GO

-- Creating foreign key on [CompanyCompId] in table 'Recruiters'
ALTER TABLE [dbo].[Recruiters]
ADD CONSTRAINT [FK_CompanyRecruiter]
    FOREIGN KEY ([CompanyCompId])
    REFERENCES [dbo].[Companies]
        ([CompId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyRecruiter'
CREATE INDEX [IX_FK_CompanyRecruiter]
ON [dbo].[Recruiters]
    ([CompanyCompId]);
GO

-- Creating foreign key on [CompanyCompId] in table 'NewsPosts'
ALTER TABLE [dbo].[NewsPosts]
ADD CONSTRAINT [FK_CompanyNewsPost]
    FOREIGN KEY ([CompanyCompId])
    REFERENCES [dbo].[Companies]
        ([CompId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyNewsPost'
CREATE INDEX [IX_FK_CompanyNewsPost]
ON [dbo].[NewsPosts]
    ([CompanyCompId]);
GO

-- Creating foreign key on [CompanyCompId] in table 'JobPostings'
ALTER TABLE [dbo].[JobPostings]
ADD CONSTRAINT [FK_CompanyJobPosting]
    FOREIGN KEY ([CompanyCompId])
    REFERENCES [dbo].[Companies]
        ([CompId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyJobPosting'
CREATE INDEX [IX_FK_CompanyJobPosting]
ON [dbo].[JobPostings]
    ([CompanyCompId]);
GO

-- Creating foreign key on [RecruiterRecId] in table 'JobPostings'
ALTER TABLE [dbo].[JobPostings]
ADD CONSTRAINT [FK_RecruiterJobPosting]
    FOREIGN KEY ([RecruiterRecId])
    REFERENCES [dbo].[Recruiters]
        ([RecId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecruiterJobPosting'
CREATE INDEX [IX_FK_RecruiterJobPosting]
ON [dbo].[JobPostings]
    ([RecruiterRecId]);
GO

-- Creating foreign key on [JobSeeker_JSId] in table 'UserMedias'
ALTER TABLE [dbo].[UserMedias]
ADD CONSTRAINT [FK_JobSeekerUserMedia]
    FOREIGN KEY ([JobSeeker_JSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerUserMedia'
CREATE INDEX [IX_FK_JobSeekerUserMedia]
ON [dbo].[UserMedias]
    ([JobSeeker_JSId]);
GO

-- Creating foreign key on [JobPosting_JobPostId] in table 'UserMedias'
ALTER TABLE [dbo].[UserMedias]
ADD CONSTRAINT [FK_JobPostingUserMedia]
    FOREIGN KEY ([JobPosting_JobPostId])
    REFERENCES [dbo].[JobPostings]
        ([JobPostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobPostingUserMedia'
CREATE INDEX [IX_FK_JobPostingUserMedia]
ON [dbo].[UserMedias]
    ([JobPosting_JobPostId]);
GO

-- Creating foreign key on [Company_CompId] in table 'UserMedias'
ALTER TABLE [dbo].[UserMedias]
ADD CONSTRAINT [FK_CompanyUserMedia]
    FOREIGN KEY ([Company_CompId])
    REFERENCES [dbo].[Companies]
        ([CompId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyUserMedia'
CREATE INDEX [IX_FK_CompanyUserMedia]
ON [dbo].[UserMedias]
    ([Company_CompId]);
GO

-- Creating foreign key on [JobPostingJobPostId] in table 'JobApplieds'
ALTER TABLE [dbo].[JobApplieds]
ADD CONSTRAINT [FK_JobPostingJobApplied]
    FOREIGN KEY ([JobPostingJobPostId])
    REFERENCES [dbo].[JobPostings]
        ([JobPostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobPostingJobApplied'
CREATE INDEX [IX_FK_JobPostingJobApplied]
ON [dbo].[JobApplieds]
    ([JobPostingJobPostId]);
GO

-- Creating foreign key on [JobSeekerJSId] in table 'JobApplieds'
ALTER TABLE [dbo].[JobApplieds]
ADD CONSTRAINT [FK_JobSeekerJobApplied]
    FOREIGN KEY ([JobSeekerJSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerJobApplied'
CREATE INDEX [IX_FK_JobSeekerJobApplied]
ON [dbo].[JobApplieds]
    ([JobSeekerJSId]);
GO

-- Creating foreign key on [JobPosting_JobPostId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [FK_JobPostingLocation]
    FOREIGN KEY ([JobPosting_JobPostId])
    REFERENCES [dbo].[JobPostings]
        ([JobPostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobPostingLocation'
CREATE INDEX [IX_FK_JobPostingLocation]
ON [dbo].[Locations]
    ([JobPosting_JobPostId]);
GO

-- Creating foreign key on [JobSeekerJSId] in table 'SkillCollections'
ALTER TABLE [dbo].[SkillCollections]
ADD CONSTRAINT [FK_JobSeekerSkillCollection]
    FOREIGN KEY ([JobSeekerJSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerSkillCollection'
CREATE INDEX [IX_FK_JobSeekerSkillCollection]
ON [dbo].[SkillCollections]
    ([JobSeekerJSId]);
GO

-- Creating foreign key on [JobSeeker_JSId] in table 'UserProfiles'
ALTER TABLE [dbo].[UserProfiles]
ADD CONSTRAINT [FK_JobSeekerUserProfile]
    FOREIGN KEY ([JobSeeker_JSId])
    REFERENCES [dbo].[JobSeekers]
        ([JSId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerUserProfile'
CREATE INDEX [IX_FK_JobSeekerUserProfile]
ON [dbo].[UserProfiles]
    ([JobSeeker_JSId]);
GO

-- Creating foreign key on [Company_CompId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [FK_CompanyLocation1]
    FOREIGN KEY ([Company_CompId])
    REFERENCES [dbo].[Companies]
        ([CompId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyLocation1'
CREATE INDEX [IX_FK_CompanyLocation1]
ON [dbo].[Locations]
    ([Company_CompId]);
GO

-- Creating foreign key on [Recruiter_RecId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [FK_RecruiterLocation]
    FOREIGN KEY ([Recruiter_RecId])
    REFERENCES [dbo].[Recruiters]
        ([RecId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecruiterLocation'
CREATE INDEX [IX_FK_RecruiterLocation]
ON [dbo].[Locations]
    ([Recruiter_RecId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------