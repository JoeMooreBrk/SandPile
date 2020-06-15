Imports SandPileC
Public Class frmShowSandPile
    Public Property frmSandPile As SandPile
    Public NumTopples As Integer = 0
    Private WithEvents myTimer As New Timer
    Private compareSP As SandPile
    Private compareTopples As Nullable(Of Integer)
    Private Sub frmShowSandPile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumTopples = 0
    End Sub
    Public Sub ShowMySandPile()
        SandPileUtils.PutSandPileInRichTextBox(rtbSandBoxArea, frmSandPile)
        Select Case frmSandPile.InSet
            Case SandPile.InSetStatus.InSet
                txtSBInfo.Text = "This sandbox is in set"
            Case SandPile.InSetStatus.NotInSet
                txtSBInfo.Text = "This sandbox is not in set"
            Case SandPile.InSetStatus.NoZero
                txtSBInfo.Text = "No zero definition available"
            Case Else
                Throw New Exception($"Invalid status for sanbox in set: {frmSandPile.InSet.ToString}")
        End Select
        txtSBInfo.Text &= $"  Grains: {frmSandPile.TotGrains} Topples: {NumTopples}"
        CheckButtons()
    End Sub

    Private Sub btnOneTopple_Click(sender As Object, e As EventArgs) Handles btnOneTopple.Click
        If frmSandPile Is Nothing Then
            btnOneTopple.Enabled = False
            MsgBox("No sandpile loaded")
        End If
        frmSandPile = SandPile.OneTopple(frmSandPile)
        NumTopples += 1
        ShowMySandPile()
    End Sub

    Private Sub btnAdd3s_Click(sender As Object, e As EventArgs) Handles btnAdd3s.Click
        Dim all3sSP = SandPileUtils.All3s(frmSandPile.Width, frmSandPile.Height)
        frmSandPile.Add(all3sSP)
        ShowMySandPile()
    End Sub
    Private Sub CheckButtons()
        If Not frmSandPile.NeedTopple Then myTimer.Stop()
        btnOneTopple.Enabled = frmSandPile.NeedTopple
    End Sub
    Private Sub myTimer_Tick() Handles myTimer.Tick
        Dim keepSameCol = 2
        Dim keepSameRow = 2
        myTimer.Stop()
        If Not frmSandPile.NeedTopple Then myTimer.Stop()
        btnOneTopple_Click(Nothing, Nothing)
        myTimer.Start()
        If frmSandPile.SandBoxArray(0, 0) <> 0 AndAlso compareSP Is Nothing Then
            myTimer.Stop()
            compareSP = frmSandPile.Clone
            compareTopples = NumTopples
            MsgBox($"Saved sandpile with populated corner at {NumTopples} topples.  Press button again to restart.")
        ElseIf Not compareSP Is Nothing Then
            compareSP.AddAtSpot(frmSandPile.SandBoxArray(keepSameCol, keepSameRow) - compareSP.SandBoxArray(keepSameCol, keepSameRow), keepSameCol, keepSameRow)
            If (compareSP.CompareTo(frmSandPile) = 0) Then
                myTimer.Stop()
                MsgBox($"Cycle found at {NumTopples - compareTopples.Value}")
            End If
        End If
    End Sub

    Private Sub btnContinuous_Click(sender As Object, e As EventArgs) Handles btnContinuous.Click
        myTimer.Interval = 100
        myTimer.Start()
    End Sub
End Class