using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Amc_Dashboard
    {

        public int PC_AMC { get; set; }
        public int PC_Waranty { get; set; }
        public int Laptop_AMC { get; set; }
        public int Laptop_Waranty { get; set; }
        public int Printer_AMC { get; set; }
        public int Printer_Waranty { get; set; }
        public int Scanner_AMC { get; set; }
        public int Scanner_Waranty { get; set; }
        public int Ups_AMC { get; set; }
        public int Ups_Waranty { get; set; }

    }

    public class Mod_Amc_Dtl 
    {
        public string Asset_Type { get; set; }
        public DateTime Warnty_Check_Date { get; set; }

        public string AMC_Start_Id { get; set; }

        public string AMC_Vendor_Id { get; set; }
        public List<SelectListItem> Vendor_List { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AMC_Start_DT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AMC_End_DT { get; set; }

        public string User_Id { get; set; }

        public List<Mod_List_Warranty_Amc> list_data { get; set; }
    }

    public class Mod_List_Warranty_Amc
    {
        public string Item_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Designation { get; set; }
        public string Item_SlNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Warnt_Start_DT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Warnt_end_DT { get; set; }

        public string Update_UserId { get; set; }
    }

    public class Mod_add_To_Amc
    {
        public string Item_Id { get; set; }
        public string Vendor_Id { get; set; }
        public string Asset_Types { get; set; }
        public string SL_Number { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AMC_Start_DT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AMC_End_DT { get; set; }
        public string Remarks { get; set; }

        public string Update_UserId { get; set; }
        public List<SelectListItem> Vendor_List { get; set; }

    }

    public class mod_AMC_Warranty_List
    {
        public string Item_Id { get; set; }
        public string Asset_Type { get; set; }
        public string Emp_Name { get; set; }
        public string Designation { get; set; }
        public string Item_SlNo { get; set; }
        public string Vendor_name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AMC_End_Dt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Warranty_End_Dt { get; set; }

        public string Update_UserId { get; set; }

    }


     public class Mod_Bulk_Amc_Update
    {
        public string Asset_Type { get; set; }
      
        public string Updated_AMC_Vendor_Id { get; set; }
        public List<SelectListItem> Vendor_List { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Updated_AMC_Start_DT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Updated_AMC_End_DT { get; set; }

        public string User_Id { get; set; }

        public List<Mod_Bulk_Amc_List> list_data { get; set; }
    }

    public class Mod_Bulk_Amc_List
    {
        public string Emp_Name { get; set; }
        public string Designation { get; set; }
        public string Item_Id { get; set; }
        public string Item_SlNo { get; set; }
        public string Present_Vendor_Name { get; set; }
        public string Present_Vendor_Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AMC_Start_DT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AMC_end_DT { get; set; }
        public bool Update_Flag { get; set; }
        public bool Obsolete_Item { get; set; }
       
    }

}