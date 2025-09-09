using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Budget_Head
    {

        public List<Mod_Budget> Get_BudgetData()
        {
            
            Mod_Budget BL_data;
            List<Mod_Budget> current_data = new List<Mod_Budget>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Head"))
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
                    BL_data = new Mod_Budget();

                    BL_data.Budget_Head_Id = Convert.ToString(dr["Budget_Head_Id"]);

                    BL_data.Budget_Name = Convert.ToString(dr["Budget_Name"]);

                    BL_data.Total_Budget_Amount = Convert.ToString(dr["Total_Budget_Amount"]);

                    BL_data.Utilized_for_Budget = Convert.ToString(dr["Budget"]);

                    BL_data.Utilized_for_Payment = Convert.ToString(dr["Payment"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_Budget_data(Mod_Budget Data, string type, string Budget_Head_ID)
        {
            int status = -1;
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Budget_Head";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Budget_ID = new SqlParameter("@Budget_Head_Id", Budget_Head_ID);
                    cmd.Parameters.Add(Budget_ID);
                }

                SqlParameter Budget_Year = new SqlParameter("@Budget_Year", Data.Budget_Year);
                cmd.Parameters.Add(Budget_Year);

                SqlParameter Budget_HeadType = new SqlParameter("@Budget_HeadType", Data.Budget_HeadType);
                cmd.Parameters.Add(Budget_HeadType);

                SqlParameter Budget_Name = new SqlParameter("@Budget_Name", Data.Budget_Name);
                cmd.Parameters.Add(Budget_Name);

                SqlParameter Total_Budget_Amount = new SqlParameter("@Total_Budget_Amount", Data.Total_Budget_Amount);
                cmd.Parameters.Add(Total_Budget_Amount);

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.Create_User);
                cmd.Parameters.Add(User_Id);

                con.Open();

                status = cmd.ExecuteNonQuery();


            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Budget Get_Data_By_ID(Mod_Budget Data, string Budget_Head_Id)
        {


            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Head"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter Budget_Id = new SqlParameter("@Budget_Head_Id", Budget_Head_Id);
                    cmd.Parameters.Add(Budget_Id);

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
                    Data.Budget_Head_Id = Convert.ToString(dt_Comuter.Rows[0]["Budget_Head_Id"]);
                    Data.Budget_Year = Convert.ToString(dt_Comuter.Rows[0]["Budget_Year"]);
                    Data.Budget_Name = Convert.ToString(dt_Comuter.Rows[0]["Budget_Name"]);
                    Data.Budget_HeadType = Convert.ToInt32(dt_Comuter.Rows[0]["Budget_HeadType"]);
                    Data.Total_Budget_Amount = Convert.ToString(dt_Comuter.Rows[0]["Total_Budget_Amount"]);
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);
                }

            }
            catch (Exception ex) { }

            return Data;
        }


    }
}