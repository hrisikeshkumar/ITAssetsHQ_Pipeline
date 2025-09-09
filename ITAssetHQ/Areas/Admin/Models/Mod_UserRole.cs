using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_UserRole
    {

        public string User_ID { get; set; }
        public string User_fullname { get; set; }
        public string[] User_Role { get; set; }
        public Boolean SU_Role { get; set; }
        public Boolean Admin_Role { get; set; }
        public Boolean Manager_Role { get; set; }
        public Boolean InventoryManager_Role { get; set; }
        public Boolean FmsEngineer_Role { get; set; }
        public Boolean ServerEngineer_Role { get; set; }
        public string Create_User { get; set; }
        public DateTime? Create_date { get; set; }

    }
}