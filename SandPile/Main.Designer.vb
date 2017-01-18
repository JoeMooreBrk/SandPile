<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SelWidth = New System.Windows.Forms.TextBox()
        Me.SelHeight = New System.Windows.Forms.TextBox()
        Me.InputButton = New System.Windows.Forms.Button()
        Me.ShowResultButton = New System.Windows.Forms.Button()
        Me.RandButton = New System.Windows.Forms.Button()
        Me.TestButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Width:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Height:"
        '
        'SelWidth
        '
        Me.SelWidth.Location = New System.Drawing.Point(67, 18)
        Me.SelWidth.Name = "SelWidth"
        Me.SelWidth.Size = New System.Drawing.Size(46, 20)
        Me.SelWidth.TabIndex = 2
        '
        'SelHeight
        '
        Me.SelHeight.Location = New System.Drawing.Point(67, 44)
        Me.SelHeight.Name = "SelHeight"
        Me.SelHeight.Size = New System.Drawing.Size(46, 20)
        Me.SelHeight.TabIndex = 3
        '
        'InputButton
        '
        Me.InputButton.Location = New System.Drawing.Point(15, 80)
        Me.InputButton.Name = "InputButton"
        Me.InputButton.Size = New System.Drawing.Size(86, 29)
        Me.InputButton.TabIndex = 4
        Me.InputButton.Text = "Input"
        Me.InputButton.UseVisualStyleBackColor = True
        '
        'ShowResultButton
        '
        Me.ShowResultButton.Location = New System.Drawing.Point(15, 147)
        Me.ShowResultButton.Name = "ShowResultButton"
        Me.ShowResultButton.Size = New System.Drawing.Size(86, 29)
        Me.ShowResultButton.TabIndex = 5
        Me.ShowResultButton.Text = "Show Result"
        Me.ShowResultButton.UseVisualStyleBackColor = True
        '
        'RandButton
        '
        Me.RandButton.Location = New System.Drawing.Point(193, 80)
        Me.RandButton.Name = "RandButton"
        Me.RandButton.Size = New System.Drawing.Size(86, 29)
        Me.RandButton.TabIndex = 6
        Me.RandButton.Text = "Rand"
        Me.RandButton.UseVisualStyleBackColor = True
        '
        'TestButton
        '
        Me.TestButton.Location = New System.Drawing.Point(193, 115)
        Me.TestButton.Name = "TestButton"
        Me.TestButton.Size = New System.Drawing.Size(86, 29)
        Me.TestButton.TabIndex = 7
        Me.TestButton.Text = "Test"
        Me.TestButton.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 239)
        Me.Controls.Add(Me.TestButton)
        Me.Controls.Add(Me.RandButton)
        Me.Controls.Add(Me.ShowResultButton)
        Me.Controls.Add(Me.InputButton)
        Me.Controls.Add(Me.SelHeight)
        Me.Controls.Add(Me.SelWidth)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Main"
        Me.Text = "Manipulate Sandpiles"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SelWidth As System.Windows.Forms.TextBox
    Friend WithEvents SelHeight As System.Windows.Forms.TextBox
    Friend WithEvents InputButton As System.Windows.Forms.Button
    Friend WithEvents ShowResultButton As System.Windows.Forms.Button
    Friend WithEvents RandButton As System.Windows.Forms.Button
    Friend WithEvents TestButton As System.Windows.Forms.Button

End Class
