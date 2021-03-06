﻿using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandBox;
using SandPileC;

namespace SandPile.Test
{
    [TestClass]
    public class ShowTest
    {
        [TestMethod]
        public void RichText()
        {
            using (var shwPile = new frmShowSandPile())
            {
                var newSP = new SandPileUtils.SandPileFromStrings(@"131
343
131");
                //var zeroTo3 = new Regex("^[0-3]$");
                //for (int i = 0; i < shwPile.rtbSandPileArea.Text.Length; i++)
                //{
                //    var thisChar = shwPile.rtbSandPileArea.Text.Substring(i, 1);
                //    if (!zeroTo3.IsMatch(thisChar))
                //    {
                //        shwPile.rtbSandPileArea.Select(i, 1);
                //        shwPile.rtbSandPileArea.SelectionColor = Color.Red;
                //    }
                //}
                //SandPileUtils.PutSandPileInRichTextBox(shwPile.rtbSandPileArea, newSP.SandPile);
                shwPile.frmSandPile = newSP.SandPile;
                shwPile.ShowMySandPile();
                shwPile.ShowInTaskbar = false;
                shwPile.ShowDialog();
            }
        }
        public static void doShow(SandPileC.SandPile sandPile)
        {
            using (var shwPile = new frmShowSandPile())
            {
                shwPile.frmSandPile = sandPile;
                shwPile.ShowMySandPile();
                shwPile.ShowInTaskbar = false;
                shwPile.ShowDialog();
            }
        }
    }
}
