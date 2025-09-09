using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Hardware.Areas.Admin.Data;
using IT_Hardware.Areas.Admin.Models;
using System.Data;
using ClosedXML.Excel;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using IT_Hardware.Infra;
using IT_Hardware.IServices;


namespace IT_Hardware.Areas.Admin.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ITManagers)]
    [Area("Admin")]
    public class Stock_ReportController : Controller
    {


        private readonly IBudgetYearService _budgetYearService;

        public Stock_ReportController(IBudgetYearService budgetYearService)
        {
            _budgetYearService = budgetYearService;
        }

        public ActionResult Stock_Report_Detail()
        {
            return View();
        }

        
        public ActionResult Inventary_Report(string Item_Type , string Report_Type)
        {

            BL_StockReport BL = new BL_StockReport();

            List<Mod_StockReport> Model;

            Model = BL.Get_CompData(Item_Type);

            return View( Model);
        }



        
        public ActionResult Budget_Report_Detail()
        {
            Mod_Stock_Report Modal = new Mod_Stock_Report();
            BL_Budget_Year b_year = new BL_Budget_Year();
            Modal.BudYear = _budgetYearService.budget_year_dropdown();


            return View( Modal);
        }



        
        public ActionResult Post_Budget_Report(Mod_Stock_Report mod_data, string Budget_Head, string Report_Type)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
               
                DataTable dt = this.GetCustomers(mod_data.Bud_year_Id);
                wb.Worksheets.Add(dt, "Budget_Sheet");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }

        }


        private DataTable GetCustomers(string Year_Code)
        {
            DataTable ds = new DataTable();
            try
            {
                
                
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_StockReport"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter Asset_type = new SqlParameter("@Bud_Year", Year_Code.Trim());
                    cmd.Parameters.Add(Asset_type);

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_BudList");
                    cmd.Parameters.Add(sqlP_type);


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ds = dt;
                        }
                    }
                }


            }
            catch (Exception ex) { }

            return ds;
        }



        [HttpPost]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }


    }
}