using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_StockReport
    {

        public string? SLNo { get; set; }
        public string? Emp_Name { get; set; }
        public string? Emp_Designation { get; set; }
        public string? Emp_Location { get; set; }
        public string? Emp_Dept { get; set; }
        public string? Item_Type { get; set; }
        public string? Item_Make { get; set; }
        public string? Item_Model { get; set; }
        public string? Item_SlNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Item_Proc_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Warranty_Exp_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? AMC_Exp_date { get; set; }
        public string? Obsolete { get; set; }
    }


    public class Mod_BudgetReport
    {
        public string Bud_Head { get; set; }
        public string Total_Bud_Approved { get; set; }
        public string Bud_Type { get; set; }
        public string Bud_Utilization { get; set; }
        public string Amount_Utilized_Before { get; set; }
        public string Amount_Left_Before_Prop { get; set; }
        public string Bud_amount { get; set; }
        public string Amount_Left_After_Prop { get; set; }
        public string Prop_Date { get; set; }
    }

}