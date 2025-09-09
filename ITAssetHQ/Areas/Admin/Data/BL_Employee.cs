using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Employee
    {

        public List<Mod_Employee> Get_EmployeeData()
        {

            Mod_Employee BL_data;
            List<Mod_Employee> current_data = new List<Mod_Employee>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
                    cmd.Parameters.Add(sqlP_type);

                    //SqlParameter sqlP_User = new SqlParameter("@Type", "Get_List");
                    //cmd.Parameters.Add(sqlP_User);

  
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
                    BL_data = new Mod_Employee();

                    BL_data.Emp_Unique_Id = Convert.ToString(dr["Unique_Id"]);

                    BL_data.Emp_Code = Convert.ToString(dr["Emp_Code"]);

                    BL_data.Emp_Name = Convert.ToString(dr["Emp_Name"]);

                    BL_data.Emp_Designation_Name = Convert.ToString(dr["Emp_Designation"]);

                    BL_data.Emp_Type = Convert.ToString(dr["Emp_Type"]);

                    BL_data.Emp_Dept_Name = Convert.ToString(dr["Emp_Dept"]);

                    BL_data.Location = Convert.ToString(dr["Emp_Location"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_Employee_data(Mod_Employee Data, string type)
        {
            int status = 0;
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Employee";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Emp_Unique_Id = new SqlParameter("@Emp_Unique_Id", Data.Emp_Unique_Id);
                    cmd.Parameters.Add(Emp_Unique_Id);
                }

                SqlParameter Emp_Code = new SqlParameter("@Emp_Code", Data.Emp_Code);
                cmd.Parameters.Add(Emp_Code);

                SqlParameter Emp_Name = new SqlParameter("@Emp_Name", Data.Emp_Name);
                cmd.Parameters.Add(Emp_Name);

                SqlParameter Emp_Designation = new SqlParameter("@Emp_Designation", Data.Emp_Designation);
                cmd.Parameters.Add(Emp_Designation);

                SqlParameter Emp_Dept = new SqlParameter("@Emp_Dept", Data.Emp_Dept);
                cmd.Parameters.Add(Emp_Dept);

                SqlParameter Emp_Type = new SqlParameter("@Emp_Type", Data.Emp_Type);
                cmd.Parameters.Add(Emp_Type);

                SqlParameter Location = new SqlParameter("@Emp_Location", Data.Location);
                cmd.Parameters.Add(Location);

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);


                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.Create_usr_id);
                cmd.Parameters.Add(User_Id);

                con.Open();

                status= cmd.ExecuteNonQuery();

               

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Employee Get_Data_By_ID(string Unique_Id , Mod_Employee Data)
        {
            
            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter Emp_Unique_Id = new SqlParameter("@Emp_Unique_Id", Unique_Id);
                    cmd.Parameters.Add(Emp_Unique_Id);

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
                    Data.Emp_Unique_Id = Convert.ToString(dt_Comuter.Rows[0]["Unique_Id"]);
                    Data.Emp_Code = Convert.ToString(dt_Comuter.Rows[0]["Emp_Code"]);
                    Data.Emp_Name = Convert.ToString(dt_Comuter.Rows[0]["Emp_Name"]);
                    Data.Emp_Designation = Convert.ToString(dt_Comuter.Rows[0]["Emp_Designation"]);
                    Data.Emp_Type = Convert.ToString(dt_Comuter.Rows[0]["Emp_Type"]);
                    Data.Emp_Dept = Convert.ToString(dt_Comuter.Rows[0]["Emp_Dept"]);
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);
                    Data.Location = Convert.ToString(dt_Comuter.Rows[0]["Emp_Location"]);
                    Data.Emp_Dept_Name = Convert.ToString(dt_Comuter.Rows[0]["Dept_name"]);
                    Data.Emp_Dept_Name = Convert.ToString(dt_Comuter.Rows[0]["Designation_name"]);

                }

            }
            catch (Exception ex) { }

            return Data;
        }


        public List<SelectListItem> Bind_EmpType()
        {
            List<SelectListItem> Emp_Type = new List<SelectListItem>();

            SelectListItem List2 = new SelectListItem();
            List2.Value = "1";
            List2.Text = "Parmanent";
            Emp_Type.Add(List2);

            SelectListItem List3 = new SelectListItem();
            List3.Value = "2";
            List3.Text = "Casual/Consultant";
            Emp_Type.Add(List3);



            return Emp_Type;
        }


        //----------------------------------------------------  Department -------------------------------------------------------
        public List<Mod_Department> Get_Department()
        {

            Mod_Department mod_data;
            List<Mod_Department> data = new List<Mod_Department>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List_Dept");
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
                    mod_data = new Mod_Department();

                    mod_data.Department_Id = Convert.ToString(dr["ID"]);

                    mod_data.Department_Name = Convert.ToString(dr["Name"]);

                    mod_data.Department_MicrosoftID = Convert.ToString(dr["MicrosoftID"]);

                    data.Add(mod_data);
                }

            }
            catch (Exception ex) { }

            return data;
        }

        public int Save_Department(Mod_Department Data, string type)
        {
            int status = 0;

            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Employee";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update_Dept" || type == "Delete_Dept")
                {
                    SqlParameter Department_Id = new SqlParameter("@Emp_Unique_Id", Data.Department_Id);
                    cmd.Parameters.Add(Department_Id);
                }

                SqlParameter Department_Name = new SqlParameter("@Emp_Name", Data.Department_Name);
                cmd.Parameters.Add(Department_Name);

                SqlParameter Department_MicrosoftID = new SqlParameter("@Emp_Designation", Data.Department_MicrosoftID);
                cmd.Parameters.Add(Department_MicrosoftID);


                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.UserId);
                cmd.Parameters.Add(User_Id);

                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Department Get_Department_By_ID(string Unique_Id, Mod_Department Data)
        {

            try
            {
                DataTable dt= new DataTable();

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Department_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter Emp_Unique_Id = new SqlParameter("@Emp_Unique_Id", Unique_Id);
                    cmd.Parameters.Add(Emp_Unique_Id);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    Data.Department_Id = Convert.ToString(dt.Rows[0]["Unique_Id"]);
                    Data.Department_Name = Convert.ToString(dt.Rows[0]["Emp_Code"]);
                    Data.Department_MicrosoftID = Convert.ToString(dt.Rows[0]["Emp_Name"]);
                }

            }
            catch (Exception ex) { }

            return Data;
        }


        //----------------------------------------------------  Designation -------------------------------------------------------

        public List<Mod_Designation> Get_Designation()
        {

            Mod_Designation mod_data;
            List<Mod_Designation> data = new List<Mod_Designation>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List_Designation");
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
                    mod_data = new Mod_Designation();

                    mod_data.Designation_Id = Convert.ToString(dr["ID"]);
                    mod_data.Designation_Type = Convert.ToString(dr["Type"]);
                    mod_data.Designation_Name = Convert.ToString(dr["Name"]);
                    mod_data.Designation_MicrosoftID = Convert.ToString(dr["MicrosoftID"]);

                    data.Add(mod_data);
                }

            }
            catch (Exception ex) { }

            return data;
        }

        public int Save_Designation(Mod_Designation Data, string type)
        {
            int status = 0;

            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Employee";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update_Designation" || type == "Delete_Designation")
                {
                    SqlParameter Designation_Id = new SqlParameter("@Emp_Unique_Id", Data.Designation_Id);
                    cmd.Parameters.Add(Designation_Id);
                }

                SqlParameter Designation_Name = new SqlParameter("@Emp_Name", Data.Designation_Name);
                cmd.Parameters.Add(Designation_Name);

                SqlParameter Designation_Type = new SqlParameter("@Emp_Type", Data.Designation_Type);
                cmd.Parameters.Add(Designation_Type);

                SqlParameter Designation_MicrosoftID = new SqlParameter("@Emp_Designation", Data.Designation_MicrosoftID);
                cmd.Parameters.Add(Designation_MicrosoftID);


                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.UserId);
                cmd.Parameters.Add(User_Id);

                con.Open();

                status = cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Designation Get_Designation_By_ID(string Unique_Id, Mod_Designation Data)
        {

            try
            {
                DataTable dt = new DataTable();

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Designation_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter Emp_Unique_Id = new SqlParameter("@Emp_Unique_Id", Unique_Id);
                    cmd.Parameters.Add(Emp_Unique_Id);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    Data.Designation_Id = Convert.ToString(dt.Rows[0]["Unique_Id"]);
                    Data.Designation_Name = Convert.ToString(dt.Rows[0]["Emp_Code"]);
                    Data.Designation_MicrosoftID = Convert.ToString(dt.Rows[0]["Emp_Name"]);
                }

            }
            catch (Exception ex) { }

            return Data;
        }



        //----------------------------------------------------  Bind Data -------------------------------------------------------

        public List<SelectListItem> Bind_Designation(string Type)
        {
            List<SelectListItem> Designation_List = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Designation_DropDown");
                    cmd.Parameters.Add(sqlP_type);
                    SqlParameter sqlP_Desigtype = new SqlParameter("@Emp_Type", Type);
                    cmd.Parameters.Add(sqlP_Desigtype);

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
                    SelectListItem val = new SelectListItem();

                    val.Value = Convert.ToString(dr["Val"]);
                    val.Text = Convert.ToString(dr["Text"]);

                    Designation_List.Add(val);
                }

            }
            catch (Exception ex) { }


            return Designation_List;
        }

        public List<SelectListItem> Bind_Dept()
        {
            List<SelectListItem> Dept_List = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;

                using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Dept_DropDown");
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

                    SelectListItem val = new SelectListItem();

                    val.Value = Convert.ToString(dr["Val"]);
                    val.Text = Convert.ToString(dr["Text"]);

                    Dept_List.Add(val);
                }

            }
            catch (Exception ex) { }



            return Dept_List;
        }

    }

}