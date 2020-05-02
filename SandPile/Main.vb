Public Class Main

    <Obsolete("Use SandPileC.SandPile instead")>
    Private Class SandPileVB
        Implements ICloneable
        Implements IComparable(Of SandPileVB)
        Public ReadOnly Width As Integer
        Public ReadOnly Height As Integer
        Private prvSandBoxArray(,) As Integer
        Public Shared Zero3x3 As New SandPileVB({{2, 1, 2}, {1, 0, 1}, {2, 1, 2}})
        Public Shared Zero4x4 As New SandPileVB({{2, 3, 3, 2}, {3, 2, 2, 3}, {3, 2, 2, 3}, {2, 3, 3, 2}}, 4, 4)
        Public Shared Zero5x5 As New SandPileVB({{2, 3, 2, 3, 2}, {3, 2, 1, 2, 3}, {2, 1, 0, 1, 2}, {3, 2, 1, 2, 3}, {2, 3, 2, 3, 2}}, 5, 5)
        Public Property SandBoxArray As Integer(,)
            Get
                Return prvSandBoxArray
            End Get
            Private Set(value As Integer(,))
                prvSandBoxArray = value
            End Set
        End Property
        Public Function NeedTopple() As Boolean
            NeedTopple = False
            For Each thisVal In SandBoxArray
                If thisVal > 3 Then Return True
            Next
        End Function
        Public Sub ToppleMe()
            Dim meCopy As SandPileVB = Me.Clone
            Do Until Not meCopy.NeedTopple()
                meCopy = SandPileVB.OneTopple(meCopy)
            Loop
            SandBoxArray = meCopy.SandBoxArray.Clone
        End Sub
        Public Shared Function EquivArrayAllSame(ArrayIn(,) As Integer, Optional InitVal As Integer = 0) As Integer(,)
            Dim RetArray(,) As Integer = ArrayIn.Clone
            For thisColNum As Integer = 0 To ArrayIn.GetUpperBound(0)
                For thisRowNum As Integer = 0 To ArrayIn.GetUpperBound(1)
                    RetArray(thisColNum, thisRowNum) = InitVal
                Next
            Next
            Return RetArray
        End Function
        Public Sub Add(SBToAdd As SandPileVB, Optional FullTopple As Boolean = False)
            If SBToAdd.Width <> Width Then Throw New Exception("Cannot add a sandbox of different width: " & Width & " vs " & SBToAdd.Width)
            If SBToAdd.Height <> Height Then Throw New Exception("Cannot add a sandbox of different height: " & Height & " vs " & SBToAdd.Height)
            For thisColNum As Integer = 0 To Width - 1
                For thisRowNum As Integer = 0 To Height - 1
                    Dim thisVal As Integer = SBToAdd.SandBoxArray(thisColNum, thisRowNum)
                    SandBoxArray(thisColNum, thisRowNum) += thisVal
                Next
            Next
            If FullTopple Then ToppleMe()
        End Sub
        Public Function MeFull() As SandPileVB
            Return New SandPileVB(EquivArrayAllSame(SandBoxArray, 3), Width, Height)
        End Function
        Public Function MyZero() As SandPileVB
            If Width = 3 And Height = 3 Then Return Zero3x3
            If Width = 4 And Height = 4 Then Return Zero4x4
            If Width = 5 And Height = 5 Then Return Zero5x5
            Return Nothing
        End Function
        Public Function InSet() As Boolean
            If MyZero() Is Nothing Then Throw New Exception("Cannot calculate Inset for these dimensions")
            Dim meCopy As SandPileVB = Me.Clone
            meCopy.Add(MyZero)
            Return Me.CompareFullyToppled(meCopy) = 0
        End Function
        Public Function TotGrains() As Integer
            TotGrains = 0
            Dim meToTopple As SandPileVB = Me.Clone
            meToTopple.ToppleMe()
            For thisColNum As Integer = 0 To Width - 1
                For thisRowNum As Integer = 0 To Height - 1
                    TotGrains += meToTopple.SandBoxArray(thisColNum, thisRowNum)
                Next
            Next
        End Function
        Public Shared Function OneTopple(_SandBox As SandPileVB) As SandPileVB
            If Not _SandBox.NeedTopple Then Return _SandBox
            Dim ArrayOut(,) As Integer = EquivArrayAllSame(_SandBox.SandBoxArray)
            For thisColNum As Integer = 0 To _SandBox.Width - 1
                For thisRowNum As Integer = 0 To _SandBox.Height - 1
                    Dim thisVal As Integer = _SandBox.SandBoxArray(thisColNum, thisRowNum)
                    If thisVal > 3 Then
                        ArrayOut(thisColNum, thisRowNum) += thisVal - 4
                        If thisRowNum > 0 Then ArrayOut(thisColNum, thisRowNum - 1) += 1
                        If thisRowNum < _SandBox.Width - 1 Then ArrayOut(thisColNum, thisRowNum + 1) += 1
                        If thisColNum > 0 Then ArrayOut(thisColNum - 1, thisRowNum) += 1
                        If thisColNum < _SandBox.Height - 1 Then ArrayOut(thisColNum + 1, thisRowNum) += 1
                    Else
                        ArrayOut(thisColNum, thisRowNum) += thisVal
                    End If
                Next
            Next
            Return New SandPileVB(ArrayOut, _SandBox.Width, _SandBox.Height)
        End Function
        Public Sub New(Elements(,) As Integer, Optional _Width As Integer = 3, Optional _Height As Integer = 3)
            Width = _Width
            Height = _Height
            If Elements.GetLowerBound(0) <> 0 Then Throw New Exception("Index of first dimension of array (Width) must be zero")
            If Elements.GetUpperBound(0) + 1 <> Width Then Throw New Exception("Width is " & Width & ". but first dimension of array is " & Elements.GetUpperBound(0) + 1)
            If Elements.GetLowerBound(1) <> 0 Then Throw New Exception("Index of 2nd dimension (Height) of array must be zero")
            If Elements.GetUpperBound(1) + 1 <> Height Then Throw New Exception("Height is " & Height & ". but second dimension of array is " & Elements.GetUpperBound(1) + 1)
            SandBoxArray = Elements
        End Sub
        Public Overrides Function ToString() As String
            ToString = "-----------" & vbCrLf
            ToString &= "NeedTopple: " & NeedTopple() & vbCrLf
            For thisColNum As Integer = 0 To Width - 1
                For thisRowNum As Integer = 0 To Height - 1
                    ToString &= SandBoxArray(thisColNum, thisRowNum).ToString.PadRight(4)
                Next
                ToString &= vbCrLf
            Next
            ToString &= "-----------" & vbCrLf
        End Function
        Public Function Clone() As Object Implements ICloneable.Clone
            Return New SandPileVB(Me.SandBoxArray.Clone, Me.Width, Me.Height)
        End Function
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Public Function CompareTo(other As Main.SandPileVB) As Integer Implements IComparable(Of SandPileVB).CompareTo
            If Width <> other.Width Then Return Width.CompareTo(other.Width)
            If Height <> other.Height Then Return Height.CompareTo(other.Height)
            For i As Integer = 0 To SandBoxArray.GetLength(0) - 1
                For j As Integer = 0 To SandBoxArray.GetLength(1) - 1
                    If SandBoxArray(i, j).CompareTo(other.SandBoxArray(i, j)) <> 0 Then Return SandBoxArray(i, j).CompareTo(other.SandBoxArray(i, j)) <> 0
                Next
            Next
            Return 0
        End Function
        Public Function CompareFullyToppled(other As SandPileVB) As Integer
            If Width <> other.Width Then Return Width.CompareTo(other.Width)
            If Height <> other.Height Then Return Height.CompareTo(other.Height)
            Dim origToppled As SandPileVB = Me.Clone
            Dim otherToppled As SandPileVB = other.Clone
            origToppled.ToppleMe()
            otherToppled.ToppleMe()
            For thisColNum As Integer = 0 To Width - 1
                For thisRowNum As Integer = 0 To Height - 1
                    If origToppled.SandBoxArray(thisColNum, thisRowNum) <> otherToppled.SandBoxArray(thisColNum, thisRowNum) Then Return origToppled.SandBoxArray(thisColNum, thisRowNum).CompareTo(otherToppled.SandBoxArray(thisColNum, thisRowNum))
                Next
            Next
            Return 0
        End Function
    End Class

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SelWidth.Text = 3
        SelHeight.Text = 3

        Dim SBArrIn(,) = {{1, 1, 1}, {2, 2, 2}, {3, 3, 3}}
        Dim SBArrAdd(,) = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}

        Try
            Dim SBIn As New SandPileVB(SBArrAdd)
            MsgBox(New SandPileVB(SBArrIn).ToString)
            Dim SBCopy As SandPileVB = SBIn.Clone
            SBIn.Add(SandPileVB.Zero3x3)
            MsgBox("In set: " & SBIn.InSet)
            'MsgBox(SBCopy.CompareTo(SBIn))
            'SBCopy.ToppleMe()
            MsgBox(SBCopy.ToString)
            Do Until Not SBIn.NeedTopple
                SBIn = SandPileVB.OneTopple(SBIn)
                MsgBox(SBIn.ToString)
            Loop
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub InputButton_Click(sender As Object, e As EventArgs) Handles InputButton.Click

        Dim thisPA() As Integer = {SelWidth.Text, SelHeight.Text}
        Dim thisArr(,) As Integer = Array.CreateInstance(GetType(Integer), thisPA)
        For thisRowNum As Integer = 0 To thisArr.GetUpperBound(1)
            Dim csv As String = InputBox("Input Row " & thisRowNum + 1 & " of " & SelWidth.Text & " numbers separated by commas")
            Dim csvSpl() As String = Split(csv, ",")
            If csvSpl.Count <> SelWidth.Text Then
                MsgBox("Invalid number of cells")
                Exit Sub
            End If
            For thisColNum As Integer = 0 To csvSpl.Count - 1
                Dim thisVal As String = csvSpl(thisColNum)
                If Not IsNumeric(thisVal) OrElse thisVal <> Math.Floor(Convert.ToDecimal(thisVal)) OrElse thisVal < 0 Then
                    MsgBox("Each value must be an integer")
                    Exit Sub
                End If
                thisArr.SetValue(Convert.ToInt32(thisVal), {thisColNum, thisRowNum})
            Next
        Next
        MsgBox("Array successfully initialized")
        Dim thisSandPile As New SandPileVB(thisArr, SelWidth.Text, SelHeight.Text)
        MsgBox(thisSandPile.ToString)
        Do Until thisSandPile.NeedTopple = False
            thisSandPile = SandPileVB.OneTopple(thisSandPile)
            MsgBox(thisSandPile.ToString)
        Loop

    End Sub

    Private Sub RandButton_Click(sender As Object, e As EventArgs) Handles RandButton.Click

        Dim totBefore As Integer = 0
        Dim totAfter As Integer = 0
        Dim totHist(3) As Integer
        Dim thisRand As New Random
        For i As Integer = 1 To 1000
            Dim thisAD() As Integer = {SelWidth.Text, SelHeight.Text}
            Dim thisArr(,) As Integer = Array.CreateInstance(GetType(Integer), thisAD)
            For thisRowNum As Integer = 0 To thisArr.GetUpperBound(0)
                For thisColNum As Integer = 0 To thisArr.GetUpperBound(1)
                    Dim thisNext As Integer = thisRand.Next(1000 * 3) Mod 4
                    totHist(thisNext) += 1
                    thisArr(thisRowNum, thisColNum) = thisNext
                Next
            Next
            Dim randPile As New SandPileVB(thisArr, SelWidth.Text, SelHeight.Text)
            If randPile.InSet Then totBefore += 1
            ' MsgBox("Orig:" & vbCrLf & vbCrLf & randPile.ToString & vbCrLf & vbCrLf & "In Set: " & randPile.InSet)
            randPile.Add(randPile.MeFull)
            If randPile.InSet Then totAfter += 1
            ' MsgBox("After Adding:" & vbCrLf & vbCrLf & randPile.ToString & vbCrLf & vbCrLf & "In Set: " & randPile.InSet)
        Next
        MsgBox("Total Before: " & totBefore & " Total After: " & totAfter)
        MsgBox("Totals " & totHist(0) & " " & totHist(1) & " " & totHist(2) & " " & totHist(3))


    End Sub

    Private Sub TestButton_Click(sender As Object, e As EventArgs) Handles TestButton.Click

        Dim myArr(,) As Integer = Array.CreateInstance(GetType(Integer), {101, 101})
        Dim myArrZero(,) As Integer = SandPileVB.EquivArrayAllSame(myArr)
        myArrZero(50, 50) = 1032 * 1032
        Dim mySP As New SandPileVB(myArrZero, 101, 101)
        mySP.ToppleMe()
        MsgBox(mySP.ToString)
        MsgBox(mySP.TotGrains)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using showIt As New frmShowSandPile
            showIt.ShowDialog()
        End Using
    End Sub
End Class
