using Microsoft.AspNetCore.Mvc.Rendering;
using IT_Hardware.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace IT_Hardware.Areas.Admin.Data
{
    public class FileModel
    {
        public string File_Id { get; set; }
        public string File_Name { get; set; }
        public string File_Table { get; set; }
        public string User_Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}