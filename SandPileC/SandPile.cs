using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SandPileC.SandPileUtils;

namespace SandPileC
{
    public class SandPile : ICloneable, IComparable<SandPile>
    {
        public readonly int Width;
        public readonly int Height;
        public InSetStatus InSet = InSetStatus.Unknown;
        public readonly SandPile MyZero;
        public enum InSetStatus
        {
            NoCheck,
            Unknown,
            InSet,
            NotInSet,
            NoZero
        }
        private int[,] prvSandPileArray;
        public int[,] SandPileArray
        {
            get
            {
                return prvSandPileArray;
            }
            private set
            {
                prvSandPileArray = value;
            }
        }
        public bool NeedTopple()
        {
            foreach (var thisVal in SandPileArray)
            {
                if (thisVal > 3)
                    return true;
            }
            return false;
        }
        public static List<SandPile> LoadKnownZeros
        {
            get
            {
                var retList = new List<SandPile>();
                retList.Add(new SandPile(
                    new int[,] { { 2, 1, 2 }, { 1, 0, 1 }, { 2, 1, 2 } },
                    3, 3, true));
                retList.Add(new SandPile(
                    new int[,] { { 2, 3, 3, 2 }, { 3, 2, 2, 3 }, { 3, 2, 2, 3 }, { 2, 3, 3, 2 } },
                    4, 4, true));
                retList.Add(new SandPile(
                    new int[,] { { 2, 3, 2, 3, 2 }, { 3, 2, 1, 2, 3 }, { 2, 1, 0, 1, 2 }, { 3, 2, 1, 2, 3 }, { 2, 3, 2, 3, 2 } },
                    5, 5, true));
                return retList;
            }
            set { }
        }
        public void ToppleMe()
        {
            SandPile meCopy = (SandPile)this.Clone();
            while (!!meCopy.NeedTopple())
                meCopy = SandPile.OneTopple(meCopy);
            SandPileArray = (int[,])meCopy.SandPileArray.Clone();
        }
        public static int[,] EquivArrayAllSame(int[,] ArrayIn, int InitVal = 0)
        {
            int[,] RetArray = (int[,])ArrayIn.Clone();
            for (int thisColNum = 0; thisColNum <= ArrayIn.GetUpperBound(0); thisColNum++)
            {
                for (int thisRowNum = 0; thisRowNum <= ArrayIn.GetUpperBound(1); thisRowNum++)
                    RetArray[thisColNum, thisRowNum] = InitVal;
            }
            return RetArray;
        }
        public void Add(SandPile SBToAdd, bool FullTopple = false)
        {
            if (SBToAdd.Width != Width)
                throw new Exception("Cannot add a sandpile of different width: " + Width + " vs " + SBToAdd.Width);
            if (SBToAdd.Height != Height)
                throw new Exception("Cannot add a sandpile of different height: " + Height + " vs " + SBToAdd.Height);
            for (int thisColNum = 0; thisColNum <= Width - 1; thisColNum++)
            {
                for (int thisRowNum = 0; thisRowNum <= Height - 1; thisRowNum++)
                {
                    int thisVal = SBToAdd.SandPileArray[thisColNum, thisRowNum];
                    SandPileArray[thisColNum, thisRowNum] += thisVal;
                }
            }
            // After an add, need to check inset.  Only way to ensure that inset follows is to know that what was added was inset.
            if (InSet != InSetStatus.NoCheck) InSet = CheckInSet();
            if (FullTopple)
                ToppleMe();
        }
        public void AddAtSpot(int grainsToAdd, int colLoc, int rowLoc)
        {
            if (colLoc > Width - 1 || rowLoc > Height - 1) throw new ArgumentOutOfRangeException("Position of add is invalid for this sandpile");
            SandPileArray[colLoc, rowLoc] += grainsToAdd;
        }
        public SandPile MeFull()
        {
            return new SandPile(EquivArrayAllSame(SandPileArray, 3), Width, Height, false, InSet);
        }
        public InSetStatus CheckInSet()
        {
            if (MyZero == null) return InSetStatus.NoZero; 
            InSet = InSetStatus.NoCheck;
            SandPile meCopy = (SandPile)this.Clone();
            meCopy.Add(MyZero);
            if (this.CompareFullyToppled(meCopy) == 0) return InSetStatus.InSet;
            else return InSetStatus.NotInSet;
        }
        public int TotGrains()
        {
            var totGrains = 0;
            foreach (var thisTot in SandPileArray) totGrains += thisTot;
            return totGrains;
        }
        public static SandPile OneTopple(SandPile _SandPile)
        {
            if (!_SandPile.NeedTopple())
                return _SandPile;
            var startGrains = _SandPile.TotGrains();
            int[,] ArrayOut = EquivArrayAllSame(_SandPile.SandPileArray);
            for (int thisColNum = 0; thisColNum <= _SandPile.Width - 1; thisColNum++)
            {
                for (int thisRowNum = 0; thisRowNum <= _SandPile.Height - 1; thisRowNum++)
                {
                    int thisVal = _SandPile.SandPileArray[thisColNum, thisRowNum];
                    if (thisVal > 3)
                    {
                        ArrayOut[thisColNum, thisRowNum] += thisVal - 4;
                        if (thisRowNum > 0)
                            ArrayOut[thisColNum, thisRowNum - 1] += 1;
                        if (thisRowNum < _SandPile.Height - 1)
                            ArrayOut[thisColNum, thisRowNum + 1] += 1;
                        if (thisColNum > 0)
                            ArrayOut[thisColNum - 1, thisRowNum] += 1;
                        if (thisColNum < _SandPile.Width - 1)
                            ArrayOut[thisColNum + 1, thisRowNum] += 1;
                    }
                    else
                        ArrayOut[thisColNum, thisRowNum] += thisVal;
                }
            }
            var retSP = new SandPile(ArrayOut, _SandPile.Width, _SandPile.Height, false, _SandPile.InSet);
            if (retSP.TotGrains() > startGrains) throw new Exception($"This topple gained grains! Orig: {_SandPile.ToString()} Toppled: {retSP.ToString()}");
            return retSP;
        }
        private static int[,] ArrayOfArrays2TwoDim(int[][] Elements, int _Width = 3, int _Height = 3)
        {
            int[,] twoDim = new int[_Width, _Height];
            for (int i = 0; i < _Width; i++)
            {
                for (int j = 0; j < _Height; j++)
                {
                    twoDim[i, j] = Elements[j][i];
                }
            }
            return twoDim;
        }
        public SandPile(
            int[][] Elements,
            int _Width = 3,
            int _Height = 3) :
            this(ArrayOfArrays2TwoDim(Elements, _Width, _Height), _Width, _Height, false, InSetStatus.Unknown)
        { }
        public SandPile(
            int[,] Elements,
            int _Width = 3,
            int _Height = 3) :
            this(Elements, _Width, _Height, false, InSetStatus.Unknown)
        { }
        private SandPile(
            int[][] Elements,
            int _Width,
            int _Heigh,
            bool isZero,
            InSetStatus _inSet) :
            this(ArrayOfArrays2TwoDim(Elements, _Width, 3), _Width, 3, isZero, _inSet)
        { }
        private SandPile(int[,] Elements,
            int _Width = 3,
            int _Height = 3,
            bool isZero = false,
            InSetStatus _inSet = InSetStatus.Unknown)
        {
            Width = _Width;
            Height = _Height;
            if (Elements.GetLowerBound(0) != 0)
                throw new Exception("Index of first dimension of array (Width) must be zero");
            if (Elements.GetUpperBound(0) + 1 != Width)
                throw new Exception("Width is " + Width + ". but first dimension of array is " + Elements.GetUpperBound(0) + 1);
            if (Elements.GetLowerBound(1) != 0)
                throw new Exception("Index of 2nd dimension (Height) of array must be zero");
            if (Elements.GetUpperBound(1) + 1 != Height)
                throw new Exception("Height is " + Height + ". but second dimension of array is " + Elements.GetUpperBound(1) + 1);
            SandPileArray = Elements;
            if (!isZero) MyZero = GetZero();
            if (_inSet == InSetStatus.Unknown) InSet = CheckInSet();
            else InSet = _inSet;
        }
        public SandPile GetZero()
        {
            return KnownZeros.OfDimension(this.Width, this.Height);
        }
        public string RawDump()
        {
            var retStr = "";
            for (int thisRowNum = 0; thisRowNum <= Height - 1; thisRowNum++)
            {
                for (int thisColNum = 0; thisColNum <= Width - 1; thisColNum++)
                {
                    var useVal = SandPileArray[thisColNum, thisRowNum];
                    if (useVal <= 9) retStr += useVal.ToString();
                    else retStr += "*";
                }
                if (thisRowNum != Height - 1) retStr += "\r\n";
            }
            return retStr;
        }
        public override string ToString()
        {
            var retStr = "-----------\r\n";
            retStr += $"NeedTopple: {NeedTopple()}\r\n";
            for (int thisRowNum = 0; thisRowNum <= Height - 1; thisRowNum++)
            {
                for (int thisColNum = 0; thisColNum <= Width - 1; thisColNum++)
                {
                    retStr += SandPileArray[thisColNum, thisRowNum].ToString().PadRight(4);
                }
                retStr += "\r\n";
            }
            retStr += "-----------\r\n";
            return retStr;
        }
        public object Clone()
        {
            return new SandPile((int[,])this.SandPileArray.Clone(), this.Width, this.Height, false, this.InSet);
        }
        ~SandPile()
        {
        }
        public int CompareTo(SandPile other)
        {
            if (Width != other.Width)
                return Width.CompareTo(other.Width);
            if (Height != other.Height)
                return Height.CompareTo(other.Height);
            for (int i = 0; i < SandPileArray.GetLength(0); i++)
                for (int j = 0; j < SandPileArray.GetLength(1); j++)
                {
                    if (SandPileArray[i, j].CompareTo(other.SandPileArray[i, j]) != 0) return SandPileArray[i, j].CompareTo(other.SandPileArray[i, j]);
                }
            return 0;
        }
        public int CompareFullyToppled(SandPile other)
        {
            if (Width != other.Width)
                return Width.CompareTo(other.Width);
            if (Height != other.Height)
                return Height.CompareTo(other.Height);
            SandPile origToppled = (SandPile)this.Clone();
            SandPile otherToppled = (SandPile)other.Clone();
            origToppled.ToppleMe();
            otherToppled.ToppleMe();
            for (int thisColNum = 0; thisColNum <= Width - 1; thisColNum++)
            {
                for (int thisRowNum = 0; thisRowNum <= Height - 1; thisRowNum++)
                {
                    if (origToppled.SandPileArray[thisRowNum, thisColNum] != otherToppled.SandPileArray[thisRowNum, thisColNum])
                        return origToppled.SandPileArray[thisRowNum, thisColNum].CompareTo(otherToppled.SandPileArray[thisRowNum, thisColNum]);
                }
            }
            return 0;
        }
        public int CompareDimensions(SandPile other)
        {
            if (Width.CompareTo(other.Width) != 0) return Width.CompareTo(other.Width);
            return Height.CompareTo(other.Height);
        }
    }
}
