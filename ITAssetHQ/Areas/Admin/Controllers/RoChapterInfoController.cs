using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Graph.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class RoChapterInfoController : Controller
    {
        private IHostingEnvironment Environment;
        private string fileloc;
        public RoChapterInfoController(IHostingEnvironment _environment)
        {
            Environment = _environment;
            fileloc = new ConfigurationDoc().GetFileLoc;
        }

        public ActionResult ChapterInfo(RoChapterInfo_Mod data)
        {
            RoChapterInfo_BL com = new RoChapterInfo_BL();

            if (data.ChapterName == null)
                data.ChapterName = "Agra";

            data.ChapterList = com.Get_AllOfficeList();
            data.chapterDetail = com.Get_ChaptersData(data.ChapterName);

            return View(data);
        }


        public ContentResult InvoiceDownload(string fileName)
        {

           
            //string wwwPath = this.Environment.WebRootPath;
            //string contentPath = this.Environment.ContentRootPath;

            
            string path = Path.Combine(fileloc, "\\Invoice");

            //Read the File as Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }

        public ContentResult QuotationDownload(string fileName)
        {
            //string wwwPath = this.Environment.WebRootPath;
            //string contentPath = this.Environment.ContentRootPath;
            //string path = Path.Combine(this.Environment.WebRootPath, "Files\\ChapterFile\\Quotation\\Procurement\\");
            
            string path = Path.Combine(fileloc, "Quotation\\Procurement\\");

            //Read the File as Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }

        [HttpPost]
        public ContentResult SanctionOrderDownload(string fileName)
        {

            //string wwwPath = this.Environment.WebRootPath;
            //string contentPath = this.Environment.ContentRootPath;
            //string path = Path.Combine(this.Environment.WebRootPath, "Files\\ChapterFile\\SanctionOrder\\");

            string path = Path.Combine(fileloc, "Quotation\\SanctionOrder\\");

            //Read the File as Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }


        public ContentResult ApprovalDownload(string fileName)
        {

            string path = Path.Combine(fileloc, "Approval\\");

            //Read the File as Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);

            //Convert File to Base64 string and send to Client.
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }

    }
}
