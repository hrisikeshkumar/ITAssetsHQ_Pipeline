using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;

namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class Shift_ItemController : Controller
    {
        public ActionResult Shift_Item_Details()
        {
            BL_Shift_Item com = new BL_Shift_Item();

            List<Mod_Shift_Item> pc_List = com.Get_Shift_ItemData();

            return View(pc_List);
        }
    
        [HttpGet]
        public ActionResult Shift_Item_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View();
        }


        
        [HttpPost]
        public ActionResult Shift_Item_Create_Post(Mod_Shift_Item Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Shift_Item save_data = new BL_Shift_Item();
                    int status = save_data.Save_Shift_Item_data(Get_Data, "Add_new", "");

                    if (status == 1)
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

            return RedirectToAction("Create_Item", "Shift_Item");
        }


        
        public ActionResult Edit_Shift_Item(string id)
        {
            BL_Shift_Item Md_Com = new BL_Shift_Item();
            Mod_Shift_Item data = Md_Com.Get_Data_By_ID(id);

            return View( data);
        }


        
        public ActionResult Update_Shift_Item(Mod_Shift_Item Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Shift_Item Md_Asset = new BL_Shift_Item();

                    status = Md_Asset.Save_Shift_Item_data(Get_Data, "Update", Asset_ID);

                    if (status == 1)
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

            return RedirectToAction("Shift_Item_Details", "Shift_Item");
        }


        
        public ActionResult Delete_Shift_Item(Mod_Shift_Item Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Shift_Item Md_Asset = new BL_Shift_Item();

                    status = Md_Asset.Save_Shift_Item_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Shift_Item_Details", "Shift_Item");
        }


    }
}