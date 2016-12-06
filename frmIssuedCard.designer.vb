<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIssuedCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIssuedCard))
        Me.lstIssuedCard = New System.Windows.Forms.ListView()
        Me.btView = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.RowToolStrip = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btSendToAll = New System.Windows.Forms.Button()
        Me.txtIssuedDate = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.dpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstIssuedCard
        '
        Me.lstIssuedCard.FullRowSelect = True
        Me.lstIssuedCard.GridLines = True
        Me.lstIssuedCard.Location = New System.Drawing.Point(12, 43)
        Me.lstIssuedCard.Name = "lstIssuedCard"
        Me.lstIssuedCard.Size = New System.Drawing.Size(706, 217)
        Me.lstIssuedCard.TabIndex = 12
        Me.lstIssuedCard.UseCompatibleStateImageBehavior = False
        Me.lstIssuedCard.View = System.Windows.Forms.View.Details
        '
        'btView
        '
        Me.btView.Location = New System.Drawing.Point(643, 14)
        Me.btView.Name = "btView"
        Me.btView.Size = New System.Drawing.Size(75, 23)
        Me.btView.TabIndex = 15
        Me.btView.Text = "View"
        Me.btView.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RowToolStrip})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 417)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(734, 22)
        Me.StatusStrip1.TabIndex = 16
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'RowToolStrip
        '
        Me.RowToolStrip.Name = "RowToolStrip"
        Me.RowToolStrip.Size = New System.Drawing.Size(78, 17)
        Me.RowToolStrip.Text = "RowToolStrip"
        '
        'btSendToAll
        '
        Me.btSendToAll.Location = New System.Drawing.Point(451, 374)
        Me.btSendToAll.Name = "btSendToAll"
        Me.btSendToAll.Size = New System.Drawing.Size(75, 23)
        Me.btSendToAll.TabIndex = 18
        Me.btSendToAll.Text = "Send to All"
        Me.btSendToAll.UseVisualStyleBackColor = True
        '
        'txtIssuedDate
        '
        Me.txtIssuedDate.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtIssuedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssuedDate.Location = New System.Drawing.Point(12, 292)
        Me.txtIssuedDate.Multiline = True
        Me.txtIssuedDate.Name = "txtIssuedDate"
        Me.txtIssuedDate.ReadOnly = True
        Me.txtIssuedDate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtIssuedDate.Size = New System.Drawing.Size(433, 105)
        Me.txtIssuedDate.TabIndex = 19
        Me.txtIssuedDate.Text = "txtIssuedDate"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 268)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Mobile"
        '
        'txtMobile
        '
        Me.txtMobile.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.Location = New System.Drawing.Point(56, 266)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReadOnly = True
        Me.txtMobile.Size = New System.Drawing.Size(389, 20)
        Me.txtMobile.TabIndex = 21
        Me.txtMobile.Text = "txtMobile"
        '
        'dpFrom
        '
        Me.dpFrom.CustomFormat = "MMM, dd yyyy"
        Me.dpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFrom.Location = New System.Drawing.Point(518, 17)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(119, 20)
        Me.dpFrom.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(482, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "From"
        '
        'frmIssuedCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 439)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dpFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.txtIssuedDate)
        Me.Controls.Add(Me.btSendToAll)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btView)
        Me.Controls.Add(Me.lstIssuedCard)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIssuedCard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Issued Card"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstIssuedCard As System.Windows.Forms.ListView
    Friend WithEvents btView As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents RowToolStrip As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btSendToAll As System.Windows.Forms.Button
    Friend WithEvents txtIssuedDate As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
