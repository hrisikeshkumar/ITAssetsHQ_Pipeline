namespace IT_Hardware.Areas.Admin.Models
{
    public class ItemInfo_Mod
    {
        public string? Serial_No { get; set; }
        public string? Invoice_Info { get; set; }
        public string? PO_Info { get; set; }
        public string? Approval_Info { get; set; }
    }

    public class ItemdetailInfo_Mod : ItemInfo_Mod
    {
        public string? Invoice_FileId { get; set; }
        public string? PO_Info_FileId { get; set; }
        public string? Approval_Info_FileId { get; set; }
    }
}
