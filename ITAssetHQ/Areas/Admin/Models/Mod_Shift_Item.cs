using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Shift_Item
    {

        public string? Item_Shift_Id { get; set; }
        [Required]
        public string? Item_Id { get; set; }
       
        public string? Item_SerialNo { get; set; }
        public string? Item_Name { get; set; }
        public string? Item_Shift_Location { get; set; }
        public string? Present_Company { get; set; }
        public string? Shifted_Company { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? Shift_date { get; set; }
        public string? Remarks { get; set; }
        public string? Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string? Verfd_status { get; set; }
        public string? Verfd_usr_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Verfd_date { get; set; }

    }
}