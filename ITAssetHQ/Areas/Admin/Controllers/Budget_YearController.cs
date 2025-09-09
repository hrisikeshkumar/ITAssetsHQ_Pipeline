using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;
using DocumentFormat.OpenXml.EMMA;

namespace IT_Hardware.Areas.Admin.Controllers
{

    [Authorize(Policy = AuthorizationPolicies.ITStaffs)]
    [Area("Admin")]
    public class Budget_YearController : Controller
    {

        
        public ActionResult Budget_Year_Details()
        {
            BL_Budget_Year com = new BL_Budget_Year();
            Mod_Budget_Year mod_data = new Mod_Budget_Year();

            mod_data.List_Bud_Year=com.Get_Data();
           


            return View( mod_data);
        }

        
        public ActionResult Budget_Year_Insert(Mod_Budget_Year data)
        {

            try
            {              
                if (ModelState.IsValid)
                {
                    BL_Budget_Year save_data = new BL_Budget_Year();
                    int status = save_data.Save_data(data, "Save");

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

            return RedirectToAction("Budget_Year_Details", "Budget_Year");
        }

        
        public ActionResult Budget_Year_Update(string Bud_Id)
        {
            
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Budget_Year save_data = new BL_Budget_Year();
                    Mod_Budget_Year data = new Mod_Budget_Year();
                    data.Bud_Id = Bud_Id;
                    int status = save_data.Save_data(data,"Delete");

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

            return RedirectToAction("Budget_Year_Details", "Budget_Year");
        }

    }
}