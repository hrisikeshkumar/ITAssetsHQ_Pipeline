using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_AssetMaster
    {

        public List<Mod_AssetMaster> Get_Data()
        {

            Mod_AssetMaster BL_data;
            List<Mod_AssetMaster> current_data = new List<Mod_AssetMaster>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_AsetMaster"))
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
                    BL_data = new Mod_AssetMaster();
                    BL_data.Asset_ID = Convert.ToString(dr["ID"]);
                    BL_data.Asset_make = Convert.ToString(dr["Make"]);
                    BL_data.Asset_Model = Convert.ToString(dr["Model"]);
                    BL_data.Asset_Type = Convert.ToString(dr["Aset_Type"]);

                    BL_data.Asset_Status = Convert.ToInt32( (dr["Aset_Status"] == DBNull.Value) ? 0 : dr["Aset_Status"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_data(Mod_AssetMaster Data, string type, string Asset_ID)
        {
            int status = 0;
            
            SqlConnection con = new DBConnection().con;
            try
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AsetMaster";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Asset_Id= new SqlParameter("@Item_ID", Asset_ID);
                    cmd.Parameters.Add(Asset_Id);
                }

                SqlParameter Asset_make = new SqlParameter("@Item_Make", Data.Asset_make);
                cmd.Parameters.Add(Asset_make);

                SqlParameter Asset_Model = new SqlParameter("@Item_Model", Data.Asset_Model);
                cmd.Parameters.Add(Asset_Model);

                SqlParameter Asset_Type = new SqlParameter("@Item_Type", Data.Asset_Type);
                cmd.Parameters.Add(Asset_Type);

                SqlParameter User_Id = new SqlParameter("@Create_User", Data.Create_User);
                cmd.Parameters.Add(User_Id);

                con.Open();

                status= cmd.ExecuteNonQuery();

                

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;   
        }

        public Mod_AssetMaster Get_Data_By_ID(string Asset_Id)
        {
            Mod_AssetMaster Data = new Mod_AssetMaster();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_AsetMaster"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Asset_ID = new SqlParameter("@Item_ID", Asset_Id);
                    cmd.Parameters.Add(sqlP_Asset_ID);

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
                    Data.Asset_ID = Convert.ToString(dt_Comuter.Rows[0]["ID"]);
                    Data.Asset_make = Convert.ToString(dt_Comuter.Rows[0]["Make"]);
                    Data.Asset_Model = Convert.ToString(dt_Comuter.Rows[0]["model"]);
                    Data.Asset_Type = Convert.ToString(dt_Comuter.Rows[0]["Aset_Type"]);
                    ;
                }

            }
            catch (Exception ex) { }

                return Data;
        }

    }
}