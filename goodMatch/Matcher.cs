using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace goodMatch
{
    class Matcher
    {
        public static string calcSequence(string sentence)
        {
            string sequence = "";

            while(sentence.Length > 0)
            {
                int occur = 0;
                for (int i = 0; i < sentence.Length; i++)
                {
                    if (Char.IsWhiteSpace(sentence[0]))
                    {
                        sentence = sentence.Replace(sentence[0].ToString(), string.Empty);
                    }
                    
                    if (sentence[0] == sentence[i])
                    {
                        occur++;
                    }
                }

                if(occur != 0)
                {
                    sequence += occur;
                    sentence = sentence.Replace(sentence[0].ToString(), string.Empty);
                }
            }
            return sequence;
        }

        public static string sumSequence(string sequence)
        {
            string sum = "";

            while (sequence.Length > 0)
            {
                if (sequence.Length == 1)
                {
                    sum += sequence;
                    sequence = string.Empty;
                }
                else
                {
                    sum += int.Parse(sequence[0].ToString()) + int.Parse(sequence[sequence.Length - 1].ToString());
                    sequence = sequence.Substring(1, sequence.Length - 2);
                }

            }

            return sum;
        }

        public static string findPercentage(string sequence)
        {
            string percent = sumSequence(sequence);

            while (percent.Length > 2)
            {
                //Console.WriteLine(percent);
                //Console.WriteLine(percent.Length);
                percent = sumSequence(percent);
            }

            return percent;
        }

        public static string appendSentence(string sentence, string percent)
        {
            if(int.Parse(percent) > 80)
            {
                sentence += " " + percent + "%, GOOD MATCH.";
            }
            else
            {
                sentence += " " + percent + "%";
            }

            return sentence;
        }

        public static bool validateInput(string name)
        {
            foreach(char z in name)
            {
                if (!Char.IsLetter(z))
                {
                    return false;
                }
            }

            return true;
        }

        public static string getInput(string name)
        {
            while (!validateInput(name))
            {
                Console.WriteLine("Invalid input, please enter a valid name:");
                name = Console.ReadLine();
            }

            return name;
        }

        public static List<string> readFromCSV(string filepath, string category)
        {
            List<string> list = new List<string>();
            Regex alphabetic = new Regex("^[A-Z]+$");
            Regex catCheck = new Regex("^[MF]$");

            if(File.Exists(filepath))
            {
                if(!filepath.ToLower().EndsWith(".csv"))
                {
                    Logger.Log("Invalid File Type: CSV file required");
                }
                else
                {
                    var readlines = File.ReadAllLines(filepath);
                    foreach (var line in readlines)
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 2)
                        {
                            data[0] = data[0].Trim().ToUpper();
                            data[1] = data[1].Trim().ToUpper();

                            if (alphabetic.IsMatch(data[0]))
                            {
                                if (catCheck.IsMatch(data[1]))
                                {
                                    if(data[1].Equals(category.ToUpper()))
                                    {
                                        if (!list.Contains(data[0]))
                                        {
                                            list.Add(data[0]);
                                        }
                                    }
                                }
                                else
                                {
                                    Logger.Log("Invalid Gender Category: Should be either m or f" + line);
                                }
                            }
                            else
                            {
                                Logger.Log("Invalid Name: Only Alphabetic Characters Allowed" + line);
                            }
                        }
                        else
                        {
                            Logger.Log("Invalid Input: Correct Format => [name], [m/f]" + line);
                        }
                    }
                } 
            }
            else
            {
                Logger.Log("File Does Not Exist");
            }
            
            return list;
        }

        public static void printList(List<string> list)
        {
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
        }
    }
}
