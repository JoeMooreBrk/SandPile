Imports SandPileC
Public Class frmShowSandPile
    Public Property frmSandPile As SandPile
    Private Sub frmShowSandPile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub ShowMySandPile()
        SandPileUtils.PutSandPileInRichTextBox(rtbSandBoxArea, frmSandPile)
        If Not frmSandPile.InSet.HasValue Then
            txtSBInfo.Text = "No zero definition available"
        ElseIf frmSandPile.InSet.Value Then
            txtSBInfo.Text = "This sandbox is in set"
        Else
            txtSBInfo.Text = "This sandbox is not in set"
        End If
        CheckButtons()
    End Sub

    Private Sub btnOneTopple_Click(sender As Object, e As EventArgs) Handles btnOneTopple.Click
        If frmSandPile Is Nothing Then
            btnOneTopple.Enabled = False
            MsgBox("No sandpile loaded")
        End If
        frmSandPile = SandPile.OneTopple(frmSandPile)
        ShowMySandPile()
    End Sub

    Private Sub btnAdd3s_Click(sender As Object, e As EventArgs) Handles btnAdd3s.Click
        Dim all3sSP = SandPileUtils.All3s(frmSandPile.Width, frmSandPile.Height)
        frmSandPile.Add(all3sSP)
        ShowMySandPile()
    End Sub
    Private Sub CheckButtons()
        btnOneTopple.Enabled = frmSandPile.NeedTopple
    End Sub
End Class