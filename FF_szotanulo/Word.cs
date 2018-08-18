using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_szotanulo
{
    public enum TryResult
    {
        bad, good
    }
    public class Word
    {
        string english;
        string hungarian;
        int avg_responsetime;
        int bad_tries;
        int good_tries;

        public string English
        {
            get { return this.english; }
            set { this.english = value; }
        }

        public string Hungarian
        {
            get { return this.hungarian; }
            set { this.hungarian = value; }
        }

        public int AvgResponseTime
        {
            get { return this.avg_responsetime; }
        }

        public int BadTries
        {
            get { return this.bad_tries; }
        }

        public int GoodTries
        {
            get { return this.good_tries; }
        }

        public Word(string import_line)
        {
            //tiger;tigris;1985;2;3
            //eng;hun;avg in ms;bad;good
            string[] pieces = import_line.Split(';');
            if (pieces.Length == 5)
            {
                this.english = pieces[0];
                this.hungarian = pieces[1];
                this.avg_responsetime = int.Parse(pieces[2]);
                this.bad_tries = int.Parse(pieces[3]);
                this.good_tries = int.Parse(pieces[4]);
            }
        }

        public string ExportLine
        {
            get
            {
                return this.english + ";" + this.hungarian + ";" + this.avg_responsetime + ";" + this.bad_tries + ";" + this.good_tries;
            }
        }

        public void AddResult(int response_time, TryResult result)
        {
            if (avg_responsetime == 0)
            {
                //this is just the first try of this word
                avg_responsetime = response_time;
            }
            else
            {
                //not the first try - calculate average
                avg_responsetime += response_time;
                avg_responsetime = avg_responsetime / 2;
            }
            switch (result)
            {
                case TryResult.bad:
                    this.bad_tries++;
                    break;
                case TryResult.good:
                    this.good_tries++;
                    break;
                default:
                    break;
            }
        }
    }
}
