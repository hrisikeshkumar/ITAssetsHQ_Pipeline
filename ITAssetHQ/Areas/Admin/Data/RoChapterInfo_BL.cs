using IT_Hardware.Areas.Admin.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.EMMA;

namespace IT_Hardware.Areas.Admin.Data
{
    public class RoChapterInfo_BL
    {
        public List<RoChapterInfo> Get_ChaptersData(string chapterName)
        {

            RoChapterInfo BL_data;
            List<RoChapterInfo> Listdata = new List<RoChapterInfo>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().conChapter;

                using (SqlCommand cmd = new SqlCommand("sp_ChapterInfo"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_OfficeData");
                    SqlParameter sqlP_chapterName = new SqlParameter("@chapterName", chapterName);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);
                    cmd.Parameters.Add(sqlP_chapterName);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter = dt;
                        }
                    }
                }

                foreach (DataRow dr in dt_Comuter.Rows)
                {

                  
                    BL_data = new RoChapterInfo();
                    BL_data.ChapterName = chapterName;
                    BL_data.ItemId = Convert.ToString(dr["Item_Id"]);
                    BL_data.AssetType = Convert.ToString(dr["Asset_Type"]);
                    BL_data.Model = Convert.ToString(dr["Model"]);
                    BL_data.SerialNo = Convert.ToString(dr["Item_SlNo"]);
                    BL_data.ProcDate = Convert.ToDateTime(dr["Proc_Date"]);                   
                    BL_data.Price = Convert.ToInt32(dr["Asset_Price"]);
                    BL_data.Inv_No = Convert.ToString(dr["Inv_No"]);
                    BL_data.Inv_date = Convert.ToDateTime(dr["Inv_date"]);
                    BL_data.FundSource = Convert.ToString(dr["OwnerName"]);
                    BL_data.SOFile = Convert.ToString(dr["Sanction_FileName"]);                   
                    BL_data.InvFile = Convert.ToString(dr["Inv_File_Name"]);
                    BL_data.ApprovalFile = Convert.ToString(dr["Approval_File_Name"]);
                    //BL_data.QuoteFile = Convert.ToString(dr["QuoteFile_Name"]);
                    BL_data.QuoteFiles = new List<string>();

                    if (Convert.ToString(dr["QuoteFile_Name"]) != string.Empty)
                    { 
                        foreach (string file in Convert.ToString(dr["QuoteFile_Name"]).Split(','))
                        {
                            BL_data.QuoteFiles.Add(file);
                        }
                    }
                    Listdata.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return Listdata;
        }


        public List<SelectListItem> Get_AllOfficeList()
        {

            List<SelectListItem> Listdata = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().conChapter;

                using (SqlCommand cmd = new SqlCommand("sp_ChapterInfo"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_ChapterList");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter = dt;
                        }
                    }
                }

                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    SelectListItem data = new SelectListItem();

                    data.Text = Convert.ToString(dr["ChapterName"]);
                    data.Value = Convert.ToString(dr["Emailid"]);

                    Listdata.Add(data);
                }

            }
            catch (Exception ex) { }

            return Listdata;
        }


    }
}
