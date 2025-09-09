using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware.Areas.Chapter_Hardware.Models
{
    public class ROChapter_Mod
    {
        public string Item_id { get; set; }
        [Required]
        public string Item_Make_id { get; set; }
        [Required]
        public string Item_Model_id { get; set; }
        public List<SelectListItem> Item_Make_List { get; set; }
        public List<SelectListItem> Item_Model_List { get; set; }
        public string Item_Type { get; set; }
        [Required]
        public string Item_serial_No { get; set; }
        [Required]
        public DateTime? Proc_date { get; set; }
        public string ROChapterName { get; set; }
        public string Fund_Provided { get; set; }
        public string Vendor_Name { get; set; }
        public string Invoice_Number { get; set; }
        public string Invoice_File { get; set; }
        [Required]
        public int price { get; set; }
        public string Remarks { get; set; }
        public string Sanction_Order_File { get; set; }
        public string Sanction_Order_ID { get; set; }
        public DateOnly Sanction_Order_Date { get; set; }
        public string Item_Sold { get; set; }
        public int Sold_DT { get; set; }
        public string Sold_To { get; set; }
        public string Sold_Doc { get; set; }
        public string Create_user { get; set; }
    }
}
