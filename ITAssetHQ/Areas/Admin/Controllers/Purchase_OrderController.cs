using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using DocumentFormat.OpenXml.EMMA;
using IT_Hardware.Infra;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;

namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy = AuthorizationPolicies.ITStaffs)]
    [Area("Admin")]
    public class Purchase_OrderController : Controller
    {

        private IHostingEnvironment Environment;

        public Purchase_OrderController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        public ActionResult PO_Details(string Message, string Vender_Id)
        {
            if (Vender_Id is null)
                Vender_Id = string.Empty;

            BL_Porder objPO = new BL_Porder();

            string sqlTpye = string.Empty;
            if (Vender_Id != string.Empty)
            {
                sqlTpye = "Get_PO_by_Vender";
                ViewBag.FilterBy_Vender = "Yes";
            }
            else
            {
                sqlTpye = "Get_All_PO";
                ViewBag.FilterBy_Vender = "No";
            }

            List<Mod_POrder> pc_List = objPO.Get_All_PO_Data(sqlTpye, Vender_Id);

            return View( pc_List);
        }

        
        public ActionResult PO_Create_Item()
        {
            Mod_POrder mod_PO = new Mod_POrder();
            BL_Porder com = new BL_Porder();
            mod_PO.Vendor_List = com.Vendor_List();
            mod_PO.PO_Date=DateTime.Now;
            mod_PO.PO_End_Date = DateTime.Now;
            mod_PO.PO_ST_Date = DateTime.Now;


            return View( mod_PO);
        }

       
        public ActionResult PO_Create_Post(Mod_POrder PO_Data)
        {
            string Message = "";
            try
            {

                if (PO_Data.File_PO !=null)
                {
                    if (Path.GetExtension(PO_Data.File_PO.FileName) != ".pdf")
                    {
                        TempData["Message"] = String.Format("Only PDF files are accepted");
                        return RedirectToAction("PO_Create_Item");
                    }
                }

                PO_Data.Create_usr_id = HttpContext.User.Identity.Name;
                string Vendor_Id = string.Empty;
                if (ModelState.IsValid)
                {

                    BL_Porder save_data = new BL_Porder();

                    int status = 0;

                    if (PO_Data.File_PO != null)
                    {
                        
                        PO_Data.PO_File_Name = "Y";
                        status = save_data.Save_PO_data(PO_Data, "Add_new", "", out string PO_Id, out string PO_File_Name);

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/PO/");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        //get file extension
                        FileInfo fileInfo = new FileInfo(PO_Data.File_PO.FileName);
                        string fileName = PO_File_Name;

                        string fileNameWithPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            PO_Data.File_PO.CopyTo(stream);
                        }
                    }
                    else 
                    {
                        PO_Data.PO_File_Name = "N";
                        status = save_data.Save_PO_data(PO_Data, "Add_new", "", out string PO_Id, out string PO_File_Name);
                    }

                    if (status>=0)
                    {
                        TempData["Message"] = String.Format("Data save successfully");
                    }
                    else
                    {

                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("Data is not saved");

            }

            return RedirectToAction("PO_Create_Item", "Purchase_Order");
        }


        public ActionResult Edit_PO(string id)
        { 
            BL_Porder data = new BL_Porder();
            Mod_POrder mod_PO = data.Get_Data_By_ID( id);
            mod_PO.Vendor_List = data.Vendor_List();
            mod_PO.ApprovalList = data.GET_PO_Approval(id);


            if (HttpContext.User.Identity.Name.ToUpper().Trim() == mod_PO.Create_usr_id.ToUpper().Trim())
            {
                ViewBag.SameUser = "Yes";
            }
            else
            {
                ViewBag.SameUser = "No";
            }


            return View( mod_PO);
        }


        [HttpPost]
        public ActionResult Update_PO(Mod_POrder PO_Data)
        {
            string Message = "";
            try
            {
                PO_Data.Create_usr_id = HttpContext.User.Identity.Name;
                string Vendor_Id = string.Empty;
                if (ModelState.IsValid)
                {
                    BL_Porder save_data = new BL_Porder();

                    int status = 0;

                    PO_Data.PO_File_Name = "N";
                    status = save_data.Save_PO_data(PO_Data, "Update", "", out string PO_Id, out string PO_File_Name);
                                         
                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data save successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("Data is not saved");

            }

            return RedirectToAction("Edit_PO", "Purchase_Order", new { id= PO_Data.PO_id });
        }


        [HttpPost]
        public JsonResult Upload_POFile()
        {
            string PO_Id = Request.Form["PO_Id"].ToString();
            string PO_FileId = Request.Form["PO_File_Id"].ToString();
            IFormFile postedFile = Request.Form.Files[0];
            string ext = System.IO.Path.GetExtension(postedFile.FileName);
            string PO_FileName = postedFile.FileName;
            PO_FileId=PO_FileId.Replace("/","-");
            PO_FileId=PO_FileId.Replace("\\", "-");
            Dictionary<string, string> retval = new Dictionary<string, string>();
            

            if (ext != ".pdf")
            {
                retval.Add("status", "0");             
                return Json(retval);
            }

            try
            {
                if (postedFile != null)
                {
                    int status = 0;
                    using (SqlConnection con = new DBConnection().con)
                    {
                        string query = "update PO_Table set PO_File_Id=@PO_FileId , PO_File_Name=@PO_FileName , User_Id=@UserId, Create_Date=getdate() where LTRIM(RTRIM(PO_id))=@PO_Id";
                        SqlCommand cmd = new SqlCommand(query);

                        cmd.Connection = con;
                           
                        con.Open();


                        cmd.Parameters.Clear();
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@PO_FileId", PO_FileId);
                        cmd.Parameters.AddWithValue("@PO_FileName", PO_FileName);
                        cmd.Parameters.AddWithValue("@PO_Id", PO_Id.Trim());
                        cmd.Parameters.AddWithValue("@UserId", HttpContext.User.Identity.Name.ToString());

                        status = Convert.ToInt32(cmd.ExecuteNonQuery());


                        con.Close();
                    }

                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/PO/");
                   
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream stream = new FileStream(Path.Combine(path, PO_FileId.ToString()), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }


                    if (status < 1)
                    {
                        retval.Add("status", "0");
                    }
                    else { retval.Add("status", "1"); }

                  
                }

            }
            catch (Exception ex) {
                retval.Add("status", "-1");
            }

            return Json(retval);
        }


        public ActionResult Delete_PO(Mod_Vendor Get_Data, string id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {


                    BL_Vendor Md_Asset = new BL_Vendor();

                    string out_param = string.Empty;

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Delete", id, out out_param);

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
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

            return RedirectToAction("Vendor_Details", "Vendor");
        }


        public ContentResult Download(string fileName)
        {

            fileName = fileName.Replace("/", "-");
            fileName = fileName.Replace("\\", "-");

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Files\\HQ\\PO\\");

            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName );

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);

        }


        public JsonResult AutoComplete(string SL_No)
        {

            BL_Porder data = new BL_Porder();
            List<Item_SL_Wise> list = data.Approval_List(SL_No);


            return Json(list);
        }

    }
}