using System;

namespace LevelDictionary.Persistence.Entities
{
    public class Word
    {
        public Guid Id { get; set; }
        
        public string Value { get; set; }

        public string Level { get; set; }
    }
}