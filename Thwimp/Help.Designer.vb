<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Help
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Help))
        Me.helpBox = New System.Windows.Forms.TextBox()
        Me.picHelp = New System.Windows.Forms.PictureBox()
        CType(Me.picHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'helpBox
        '
        Me.helpBox.Location = New System.Drawing.Point(66, 12)
        Me.helpBox.Multiline = True
        Me.helpBox.Name = "helpBox"
        Me.helpBox.ReadOnly = True
        Me.helpBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.helpBox.Size = New System.Drawing.Size(814, 600)
        Me.helpBox.TabIndex = 0
        '
        'picHelp
        '
        Me.picHelp.Location = New System.Drawing.Point(12, 291)
        Me.picHelp.Name = "picHelp"
        Me.picHelp.Size = New System.Drawing.Size(48, 48)
        Me.picHelp.TabIndex = 1
        Me.picHelp.TabStop = False
        '
        'Help
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 615)
        Me.Controls.Add(Me.picHelp)
        Me.Controls.Add(Me.helpBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Help"
        Me.Text = "Thwimp CLI"
        CType(Me.picHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents helpBox As System.Windows.Forms.TextBox
    Friend WithEvents picHelp As System.Windows.Forms.PictureBox
End Class
