/* Đề mẫu 3 */
use Northwind
go

/* Câu 1a */
create proc DSDonHangNhanVienTrongKhoangThoiGianTheoKeyword
	@size int,
	@page int,
	@keyword nvarchar(20),
	@dateF datetime,
	@dateT datetime
as
begin
	declare @start int = @size * @page - @size + 1;
	declare @end int = @size * @page;

	with s as (
		select row_number() over (order by o.OrderDate) as STT, o.*
		from Orders o inner join Employees e on o.EmployeeID = e.EmployeeID
		where o.OrderDate between @dateF and @dateT and 
				((e.FirstName like ('%'+ @keyword + '%') or (e.LastName like ('%'+ @keyword + '%'))))
	)
	select * from s
	where STT between @start and @end 
end
go
exec DSDonHangNhanVienTrongKhoangThoiGianTheoKeyword 100, 1, 'Pea', '1996-07-08', '1997-07-08'

/* Câu 1b */
create proc DSMatHangBanChayNhat
	@size int,
	@page int,
	@month int,
	@year int
as
begin 
	declare @start int = @size * @page - @size + 1;
	declare @end int = @size * @page;

	with s as (
		select row_number() over (order by round(sum(od.UnitPrice * od.Quantity * (1 - od.Discount)), 2) desc) as STT, pro.ProductName, 
				round(sum(od.UnitPrice * od.Quantity * (1 - od.Discount)), 2) as DanhThu
		from Orders o inner join [Order Details] od on o.OrderID = od.OrderID inner join Products pro on od.ProductID = pro.ProductID
		where month(o.OrderDate) = @month and year(o.OrderDate) = @year
		group by pro.ProductName
	)
	select *
	from s
	where STT between @start and @end 
end
go
exec DSMatHangBanChayNhat 100, 1, 7, 1996

/* Câu 1c */
create proc DoanhThuTheoQuocGia
	@month int,
	@year int
as
begin
	select o.ShipCountry, round(sum(od.UnitPrice * od.Quantity * (1 - od.Discount)), 2) as DoanhThu
	from Orders o, [Order Details] od
	where o.OrderID = od.OrderID and
			month(o.OrderDate) = @month and
			year(o.OrderDate) = @year
	group by o.ShipCountry
end
go
exec DoanhThuTheoQuocGia 7, 1997