using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LevelDictionary.Application.Interfaces;
using LevelDictionary.Persistence.Entities;
using LevelDictionary.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LevelDictionary.Application.Services
{
    public class WordsService: IWordsService
    {
        private readonly ILevelDictionaryDbContext _context;

        public WordsService(ILevelDictionaryDbContext context)
        {
            _context = context;
        }
        public async Task AddWord(Word word)
        {
            word.Value = word.Value.ToLower();
            word.Level = word.Level.ToLower();
            _context.Word.Add(word);
            await _context.SaveChangesAsync(new CancellationToken());
        }

        public async Task<IList<Word>> GetWordsByLevel(string level)
        {
            var words = await _context.Word.Where(word => word.Level == level.ToLower()).ToListAsync();
            return words;
        }

        public async Task DeleteWord(string value)
        {
            var word = _context.Word.FirstOrDefault(word => word.Value == value.ToLower());
            _context.Word.Remove(word);
            await _context.SaveChangesAsync(new CancellationToken());
        }

        public async Task<IList<Word>> GetAllWords()
        {
            return await _context.Word.ToListAsync();
        }

        public async Task<string> GetLevelByWord(string word)
        {
            var result = await _context.Word.FirstOrDefaultAsync(w => w.Value == word.ToLower());
            return result.Level;
        }
    }
}