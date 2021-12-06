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
            //Clear old logs
            Logger.ClearTextFile();

            //Get input for player names
            Console.WriteLine("Please input the name of player 1:");
            string name1 = Matcher.getInput(Console.ReadLine().Trim());
            Console.WriteLine("Please input the name of player 2:");
            string name2 = Matcher.getInput(Console.ReadLine().Trim());
            
            //Instantiate Match class
            Match match1 = new Match(name1, name2);
            //Print result
            Console.WriteLine("\nScore: "+match1.getSentence()+"\n");

            //Get CSV file path
            Console.WriteLine("Please enter file path:");
            string filename = Console.ReadLine();

            //Read player names from file and add to applicable lists
            List<string> playersM = Matcher.readFromCSV(filename, "m");
            List<string> playersF = Matcher.readFromCSV(filename, "f");

            //Print lists of players by gender
            Console.WriteLine("\nFemale players:\n");
            Matcher.printList(playersF);
            Console.WriteLine("\nMale players:\n");
            Matcher.printList(playersM);
            
            //Create list of Match objects
            List<Match> matches = new List<Match>();

            //Run program for each player in first set against each player in second set
            //Store each match in matches list
            foreach (string m in playersM)
            {
                foreach (string f in playersF)
                {
                    Match matchup = new Match(m, f);
                    matches.Add(matchup);
                }
            }
            
            //*********REVERSE*********
            //Run program for each player in second set against each player in first set
            //Store each match in matches list
            foreach (string f in playersF)
            {
                foreach (string m in playersM)
                {
                    Match matchup = new Match(f, m);
                    matches.Add(matchup);
                }
            }

            //Sort match scores by percentage descending order
            //Sort alphabetically if multiple results are the same
            matches.Sort((a, b) =>
            {
                var percentageSort = b.getPercent().CompareTo(a.getPercent());
                return percentageSort != 0 ? percentageSort : a.getSentence().CompareTo(b.getSentence());
            });

            //Create StreamWriter object to write to text file
            TextWriter tw = new StreamWriter("output.txt");

            //Write scores and print results in textfile
            Console.WriteLine("\nScores:\n");
            foreach (Match tennis in matches)
            {
                Console.WriteLine(tennis.getSentence());
                tw.WriteLine(tennis.getSentence());
            }
            tw.Close();
            Console.WriteLine("\nResults printed in output.txt!\nLogs saved in logs.txt!");
        }
    }
}
