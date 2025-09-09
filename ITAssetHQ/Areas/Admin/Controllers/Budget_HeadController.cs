using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;
using IT_Hardware.IServices;

namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITStaffs)]
    [Area("Admin")]
    public class Budget_HeadController : Controller
    {

        private readonly IBudgetYearService _budgetYearService;

        public Budget_HeadController( IBudgetYearService budgetYearService)
        {
            _budgetYearService = budgetYearService;
        }


        public ActionResult Budget_Head_Details()
        {
            BL_Budget_Head com = new BL_Budget_Head();

            List<Mod_Budget> pc_List = com.Get_BudgetData();

            return View( pc_List);
        }


        
        [HttpGet]
        public ActionResult Budget_Head_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Budget Mod_data = new Mod_Budget();

            BL_Budget_Year bud_year = new BL_Budget_Year();

            Mod_data.Bud_year_List = _budgetYearService.budget_year_dropdown();


            return View( Mod_data);
        }

        
        [HttpPost]
        public ActionResult Budget_Head_CreateItem_Post(Mod_Budget Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {
                    BL_Budget_Head save_data = new BL_Budget_Head();
                    int status = save_data.Save_Budget_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Budget_Head_Create_Item", "Budget_Head");
        }


        
        public ActionResult Edit_Budget_Head(string id)
        {

            BL_Budget_Head BL_data = new BL_Budget_Head();
            Mod_Budget Model_data = new Mod_Budget();
           
            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            BL_Budget_Year bud_year = new BL_Budget_Year();

            Model_data.Bud_year_List = _budgetYearService.budget_year_dropdown();


            return View( Model_data);
        }


        
        public ActionResult Update_Budget_Head(Mod_Budget Get_Data, string Budget_Head_Id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Budget_Head Md_Asset = new BL_Budget_Head();

                    status = Md_Asset.Save_Budget_data(Get_Data, "Update", Budget_Head_Id);

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

            return RedirectToAction("Budget_Head_Details", "Budget_Head");
        }


        
        public ActionResult Delete_Budget_Head(Mod_Budget Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Budget_Head Md_Asset = new BL_Budget_Head();

                    status = Md_Asset.Save_Budget_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Budget_Head_Details", "Budget_Head");
        }



    }

}