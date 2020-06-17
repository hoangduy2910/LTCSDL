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
    }
}
