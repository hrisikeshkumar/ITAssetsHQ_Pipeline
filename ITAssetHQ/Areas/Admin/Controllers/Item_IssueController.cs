using DocumentFormat.OpenXml.Office2010.Excel;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class Item_IssueController : Controller
    {

        private IHostingEnvironment Environment;

        public Item_IssueController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public ActionResult Item_Issue_Details(string Id, string type)
        {

            if (Id is null)
                Id = string.Empty;

            if (type is null)
                type = string.Empty;

            ItemIssue_Mod model = new ItemIssue_Mod();

            BL_Item_Issue item = new BL_Item_Issue();


            string sqlTpye = string.Empty;
            if (Id != string.Empty && type != string.Empty)
            {
                if (type == "PO")
                {
                    model.Item_Issues = item.Get_Item_By_Sl(Id, "POID_Wise");
                }
                else
                {
                    model.Item_Issues = item.Get_Item_By_Sl(Id, "Vender_Wise");
                }
            }
            else
            {
                model.Item_Issues = item.Get_Item_IssueData();
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult Item_Issue_Create_Item(string Message, string AssetId )
        {
            ViewBag.Message = Message;

            if (AssetId is null)
            {
                AssetId = string.Empty;
            }

            Mod_Item_Issue Mod_data;
            

            if (AssetId == string.Empty)
            {
                Mod_data = new Mod_Item_Issue();
            }
            else 
            {
                 Mod_data = new BL_Item_Issue().Get_Data_By_ID(AssetId);
            }

            Mod_data.Issued_date = DateTime.Now;

            return View(Mod_data);
        }

        [HttpPost]
        public ActionResult Item_Issue_Create_Post(Mod_Item_Issue Get_Data)
        {


            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    int File_Exist = 0;
                    if (Get_Data.Issue_File != null)
                    {
                        File_Exist = 1;
                        if (Path.GetExtension(Get_Data.Issue_File.FileName) != ".pdf")
                        {
                            TempData["Message"] = String.Format("Only PDF files are accepted");
                            return RedirectToAction("Item_Issue_Create_Item");
                        }
                    }

                    BL_Item_Issue save_data = new BL_Item_Issue();
                    string File_Id= string.Empty;
                    

                    int status = save_data.Save_Item_Issue_data(Get_Data, "Add_new", "", File_Exist,  out File_Id);

                    if (status >= 0)
                    {

                        if (File_Exist>0)
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/ItemIssue/");

                            //create folder if not exist
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            //get file extension
                            FileInfo fileInfo = new FileInfo(Get_Data.Issue_File.FileName);
                            string fileName = File_Id;

                            string fileNameWithPath = Path.Combine(path, fileName);

                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                Get_Data.Issue_File.CopyTo(stream);
                            }
                        }                       
                    }

                    if (status < 1)
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data save successfully");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required field are not provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("Data is not saved");

            }

            return RedirectToAction("Item_Issue_Create_Item", "Item_Issue");
        }
     
        //public ActionResult Edit_Item_Issue(string id)
        //{
        //    BL_Item_Issue Md_Com = new BL_Item_Issue();
        //    Mod_Item_Issue data = Md_Com.Get_Data_By_ID(id);

        //    return View( data);
        //}
       
        public ActionResult Update_Item_Issue(Mod_Item_Issue Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    BL_Item_Issue Md_Asset = new BL_Item_Issue();

                    string fileId = string.Empty;
                    status = Md_Asset.Save_Item_Issue_data(Get_Data, "Update", Asset_ID,0, out fileId);

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

            return RedirectToAction("Item_Issue_Details", "Item_Issue");
        }
    
        public ActionResult Delete_Item_Issue(Mod_Item_Issue Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Item_Issue Md_Asset = new BL_Item_Issue();

                    //status = Md_Asset.Save_Item_Issue_data(Get_Data, "Delete", id);

                    if (status == 1)
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

            return RedirectToAction("Item_Issue_Details", "Item_Issue");
        }

        [HttpPost]
        public JsonResult AutoComplete(string SL_No)
        {

            BL_Item_Issue data = new BL_Item_Issue();
            List<Item_SL_Wise> list = data.Item_SLnumber_List(SL_No);


            return Json(list);
        }

        [HttpPost]
        public JsonResult Find_Item_Issue(string Item_Id)
        {

            Mod_Item_Issue_Employee Emp_Details= new Mod_Item_Issue_Employee();

            BL_Item_Issue Item_data = new BL_Item_Issue();

            Emp_Details = Item_data.Issue_Employee(Emp_Details, Item_Id, "Item_Issue");

            return Json(Emp_Details);

        }
       
        [HttpPost]
        public JsonResult AutoComplete_TransferEmployee(string EmpID)
        {

            BL_Item_Issue data = new BL_Item_Issue();
            List<Mod_Item_Issue_Employee> list = data.Emp_List(EmpID);

            return Json(list);
        }

        public JsonResult Search_Item(string SearchVal, string SearchType)
        {

            BL_Item_Issue com = new BL_Item_Issue();

            return Json(com.Get_Item_By_Sl( SearchVal,  SearchType));

        }

        public ContentResult Download(string fileName)
        {

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Files\\HQ\\ItemIssue\\");

            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);

        }



    }
}