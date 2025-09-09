using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace IT_Hardware.Areas.Admin.Data
{
    public class Bl_Serial_Check
    {



        public Boolean Find_Sl(string SL_Number)
        {
            Boolean Find_SL = false;

            try
            {
                
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.CommandText = "select dbo.Fun_Sl_Finder(@SL_Number)";

                    SqlParameter sqlP_SL_Number = new SqlParameter("@SL_Number", SL_Number);
                    cmd.Parameters.Add(sqlP_SL_Number);

                    con.Open();

                    Find_SL = Convert.ToBoolean( cmd.ExecuteScalar());

                    con.Close();

                }

                //Find_SL = Convert.ToBoolean(dt_Comuter.Rows[0][0]);

            }
            catch (Exception ex) { }
           

            return Find_SL;
        }



    }
}