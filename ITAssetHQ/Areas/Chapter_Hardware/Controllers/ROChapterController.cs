using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Chapter_Hardware.Data;
using IT_Hardware.Areas.Chapter_Hardware.Models;

namespace IT_Hardware.Areas.Chapter_Hardware.Controllers
{
    [Authorize]
    [Area("Chapter_Hardware")]
    public class ROChapterController : Controller
    {
        public ActionResult Details()
        {
            ROChapter_BL com = new ROChapter_BL();

            List<ROChapter_Mod> Asset_List = com.Get_CompData();

            return View(Asset_List);
        }


        [HttpGet]
        public ActionResult Create_Item(string Message)
        {
            ViewBag.Message = Message;

            ROChapter_Mod Mod_data = new ROChapter_Mod();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");
           // Mod_data.Vendor_Name = Make_List.Vendor_List();

            return View(Mod_data);
        }


        [HttpPost]
        public ActionResult Create_Item(ROChapter_Mod Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_user = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {
                    ROChapter_BL save_data = new ROChapter_BL();
                    int status = save_data.Save_data(Get_Data, "Add_new", "");

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
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("Data is not saved");

            }

            return RedirectToAction("Create_Item", "ROChapter");
        }


        public ActionResult Edit_Item(string id)
        {
            ROChapter_BL BL_data = new ROChapter_BL();
            ROChapter_Mod Model_data = new ROChapter_Mod();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Model_data.Item_Make_id.Trim().ToString());

            return View("~/Areas/Admin/Views/Computer/Com_Edit_Item.cshtml", Model_data);
        }


        
        public ActionResult Update_Item(ROChapter_Mod Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_user = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    ROChapter_BL Md_Asset = new ROChapter_BL();

                    status = Md_Asset.Save_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("Details", "ROChapter");
        }


        public ActionResult Delete_Item(ROChapter_Mod Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    ROChapter_BL Md_Asset = new ROChapter_BL();

                    status = Md_Asset.Save_data(Get_Data, "Delete", Item_id);

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

            return RedirectToAction("Details", "ROChapter");
        }



        //public JsonResult Model_List(string Item_Make)
        //{

        //    Item_MakeModel Make_List = new Item_MakeModel();

        //    ROChapter_Mod Mod_Make = new ROChapter_Mod();

        //    Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Item_Make);

        //    return Json(Mod_Make.Item_Model_List);

        //}



        //public JsonResult Sl_Finder(string Item_SlNo)
        //{

        //    Bl_Serial_Check Find_Sl = new Bl_Serial_Check();

        //    return Json(Find_Sl.Find_Sl(Item_SlNo), .AllowGet);

        //}



    }
}
