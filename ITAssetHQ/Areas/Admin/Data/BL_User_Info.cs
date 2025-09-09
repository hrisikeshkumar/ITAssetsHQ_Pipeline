using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_User_Info
    {

        public List<Mod_User_Info> Get_User_InfoData()
        {

            Mod_User_Info BL_data;
            List<Mod_User_Info> current_data = new List<Mod_User_Info>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_User_Info"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
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
                    BL_data = new Mod_User_Info();

                    BL_data.User_ID = Convert.ToString(dr["UserID"]);

                    BL_data.Emp_Code = Convert.ToString(dr["Emp_Code"]);

                    BL_data.User_First_Name = Convert.ToString(dr["FirstName"]);

                    BL_data.User_Last_Name = Convert.ToString(dr["LastName"]);

                    BL_data.User_Email = Convert.ToString(dr["User_Email"]);

                    //BL_data.User_Password = Convert.ToString(dr["User_First_Name"]);

                    if (Convert.ToInt32(dr["Active"]) ==1 )
                        BL_data.Active = true;
                    else
                        BL_data.Active = false;

                    BL_data.Remarks = Convert.ToString(dr["Remarks"]);



                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_User_Info_data(Mod_User_Info Data, string type, string User_Info_ID)
        {
            int status = 1;
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_User_Info";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Sql_User_Info_ID = new SqlParameter("@User_Info_Id", User_Info_ID);
                    cmd.Parameters.Add(Sql_User_Info_ID);
                }

                SqlParameter Emp_Code = new SqlParameter("@Emp_Code", Data.Emp_Code);
                cmd.Parameters.Add(Emp_Code);

                SqlParameter User_First_Name = new SqlParameter("@User_First_Name", Data.User_First_Name);
                cmd.Parameters.Add(User_First_Name);

                SqlParameter User_Last_Name = new SqlParameter("@User_Last_Name", Data.User_Last_Name);
                cmd.Parameters.Add(User_Last_Name);

                SqlParameter User_Email = new SqlParameter("@User_Email", Data.User_Email);
                cmd.Parameters.Add(User_Email);

                SqlParameter User_Password = new SqlParameter("@User_Password", Data.User_Password);
                cmd.Parameters.Add(User_Password);

                SqlParameter Active = new SqlParameter("@Active", (Data.Active == true ? 1 : 0) );
                cmd.Parameters.Add(Active); 
                
                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

              
                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_User_Info Get_Data_By_ID(Mod_User_Info Data, string User_Info_Id)
        {


            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_User_Info"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_User_ID = new SqlParameter("@User_ID", User_Info_Id);
                    cmd.Parameters.Add(sqlP_User_ID);

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
                    
                    Data.User_ID = Convert.ToString(dt_Comuter.Rows[0]["UserID"]);
                    Data.Emp_Code = Convert.ToString(dt_Comuter.Rows[0]["Emp_Code"]);
                    Data.User_First_Name = Convert.ToString(dt_Comuter.Rows[0]["FirstName"]);
                    Data.User_Last_Name = Convert.ToString(dt_Comuter.Rows[0]["LastName"]);
                    Data.User_Email = Convert.ToString(dt_Comuter.Rows[0]["User_Email"]);
                    //BL_data.User_Password = Convert.ToString(dr["User_First_Name"]);
                    if (Convert.ToInt32(dt_Comuter.Rows[0]["Active"]) == 1)
                        Data.Active = true;
                    else
                        Data.Active = false;
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);

                }

            }
            catch (Exception ex) { }

            return Data;
        }

    }
}