using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_User_Info
    {
        public string User_ID { get; set; }
        public string Emp_Code { get; set; }
        public string User_First_Name { get; set; }
        public string User_Last_Name { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public string Remarks { get; set; }
        public Boolean Active { get; set; }
        public string Create_User { get; set; }
        public DateTime? Create_date { get; set; }
        
    }
}