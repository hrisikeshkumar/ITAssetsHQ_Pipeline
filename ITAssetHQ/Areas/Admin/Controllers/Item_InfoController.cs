using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITStaffs)]
    [Area("Admin")]
    public class Item_InfoController : Controller
    {
        private IHostingEnvironment Environment;
        public Item_InfoController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

       
        public JsonResult GetDetails(string serialno)
        {
            ItemInfo_BL itemInfo = new ItemInfo_BL();
            ItemInfo_Mod mod = new ItemInfo_Mod();
            mod.Serial_No = serialno;
            return Json(itemInfo.Get_Item_IssueData(mod));       
        }


        public ContentResult Download(string SerialNo, string FileType)
        {

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path= string.Empty;
            string FileName = string.Empty;

            ItemInfo_BL itemInfo = new ItemInfo_BL();
            ItemdetailInfo_Mod inpitdata = new ItemdetailInfo_Mod();
            inpitdata.Serial_No = SerialNo;
            ItemdetailInfo_Mod data =itemInfo.Get_Item_IssueData(inpitdata);


            if (FileType == "PO")
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/HQ/PO/");
                FileName= data.PO_Info_FileId;
            }
            else if(FileType == "Invoice")
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/FinalApproval/");
                FileName = data.Invoice_FileId;
            }
            else if (FileType == "Approval")
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/FinalApproval/");
                FileName = data.Approval_Info_FileId ;
            }
            

            byte[] bytes = System.IO.File.ReadAllBytes(path + FileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);

        }


    }
}
