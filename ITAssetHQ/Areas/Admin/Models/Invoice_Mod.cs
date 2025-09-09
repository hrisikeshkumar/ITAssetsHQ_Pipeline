using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Invoice_Mod
    {
        public string? Invoice_id { get; set; }
        public string? Invoice_No { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Invoice_Date { get; set; }
        public string? PO_Id { get; set; }
        public List<SelectListItem>? PO_list { get; set; }
        public string? Invoice_Subject { get; set; }
        public int Invoice_Value { get; set; }
        public int? Penalty_Amount { get; set; }
        public string? Penalty_Reason { get; set; }
        public string? Remarks { get; set; }
        public string? Create_usr_id { get; set; }
        public IFormFile? File_Invoice { get; set; }
        public string? FileName_Invoice { get; set; }
        public string? FileId_Invoice { get; set; }
    }
}
