using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware.Areas.Admin.Models
{
    public class Mod_Admin_dashB
    {
        public Proposal_details Prop_detail { get; set; }
        //public Proposal_WorkFlow proposal_WorkFlow { get; set; }
        public DateTime IT_Initiate_Date { get; set; }
        public List<mod_Admin_Propsal_List> List_Proposal { get; set; }
        public List<mod_Admin_Bill_Process_List> List_Bill_Process { get; set; }
        public List<mod_SLA_Expiary_List> List_SLA_Expiary { get; set; }
    }
     

    public class File_List
    {
        public string File_Id { get; set; }
        public string File_Name { get; set; }
    }

    public class mod_Admin_Propsal_List
    {
       public string Proposal_Id { get; set; }
        public string Utilization_Details { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime IT_Initiate_Date { get; set; }
        public string Status { get; set; }
        public string NoteLocation { get; set; }
    }

    public class mod_Admin_Bill_Process_List
    {
        public string Proposal_Id { get; set; }

        public string Utilization_Details { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime IT_Initiate_Date { get; set; }
        public string Status { get; set; }
        public string NoteLocation { get; set; }

    }

    public class mod_SLA_Expiary_List
    {
        public string SLA_Id { get; set; }
        public string Vendor_Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Expiary_Date { get; set; }
        public string Status { get; set; }

    }

    public class Grid_Class
    {
        public string Particular { get; set; }
        public string Proposal_Id { get; set; }
        public string StartDate { get; set; }
        public string Status { get; set; }
        public string NoteLocation { get; set; }
    }

    public class WorkFlow
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime SendDate { get; set; }
        public int WorkFlow_Id { get; set; }
        public string FromDte { get; set; }
        public string ToDte { get; set; }
        public string File_Id { get; set; }
        public string Remarks { get; set; }
        public IFormFile WorkFlow_File { get; set; }
        public int LastRow { get; set; }
    }

    public class Proposal_details
    {
        public string? Proposal_Id { get; set; }
        public string? Utilization_Details { get; set; }      
        public string? Proposal_Type { get; set; }
        public string? Budget_Head_Type { get; set; }
        public string? PO_File_Name { get; set; }
        public string? PO_File_Id { get; set; }
        public string? Invoice_Info { get; set; }
        public string? Assets_Info { get; set; }
        public string? Status { get; set; }
        public int? StatusId { get; set; }
        public string? NoteLocation { get; set; }
        public List<SelectListItem>? Department_List { get; set; }
        public List<SelectListItem>? Status_List { get; set; }
        public string? Approval_File_Id { get; set; }
        public string? Approval_File_Name { get; set; }
        public IFormFile Approval_File { get; set; }
        public string? Update_UserId { get; set; }

        public List<File_List>? Prop_Files { get; set; }

        public WorkFlow Work_Flow { get; set; }
        public List<WorkFlow> WorkFlowList { get; set; }
    }



}