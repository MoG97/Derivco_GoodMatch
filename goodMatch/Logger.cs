using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace goodMatch
{
    public class Logger
    {
        //Write logs to text file
        public static void Log(string text)
        {
            File.AppendAllText("logs.txt", text + Environment.NewLine);
        }

        //Clear logs file
        public static void ClearTextFile()
        {
            File.WriteAllText("logs.txt", string.Empty);
        }
    }
}
