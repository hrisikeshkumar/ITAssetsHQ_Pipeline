using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware.Areas.Admin.Models
{

    public class RoChapterInfo_Mod
    {
        public string ChapterName {get; set;}

        public List<SelectListItem> ChapterList {get; set;}

        public List<RoChapterInfo> chapterDetail { get; set; }

    }

    public class RoChapterInfo
    {
        public string ChapterName { get; set; }
        public string ItemId { get; set; }
        public string AssetType { get; set; }
        public string SerialNo { get; set; }
        public string Model { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ProcDate { get; set; }
        public int Price { get; set; }
        public string Inv_No { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Inv_date { get; set; }
        public string FundSource { get; set; }
        public string SOFile { get; set; }
        public string InvFile { get; set; }
        public string ApprovalFile { get; set; }
        public List<string> QuoteFiles { get; set; }
    }
}
