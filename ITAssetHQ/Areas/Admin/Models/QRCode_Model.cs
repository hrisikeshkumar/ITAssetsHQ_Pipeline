using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware.Areas.Admin.Models
{

    public class View_MOdel_QRCode
    {
        public string? Department { get; set; }
        public string? AssetType { get; set; }

        public List<SelectListItem>? DepartmentList { get; set; }
        public List<SelectListItem>? AssetTypeList { get; set; }
        public List<QRCode_Model>? QRCode { get; set; }
    }


    public class QRCode_Model
    {
        public string? Item_Issue_Id { get; set; }
        public string? Item_Id { get; set; }
        public int? SelectAsset { get; set; }
        public string? Asset_Id { get; set; }
        public string? Asset_Type { get; set; }
        public string? Asset_SerialNo { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? EmployeeName { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Issued_date { get; set; }
        public string? UserId { get; set; }
    }

    public class AssetInfo_Model : QRCode_Model
    {
        public string? PO_No { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? PO_Date { get; set; }
        public int? PO_Value { get; set; }
        public int? Asset_Value { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Procuremt_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Warranty_End_Date { get; set; }
        public string? VenderName { get; set; }

        public List<ShiftAsset>? ShiftingHistory { get; set; }
        public List<AssetService>? ServiceHistory { get; set; }
    }

    public class ShiftAsset
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Issued_date { get; set; }

        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Designation { get; set; }

        public string? Department { get; set; }
    }

    public class AssetService
    {
        
        public string? Issue_Id { get; set; }
        public string? VenderName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Issue_Create_Date { get; set; }
        public string? IssueInfo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Issue_Resolve_Date { get; set; }
        public bool? Resolved { get; set; }
        public string? Resolution_Detail { get; set; }
    }

    public class CodeInfo
    {
        public string? URL { get; set; }
        public string? Serial_No { get; set; }
    }


    public class RaiseIssue_Mod
    {
        public string? Id { get; set; }
        public string? AssetId { get; set; }
        public string? Item_Issue_Id { get; set; }
        public string? AssetType_SerialNo { get; set; } 
        public string? Employee_Name_Desig_Dept { get; set; }
        public string? Make_Model { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Issue_Create_Date { get; set; }
        public string? IssueInfo { get; set; }
       
        public string? VenderName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? Issue_Resolve_Date { get; set; }
        public string? Resolution_Detail { get; set; }
        public bool? Resolved { get; set; }
        public string? Remarks { get; set; }
        public string? UserId { get; set; }
    }

}
