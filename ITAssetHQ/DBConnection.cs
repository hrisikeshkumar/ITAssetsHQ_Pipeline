using System.Data.SqlClient;

namespace IT_Hardware
{
    public class DBConnection
    {
        public SqlConnection con { get; set; }
        public SqlConnection conChapter { get; set; }

        public DBConnection()
        {
            var configuation = new ConfigurationDoc().GetConfiguration();
            con = new SqlConnection(configuation.GetSection("ConnectionStrings").GetSection("SQLConnection").Value);
            conChapter = new SqlConnection(configuation.GetSection("ConnectionStrings").GetSection("ChapterConn").Value);
        }

        //public IConfigurationRoot GetConfiguration()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //    return builder.Build();
        //}

    }
}
