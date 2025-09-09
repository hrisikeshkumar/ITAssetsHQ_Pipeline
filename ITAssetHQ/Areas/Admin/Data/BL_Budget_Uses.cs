using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using IT_Hardware.IServices;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Budget_Uses : IServices.IBudgetUsesService
    {

        List<Bud_Uses_List> IBudgetUsesService.Get_BudgetData()
        {

            Bud_Uses_List BL_data;
            List<Bud_Uses_List> current_data = new List<Bud_Uses_List>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Uses"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
                    cmd.Parameters.Add(sqlP_type);

                    //SqlParameter sqlP_Year = new SqlParameter("@Budget_Year", "2022-2023");
                    //cmd.Parameters.Add(sqlP_Year);

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
                    BL_data = new Bud_Uses_List();

                    BL_data.Budget_Uses_Id = Convert.ToString(dr["Budget_Uses_Id"]);

                    BL_data.Utilization_Details = Convert.ToString(dr["Utilization_Details"]);

                    BL_data.Budget_Name = Convert.ToString(dr["Budget_Name"]);

                    BL_data.Budget_Amount = Convert.ToInt32(dr["Budget_Amount"]);

                    BL_data.Budget_Type = Convert.ToString(dr["Budget_Type"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_Budget_data(Mod_Budget_Uses Data, string type, string Budget_Uses_ID)
        {
            int status = 1;
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Budget_Uses";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Budget_ID = new SqlParameter("@Budget_Uses_Id", Budget_Uses_ID);
                    cmd.Parameters.Add(Budget_ID);
                }

                SqlParameter Budget_Head_Id = new SqlParameter("@Budget_Head_Id", Data.Budget_Head_Id);
                cmd.Parameters.Add(Budget_Head_Id);

                SqlParameter Budget_Type = new SqlParameter("@Budget_Type", Data.Budget_Type);
                cmd.Parameters.Add(Budget_Type);

                SqlParameter PO_Id = new SqlParameter("@PO_Id", Data.PO_id);
                cmd.Parameters.Add(PO_Id);

                SqlParameter Utilization_Details = new SqlParameter("@Utilization_Details", Data.Utilization_Details);
                cmd.Parameters.Add(Utilization_Details);

                SqlParameter Total_approved_Budget = new SqlParameter("@Total_Approved_Budget", Data.Total_Approved_Budget);
                cmd.Parameters.Add(Total_approved_Budget);

                SqlParameter Amount_Utilized_Before = new SqlParameter("@Amount_Utilized_Before", Data.Amount_Utilized_Before);
                cmd.Parameters.Add(Amount_Utilized_Before);

                SqlParameter Balance_Available = new SqlParameter("@Balance_Available", Data.Balance_Available);
                cmd.Parameters.Add(Balance_Available);

                SqlParameter Budget_Amount = new SqlParameter("@Budget_Amount", Data.Budget_Amount);
                cmd.Parameters.Add(Budget_Amount);

                SqlParameter Remaning_Balance = new SqlParameter("@Remaning_Balance", Data.Remaning_Balance);
                cmd.Parameters.Add(Remaning_Balance);

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

                SqlParameter Entry_Date = new SqlParameter("@Processing_Date", Data.Processing_Date);
                cmd.Parameters.Add(Entry_Date);

                SqlParameter UserId = new SqlParameter("@Create_Usr_Id", Data.Create_User);
                cmd.Parameters.Add(UserId);


                con.Open();

                status = cmd.ExecuteNonQuery();


            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Budget_Uses Get_Data_By_ID(Mod_Budget_Uses Data, string Budget_Uses_Id)
        {


            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Uses"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Asset_ID = new SqlParameter("@Budget_Uses_Id", Budget_Uses_Id);
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
                    Data.Budget_Head_Id = Convert.ToString(dt_Comuter.Rows[0]["Budget_Head_Id"]);
                    Data.Budget_Uses_Id = Convert.ToString(dt_Comuter.Rows[0]["Budget_Uses_Id"]);
                    Data.Budget_Name = Convert.ToString(dt_Comuter.Rows[0]["Budget_Name"]);
                    Data.Budget_Year = Convert.ToString(dt_Comuter.Rows[0]["Budget_Year"]);
                    Data.Utilization_Details = Convert.ToString(dt_Comuter.Rows[0]["Utilization_Details"]);
                    Data.Budget_Type = Convert.ToString(dt_Comuter.Rows[0]["Budget_Type"]);
                    Data.PO_id = Convert.ToString(dt_Comuter.Rows[0]["PO_id"]);
                    Data.PO_No = Convert.ToString(dt_Comuter.Rows[0]["PO_No"]);
                    Data.Total_Approved_Budget = Convert.ToInt32(dt_Comuter.Rows[0]["Total_Budget_Amount"]);
                    Data.Amount_Utilized_Before = Convert.ToInt32(dt_Comuter.Rows[0]["Amount_Utilized"]);
                    Data.Balance_Available = Convert.ToInt32(dt_Comuter.Rows[0]["Balance_Available"]);
                    Data.Budget_Amount = Convert.ToInt32(dt_Comuter.Rows[0]["Budget_Amount"]);
                    Data.Remaning_Balance = Convert.ToInt32(dt_Comuter.Rows[0]["Remaning_Balance"]);
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);
                    Data.Processing_Date = Convert.ToDateTime(dt_Comuter.Rows[0]["Entry_Date"]);
                }

            }
            catch (Exception ex) { }

            return Data;
        }

        public void Get_Budget_Head(Mod_Budget_Uses Mod_Data, string Bud_Year)
        {

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_GetAllBudgetHead"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_Bud_Year = new SqlParameter("@Year", Bud_Year);
                    cmd.Parameters.Add(sqlP_Bud_Year);

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
                    List<SelectListItem> List_Item = new List<SelectListItem>();
                    SelectListItem li = new SelectListItem();

                    li.Text = "Please Select";
                    li.Value = string.Empty;
                    List_Item.Add(li);

                    foreach (DataRow dr in dt_Comuter.Rows)
                    {
                        li = new SelectListItem();
                        li.Text = Convert.ToString(dr["Budget_Name"]);
                        li.Value = Convert.ToString(dr["Budget_Head_Id"]);

                        List_Item.Add(li);
                    }
                    Mod_Data.Budget_List = List_Item;
                }
            }
            catch (Exception ex) { }
        }

        public void Get_Prev_Budget_Uses(Mod_Budget_Uses Mod_Data, string Bud_Head_Id, string Bud_Year)
        {

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Uses"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_Bud_Head_Id = new SqlParameter("@Budget_Head_Id", Bud_Head_Id);
                    cmd.Parameters.Add(sqlP_Bud_Head_Id);

                    SqlParameter sqlP_Bud_Year = new SqlParameter("@Budget_Year", Bud_Year);
                    cmd.Parameters.Add(sqlP_Bud_Year);

                    SqlParameter sqlP_Type = new SqlParameter("@Type", "Get_Bud_Info");
                    cmd.Parameters.Add(sqlP_Type);

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
                                     
                    Mod_Data.Total_Approved_Budget = Convert.ToInt32(dt_Comuter.Rows[0]["Total_Approved_Budget"]);

                    if (Convert.ToString( dt_Comuter.Rows[0]["Amount_Utilized_Prev"] ) != string.Empty )
                        Mod_Data.Amount_Utilized_Before = Convert.ToInt32(dt_Comuter.Rows[0]["Amount_Utilized_Prev"]);

                    if (Convert.ToString(dt_Comuter.Rows[0]["Balance_Budget"]) != string.Empty)
                        Mod_Data.Balance_Available = Convert.ToInt32(dt_Comuter.Rows[0]["Balance_Budget"]);
                }
            }
            catch (Exception ex) { }
        }

        List<Bud_Uses_List> IBudgetUsesService.Get_BudgetUses_By_BudId(string Bud_head_Id)
        {

            Bud_Uses_List BL_data;
            List<Bud_Uses_List> current_data = new List<Bud_Uses_List>();

            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Uses"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List_By_BudId");
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Year = new SqlParameter("@Budget_Head_Id", Bud_head_Id);
                    cmd.Parameters.Add(sqlP_Year);

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
                    BL_data = new Bud_Uses_List();

                    BL_data.Budget_Uses_Id = Convert.ToString(dr["Budget_Uses_Id"]);

                    BL_data.Utilization_Details = Convert.ToString(dr["Utilization_Details"]);

                    BL_data.Budget_Name = Convert.ToString(dr["Budget_Name"]);

                    BL_data.Budget_Amount = Convert.ToInt32(dr["Budget_Amount"]);

                    BL_data.Budget_Type = Convert.ToString(dr["Budget_Type"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }


        public List<PO_Info> Get_PO_Info(string PO_No)
        {

            
            List<PO_Info> data = new List<PO_Info>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_PO_Info"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;


                    SqlParameter sqlP_Year = new SqlParameter("@Variable", PO_No);
                    cmd.Parameters.Add(sqlP_Year);

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
                    PO_Info PO_data = new PO_Info();

                    PO_data.PO_Id = Convert.ToString(dr["PO_id"]);

                    PO_data.Vendor_Name = Convert.ToString(dr["Vendor_name"]);

                    PO_data.PO_No = Convert.ToString(dr["PO_No"]);

                    PO_data.PO_Date =  Convert.ToDateTime(dr["PO_Date"]).ToString().Substring(0,10);

                    PO_data.PO_Detail = Convert.ToString(dr["PO_Sub"]);
        

                    data.Add(PO_data);
                }

            }
            catch (Exception ex) { }

            return data;
        }

    }
}