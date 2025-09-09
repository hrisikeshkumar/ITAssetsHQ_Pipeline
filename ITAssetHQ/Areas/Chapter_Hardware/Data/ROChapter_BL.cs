using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Chapter_Hardware.Models;
using System.Data;
using System.Data.SqlClient;

namespace IT_Hardware.Areas.Chapter_Hardware.Data
{
    public class ROChapter_BL
    {
        public List<ROChapter_Mod> Get_CompData()
        {

            ROChapter_Mod BL_data;
            List<ROChapter_Mod> current_data = new List<ROChapter_Mod>();

            try
            {
                SqlConnection con = new DBConnection().con;
                
                DataTable dt_Comuter;
                using (SqlCommand cmd = new SqlCommand("sp_ROsAset"))
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
                    BL_data = new ROChapter_Mod();

                    BL_data.Item_Type = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Item_serial_No = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Fund_Provided= Convert.ToString(dr["Fund_Provided"]);

                    BL_data.Proc_date = Convert.ToDateTime(dr["Proc_Date"]);

                    BL_data.price = Convert.ToInt32(dr["Asset_Price"]);

                    BL_data.Item_Sold = Convert.ToString(dr["Item_Sold"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_data(ROChapter_Mod Data, string type, string Asset_ID)
        {
            int status = 1;
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ROsAset";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Asset_Id = new SqlParameter("@Item_id", Asset_ID);
                    cmd.Parameters.Add(Asset_Id);
                }


                SqlParameter Asset_Make_Id = new SqlParameter("@Item_Model_id", Data.Item_Model_id);
                cmd.Parameters.Add(Asset_Make_Id);
                SqlParameter Asset_Type = new SqlParameter("@Item_Type", Data.Item_Type);
                cmd.Parameters.Add(Asset_Type);
                SqlParameter Asset_SL_No = new SqlParameter("@Item_serial_No", Data.Item_serial_No);
                cmd.Parameters.Add(Asset_SL_No);
                SqlParameter ProcDate = new SqlParameter("@Proc_date", Data.Proc_date);
                cmd.Parameters.Add(ProcDate);
                SqlParameter ROChapterName = new SqlParameter("@ROChapterName", Data.ROChapterName);
                cmd.Parameters.Add(ROChapterName);
                SqlParameter Fund_Provided = new SqlParameter("@Fund_Provided", Data.ROChapterName);
                cmd.Parameters.Add(Fund_Provided);
                SqlParameter sql_Vendor = new SqlParameter("@Vendor_Id", Data.Vendor_Name);
                cmd.Parameters.Add(sql_Vendor);
                SqlParameter Invoice_Number = new SqlParameter("@Invoice_Number", Data.Invoice_Number);
                cmd.Parameters.Add(Invoice_Number);
                SqlParameter Invoice_File_Name = new SqlParameter("@Invoice_File_Name", Data.Invoice_File);
                cmd.Parameters.Add(Invoice_File_Name);
                SqlParameter price = new SqlParameter("@price", Data.price);
                cmd.Parameters.Add(price);
                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);
                SqlParameter Sanction_Order_File_Name = new SqlParameter("@Sanction_Order_File_Name", Data.Sanction_Order_File);
                cmd.Parameters.Add(Sanction_Order_File_Name);
                SqlParameter Sanction_Order_ID = new SqlParameter("@Sanction_Order_ID", Data.Invoice_Number);
                cmd.Parameters.Add(@Sanction_Order_ID);
                SqlParameter Sanction_Order_Date = new SqlParameter("@Sanction_Order_Date", Data.Invoice_File);
                cmd.Parameters.Add(Sanction_Order_Date);
                SqlParameter Item_Sold = new SqlParameter("@Item_Sold", Data.Item_Sold);
                cmd.Parameters.Add(Item_Sold);
                SqlParameter Sold_DT = new SqlParameter("@Sold_DT", Data.Sanction_Order_File);
                cmd.Parameters.Add(Sold_DT);
                SqlParameter Sold_To = new SqlParameter("@Sold_To", Data.Invoice_Number);
                cmd.Parameters.Add(Sold_To);
                SqlParameter Sold_Doc_File = new SqlParameter("@Sold_Doc_File", Data.Invoice_File);
                cmd.Parameters.Add(Sold_Doc_File);
                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.Create_user);
                cmd.Parameters.Add(User_Id);


                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }


        public ROChapter_Mod Get_Data_By_ID(ROChapter_Mod Data, string Asset_Id)
        {
            try
            {
                DataTable dt_Comuter;
                SqlConnection con = new DBConnection().con;

                using (SqlCommand cmd = new SqlCommand("sp_ROsAset"))
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

                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    Data = new ROChapter_Mod();

                    Data.Item_Type = Convert.ToString(dr["Asset_Type"]);

                    Data.Item_serial_No = Convert.ToString(dr["Item_SlNo"]);

                    Data.Fund_Provided = Convert.ToString(dr["Fund_Provided"]);

                    Data.Proc_date = Convert.ToDateTime(dr["Proc_Date"]);

                    Data.price = Convert.ToInt32(dr["Asset_Price"]);

                    Data.Item_Sold = Convert.ToString(dr["Item_Sold"]);

                   
                }

            }
            catch (Exception ex) { }

            return Data;
        }


        public List<SelectListItem> Item_MakeModel_List(string Item_Type, string List_Type, string Item_Make)
        {

            List<SelectListItem> List_Item = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("ItemMakeModel_List"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_Item_Type = new SqlParameter("@Item_Type", Item_Type);
                    cmd.Parameters.Add(sqlP_Item_Type);

                    SqlParameter sqlP_List_Type = new SqlParameter("@List_Type", List_Type);
                    cmd.Parameters.Add(sqlP_List_Type);

                    if (List_Type != "MAKE")
                    {
                        SqlParameter sqlP_Item_Make = new SqlParameter("@Make", Item_Make);
                        cmd.Parameters.Add(sqlP_Item_Make);
                    }

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
                    SelectListItem Listdata = new SelectListItem();
                    Listdata.Value = Convert.ToString(dr[0]);
                    Listdata.Text = Convert.ToString(dr[1]);

                    List_Item.Add(Listdata);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }


    }
}
