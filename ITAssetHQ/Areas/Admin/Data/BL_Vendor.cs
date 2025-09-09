using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Vendor
    {

            public List<Mod_Vendor> Get_VendorData()
            {

                Mod_Vendor BL_data;
                List<Mod_Vendor> current_data = new List<Mod_Vendor>();

                try
                {
                    DataTable dt_Comuter;
                    
                    SqlConnection con = new DBConnection().con;


                    using (SqlCommand cmd = new SqlCommand("sp_Vendor"))
                    {
                        SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.Add(sqlP_type);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                dt_Comuter = dt;
                            }
                        }
                    }


                    foreach (DataRow dr in dt_Comuter.Rows)
                    {
                        BL_data = new Mod_Vendor();

                        BL_data.Vendor_id = Convert.ToString(dr["Vendor_ID"]);

                        BL_data.Vendor_name = Convert.ToString(dr["Vendor_name"]);

                        BL_data.PO_Issued = Convert.ToString(dr["Total_PO"]);

                        BL_data.Invoice_Processed = Convert.ToString(dr["Invoice_Processed"]);

                        current_data.Add(BL_data);
                    }

                }
                catch (Exception ex) { }

                return current_data;
            }

            public int Save_Vendor_data(Mod_Vendor Data, string type, string Vendor_ID , out string Vendor_Id_Update)
            {
                
                int status = -1;
                Vendor_Id_Update = string.Empty;
                
                SqlConnection con = new DBConnection().con;
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Vendor";

                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", type);
                    cmd.Parameters.Add(sqlP_type);

                    if (type == "Update" || type == "Delete")
                    {
                        SqlParameter Vendor_Id = new SqlParameter("@Vendor_ID", Vendor_ID);
                        cmd.Parameters.Add(Vendor_Id);
                    }

                    SqlParameter Asset_Make_Id = new SqlParameter("@Vendor_name", Data.Vendor_name);
                    cmd.Parameters.Add(Asset_Make_Id);

                    SqlParameter Asset_SL_No = new SqlParameter("@Vendor_Addr", Data.Vendor_Addr);
                    cmd.Parameters.Add(Asset_SL_No);                

                    SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                    cmd.Parameters.Add(Remarks);


                    SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.Create_usr_id);
                    cmd.Parameters.Add(User_Id);


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {

                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                Vendor_Id_Update = Convert.ToString(dt.Rows[0]["Vendor_Id"]);
                                status = Convert.ToInt32(dt.Rows[0]["Row_Effect"]);
                            }

                        }
                    }



                }
                catch (Exception ex) { status = -1; }
                    finally { con.Close(); }

                    return status;
                }

            public Mod_Vendor Get_Data_By_ID(string Vendor_Id)
            {
                Mod_Vendor Data = new Mod_Vendor();

                try
                {
                    DataTable dt_Comuter;
                    
                    SqlConnection con = new DBConnection().con;


                    using (SqlCommand cmd = new SqlCommand("sp_Vendor"))
                    {
                        SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.Add(sqlP_type);

                        SqlParameter VendorId = new SqlParameter("@Vendor_ID", Vendor_Id);
                        cmd.Parameters.Add(VendorId);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                dt_Comuter = dt;
                            }
                        }
                    }

                    if (dt_Comuter.Rows.Count > 0)
                    {
                        Data.Vendor_id = Convert.ToString(dt_Comuter.Rows[0]["Vendor_ID"]);
                        Data.Vendor_name = Convert.ToString(dt_Comuter.Rows[0]["Vendor_name"]);
                        Data.Vendor_Addr = Convert.ToString(dt_Comuter.Rows[0]["Vendor_Address"]);
                        Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["remarks"]);

                    }

                }
                catch (Exception ex) { }

                return Data;
            }




        

    }
}