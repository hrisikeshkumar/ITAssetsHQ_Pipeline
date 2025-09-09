using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Stock_Report
    {
        public string? Bud_year_Id { get; set; }
        public List<SelectListItem>? BudYear { get; set; }
    }
}