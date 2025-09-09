using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using IT_Hardware.Infra;

namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITStaffs)]
    [Area("Admin")]
    public class VendorController : Controller
    {
        
        public ActionResult Vendor_Details()
        {
            BL_Vendor com = new BL_Vendor();

            List<Mod_Vendor> pc_List = com.Get_VendorData();

            return View(pc_List);
        }


     
        [HttpGet]
        public ActionResult Vendor_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vendor_Create_Post(Mod_Vendor Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                string Vendor_Id = string.Empty;
                if (ModelState.IsValid)
                {
                    BL_Vendor save_data = new BL_Vendor();
                    int status = save_data.Save_Vendor_data(Get_Data, "Add_new", "", out Vendor_Id);

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

            return RedirectToAction("Vendor_Create_Item", "Vendor");
        }



        public ActionResult Edit_Vendor(string id)
        {
            BL_Vendor Md_Com = new BL_Vendor();
            Mod_Vendor data = Md_Com.Get_Data_By_ID(id);


            return View( data);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_Vendor(Mod_Vendor Get_Data)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Vendor Md_Asset = new BL_Vendor();

                    string out_param = string.Empty;

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Update", Get_Data.Vendor_id, out out_param);

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data have saved successfully");
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

            return RedirectToAction("Vendor_Details", "Vendor");
        }

        public ActionResult Delete_Vendor(Mod_Vendor Get_Data, string id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {


                    BL_Vendor Md_Asset = new BL_Vendor();

                    string out_param = string.Empty;

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Delete", id,  out out_param);

                    if (status >0)
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



        [HttpPost]
        public JsonResult FiliUpload(FormFile postedFile)
        {


            string SLA_Id = Request.Form["SLA_Id"].ToString();

            using (MemoryStream ms = new MemoryStream())
            {
                postedFile.CopyTo(ms);

                using (SqlConnection con = new DBConnection().con)
                {
                    string query = "INSERT INTO tblFiles VALUES (@Name, @ContentType, @Data)";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
                        cmd.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
                        cmd.Parameters.AddWithValue("@Data", ms.ToArray());
                        cmd.Parameters.AddWithValue("@Ref_Id", SLA_Id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }



            return Json(GetFiles_By_Id(SLA_Id));

        }


        private static List<FileModel> GetFiles_By_Id(string SLA_Id)
        {
            List<FileModel> files = new List<FileModel>();
            
            using (SqlConnection con = new DBConnection().con)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT File_Id, File_Name FROM File_table where  LTRIM(RTRIM(File_table))= 'Vendor' and  LTRIM(RTRIM(File_Ref_Id))=LTRIM(RTRIM('" + SLA_Id + "')) "))
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


        public FileResult Download(string fileId)
        {


            byte[] bytes;
            string fileName, contentType;
            
            using (SqlConnection con = new DBConnection().con)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select File_Id, File_table, File_Name, ContentType, File_Data from File_table WHERE LTRIM(RTRIM(File_Id))= LTRIM(RTRIM(@Id))";
                    cmd.Parameters.AddWithValue("@Id", fileId);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["File_Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["File_Name"].ToString();
                    }
                    con.Close();
                }
            }

            return File(bytes, contentType, fileName);
        }
    }
}