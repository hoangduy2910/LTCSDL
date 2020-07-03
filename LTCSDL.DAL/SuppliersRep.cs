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
    using LTCSDL.Common.Req;

    public class SuppliersRep : GenericRep<NorthwindContext, Suppliers>
    {
        public object ThemMoiSupplier(SuppliersReq sup)
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
                cmd.CommandText = "ThemSupplier";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyName", sup.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", sup.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", sup.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", sup.Address);
                cmd.Parameters.AddWithValue("@City", sup.City);
                cmd.Parameters.AddWithValue("@Region", sup.Region);
                cmd.Parameters.AddWithValue("@PostalCode", sup.PostalCode);
                cmd.Parameters.AddWithValue("@Country", sup.Country);
                cmd.Parameters.AddWithValue("@Phone", sup.Phone);
                cmd.Parameters.AddWithValue("@Fax", sup.Fax);
                cmd.Parameters.AddWithValue("@HomePage", sup.HomePage);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            SupplierID = row["SupplierID"],
                            CompanyName = row["CompanyName"],                              
                            ContactName = row["ContactName"],                    
                            ContactTitle = row["ContactTitle"],                   
                            Address = row["Address"],                                                      
                            City = row["City"],            
                            Region = row["Region"],          
                            PostalCode = row["PostalCode"], 
                            Country = row["Country"],         
                            Phone = row["Phone"],                    
                            Fax = row["Fax"],                      
                            HomePage = row["HomePage"],
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

        public object CapNhatSupplier(SuppliersReq sup)
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
                cmd.CommandText = "CapNhatSupplier";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierID", sup.SupplierId);
                cmd.Parameters.AddWithValue("@CompanyName", sup.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", sup.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", sup.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", sup.Address);
                cmd.Parameters.AddWithValue("@City", sup.City);
                cmd.Parameters.AddWithValue("@Region", sup.Region);
                cmd.Parameters.AddWithValue("@PostalCode", sup.PostalCode);
                cmd.Parameters.AddWithValue("@Country", sup.Country);
                cmd.Parameters.AddWithValue("@Phone", sup.Phone);
                cmd.Parameters.AddWithValue("@Fax", sup.Fax);
                cmd.Parameters.AddWithValue("@HomePage", sup.HomePage);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            SupplierID = row["SupplierID"],
                            CompanyName = row["CompanyName"],
                            ContactName = row["ContactName"],
                            ContactTitle = row["ContactTitle"],
                            Address = row["Address"],
                            City = row["City"],
                            Region = row["Region"],
                            PostalCode = row["PostalCode"],
                            Country = row["Country"],
                            Phone = row["Phone"],
                            Fax = row["Fax"],
                            HomePage = row["HomePage"],
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
