using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace IT_Hardware.Areas.Admin.Data
{
    public class Invoice_BL
    {
        public List<Invoice_Mod> Get_All_Invoice(string UserId)
        {

            Invoice_Mod BL_data;
            List<Invoice_Mod> current_data = new List<Invoice_Mod>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Invoice_HQ"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    //SqlParameter sqlP_User = new SqlParameter("@Create_Usr_Id", UserId);
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
                    BL_data = new Invoice_Mod();

                    BL_data.Invoice_id = Convert.ToString(dr["Invoice_Id"]);
                    BL_data.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                    BL_data.Invoice_Subject = Convert.ToString(dr["InvSubject"]);
                    if(dr["Invoice_Date"] != DBNull.Value)
                        BL_data.Invoice_Date =  Convert.ToDateTime(dr["Invoice_Date"]);
                    if (dr["Inv_Value"] != DBNull.Value)
                        BL_data.Invoice_Value = Convert.ToInt32(dr["Inv_Value"]);
                    if (dr["Penalty_Amt"] != DBNull.Value)
                        BL_data.Penalty_Amount = Convert.ToInt32(dr["Penalty_Amt"]);
                    if (dr["Penalty_Reason"] != DBNull.Value)
                        BL_data.Penalty_Reason = Convert.ToString(dr["Penalty_Reason"]);
                    if (dr["Inv_File_ID"] != DBNull.Value)
                        BL_data.FileId_Invoice = Convert.ToString(dr["Inv_File_ID"]);
                    if (dr["Inv_File_Name"] != DBNull.Value)
                        BL_data.FileName_Invoice = Convert.ToString(dr["Inv_File_Name"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_data(Invoice_Mod Data, string type,  out string Inv_File_Name )
        {

            int status = -1;
            Inv_File_Name = string.Empty;

            SqlConnection con = new DBConnection().con;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Invoice_HQ";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Invoice_Id = new SqlParameter("@Inv_id", Data.Invoice_id);
                    cmd.Parameters.Add(Invoice_Id);
                }

                SqlParameter Invoice_No = new SqlParameter("@Inv_no", Data.Invoice_No);
                cmd.Parameters.Add(Invoice_No);

                SqlParameter Invoice_Subject = new SqlParameter("@Subject", Data.Invoice_Subject);
                cmd.Parameters.Add(Invoice_Subject);

                SqlParameter PO_Id = new SqlParameter("@PO_Id", Data.PO_Id);
                cmd.Parameters.Add(PO_Id);

                SqlParameter Invoice_Value = new SqlParameter("@Inv_Value", Data.Invoice_Value);
                cmd.Parameters.Add(Invoice_Value);

                SqlParameter Penalty_Amount = new SqlParameter("@Penalty_Amt", Data.Penalty_Amount);
                cmd.Parameters.Add(Penalty_Amount);  

                SqlParameter Penalty_Reason = new SqlParameter("@Penalty_Reason", Data.Penalty_Reason);
                cmd.Parameters.Add(Penalty_Reason);

                SqlParameter Invoice_Date = new SqlParameter("@Inv_date", Data.Invoice_Date);
                cmd.Parameters.Add(Invoice_Date);

                

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);


                if (Data.File_Invoice != null)
                {
                    SqlParameter InvFile_Exit = new SqlParameter("@Inv_FileExist", 1);
                    cmd.Parameters.Add(InvFile_Exit);

                    SqlParameter Invoice_FileName = new SqlParameter("@Inv_FileName", Data.File_Invoice.FileName);
                    cmd.Parameters.Add(Invoice_FileName);

                    SqlParameter Invoice_FileExt = new SqlParameter("@Inv_File_Extension", Path.GetExtension(Data.File_Invoice.FileName).ToString());
                    cmd.Parameters.Add(Invoice_FileExt);
                }


                SqlParameter User_Id = new SqlParameter("@Create_Usr_Id", Data.Create_usr_id);
                cmd.Parameters.Add(User_Id);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                { 
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            status = Convert.ToInt32(dt.Rows[0]["Row_Effect"]);
                            Inv_File_Name = Convert.ToString(dt.Rows[0]["Invoice_File"]);
                        }
                    }
                }
            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Invoice_Mod Get_Data_By_ID(string Invoice_Id, string UserId)
        {
            Invoice_Mod Data = new Invoice_Mod();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;

                using (SqlCommand cmd = new SqlCommand("sp_Invoice_HQ"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter Inv_Id = new SqlParameter("@Inv_id", Invoice_Id);
                    cmd.Parameters.Add(Inv_Id);

                    //SqlParameter sqlUserId = new SqlParameter("@Create_Usr_Id", UserId);
                    //cmd.Parameters.Add(sqlUserId);

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
                        Data = new Invoice_Mod();

                        Data.Invoice_id = Convert.ToString(dt_Comuter.Rows[0]["Invoice_Id"]);
                        Data.Invoice_No = Convert.ToString(dt_Comuter.Rows[0]["Invoice_No"]);
                        Data.Invoice_Subject = Convert.ToString(dt_Comuter.Rows[0]["InvSubject"]);


                        if (dt_Comuter.Rows[0]["Invoice_Date"] != DBNull.Value)
                            Data.Invoice_Date = Convert.ToDateTime(dt_Comuter.Rows[0]["Invoice_Date"]);
                        if (dt_Comuter.Rows[0]["Inv_Value"] != DBNull.Value)
                            Data.Invoice_Value = Convert.ToInt32(dt_Comuter.Rows[0]["Inv_Value"]);
                        if (dt_Comuter.Rows[0]["PO_Id"] != DBNull.Value)
                            Data.Penalty_Amount = Convert.ToInt32(dt_Comuter.Rows[0]["PO_Id"]);
                        if (dt_Comuter.Rows[0]["Penalty_Amt"] != DBNull.Value)
                            Data.Penalty_Amount = Convert.ToInt32(dt_Comuter.Rows[0]["Penalty_Amt"]);
                        if (dt_Comuter.Rows[0]["Penalty_Reason"] != DBNull.Value)
                            Data.Penalty_Reason = Convert.ToString(dt_Comuter.Rows[0]["Penalty_Reason"]);
                        if (dt_Comuter.Rows[0]["Inv_File_ID"] != DBNull.Value)
                            Data.FileId_Invoice = Convert.ToString(dt_Comuter.Rows[0]["Inv_File_ID"]);
                        if (dt_Comuter.Rows[0]["Inv_File_Name"] != DBNull.Value)
                            Data.FileName_Invoice = Convert.ToString(dt_Comuter.Rows[0]["Inv_File_Name"]);

                }

            }
            catch (Exception ex) { }

            return Data;
        }


        public List<SelectListItem> PO_List(string UserId)
        {

            List<SelectListItem> List_Item = new List<SelectListItem>();

            try
            {
                DataTable dt_PO;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = 
                    new SqlCommand("SELECT * from  dbo.Get_All_PO()"))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_PO = dt;
                        }
                    }
                }

                foreach (DataRow dr in dt_PO.Rows)
                {
                    SelectListItem Listdata = new SelectListItem();
                    Listdata.Value = Convert.ToString(dr["PO_id"]);
                    Listdata.Text = Convert.ToString(dr["PO_Name"]);

                    List_Item.Add(Listdata);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }


        public List<SelectListItem> Fin_Year_List()
        {

            List<SelectListItem> List_Item = new List<SelectListItem>();

            try
            {
                DataTable dt_PO;

                SqlConnection con = new DBConnection().con;


                //using (SqlCommand cmd = new SqlCommand("SELECT * from  dbo.Get_All_Budget_Year()"))

                using (SqlCommand cmd = new SqlCommand("SELECT * from  dbo.Get_All_Fin_Year()"))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_PO = dt;
                        }
                    }
                }

                foreach (DataRow dr in dt_PO.Rows)
                {
                    SelectListItem Listdata = new SelectListItem();
                    Listdata.Value = Convert.ToString(dr["Bud_Id"]);
                    Listdata.Text = Convert.ToString(dr["Bud_Year"]);

                    List_Item.Add(Listdata);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }

        public List<SelectListItem> Budget_Head_List(string FinYear)
        {

            List<SelectListItem> List_Item = new List<SelectListItem>();

            try
            {
                DataTable dt_PO;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("SELECT * from  dbo.Get_Budget_ByYear(@FinYear)"))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlParameter SqlFinYear = new SqlParameter("@FinYear", FinYear);
                    cmd.Parameters.Add(SqlFinYear);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_PO = dt;
                        }
                    }
                }

                foreach (DataRow dr in dt_PO.Rows)
                {
                    SelectListItem Listdata = new SelectListItem();
                    Listdata.Value = Convert.ToString(dr["Budget_Head_Id"]);
                    Listdata.Text = Convert.ToString(dr["Budget_Name"]);

                    List_Item.Add(Listdata);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }

        public string Get_file_name(string type)
        {

            return "";
        }

    }
}
