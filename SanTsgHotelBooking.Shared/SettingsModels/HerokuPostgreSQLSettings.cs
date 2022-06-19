namespace SanTsgHotelBooking.Shared.SettingsModels
{
    public class HerokuPostgreSQLSettings
    {
        public static string GetHerokuConnectionString(string conString)
        {
            string connectionUrl = conString;
            if (string.IsNullOrEmpty(conString))
            {
                string? dbURL = Environment.GetEnvironmentVariable("DATABASE_URL");
                connectionUrl = dbURL ?? "Postgre dburl missing";
            }
            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
    }
}
