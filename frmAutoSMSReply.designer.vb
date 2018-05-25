<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoSMSReply
    Inherits MetroFramework.Forms.MetroForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutoSMSReply))
        Me.btSearch = New System.Windows.Forms.Button()
        Me.lvLogs = New System.Windows.Forms.ListView()
        Me.No = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ID = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.SMS = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.mobile = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btViewAll = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.FileToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.ExtractToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripSplitButton2 = New System.Windows.Forms.ToolStripSplitButton()
        Me.IssuedCardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeloToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MessagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSetting = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripPB = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ServerIPToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TotalCountToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btRead = New System.Windows.Forms.Button()
        Me.lbStatus = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lbTime = New System.Windows.Forms.Label()
        Me.btRestartServiceSMS = New System.Windows.Forms.Button()
        Me.btResendAll = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout
        Me.StatusStrip1.SuspendLayout
        Me.Panel1.SuspendLayout
        Me.SuspendLayout
        '
        'btSearch
        '
        Me.btSearch.BackColor = System.Drawing.Color.Teal
        Me.btSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btSearch.ForeColor = System.Drawing.Color.White
        Me.btSearch.Location = New System.Drawing.Point(23, 88)
        Me.btSearch.Name = "btSearch"
        Me.btSearch.Size = New System.Drawing.Size(96, 23)
        Me.btSearch.TabIndex = 2
        Me.btSearch.Text = "Inbox SMS"
        Me.btSearch.UseVisualStyleBackColor = false
        '
        'lvLogs
        '
        Me.lvLogs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.No, Me.ID, Me.SMS, Me.mobile})
        Me.lvLogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lvLogs.FullRowSelect = true
        Me.lvLogs.GridLines = true
        Me.lvLogs.Location = New System.Drawing.Point(23, 117)
        Me.lvLogs.Name = "lvLogs"
        Me.lvLogs.Size = New System.Drawing.Size(537, 276)
        Me.lvLogs.TabIndex = 4
        Me.lvLogs.UseCompatibleStateImageBehavior = false
        Me.lvLogs.View = System.Windows.Forms.View.Details
        '
        'No
        '
        Me.No.Text = "No"
        Me.No.Width = 40
        '
        'ID
        '
        Me.ID.Text = "ID"
        Me.ID.Width = 55
        '
        'SMS
        '
        Me.SMS.Text = "SMS"
        Me.SMS.Width = 300
        '
        'mobile
        '
        Me.mobile.Text = "Mobile"
        Me.mobile.Width = 100
        '
        'btViewAll
        '
        Me.btViewAll.BackColor = System.Drawing.Color.Teal
        Me.btViewAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btViewAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btViewAll.ForeColor = System.Drawing.Color.White
        Me.btViewAll.Location = New System.Drawing.Point(423, 88)
        Me.btViewAll.Name = "btViewAll"
        Me.btViewAll.Size = New System.Drawing.Size(122, 23)
        Me.btViewAll.TabIndex = 3
        Me.btViewAll.Text = "View and Send"
        Me.btViewAll.UseVisualStyleBackColor = false
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.White
        Me.ToolStrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripSplitButton1, Me.ViewToolStripSplitButton2, Me.ToolStripSetting, Me.ToolStripPB, Me.ToolStripSplitButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(20, 60)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(541, 25)
        Me.ToolStrip1.TabIndex = 11
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'FileToolStripSplitButton1
        '
        Me.FileToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.FileToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExtractToExcelToolStripMenuItem, Me.ToolStripTextBox1, Me.ToolStripSeparator1, Me.CloseToolStripMenuItem})
        Me.FileToolStripSplitButton1.Font = New System.Drawing.Font("Calibri", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FileToolStripSplitButton1.Image = CType(resources.GetObject("FileToolStripSplitButton1.Image"),System.Drawing.Image)
        Me.FileToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FileToolStripSplitButton1.Name = "FileToolStripSplitButton1"
        Me.FileToolStripSplitButton1.Size = New System.Drawing.Size(41, 22)
        Me.FileToolStripSplitButton1.Text = "File"
        '
        'ExtractToExcelToolStripMenuItem
        '
        Me.ExtractToExcelToolStripMenuItem.Enabled = false
        Me.ExtractToExcelToolStripMenuItem.Name = "ExtractToExcelToolStripMenuItem"
        Me.ExtractToExcelToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.ExtractToExcelToolStripMenuItem.Text = "Extract to Excel"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Calibri", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 22)
        Me.ToolStripTextBox1.Text = "Synchronize"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(157, 6)
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Font = New System.Drawing.Font("Calibri", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'ViewToolStripSplitButton2
        '
        Me.ViewToolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ViewToolStripSplitButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IssuedCardToolStripMenuItem, Me.SyToolStripMenuItem})
        Me.ViewToolStripSplitButton2.Font = New System.Drawing.Font("Calibri", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ViewToolStripSplitButton2.Image = CType(resources.GetObject("ViewToolStripSplitButton2.Image"),System.Drawing.Image)
        Me.ViewToolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ViewToolStripSplitButton2.Name = "ViewToolStripSplitButton2"
        Me.ViewToolStripSplitButton2.Size = New System.Drawing.Size(48, 22)
        Me.ViewToolStripSplitButton2.Text = "View"
        '
        'IssuedCardToolStripMenuItem
        '
        Me.IssuedCardToolStripMenuItem.Name = "IssuedCardToolStripMenuItem"
        Me.IssuedCardToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IssuedCardToolStripMenuItem.Text = "Issued Card"
        '
        'SyToolStripMenuItem
        '
        Me.SyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BeloToolStripMenuItem, Me.MessagesToolStripMenuItem})
        Me.SyToolStripMenuItem.Name = "SyToolStripMenuItem"
        Me.SyToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SyToolStripMenuItem.Text = "Synchronization"
        '
        'BeloToolStripMenuItem
        '
        Me.BeloToolStripMenuItem.Name = "BeloToolStripMenuItem"
        Me.BeloToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.BeloToolStripMenuItem.Text = "Belo Database"
        '
        'MessagesToolStripMenuItem
        '
        Me.MessagesToolStripMenuItem.Name = "MessagesToolStripMenuItem"
        Me.MessagesToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.MessagesToolStripMenuItem.Text = "Messages"
        '
        'ToolStripSetting
        '
        Me.ToolStripSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripSetting.Font = New System.Drawing.Font("Calibri", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStripSetting.Image = CType(resources.GetObject("ToolStripSetting.Image"),System.Drawing.Image)
        Me.ToolStripSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSetting.Name = "ToolStripSetting"
        Me.ToolStripSetting.Size = New System.Drawing.Size(46, 22)
        Me.ToolStripSetting.Text = "Setting"
        '
        'ToolStripPB
        '
        Me.ToolStripPB.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar
        Me.ToolStripPB.Name = "ToolStripPB"
        Me.ToolStripPB.Size = New System.Drawing.Size(100, 22)
        Me.ToolStripPB.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.ErrorsToolStripMenuItem})
        Me.ToolStripSplitButton1.Font = New System.Drawing.Font("Calibri", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStripSplitButton1.Image = CType(resources.GetObject("ToolStripSplitButton1.Image"),System.Drawing.Image)
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(52, 22)
        Me.ToolStripSplitButton1.Text = "About"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(106, 22)
        Me.AboutToolStripMenuItem.Text = "Footer"
        '
        'ErrorsToolStripMenuItem
        '
        Me.ErrorsToolStripMenuItem.Name = "ErrorsToolStripMenuItem"
        Me.ErrorsToolStripMenuItem.Size = New System.Drawing.Size(106, 22)
        Me.ErrorsToolStripMenuItem.Text = "Errors"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Teal
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServerIPToolStripStatusLabel, Me.TotalCountToolStripStatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(20, 433)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(541, 22)
        Me.StatusStrip1.TabIndex = 15
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ServerIPToolStripStatusLabel
        '
        Me.ServerIPToolStripStatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ServerIPToolStripStatusLabel.ForeColor = System.Drawing.Color.White
        Me.ServerIPToolStripStatusLabel.Name = "ServerIPToolStripStatusLabel"
        Me.ServerIPToolStripStatusLabel.Size = New System.Drawing.Size(53, 17)
        Me.ServerIPToolStripStatusLabel.Text = "ServerIP"
        '
        'TotalCountToolStripStatusLabel
        '
        Me.TotalCountToolStripStatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TotalCountToolStripStatusLabel.ForeColor = System.Drawing.Color.White
        Me.TotalCountToolStripStatusLabel.Name = "TotalCountToolStripStatusLabel"
        Me.TotalCountToolStripStatusLabel.Size = New System.Drawing.Size(66, 17)
        Me.TotalCountToolStripStatusLabel.Text = "TotalCount"
        '
        'btRead
        '
        Me.btRead.BackColor = System.Drawing.Color.Teal
        Me.btRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btRead.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btRead.ForeColor = System.Drawing.Color.White
        Me.btRead.Location = New System.Drawing.Point(286, 88)
        Me.btRead.Name = "btRead"
        Me.btRead.Size = New System.Drawing.Size(121, 23)
        Me.btRead.TabIndex = 18
        Me.btRead.Text = "Read SMS"
        Me.btRead.UseVisualStyleBackColor = false
        Me.btRead.Visible = false
        '
        'lbStatus
        '
        Me.lbStatus.AutoSize = true
        Me.lbStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbStatus.Location = New System.Drawing.Point(17, 418)
        Me.lbStatus.Name = "lbStatus"
        Me.lbStatus.Size = New System.Drawing.Size(41, 15)
        Me.lbStatus.TabIndex = 19
        Me.lbStatus.Text = "Status"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'lbTime
        '
        Me.lbTime.AutoSize = true
        Me.lbTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbTime.Location = New System.Drawing.Point(178, 418)
        Me.lbTime.Name = "lbTime"
        Me.lbTime.Size = New System.Drawing.Size(35, 15)
        Me.lbTime.TabIndex = 20
        Me.lbTime.Text = "Time"
        Me.lbTime.Visible = false
        '
        'btRestartServiceSMS
        '
        Me.btRestartServiceSMS.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btRestartServiceSMS.Location = New System.Drawing.Point(427, 428)
        Me.btRestartServiceSMS.Name = "btRestartServiceSMS"
        Me.btRestartServiceSMS.Size = New System.Drawing.Size(112, 23)
        Me.btRestartServiceSMS.TabIndex = 71
        Me.btRestartServiceSMS.Text = "RestartServiceSMS"
        Me.btRestartServiceSMS.UseVisualStyleBackColor = true
        Me.btRestartServiceSMS.Visible = false
        '
        'btResendAll
        '
        Me.btResendAll.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btResendAll.Location = New System.Drawing.Point(287, 428)
        Me.btResendAll.Name = "btResendAll"
        Me.btResendAll.Size = New System.Drawing.Size(75, 23)
        Me.btResendAll.TabIndex = 72
        Me.btResendAll.Text = "Resend All"
        Me.btResendAll.UseVisualStyleBackColor = true
        Me.btResendAll.Visible = false
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btResendAll)
        Me.Panel1.Controls.Add(Me.btRestartServiceSMS)
        Me.Panel1.Location = New System.Drawing.Point(488, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(54, 32)
        Me.Panel1.TabIndex = 73
        Me.Panel1.Visible = false
        '
        'frmAutoSMSReply
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 475)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btSearch)
        Me.Controls.Add(Me.lbTime)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lbStatus)
        Me.Controls.Add(Me.btViewAll)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btRead)
        Me.Controls.Add(Me.lvLogs)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmAutoSMSReply"
        Me.Resizable = false
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow
        Me.Text = "Auto SMS Reply"
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.StatusStrip1.ResumeLayout(false)
        Me.StatusStrip1.PerformLayout
        Me.Panel1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btSearch As System.Windows.Forms.Button
    Friend WithEvents lvLogs As System.Windows.Forms.ListView
    Friend WithEvents No As System.Windows.Forms.ColumnHeader
    Friend WithEvents ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents btViewAll As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents FileToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ExtractToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripPB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ServerIPToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TotalCountToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btRead As System.Windows.Forms.Button
    Friend WithEvents lbStatus As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lbTime As System.Windows.Forms.Label
    Friend WithEvents ToolStripSetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents SMS As System.Windows.Forms.ColumnHeader
    Friend WithEvents mobile As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ErrorsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripSplitButton2 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents IssuedCardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents SyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeloToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MessagesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btRestartServiceSMS As System.Windows.Forms.Button
    Friend WithEvents btResendAll As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
