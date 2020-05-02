using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandPileC;

namespace SandPile.Test
{
    [TestClass]
    public class TallTower
    {
        [TestMethod]
        public void TenByTenwith100()
        {
            var tenByTen = SandPileUtils.All0s(5, 5);
            tenByTen.AddAtSpot(10000, 2, 2);
            ShowTest.doShow(tenByTen);
        }
    }
}
