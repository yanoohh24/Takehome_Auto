Imports Microsoft.Win32
Imports System.Net

Public Class frmLogin

    Private Sub btLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLogin.Click
        Try
            Host = "192.168.100.172" '192.168.100.250"
            UserName = "root" '"admin"
            Password = "belo" '"webdeveoper"

            connStrBMG = "Database=belo_database;Data Source=" & Host & ";User Id=" & UserName & ";Password=" & Password & ";UseCompression=True;Connection Timeout=28800"
            connStrSMS = "Database=Messages;Data Source=" & Host & ";User Id=" & UserName & ";Password=" & Password & ";UseCompression=True;Connection Timeout=28800"

            'RegistryRecordLogin()
            FFAutoSMS.Show()
            FFAutoSMS.BringToFront()
            'Form1.Show()
            ClearText()
        Catch ex As Exception
            FFAutoSMS = New frmAutoSMSReply()
            FFAutoSMS.Show()
            FFAutoSMS.BringToFront()
        End Try

    End Sub
    Public Sub ClearText()
        txtUsername.Clear()
        txtPassword.Clear()
    End Sub
    Private Sub cbLocation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
        Else
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub GetIPAddress()
        ClientHostName = Dns.GetHostName()
        ClientHostIP = Dns.GetHostByName(ClientHostName).AddressList(0).ToString()
    End Sub
    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUsername.Text = ""
        txtPassword.Text = ""
        RegistryGetLogin()
        GetIPAddress()
    End Sub

    Private Sub RegistryGetLogin()
        Dim regKey As RegistryKey

        Try
            'Get Registry Record
            regKey = Registry.LocalMachine.OpenSubKey("SYSTEM\BeloSMSAuto", True)

            If Len(regKey.GetValue("smsusername", "").ToString().Trim()) > 1 Then
                UserName = regKey.GetValue("smsusername", "").ToString().Trim()

            Else
                regKey.SetValue("smsusername", "root")
                UserName = "root"
            End If

            If Len(regKey.GetValue("smspassword", "").ToString().Trim()) > 1 Then
                Password = regKey.GetValue("smspassword", "").ToString().Trim()
            Else
                regKey.SetValue("smspassword", "")
                Password = ""
            End If

            If Len(regKey.GetValue("smsHostIP", "").ToString().Trim()) > 1 Then
                Host = regKey.GetValue("smsHostIP", "").ToString().Trim()
            Else
                regKey.SetValue("smsHostIP", "localhost")
                Host = "localhost"

            End If

            regKey.Close()

        Catch ex As Exception
            'Create Registry Record
            regKey = Registry.LocalMachine.OpenSubKey("SYSTEM", True)
            regKey.CreateSubKey("BeloSMSAuto")
            regKey = Registry.LocalMachine.OpenSubKey("SYSTEM\BeloSMSAuto", True)

            UserName = "admin"
            Password = "webdeveoper"
            Host = "192.168.100.250"

            regKey.SetValue("smsusername", UserName)
            regKey.SetValue("smspassword", Password)
            regKey.SetValue("smsHostIP", Host)

            regKey.Close()

        End Try
    End Sub
    Private Sub RegistryRecordLogin()
        Dim regKey As RegistryKey
        Try
            'Create Registry Record
            regKey = Registry.LocalMachine.OpenSubKey("SYSTEM\BeloSMSAuto", True)
            regKey.SetValue("smsusername", UserName)
            regKey.SetValue("smspassword", Password)
            regKey.SetValue("smsHostIP", Host)
            regKey.Close()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try

    End Sub


    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        End
    End Sub

    Private Sub frmLogin_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        btLogin_Click(Me, EventArgs.Empty)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Panel2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub


    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    'Move FORM WHeN PANEL MOVED
    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        drag = True 'Sets the variable drag to true.
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey
    End Sub
    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If

    End Sub
    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp
        drag = False
    End Sub
End Class