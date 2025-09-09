using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;


namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class IT_Hardware_AMCController : Controller
    {
        // GET: Admin/IT_Hardware_AMC
        public ActionResult Amc_DashBoard()
        {

            Mod_Amc_Dashboard mod_data = new Mod_Amc_Dashboard();
            BL_Hardware_Amc Get_data = new BL_Hardware_Amc();
            Get_data.Get_Amc_Data(mod_data);

           return View(  mod_data);
        }

        public ActionResult Find_Warranty_Expired(Mod_Amc_Dtl mod_data, string Types)
        {
           
            BL_Hardware_Amc B_Layer = new BL_Hardware_Amc();

            if (Types == null || Types ==string.Empty)
                Types = "Desktop";

            mod_data.Asset_Type = Types;
            if (mod_data.Warnty_Check_Date == null || mod_data.Warnty_Check_Date ==Convert.ToDateTime("01-01-0001"))
                mod_data.Warnty_Check_Date = System.DateTime.Today;

            mod_data.list_data = B_Layer.Find_Warranty_Expired(mod_data, Types);


            BL_SLA vendor_data = new BL_SLA();

            mod_data.Vendor_List=vendor_data.Vendor_List();


            return View( mod_data);
        }

     
        public ActionResult Find_Amc_To_Renew()
        {
            return View();
        }
        
        public ActionResult Find_Amc_To_Remove()
        {
            return View();
        }

        public ActionResult Get_List_AMC( string Types)
        {

            BL_Hardware_Amc B_Layer = new BL_Hardware_Amc();

            List<mod_AMC_Warranty_List> mod_data = new List<mod_AMC_Warranty_List>();
   
            mod_data = B_Layer.Get_Item_in_AMC_or_Warranty("AMC", Types);

            ViewBag.Assettype = Types;

          

            return View( mod_data);
        }

        public ActionResult Get_List_Warranty(string Types)
        {

            BL_Hardware_Amc B_Layer = new BL_Hardware_Amc();

            List <mod_AMC_Warranty_List> mod_data = new List<mod_AMC_Warranty_List>();

            mod_data = B_Layer.Get_Item_in_AMC_or_Warranty("Warranty", Types);

            ViewBag.Assettype = Types;

            return View(  mod_data);
        }

        public ActionResult Remove_From_AMC(string Item_Id, string Types)
        {

            int status = 0;
            try
            {
                string usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Hardware_Amc Md_Asset = new BL_Hardware_Amc();

                    status = Md_Asset.Remove_From_Amc(Item_Id, usr_id);

                    if (status >0 )
                    {
                        TempData["Message"] = String.Format("Asset has removed from AMC");
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

            return RedirectToAction("Get_List_AMC", "IT_Hardware_AMC", new { AssetTypes= Types });

  
        }

        public ActionResult Shift_Warnty_To_Amc(Mod_Amc_Dtl data)
        {
            int status = -1;
            try
            {
                data.User_Id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Hardware_Amc Md_Asset = new BL_Hardware_Amc();

                    status = Md_Asset.Add_To_Amc(data);

                    if (status !=-1)
                    {
                        TempData["Message"] = String.Format("Asset has Added to AMC");
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

            return RedirectToAction("Amc_DashBoard", "IT_Hardware_AMC", new { AssetTypes = data.Asset_Type });

        }

        public ActionResult Update_Bulk_Amc(string Types)
        {
            BL_Hardware_Amc B_Layer = new BL_Hardware_Amc();

            Mod_Bulk_Amc_Update mod_data = new Mod_Bulk_Amc_Update();

            mod_data.list_data = B_Layer.Get_Bulk_Item_AMC( Types);

            ViewBag.Assettype = Types;

            BL_SLA vendor_list = new BL_SLA();

            mod_data.Vendor_List = vendor_list.Vendor_List();

            mod_data.Updated_AMC_Start_DT = System.DateTime.Today;
            mod_data.Updated_AMC_End_DT = System.DateTime.Today.AddYears(3);

           

            return View( mod_data);
        }

        public ActionResult Update_Bulk_AMC_Data(Mod_Bulk_Amc_Update mod_Data)
        {

            int status = 0;
            try
            {
                mod_Data.User_Id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Hardware_Amc Md_Asset = new BL_Hardware_Amc();

                    status = Md_Asset.Update_Bulk_AMC(mod_Data);

                    if (status >0)
                    {
                        TempData["Message"] = String.Format("Asset has removed from AMC");
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

            return RedirectToAction("Get_List_AMC", "IT_Hardware_AMC", new { AssetTypes = "" });


        }

    }
}