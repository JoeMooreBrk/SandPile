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
            var tenByTen = SandPileUtils.All0s(11, 11);
            tenByTen.AddAtSpot(1000, 5, 5);
            ShowTest.doShow(tenByTen);
        }
    }
}
