using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Vendor
    {

        public string? Vendor_id { get; set; }

        [Required]
        public string Vendor_name { get; set; }

        [Required]
        public string Vendor_Addr { get; set; }

        public string? PO_Issued { get; set; }

        public string? Invoice_Processed { get; set; }
        public string? Remarks { get; set; }
        public string? Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string? Verfd_status { get; set; }
        //public List<IFormFile>? File_List { get; set; }

    }
}