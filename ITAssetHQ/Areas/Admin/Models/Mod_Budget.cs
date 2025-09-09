using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;



namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Budget
    {
        public string? Budget_Head_Id { get; set; }
        [Required]
        public string? Budget_Year { get; set; }
        public int? Budget_HeadType { get; set; }

        [Required]
        public string? Budget_Name { get; set; }
        [Required]
        public string? Total_Budget_Amount { get; set; }
        public string? Utilized_for_Budget { get; set; }
        public string? Utilized_for_Payment { get; set; }
        public string? Remarks { get; set; }
        public string? Create_User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Create_date { get; set; }

        public List<SelectListItem>? Bud_year_List { get; set; }
    }
}