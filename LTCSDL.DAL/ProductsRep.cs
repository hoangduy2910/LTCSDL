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
    }
}
