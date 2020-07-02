using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LTCSDL.DAL
{
    using Models;
    using LTCSDL.Common.DAL;
    using Microsoft.VisualBasic;
    using LTCSDL.Common.Req;

    public class EmployeesRep : GenericRep<NorthwindContext, Employees>
    {
        public object GetDoanhThuNhanVienTrongNgay(DateTime date)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "DoanhThuNhanVienTrongNgay";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", date);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            EmployeeID = row["EmployeeID"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            DoanhThu = row["DoanhThu"]
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        public object GetDoanhThuNhanVienTrongKhoangThoiGian(DateTime dateBegin, DateTime dateEnd)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "DoanhThuNhanVienTrongKhoangThoiGian";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dateBegin", dateBegin);
                cmd.Parameters.AddWithValue("@dateEnd", dateEnd);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            EmployeeID = row["EmployeeID"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            DoanhThu = row["DoanhThu"]
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        public object GetDoanhThuNhanVienTrongNgay_LinQ(DateTime date)
        {   
            /*
            var res = All.Join(this.Context.Orders, a => a.EmployeeId, b => b.EmployeeId, (a, b) => new
            {
                a.EmployeeId,
                a.FirstName,
                a.LastName,
                b.OrderId,
                b.OrderDate
            }).Join(this.Context.OrderDetails, a => a.OrderId, c => c.OrderId, (a, c) => new
            {
                a.EmployeeId,
                a.FirstName,
                a.LastName,
                a.OrderDate,
                DoanhThu = (Convert.ToDouble(c.Quantity) * Convert.ToDouble(c.UnitPrice) * (1 - Convert.ToDouble(c.Discount))).ToString()
            }).Where(emp => emp.OrderDate.Value.Day == date.Day && emp.OrderDate.Value.Month == date.Month &&
                            emp.OrderDate.Value.Year == date.Year)
            .GroupBy(emp => new { emp.EmployeeId, emp.FirstName, emp.LastName, emp.DoanhThu });
            */

            var db = Context;
            var res = from e in db.Employees join o in db.Orders on e.EmployeeId equals o.EmployeeId
                        join od in db.OrderDetails on o.OrderId equals od.OrderId
                      where o.OrderDate.Value.Day == date.Day && o.OrderDate.Value.Month == date.Month && o.OrderDate.Value.Year == date.Year
                      group od by new { e.EmployeeId, e.FirstName, e.LastName } into g
                      select new 
                      {
                          g.Key.EmployeeId, 
                          g.Key.FirstName, 
                          g.Key.LastName,
                          DoanhThu = Math.Round(g.Sum(od => Convert.ToDouble(od.Quantity) * Convert.ToDouble(od.UnitPrice) * (1 - od.Discount)), 2)
                      };

            return res;
        }

        public object GetDoanhThuNhanVienTrongKhoangThoiGian_LinQ(DateTime dateBegin, DateTime dateEnd)
        {
            var db = Context;
            var res = from e in db.Employees
                      join o in db.Orders on e.EmployeeId equals o.EmployeeId
                      join od in db.OrderDetails on o.OrderId equals od.OrderId
                      where o.OrderDate >= dateBegin && o.OrderDate <= dateEnd
                      group od by new { e.EmployeeId, e.FirstName, e.LastName } into g
                      select new
                      {
                          g.Key.EmployeeId,
                          g.Key.FirstName,
                          g.Key.LastName,
                          DoanhThu = Math.Round(g.Sum(od => Convert.ToDouble(od.Quantity) * Convert.ToDouble(od.UnitPrice) * (1 - od.Discount)), 2)
                      };

            return res;
        }
    }
}
