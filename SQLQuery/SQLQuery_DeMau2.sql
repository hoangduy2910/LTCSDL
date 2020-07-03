/* De mau 2 */
use Northwind
go

/* Cau 1 */
create proc DanhSachDonHangTrongKhoangThoiGian 
	@dateF datetime, 
	@dateT datetime,
	@size int,
	@page int
as
begin
	declare @start int = @size * @page - @size - 1;
	declare @end int = @size * @page;

	with s as (
		select row_number() over (order by OrderDate) as STT, *
		from Orders
		where OrderDate between @dateF and @dateT	
	)

	select * 
	from s
	where STT between @start and @end
end
go
exec DanhSachDonHangTrongKhoangThoiGian '1996-07-04', '1996-07-10', 10, 1

create proc ChiTietDonHang
	@MaDH int
as
begin
	select *
	from [Order Details]
	where OrderID = @MaDH
end
go 
exec ChiTietDonHang 10250