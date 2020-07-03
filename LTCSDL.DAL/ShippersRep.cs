using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LTCSDL.DAL
{
    using Models;
    using LTCSDL.Common.DAL;
    using LTCSDL.Common.BLL;
    using Microsoft.EntityFrameworkCore.Internal;
    using LTCSDL.Common.Req;

    public class ShippersRep : GenericRep<NorthwindContext, Shippers>
    {
        public Shippers ThemMoiShipper(Shippers ship)
        {
            Shippers x = new Shippers();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                string sql = "insert into [dbo].[Shippers] ([CompanyName], [Phone]) values ('" + ship.CompanyName + "', '" + ship.Phone + "')";
                sql = sql + "select * from [dbo].[Shippers] where ShipperID = @@identity";
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        x = new Shippers
                        {
                            ShipperId = Int32.Parse(row["ShipperID"].ToString()),
                            CompanyName = row["CompanyName"].ToString(),
                            Phone = row["[Phone]"].ToString()
                        };
                    }
                }
            }
            catch (Exception e)
            {
                x = null;
            }
            return x;
        }

        public object CapNhatShipper(ShippersReq ship)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                string sql = "update [dbo].[Shippers] set [CompanyName] = '" + ship.CompanyName + "', [Phone] = '" + ship.Phone + "'";
                sql = sql + "where ShipperID = '" + ship.ShipperId + "'";
                sql = sql + "select * from [dbo].[Shippers] where ShipperID = '" + ship.ShipperId + "'";
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new 
                        {
                            ShipperId = row["ShipperID"],
                            CompanyName = row["CompanyName"],
                            Phone = row["Phone"]
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

        public object DoanhThuShipperTrongKhoangThoiGian(DateTime dateBegin, DateTime dateEnd)
        {
            var res = All.Join(this.Context.Orders, a => a.ShipperId, b => b.ShipVia, (a, b) => new
            {
                a.ShipperId,
                a.CompanyName,
                a.Phone,
                b.OrderId,
                b.ShippedDate
            }).Join(this.Context.OrderDetails, a => a.OrderId, c => c.OrderId, (a, c) => new
            {
                a.ShipperId,
                a.CompanyName,
                a.Phone,
                a.ShippedDate,
                DoanhThu = c.Quantity * Convert.ToDouble(c.UnitPrice) * (1 - Convert.ToDouble(c.Discount))
            }).Where(s => s.ShippedDate >= dateBegin && s.ShippedDate <= dateEnd)
            .GroupBy(s => new { s.ShipperId, s.CompanyName, s.Phone })
            .Select(gr => new { 
                gr.Key.ShipperId,
                gr.Key.CompanyName,
                gr.Key.Phone,
                DoanhThu = Math.Round(gr.Sum(s => s.DoanhThu))
            });

            return res;
        }
    }
}
