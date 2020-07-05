/* De mau 5 */
use Northwind
go

/* Cau 1a */ 
create proc DanhSachProductKhongCoDonHangTrongNgay
	@size int,
	@page int,
	@date datetime
as
begin
	declare @start int = @size * @page - @size + 1;
	declare @end int = @size * @page;

	with s as (
		select row_number() over (order by ProductName) as STT, *
		from Products
		where ProductID not in (
			select distinct p.ProductID
			from Products p inner join [Order Details] od on p.ProductID = od.ProductID
				inner join Orders o on od.OrderID = o.OrderID
			where o.OrderDate = @date
		)
	)
	select * from s
	where STT between @start and @end
end
go
exec DanhSachProductKhongCoDonHangTrongNgay 10, 1, '1996-07-08'


/* Cau 1b */ 
create proc DanhSachProductKhongCoTonKho
	@size int,
	@page int
as
begin
	declare @start int = @size * @page - @size + 1;
	declare @end int = @size * @page;

	with s as (
		select row_number() over (order by ProductName) as STT, *
		from Products
		where UnitsInStock = 0
	)
	select * from s
	where STT between @start and @end
end
go
exec DanhSachProductKhongCoTonKho 100, 1

/* Cau 1c */ 
create proc TimKiemOrderTheoCompanyNameVaEmployeeName
	@size int,
	@page int,
	@companyName nvarchar(50),
	@employeeName nvarchar(50)
as
begin
	declare @start int = @size * @page - @size + 1;
	declare @end int = @size * @page;

	with s as (
		select row_number() over (order by o.OrderDate) as STT, o.*
		from Orders o inner join Employees e on o.EmployeeID = e.EmployeeID
				inner join Customers c on o.CustomerID = c.CustomerID
		where c.CompanyName like '%' + @companyName + '%' and 
				(e.FirstName like '%' + @employeeName + '%' or e.LastName like '%' + @employeeName + '%')
	)
	select * from s
	where STT between @start and @end
end
go
exec TimKiemOrderTheoCompanyNameVaEmployeeName 100, 1, 'Alfreds Futterkiste', 'Pea'