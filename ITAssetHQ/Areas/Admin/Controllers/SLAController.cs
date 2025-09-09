using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using System.Data.SqlClient;
using IT_Hardware.Infra;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;

namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITStaffs)]
    [Area("Admin")]
    public class SLAController : Controller
    {

        private IHostingEnvironment Environment;

        public SLAController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }


        public ActionResult SLA_Details()
        {
            BL_SLA com = new BL_SLA();

            List<Mod_SLA> SLA_List = com.Get_SLAData();

            return View(SLA_List);
        } 

        [HttpGet]
        public ActionResult SLA_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Invoice_BL Inv_Data = new Invoice_BL();
            Mod_SLA Mod_data = new Mod_SLA();
            Mod_data.PO_List = Inv_Data.PO_List(HttpContext.User.Identity.Name.ToString());
            Mod_data.Expiry_DT = DateTime.Now;
            Mod_data.Service_ST_DT = DateTime.Now;

            return View( Mod_data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SLA_CreateItem_Post(Mod_SLA Data)
        {

            string Message = "";
            try
            {

                if (Data.SLA_File != null)
                {
                    if (Path.GetExtension(Data.SLA_File.FileName) != ".pdf")
                    {
                        TempData["Message"] = String.Format("Only PDF files are accepted");
                        return RedirectToAction("PO_Create_Item");
                    }
                }

                Data.Create_usr_id = HttpContext.User.Identity.Name;
                string Vendor_Id = string.Empty;
                if (ModelState.IsValid)
                {

                    BL_SLA save_data = new BL_SLA();

                    int status = 0;

                    if (Data.SLA_File != null)
                    {
                        status = save_data.Save_data(Data, "Add_new");

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/SLA/");

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        string fileNameWithPath = Path.Combine(path, Data.SLA_File.FileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            Data.SLA_File.CopyTo(stream);
                        }
                    }
                    else
                    {
                        status = save_data.Save_data(Data, "Add_new");
                    }

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


            return RedirectToAction("SLA_Create_Item", "SLA");
        }


        [HttpGet]
        public ActionResult Edit_SLA(string id)
        {
            BL_SLA BL_data = new BL_SLA();

            Mod_SLA Mod_data = new Mod_SLA();
            BL_data.Get_Data_By_ID(Mod_data, id);
            Invoice_BL Inv_Data = new Invoice_BL();
            Mod_data.PO_List = Inv_Data.PO_List(HttpContext.User.Identity.Name);

            return View( Mod_data);
        }


        [HttpPost]
        public ActionResult Update_SLA(Mod_SLA Get_Data)
        {
            int status = 0;
            string SLA_Id_I = string.Empty;
            string SLA_FileName = string.Empty;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_SLA save_data = new BL_SLA();
                    if (Get_Data.SLA_File != null)
                    {
                        FileInfo fileInfo = new FileInfo(Get_Data.SLA_File.FileName);
                        Get_Data.SLA_File_Name = fileInfo.Extension;
                        status = save_data.Save_data(Get_Data, "Update");

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/SLA");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        //get file extension

                        string fileName = SLA_FileName ;

                        string fileNameWithPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            Get_Data.SLA_File.CopyTo(stream);
                        }
                    }
                    else
                    {
                        status = save_data.Save_data(Get_Data, "Update");
                    }

                    TempData["Message"] = String.Format("Data saved successfully");

                }
                else
                {
                    TempData["Message"] = String.Format("Required data are not provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("Data is not saved");

            }

            return RedirectToAction("Edit_SLA", "SLA" , new { id = Get_Data.SLA_Id });
        }


        public ActionResult Delete_SLA(Mod_SLA Get_Data, string id)
        {
           
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    string SLA_Id = string.Empty;
                    string SLA_FileName = string.Empty;

                    BL_SLA Md_Asset = new BL_SLA();

                    int status = Md_Asset.Save_data(Get_Data, "Delete");

                    if (status>0)
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

            return RedirectToAction("SLA_Details", "SLA");
        }



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
        //}




        //-----------------------------------  File Function ---------------------------------------------



        [HttpPost]
        public JsonResult FiliUpload(string SLA_Id)
        {

          
            string SLA_Id1 = SLA_Id.ToString();
            IFormFile postedFile = Request.Form.Files[0];
            if (postedFile.Length > 0)
            {

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/SLA");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(postedFile.FileName);
                string fileName = SLA_Id + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
            }

            return Json(GetFiles_By_Id(""));

        }



        //private static List<FileModel> GetFiles()
        //{
        //    List<FileModel> files = new List<FileModel>();
        //    
        //    using (SqlConnection con = new DBConnection().con)
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SELECT File_Id, File_Name FROM File_table"))
        //        {
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    files.Add(new FileModel
        //                    {
        //                        File_Id = Convert.ToString(sdr["File_Id"]),
        //                        File_Name = Convert.ToString(sdr["File_Name"])
        //                    }) ;
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //    return files;
        //}



        private static List<FileModel> GetFiles_By_Id(string SLA_Id)
        {
            List<FileModel> files = new List<FileModel>();
            
            using (SqlConnection con = new DBConnection().con)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT File_Id, File_Name FROM File_table where  LTRIM(RTRIM(File_table))= 'SLA' and  LTRIM(RTRIM(File_Ref_Id))=LTRIM(RTRIM('" + SLA_Id + "')) "))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new FileModel
                            {
                                File_Id = Convert.ToString(sdr["File_Id"]),
                                File_Name = Convert.ToString(sdr["File_Name"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return files;
        }


        public JsonResult DeleteFile(string FileId, string RefId)
        {

            
            using (SqlConnection con = new DBConnection().con)
            {
                string query = "delete from File_table where LTRIM(RTRIM(File_Id)) =LTRIM(RTRIM(@File_Id))";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@File_Id", FileId);               
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            return Json(GetFiles_By_Id(RefId));

        }

        public ContentResult Download(string fileName)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Files\\HQ\\SLA\\");

            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }




    }
}