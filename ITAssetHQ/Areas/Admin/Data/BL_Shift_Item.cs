using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Shift_Item
    {
        public List<Mod_Shift_Item> Get_Shift_ItemData()
        {

            Mod_Shift_Item BL_data;
            List<Mod_Shift_Item> current_data = new List<Mod_Shift_Item>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Computer"))
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
                    BL_data = new Mod_Shift_Item();

                    BL_data.Item_Shift_Id = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Item_Name = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Shifted_Company = Convert.ToString(dr["Item_Id"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_Shift_Item_data(Mod_Shift_Item Data, string type, string Shift_Id)
        {
            int status = 1;
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Computer";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter ItemShift_Id = new SqlParameter("@Item_Id", Shift_Id);
                    cmd.Parameters.Add(ItemShift_Id);
                }

                SqlParameter Item_Id = new SqlParameter("@Item_MakeId", Data.Item_Id);
                cmd.Parameters.Add(Item_Id);

                SqlParameter Present_Company = new SqlParameter("@Item_serial_No", Data.Present_Company);
                cmd.Parameters.Add(Present_Company);

                SqlParameter Shifted_Company = new SqlParameter("@Proc_Date", Data.Shifted_Company);
                cmd.Parameters.Add(Shifted_Company);

                SqlParameter Shift_date = new SqlParameter("@Warnt_end_DT", Data.Shift_date);
                cmd.Parameters.Add(Shift_date);

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

                SqlParameter Asset_Type = new SqlParameter("@Asset_Type", "Shift_Item");
                cmd.Parameters.Add(Asset_Type);

                con.Open();

                cmd.ExecuteNonQuery();

                status = 0;

            }
            catch (Exception ex) { status = 1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Shift_Item Get_Data_By_ID(string Shift_Unique_Id)
        {
            Mod_Shift_Item Data = new Mod_Shift_Item();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Computer"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Shift_Id = new SqlParameter("@Item_ID", Shift_Unique_Id);
                    cmd.Parameters.Add(sqlP_Shift_Id);

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
                    Data.Item_Shift_Id = Convert.ToString(dt_Comuter.Rows[0]["Item_Id"]);
                    Data.Item_SerialNo = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Item_Name = Convert.ToString(dt_Comuter.Rows[0]["Item_SlNo"]);
                    Data.Item_Shift_Location = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Present_Company = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Shifted_Company = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Shift_date = Convert.ToDateTime(dt_Comuter.Rows[0]["Proc_Date"]).Date;
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);

                }

            }
            catch (Exception ex) { }

            return Data;
        }
    }
}