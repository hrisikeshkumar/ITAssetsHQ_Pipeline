using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Budget_Uses
    {
        [Required]
        public string? Budget_Head_Id { get; set; }
        public string?  Budget_Uses_Id { get; set; }
        public string? Budget_Name { get; set; }
        public string? Budget_Year { get; set; }
        public string? Utilization_Details { get; set; }
        public string? Budget_Type { get; set; }

        public string? PO_id { get; set; }
        public string? PO_No { get; set; }
        public int? Total_Approved_Budget { get; set; }
        [Required]
        public int? Amount_Utilized_Before { get; set; }
        [Required]
        public int? Balance_Available { get; set; }
        [Required]
        public int? Budget_Amount { get; set; }
        [Required]
        public int? Remaning_Balance { get; set; }
        public string? Remarks { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Processing_Date { get; set; }
        public string? Create_User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Create_date { get; set; }

        public List<SelectListItem>? Budget_List { get; set; }

        public List<SelectListItem>? Budget_Year_List { get; set; }

        public List<Bud_Uses_List>? Bud_us_list { get; set; }
    }

    public class Bud_Uses_List
    {
        public string? Utilization_Details { get; set; }
        public string? Budget_Uses_Id { get; set; }
        public string? Budget_Name { get; set; }
        public int? Budget_Amount { get; set; }
        public string? Budget_Type { get; set; }
    }


    public class PO_Info
    {
        public string? PO_Id { get; set; }
        public string? PO_No { get; set; }
        public string? PO_Date { get; set; }
        public string? PO_Detail { get; set; }
        public string? Vendor_Name { get; set; }
    }

}