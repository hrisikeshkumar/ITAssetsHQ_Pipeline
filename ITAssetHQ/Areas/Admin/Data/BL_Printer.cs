using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Printer
    {

        public List<Mod_Printer> Get_PrinterData()
        {

            Mod_Printer BL_data;
            List<Mod_Printer> current_data = new List<Mod_Printer>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Computer"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Asset_Type = new SqlParameter("@Asset_Type", "Printer");
                    cmd.Parameters.Add(sqlP_Asset_Type);

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
                    BL_data = new Mod_Printer();

                    BL_data.Item_Type = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Item_serial_No = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Item_id = Convert.ToString(dr["Item_Id"]);

                    BL_data.Make_Name = Convert.ToString(dr["Make"]);

                    BL_data.Model_Name = Convert.ToString(dr["model"]);

                    if (dr["Proc_Date"] != DBNull.Value)
                        BL_data.Proc_date = Convert.ToDateTime(dr["Proc_Date"]);

                    if (dr["Asset_Price"] != DBNull.Value)
                        BL_data.price = Convert.ToInt32(dr["Asset_Price"]);

                    BL_data.PO_No = Convert.ToString(dr["PO_No"]);

                    BL_data.PO_Id = Convert.ToString(dr["PO_Id"]);

                    if (dr["PO_Date"] != DBNull.Value)
                        BL_data.PO_Date = Convert.ToDateTime(dr["PO_Date"]);

                    if (dr["Warnt_end_DT"] != DBNull.Value)
                        BL_data.Warnt_end_dt = Convert.ToDateTime(dr["Warnt_end_DT"]);


                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_Printer_data(Mod_Printer Data, string type, string Asset_ID)
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
                    SqlParameter Asset_Id = new SqlParameter("@Item_Id", Asset_ID);
                    cmd.Parameters.Add(Asset_Id);
                }

                SqlParameter Asset_Make_Id = new SqlParameter("@Item_Model_id", Data.Item_Model_id);
                cmd.Parameters.Add(Asset_Make_Id);

                SqlParameter sql_PO_Id = new SqlParameter("@PO_Id", Data.PO_Id);
                cmd.Parameters.Add(sql_PO_Id);

                SqlParameter Asset_SL_No = new SqlParameter("@Item_serial_No", Data.Item_serial_No);
                cmd.Parameters.Add(Asset_SL_No);

                SqlParameter Proc_Date = new SqlParameter("@Proc_Date", Data.Proc_date);
                cmd.Parameters.Add(Proc_Date);

                SqlParameter Warnt_end_dt = new SqlParameter("@Warnt_end_DT", Data.Warnt_end_dt);
                cmd.Parameters.Add(Warnt_end_dt);

                SqlParameter Asset_Price = new SqlParameter("@Asset_Price", Data.price);
                cmd.Parameters.Add(Asset_Price);

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

                SqlParameter Asset_Type = new SqlParameter("@Asset_Type", "Printer");
                cmd.Parameters.Add(Asset_Type);


                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.Create_usr_id);
                cmd.Parameters.Add(User_Id);

                con.Open();

                status = cmd.ExecuteNonQuery();

                

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Printer Get_Data_By_ID(Mod_Printer Data, string Asset_Id)
        {
             

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
                    Data.Item_id = Convert.ToString(dt_Comuter.Rows[0]["Item_Id"]);
                    Data.Item_Make_id = Convert.ToString(dt_Comuter.Rows[0]["Make"]);
                    Data.Item_Model_id = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.PO_Id = Convert.ToString(dt_Comuter.Rows[0]["PO_Id"]);
                    Data.PO_No = Convert.ToString(dt_Comuter.Rows[0]["PO_No"]);
                    Data.Item_serial_No = Convert.ToString(dt_Comuter.Rows[0]["Item_SlNo"]);
                    Data.Proc_date = Convert.ToDateTime(dt_Comuter.Rows[0]["Proc_Date"]).Date;
                    Data.Warnt_end_dt = Convert.ToDateTime(dt_Comuter.Rows[0]["Warnt_end_DT"]).Date;
                    Data.price = Convert.ToInt32(dt_Comuter.Rows[0]["Asset_Price"]);
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);

                }

            }
            catch (Exception ex) { }

            return Data;
        }


    }
}