using IT_Hardware.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Graph.Models;
using System.Data;
using System.Data.SqlClient;

namespace IT_Hardware.Areas.Admin.Data
{
    public class ItemInfo_BL
    {

        public ItemInfo_Mod Get_Item_IssueData(ItemInfo_Mod itemInfo)
        {
            ItemdetailInfo_Mod itemdtlInfo=new ItemdetailInfo_Mod();

            itemdtlInfo.Serial_No = itemInfo.Serial_No;

            return getdetails(itemdtlInfo);
        }

        public ItemdetailInfo_Mod Get_Item_IssueData(ItemdetailInfo_Mod itemInfo)
        {
            
            return getdetails(itemInfo);
        }


        private ItemdetailInfo_Mod getdetails(ItemdetailInfo_Mod item)
        {
            ItemdetailInfo_Mod data = new ItemdetailInfo_Mod();

            try
            {
                DataTable dt_Comuter;

                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("sp_ItemInfo"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@SerialNo", item.Serial_No);
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
                data.Serial_No = item.Serial_No;

                if (dt_Comuter.Rows.Count > 0)
                {
                    data.Invoice_Info = Convert.ToString(dt_Comuter.Rows[0]["Invoice_File_Id"]);
                    data.PO_Info = Convert.ToString(dt_Comuter.Rows[0]["PO_File"]);
                    data.Approval_Info = Convert.ToString(dt_Comuter.Rows[0]["Approval_File"]);
                    data.Invoice_FileId = Convert.ToString(dt_Comuter.Rows[0]["Invoice_File_Id"]);
                    data.PO_Info_FileId = Convert.ToString(dt_Comuter.Rows[0]["PO_File"]);
                    data.Approval_Info_FileId = Convert.ToString(dt_Comuter.Rows[0]["Approval_File"]);
                }

                item = data;

            }
            catch (Exception ex) { }

            return item;
        }


        public List<SelectListItem> DepartmentList()
        {
            List<SelectListItem> Value = new List<SelectListItem>();

            SelectListItem No = new SelectListItem("Please Select a Department", "-1");
            Value.Add(No);
            SelectListItem ReD = new SelectListItem("Requested Department", "0");
            Value.Add(ReD);
            SelectListItem IT = new SelectListItem("IT", "1");
            Value.Add(IT);
            SelectListItem FA = new SelectListItem("F&A", "2");
            Value.Add(FA);
            SelectListItem IA = new SelectListItem("IA", "3");
            Value.Add(IA);
            SelectListItem Secretary = new SelectListItem("Secretary Office", "4");
            Value.Add(Secretary);
            SelectListItem Purchase = new SelectListItem("Purchase", "5");
            Value.Add(Purchase);

            return Value;
        }


        public List<SelectListItem> StatusList()
        {

            List<SelectListItem> Value = new List<SelectListItem>();

            SelectListItem Initiated = new SelectListItem("Initiated", "-1");
            Value.Add(Initiated);
            SelectListItem Note = new SelectListItem("Note Forwarded", "1");
            Value.Add(Note);
            SelectListItem Approved = new SelectListItem("Approved", "2");
            Value.Add(Approved);
            SelectListItem PurchaseOrder = new SelectListItem("Purchase Order Issued", "3");
            Value.Add(PurchaseOrder);          
            SelectListItem Item = new SelectListItem("Item Received", "4");
            Value.Add(Item);           
            SelectListItem SancOrder = new SelectListItem("Sanction Order Issued", "5");
            Value.Add(SancOrder);
            SelectListItem Bill = new SelectListItem("Bill Processed", "6");
            Value.Add(Bill);
            SelectListItem Hold = new SelectListItem("Hold", "98");
            Value.Add(Hold);
            SelectListItem Withdrawn = new SelectListItem("Withdrawn", "99");
            Value.Add(Withdrawn);
            SelectListItem Completed = new SelectListItem("Completed", "100");
            Value.Add(Completed);


            return Value;
        }

    }
}
