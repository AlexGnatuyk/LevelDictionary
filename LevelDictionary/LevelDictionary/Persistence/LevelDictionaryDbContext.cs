using LevelDictionary.Persistence.Entities;
using LevelDictionary.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LevelDictionary.Persistence
{
    public class LevelDictionaryDbContext: DbContext, ILevelDictionaryDbContext
    {
        public DbSet<Word> Word { get; set; }
        
        public LevelDictionaryDbContext(DbContextOptions<LevelDictionaryDbContext> options)
            : base(options) { }

        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     builder.ApplyConfiguration(new NoteConfiguration());
        //     base.OnModelCreating(builder);
        // }
    }
}