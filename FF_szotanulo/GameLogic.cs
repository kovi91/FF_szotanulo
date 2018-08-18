using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FF_szotanulo
{
    public class GameLogic
    {
        WordDictionary[] collections;
        Random r;
        public GameLogic()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = di.GetFiles("*.words");

            collections = new WordDictionary[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                collections[i] = new WordDictionary(File.ReadAllLines(files[i].FullName, Encoding.UTF8), files[i].Name.Split('.')[0]);
            }
            r = new Random();
        }

        public string[] GetDictionaries()
        {
            string[] dictionaryNames = new string[collections.Length];
            for (int i = 0; i < collections.Length; i++)
            {
                dictionaryNames[i] = collections[i].Dictionary_name;
            }
            return dictionaryNames;
        }

        public void EndGame()
        {
            for (int i = 0; i < collections.Length; i++)
            {
                StreamWriter sw = new StreamWriter(collections[i].Dictionary_name + ".words", false, Encoding.UTF8);
                for (int j = 0; j < collections[i].Words.Length; j++)
                {
                    sw.WriteLine(collections[i].Words[j].ExportLine);
                }
                sw.Close();
            }
        }

        public LevelViewModel StartGame(int id = -1, int size = 10)
        {
            LevelViewModel lvm = null;
            if (id == -1)
            {
                PrepareMultiple(ref lvm, size);
            }
            else
            {
                PrepareSingle(ref lvm, id, size);
            }

            return lvm;
        }

        public void PrepareSingle(ref LevelViewModel lvm, int id, int size)
        {
            if (size > collections[id].Words.Length)
            {
                size = collections[id].Words.Length;
            }

            lvm = new LevelViewModel(size);

            for (int i = 0; i < size; i++)
            {
                lvm.AddWordViewModel(collections[id].Words[r.Next(0, collections[id].Words.Length)]);
            }

            MixWords(lvm);
        }

        public void PrepareMultiple(ref LevelViewModel lvm, int size)
        {
            lvm = new LevelViewModel(size);

            for (int i = 0; i < size; i++)
            {
                int collection_random_id = r.Next(0, collections.Length);
                int word_random_id = r.Next(0, collections[collection_random_id].Words.Length);
                lvm.AddWordViewModel(collections[collection_random_id].Words[word_random_id]);
            }

            MixWords(lvm);
        }

        public void MixWords(LevelViewModel lvm)
        {
            Word tmp;
            for (int i = 0; i < 10000; i++)
            {
                int a = r.Next(0, lvm.LevelWords.Length);
                int b = r.Next(0, lvm.LevelWords.Length);
                tmp = lvm.LevelWords[a];
                lvm.LevelWords[a] = lvm.LevelWords[b];
                lvm.LevelWords[b] = tmp;
            }
        }
    }
}
