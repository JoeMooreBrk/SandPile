Public Class Main


    Private Class SandBox
        Implements ICloneable
        Implements IComparable(Of SandBox)
        Public ReadOnly Width As Integer
        Public ReadOnly Height As Integer
        Private prvSandBoxArray(,) As Integer
        Public Shared Zero3x3 As New SandBox({{2, 1, 2}, {1, 0, 1}, {2, 1, 2}})
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
            Dim meCopy As SandBox = Me.Clone
            Do Until Not meCopy.NeedTopple()
                meCopy = SandBox.OneTopple(meCopy)
            Loop
            SandBoxArray = meCopy.SandBoxArray.Clone
        End Sub
        Public Shared Function EquivArrayAsZeroes(ArrayIn(,) As Integer) As Integer(,)
            Dim RetArray(,) As Integer = ArrayIn.Clone
            For thisColNum As Integer = 0 To ArrayIn.GetUpperBound(0)
                For thisRowNum As Integer = 0 To ArrayIn.GetUpperBound(1)
                    RetArray(thisColNum, thisRowNum) = 0
                Next
            Next
            Return RetArray
        End Function
        Public Sub Add(SBToAdd As SandBox, Optional FullTopple As Boolean = False)
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
        Public Function InSet() As Boolean
            If Width <> 3 Or Height <> 3 Then Throw New Exception("Only 3x3 can currently be checked")
            Dim meCopy As SandBox = Me.Clone
            meCopy.Add(Zero3x3)
            Return Me.CompareTo(meCopy) = 0
        End Function
        Public Shared Function OneTopple(_SandBox As SandBox) As SandBox
            If Not _SandBox.NeedTopple Then Return _SandBox
            Dim ArrayOut(,) As Integer = EquivArrayAsZeroes(_SandBox.SandBoxArray)
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
            Return New SandBox(ArrayOut, _SandBox.Width, _SandBox.Height)
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
            Return DirectCast(MemberwiseClone(), SandBox)
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Public Function CompareTo(other As SandBox) As Integer Implements IComparable(Of SandBox).CompareTo
            If Width <> other.Width Then Return Width.CompareTo(other.Width)
            If Height <> other.Height Then Return Height.CompareTo(other.Height)
            Dim origToppled As SandBox = Me.Clone
            Dim otherToppled As SandBox = other.Clone
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

        Dim SBArrIn(,) = {{3, 3, 3}, {3, 2, 3}, {3, 3, 3}}
        Dim SBArrAdd(,) = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}

        Try
            Dim SBIn As New SandBox(SBArrAdd)
            Dim SBCopy As SandBox = SBIn.Clone
            SBIn.Add(SandBox.Zero3x3)
            MsgBox("In set: " & SBIn.InSet)
            'MsgBox(SBCopy.CompareTo(SBIn))
            'SBCopy.ToppleMe()
            MsgBox(SBCopy.ToString)
            Do Until Not SBIn.NeedTopple
                SBIn = SandBox.OneTopple(SBIn)
                MsgBox(SBIn.ToString)
            Loop
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try

    End Sub

End Class
