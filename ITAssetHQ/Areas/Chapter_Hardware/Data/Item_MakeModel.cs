using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Data;

namespace IT_Hardware.Areas.Chapter_Hardware.Data
{
    internal class Item_MakeModel
    {


        public List<SelectListItem> Item_MakeModel_List(string Item_Type, string List_Type, string Item_Make)
        {

            List<SelectListItem> List_Item = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;
                SqlConnection con = new DBConnection().con;


                using (SqlCommand cmd = new SqlCommand("ItemMakeModel_List"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_Item_Type = new SqlParameter("@Item_Type", Item_Type);
                    cmd.Parameters.Add(sqlP_Item_Type);

                    SqlParameter sqlP_List_Type = new SqlParameter("@List_Type", List_Type);
                    cmd.Parameters.Add(sqlP_List_Type);

                    if (List_Type != "MAKE")
                    {
                        SqlParameter sqlP_Item_Make = new SqlParameter("@Make", Item_Make);
                        cmd.Parameters.Add(sqlP_Item_Make);
                    }

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
                    SelectListItem Listdata = new SelectListItem();
                    Listdata.Value = Convert.ToString(dr[0]);
                    Listdata.Text = Convert.ToString(dr[1]);

                    List_Item.Add(Listdata);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }



        public List<SelectListItem> Vendor_List()
        {

            List<SelectListItem> List_Item = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;
                SqlConnection con = new DBConnection().con;

                using (SqlCommand cmd = new SqlCommand("sp_VendorList"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

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
                    SelectListItem Listdata = new SelectListItem();
                    Listdata.Value = Convert.ToString(dr[0]);
                    Listdata.Text = Convert.ToString(dr[1]);

                    List_Item.Add(Listdata);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }

    }
}