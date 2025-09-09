using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{


    public class ItemIssue_Mod
    {
        public ItemInfo_Mod? itemInfo{ get; set; }
        public List<Mod_Item_Issue>? Item_Issues { get; set; }
    }

    public class Mod_Item_Issue_Employee
    {
        public string? Previous_Custady_Id { get; set; }
        public string? Previous_Emp_Name { get; set; }
        public string? Previous_Emp_Designation { get; set; }
        public string? Previous_Emp_Type { get; set; }
        public string? Previous_Emp_Dept { get; set; }
        public string? Previous_Emp_Location { get; set; }

        [Required]
        public string? Transfered_Custady_Id { get; set; }
        public string? Transfered_Emp_Name { get; set; }
        public string? Transfered_Emp_Designation { get; set; }
        public string? Transfered_Emp_Type { get; set; }
        public string? Transfered_Emp_Dept { get; set; }
        public string? Transfered_Emp_Location { get; set; }
        public string? Issue_File_Id { get; set; }


    }


    public class Mod_Item_Issue : Mod_Item_Issue_Employee
    {

        public string? Item_Issue_Id { get; set; }
        public string? Item_Id { get; set; }
        public string? Item_Type { get; set; }
        [Required]
        public string? Item_SerialNo { get; set; }
        public string? Item_Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Issued_date { get; set; }
        public string? Remarks { get; set; }
        public string? Create_usr_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Create_date { get; set; }
        public string? Verfd_status { get; set; }
        public string? Verfd_usr_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Verfd_date { get; set; }

        public IFormFile? Issue_File { get; set; }
    }
}