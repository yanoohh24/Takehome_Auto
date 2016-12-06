<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGSMSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGSMSetting))
        Me.lstSetting = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btView = New System.Windows.Forms.Button()
        Me.chEnable = New System.Windows.Forms.CheckBox()
        Me.chEnableReceive = New System.Windows.Forms.CheckBox()
        Me.chEnableSend = New System.Windows.Forms.CheckBox()
        Me.chMultiPart = New System.Windows.Forms.CheckBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btUpdate = New System.Windows.Forms.Button()
        Me.btCancel = New System.Windows.Forms.Button()
        Me.cbStorage = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbComPort = New System.Windows.Forms.ComboBox()
        Me.chLMonDevice = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtService = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btStopSrv = New System.Windows.Forms.Button()
        Me.btStartSrv = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnBeloSync = New System.Windows.Forms.Button()
        Me.btnMessagesSync = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstSetting
        '
        Me.lstSetting.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lstSetting.FullRowSelect = True
        Me.lstSetting.GridLines = True
        Me.lstSetting.Location = New System.Drawing.Point(12, 12)
        Me.lstSetting.Name = "lstSetting"
        Me.lstSetting.Size = New System.Drawing.Size(735, 79)
        Me.lstSetting.TabIndex = 0
        Me.lstSetting.UseCompatibleStateImageBehavior = False
        Me.lstSetting.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Device Name"
        Me.ColumnHeader2.Width = 200
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Device Port"
        Me.ColumnHeader3.Width = 80
        '
        'btView
        '
        Me.btView.Location = New System.Drawing.Point(672, 97)
        Me.btView.Name = "btView"
        Me.btView.Size = New System.Drawing.Size(75, 23)
        Me.btView.TabIndex = 1
        Me.btView.Text = "View"
        Me.btView.UseVisualStyleBackColor = True
        Me.btView.Visible = False
        '
        'chEnable
        '
        Me.chEnable.AutoSize = True
        Me.chEnable.Location = New System.Drawing.Point(6, 45)
        Me.chEnable.Name = "chEnable"
        Me.chEnable.Size = New System.Drawing.Size(59, 17)
        Me.chEnable.TabIndex = 2
        Me.chEnable.Text = "Enable"
        Me.chEnable.UseVisualStyleBackColor = True
        '
        'chEnableReceive
        '
        Me.chEnableReceive.AutoSize = True
        Me.chEnableReceive.Location = New System.Drawing.Point(6, 91)
        Me.chEnableReceive.Name = "chEnableReceive"
        Me.chEnableReceive.Size = New System.Drawing.Size(99, 17)
        Me.chEnableReceive.TabIndex = 4
        Me.chEnableReceive.Text = "EnableReceive"
        Me.chEnableReceive.UseVisualStyleBackColor = True
        '
        'chEnableSend
        '
        Me.chEnableSend.AutoSize = True
        Me.chEnableSend.Location = New System.Drawing.Point(6, 68)
        Me.chEnableSend.Name = "chEnableSend"
        Me.chEnableSend.Size = New System.Drawing.Size(84, 17)
        Me.chEnableSend.TabIndex = 3
        Me.chEnableSend.Text = "EnableSend"
        Me.chEnableSend.UseVisualStyleBackColor = True
        '
        'chMultiPart
        '
        Me.chMultiPart.AutoSize = True
        Me.chMultiPart.Location = New System.Drawing.Point(6, 114)
        Me.chMultiPart.Name = "chMultiPart"
        Me.chMultiPart.Size = New System.Drawing.Size(67, 17)
        Me.chMultiPart.TabIndex = 5
        Me.chMultiPart.Text = "MultiPart"
        Me.chMultiPart.UseVisualStyleBackColor = True
        '
        'txtID
        '
        Me.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(6, 19)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(100, 20)
        Me.txtID.TabIndex = 1
        Me.txtID.Text = "txtID"
        '
        'btUpdate
        '
        Me.btUpdate.Location = New System.Drawing.Point(18, 214)
        Me.btUpdate.Name = "btUpdate"
        Me.btUpdate.Size = New System.Drawing.Size(63, 23)
        Me.btUpdate.TabIndex = 8
        Me.btUpdate.Text = "Update"
        Me.btUpdate.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(87, 214)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(63, 23)
        Me.btCancel.TabIndex = 9
        Me.btCancel.Text = "Cancel"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'cbStorage
        '
        Me.cbStorage.FormattingEnabled = True
        Me.cbStorage.Location = New System.Drawing.Point(50, 187)
        Me.cbStorage.Name = "cbStorage"
        Me.cbStorage.Size = New System.Drawing.Size(100, 21)
        Me.cbStorage.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 163)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Device"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 190)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Storage"
        '
        'cbComPort
        '
        Me.cbComPort.FormattingEnabled = True
        Me.cbComPort.Location = New System.Drawing.Point(50, 160)
        Me.cbComPort.Name = "cbComPort"
        Me.cbComPort.Size = New System.Drawing.Size(100, 21)
        Me.cbComPort.TabIndex = 6
        '
        'chLMonDevice
        '
        Me.chLMonDevice.AutoSize = True
        Me.chLMonDevice.Location = New System.Drawing.Point(6, 137)
        Me.chLMonDevice.Name = "chLMonDevice"
        Me.chLMonDevice.Size = New System.Drawing.Size(119, 17)
        Me.chLMonDevice.TabIndex = 13
        Me.chLMonDevice.Text = "Message on device"
        Me.chLMonDevice.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.cbComPort)
        Me.GroupBox1.Controls.Add(Me.chLMonDevice)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.chEnable)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chEnableReceive)
        Me.GroupBox1.Controls.Add(Me.cbStorage)
        Me.GroupBox1.Controls.Add(Me.chEnableSend)
        Me.GroupBox1.Controls.Add(Me.btCancel)
        Me.GroupBox1.Controls.Add(Me.btUpdate)
        Me.GroupBox1.Controls.Add(Me.chMultiPart)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 97)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(189, 248)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GSM device setting"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtService)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btStopSrv)
        Me.GroupBox2.Controls.Add(Me.btStartSrv)
        Me.GroupBox2.Location = New System.Drawing.Point(207, 97)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(252, 85)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GSM service control"
        '
        'txtService
        '
        Me.txtService.Location = New System.Drawing.Point(79, 19)
        Me.txtService.Name = "txtService"
        Me.txtService.ReadOnly = True
        Me.txtService.Size = New System.Drawing.Size(156, 20)
        Me.txtService.TabIndex = 3
        Me.txtService.Text = "txtService"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Current state"
        '
        'btStopSrv
        '
        Me.btStopSrv.Location = New System.Drawing.Point(79, 45)
        Me.btStopSrv.Name = "btStopSrv"
        Me.btStopSrv.Size = New System.Drawing.Size(75, 23)
        Me.btStopSrv.TabIndex = 1
        Me.btStopSrv.Text = "Stop"
        Me.btStopSrv.UseVisualStyleBackColor = True
        '
        'btStartSrv
        '
        Me.btStartSrv.Location = New System.Drawing.Point(160, 45)
        Me.btStartSrv.Name = "btStartSrv"
        Me.btStartSrv.Size = New System.Drawing.Size(75, 23)
        Me.btStartSrv.TabIndex = 0
        Me.btStartSrv.Text = "Start"
        Me.btStartSrv.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.btnMessagesSync)
        Me.GroupBox3.Controls.Add(Me.btnBeloSync)
        Me.GroupBox3.Location = New System.Drawing.Point(207, 189)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(252, 84)
        Me.GroupBox3.TabIndex = 16
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Synchronize database"
        '
        'btnBeloSync
        '
        Me.btnBeloSync.Location = New System.Drawing.Point(160, 22)
        Me.btnBeloSync.Name = "btnBeloSync"
        Me.btnBeloSync.Size = New System.Drawing.Size(75, 23)
        Me.btnBeloSync.TabIndex = 0
        Me.btnBeloSync.Text = "Synchronize"
        Me.btnBeloSync.UseVisualStyleBackColor = True
        '
        'btnMessagesSync
        '
        Me.btnMessagesSync.Location = New System.Drawing.Point(160, 47)
        Me.btnMessagesSync.Name = "btnMessagesSync"
        Me.btnMessagesSync.Size = New System.Drawing.Size(75, 23)
        Me.btnMessagesSync.TabIndex = 1
        Me.btnMessagesSync.Text = "Synchronize"
        Me.btnMessagesSync.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Belo"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Messages"
        '
        'frmGSMSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 357)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btView)
        Me.Controls.Add(Me.lstSetting)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGSMSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GSM Modem Setting"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstSetting As System.Windows.Forms.ListView
    Friend WithEvents btView As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chEnable As System.Windows.Forms.CheckBox
    Friend WithEvents chEnableReceive As System.Windows.Forms.CheckBox
    Friend WithEvents chEnableSend As System.Windows.Forms.CheckBox
    Friend WithEvents chMultiPart As System.Windows.Forms.CheckBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btUpdate As System.Windows.Forms.Button
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents cbStorage As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbComPort As System.Windows.Forms.ComboBox
    Friend WithEvents chLMonDevice As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btStartSrv As System.Windows.Forms.Button
    Friend WithEvents btStopSrv As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtService As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnMessagesSync As System.Windows.Forms.Button
    Friend WithEvents btnBeloSync As System.Windows.Forms.Button
End Class
