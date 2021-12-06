using System;
using System.Collections.Generic;
using System.Text;

namespace goodMatch
{
    class Match
    {
        //Declare variables
        private string player1;
        private string player2;
        private string sentence;
        private string sequence;
        private int percent;

        //Constructor
        public Match(string player1, string player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            //Get process start time
            var startTime = DateTime.Now;

            //Log process start time
            Logger.Log("Starting matching process at " + startTime);
            
            //Run good match process
            sentence = (this.player1 + " matches " + this.player2).ToUpper();
            sequence = Matcher.calcSequence(sentence);
            percent = calcPercent(sequence);
            sentence = Matcher.appendSentence(sentence, percent.ToString());

            //Log execution time
            Logger.Log("Completed matching process");
            Logger.Log("Execution Time: " + (DateTime.Now - startTime));
        }

        //Store score in int 
        private int calcPercent(string sequence)
        {
            this.percent = int.Parse(Matcher.findPercentage(sequence));

            return this.percent;
        }

        //Accessors
        public string getPlayer1()
        {
            return player1;
        }

        public string getPlayer2()
        {
            return player2;
        }

        public string getSentence()
        {
            return sentence;
        }

        public string getSequence()
        {
            return sequence;
        }

        public int getPercent()
        {
            return percent;
        }
    }
}
