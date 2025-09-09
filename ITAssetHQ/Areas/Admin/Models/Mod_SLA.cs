using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_SLA
    {   
        public string? SLA_Id { get; set; }   /*pp*/
        public string? PO_id { get; set; }
        public string? Vender_Name { get; set; }
        public string? PO_Details { get; set; }   /*pp*/
        public List<SelectListItem>? PO_List { get; set; }   /*pp*/
        public string? Service_Type_Short { get; set; }
        public string? Service_Type_Details { get; set; }
        public string? SLA_File_Name { get; set; }
        public string? Remarks { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Service_ST_DT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Expiry_DT { get; set; }
        public string? Create_usr_id { get; set; }    /*pp*/
        public string? Verfd_status { get; set; }    /*pp*/
        public IFormFile? SLA_File { get; set; }
    }
}