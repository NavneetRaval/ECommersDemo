
USE [master]
GO
/****** Object:  Database [ECommerceDemo]    Script Date: 11/06/2020 10:18:22 PM ******/
DROP DATABASE [ECommerceDemo]
GO
/****** Object:  Database [ECommerceDemo]    Script Date: 11/06/2020 10:18:22 PM ******/
CREATE DATABASE [ECommerceDemo]
 GO
USE [ECommerceDemo]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProdCatId] [int] NOT NULL,
	[ProdName] [varchar](250) NOT NULL,
	[ProdDescription] [varchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttribute](
	[ProductId] [bigint] NOT NULL,
	[AttributeId] [int] NOT NULL,
	[AttributeValue] [varchar](250) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttributeLookup]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttributeLookup](
	[AttributeId] [int] IDENTITY(1,1) NOT NULL,
	[ProdCatId] [int] NOT NULL,
	[AttributeName] [varchar](250) NOT NULL,
 CONSTRAINT [PK_ProductAttributeLookup] PRIMARY KEY CLUSTERED 
(
	[AttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProdCatId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](250) NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProdCatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([ProductId], [ProdCatId], [ProdName], [ProdDescription]) VALUES (2, 2, N'Asus', N'laptop')
GO
INSERT [dbo].[Product] ([ProductId], [ProdCatId], [ProdName], [ProdDescription]) VALUES (3, 1, N'Product C', N'hhh')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (2, 4, N'4')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (2, 5, N'12')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (2, 6, N'12')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (3, 1, N'White')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (3, 2, N'77')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (3, 3, N'GJ18258')
GO
SET IDENTITY_INSERT [dbo].[ProductAttributeLookup] ON 
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (1, 1, N'Color')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (2, 1, N'Make')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (3, 1, N'Model')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (4, 2, N'RAM')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (5, 2, N'Front Camera')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (6, 2, N'Rear Camera')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (7, 2, N'RAM2')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (8, 2, N'RAM3')
GO
SET IDENTITY_INSERT [dbo].[ProductAttributeLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 
GO
INSERT [dbo].[ProductCategory] ([ProdCatId], [CategoryName]) VALUES (1, N'Car')
GO
INSERT [dbo].[ProductCategory] ([ProdCatId], [CategoryName]) VALUES (2, N'Mobile')
GO
INSERT [dbo].[ProductCategory] ([ProdCatId], [CategoryName]) VALUES (3, N'Laptop')
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY([ProdCatId])
REFERENCES [dbo].[ProductCategory] ([ProdCatId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductCategory]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_Product]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_ProductAttributeLookup] FOREIGN KEY([AttributeId])
REFERENCES [dbo].[ProductAttributeLookup] ([AttributeId])
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_ProductAttributeLookup]
GO
ALTER TABLE [dbo].[ProductAttributeLookup]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttributeLookup_ProductCategory] FOREIGN KEY([ProdCatId])
REFERENCES [dbo].[ProductCategory] ([ProdCatId])
GO
ALTER TABLE [dbo].[ProductAttributeLookup] CHECK CONSTRAINT [FK_ProductAttributeLookup_ProductCategory]
GO
/****** Object:  StoredProcedure [dbo].[AddupdateCategoriesdetails]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[AddupdateCategoriesdetails]
(
 @CategoryName         VARCHAR (250), 
 @ProdCatId         INT 
 
 ) 
AS 
  BEGIN 
      IF @ProdCatId > 0 
        BEGIN 
            UPDATE ProductCategory 
            SET    
                  
                   CategoryName = @CategoryName 
            WHERE  ProdCatId = @ProdCatId 
        END 
      ELSE 
        BEGIN 
            INSERT INTO ProductCategory(CategoryName) 
            VALUES     (@CategoryName) 
        END 
  END 
GO
/****** Object:  StoredProcedure [dbo].[AddUpdateProductAttribute]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddUpdateProductAttribute]
(
 @AttributeValue      VARCHAR (250), 
 @ProductId           INT =null,
 @AttributeId         INT =null
 ) 
AS 
  BEGIN 
      IF @ProductId > 0 
        BEGIN 
            delete from  [dbo].[ProductAttribute] where ProductId=@ProductId and AttributeId=@AttributeId

		    insert into [dbo].[ProductAttribute](ProductId,AttributeId,AttributeValue) 
			values(@ProductId,@AttributeId,@AttributeValue)

        END 
    
  END 
GO
/****** Object:  StoredProcedure [dbo].[AddUpdateProductAttributeLookup]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[AddUpdateProductAttributeLookup]
(
 @AttributeName       VARCHAR (250), 
 @ProdCatId           INT =null,
 @AttributeId         INT =null
 
 ) 
AS 
  BEGIN 
      IF @AttributeId > 0 
        BEGIN 
            UPDATE [dbo].[ProductAttributeLookup]
            SET     AttributeName = @AttributeName 
            WHERE  ProdCatId = @ProdCatId and AttributeId=@AttributeId
        END 
      ELSE 
        BEGIN 
            INSERT INTO [dbo].[ProductAttributeLookup](AttributeName,ProdCatId) 
            VALUES     (@AttributeName,@ProdCatId) 
        END 
  END 
GO
/****** Object:  StoredProcedure [dbo].[Addupdateproductdetails]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Addupdateproductdetails]
(@ProductId         INT, 
 @ProdName          VARCHAR (250), 
 @ProdCatId         INT, 
 @ProdDescription   VARCHAR (Max)
 ) 
AS 
  BEGIN 
      IF @ProductId > 0 
        BEGIN 
            UPDATE product 
            SET    ProdName = @ProdName, 
                   ProdCatId = @ProdCatId, 
                   ProdDescription = @ProdDescription 
            WHERE  productid = @ProductId 
			select  @ProductId as  productid 
                  
        END 
      ELSE 
        BEGIN 
            INSERT INTO product(ProdName,ProdCatId,ProdDescription) 
            VALUES     (@ProdName, 
                        @ProdCatId, 
                        @ProdDescription) 
						select  @@IDENTITY as  productid
        END 
		
  END 
GO
/****** Object:  StoredProcedure [dbo].[DeleteProductAttributebyid]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DeleteProductAttributebyid] (@AttributeId INT) 
AS 
  BEGIN 
      DELETE FROM  [dbo].[ProductAttributeLookup]
      WHERE  AttributeId = @AttributeId 
  END 
GO
/****** Object:  StoredProcedure [dbo].[Deleteproductbyid]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Deleteproductbyid] (@ProductId INT) 
AS 
  BEGIN 
      DELETE FROM product 
      WHERE  productid = @ProductId 
  END 
GO
/****** Object:  StoredProcedure [dbo].[DeleteproductCategorybyid]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[DeleteproductCategorybyid] (@ProductId INT) 
AS 
  BEGIN 
      DELETE FROM ProductCategory 
      WHERE  ProdCatId = @ProductId 
  END 
GO
/****** Object:  StoredProcedure [dbo].[getProductAttributeById]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
  
 -- EXEC [getProductAttributeById] 1008
CREATE Procedure [dbo].[getProductAttributeById]  
(  
    @AttributeId INT =null  
)  
AS  
BEGIN   
   
 SELECT c.ProdCatId,  
           p.CategoryName,  
     c.AttributeId,
	 c.AttributeName
       
  FROM  [dbo].[ProductAttributeLookup] c  
  inner join [dbo].[ProductCategory] p on p.ProdCatId=c.ProdCatId  
    
  WHERE   
   c.AttributeId=@AttributeId  
   
END  
GO
/****** Object:  StoredProcedure [dbo].[GetProductAttributeInfo]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- EXEC GetProductAttributeInfo NULL,NULL,NULL,1,10
CREATE Procedure [dbo].[GetProductAttributeInfo]
(
    @FilterText varchar(200) = NULL
   ,@SortColumn varchar(20) = 'ProdCatId'
   ,@SortDirection varchar(4)= 'desc'
   ,@PageIndex INT =1
   ,@PageSize INT  =10
)
AS
BEGIN 
	IF OBJECT_ID('tempdb..#ProductAttributeLookup') IS NOT NULL
			DROP TABLE #ProductAttributeLookup

	CREATE TABLE #ProductAttributeLookup 
	(
		
		AttributeId int NULL,
		ProdCatId int null,
		CategoryName VARCHAR(250) NULL,
		AttributeName VARCHAR(250) NULL,
		RowNumber int
		
	)
	INSERT INTO #ProductAttributeLookup(AttributeId,ProdCatId,CategoryName,AttributeName,RowNumber)
	SELECT c.AttributeId,
           c.ProdCatId,
		   cc.CategoryName,
		   c.AttributeName,
		 ROW_NUMBER() OVER (
			ORDER BY 
			CASE WHEN @SortColumn = 'AttributeId' and @SortDirection = 'asc' 
				THEN c.ProdCatId END ASC, 
			CASE WHEN @SortColumn = 'AttributeId' and @SortDirection = 'desc' 
				THEN c.ProdCatId END DESC,
			CASE WHEN @SortColumn = 'CategoryName' and @SortDirection = 'asc' 
				THEN cc.CategoryName END ASC, 
			CASE WHEN @SortColumn = 'CategoryName' and @SortDirection = 'desc' 
				THEN cc.CategoryName END DESC,
			CASE WHEN @SortColumn = 'AttributeName' and @SortDirection = 'asc' 
				THEN c.AttributeName END ASC, 
			CASE WHEN @SortColumn = 'AttributeName' and @SortDirection = 'desc' 
				THEN c.AttributeName END DESC
			) row_num
		FROM [dbo].ProductAttributeLookup c 
		inner join [dbo].[ProductCategory] cc on cc.ProdCatId=c.ProdCatId
		WHERE 
		 cc.CategoryName LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, cc.CategoryName) +'%'
		or c.AttributeName LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, c.AttributeName) +'%'
		or c.AttributeId LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, cast(c.AttributeId as varchar)) +'%'

	 DECLARE @TotalRowCount as int
	 SELECT @TotalRowCount=COUNT(1) FROM #ProductAttributeLookup
			SELECT RowNumber,CategoryName,ProdCatId,AttributeName,AttributeId,
				@TotalRowCount AS TotalRowCount 
			FROM #ProductAttributeLookup
			WHERE RowNumber BETWEEN ((@PageIndex-1)*@PageSize)+1 AND  (@PageSize*@PageIndex)	
			ORDER BY RowNumber
	
	IF OBJECT_ID('tempdb..#ProductAttributeLookup') IS NOT NULL
			DROP TABLE #ProductAttributeLookup

END
GO
/****** Object:  StoredProcedure [dbo].[getProductAttributeLookupById]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[getProductAttributeLookupById]
(
    @ProdCatId INT =null,
	@ProductId int=null
)
AS
BEGIN 
	if(@ProductId>0)
	begin
	SELECT distinct p.AttributeId,
           p.ProdCatId,
		   p.AttributeName,
		   c.CategoryName,
		  a.AttributeValue
		FROM [dbo].ProductAttributeLookup p
		Inner Join [dbo].[ProductCategory] c  on p.ProdCatId=c.ProdCatId
		Left join [dbo].[ProductAttribute] a on a.AttributeId=p.AttributeId
		WHERE 
		 p.ProdCatId=@ProdCatId and a.ProductId=@ProductId
		 end
		 else
		 begin
		 SELECT distinct p.AttributeId,
           p.ProdCatId,
		   p.AttributeName,
		   c.CategoryName
		  
		FROM [dbo].ProductAttributeLookup p
		Inner Join [dbo].[ProductCategory] c  on p.ProdCatId=c.ProdCatId
		where p.ProdCatId=@ProdCatId
		 end
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductCategoriesData]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- EXEC GetProductCategoriesData NULL,NULL,NULL,1,10
CREATE Procedure [dbo].[GetProductCategoriesData]
(
    @FilterText varchar(200) = NULL
   ,@SortColumn varchar(20) = 'ProdCatId'
   ,@SortDirection varchar(4)= 'desc'
   ,@PageIndex INT =1
   ,@PageSize INT  =10
)
AS
BEGIN 
	IF OBJECT_ID('tempdb..#tmpProductCategories') IS NOT NULL
			DROP TABLE #tmpProductCategories

	CREATE TABLE #tmpProductCategories 
	(
		
		ProdCatId int NULL,
		CategoryName VARCHAR(250) NULL,
		RowNumber int
		
	)
	INSERT INTO #tmpProductCategories(ProdCatId,CategoryName,RowNumber)
	SELECT c.ProdCatId,
           c.CategoryName,
		 ROW_NUMBER() OVER (
			ORDER BY 
			CASE WHEN @SortColumn = 'ProdCatId' and @SortDirection = 'asc' 
				THEN c.ProdCatId END ASC, 
			CASE WHEN @SortColumn = 'ProdCatId' and @SortDirection = 'desc' 
				THEN c.ProdCatId END DESC,
			CASE WHEN @SortColumn = 'CategoryName' and @SortDirection = 'asc' 
				THEN c.CategoryName END ASC, 
			CASE WHEN @SortColumn = 'CategoryName' and @SortDirection = 'desc' 
				THEN c.CategoryName END DESC
			) row_num
		FROM [dbo].ProductCategory c 
		WHERE 
		 c.CategoryName LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, c.CategoryName) +'%'
		or c.ProdCatId LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, cast(c.ProdCatId as varchar)) +'%'

	 DECLARE @TotalRowCount as int
	 SELECT @TotalRowCount=COUNT(1) FROM #tmpProductCategories
			SELECT RowNumber,CategoryName,ProdCatId,
				@TotalRowCount AS TotalRowCount 
			FROM #tmpProductCategories
			WHERE RowNumber BETWEEN ((@PageIndex-1)*@PageSize)+1 AND  (@PageSize*@PageIndex)	
			ORDER BY RowNumber
	
	IF OBJECT_ID('tempdb..#tmpProductCategories') IS NOT NULL
			DROP TABLE #tmpProductCategories

END
GO
/****** Object:  StoredProcedure [dbo].[getProductCategoryDetilsById]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[getProductCategoryDetilsById]
(
    @ProdCatId INT =null
)
AS
BEGIN 
	
	SELECT c.ProdCatId,
           c.CategoryName
		 
		FROM [dbo].ProductCategory c 
		WHERE 
		 c.ProdCatId=@ProdCatId
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductData]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- EXEC GetProductData NULL,NULL,NULL,1,10
CREATE Procedure [dbo].[GetProductData]
(
   @FilterText varchar(200) = NULL
   ,@SortColumn varchar(20) = 'ProductId'
   ,@SortDirection varchar(4)= 'desc'
   ,@PageIndex INT =1
   ,@PageSize INT  =10
)
AS
BEGIN 
	IF OBJECT_ID('tempdb..#tmpProduct') IS NOT NULL
			DROP TABLE #tmpProduct

	CREATE TABLE #tmpProduct 
	(
		ProductId int NULL,
		ProdCatId int NULL,
		ProdDescription VARCHAR(max) NULL,
		CategoryName VARCHAR(250) NULL,
		ProdName     VARCHAR(250) NULL,
		RowNumber int
		
	)
	INSERT INTO #tmpProduct(ProdName,ProdCatId,ProdDescription,ProductId,CategoryName,RowNumber)
	SELECT p.ProdName,
           p.ProdCatId,
           p.ProdDescription,
           p.ProductId,
           c.CategoryName,
		 ROW_NUMBER() OVER (
			ORDER BY 
			CASE WHEN @SortColumn = 'ProductId' and @SortDirection = 'asc' 
				THEN p.ProductId END ASC, 
			CASE WHEN @SortColumn = 'ProductId' and @SortDirection = 'desc' 
				THEN p.ProductId END DESC,
			CASE WHEN @SortColumn = 'ProdName' and @SortDirection = 'asc' 
				THEN p.ProdName END ASC, 
			CASE WHEN @SortColumn = 'ProdName' and @SortDirection = 'desc' 
				THEN p.ProdName END DESC,
			CASE WHEN @SortColumn = 'CategoryName' and @SortDirection = 'asc' 
				THEN c.CategoryName END ASC, 
			CASE WHEN @SortColumn = 'CategoryName' and @SortDirection = 'desc' 
				THEN c.CategoryName END DESC,
			CASE WHEN @SortColumn = 'ProdDescription' and @SortDirection = 'asc' 
				THEN p.ProdDescription END ASC, 
			CASE WHEN @SortColumn = 'ProdDescription' and @SortDirection = 'desc' 
				THEN p.ProdDescription END DESC
		   ) row_num
		FROM [dbo].Product AS p
		INNER JOIN [dbo].ProductCategory c ON p.ProdCatId=c.ProdCatId
		WHERE p.ProdName LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, p.ProdName) +'%'
		or p.ProductId LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, cast(p.ProductId as varchar)) +'%'
		OR p.ProdDescription LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, p.ProdDescription) +'%'
		OR c.CategoryName LIKE '%'+ IIF(@FilterText IS NOT NULL, @FilterText, c.CategoryName) +'%'
		

	 DECLARE @TotalRowCount as int
	 SELECT @TotalRowCount=COUNT(1) FROM #tmpProduct
			SELECT RowNumber,ProdName,CategoryName,ProdCatId,ProdDescription,ProductId,
				@TotalRowCount AS TotalRowCount 
			FROM #tmpProduct
			WHERE RowNumber BETWEEN ((@PageIndex-1)*@PageSize)+1 AND  (@PageSize*@PageIndex)	
			ORDER BY RowNumber
	
	

END
GO
/****** Object:  StoredProcedure [dbo].[getProductDetilsById]    Script Date: 11/06/2020 10:18:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[getProductDetilsById]
(
    @ProductId INT =null
)
AS
BEGIN 
	
	SELECT p.ProductId,
           p.ProdCatId,
		   p.ProdDescription,
		   p.ProdName
		 
		FROM [dbo].Product p
		WHERE 
		 p.ProductId=@ProductId
	
END
GO
USE [master]
GO
ALTER DATABASE [ECommerceDemo] SET  READ_WRITE 
GO
