using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace goodMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.ClearTextFile();
            Console.WriteLine("Please input the name of player 1:");
            string name1 = Matcher.getInput(Console.ReadLine());
            
            Console.WriteLine("Please input the name of player 2:");
            string name2 = Matcher.getInput(Console.ReadLine());

            Match match1 = new Match(name1, name2);

            Console.WriteLine(match1.getSentence());

            Console.WriteLine("Please enter file path:");
            string filename = Console.ReadLine();

            List<string> playersM = Matcher.readFromCSV(filename, "m");
            List<string> playersF = Matcher.readFromCSV(filename, "f");

            Console.WriteLine("Female players:");
            Matcher.printList(playersF);
            Console.WriteLine("Male players:");
            Matcher.printList(playersM);
            

            List<Match> matches = new List<Match>();

            foreach (string m in playersM)
            {
                foreach (string f in playersF)
                {
                    Match matchup = new Match(m, f);
                    matches.Add(matchup);
                }
            }

            foreach (string f in playersF)
            {
                foreach (string m in playersM)
                {
                    Match matchup = new Match(f, m);
                    matches.Add(matchup);
                }
            }

            matches.Sort((a, b) =>
            {
                var percentageSort = b.getPercent().CompareTo(a.getPercent());
                return percentageSort != 0 ? percentageSort : a.getSentence().CompareTo(b.getSentence());
            });

            TextWriter tw = new StreamWriter("output.txt");

            foreach(Match tennis in matches)
            {
                Console.WriteLine(tennis.getSentence());
                tw.WriteLine(tennis.getSentence());
            }
            tw.Close();
        }
    }
}
