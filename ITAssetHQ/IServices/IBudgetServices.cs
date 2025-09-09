using IT_Hardware.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Hardware.IServices
{
    public interface IBudgetUsesService
    {
        List<Bud_Uses_List> Get_BudgetData();
        Mod_Budget_Uses Get_Data_By_ID(Mod_Budget_Uses model, string id);
        void Get_Budget_Head(Mod_Budget_Uses model, string yearcode);
        void Get_Prev_Budget_Uses(Mod_Budget_Uses model, string budHeadId, string yearcode);
        List<Bud_Uses_List> Get_BudgetUses_By_BudId(string budHeadId);
        List<PO_Info> Get_PO_Info(string inputData);
        int Save_Budget_data(Mod_Budget_Uses data, string action, string id);
    }

    public interface IBudgetYearService
    {
        List<SelectListItem> budget_year_dropdown();
    }

}
