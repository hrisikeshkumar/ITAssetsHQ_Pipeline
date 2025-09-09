using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Item_Issue
    {

        public List<Mod_Item_Issue> Get_Item_IssueData()
        {

            Mod_Item_Issue BL_data;
            List<Mod_Item_Issue> current_data = new List<Mod_Item_Issue>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Item_Issue_HQ"))
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
                    BL_data = new Mod_Item_Issue();

                    BL_data.Item_Issue_Id = Convert.ToString(dr["Issue_Id"]);

                    BL_data.Item_SerialNo = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Transfered_Emp_Name = Convert.ToString(dr["Emp_Name"]);

                    BL_data.Transfered_Emp_Designation = Convert.ToString(dr["Designation_name"]);

                    BL_data.Transfered_Emp_Dept = Convert.ToString(dr["Dept"]);

                    BL_data.Previous_Emp_Name = Convert.ToString(dr["Prev_Emp_Name"]);

                    BL_data.Issued_date = Convert.ToDateTime(dr["Item_Issue_date"]);

                    BL_data.Item_Type = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Issue_File_Id = Convert.ToString(dr["FileId"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }


        public List<Mod_Item_Issue> Get_Item_By_Sl(string SearchVal, string SearchType)
        {

            Mod_Item_Issue BL_data;
            List<Mod_Item_Issue> current_data = new List<Mod_Item_Issue>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Item_Issue_HQ"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_By_SL");
                    cmd.Parameters.Add(sqlP_type);
                    SqlParameter sqlP_SearchType = new SqlParameter("@SearchType", SearchType);
                    cmd.Parameters.Add(sqlP_SearchType);
                    SqlParameter Sl_No = new SqlParameter("@Item_Id", SearchVal);
                    cmd.Parameters.Add(Sl_No);

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
                    BL_data = new Mod_Item_Issue();

                    BL_data.Item_Issue_Id = Convert.ToString(dr["Issue_Id"]);

                    BL_data.Item_SerialNo = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Transfered_Emp_Name = Convert.ToString(dr["Emp_Name"]);

                    BL_data.Transfered_Emp_Designation = Convert.ToString(dr["Designation_name"]);

                    BL_data.Transfered_Emp_Dept = Convert.ToString(dr["Dept"]);

                    BL_data.Previous_Emp_Name = Convert.ToString(dr["Prev_Emp_Name"]);

                    BL_data.Issued_date = Convert.ToDateTime(dr["Item_Issue_date"]);

                    BL_data.Item_Type = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Issue_File_Id = Convert.ToString(dr["FileId"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }


        public int Save_Item_Issue_data(Mod_Item_Issue Data, string type, string Issued_Id, int File_Exist, out string File_Id)
        {
            File_Id=string.Empty;
            int status = 1;
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Item_Issue_HQ";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter SqlIssued_Id = new SqlParameter("@Item_Issue_id", Issued_Id);
                    cmd.Parameters.Add(SqlIssued_Id);
                }

                SqlParameter Item_Id = new SqlParameter("@Item_Id", Data.Item_Id);
                cmd.Parameters.Add(Item_Id);

                SqlParameter Present_Company = new SqlParameter("@Previous_Emp_Id", Data.Previous_Custady_Id);
                cmd.Parameters.Add(Present_Company);

                SqlParameter Shifted_Company = new SqlParameter("@Present_Emp_Id", Data.Transfered_Custady_Id);
                cmd.Parameters.Add(Shifted_Company);

                SqlParameter Shift_date = new SqlParameter("@Item_Issue_Date", Data.Issued_date);
                cmd.Parameters.Add(Shift_date);

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

                SqlParameter Sql_FileExist = new SqlParameter("@File_Exist", File_Exist);
                cmd.Parameters.Add(Sql_FileExist);


                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.Create_usr_id);
                cmd.Parameters.Add(User_Id);

                con.Open();



                using (SqlDataAdapter sda = new SqlDataAdapter())
                {

                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            File_Id = Convert.ToString(dt.Rows[0]["File_Id"]);
                            status = Convert.ToInt32(dt.Rows[0]["Row_Effect"]);
                        }
                    }
                }



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }


        public Mod_Item_Issue Get_Data_By_ID(string AssetId)
        {
            Mod_Item_Issue Data = new Mod_Item_Issue();

            try
            {
                DataTable dt_Comuter;               
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Item_Issue_HQ"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Asset_Issue_Info");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Asset_Id = new SqlParameter("@Item_ID", AssetId);
                    cmd.Parameters.Add(sqlP_Asset_Id);

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
                    Data.Item_Id = Convert.ToString(dt_Comuter.Rows[0]["Item_Id"]);
                    Data.Item_SerialNo = Convert.ToString(dt_Comuter.Rows[0]["Item_SlNo"]);
                    Data.Item_Name = Convert.ToString(dt_Comuter.Rows[0]["Asset_Type"]);
                    Data.Previous_Emp_Name = Convert.ToString(dt_Comuter.Rows[0]["Emp_Name"]);
                    Data.Previous_Emp_Designation = Convert.ToString(dt_Comuter.Rows[0]["Designation"]);
                    Data.Previous_Emp_Dept = Convert.ToString(dt_Comuter.Rows[0]["Dept"]);
                    Data.Previous_Emp_Location = Convert.ToString(dt_Comuter.Rows[0]["Emp_Location"]);
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);
                }

            }
            catch (Exception ex) { }

            return Data;
        }


        public List<Item_SL_Wise> Item_SLnumber_List( string Sl_No)
        {

            List<Item_SL_Wise> List_Item =new List<Item_SL_Wise>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("Item_List_Serial_No_Wise_HQ"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_Item_Type = new SqlParameter("@Item_SlNo", Sl_No);
                    cmd.Parameters.Add(sqlP_Item_Type);

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
                    Item_SL_Wise item = new Item_SL_Wise();
                    item.Item_Id = Convert.ToString(dr["Item_Id"]);
                    item.Item_SL_Number = Convert.ToString(dr["Item_SlNo"]);
                    item.Item_Type = Convert.ToString(dr["Asset_Type"]);

                    List_Item.Add(item);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }


        public Mod_Item_Issue_Employee Issue_Employee(Mod_Item_Issue_Employee Emp_Details, string Sl_No, string Type)
        {

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("Find_Item_Issue"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_Item_Id = new SqlParameter("@Item_Id", Sl_No);
                    cmd.Parameters.Add(sqlP_Item_Id);
                    SqlParameter sqlP_Item_Type = new SqlParameter("@Type", Type);
                    cmd.Parameters.Add(sqlP_Item_Type);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter = dt;

                            if (dt_Comuter.Rows.Count >0)
                            {
                               

                                    Emp_Details.Previous_Custady_Id = Convert.ToString(dt_Comuter.Rows[0]["Present_Emp_Id"]);
                                    Emp_Details.Previous_Emp_Name = Convert.ToString(dt_Comuter.Rows[0]["Emp_Name"]);
                                    Emp_Details.Previous_Emp_Designation = Convert.ToString(dt_Comuter.Rows[0]["Designation_name"]);
                                    Emp_Details.Previous_Emp_Dept = Convert.ToString(dt_Comuter.Rows[0]["Dept_name"]);
                                    Emp_Details.Previous_Emp_Location = Convert.ToString(dt_Comuter.Rows[0]["Emp_Location"]);
                                    //Emp_Details.Transfered_Emp_Type = Convert.ToString(dt_Comuter.Rows[0]["Item_Id"]);
                                    Emp_Details.Issue_File_Id = Convert.ToString(dt_Comuter.Rows[0]["FileId"]);

                            }

                        }
                    }
                }

             
                


            }
            catch (Exception ex) { }

            return Emp_Details;
        }


        public List<Mod_Item_Issue_Employee> Emp_List(string EmpName)
        {

            List<Mod_Item_Issue_Employee> List_Item = new List<Mod_Item_Issue_Employee>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("Emp_List_HQ"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_Item_Type = new SqlParameter("@EmpName", EmpName);
                    cmd.Parameters.Add(sqlP_Item_Type);

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
                    Mod_Item_Issue_Employee item = new Mod_Item_Issue_Employee();

                    item.Transfered_Custady_Id = Convert.ToString(dr["Unique_Id"]);
                    item.Transfered_Emp_Name = Convert.ToString(dr["Emp_Name"]);
                    item.Transfered_Emp_Designation = Convert.ToString(dr["Designation_name"]);
                    item.Transfered_Emp_Dept = Convert.ToString(dr["Dept_name"]);
                    item.Transfered_Emp_Location = Convert.ToString(dr["Emp_Location"]);

                    List_Item.Add(item);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }

    }


    public class Item_SL_Wise
    { 
        public string Item_Id { get; set; }
        public string Item_SL_Number { get; set; }
        public string Item_Type { get; set; }
    }



}