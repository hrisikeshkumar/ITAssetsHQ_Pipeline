using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;


namespace IT_Hardware.Areas.Admin.Controllers
{
    public class UserRoleController : Controller
    {

        [Authorize(Roles = "SU, Admin")]
        public ActionResult UserRole_Details()
        {
            BL_UserRole com = new BL_UserRole();

            List<Mod_UserRole> Mod_userrole = com.Get_UserRoleData();

            return View("~/Areas/Admin/Views/UserRole/UserRole_Details.cshtml", Mod_userrole);
        }

        [HttpPost]
        [Authorize(Roles = "SU")]
        public ActionResult Update_UserRole(Mod_UserRole[] Get_Data)
        {


            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_UserRole Md_Asset = new BL_UserRole();

                    status = Md_Asset.Save_UserRole_data(Get_Data, "Update");

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

            return RedirectToAction("UserRole_Details", "UserRole");
        }


    }
}