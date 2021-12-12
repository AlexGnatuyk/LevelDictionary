using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExcelDataReader;
using LevelDictionary.Application.Interfaces;
using LevelDictionary.Persistence.Entities;
using LevelDictionary.Persistence.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LevelDictionary.Application.Services
{
    public class WordsService: IWordsService
    {
        private readonly ILevelDictionaryDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public WordsService(ILevelDictionaryDbContext context,  IWebHostEnvironment environment, IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
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

        public async Task<List<Word>> SaveExcelFile(IFormFile file)
        {
            var fileName = $"{_environment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream  = System.IO.File.Create(fileName))
            {
                await file.CopyToAsync(fileStream);
                fileStream.Flush();
            }

            var words = GetWordsFromExcel(file.FileName);
            await _context.Word.AddRangeAsync(words);
            await _context.SaveChangesAsync(new CancellationToken());
            
            return words;
        }
        
        private List<Word> GetWordsFromExcel(string fName)
        {
            List<Word> words = new List<Word>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        words.Add(new Word
                        {
                            Id = Guid.NewGuid(),
                            Value = reader.GetValue(0).ToString().ToLower(),
                            Level = reader.GetValue(1).ToString().ToLower()
                        });
                    }
                }
                return words;
            }
        }
    }
}