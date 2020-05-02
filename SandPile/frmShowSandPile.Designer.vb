<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowSandPile
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rtbSandBoxArea = New System.Windows.Forms.RichTextBox()
        Me.btnOneTopple = New System.Windows.Forms.Button()
        Me.btnAdd3s = New System.Windows.Forms.Button()
        Me.txtSBInfo = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'rtbSandBoxArea
        '
        Me.rtbSandBoxArea.Location = New System.Drawing.Point(12, 12)
        Me.rtbSandBoxArea.Name = "rtbSandBoxArea"
        Me.rtbSandBoxArea.ReadOnly = True
        Me.rtbSandBoxArea.Size = New System.Drawing.Size(771, 501)
        Me.rtbSandBoxArea.TabIndex = 0
        Me.rtbSandBoxArea.Text = ""
        Me.rtbSandBoxArea.WordWrap = False
        '
        'btnOneTopple
        '
        Me.btnOneTopple.Location = New System.Drawing.Point(12, 547)
        Me.btnOneTopple.Name = "btnOneTopple"
        Me.btnOneTopple.Size = New System.Drawing.Size(103, 27)
        Me.btnOneTopple.TabIndex = 1
        Me.btnOneTopple.Text = "One Topple"
        Me.btnOneTopple.UseVisualStyleBackColor = True
        '
        'btnAdd3s
        '
        Me.btnAdd3s.Location = New System.Drawing.Point(12, 580)
        Me.btnAdd3s.Name = "btnAdd3s"
        Me.btnAdd3s.Size = New System.Drawing.Size(103, 27)
        Me.btnAdd3s.TabIndex = 2
        Me.btnAdd3s.Text = "Add 3s"
        Me.btnAdd3s.UseVisualStyleBackColor = True
        '
        'txtSBInfo
        '
        Me.txtSBInfo.Location = New System.Drawing.Point(12, 519)
        Me.txtSBInfo.Name = "txtSBInfo"
        Me.txtSBInfo.ReadOnly = True
        Me.txtSBInfo.Size = New System.Drawing.Size(771, 22)
        Me.txtSBInfo.TabIndex = 3
        '
        'frmShowSandPile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 613)
        Me.Controls.Add(Me.txtSBInfo)
        Me.Controls.Add(Me.btnAdd3s)
        Me.Controls.Add(Me.btnOneTopple)
        Me.Controls.Add(Me.rtbSandBoxArea)
        Me.Name = "frmShowSandPile"
        Me.Text = "Show Pile"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents rtbSandBoxArea As RichTextBox
    Friend WithEvents btnOneTopple As Button
    Friend WithEvents btnAdd3s As Button
    Friend WithEvents txtSBInfo As TextBox
End Class
