using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Budget_Year
    {
        public string? Bud_Id { get; set; }
        [Required]
        public string? Bud_Year { get; set; }
        public Boolean default_Bud { get; set; }

        public string? Update_UserId { get; set; }

        public List<Mod_Budget_Year>? List_Bud_Year{get; set;}
    }

}