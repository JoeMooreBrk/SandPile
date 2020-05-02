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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Width:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Height:"
        '
        'SelWidth
        '
        Me.SelWidth.Location = New System.Drawing.Point(89, 22)
        Me.SelWidth.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SelWidth.Name = "SelWidth"
        Me.SelWidth.Size = New System.Drawing.Size(60, 22)
        Me.SelWidth.TabIndex = 2
        '
        'SelHeight
        '
        Me.SelHeight.Location = New System.Drawing.Point(89, 54)
        Me.SelHeight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SelHeight.Name = "SelHeight"
        Me.SelHeight.Size = New System.Drawing.Size(60, 22)
        Me.SelHeight.TabIndex = 3
        '
        'InputButton
        '
        Me.InputButton.Location = New System.Drawing.Point(20, 98)
        Me.InputButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.InputButton.Name = "InputButton"
        Me.InputButton.Size = New System.Drawing.Size(115, 36)
        Me.InputButton.TabIndex = 4
        Me.InputButton.Text = "Input"
        Me.InputButton.UseVisualStyleBackColor = True
        '
        'ShowResultButton
        '
        Me.ShowResultButton.Location = New System.Drawing.Point(20, 181)
        Me.ShowResultButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ShowResultButton.Name = "ShowResultButton"
        Me.ShowResultButton.Size = New System.Drawing.Size(115, 36)
        Me.ShowResultButton.TabIndex = 5
        Me.ShowResultButton.Text = "Show Result"
        Me.ShowResultButton.UseVisualStyleBackColor = True
        '
        'RandButton
        '
        Me.RandButton.Location = New System.Drawing.Point(257, 98)
        Me.RandButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RandButton.Name = "RandButton"
        Me.RandButton.Size = New System.Drawing.Size(115, 36)
        Me.RandButton.TabIndex = 6
        Me.RandButton.Text = "Rand"
        Me.RandButton.UseVisualStyleBackColor = True
        '
        'TestButton
        '
        Me.TestButton.Location = New System.Drawing.Point(257, 142)
        Me.TestButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TestButton.Name = "TestButton"
        Me.TestButton.Size = New System.Drawing.Size(115, 36)
        Me.TestButton.TabIndex = 7
        Me.TestButton.Text = "Test"
        Me.TestButton.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(480, 236)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 36)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(740, 294)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TestButton)
        Me.Controls.Add(Me.RandButton)
        Me.Controls.Add(Me.ShowResultButton)
        Me.Controls.Add(Me.InputButton)
        Me.Controls.Add(Me.SelHeight)
        Me.Controls.Add(Me.SelWidth)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
    Friend WithEvents Button1 As Button
End Class
