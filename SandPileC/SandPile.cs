﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SandPileC
{
    public static class SandPileInit
    {
        public static List<SandPile> KnownZeros = LoadZeroes.LoadKnownZeros;
    }
    public class LoadZeroes
    {
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
    }
    public class SandPile : ICloneable, IComparable<SandPile>
    {
        public readonly int Width;
        public readonly int Height;
        public readonly SandPile MyZero;
        private int[,] prvSandBoxArray;
        public int[,] SandBoxArray
        {
            get
            {
                return prvSandBoxArray;
            }
            private set
            {
                prvSandBoxArray = value;
            }
        }
        public bool NeedTopple()
        {
            foreach (var thisVal in SandBoxArray)
            {
                if (thisVal > 3)
                    return true;
            }
            return false;
        }
        public void ToppleMe()
        {
            SandPile meCopy = (SandPile)this.Clone();
            while (!!meCopy.NeedTopple())
                meCopy = SandPile.OneTopple(meCopy);
            SandBoxArray = (int[,])meCopy.SandBoxArray.Clone();
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
                throw new Exception("Cannot add a sandbox of different width: " + Width + " vs " + SBToAdd.Width);
            if (SBToAdd.Height != Height)
                throw new Exception("Cannot add a sandbox of different height: " + Height + " vs " + SBToAdd.Height);
            for (int thisColNum = 0; thisColNum <= Width - 1; thisColNum++)
            {
                for (int thisRowNum = 0; thisRowNum <= Height - 1; thisRowNum++)
                {
                    int thisVal = SBToAdd.SandBoxArray[thisColNum, thisRowNum];
                    SandBoxArray[thisColNum, thisRowNum] += thisVal;
                }
            }
            if (FullTopple)
                ToppleMe();
        }
        public void AddAtSpot(int grainsToAdd, int colLoc, int rowLoc)
        {
            if (colLoc > Width - 1 || rowLoc > Height - 1) throw new ArgumentOutOfRangeException("Position of add is invalid for this sandpile");
            SandBoxArray[colLoc, rowLoc] += grainsToAdd;
        }
        public SandPile MeFull()
        {
            return new SandPile(EquivArrayAllSame(SandBoxArray, 3), Width, Height);
        }
        public bool? InSet()
        {
            if (MyZero == null) return null;
            SandPile meCopy = (SandPile)this.Clone();
            meCopy.Add(MyZero);
            return this.CompareFullyToppled(meCopy) == 0;
        }
        public int TotGrains()
        {
            var totGrains = 0;
            foreach (var thisTot in SandBoxArray) totGrains += thisTot;
            return totGrains;
        }
        public static SandPile OneTopple(SandPile _SandBox)
        {
            if (!_SandBox.NeedTopple())
                return _SandBox;
            var startGrains = _SandBox.TotGrains();
            int[,] ArrayOut = EquivArrayAllSame(_SandBox.SandBoxArray);
            for (int thisColNum = 0; thisColNum <= _SandBox.Width - 1; thisColNum++)
            {
                for (int thisRowNum = 0; thisRowNum <= _SandBox.Height - 1; thisRowNum++)
                {
                    int thisVal = _SandBox.SandBoxArray[thisColNum, thisRowNum];
                    if (thisVal > 3)
                    {
                        ArrayOut[thisColNum, thisRowNum] += thisVal - 4;
                        if (thisRowNum > 0)
                            ArrayOut[thisColNum, thisRowNum - 1] += 1;
                        if (thisRowNum < _SandBox.Height - 1)
                            ArrayOut[thisColNum, thisRowNum + 1] += 1;
                        if (thisColNum > 0)
                            ArrayOut[thisColNum - 1, thisRowNum] += 1;
                        if (thisColNum < _SandBox.Width - 1)
                            ArrayOut[thisColNum + 1, thisRowNum] += 1;
                    }
                    else
                        ArrayOut[thisColNum, thisRowNum] += thisVal;
                }
            }
            var retSP = new SandPile(ArrayOut, _SandBox.Width, _SandBox.Height);
            if (retSP.TotGrains() > startGrains) throw new Exception($"This topple gained grains! Orig: {_SandBox.ToString()} Toppled: {retSP.ToString()}");
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
        public SandPile(int[][] Elements, int _Width = 3, int _Height = 3, bool isZero = false) :
            this(ArrayOfArrays2TwoDim(Elements, _Width, _Height), _Width, _Height, isZero)
        { }
        public SandPile(int[,] Elements, int _Width = 3, int _Height = 3, bool isZero = false)
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
            SandBoxArray = Elements;
            if (!isZero) MyZero = GetZero();
        }
        public SandPile GetZero()
        {
            var zerosOfDim = SandPileInit.KnownZeros.
                Where(s => s.CompareDimensions(this) == 0).
                ToList();
            if (zerosOfDim.Count() > 1) throw new Exception($"More than 1 zero of width {Width} and height {Height} exist");
            if (zerosOfDim.Count() == 0) return null;
            return zerosOfDim[0];
        }
        public string RawDump()
        {
            var retStr = "";
            for (int thisRowNum = 0; thisRowNum <= Height - 1; thisRowNum++)
            {
                for (int thisColNum = 0; thisColNum <= Width - 1; thisColNum++)
                {
                    var useVal = SandBoxArray[thisColNum, thisRowNum];
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
                    retStr += SandBoxArray[thisColNum, thisRowNum].ToString().PadRight(4);
                }
                retStr += "\r\n";
            }
            retStr += "-----------\r\n";
            return retStr;
        }
        public object Clone()
        {
            return new SandPile((int[,])this.SandBoxArray.Clone(), this.Width, this.Height);
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
            for (int i = 0; i < SandBoxArray.GetLength(0); i++)
                for (int j = 0; j < SandBoxArray.GetLength(1); j++)
                {
                    if (SandBoxArray[i, j].CompareTo(other.SandBoxArray[i, j]) != 0) return SandBoxArray[i, j].CompareTo(other.SandBoxArray[i, j]);
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
                    if (origToppled.SandBoxArray[thisRowNum, thisColNum] != otherToppled.SandBoxArray[thisRowNum, thisColNum])
                        return origToppled.SandBoxArray[thisRowNum, thisColNum].CompareTo(otherToppled.SandBoxArray[thisRowNum, thisColNum]);
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
