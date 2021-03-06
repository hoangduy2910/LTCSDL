﻿using System;
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

    public class ProductsRep : GenericRep<NorthwindContext, Products>
    {
        public object GetDSMatHangBanChayNhat(int size, int page, int month, int year)
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
                cmd.CommandText = "DSMatHangBanChayNhat";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            STT = row["STT"],
                            ProductName = row["ProductName"],
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

        public object GetDSMatHangBanChayNhat_LinQ(int size, int page, int month, int year)
        {
            var ds = All.Join(this.Context.OrderDetails, a => a.ProductId, c => c.ProductId, (a, c) => new
            {
                a.ProductId,
                c.OrderId,
                c.Quantity,
                c.UnitPrice,
                c.Discount
            }).Join(this.Context.Orders, a => a.OrderId, b => b.OrderId, (a, b) => new
            {
                a.ProductId,
                a.OrderId,
                a.Quantity,
                a.UnitPrice,
                a.Discount,
                b.OrderDate,
                b.ShipCountry,
                DoanhThu = a.Quantity * Convert.ToDouble(a.UnitPrice) * (1 - Convert.ToDouble(a.Discount))
            }).Where(emp => emp.OrderDate.Value.Month == month && emp.OrderDate.Value.Year == year)
            .GroupBy(emp => new { emp.ShipCountry })
            .Select(group => new {
                group.Key.ShipCountry,
                DoanhThu = Math.Round(group.Sum(emp => emp.DoanhThu), 2)
            });

            var offset = (page - 1) * size;
            var totalRecord = ds.Count();
            var totalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)((totalRecord / size) + 1);
            var data = ds.Skip(offset).Take(size).ToList();

            return new
            {
                Data = data,
                totalRecord = totalRecord,
                totalPage = totalPage,
                page = page,
                size = size
            };
        }

        public object GetDanhSachProductKhongCoDonHangTrongNgay(int size, int page, DateTime date)
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
                cmd.CommandText = "DanhSachProductKhongCoDonHangTrongNgay";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@date", date);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            STT = row["STT"],               
                            ProductID = row["ProductID"],
                            ProductName = row["ProductName"],
                            SupplierID = row["SupplierID"],
                            CategoryID = row["CategoryID"],
                            QuantityPerUnit = row["QuantityPerUnit"],
                            UnitPrice = row["UnitPrice"],
                            UnitsInStock = row["UnitsInStock"],
                            UnitsOnOrder = row["UnitsOnOrder"],
                            ReorderLevel = row["ReorderLevel"],
                            Discontinued = row["Discontinued"]
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

        public object ThemMoiProduct(InsertProductReq pro)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                string sql = "insert into [dbo].[Products] ([ProductName] ,[SupplierID] ,[CategoryID] ,[QuantityPerUnit] ,[UnitPrice] ,[UnitsInStock] ,[UnitsOnOrder] ,[ReorderLevel] ,[Discontinued])";
                sql = sql + "values ('" + pro.ProductName + "', '" + pro.SupplierId + "', '" + pro.CategoryId + "', '" + pro.QuantityPerUnit + "', '" + pro.UnitPrice + "', '" + pro.UnitsInStock + "', '" + pro.UnitsOnOrder + "', '" + pro.ReorderLevel + "', '" + pro.Discontinued + "')";
                sql = sql + "select * from [dbo].[Products] where ProductID = @@identity";
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
                            ProductID = row["ProductID"],
                            ProductName = row["ProductName"],
                            SupplierID = row["SupplierID"],
                            CategoryID = row["CategoryID"],
                            QuantityPerUnit = row["QuantityPerUnit"],
                            UnitPrice = row["UnitPrice"],
                            UnitsInStock = row["UnitsInStock"],
                            UnitsOnOrder = row["UnitsOnOrder"],
                            ReorderLevel = row["ReorderLevel"],
                            Discontinued = row["Discontinued"]
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
    }
}
