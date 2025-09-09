using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using IT_Hardware.Areas.Admin.Models;
using System.Data;
using System.Data.SqlClient;

namespace IT_Hardware.Areas.Admin.Data
{
    public class QRCode_BL
    {
        public List<QRCode_Model> AssetsList(string Dept, string type)
        {

            QRCode_Model data;
            List<QRCode_Model> List_data = new List<QRCode_Model>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_QRCode_Info"))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_All_Assets");
                    cmd.Parameters.Add(sqlP_type);

                    if(Dept !="" && Dept != "-1")
                    { 
                        SqlParameter sqlP_DeptType = new SqlParameter("@InnerType1", Dept);
                        cmd.Parameters.Add(sqlP_DeptType);
                    }

                    if (type != "" && type != "All")
                    {
                        SqlParameter sqlP_Assettype = new SqlParameter("@InnerType2", type);
                        cmd.Parameters.Add(sqlP_Assettype);
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
                    data = new QRCode_Model();


                    data.Item_Id = Convert.ToString(dr["Item_Id"]);

                    data.Asset_SerialNo = Convert.ToString(dr["Item_SlNo"]);

                    data.Asset_Type = Convert.ToString(dr["Asset_Type"]);

                    data.Make = Convert.ToString(dr["Model"]);

                    data.EmployeeName = Convert.ToString(dr["Emp_Name"]);

                    data.Designation = Convert.ToString(dr["Designation"]);

                    data.Department = Convert.ToString(dr["Department"]);


                    List_data.Add(data);
                }

            }
            catch (Exception ex) { }

            return List_data;
        }

        //-------------------------------   Asset Detail Information  -------------------------------------
        public AssetInfo_Model Asset_Detail_Info(string Id)
        {

            AssetInfo_Model model = new AssetInfo_Model();

            try
            {
                DataSet data;
                SqlConnection con;

                using (con = new DBConnection().con)
                {
                    SqlCommand cmd = new SqlCommand("sp_QRCode_Info");
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Asset_Detail_Info");
                    cmd.Parameters.Add(sqlP_type);
                    SqlParameter sqlP_Id = new SqlParameter("@Item_Issue_Id", Id);
                    cmd.Parameters.Add(sqlP_Id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataSet dt = new DataSet())
                        {
                            sda.Fill(dt);
                            data = dt;
                        }
                    } 
                }

                if (data.Tables[0].Rows.Count > 0)
                {

                    model.Asset_Id = Convert.ToString(data.Tables[0].Rows[0]["Item_Id"]);

                    model.Asset_Type = Convert.ToString(data.Tables[0].Rows[0]["Asset_Type"]);

                    model.Asset_SerialNo = Convert.ToString(data.Tables[0].Rows[0]["Item_SlNo"]);

                    model.Procuremt_Date = Convert.ToDateTime(data.Tables[0].Rows[0]["Proc_Date"]);

                    model.Warranty_End_Date = Convert.ToDateTime(data.Tables[0].Rows[0]["Warnt_end_DT"]);

                    model.Make = Convert.ToString(data.Tables[0].Rows[0]["Make"]);

                    model.Model = Convert.ToString(data.Tables[0].Rows[0]["model"]);

                    model.Asset_Value = Convert.ToInt32(data.Tables[0].Rows[0]["Asset_Price"]);

                    model.VenderName = Convert.ToString(data.Tables[0].Rows[0]["Vendor_name"]);

                    model.PO_No = Convert.ToString(data.Tables[0].Rows[0]["PO_No"]);

                    model.PO_Date = Convert.ToDateTime(data.Tables[0].Rows[0]["PO_Date"]);

                    model.PO_Value = Convert.ToInt32(data.Tables[0].Rows[0]["PO_Value"]);

                }

                List<ShiftAsset> ShiftHistory = new List<ShiftAsset>();
                

                foreach (DataRow dr in data.Tables[1].Rows)
                {
                    ShiftAsset shift = new ShiftAsset();

                    shift.Issued_date = Convert.ToDateTime(dr["Item_Issue_date"]);

                    shift.EmployeeName = Convert.ToString(dr["Emp_Name"]);

                    shift.Designation = Convert.ToString(dr["Designation"]);

                    shift.Department = Convert.ToString(dr["Dept"]);

                    ShiftHistory.Add(shift);

                }
                model.ShiftingHistory = ShiftHistory;


                List<AssetService> ServiceHistory = new List<AssetService>();
                if (data.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in data.Tables[2].Rows)
                    { 
                        AssetService service = new AssetService();

                        //service.EmployeeName = Convert.ToString(dr["Emp_Name"]);
                        //service.Designation = Convert.ToString(dr["Designation"]);
                        //service.Department = Convert.ToString(dr["Department"]);

                        service.Issue_Id = Convert.ToString(dr["Id"]);
                        service.Issue_Create_Date = Convert.ToDateTime(dr["Issue_Create_Date"]);
                        service.IssueInfo = Convert.ToString(dr["IssueInfo"]);
                        service.VenderName = Convert.ToString(dr["Vendor_name"]);
                        service.Resolved = Convert.ToInt32(dr["Resolved"]) == 1 ? true : false;
                        service.Issue_Resolve_Date = Convert.ToDateTime(dr["Issue_Resolve_Date"]);
                        service.Resolution_Detail = Convert.ToString(dr["Resolution_Detail"]);

                        ServiceHistory.Add(service);
                    }
                }
                else
                {
                    AssetService service = new AssetService();
                    service.Issue_Create_Date = DateTime.Now.AddDays(1) ;
                    ServiceHistory.Add(service);
                }


                model.ServiceHistory = ServiceHistory;
            }
            catch (Exception ex) { }

            return model;
        }


        public string Get_SerialNo(string Id)
        {
            String SLNO= string.Empty;
            using (SqlConnection con = new DBConnection().con)
            {
                using (SqlCommand cmd = new SqlCommand("select Item_SlNo from Assets_details where LTRIM(RTRIM(Item_Id)) = LTRIM(RTRIM(@ItemId))"))
                {
                    cmd.Parameters.AddWithValue("@ItemId", Id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            SLNO = Convert.ToString(sdr["Item_SlNo"]);
                               
                        }
                    }
                    con.Close();
                }
            }

            return SLNO;

        }

        //-------------------------------Asset Service---------------------------------------------
        public RaiseIssue_Mod Get_Asset_Service_Info(string AssetId, string type)
        {
            RaiseIssue_Mod data = new RaiseIssue_Mod() ;

            try
            {
                DataTable dt;

                SqlConnection con = new DBConnection().con;

                using (SqlCommand cmd = new SqlCommand("sp_QRCode_Info"))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    if (type == "New")
                    {
                        SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Asset_Info");
                        cmd.Parameters.Add(sqlP_type);
                        SqlParameter Asset_Id = new SqlParameter("@Asset_Id", AssetId);
                        cmd.Parameters.Add(Asset_Id);
                    }
                    else
                    {
                        SqlParameter sqlP_type = new SqlParameter("@Type", "Get_ServiceIssue");
                        cmd.Parameters.Add(sqlP_type);
                        SqlParameter Issue_Id = new SqlParameter("@Item_Issue_Id", AssetId);
                        cmd.Parameters.Add(Issue_Id);
                    }
                        
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        dt = new DataTable();
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);                      
                    }
                }

                data.Item_Issue_Id = AssetId;
                data.AssetId = Convert.ToString(dt.Rows[0]["Item_Id"]); ;
                data.AssetType_SerialNo = Convert.ToString(dt.Rows[0]["Asset_Type"]);
                data.Make_Model = Convert.ToString(dt.Rows[0]["Model"]);
                data.Employee_Name_Desig_Dept = Convert.ToString(dt.Rows[0]["Emp_Name"]);
                //data.Item_Issue_Id = Convert.ToString(dt.Rows[0]["Issue_Id"]);

                if(type == "New")
                {
                    data.Issue_Create_Date = DateTime.Now;
                }
                else
                {
                    data.Id = Convert.ToString(dt.Rows[0]["ID"]);
                    data.Issue_Create_Date = Convert.ToDateTime(dt.Rows[0]["Issue_Create_Date"]);
                    data.IssueInfo = Convert.ToString(dt.Rows[0]["IssueInfo"]);
                    data.Resolved = Convert.ToInt32(dt.Rows[0]["Resolved"]) == 1 ? true : false;
                    data.VenderName = Convert.ToString(dt.Rows[0]["VenderName"]);
                    data.Issue_Resolve_Date = Convert.ToDateTime(dt.Rows[0]["Issue_Resolve_Date"]);
                    data.Resolution_Detail = Convert.ToString(dt.Rows[0]["Resolution_Detail"]);
                    data.Remarks = Convert.ToString(dt.Rows[0]["Remarks"]);
                }               
            }
            catch (Exception ex) { }

            return data;
        }

        public int InsUpd_AssetService(RaiseIssue_Mod Inputdata, string type)
        {

            int status = 1;

            SqlConnection con = new DBConnection().con;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_QRCode_Info";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                SqlParameter AssetId = new SqlParameter("@Asset_Id", Inputdata.AssetId);
                cmd.Parameters.Add(AssetId);

                SqlParameter Item_Issue_Id = new SqlParameter("@Item_Issue_Id", Inputdata.Item_Issue_Id);
                cmd.Parameters.Add(Item_Issue_Id);

                SqlParameter Issue_Create_Date = new SqlParameter("@Issue_Create_Date", Inputdata.Issue_Create_Date);
                cmd.Parameters.Add(Issue_Create_Date);

                SqlParameter IssueInfo = new SqlParameter("@IssueDetails", Inputdata.IssueInfo);
                cmd.Parameters.Add(IssueInfo);

                SqlParameter VenderName = new SqlParameter("@VenderId", Inputdata.VenderName);
                cmd.Parameters.Add(VenderName);

                if (Inputdata.Resolved != null)
                {
                    SqlParameter Resolved = new SqlParameter("@Resolved", Inputdata.Resolved == true ? 1 : 0);
                    cmd.Parameters.Add(Resolved);
                }
                SqlParameter Issue_Resolve_Date = new SqlParameter("@Issue_Resolve_Date", Inputdata.Issue_Resolve_Date);
                cmd.Parameters.Add(Issue_Resolve_Date);

                SqlParameter Resolution_Detail = new SqlParameter("@Resolution_Detail", Inputdata.Resolution_Detail);
                cmd.Parameters.Add(Resolution_Detail);

                SqlParameter Remarks = new SqlParameter("@Remarks", Inputdata.Remarks);
                cmd.Parameters.Add(Remarks);

                SqlParameter User_Id = new SqlParameter("@UserId", Inputdata.UserId);
                cmd.Parameters.Add(User_Id);


                con.Open();

                status = cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;

        }

        public List<RaiseIssue_Mod> Get_All_Serivce_Info(string user, string IssueType)
        {
            List<RaiseIssue_Mod> Listdata = new List<RaiseIssue_Mod>() ;

            try
            {
                DataTable dt= new DataTable();

                SqlConnection con = new DBConnection().con;

                using (SqlCommand cmd = new SqlCommand("sp_QRCode_Info"))
                {                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_All_Issue");
                    cmd.Parameters.Add(sqlP_type);
                    SqlParameter sqlP_IssueType = new SqlParameter("@InnerType1", IssueType);
                    cmd.Parameters.Add(sqlP_IssueType);
                    SqlParameter sqlP_user = new SqlParameter("@InnerType2", user);
                    cmd.Parameters.Add(sqlP_user);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);                     
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    RaiseIssue_Mod data = new RaiseIssue_Mod();

                    data.AssetId = Convert.ToString(dr["Asset_Id"]);
                    data.AssetType_SerialNo = Convert.ToString(dr["Asset_Type"]);
                    data.Make_Model = Convert.ToString(dr["model"]);
                    data.Employee_Name_Desig_Dept = Convert.ToString(dr["Emp_Name"]);                   
                    data.Id = Convert.ToString(dr["ID"]);
                    data.Issue_Create_Date = Convert.ToDateTime(dr["Issue_Create_Date"]);
                    data.IssueInfo = Convert.ToString(dr["IssueInfo"]);
                    if(dr["Resolved"] != DBNull.Value)
                    {
                        data.Resolved = Convert.ToInt32(dr["Resolved"]) == 1 ? true : false;
                    }
                    data.VenderName = Convert.ToString(dt.Rows[0]["VenderName"]);
                    if (dr["Issue_Resolve_Date"] != DBNull.Value)
                    {
                        data.Issue_Resolve_Date = Convert.ToDateTime(dr["Issue_Resolve_Date"]);
                    }
                    data.Resolution_Detail = Convert.ToString(dr["Resolution_Detail"]);
                    data.Remarks = Convert.ToString(dr["Remarks"]);

                    Listdata.Add(data);
                }

            }
            catch (Exception ex) { }

            return Listdata;
        }
       
    }
}
