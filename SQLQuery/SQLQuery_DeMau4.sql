/* De mau4 */
use Northwind
go

/* Cau 1a */
create proc ThemSupplier
	@CompanyName nvarchar(60),                             
	@ContactName nvarchar(60),                   
	@ContactTitle nvarchar(60),                   
	@Address nvarchar(60),                                                      
	@City nvarchar(60),           
	@Region nvarchar(60),         
	@PostalCode nvarchar(60),
	@Country nvarchar(60),        
	@Phone nvarchar(60),                    
	@Fax nvarchar(60),                      
	@HomePage nvarchar(60)
as
begin
	insert into [dbo].[Suppliers]
		([CompanyName]
		,[ContactName]
		,[ContactTitle]
		,[Address]
		,[City]
		,[Region]
		,[PostalCode]
		,[Country]
		,[Phone]
		,[Fax]
		,[HomePage])
	values
		(@CompanyName,                             
		@ContactName,                   
		@ContactTitle,                   
		@Address,                                                      
		@City,           
		@Region,         
		@PostalCode,
		@Country,        
		@Phone,                    
		@Fax,                      
		@HomePage)
	select * from [dbo].[Suppliers] where SupplierID = @@identity
end
go
exec ThemSupplier 'ASD', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11'

/* Cau 1b */
alter proc CapNhatSupplier
	@SupplierID int,
	@CompanyName nvarchar(60),                             
	@ContactName nvarchar(60),                   
	@ContactTitle nvarchar(60),                   
	@Address nvarchar(60),                                                      
	@City nvarchar(60),           
	@Region nvarchar(60),         
	@PostalCode nvarchar(60),
	@Country nvarchar(60),        
	@Phone nvarchar(60),                    
	@Fax nvarchar(60),                      
	@HomePage nvarchar(60)
as
begin
	update [dbo].[Suppliers]
	set 
		[CompanyName] = @CompanyName,
		[ContactName] = @ContactName,
		[ContactTitle] = @ContactTitle,
		[Address] = @Address,
		[City] = @City,
		[Region] = @Region,
		[PostalCode] = @PostalCode,
		[Country] = @Country,
		[Phone] = @Phone,
		[Fax] = @Fax,
		[HomePage] = @HomePage
	where SupplierID = @SupplierID
	select * from [dbo].[Suppliers] where SupplierID = @SupplierID
end
go
exec CapNhatSupplier 30, 'QWE', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11'

/* Cau 1c */
create proc TimKiemSupplierTheoCompanyNameVaCountry
	@size int,
	@page int,
	@CompanyName nvarchar(40),
	@Country nvarchar(15)
as
begin
	declare @start int = @size * @page - @size + 1;
	declare @end int = @size * @page;

	with s as (
		select row_number() over (order by CompanyName) as STT, *
		from Suppliers
		where CompanyName like '%' + @CompanyName + '%' or Country like '%' + @Country + '%'
	)
	select * from s
	where STT between @start and @end 
end
go
exec TimKiemSupplierTheoCompanyNameVaCountry 100, 1, 'Ex', 'U'