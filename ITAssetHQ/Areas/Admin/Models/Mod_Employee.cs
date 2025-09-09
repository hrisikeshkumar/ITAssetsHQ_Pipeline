using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Employee
    {
        public string? Emp_Unique_Id { get; set; }
        public string? Emp_Code { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string? Emp_Name { get; set; }

        public string? Emp_Designation { get; set; }

        public string? Emp_Designation_Name { get; set; }

        public string? Emp_Dept { get; set; }
        public string? Emp_Dept_Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string? Emp_Type { get; set; }
        public List<SelectListItem>? Designation_List { get; set; }
        public List<SelectListItem>? Dept_List { get; set; }
        public List<SelectListItem>? Emp_Type_List { get; set; }
        public string? Remarks { get; set; }
        public string? Location { get; set; }
        public string? Create_usr_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Create_date { get; set; }

    }


    public class Mod_Department
    {
        public string? Department_Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? Department_Name { get; set; }
        public string? Department_MicrosoftID { get; set; }
        public string? UserId { get; set; }
    }

    public class Mod_Designation
    {
        public string? Designation_Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? Designation_Type { get; set; }
        public string? Designation_Name { get; set; }
        public string? Designation_MicrosoftID { get; set; }
        public List<SelectListItem>? Designation_Type_List { get; set; }
        public string? UserId { get; set; }
    }

}