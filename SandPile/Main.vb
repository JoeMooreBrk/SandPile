Imports SandPileC
Imports SandPileC.SandPileUtils

Public Class Main

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SelWidth.Text = 3
        SelHeight.Text = 3

        Dim SBArrIn(,) = {{1, 1, 1}, {2, 2, 2}, {3, 3, 3}}
        Dim SBArrAdd(,) = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}

        Try
            Dim SBIn As New SandPile(SBArrAdd)
            MsgBox(New SandPile(SBArrIn).ToString)
            Dim SBCopy As SandPile = SBIn.Clone
            SBIn.Add(KnownZeros.OfDimension(3, 3))
            MsgBox("In set: " & SBIn.InSet)
            'MsgBox(SBCopy.CompareTo(SBIn))
            'SBCopy.ToppleMe()
            MsgBox(SBCopy.ToString)
            Do Until Not SBIn.NeedTopple
                SBIn = SandPile.OneTopple(SBIn)
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
        Dim thisSandPile As New SandPile(thisArr, SelWidth.Text, SelHeight.Text)
        MsgBox(thisSandPile.ToString)
        Do Until thisSandPile.NeedTopple = False
            thisSandPile = SandPile.OneTopple(thisSandPile)
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
            Dim randPile As New SandPile(thisArr, SelWidth.Text, SelHeight.Text)
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
        Dim myArrZero(,) As Integer = SandPile.EquivArrayAllSame(myArr)
        myArrZero(50, 50) = 1032 * 1032
        Dim mySP As New SandPile(myArrZero, 101, 101)
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
