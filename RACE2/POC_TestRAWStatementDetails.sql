USE [pocracinfdb1401]
GO

INSERT INTO [dbo].[RAW_StatementDetails]
           ([DocumentName]
           ,[ReservoirName]
           ,[SupervisingEngineer_Short]
           ,[IsTypeOfStatement12_2]
           ,[IsTypeOfStatement12_2A]
           ,[NearestTown]
           ,[PeriodStart]
           ,[PeriodEnd]
           ,[StatementDate]
           ,[GridReference]
           ,[UndertakeName]
           ,[UndertakerAddress]
           ,[UndertakerEmail]
           ,[UndertakerPhone]
           ,[SupervisingEngineer_Long]
           ,[SupervisingEngineerCompany]
           ,[SupervisingEngineerAddress]
           ,[SupervisingEngineerEmail]
           ,[SupervisingEngineerPhone]
           ,[HasAlternativeEngineerYes]
           ,[HasAlternativeEngineerNo]
           ,[LastInspectingEngineerName]
           ,[LastInspectingEngineerPhone]
           ,[LastInspectionDate]
           ,[LastCertificationDate]
           ,[IsEarlyInspectionRequiredYes]
           ,[IsEarlyInspectionRequiredNo]
           ,[NextInspectionDate]
           ,[HasNoMaintenanceItems]
           ,[HasNoMIOSItems]
           ,[HasNoWatchItems]
           ,[LastModifiedDateTime]
           ,[SignatureDate]
           ,[SignatureStrip])
     VALUES
           ('1000020120231201_20231220150912.pdf','Reservoir One','Kriss Sahoo','TRUE','TRUE','Basildon, London','01/12/2022','06/11/2023','08/12/2023',
		   '574340','Thames Water','61 Collingwood Road, Basildon,  SS16 4BL','ava.johnson@race.tst','07875111223','Kriss Sahoo','Best Engineering Ltd',
		   '9 Moor View, Melkridge, NE49 0LS','kriss.sahoo@capgemini.com','07875111223','FALSE','TRUE','','','01/01/2014','01/01/2020','TRUE','FALSE','01/01/2034',
		   '','','','12/12/2023','12/12/2023','')
           
          
        
GO


