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
    [Authorize(Policy = AuthorizationPolicies.ITStaffs)]
    [Area("Admin")]
    public class InvoiceController : Controller
    {
        private IHostingEnvironment Environment;

        public InvoiceController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        public ActionResult List_Invoice(string Message)
        {
            Invoice_BL objPO = new Invoice_BL();
            List<Invoice_Mod> Inv_List = objPO.Get_All_Invoice(HttpContext.User.Identity.Name.ToString());

            return View(Inv_List);
        }

        public ActionResult Create_Invoice()
        {
            Invoice_Mod mod_PO = new Invoice_Mod();
            Invoice_BL Inv_Data = new Invoice_BL();
            mod_PO.PO_list = Inv_Data.PO_List(HttpContext.User.Identity.Name.ToString());
            mod_PO.Invoice_Date = DateTime.Today;
            return View( mod_PO);
        }

        [HttpPost]
        public ActionResult Invoice_Create_Post(Invoice_Mod Data)
        {
            string Message = "";
            try
            {

                if (Data.File_Invoice != null)
                {
                    if (Path.GetExtension(Data.File_Invoice.FileName) != ".pdf")
                    {
                        TempData["Message"] = String.Format("Only PDF files are accepted");
                        return RedirectToAction("PO_Create_Item");
                    }
                }

                Data.Create_usr_id = HttpContext.User.Identity.Name;
                string Vendor_Id = string.Empty;
                if (ModelState.IsValid)
                {

                    Invoice_BL save_data = new Invoice_BL();

                    int status = 0;

                    if (Data.File_Invoice != null)
                    {
                        Data.FileName_Invoice = "Y";
                        status = save_data.Save_data(Data, "Add_new", out string Inv_Id);

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/Invoice/");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        //get file extension
                        FileInfo fileInfo = new FileInfo(Data.File_Invoice.FileName);
                        string fileName = Inv_Id ;

                        string fileNameWithPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            Data.File_Invoice.CopyTo(stream);
                        }
                    }
                    else
                    {
                        Data.FileName_Invoice = "N";
                        status = save_data.Save_data(Data, "Add_new", out string Inv_Id);
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

            return RedirectToAction("Create_Invoice", "Invoice");
        }

        [HttpGet]
        public ActionResult Edit_Invoice(string id)
        {      
            Invoice_BL Inv_Data = new Invoice_BL();
            Invoice_Mod mod_Inv= Inv_Data.Get_Data_By_ID(id, HttpContext.User.Identity.Name.ToString());
            mod_Inv.PO_list = Inv_Data.PO_List(HttpContext.User.Identity.Name.ToString());
            return View(mod_Inv);
        }


        [HttpPost]
        public ActionResult Update_Invoice(Invoice_Mod Get_Data)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    Invoice_BL Md_Asset = new Invoice_BL();

                    string out_param = string.Empty;

                    //status = Md_Asset.Save_data(Get_Data, "Update", Get_Data., out out_param);

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

            return RedirectToAction("List_Invoice", "Invoice");
        }


        public ActionResult Delete_Invoice(Invoice_Mod Get_Data, string id)
        {
            int status = 0;
            try
            {

                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {

                    Invoice_BL Md_Asset = new Invoice_BL();

                    string out_param = string.Empty;

                    //---------status = Md_Asset.Save_Vendor_data(Get_Data, "Delete", id, out out_param);

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

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Files\\HQ\\Invoice\\");

            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName );

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);

        }




        public JsonResult ReplaceFile(IFormFile file, string FileType, string fileName)
        {
            string message = "Error Occoured";
           
            if (file != null)
            {
                try
                {
                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    string path = Path.Combine(this.Environment.WebRootPath, "wwwroot\\Files\\HQ\\Invoice\\");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    message = "File Replaced Successfully";
                }
                catch (Exception ex) { }
            }

            return Json(message);
        }

    }
}