using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;

namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class MasterDataController : Controller
    {

        public ActionResult List_Make_Data()
        {
            BL_AssetMaster AssetMaster_data = new BL_AssetMaster();

            List<Mod_AssetMaster> MasterList = AssetMaster_data.Get_Data();

            return View(  MasterList);
        }


        
        [HttpGet]
        public ActionResult Add_AssetMakeModel(Mod_AssetMaster Get_Data)
        {
            return View();
        }


        
        [HttpPost]
        public ActionResult Save_AssetMakeModel(Mod_AssetMaster Get_Data)
        {
            int status = 0;
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_AssetMaster Md_Asset = new BL_AssetMaster();

                    status = Md_Asset.Save_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Add_AssetMakeModel", "MasterData");
        }


        
        [HttpGet]
        public ActionResult EdIT_HardwaresMasterData(string id)
        {
            
            BL_AssetMaster Md_Asset = new BL_AssetMaster();
            //Md_Asset.Get_Data_By_ID(Asset.Asset_ID.ToString().Trim());
            Mod_AssetMaster data = Md_Asset.Get_Data_By_ID(id);

            return View( data);
        }


        
        [HttpPost]
        public ActionResult Update_Makedata(Mod_AssetMaster Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_AssetMaster Md_Asset = new BL_AssetMaster();

                    status = Md_Asset.Save_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("List_Make_Data", "MasterData");
        }

        
        public ActionResult Delete_Item(Mod_AssetMaster Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_AssetMaster Md_Asset = new BL_AssetMaster();

                    status = Md_Asset.Save_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Get_Master_Data", "MasterData");
        }



    }
}