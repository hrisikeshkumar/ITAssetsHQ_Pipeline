using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;



namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Laptop
    {
        public string? Item_id { get; set; }
        [Required]
        public string? Item_Make_id { get; set; }
        public List<SelectListItem>? Item_Make_List { get; set; }
        public string? Item_Model_id { get; set; }
        public List<SelectListItem>? Item_Model_List { get; set; }



        public string? Make_Name { get; set; }
        public string? Model_Name { get; set; }
        public string? Asset_Price { get; set; }
        public string? PO_Id { get; set; }
        public string? PO_No { get; set; }
        public DateTime? PO_Date { get; set; }
        public DateTime? WrntEnd_Date { get; set; }


        public string? Item_Type { get; set; }

        [Required]
        public string? Item_serial_No { get; set; }
        public string? Remarks { get; set; }

        [Required]
        public int? price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Proc_date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Warnt_end_dt { get; set; }
        public string? Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string? Verfd_status { get; set; }
        public string? Verfd_usr_id { get; set; }
        public DateTime? Verfd_date { get; set; }
        public List<SelectListItem>? PO_List { get; set; }


    }
}