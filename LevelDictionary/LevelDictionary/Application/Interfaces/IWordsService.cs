using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LevelDictionary.Persistence.Entities;
using Microsoft.AspNetCore.Http;

namespace LevelDictionary.Application.Interfaces
{
    public interface IWordsService
    {
        Task AddWord(Word word);
        Task<IList<Word>> GetWordsByLevel(string level);
        Task DeleteWord(string value);
        Task<IList<Word>> GetAllWords();
        Task<string> GetLevelByWord(string word);
        Task<List<Word>> SaveExcelFile(IFormFile file);
    }
}