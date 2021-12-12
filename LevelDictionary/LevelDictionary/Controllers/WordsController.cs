using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ExcelDataReader;
using LevelDictionary.Application.Interfaces;
using LevelDictionary.Persistence.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace LevelDictionary.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpPost]
        [Route("AddWord")]
        public async Task<ActionResult> AddWord([FromBody] Word newWord)
        {
            await _wordsService.AddWord(newWord);
            return Ok();
        }

        [HttpGet]
        [Route("GetWordsByLevel")]
        public async Task<ActionResult<IList<Word>>> GetWordsByLevel(string level)
        {
            var words = await _wordsService.GetWordsByLevel(level);
            return Ok(words);
        }

        [HttpGet]
        [Route("GetLevelByWord")]
        public async Task<ActionResult<string>> GetLevelByWord(string word)
        {
            var level = await _wordsService.GetLevelByWord(word);
            
            return Ok(JsonSerializer.Serialize(level));
        }

        [HttpGet]
        [Route("GetAllWords")]
        public async Task<ActionResult<IList<Word>>> GetAllWords()
        {
            var words = await _wordsService.GetAllWords();
            return Ok(words);
        }

        [HttpDelete]
        [Route("DeleteWord")]
        public async Task<ActionResult> DeleteWord(string value)
        {
            await _wordsService.DeleteWord(value);
            return Ok();
        }

        [HttpPost]
        [Route("UploadExcelFile")]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            var words = await _wordsService.SaveExcelFile(file);
            return Ok(words);
        }
    }
}