using System.Threading;
using System.Threading.Tasks;
using LevelDictionary.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LevelDictionary.Persistence.Interfaces
{
    public interface ILevelDictionaryDbContext
    {
        public DbSet<Word> Word { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}