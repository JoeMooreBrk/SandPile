using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandPileC;
using static SandPileC.SandPile;
using static SandPileC.SandPileUtils;

namespace SandPile.Test
{
    [TestClass]
    public class TestInitVals
    {
        [TestMethod]
        public void CheckChars()
        {
            var twelve = "12";
            var asChars = twelve.ToArray();
            var foo = Convert.ToInt32(asChars[1].ToString());
        }
        [TestMethod]
        public void TestStringIn()
        {
            var test1 = @"0153
3210
1111
2222
0303";
            var sand1 = new SandPileFromStrings(test1);
            Assert.AreEqual(test1, sand1.SandPile.RawDump());
            var str = sand1.SandPile.ToString();
            Assert.AreEqual(InSetStatus.NoZero, sand1.SandPile.InSet);
            str = sand1.SandPile.ToString();
            sand1.SandPile.ToppleMe();
            var numInSet = 0;
            var all3s = All3s(3, 3);
            for (int i = 0; i < 20; i++)
            {
                var rand33 = GetRandomSandPile(3, 3);
                if (rand33.InSet == InSetStatus.InSet) numInSet += 1;
                rand33.Add(all3s);
                rand33.ToppleMe();
                Assert.AreEqual(InSetStatus.InSet, rand33.InSet);
            }
            var all2sStr = @"222
222
222";
            var all2SP = new SandPileFromStrings(all2sStr);
            Assert.AreEqual(InSetStatus.InSet, all2SP.SandPile.InSet);
        }
    }
}
