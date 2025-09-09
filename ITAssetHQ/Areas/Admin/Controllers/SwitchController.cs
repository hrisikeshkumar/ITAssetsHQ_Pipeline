using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;


namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class SwitchController : Controller
    {

        public ActionResult Switch_Details()
        {
            BL_Switch com = new BL_Switch();

            List<Mod_Switch> pc_List = com.Get_SwitchData();

            return View(pc_List);
        }


        
        [HttpGet]
        public ActionResult Switch_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Switch Mod_data = new Mod_Switch();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Switch", "MAKE", "");
            Mod_data.PO_List = Make_List.Vendor_and_PO_List("PO");
            Mod_data.Proc_date = DateTime.Now;
            Mod_data.WrntEnd_Date = DateTime.Now;

            return View(Mod_data);

        }


        
        [HttpPost]
        public ActionResult Switch_CreateItem_Post(Mod_Switch Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Switch save_data = new BL_Switch();
                    int status = save_data.Save_Switch_data(Get_Data, "Add_new", "");

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
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

            return RedirectToAction("Switch_Create_Item", "Switch");
        }


        
        public ActionResult Edit_Switch(string id)
        {
           
            BL_Switch BL_data = new BL_Switch();
            Mod_Switch Model_data = new Mod_Switch();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Switch", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Switch", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View( Model_data);
        }


        
        public ActionResult Update_Switch(Mod_Switch Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Switch Md_Asset = new BL_Switch();

                    status = Md_Asset.Save_Switch_data(Get_Data, "Update", Item_id);

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
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

            return RedirectToAction("Switch_Details", "Switch");
        }


        
        public ActionResult Delete_Switch(Mod_Switch Get_Data, string id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {


                    BL_Switch Md_Asset = new BL_Switch();

                    status = Md_Asset.Save_Switch_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Switch_Details", "Switch");
        }


        
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Switch", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List);

        }



    }
}