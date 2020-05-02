using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SandPileC
{
    public class SandPileUtils
    {
        public class SandPileFromStrings
        {
            public string StringIn { get; private set; }
            public bool IsValid { get; private set; }
            public SandPile SandPile { get; private set; }
            public string Message { get; private set; }
            public SandPileFromStrings(string stringIn)
            {
                StringIn = stringIn;
                SandPile = null;
                IsValid = false;
                // This has the effect of working for any combination of CR and LF but will ignore any blank lines
                var spRows = stringIn.Split('\r', '\n').
                    Where(r => !string.IsNullOrEmpty(r.Trim())).
                    Select(r => r.Trim()).
                    ToArray();
                // Make sure all rows are of same length
                if (spRows.
                    GroupBy(r => r.Length).
                    ToList().Count() != 1)
                {
                    Message = "Different numbers of columns in rows";
                    return;
                }
                if (spRows.
                    Where(r => !OnlyDigits(r)).Count() != 0)
                {
                    Message = "Value other than digit in data";
                }
                var rowColArr = spRows.
                    Select(r => r.Select(c => Convert.ToInt32(c.ToString())).ToArray()).
                    ToArray();
                SandPile = new SandPile(rowColArr, rowColArr[0].Count(), rowColArr.Count());
                IsValid = true;
            }
        }
        private static Random randGen = new Random();
        public static SandPile GetRandomSandPile(int _width, int _height)
        {
            int[,] randArr = new int[_width, _height];
            for (int i = 0; i < randArr.GetLength(0); i++)
                for (int j = 0; j < randArr.GetLength(1); j++)
                {
                    randArr[i, j] = randGen.Next() % 4;
                }
            return new SandPile(randArr, _width, _height);
        }
        public static SandPile All3s(int _width, int _height)
        {
            int[,] retArr = new int[_width, _height];
            for (int i = 0; i < retArr.GetLength(0); i++)
                for (int j = 0; j < retArr.GetLength(1); j++)
                {
                    retArr[i, j] = 3;
                }
            return new SandPile(retArr, _width, _height);
        }
        public static SandPile All0s(int _width, int _height)
        {
            int[,] retArr = new int[_width, _height];
            for (int i = 0; i < retArr.GetLength(0); i++)
                for (int j = 0; j < retArr.GetLength(1); j++) retArr[i, j] = 0;
            return new SandPile(retArr, _width, _height);
        }
        public static void PutSandPileInRichTextBox(RichTextBox richTextBox, SandPile sandPile)
        {
            richTextBox.Text = sandPile.RawDump();
            var zeroTo3 = new Regex("^[0-3]$");
            for (int i = 0; i < richTextBox.Text.Length; i++)
            {
                var thisChar = richTextBox.Text.Substring(i, 1);
                richTextBox.Select(i, 1);
                if (!zeroTo3.IsMatch(thisChar)) richTextBox.SelectionColor = Color.Red;
                else
                {
                    switch (thisChar)
                    {
                        case "0": richTextBox.SelectionColor = Color.Gray; break;
                        case "1": richTextBox.SelectionColor = Color.Green; break;
                        case "2": richTextBox.SelectionColor = Color.Blue; break;
                        case "3": richTextBox.SelectionColor = Color.DarkViolet; break;
                        default: throw new Exception($"Unknown character: {thisChar}");
                    }
                }
            }
            richTextBox.Select(0, 0);
        }
        private static Regex digRegEx = new Regex("^[0-9]*$");
        public static bool OnlyDigits(string stringIn) => digRegEx.IsMatch(stringIn);
    }
}
