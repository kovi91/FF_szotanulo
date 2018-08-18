using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_szotanulo
{
    public class WordDictionary
    {
        Word[] words;
        string dictionary_name;

        public Word[] Words
        {
            get { return this.words; }
        }

        public string Dictionary_name
        {
            get { return this.dictionary_name; }
        }

        public WordDictionary(string [] linesOfFile, string fileName)
        {
            words = new Word[linesOfFile.Length];
            for (int i = 0; i < linesOfFile.Length; i++)
            {
                words[i] = new Word(linesOfFile[i]);
            }
            this.dictionary_name = fileName;
        }

        
    }
}
