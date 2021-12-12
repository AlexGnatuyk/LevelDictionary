namespace LevelDictionary.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(LevelDictionaryDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}