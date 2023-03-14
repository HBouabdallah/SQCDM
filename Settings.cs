namespace IndustryIncident
{
    public class Settings
    {
        public string  connectionString { get; set; }
        public string  logPath { get; set; }

        public static Settings GetSettings(IConfiguration configuration)
        {
            return new Settings()
            {
                connectionString = configuration.GetConnectionString("connection").ToString(),
                logPath = configuration.GetSection("logPath").Value.ToString()
            };
        }
    }
}
