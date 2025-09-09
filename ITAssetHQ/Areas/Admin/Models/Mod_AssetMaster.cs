using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_AssetMaster
    {
        public string? Asset_ID { get; set; }
        [Required]
        public string? Asset_make { get; set; }
        [Required]
        public string? Asset_Model { get; set; }
        [Required]
        public string? Asset_Type { get; set; }
        public int? Asset_Status { get; set; }
        public string? Create_User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Create_Date { get; set; }
        public string? Update_User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Update_Date { get; set; }

    }
}