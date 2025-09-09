using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using System.Data.SqlClient;
using IT_Hardware.Infra;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy =   AuthorizationPolicies.AllAccess)]
    [Area("Admin")]
    public class Admin_DashboardController : Controller
    {

        private IHostingEnvironment Environment;

        public Admin_DashboardController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public ActionResult Admin_Dashboard(string Id, string type)
        {

            if (Id is null)
                Id = string.Empty;

            if (type is null)
                type = string.Empty;

            BL_Admin_DashB B_Layer = new BL_Admin_DashB();

            Mod_Admin_dashB mod_Data = new Mod_Admin_dashB();

            mod_Data.Prop_detail = new Proposal_details();

            //Proposal
            mod_Data.List_Proposal = B_Layer.Get_List_Proposal();

            /* ---------------------Invoices  ------------------------------*/
            
            string sqlTpye = string.Empty;
            if (Id != string.Empty && type != string.Empty)
            {
                if (type=="PO")
                {
                    sqlTpye = "Get_Bill_by_PO";
                    ViewBag.Filter = "Yes";
                }
                else
                {
                    sqlTpye = "Get_Bill_by_Vender";
                    ViewBag.Filter = "Yes";
                } 
            }
            else
            {
                sqlTpye = "Get_Bill_List";
                ViewBag.Filter = "No";
            }


            string PO_No = string.Empty;
            mod_Data.List_Bill_Process = B_Layer.Get_List_Bills(Id, sqlTpye, out PO_No);

            /* ---------------------- Invoices ------------------------------*/

            if (type == "PO")
            {
                ViewBag.PO_No = PO_No;
            }
           
                
            return View(mod_Data);
        }


        public async Task<JsonResult> Get_Proposal_Detail_for_Modal( string Proposal_Id)
        {
            BL_Admin_DashB B_Layer = new BL_Admin_DashB();
            Mod_Admin_dashB mod_Data = new Mod_Admin_dashB();
            mod_Data.Prop_detail = new Proposal_details();


            // Other Information
            mod_Data.Prop_detail = await B_Layer.Get_Proposal_By_Id(mod_Data, Proposal_Id);

            // WorkFlow Details
            mod_Data.Prop_detail.WorkFlowList = B_Layer.GetWorkFlowList(Proposal_Id, HttpContext.User.Identity.Name.ToString().Trim());

         
            //Final Approval Files
            mod_Data.Prop_detail.Prop_Files = GetFinalApprovalFiles_By_Id(Proposal_Id);

            return Json(mod_Data);
        }

        
        //-----------------------------------         Proposal Details       --------------------------------------------------
        public async Task<ActionResult> Edit_proposal(Mod_Admin_dashB Proposal)
        {
            Mod_Admin_dashB mod_Data = new Mod_Admin_dashB();
            BL_Admin_DashB B_Layer = new BL_Admin_DashB();
            mod_Data.Prop_detail = new Proposal_details();

            //ID
            mod_Data.Prop_detail.Proposal_Id = Proposal.Prop_detail.Proposal_Id;

            //Other Information
            mod_Data.Prop_detail = await B_Layer.Get_Proposal_By_Id(mod_Data, Proposal.Prop_detail.Proposal_Id);

            if (HttpContext.User.Identity.Name.ToUpper().Trim() == mod_Data.Prop_detail.Update_UserId.ToUpper().Trim())
            {
                ViewBag.SameUser = "Yes";
            }
            else
            {
                ViewBag.SameUser = "No";
            }

                // WorkFlow Details
                mod_Data.Prop_detail.WorkFlowList = B_Layer.GetWorkFlowList(Proposal.Prop_detail.Proposal_Id, HttpContext.User.Identity.Name.ToString().Trim());

            //Final Approval Files
            mod_Data.Prop_detail.Prop_Files = GetFinalApprovalFiles_By_Id(Proposal.Prop_detail.Proposal_Id);

           
            mod_Data.Prop_detail.Department_List = new ItemInfo_BL().DepartmentList();
            mod_Data.Prop_detail.Status_List = new ItemInfo_BL().StatusList();
            mod_Data.Prop_detail.Work_Flow = new WorkFlow();
            mod_Data.Prop_detail.Work_Flow.SendDate = DateTime.Today;

            return View(mod_Data.Prop_detail);
        }

        
        public ActionResult Update_proposal(Proposal_details Proposal_data)
        {

            int status = 0;
            try
            {
               
                if (ModelState.IsValid)
                {
                    BL_Admin_DashB B_Layer = new BL_Admin_DashB();


                    Proposal_data.Update_UserId = HttpContext.User.Identity.Name;

                    //status = B_Layer.Update_proposal(Proposal_data);

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data have saved successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("Data is not saved");

            }

            return RedirectToAction("Admin_Dashboard", "Admin_Dashboard");
        }


        [RequestSizeLimit(100 * 1024 * 1024)]
        public JsonResult AddWorkFlow(string ProposalId, WorkFlow data)
        {
            BL_Admin_DashB mod = new BL_Admin_DashB();


            if (data.WorkFlow_File != null)
            {
                string ext = System.IO.Path.GetExtension(data.WorkFlow_File.FileName);
                if (ext != ".pdf")
                {

                    return Json(mod.GetWorkFlowList(ProposalId, HttpContext.User.Identity.Name.ToString().Trim()));
                }
            }
            try
            {
                string FileName = string.Empty;
                mod.Add_Delete_WorkFlow(ProposalId, HttpContext.User.Identity.Name, "Add_WorkFlowList",  data, out FileName);

                if (data.WorkFlow_File != null)
                {
                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    string path = Path.Combine(this.Environment.WebRootPath, "Files\\WorkFlowFiles\\");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream stream = new FileStream(Path.Combine(path, FileName), FileMode.Create))
                    {
                        data.WorkFlow_File.CopyTo(stream);
                    }

                }

            }
            catch (Exception ex) { }

            return Json(mod.GetWorkFlowList(ProposalId, HttpContext.User.Identity.Name.ToString().Trim()));
        }

        public JsonResult DeleteWorkFlow(int WorkFlow_Id, string ProposalId)
        {
            BL_Admin_DashB mod = new BL_Admin_DashB();

            WorkFlow data = new WorkFlow();
            data.WorkFlow_Id = WorkFlow_Id;

            try
            {
                string FileName = string.Empty;
                mod.Add_Delete_WorkFlow(ProposalId, HttpContext.User.Identity.Name, "Delete_WorkFlowList", data, out FileName);

            }
            catch (Exception ex) { }

            return Json(mod.GetWorkFlowList(ProposalId, HttpContext.User.Identity.Name.ToString().Trim()));
        }


        private static List<File_List> GetFinalApprovalFiles_By_Id(string Proposal_Id)
        {
            List<File_List> files = new List<File_List>();

            try
            {
                using (SqlConnection con = new DBConnection().con)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT  Proposal_File_ID, Proposal_Id, FileName from Proposal_Files where  LTRIM(RTRIM(Proposal_Id))=LTRIM(RTRIM('" + Proposal_Id + "')) "))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                files.Add(new File_List
                                {
                                    File_Id = Convert.ToString(sdr["FileName"]),
                                    File_Name = Convert.ToString(sdr["FileName"])
                                });
                            }
                        }
                        con.Close();
                    }
                }

            }
            catch (Exception ex) { }

            return files;
        }


        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public JsonResult Upload_FinalApprovalFile()
        {
            string Proposal_Id = Request.Form["Proposal_Id"].ToString();
            IFormFile postedFile = Request.Form.Files[0];
            string ext = System.IO.Path.GetExtension(postedFile.FileName);
            if (ext != ".pdf") 
            {
                return Json(new SelectListItem("Duplicate", "Accept Pdf Files only"));
            }
            
            try
            {              
                if (postedFile != null)
                {
                    int FileId=0;
                    string FileName = string.Empty;
                    using (SqlConnection con = new DBConnection().con)
                    {
                        //string query = " Select Proposal_File_ID, Proposal_Id, FileName from Proposal_Files where LTRIM(RTRIM(FileName))= LTRIM(RTRIM(@FileName))";

                        string query = " select NEWID() Filename";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            //cmd.Parameters.AddWithValue("@FileName", Path.GetFileName(postedFile.FileName));
                            con.Open();

                            SqlDataReader sdr = cmd.ExecuteReader();

                            
                            //Looping through each record
                            while (sdr.Read())
                            {
                                FileName = sdr["Filename"].ToString().Trim() +".pdf";
                            }

                            sdr.Close();

                            query = " insert into Proposal_Files (Proposal_Id, FileName, Updated_By , Update_Time) " +
                                          " values (@ProposalID, @FileName, @UserId, @Datetime ); SELECT SCOPE_IDENTITY(); ";

                            cmd.Parameters.Clear();
                            cmd.CommandText = query;
                            cmd.Parameters.AddWithValue("@ProposalID", Proposal_Id);
                            cmd.Parameters.AddWithValue("@FileName", FileName);
                            cmd.Parameters.AddWithValue("@UserId", HttpContext.User.Identity.Name.ToString());
                            cmd.Parameters.AddWithValue("@Datetime", DateTime.Now);

                            FileId = Convert.ToInt32(cmd.ExecuteScalar());

                            con.Close();

                        }
                    }

                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    string path = Path.Combine(this.Environment.WebRootPath, "Files\\FinalApproval\\");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }


                    using (FileStream stream = new FileStream(Path.Combine(path, FileName.ToString()), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }


                }

            }
            catch (Exception ex) { }

            return Json(GetFinalApprovalFiles_By_Id(Proposal_Id));
        }


        public JsonResult DeleteFile_FinalApproval(string FileId, string RefId)
        {
            try
            {
                
                using (SqlConnection con = new DBConnection().con)
                {
                    string query = "delete from Proposal_Files where LTRIM(RTRIM(FileName)) =LTRIM(RTRIM(@File_Id))";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@File_Id", FileId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                string file_name = FileId + ".pdf" ;
                string path = Path.Combine(this.Environment.WebRootPath, "Files\\FinalApproval\\", file_name);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }
              
            }catch (Exception ex) { }   

            return Json(GetFinalApprovalFiles_By_Id(RefId));
        }

        public ContentResult Download_FinalApprovalFile(string fileName)
        {

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Files\\FinalApproval\\");

            string file_name = fileName ;
            //Read the File as Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path + file_name);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);         
        }

        public ContentResult Download_WorkFlowFile(string fileName)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Files\\WorkFlowFiles\\");

            //Read the File as Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }

        public JsonResult GridData( string Input, string Type, int Page_No)
        {
            BL_Admin_DashB B_Layer = new BL_Admin_DashB();

            string dataType = string.Empty;

            if (Type == "PODetail")
                dataType = "Get_Invoice_By_PO";
            else if (Type == "Get_By_Proposal")
                dataType = "Get_Invoice_By_ProposalId";
            else if (Type == "Get_Proposal")
                dataType = "Get_Proposal_By_Subject";
            else if (Type == "Get_Invoice")
                dataType = "Get_Invoice_By_Subject";
            else if (Type == "Budget")
                dataType = "Paging_Budget";
            else
                dataType = "Paging_Invoice";

            return Json(B_Layer.Get_Dashboard_Grid(Input, dataType, Page_No));
        }

        public JsonResult SaveStatus(string Proposal_Id, int Status )
        {
            BL_Admin_DashB B_Layer = new BL_Admin_DashB();

            string dataType = string.Empty;
          
            return Json(B_Layer.Update_proposal_Status(Proposal_Id, Status, HttpContext.User.Identity.Name.ToString()));
        }


        /* Own Authentication
        //protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        //{
        //    if (filterContext.HttpContext.Request.IsAuthenticated)
        //    {

        //        if (filterContext.Result is HttpUnauthorizedResult)
        //        {
        //            filterContext.Result = new RedirectResult("~/Authorization/AccessDedied");
        //        }
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectResult("~/Log_In/Log_In");
        //    }
        //} */

    }
}