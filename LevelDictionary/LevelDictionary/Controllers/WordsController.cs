using System.Collections.Generic;
using System.Threading.Tasks;
using LevelDictionary.Application.Interfaces;
using LevelDictionary.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LevelDictionary.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public class WordsController: ControllerBase
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
            return Ok(level);
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

    }
}