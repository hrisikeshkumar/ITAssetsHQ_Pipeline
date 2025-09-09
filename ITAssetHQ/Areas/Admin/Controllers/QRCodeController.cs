using Humanizer;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ContentModel;
using QRCoder;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITSupportEngineers)]
    [Area("Admin")]
    public class QRCodeController : Controller
    {
        public IActionResult AssetsInfo(string Department, string AssetType)
        {

            List <SelectListItem> Typelist = new List<SelectListItem>
            {
                new SelectListItem("All Assets", "All"),
                new SelectListItem("Desktop", "Desktop"),
                new SelectListItem("Laptop", "Laptop"),
                new SelectListItem("Printer", "Printer"),
                new SelectListItem("Scanner", "Scanner"),
                new SelectListItem("UPS", "UPS"),
                new SelectListItem( "Ipad", "AppleIpad"),
                new SelectListItem("Data Card", "DataCard"),
                new SelectListItem( "Other Item", "OtherItem"),
                new SelectListItem("Server", "Server"),
                new SelectListItem("Switch", "Switch"),
                new SelectListItem("Server", "Server"),
                new SelectListItem("Switch", "Switch")
            };

            List<SelectListItem> dept = new BL_Employee().Bind_Dept();
            dept.Add(new SelectListItem("No Department", "NoDept"));

            View_MOdel_QRCode model = new View_MOdel_QRCode
            {
                QRCode = new QRCode_BL().AssetsList(Department != null ? Department : string.Empty,
                                           AssetType != null ? AssetType : string.Empty),
                Department = Department,
                AssetType = AssetType,
                AssetTypeList = Typelist,               
                DepartmentList = dept
            };

            return View(model);
        }

        public IActionResult CodeGeneration(List<string> SelectAsset)
        {
            var qrCodes = new Dictionary<string, string>();
            var qrGenerator = new QRCodeGenerator();

            var configuation = new ConfigurationDoc().GetConfiguration();
            string URL= configuation.GetSection("QRCodeURL").Value;

            List<CodeInfo> qrCodeInputs = new List<CodeInfo>();

            foreach (string str in SelectAsset)
            {
                CodeInfo data = new CodeInfo();
                data.URL = URL + str;
                data.Serial_No = new QRCode_BL().Get_SerialNo(str);
                qrCodeInputs.Add(data);
            }


            foreach (var input in qrCodeInputs)
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(input.URL, QRCodeGenerator.ECCLevel.Q);
                var pngQrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeBytes = pngQrCode.GetGraphic(20);
                var base64 = Convert.ToBase64String(qrCodeBytes);
                qrCodes.Add(input.Serial_No, $"data:image/png;base64,{base64}");
            }

            return View(qrCodes);

        }

        public IActionResult Asset_Info_Histroy(string id)
        {

            QRCode_BL DLayer = new QRCode_BL();
            AssetInfo_Model model = DLayer.Asset_Detail_Info(id);

            return View(model);
        }

        public IActionResult RaiseIssue(string Id, string type)
        {
            QRCode_BL DLayer = new QRCode_BL();
            RaiseIssue_Mod mod ;
            if (type == "New")
            {
                ViewBag.ActionName = "RaiseIssue_Insert";
                mod = DLayer.Get_Asset_Service_Info(Id, type);
            }         
            else
            {
                ViewBag.ActionName = "RaiseIssue_Update";
                mod = DLayer.Get_Asset_Service_Info(Id, type);
            }

            return View(mod);
        }

        [HttpPost]
        public IActionResult RaiseIssue_Insert(RaiseIssue_Mod data)
        {
            QRCode_BL DLayer = new QRCode_BL();
            data.UserId = HttpContext.User.Identity.Name;
            int status = DLayer.InsUpd_AssetService(data, "Insert_AssetService");

            return RedirectToAction("Asset_Info_Histroy", new { id = data.AssetId });
        }

        [HttpPost]
        public IActionResult RaiseIssue_Update(RaiseIssue_Mod data)
        {
            QRCode_BL DLayer = new QRCode_BL();
            data.UserId = HttpContext.User.Identity.Name;
            int status= DLayer.InsUpd_AssetService(data, "Update_AssetService");

            return RedirectToAction("Asset_Info_Histroy", new { id = data.AssetId });
        }

        [HttpGet]
        public IActionResult Asset_Issue_Histroy(String AssetId)
        {
            QRCode_BL DLayer = new QRCode_BL();
            RaiseIssue_Mod mod = DLayer.Get_Asset_Service_Info(AssetId, "Get_Asset_ServiceIssue");

            return View(mod);
        }

        public IActionResult All_Asset_Issue_Histroy(string UserId, string IssueType )
        {
            if (ViewBag.IssueType != "All")
            {
                ViewBag.IssueType = IssueType;
            }
           
            if (ViewBag.User != "AllUser")
            {
                ViewBag.User = UserId;
            }
            else
            {
                ViewBag.User = string.Empty;
            }

            QRCode_BL DLayer = new QRCode_BL();
            List<RaiseIssue_Mod> mod = DLayer.Get_All_Serivce_Info(UserId, IssueType);

            return View(mod);
        }
    }
}
