<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCatchError
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
        Me.txtCatchErrors = New System.Windows.Forms.TextBox()
        Me.btCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'txtCatchErrors
        '
        Me.txtCatchErrors.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtCatchErrors.Location = New System.Drawing.Point(12, 12)
        Me.txtCatchErrors.Multiline = true
        Me.txtCatchErrors.Name = "txtCatchErrors"
        Me.txtCatchErrors.ReadOnly = true
        Me.txtCatchErrors.Size = New System.Drawing.Size(660, 354)
        Me.txtCatchErrors.TabIndex = 0
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(597, 372)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 1
        Me.btCancel.Text = "Cancel"
        Me.btCancel.UseVisualStyleBackColor = true
        '
        'frmCatchError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 401)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.txtCatchErrors)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MinimizeBox = false
        Me.Name = "frmCatchError"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCatchError"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents txtCatchErrors As System.Windows.Forms.TextBox
    Friend WithEvents btCancel As System.Windows.Forms.Button
End Class
