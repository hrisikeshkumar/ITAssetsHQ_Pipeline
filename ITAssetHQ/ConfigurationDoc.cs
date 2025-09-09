namespace IT_Hardware
{
    public class ConfigurationDoc
    {

        public string GetFileLoc { get; set; }

        public ConfigurationDoc()
        {
            var configuration = GetConfiguration();
            GetFileLoc = configuration.GetSection("RootFileLocation").GetSection("ChapterFile").Value;
        }

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }


    }
}
