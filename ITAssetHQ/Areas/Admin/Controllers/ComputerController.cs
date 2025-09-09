using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class ComputerController : Controller
    {
        
        public ActionResult Com_Details()
        {
            string userName = HttpContext.User.Identity.Name; 
            BL_Computer com= new BL_Computer();

            List<Mod_Computer> pc_List = com.Get_CompData();

            return View( pc_List);
        }

        [HttpGet]
        public ActionResult Com_Create_Item(string Message)
        {
            if (Message != string.Empty)
            {
                ViewBag.Message = Message;
            }
            Mod_Computer Mod_data = new Mod_Computer();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE","");
            Mod_data.PO_List = Make_List.Vendor_and_PO_List("PO");
            Mod_data.Proc_date = DateTime.Now;
            Mod_data.WrntEnd_Date = DateTime.Now;

            return View( Mod_data);
        }

        
        [HttpPost]
        public ActionResult Create_Item_Post(Mod_Computer Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {
                    BL_Computer save_data = new BL_Computer();
                    int status = save_data.Save_Computer_data(Get_Data, "Add_new", "");

                    if (status < 1)
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                    else {

                        TempData["Message"] = String.Format("Data save successfully");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex) {

                TempData["Message"] = string.Format("Data is not saved");
                
            }
            
            return RedirectToAction("Com_Create_Item", "Computer");
        }


        
        public ActionResult Com_Edit_Item(string id)
        {
            BL_Computer BL_data = new BL_Computer();
            Mod_Computer Model_data = new Mod_Computer();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);
            
            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Model_data.Item_Make_id.Trim().ToString());

            return View( Model_data);
        }


        
        public ActionResult Update_Computer(Mod_Computer Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Computer Md_Asset = new BL_Computer();

                    status = Md_Asset.Save_Computer_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("Com_Details", "Computer");
        }


        
        public ActionResult Delete_Item(Mod_Computer Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Computer Md_Asset = new BL_Computer();

                    status = Md_Asset.Save_Computer_data(Get_Data, "Delete", Item_id);

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

            return RedirectToAction("Com_Details", "Computer");
        }


        
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List);

        }



        
        public JsonResult Sl_Finder(string Item_SlNo)
        {

            Bl_Serial_Check Find_Sl = new Bl_Serial_Check();

            return Json(Find_Sl.Find_Sl(Item_SlNo));

        }


        public JsonResult AutoComplete_FindPO(string input)
        {

            BL_Porder data = new BL_Porder();
            List<SelectListItem> list = data.Find_PO_Info(input,"Get_Matching_PO");

            return Json(list);
        }



    }

}