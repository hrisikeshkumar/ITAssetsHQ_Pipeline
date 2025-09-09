using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using IT_Hardware.IServices;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Budget_Year : IBudgetYearService
    {
        public List<Mod_Budget_Year> Get_Data()
        {

            Mod_Budget_Year BL_data;
            List<Mod_Budget_Year> current_data = new List<Mod_Budget_Year>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Bud_year"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get");
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
                    BL_data = new Mod_Budget_Year();
                    BL_data.Bud_Id = Convert.ToString(dr["Bud_Id"]);
                    BL_data.Bud_Year = Convert.ToString(dr["Bud_Year"]);
                    BL_data.default_Bud =   Convert.ToInt32(dr["default_Bud"])== 1 ? true:false ;
                   
                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }



        public int Save_data( Mod_Budget_Year data, string type)
        {
            int status = 0;
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Bud_year";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Delete")
                {
                    SqlParameter Bud_Id = new SqlParameter("@Bud_Id", data.Bud_Id);
                    cmd.Parameters.Add(Bud_Id);
                }
                else
                { 

                    SqlParameter Bud_Year = new SqlParameter("@Bud_Year", data.Bud_Year);
                    cmd.Parameters.Add(Bud_Year);

                    SqlParameter default_Bud = new SqlParameter("@default_Bud", data.default_Bud ==true? 1:0 );
                    cmd.Parameters.Add(default_Bud);
                }


                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }


        List<SelectListItem> IBudgetYearService.budget_year_dropdown()
        {

           List<SelectListItem> current_data = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Bud_year"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get");
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

                SelectListItem item;
                item = new SelectListItem();
                item.Value = null ;
                item.Text = "Please Select Budget Year";
                current_data.Add(item);

                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    item = new SelectListItem();
                    item.Value = Convert.ToString(dr["Bud_Id"]);
                    item.Text = Convert.ToString(dr["Bud_Year"]);
                    
                    current_data.Add(item);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

       
    }
}