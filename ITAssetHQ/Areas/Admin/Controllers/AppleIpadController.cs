using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;


namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITSupportEngineers)]
    [Area("Admin")]
    public class AppleIpadController : Controller
    {
        
        public ActionResult AppleIpad_Details()
        {
            BL_AppleIpad com = new BL_AppleIpad();

            List<Mod_AppleIpad> pc_List = com.Get_AppleIpadData();

            return View(pc_List);
        }
        
        [HttpGet]
        public ActionResult AppleIpad_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_AppleIpad Mod_data = new Mod_AppleIpad();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Ipad", "MAKE", "");
            Mod_data.PO_List = Make_List.Vendor_and_PO_List("PO");
            Mod_data.Proc_date = DateTime.Now;
            Mod_data.WrntEnd_Date = DateTime.Now;

            return View(Mod_data);

        }

        
        [HttpPost]
        public ActionResult AppleIpad_CreateItem_Post(Mod_AppleIpad Get_Data)
        {
            string Message = "";
            try
            {


                if (ModelState.IsValid)
                {
                    BL_AppleIpad save_data = new BL_AppleIpad();
                    Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                    int status = save_data.Save_AppleIpad_data(Get_Data, "Add_new", "");

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("Data is not saved");

            }

            return RedirectToAction("AppleIpad_Create_Item", "AppleIpad");
        }

        
        public ActionResult Edit_AppleIpad(string id)
        {
            

            BL_AppleIpad BL_data = new BL_AppleIpad();
            Mod_AppleIpad Model_data = new Mod_AppleIpad();
            Item_MakeModel Make_List = new Item_MakeModel();


            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View(Model_data);
        }

        
        public ActionResult Update_AppleIpad(Mod_AppleIpad Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {
                    BL_AppleIpad Md_Asset = new BL_AppleIpad();

                    status = Md_Asset.Save_AppleIpad_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("AppleIpad_Details", "AppleIpad");
        }

        [Authorize(Roles = "SU, Admin")]
        public ActionResult Delete_AppleIpad(Mod_AppleIpad Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_AppleIpad Md_Asset = new BL_AppleIpad();

                    status = Md_Asset.Save_AppleIpad_data(Get_Data, "Delete", id);

                    if (status < 1)
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

            return RedirectToAction("AppleIpad_Details", "AppleIpad");
        }

        
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Ipad", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List);

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


    }
}