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
    public class Budget_UsesController : Controller
    {

        private readonly IBudgetUsesService _budgetUsesService;
        private readonly IBudgetYearService _budgetYearService;

        public Budget_UsesController(IBudgetUsesService budgetUsesService, IBudgetYearService budgetYearService)
        {
            _budgetUsesService = budgetUsesService;
            _budgetYearService = budgetYearService;
        }


        public ActionResult Budget_Uses_Details()
        {


            Mod_Budget_Uses Mod_Budget_Uses = new Mod_Budget_Uses();

             Mod_Budget_Uses.Bud_us_list = _budgetUsesService.Get_BudgetData();

            Mod_Budget_Uses.Budget_Year_List = _budgetYearService.budget_year_dropdown();

            return View( Mod_Budget_Uses);
        }

        
        [HttpGet]
        public ActionResult Budget_Uses_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Budget_Uses Mod_data = new Mod_Budget_Uses();

            Mod_data.Budget_Year_List = _budgetYearService.budget_year_dropdown();
            Mod_data.Processing_Date = DateTime.Now;

            return View( Mod_data);
        }

        
        [HttpPost]
        public ActionResult Budget_Uses_CreateItem_Post(Mod_Budget_Uses Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    int status = _budgetUsesService.Save_Budget_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Budget_Uses_Details", "Budget_Uses");
        }

        
        public ActionResult Edit_Budget_Uses(string id)
        {

            Mod_Budget_Uses Model_data = new Mod_Budget_Uses();

           

            Model_data = _budgetUsesService.Get_Data_By_ID(Model_data, id);

            BL_Budget_Year bud_year = new BL_Budget_Year();

            Model_data.Budget_Year_List = _budgetYearService.budget_year_dropdown();

            _budgetUsesService.Get_Budget_Head(Model_data, Model_data.Budget_Year);


            return View( Model_data);
        }

        
        public ActionResult Update_Budget_Uses(Mod_Budget_Uses Get_Data, string Budget_Uses_Id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    

                    status = _budgetUsesService.Save_Budget_data(Get_Data, "Update", Budget_Uses_Id);

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

            return RedirectToAction("Budget_Uses_Details", "Budget_Uses");
        }

        
        public ActionResult Delete_Budget_Uses(Mod_Budget_Uses Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {

                    status = _budgetUsesService.Save_Budget_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Budget_Uses_Details", "Budget_Uses");
        }


        
        public JsonResult Budget_List(string Yearcode)
        {

            BL_Budget_Uses Bud_List = new BL_Budget_Uses();

            Mod_Budget_Uses Mod_budget = new Mod_Budget_Uses();

            Bud_List.Get_Budget_Head(Mod_budget, Yearcode);

            return Json(Mod_budget.Budget_List );

        }


        
        public JsonResult Prev_Budget_info(string Bud_Head_Id, string Yearcode)
        {

            BL_Budget_Uses Bud_List = new BL_Budget_Uses();

            Mod_Budget_Uses Mod_budget = new Mod_Budget_Uses();

            Bud_List.Get_Prev_Budget_Uses(Mod_budget, Bud_Head_Id,  Yearcode);

            return Json(Mod_budget);

        }



        
        public JsonResult Budget_Uses_List(string Bud_head_Id)
        {

            return Json(_budgetUsesService.Get_BudgetUses_By_BudId(Bud_head_Id));

        }


        public JsonResult AutoComplete(string InputData)
        {
            
            List<PO_Info> list = _budgetUsesService.Get_PO_Info(InputData);

            return Json(list);
        }

    }

}