using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace IT_Hardware.Areas.Admin.Data
{
    public class BL_Admin_DashB
    {
        public List<mod_Admin_Propsal_List> Get_List_Proposal()
        {

            mod_Admin_Propsal_List BL_data;
            List<mod_Admin_Propsal_List> current_data = new List<mod_Admin_Propsal_List>();

            try
            {
                DataTable dt_Comuter;
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_IT_Proposal"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Proposal_List");
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
                    BL_data = new mod_Admin_Propsal_List();

                    BL_data.Proposal_Id = Convert.ToString(dr["Proposal_Id"]);

                    BL_data.Utilization_Details = Convert.ToString(dr["Utilization_Details"]);

                    BL_data.IT_Initiate_Date = Convert.ToDateTime(dr["IT_Initiate_Date"]);

                    BL_data.Status = Convert.ToString(dr["Completed_Status"]);

                    BL_data.NoteLocation = Convert.ToString(dr["NoteLocation"]);



                    if (BL_data.Status == "Completed")
                    {
                        BL_data.NoteLocation = "";
                    }

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }


        public List<mod_Admin_Bill_Process_List> Get_List_Bills(string PO_Id, string SP_Type , out string PO_No)
        {

            mod_Admin_Bill_Process_List BL_data;
            List<mod_Admin_Bill_Process_List> current_data = new List<mod_Admin_Bill_Process_List>();
            PO_No = string.Empty;
            try
            {
                DataTable dt_Comuter;
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_IT_Proposal"))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    SqlParameter sqlP_type = new SqlParameter("@Type", SP_Type);
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_PO_Id = new SqlParameter("@PO_Id", PO_Id);
                    cmd.Parameters.Add(sqlP_PO_Id);

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
                    BL_data = new mod_Admin_Bill_Process_List();

                    BL_data.Proposal_Id = Convert.ToString(dr["Proposal_Id"]);

                    BL_data.Utilization_Details = Convert.ToString(dr["Utilization_Details"]);

                    BL_data.IT_Initiate_Date = Convert.ToDateTime(dr["IT_Initiate_Date"]);

                    BL_data.Status = Convert.ToString(dr["Completed_Status"]);

                    BL_data.NoteLocation = Convert.ToString(dr["NoteLocation"]);

                    PO_No =  Convert.ToString(dr["PO_No"]);

                    if (BL_data.Status == "Completed")
                    {
                        BL_data.NoteLocation = "";
                    }

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }


        public async Task<Proposal_details> Get_Proposal_By_Id(Mod_Admin_dashB BL_data, string Proposal_Id)
        {

            try
            {
                DataSet DB_Proposal= new DataSet();
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_IT_Proposal"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Proposal_by_Id");
                    cmd.Parameters.Add(sqlP_type);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;


                    SqlParameter sql_Proposal_Id = new SqlParameter("@Proposal_Id", Proposal_Id);
                    cmd.Parameters.Add(sql_Proposal_Id);


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        //sda.Fill(DB_Proposal);
                        await Task.Run(() => sda.Fill(DB_Proposal));
                    }
                }


                if (DB_Proposal.Tables[0].Rows.Count > 0)
                {

                    BL_data.Prop_detail.Proposal_Id = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Proposal_Id"]);

                    BL_data.Prop_detail.Utilization_Details = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Utilization_Details"]);

                 
                    BL_data.Prop_detail.Proposal_Type = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Proposal_Type"]);
                    BL_data.Prop_detail.Budget_Head_Type = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Budget_Head_Type"]);
                    BL_data.Prop_detail.PO_File_Id = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["PO_File_Id"]);
                    BL_data.Prop_detail.PO_File_Name = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["PO_File_Name"]);
                    BL_data.Prop_detail.Assets_Info = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Assets_Info"]);
                    BL_data.Prop_detail.Invoice_Info = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Invoice_Info"]);
                    BL_data.Prop_detail.Status = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Completed_Status"]);
                    BL_data.Prop_detail.StatusId = Convert.ToInt32(DB_Proposal.Tables[0].Rows[0]["StatusId"]);
                    BL_data.Prop_detail.NoteLocation = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["NoteLocation"]);

                    BL_data.Prop_detail.Update_UserId = Convert.ToString(DB_Proposal.Tables[0].Rows[0]["Create_User"]);


                    if (BL_data.Prop_detail.Status == "Completed")
                    {
                        BL_data.Prop_detail.NoteLocation = "";
                    }

                }

            }
            catch (Exception ex) { }

            return BL_data.Prop_detail;
        }

        public int Update_proposal_Status(string Proposal_Id, int status, string UserId)
        {
            
            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_IT_Proposal";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", "Update_Status");
                cmd.Parameters.Add(sqlP_type);
 
                SqlParameter SqlProposal_Id = new SqlParameter("@Proposal_Id", Proposal_Id);
                cmd.Parameters.Add(SqlProposal_Id);

                SqlParameter SqlStatus = new SqlParameter("@Status", status);
                cmd.Parameters.Add(SqlStatus);

                SqlParameter SqlUserId = new SqlParameter("@UserId", UserId);
                cmd.Parameters.Add(SqlUserId);


                con.Open();

                status = cmd.ExecuteNonQuery();


            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public List<Grid_Class> Get_Dashboard_Grid(string input, string Type, int PageNo)
        {

            Grid_Class BL_data;
            List<Grid_Class> data = new List<Grid_Class>();

            try
            {
                DataTable dt_Comuter;
                SqlConnection con = new DBConnection().con;

                using (SqlCommand cmd = new SqlCommand("sp_Dashboard_Grid_Data"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", Type);
                    cmd.Parameters.Add(sqlP_type);
                    SqlParameter Proposal_Id = new SqlParameter("@Proposal_Id", input);
                    cmd.Parameters.Add(Proposal_Id);
                    SqlParameter sql_PageNo = new SqlParameter("@Page_No", PageNo);
                    cmd.Parameters.Add(sql_PageNo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    
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
                    BL_data = new Grid_Class();

                    BL_data.Proposal_Id = Convert.ToString(dr["Proposal_Id"]);

                    BL_data.Particular = Convert.ToString(dr["Utilization_Details"]);

                    BL_data.StartDate = Convert.ToDateTime(dr["IT_Initiate_Date"]).ToString().Substring(0,10);

                    BL_data.Status = Convert.ToString(dr["Completed_Status"]);

                    BL_data.NoteLocation = Convert.ToString(dr["NoteLocation"]);
                    if (BL_data.Status == "Completed")
                    {
                        BL_data.NoteLocation = "";
                    }

                    data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return data;
        }


        public List<WorkFlow> GetWorkFlowList(string ProposalId, string UserName)
        {
            List<WorkFlow> data = new List<WorkFlow>();


            DataTable DB_Proposal = new DataTable();

            using (SqlConnection con = new DBConnection().con)
            {
                SqlCommand cmd = new SqlCommand("sp_IT_Proposal");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlP_type = new SqlParameter("@Type", "Get_WorkFlowList");
                cmd.Parameters.Add(sqlP_type);

                SqlParameter sql_Proposal_Id = new SqlParameter("@Proposal_Id", ProposalId);
                cmd.Parameters.Add(sql_Proposal_Id);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    sda.Fill(DB_Proposal);
                }

                int RowCount = DB_Proposal.Rows.Count;
                int RowNumber = 0;
                string User = string.Empty;
                foreach (DataRow dr in DB_Proposal.Rows)
                {
                    WorkFlow flow = new WorkFlow();
                    RowNumber++;
                    flow.WorkFlow_Id = Convert.ToInt32(dr["WorkFlow_ID"]);
                    flow.SendDate = Convert.ToDateTime(dr["SendDate"]);
                    flow.FromDte = Convert.ToString(dr["From_Directorate"]);
                    flow.ToDte = Convert.ToString(dr["To_Directorate"]);
                    flow.File_Id = Convert.ToString(dr["FileID"]);
                    flow.Remarks = Convert.ToString(dr["Remarks"]);

                    User= Convert.ToString(dr["UpdateUser"]);

                    if (RowNumber == RowCount && User== UserName)
                    {
                        flow.LastRow = Convert.ToInt32(dr["WorkFlow_ID"]);
                    }
                    else
                    {
                        flow.LastRow = 0;
                    }

                    data.Add(flow);
                }

                return data;
            }
        }


        public int Add_Delete_WorkFlow(string PropodalId, string UserId, string Type, WorkFlow data, out string FileName)
        {
            int status = 0;
            FileName = "";

            SqlConnection con = new DBConnection().con;
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_IT_Proposal";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", Type);
                cmd.Parameters.Add(sqlP_type);

                SqlParameter Proposal_Id = new SqlParameter("@Proposal_Id", PropodalId);
                cmd.Parameters.Add(Proposal_Id);
                SqlParameter WorkFlow_Id = new SqlParameter("@WorkFlowId", data.WorkFlow_Id);
                cmd.Parameters.Add(WorkFlow_Id);
                if(Type!= "Delete_WorkFlowList")
                {
                    SqlParameter SendDate = new SqlParameter("@SendDate", data.SendDate);
                    cmd.Parameters.Add(SendDate);
                    SqlParameter FromDte = new SqlParameter("@From_Directorate", data.FromDte);
                    cmd.Parameters.Add(FromDte);
                    SqlParameter ToDte = new SqlParameter("@To_Directorate", data.ToDte);
                    cmd.Parameters.Add(ToDte);
                    SqlParameter Remarks = new SqlParameter("@Remarks", data.Remarks);
                    cmd.Parameters.Add(Remarks);
                    if (data.WorkFlow_File != null)
                    {
                        SqlParameter FileExist = new SqlParameter("@FileExist", 1);
                        cmd.Parameters.Add(FileExist);
                    }
                }
                
                SqlParameter Sq1UserId = new SqlParameter("@UserId", UserId);
                cmd.Parameters.Add(Sq1UserId);


                DataTable DB_WorkFlow = new DataTable();

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    sda.Fill(DB_WorkFlow);
                }

                foreach (DataRow dr in DB_WorkFlow.Rows)
                {
                    FileName = Convert.ToString(dr["FileName"]);
                }

                status = 1;

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }
    }
}