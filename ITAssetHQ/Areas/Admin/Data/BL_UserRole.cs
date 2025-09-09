using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_UserRole
    {
        public List<Mod_UserRole> Get_UserRoleData()
        {

            Mod_UserRole BL_Mod_Role = new Mod_UserRole();
            List<Mod_UserRole> current_data = new List<Mod_UserRole>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_UserRole"))
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
                int Prev_ID=9999;



                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    

                    if (Prev_ID != Convert.ToInt32(dr["UserID"]))
                    {
                        BL_Mod_Role = new Mod_UserRole();
                        BL_Mod_Role.User_ID = Convert.ToString(dr["UserID"]);
                        BL_Mod_Role.User_fullname = Convert.ToString(dr["fullname"]);
                        BL_Mod_Role.SU_Role = false;
                        BL_Mod_Role.Admin_Role = false;
                        BL_Mod_Role.Manager_Role = false;
                        BL_Mod_Role.InventoryManager_Role = false;
                        BL_Mod_Role.FmsEngineer_Role = false;
                        BL_Mod_Role.ServerEngineer_Role = false;

                    }


                    
                        if (Convert.ToString(dr["RoleName"]).Trim() == "SU")
                        {
                            BL_Mod_Role.SU_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "Admin")
                        {
                            BL_Mod_Role.Admin_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "Manager")
                        {
                            BL_Mod_Role.Manager_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "InventoryManager")
                        {
                            BL_Mod_Role.InventoryManager_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "FmsEngineer")
                        {
                            BL_Mod_Role.FmsEngineer_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "ServerEngineer")
                        {
                            BL_Mod_Role.ServerEngineer_Role = true;
                        }
                    

                    if (Prev_ID != Convert.ToInt32(dr["UserID"]))
                    {
                        current_data.Add(BL_Mod_Role);
                    }
                    Prev_ID = Convert.ToInt32(dr["UserID"]);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_UserRole_data(Mod_UserRole[] Data, string type)
        {
            int status = 1;
            
            SqlConnection con = new DBConnection().con;
            try
            {
               DataTable rolemapping ;

                rolemapping = Permission(Data);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UserRole";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);


                SqlParameter User_Role = new SqlParameter("@UserRole", rolemapping);
                cmd.Parameters.Add(User_Role);



                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }


        private DataTable Permission(Mod_UserRole[] data)
        {
            

            DataTable rolemapping = new DataTable();
          
            rolemapping.Columns.Add("UserID",typeof(int));
            rolemapping.Columns.Add("UserRole", typeof(int));

            for (int i = 0; i < data.Count(); i++)
            {
                

                if (data[i].SU_Role)
                {
                    DataRow dr = rolemapping.NewRow();
                    dr["UserID"] = Convert.ToString(data[i].User_ID);
                    dr["UserRole"] = 1;
                    rolemapping.Rows.Add(dr);              
                    rolemapping.AcceptChanges();


                }
                if (data[i].Admin_Role)
                {
                    DataRow dr = rolemapping.NewRow();
                    dr["UserID"] = Convert.ToString(data[i].User_ID);
                    dr["UserRole"] = 4;
                    rolemapping.Rows.Add(dr);
                    rolemapping.AcceptChanges();



                }
                if (data[i].Manager_Role)
                {
                    DataRow dr = rolemapping.NewRow();
                    dr["UserID"] = Convert.ToString(data[i].User_ID);
                    dr["UserRole"] =5;
                    rolemapping.Rows.Add(dr);
                    rolemapping.AcceptChanges();

                }
                if (data[i].InventoryManager_Role)
                {
                    DataRow dr = rolemapping.NewRow();
                    dr["UserID"] = Convert.ToString(data[i].User_ID);
                    dr["UserRole"] = 6;
                    rolemapping.Rows.Add(dr);
                    rolemapping.AcceptChanges();


                }
                if (data[i].ServerEngineer_Role)
                {
                    DataRow dr = rolemapping.NewRow();
                    dr["UserID"] = Convert.ToString(data[i].User_ID);
                    dr["UserRole"] = 8;
                    rolemapping.Rows.Add(dr);
                    rolemapping.AcceptChanges();


                }
                if (data[i].FmsEngineer_Role)
                {
                    DataRow dr = rolemapping.NewRow();
                    dr["UserID"] = Convert.ToString(data[i].User_ID);
                    dr["UserRole"] = 7;
                    rolemapping.Rows.Add(dr);
                    rolemapping.AcceptChanges();


                }

            }

            return rolemapping;
        }


        public class MapRole
        {
            public int UserID { get; set; }
            public int UserRole { get; set; }
        }

    }
}