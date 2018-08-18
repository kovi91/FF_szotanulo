using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_szotanulo
{
    public class LevelViewModel
    {
        Word[] levelWords;
        int counter;
        public Word[] LevelWords
        {
            get { return levelWords; }
        }

        public LevelViewModel(int size)
        {
            this.levelWords = new Word[size];
        }

        public void AddWordViewModel(Word newItem)
        {
            levelWords[counter] = newItem;
            counter++;
        }
    }
}
