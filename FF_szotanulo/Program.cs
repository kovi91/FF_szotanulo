using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_szotanulo
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLogic gl = new GameLogic();
            StartUp(gl);
            string response = "";
            do
            {
                Console.Clear();
                Console.WriteLine("New game? Y/N");
                response = Console.ReadLine();
                if (response == "Y" || response == "y")
                {
                    StartUp(gl);
                }
            } while (response == "Y" || response == "y");

            gl.EndGame();

        }

        static void StartUp(GameLogic gl)
        {
            Console.Clear();
            ListDictionaries(gl);
            Console.Write("Enter the number of the dictionary (or -1 for all): ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Number of words: ");
            int numberOfWords = int.Parse(Console.ReadLine());
            Play(id, numberOfWords, gl);
        }

        static void Play(int id, int numberOfWords, GameLogic gl)
        {
            LevelViewModel lvm = gl.StartGame(id, numberOfWords);
            for (int i = 0; i < lvm.LevelWords.Length; i++)
            {
                Ask(lvm.LevelWords[i]);
            }
        }

        static void Ask(Word word)
        {
            Console.Clear();
            Console.WriteLine(word.Hungarian + "    [response time: " + word.AvgResponseTime + " ms, bad tries: " + 
                word.BadTries + ", good tries: " + word.GoodTries + "]");
            Console.WriteLine();
            Console.Write("Enter translation: ");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string response = Console.ReadLine();
            sw.Stop();
            if (response == word.English)
            {
                Console.WriteLine("Good!");
                word.AddResult((int)sw.ElapsedMilliseconds, TryResult.good);
            }
            else
            {
                Console.WriteLine("Bad!");
                Console.WriteLine("Answer: " + word.English);
                word.AddResult((int)sw.ElapsedMilliseconds, TryResult.bad);
            }
            Console.WriteLine("Press <enter> to continue");
            Console.ReadLine();
        }

        static void ListDictionaries(GameLogic gl)
        {
            string[] names = gl.GetDictionaries();
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine(i + ": " + names[i]);
            }
        }
    }
}
