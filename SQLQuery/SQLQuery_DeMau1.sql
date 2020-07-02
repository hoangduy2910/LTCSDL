/* Cau 1a */
create proc DoanhThuNhanVienTrongNgay
	@date datetime
as
begin
	select e.EmployeeID, e.FirstName, e.LastName, ROUND(SUM(od.Quantity * od.UnitPrice * (1 - od.Discount)) ,2) as DoanhThu
	from Employees e inner join Orders o on e.EmployeeID = o.EmployeeID 
			inner join [Order Details] od on o.OrderID = od.OrderID
	where DAY(o.OrderDate) = DAY(@date) and
		  MONTH(o.OrderDate) = MONTH(@date) and
		  YEAR(o.OrderDate) = YEAR(@date)
	group by e.EmployeeID, e.FirstName, e.LastName
end
go
exec DoanhThuNhanVienTrongNgay '1997-07-09'


/* Cau 1b */
create proc DoanhThuNhanVienTrongKhoangThoiGian
	@dateBegin datetime,
	@dateEnd datetime
as
begin
	select e.EmployeeID, e.FirstName, e.LastName, ROUND(SUM(od.Quantity * od.UnitPrice * (1 - od.Discount)) ,2) as DoanhThu
	from Employees e inner join Orders o on e.EmployeeID = o.EmployeeID 
			inner join [Order Details] od on o.OrderID = od.OrderID
	where o.OrderDate between @dateBegin and @dateEnd
	group by e.EmployeeID, e.FirstName, e.LastName
end
go
exec DoanhThuNhanVienTrongKhoangThoiGian '1997-07-09', '1997-08-09' 