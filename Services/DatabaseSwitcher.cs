namespace BabyShopWeb2.Services
{
    public enum DatabaseType
    {
        SQLite,
        MongoDB
    }
    
    public class DatabaseSettings
    {
        public DatabaseType ActiveDatabase { get; set; } = DatabaseType.SQLite;
        public bool EnableMongoDBBackup { get; set; } = false;
    }
}
