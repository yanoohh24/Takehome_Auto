Imports System.Data.OleDb
Imports Microsoft.Win32
Imports System.ServiceProcess
Imports System.Threading

Public Class frmGSMSetting
    Dim strConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
    Dim conStringPath As String
    Public myConnection As OleDbConnection = New OleDbConnection
    Public dr As OleDbDataReader

    Private Sub ListViewSetting()
        lstSetting.Columns.Clear()
        lstSetting.Columns.Add("ID", 40, HorizontalAlignment.Left)
        lstSetting.Columns.Add("Description", 200, HorizontalAlignment.Left)
        lstSetting.Columns.Add("Enabled", 55, HorizontalAlignment.Left)
        lstSetting.Columns.Add("EnableSend", 80, HorizontalAlignment.Left)
        lstSetting.Columns.Add("EnableReceive", 90, HorizontalAlignment.Left)
        lstSetting.Columns.Add("Device", 70, HorizontalAlignment.Left)
        lstSetting.Columns.Add("MultiPart", 55, HorizontalAlignment.Left)
        lstSetting.Columns.Add("Storage", 50, HorizontalAlignment.Left)
        lstSetting.Columns.Add("Message on device", 80, HorizontalAlignment.Left)
    End Sub
    Private Sub comboItem()
        cbStorage.Items.Clear()
        cbStorage.Items.Add("'ALL' - Auto")
        cbStorage.Items.Add("'SM' - SIM")
        cbStorage.Items.Add("'ME' - Memory")
        cbStorage.Items.Add("'MT' - Any")
    End Sub
    Public Sub RegistryLoad()
        Try
            Dim regKey As RegistryKey
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System\CurrentControlSet\Services\AxSmsSvc", True)
            t = Replace(regKey.GetValue("ImagePath", "").ToString(), "Program\AxSmsSvc.exe", "Cfg\Configuration.mdb")
            regKey.Close()
        Catch ex As Exception
            MsgBox("No registry record found." & vbNewLine & ex.Message, MsgBoxStyle.Information, "Registry Load")
        End Try
    End Sub
    Sub GetSerialPortNames()
        ' Show all available COM ports. 
        For Each sp As String In My.Computer.Ports.SerialPortNames
            cbComPort.Items.Add(sp)
        Next
    End Sub

    Private Sub frmGSMSetting_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        FFAutoSMS.BringToFront()
    End Sub

    Private Sub GSM_SERVICE_CONTROL()
        Try
            Dim service As ServiceController = New ServiceController("AxSmsSvc", System.Net.Dns.GetHostName())

            If service.Status.Equals(ServiceControllerStatus.Stopped) Or service.Status.Equals(ServiceControllerStatus.StopPending) Then
                txtService.Text = "Service is stopped"
                btStartSrv.Enabled = True
                btStopSrv.Enabled = False
            Else
                txtService.Text = "Service is running"
                btStartSrv.Enabled = False
                btStopSrv.Enabled = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "GSM SERVICE CONTROL")
        End Try

        txtService.SelectionStart = Len(txtService.Text)

    End Sub
    Private Sub frmGSMSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        RegistryLoad()
        ListViewSetting()
        comboItem()
        GetSerialPortNames()

        conStringPath = strConnectionString & t
        myConnection.ConnectionString = conStringPath

        txtID.Text = ""
        GSM_SERVICE_CONTROL()
        btView_Click(Me, EventArgs.Empty)

    End Sub

    Private Sub btView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btView.Click
        Try
            btUpdate.Enabled = True
            myConnection.Open()
            Dim str As String
            str = "SELECT * FROM Channels_GsmModem"
            Dim cmd As OleDbCommand = New OleDbCommand(str, myConnection)
            dr = cmd.ExecuteReader
            lstSetting.Items.Clear()
            While dr.Read()
                Dim ls As New ListViewItem(dr.Item("id").ToString())
                ls.SubItems.Add(dr.Item("Description").ToString())
                ls.SubItems.Add(dr.Item("Enabled").ToString())
                ls.SubItems.Add(dr.Item("EnableSend").ToString())
                ls.SubItems.Add(dr.Item("EnableReceive").ToString())
                ls.SubItems.Add(dr.Item("Device").ToString())
                ls.SubItems.Add(dr.Item("MultiPart").ToString())
                ls.SubItems.Add(dr.Item("Storage").ToString())
                ls.SubItems.Add(dr.Item("LeaveMessageOnDevice").ToString())
                lstSetting.Items.Add(ls)
            End While
            myConnection.Close()

        Catch ex As Exception
            MsgBox("No registry record found.", MsgBoxStyle.Information, "Connection Query")
            btUpdate.Enabled = False
            Me.Dispose()
        End Try
    End Sub

    Private Sub lstSetting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSetting.Click
        Try
            Dim mID As String = lstSetting.SelectedItems(0).Text
            Dim mEnable As String = lstSetting.SelectedItems(0).SubItems(2).Text
            Dim mEnableSend As String = lstSetting.SelectedItems(0).SubItems(3).Text
            Dim mEnableReceive As String = lstSetting.SelectedItems(0).SubItems(4).Text
            Dim mDevice As String = lstSetting.SelectedItems(0).SubItems(5).Text
            Dim mMultiPart As String = lstSetting.SelectedItems(0).SubItems(6).Text
            Dim mStorage As Integer = lstSetting.SelectedItems(0).SubItems(7).Text
            Dim mLeaveMessageOnDevice As String = lstSetting.SelectedItems(0).SubItems(8).Text

            If mEnable = "True" Then
                chEnable.Checked = True
            Else
                chEnable.Checked = False
            End If

            If mEnableSend = "True" Then
                chEnableSend.Checked = True
            Else
                chEnableSend.Checked = False
            End If

            If mEnableReceive = "True" Then
                chEnableReceive.Checked = True
            Else
                chEnableReceive.Checked = False
            End If

            If mMultiPart = "1" Then
                chMultiPart.Checked = True
            Else
                chMultiPart.Checked = False
            End If

            If mLeaveMessageOnDevice = "True" Then
                chLMonDevice.Checked = True
            Else
                chLMonDevice.Checked = False
            End If

            cbStorage.SelectedIndex = mStorage
            txtID.Text = mID
            cbComPort.Text = mDevice

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Me.Dispose()
        End Try
    End Sub

    Private Sub btUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btUpdate.Click
        Dim str As String
        Dim mID As String = txtID.Text.Trim()
        Dim mEnable As Integer
        Dim mEnableSend As Integer
        Dim mEnableReceive As Integer
        Dim mDevice As String = Replace(cbComPort.Text.Trim, " ", "")
        Dim mMultiPart As Integer
        Dim mStorage As Integer = cbStorage.SelectedIndex
        Dim mLeaveMessageOnDevice As Integer

        If Len(txtID.Text) < 1 Then
            MsgBox("Please select GSM modem to update.")
            lstSetting.Focus()
            Exit Sub
        End If
        If chEnable.Checked = True Then
            mEnable = 1
        Else
            mEnable = 0
        End If

        If chEnableSend.Checked = True Then
            mEnableSend = 1
        Else
            mEnableSend = 0
        End If

        If chEnableReceive.Checked = True Then
            mEnableReceive = 1
        Else
            mEnableReceive = 0
        End If

        If chMultiPart.Checked = True Then
            mMultiPart = 1
        Else
            mMultiPart = 0
        End If

        If chLMonDevice.Checked = True Then
            mLeaveMessageOnDevice = 1
        Else
            mLeaveMessageOnDevice = 0
        End If

        mDevice = StrConv(cbComPort.Text.Trim, VbStrConv.Uppercase)
        mDevice = Replace(Replace(mDevice, "'", "\'"), " ", "")

        Try
            myConnection.Open()
            str = "UPDATE Channels_GsmModem SET Description='GSM Device connected to COM101', Enabled='" & mEnable & "', " _
            & "EnableSend='" & mEnableSend & "', EnableReceive='" & mEnableReceive & "', MultiPart='" & mMultiPart & "', " _
            & "Device='" & Replace(mDevice, "'", "\'") & "', Storage='" & cbStorage.SelectedIndex & "', LeaveMessageOnDevice='" & mLeaveMessageOnDevice & "' WHERE id LIKE '" & mID & "'"

            Dim cmd As OleDbCommand = New OleDbCommand(str, myConnection)
            cmd.ExecuteNonQuery()
            myConnection.Close()

            cbComPort.Text = Replace(cbComPort.Text.Trim, " ", "")

            btView_Click(Me, EventArgs.Empty)
            MsgBox("GSM Modem Setting Successfully Updated", MsgBoxStyle.Information, "GSM Setting")
            Me.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Dispose()
    End Sub

    Private Sub lstSetting_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSetting.GotFocus
        lstSetting.Items(0).Selected = True
        lstSetting.Select()

        lstSetting_Click(Me, EventArgs.Empty)
    End Sub

    Function StorageType(ByVal ID As Integer) As String

        Select Case ID
            Case 0
                StorageType = "All"
            Case 1
                StorageType = "SIM"
            Case 2
                StorageType = "Memory"
            Case 3
                StorageType = "Any"
            Case Else
                StorageType = "Undefined"
        End Select
    End Function

    Private Sub cbComPort_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbComPort.KeyDown
        If e.KeyCode = Keys.Delete Or e.KeyCode = Keys.BrowserBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub cbComPort_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbComPort.KeyPress
        If e.KeyChar = Chr(38) Or e.KeyChar = Chr(40) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cbStorage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbStorage.KeyDown
        If e.KeyCode = Keys.Delete Or e.KeyCode = Keys.BrowserBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub cbStorage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbStorage.KeyPress
        If e.KeyChar = Chr(38) Or e.KeyChar = Chr(40) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btStopSrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btStopSrv.Click
        txtService.Text = "Attempting to stop service"
        ShutdownApplication()

        Try
            Dim service As ServiceController = New ServiceController("AxSmsSvc", System.Net.Dns.GetHostName())

            btStopSrv.Enabled = False

            If service.Status.Equals(ServiceControllerStatus.Running) Or service.Status.Equals(ServiceControllerStatus.StartPending) Then
                'Start the service
                btStopSrv.Enabled = False
                service.Stop()
                Thread.Sleep(15000)
            End If

            GSM_SERVICE_CONTROL()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub btStartSrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btStartSrv.Click
        txtService.Text = "Attempting to start service"
        ShutdownApplication()

        Try
            Dim service As ServiceController = New ServiceController("AxSmsSvc", System.Net.Dns.GetHostName())

            btStartSrv.Enabled = False

            If service.Status.Equals(ServiceControllerStatus.Stopped) Or service.Status.Equals(ServiceControllerStatus.StopPending) Then
                'Start the service
                btStopSrv.Enabled = False
                service.Start()
                Thread.Sleep(15000)
            End If

            GSM_SERVICE_CONTROL()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)

        End Try
    End Sub

    Private Sub txtService_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtService.GotFocus
        txtService.SelectionStart = Len(txtService.Text)
    End Sub
    Private Sub ShutdownApplication()
        Dim proc = Process.GetProcessesByName("AxSmsUI")

        For i As Integer = 0 To proc.Count - 1
            proc(i).Kill()
        Next i
        Thread.Sleep(100)
        Application.DoEvents()
    End Sub

    Private Sub btnBeloSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeloSync.Click
        belo_module.SynchronizeDatabaseBeloMessages()
    End Sub

    Private Sub btnMessagesSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMessagesSync.Click
        belo_module.SynchronizeDatabaseMessages()
    End Sub
End Class