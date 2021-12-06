using System;
using System.Collections.Generic;
using System.Text;

namespace goodMatch
{
    class Match
    {
        private string player1;
        private string player2;
        private string sentence;
        private string sequence;
        private int percent;

        public Match(string player1, string player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            var startTime = DateTime.Now;
            Logger.Log("Starting matching process at " + startTime);
            sentence = (this.player1 + " matches " + this.player2).ToUpper();
            sequence = Matcher.calcSequence(sentence);
            percent = calcPercent(sequence);
            sentence = Matcher.appendSentence(sentence, percent.ToString());
            Logger.Log("Completed matching process");
            Logger.Log("Execution Time: " + (DateTime.Now - startTime));
        }

        private int calcPercent(string sequence)
        {
            this.percent = int.Parse(Matcher.findPercentage(sequence));

            return this.percent;
        }

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
