Imports System.ServiceProcess
Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop
Imports System.Threading
Imports System.Data.OleDb
Imports System.Reflection
Imports System.IO
Public Class frmAutoSMSReply

    Dim query As String
    Dim strEmpID As String
    Dim strDate As String
    Dim strDateFrom As String
    Dim strYear As String
    Dim strMonth As String
    Dim strDay As String
    Dim cbxLocation As String
    Dim ItemID As Integer = 0
    Dim pxName As String
    Dim strConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
    Dim conStringPath As String
    Dim provider As String
    Dim dataFile As String
    Dim connString As String
    Public myConnection As OleDbConnection = New OleDbConnection
    Public dr As OleDbDataReader
    Dim FooterInquiries As String

    Public Structure PatientInformation
        Dim ID As String
        Dim IDNO As String
        Dim Name As String
        Dim Gender As String
    End Structure

    Public Structure BranchName_DB
        Dim Code As String
        Dim Name As String
        Dim db_name As String

 
    End Structure

    Function CommandXpertSMS(ByVal msg As String, ByVal Validity As Integer, ByVal branch As String, ByVal PatientID As String, ByVal EmpMobile As String) As Integer
        Try
            Dim SMSmsg As String
            Dim query As String
            Dim MBchk As String = EmpMobile
            MBchk = Mid(MBchk, 1, 4)
            SMSmsg = Replace(msg, "'", "\'")

            'OLD DATABASE TYPE = 2 : SMS
            'NEW DATABASE TYPEID = 1 : SMS
            if not instr(1,MBchk,"+") and Len(EmpMobile) = 12 then
                EmpMobile = "+" & EmpMobile
                query = "INSERT INTO `Messages` SET DirectionID=2, TypeID=1, StatusDetailsID=200, StatusID=1, ChannelID=0, ToAddress='" & EmpMobile & "', " _
                        & " Body='" & SMSmsg & "',validity=" & Validity & ", branch='" & branch & "', PatientID='" & PatientID & "', Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

                Dim rowsEffected As Integer = 0
                Dim connection As New MySqlConnection(connStrSMS)
                Dim cmd As New MySqlCommand(query, connection)
                    
                connection.Open()

                rowsEffected = cmd.ExecuteNonQuery()

                connection.Close()

                Return rowsEffected
            elseIf  MBchk = "+639" then
                'Or MBchk = "+63906" Or MBchk = "+63915" Or MBchk = "+63916" Or MBchk = "+63917" Or MBchk = "+63926" _
                'Or MBchk = "+63935" Or MBchk = "+63936" Or MBchk = "+63937" Or MBchk = "+63994" Or MBchk = "+63927" Or MBchk = "+63996" _
                'Or MBchk = "+63997" Or MBchk = "+63817" Then 'Globe Only

                If Len(EmpMobile) = 13 Then
                    'query = "INSERT INTO Messages SET Direction=2, Type=2, StatusDetails=200, Status=1, ChannelID=0, Recipient='" & EmpMobile & "', " _
                    '& " Body='" & SMSmsg & "',validity=" & Validity & ", branch='" & branch & "', PatientID='" & PatientID & "', Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

                    query = "INSERT INTO Messages SET DirectionID=2, TypeID=1, StatusDetailsID=200, StatusID=1, ChannelID=0, ToAddress='" & EmpMobile & "', " _
                    & " Body='" & SMSmsg & "',validity=" & Validity & ", branch='" & branch & "', PatientID='" & PatientID & "', Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

                    Dim rowsEffected As Integer = 0
                    Dim connection As New MySqlConnection(connStrSMS)
                    Dim cmd As New MySqlCommand(query, connection)
                    
                    connection.Open()

                    rowsEffected = cmd.ExecuteNonQuery()

                    connection.Close()

                    Return rowsEffected

                End If
            End If
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try

    End Function
    Private Sub frmMySQL_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FFLogin.Visible = False Then
            FFLogin.Show()
        End If
    End Sub

    Private Sub ListViewHeaderTitle()

        lvLogs.View = View.Details
        lvLogs.Columns.Clear()
        lvLogs.Columns.Add("No", 40, HorizontalAlignment.Left)
        lvLogs.Columns.Add("ID", 55, HorizontalAlignment.Left)
        lvLogs.Columns.Add("SMS", 300, HorizontalAlignment.Left)
        lvLogs.Columns.Add("Mobile", 100, HorizontalAlignment.Left)

    End Sub
    Private Sub MySQL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ListViewHeaderTitle()
        Timer1.Enabled = True

        end_footer()

        lbTime.Text = "00"
        provider = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source ="
        dataFile = "C:\Users\Jimmy\Desktop\Products.accdb" ' 
        connString = provider & dataFile

        conStringPath = strConnectionString & t
        myConnection.ConnectionString = conStringPath

        ToolStripPB.Visible = False
        lbStatus.Text = ""

        ServerIPToolStripStatusLabel.Text = "Server IP: " & Host
        TotalCountToolStripStatusLabel.Text = "Rows: " & lvLogs.Items.Count
    End Sub

    Public Sub retriveDataReadToListView()
        Timer1.Enabled = False

        Try
            Dim connection As New MySqlConnection(connStrSMS)
            Dim cmd As New MySqlCommand(query, connection)
            Dim reader As MySqlDataReader
            Dim iGLCount As Long = 0

            connection.Open()
            reader = cmd.ExecuteReader()

            'this will clear the listbox
            ListViewHeaderTitle()
            lvLogs.Items.Clear()
            If reader.HasRows = True Then
                While reader.Read
                    iGLCount += 1
                    Dim ls As New ListViewItem(iGLCount.ToString())
                    ls.SubItems.Add(reader.Item("id").ToString())
                    ls.SubItems.Add(reader.Item("body").ToString())
                    ls.SubItems.Add(reader.Item("FromAddress").ToString())
                    lvLogs.Items.Add(ls)
                End While
                lbStatus.Text = ""
            Else
                lbStatus.Text = "No new message found."
            End If
            connection.Close()

            TotalCountToolStripStatusLabel.Text = "Rows: " & lvLogs.Items.Count
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            TotalCountToolStripStatusLabel.Text = "Rows: " & lvLogs.Items.Count
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
        Timer1.Enabled = True
    End Sub

    Private Sub btSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSearch.Click
        'query = "SELECT id,body,sender FROM `Messages` WHERE direction=1 AND TYPE=2 ORDER BY id DESC LIMIT 0,20"
        query = "SELECT id,body,FromAddress FROM `Messages` WHERE directionID=1 AND TYPEID=1 ORDER BY id DESC LIMIT 0,20"
        retriveDataReadToListView()
    End Sub

    Private Sub btViewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btViewAll.Click
        Timer1.Enabled = False
        lbStatus.Text = ""
        'query = "SELECT id,body,FromAddress FROM `Messages` WHERE direction=1 AND TYPE=2 AND stats LIKE '%new%'"
        query = "SELECT id,body,FromAddress FROM `Messages` WHERE directionID=1 AND TYPEID=1 AND stats LIKE '%new%'"
        'DISPLAY RECEIVED TEXT IN LISTVIEW
        retriveDataReadToListView()
        'IF MESSAGE COUNT > 1
        btRead_Click(Me, EventArgs.Empty)
        Timer1.Enabled = True
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            btSearch.PerformClick()
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Dispose()
    End Sub
    Private Sub ExtractToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtractToExcelToolStripMenuItem.Click
        Call ExportToExcel()
    End Sub
    Private Sub CloseToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Try
            Me.Dispose()
            FFLogin.Show()
        Catch ex As Exception
            FFLogin = New frmLogin()
            FFLogin.Show()
            FFLogin.BringToFront()
        End Try

    End Sub
#Region "Export to Excel"
    Private Sub ExportToExcel()
        Dim xls As New Excel.Application
        Dim book As Excel.Workbook
        Dim sheet As Excel.Worksheet

        If lvLogs.Items.Count < 1 Then
            Exit Sub
        End If
        'create a workbook and get reference to first worksheet
        xls.Workbooks.Add()
        book = xls.ActiveWorkbook
        sheet = book.ActiveSheet
        'Get columns Headers
        For i As Integer = 0 To lvLogs.Columns.Count - 1
            sheet.Cells(1, i + 1) = lvLogs.Columns(i).Text
        Next

        'step through rows and columns and copy data to worksheet
        Dim row As Integer = 1
        Dim col As Integer = 1

        ToolStripPB.Minimum = 0
        ToolStripPB.Maximum = lvLogs.Items.Count
        ToolStripPB.Visible = True
        For Each item As ListViewItem In lvLogs.Items
            For i As Integer = 0 To item.SubItems.Count - 1
                sheet.Cells(row + 1, col) = item.SubItems(i).Text
                col = col + 1
            Next
            row += 1
            col = 1
            ToolStripPB.Value += 1
        Next

        For i As Integer = 0 To lvLogs.Columns.Count - 1
            sheet.Columns(i + 1).AutoFit()
        Next
        ToolStripPB.Visible = False
        'save the workbook and clean up
        xls.Visible = True
        'book.SaveAs("c:\sssss")
        'xls.Workbooks.Close()
        'xls.Quit()
        releaseObject(sheet)
        releaseObject(book)
        releaseObject(xls)
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        'Release an automation object
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

#End Region

    Private Sub cbLocation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
        Else
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub frmAutoSMSReply_MenuComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MenuComplete

    End Sub

    Private Sub frmMySQL_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim versionNumber As Version
        frmLogin.Hide()
        FFLogin.Hide()

        versionNumber = Assembly.GetExecutingAssembly().GetName().Version
        Me.Text = "Auto SMS Reply " & versionNumber.ToString
        Me.Text = "Auto SMS Reply " & versionNumber.ToString

    End Sub

    Private Sub btRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRead.Click
        For Each item As ListViewItem In lvLogs.Items
            Dim id As String = item.SubItems(1).Text.Trim
            Dim sms As String = item.SubItems(2).Text.Trim
            Dim smsSender As String = item.SubItems(3).Text.Trim
            'SPLIT MESSAGES FOR VALIDATING (AND WHAT MESSAGE WILL BE SENT)
            SplitSMS(sms, smsSender, id)
        Next
    End Sub

    Function AnotherConversion(ByVal s As String) As String
        Dim i As Integer
        Dim sb As New System.Text.StringBuilder(s.Length \ 2)
        For i = 0 To s.Length - 2 Step 2
            sb.Append(Chr(Convert.ToByte(s.Substring(i, 2), 16)))
        Next
        Return sb.ToString
    End Function


    Function HexToString(ByVal hex As String) As String
        Try
            Dim text As New System.Text.StringBuilder(hex.Length \ 2)
            For i As Integer = 0 To hex.Length - 2 Step 2
                text.Append(Chr(Convert.ToByte(hex.Substring(i, 2), 16)))
            Next
            Return text.ToString
        Catch ex As Exception

        End Try
      
    End Function

    Function SplitSMS(ByVal SMS As String, ByVal smsSender As String, ByVal ID As String)
    
        Dim str() As String
        Dim AppCode As String
        Dim pxInf As PatientInformation
        Dim SMS_out As String
        pxappt_count = 0

        SMS = SMS.ToString.Trim
        'CHECKPATIENTID
        Patient_ID(pxInf, smsSender)


        '---FOR TESTING PURPOSES / REMOVE THIS CODE ON ACTUAL IMPLEMENTATION

        'If smsSender = "+639059358605" Or smsSender = "+639989664970" Then
        '    GoTo exp_number
        'Else
        '    Exit Function
        'End If
exp_number:
        '--------------------------
        '------Updates by ian 07/04/16--------
        'read exemption mobile numbers  
        Dim TextLine As String
        Dim objReader As New System.IO.StreamReader(Application.StartupPath & "\Modem_no.dll")
        
        'check if the sender is the modem number
        Do While objReader.Peek() <> -1
            TextLine = objReader.ReadLine().Trim & vbNewLine
            'IF SENDER EQUAL THIS NUMBER, RETURN THE FUNCTION   
            If smsSender = TextLine.Trim Then
                UpdateSMS_ID("delete from `Messages` WHERE id='" & ID & "'")
                smS_no = smsSender
                Exit Function
            End If
        Loop

        '-----end of updates----

        If SMS Is Nothing Then
            CommandXpertSMS(smsFormat, 0, "", "", smsSender)                            'Messages ( Database : Messages )
            UpdateSMS_ID("UPDATE `Messages` SET Stats='read' WHERE id='" & ID & "'")    
            Exit Function
        End If

        str = StrConv(SMS.ToString.Trim(), VbStrConv.Uppercase).Split(" ")

        Select Case str.Length
            Case 1
                Select Case StrConv(SMS.ToString.Trim, VbStrConv.Uppercase)
                    Case "HELP"

                        SMS_out = SMS_queries_Key("HELP")
                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)

                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)

                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                        Exit Function

                    Case "BB"

                        SMS_out = SMS_queries_Key("BB")
                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)

                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)

                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")

                        Exit Function
                    Case "CAREERS"

                        SMS_out = SMS_queries_Key("CAREERS")
                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)
                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)

                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                        Exit Function

                    Case "ADDRESS"

                        SMS_out = SMS_queries_Key("ADDRESS")
                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)

                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)

                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                        Exit Function
                    Case "PROMOS"

                        SMS_out = SMS_queries_Key("PROMOS")
                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)

                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)

                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                        Exit Function

                    Case "OFF"
                        Dim updateSMSAlert As String = smsSender
                        updateSMSAlert = Mid(smsSender, 4, 10)

                        If SMS_Alert_OFF(updateSMSAlert) > 0 Then
                            SMS_out = "Hi " & PX_MrMs(pxInf.Gender) & pxInf.Name & "! Your message is acknowledged!" & vbNewLine _
                            & "Thank you and have a Belo Beautiful `! " & vbNewLine & vbNewLine _
                            & "This is a system generated message. " & vbNewLine & vbNewLine _
                            & FooterInquiries
                        'Else
                        '    SMS_out = "Your " & smsSender & " mobile number is not registed on our database." & vbNewLine _
                        '    & "Thank you and have a Belo Beautiful Day! " & vbNewLine & vbNewLine _
                        '    & "This is a system generated message. " & vbNewLine & vbNewLine _
                        '    & FooterInquiries

                        End If

                        'SMS_Alert_OFF(updateSMSAlert) > 0 Then
                        '    SMS_out = "Hi " & PX_MrMs(pxInf.Gender) & pxInf.Name & "! Your message is acknowledged!" & vbNewLine _
                        '             & "Thank you and have a Belo Beautiful Day! " & vbNewLine & vbNewLine _
                        '             & "This is a system generated message. " & vbNewLine & vbNewLine _
                        '             & FooterInquiries
                        
                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)
                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)
                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                        Exit Function

                    Case "ON"
                        Dim updateSMSAlert As String = smsSender
                        updateSMSAlert = Mid(smsSender, 4, 10)

                        If SMS_Alert_ON(updateSMSAlert) > 0 Then
                            SMS_out = "Hi " & PX_MrMs(pxInf.Gender) & pxInf.Name & "! Your message is acknowledged!" & vbNewLine _
                            & "Thank you and have a Belo Beautiful Day! " & vbNewLine & vbNewLine _
                            & "This is a system generated message. " & vbNewLine & vbNewLine _
                            & FooterInquiries
                        Else
                            SMS_out = "Your " & smsSender & " mobile number is not registed on our database." & vbNewLine _
                            & "Thank you and have a Belo Beautiful Day! " & vbNewLine & vbNewLine _
                            & "This is a system generated message. " & vbNewLine & vbNewLine _
                            & FooterInquiries
                        End If

                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)

                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)
                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                        Exit Function
                    Case Else
                        GoTo Jump_on_case_1
                End Select
            Case 2
                Dim hasRows As Boolean = False
                Dim strAppCode() As String
                strAppCode = SMS.Split(" ")
                Dim getresult As String = Replace(strAppCode(0), "'", "")
                query = ""
                'query = "select code from ref_branch where autosms = '1' and code = '" & getresult & "'"
                query = "select id from `branches` where id = '" & getresult & "'"
                Dim connection3 As New MySqlConnection(connStrBMG)
                Dim cmd3 As New MySqlCommand(query, connection3)
                Dim reader3 As MySqlDataReader
                Dim sql3 As String = ""

                connection3.Open()
                reader3 = cmd3.ExecuteReader()

                If reader3.HasRows = True Then
                    hasRows = True
                Else
                    hasRows = False
                End If
                connection3.Close()

                If hasRows = True Then
                    GoTo Approved_branch
                ElseIf SMS = "ABOUT BELO" Then
                    GoTo Approved_branch
                Else
                    GoTo Jump_on_case_1
                End If
Approved_branch:
                Select Case StrConv(SMS.ToString.Trim, VbStrConv.Uppercase)

                    Case "ABOUT BELO"
                        messages_sms_in(SMS.Trim, "", pxInf.ID, smsSender)
                        messages_sms_out(AboutBelo.Trim, "", pxInf.ID, smsSender)

                        CommandXpertSMS(SMS_out.Trim, 0, "", pxInf.ID, smsSender)
                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                        Exit Function

                        'Case "01 Nqwe", "04 Nqwe", "07 Nqwe"
                    Case "01 NO", "02 NO", "03 NO", "04 NO", "05 NO", "06 NO", "07 NO", "08 NO", "09 NO", "10 NO", "11 NO", "12 NO", "13 NO", "14 NO", "15 NO", "22 NO"
                        AppointmentSchedule = "no"
                        'NOBRANCH
                        Dim brnchCode As String = SMS_branchCode(SMS)
                        Dim BrnchInfo As BranchName_DB

                        If pxInf.IDNO.Length > 0 Then
                            Dim BranchDB As String = BranchCode(BrnchInfo, brnchCode)
                            'SEARCH APPOINTMENT IN MESSAGES_SMS
                            Dim check_appointment As String = searchAppointment(smsSender, False, pxInf.IDNO)

                            If check_appointment = "invalid_keyword" Then
                                GoTo Jump_on_case_final
                            End If

                            If check_appointment = "CANCELLED_1" And pxappt_count = 1 Then

                                'CANCELLED_1
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", Your appointment on " & AppointmentSchedule & " has been cancelled." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", Your appointment for today at " & AppointmentScheduleTime & " has been cancelled." & vbNewLine _
                           & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                           & "This is a system generated message." & vbNewLine & vbNewLine _
                           & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                           & FooterInquiries


                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)        'Belo_database ( Messages_SMS )
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)        'Messages ( Database : Messages ) 

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")

                                Exit Function

                                'more than 1 appointment
                            ElseIf check_appointment = "CANCELLED_1" And pxappt_count > 1 Then

                                'CANCELLED_1
                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ",All Your appointments for today have been cancelled." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries


                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CANCELLED_2" And pxappt_count_cancel = 1 Then

                                'CANCELLED_2

                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been cancelled already." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment appointment for today at " & AppointmentScheduleTime & " has been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'more than 1 appointment
                            ElseIf check_appointment = "CANCELLED_2" And pxappt_count_cancel > 1 Then

                                'CANCELLED_2

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that all your requested appointments for today have been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            Else
                                GoTo Jump_on_case_final
                                'NO Appointment
                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that you do not have any appointment for today." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function
                            End If
                        Else
                            GoTo Jump_on_case_1
                        End If

                    Case "01 YES", "02 YES", "03 YES", "04 YES", "05 YES", "06 YES", "07 YES", "08 YES", "09 YES", "10 YES", "11 YES", "12 YES", "13 YES", "14 YES", "15 YES", "22 YES"
                        AppointmentSchedule = "yes"

                        Dim brnchCode As String = SMS_branchCode(SMS)
                        Dim BrnchInfo As BranchName_DB
                      
                        If pxInf.IDNO.Length > 0 Then
                            'CHECK APPOINTMENT IN MESSAGES_SMS
                            Dim BranchDB As String = BranchCode(BrnchInfo, brnchCode)
                            'Search for Today's Appointments 
                            Dim check_appointment As String = searchAppointment(smsSender, True, pxInf.IDNO)

                            If check_appointment = "invalid_keyword" Then
                                GoTo Jump_on_case_final
                            End If

                            If check_appointment = "CONFIRMED_1" And pxappt_count = 1 Then

                                'CONFIRMED_1

                                '  SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been confirmed." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _mh]
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment for today at " & AppointmentScheduleTime & " has been confirmed." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function


                            ElseIf check_appointment = "CONFIRMED_1" And pxappt_count > 1 Then

                                'CONFIRMED_1

                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". all your appointments for today have been confirmed." & vbNewLine _
                              & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                              & "This is a system generated message." & vbNewLine & vbNewLine _
                              & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CONFIRMED_2" And pxappt_count = 1 Then

                                'CONFIRMED_2
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been confirmed already." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for today at " & AppointmentScheduleTime & " has been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                               & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                               & "This is a system generated message." & vbNewLine & vbNewLine _
                               & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CONFIRMED_2" And pxappt_count > 1 Then
                                'CONFIRMED_2

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that all your requested appointments for today have been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                               & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                               & "This is a system generated message." & vbNewLine & vbNewLine _
                               & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CONFIRMED_3" And pxappt_count = 1 Then

                                'CONFIRMED_3
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been confirmed already." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for today at " & AppointmentScheduleTime & " has been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                               & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                               & "This is a system generated message." & vbNewLine & vbNewLine _
                               & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'MORE THAN ONE APPOINTMENT
                            ElseIf check_appointment = "CONFIRMED_3" And pxappt_count > 1 Then

                                'CONFIRMED_3

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that all your requested appointments for today have been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                               & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                               & "This is a system generated message." & vbNewLine & vbNewLine _
                               & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CANCELLED" And pxappt_count_cancel = 0 Then
                                'CANCELLED
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been cancelled already." & vbNewLine _
                                '& "To reconfirm your appointment, you may call 819 - BELO (2356)" & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."


                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". we would like to inform you that your requested appointment for today at " & AppointmentScheduleTime & " has been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                           & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                           & "This is a system generated message." & vbNewLine & vbNewLine _
                           & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function


                            ElseIf check_appointment = "CANCELLED" And pxappt_count_cancel = 1 Then
                                'CANCELLED
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been cancelled already." & vbNewLine _
                                '& "To reconfirm your appointment, you may call 819 - BELO (2356)" & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group." 


                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". we would like to inform you that your requested appointment for today at " & AppointmentScheduleTime & " has been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                           & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                           & "This is a system generated message." & vbNewLine & vbNewLine _
                           & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'MORE THAN ONE APPOINTMENT
                            ElseIf check_appointment = "CANCELLED" And pxappt_count_cancel > 1 Then
                                'CANCELLED


                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". we would like to inform you that all your requested appointments for today have been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                           & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                           & "This is a system generated message." & vbNewLine & vbNewLine _
                           & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CHANGE_STATS" Then


                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for tomorrow at " & AppointmentScheduleTime & " has been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            Else
                                GoTo Jump_on_case_final
                                'NO Appointment
                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that you do not have any appointment for today." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function
                            End If
                        Else
                            GoTo Jump_on_case_1
                        End If

                        ' Case "01 DENYqwe", "04 DENYqwe", "07 DENYqwe"
                    Case "01 DENY", "02 DENY", "03 DENY", "04 DENY", "05 DENY", "06 DENY", "07 DENY", "08 DENY", "09 DENY", "10 DENY", "11 DENY", "12 DENY", "13 DENY", "14 DENY", "15 DENY", "22 DENY"
                        'BRANCHDENY
                        Dim brnchCode As String = SMS_branchCode(SMS)
                        Dim BrnchInfo As BranchName_DB

                        If pxInf.IDNO.Length > 0 Then
                            Dim BranchDB As String = BranchCode(BrnchInfo, brnchCode)
                            'CHECKDENY
                            Dim check_appointment As String = searchAppointmentTomorrow(smsSender, False, pxInf.IDNO)

                            If check_appointment = "invalid_keyword" Then
                                GoTo Jump_on_case_final
                            End If

                            If check_appointment = "CANCELLED_1" And pxappt_count = 1 Then

                                'CANCELLED_1
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been cancelled." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment for tomorrow at " & AppointmentScheduleTime & " has been cancelled." & vbNewLine _
                               & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                               & "This is a system generated message." & vbNewLine & vbNewLine _
                               & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CANCELLED_1" And pxappt_count > 1 Then

                                'CANCELLED_1
                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". all your appointments for tomorrow  have been cancelled." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CANCELLED_2" And pxappt_count_cancel = 1 Then

                                'CANCELLED_2
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been cancelled already." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for tomorrow at " & AppointmentScheduleTime & " has been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'morethan 1 appointments
                            ElseIf check_appointment = "CANCELLED_2" And pxappt_count_cancel > 1 Then

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that all your requested appointments for tomorrow have been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function


                            ElseIf check_appointment = "CANCELLED_3" And pxappt_count = 1 Then

                                'CANCELLED_3
                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", Your appointment for tomorrow at " & AppointmentScheduleTime & " has been cancelled already." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'morethan 1 ppointment
                            ElseIf check_appointment = "CANCELLED_3" And pxappt_count > 1 Then

                                'CANCELLED_3
                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", all your appointments for tomorrow have been cancelled already." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function


                            Else
                                GoTo Jump_on_case_final
                                'NO Appointment
                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & " we would like to inform you that you do not have any appointment for tomorrow." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function
                            End If
                        Else
                            GoTo Jump_on_case_1
                        End If


                        ' Case "01 ACCEPTqwe", "04 ACCEPTqwe", "07 ACCEPTqwe"

                    Case "01 ACCEPT", "02 ACCEPT", "03 ACCEPT", "04 ACCEPT", "05 ACCEPT", "06 ACCEPT", "07 ACCEPT", "08 ACCEPT", "09 ACCEPT", "10 ACCEPT", "11 ACCEPT", "12 ACCEPT", "13 ACCEPT", "14 ACCEPT", "15 ACCEPT", "22 ACCEPT"
YES_TO_ACCEPT:
                        yestoaccept = False
                        Dim brnchCode As String = SMS_branchCode(SMS)
                        Dim BrnchInfo As BranchName_DB
                        'BRANCHACCEPT   
                        If pxInf.IDNO.Length > 0 Then
                            Dim BranchDB As String = BranchCode(BrnchInfo, brnchCode)
                            Dim check_appointment As String = searchAppointmentTomorrow(smsSender, True, pxInf.IDNO)
                            'CHECKACCEPT
                            If check_appointment = "invalid_keyword" Then
                                GoTo Jump_on_case_final
                            End If

                            If check_appointment = "CONFIRMED_1" And pxappt_count = 1 Then

                                'CONFIRMED_1
                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment for tomorrow at " & AppointmentScheduleTime & " has been confirmed." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'more than 1 appointment
                            ElseIf check_appointment = "CONFIRMED_1" And pxappt_count > 1 Then

                                'CONFIRMED_1
                                SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". all your appointments for Tomorrow have been confirmed." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")

                                pxappt_count = 0
                                Exit Function

                            ElseIf check_appointment = "CONFIRMED_2" And pxappt_count = 1 Then

                                'CONFIRMED_2
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been confirmed already." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for tomorrow at " & AppointmentScheduleTime & " has been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)        '
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)        '
                                
                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)             'Messages ( Database : Messages )
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'morethan 1 appoinment
                            ElseIf check_appointment = "CONFIRMED_2" And pxappt_count > 1 Then

                                'CONFIRMED_2
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been confirmed already." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that all your requested appointments for tomorrow have been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CONFIRMED_3" And pxappt_count = 1 Then

                                'CONFIRMED_3
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been confirmed already." & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for tomorrow at " & AppointmentScheduleTime & " has been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                                'morethan 1 appoinment
                            ElseIf check_appointment = "CONFIRMED_3" And pxappt_count > 1 Then

                                'CONFIRMED_3

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that all your requested appointments for tomorrow have been confirmed based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CANCELLED" And pxappt_count_cancel = 1 Then
                                'CANCELLED
                                'SMS_out = "Thank you for your immediate response " & PX_MrMs(pxInf.Gender) & pxInf.Name & ". Your appointment on " & AppointmentSchedule & " has been cancelled already." & vbNewLine _
                                '& "To reconfirm your appointment, you may call 819 - BELO (2356)" & vbNewLine _
                                '& "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                '& "This is a system generated message." & vbNewLine & vbNewLine _
                                '& "-" & BrnchInfo.Name & ", Belo Medical Group."

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for tomorrow at " & AppointmentScheduleTime & " has been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                           & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                           & "This is a system generated message." & vbNewLine & vbNewLine _
                           & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function
                                'morethan 1 appoinment
                            ElseIf check_appointment = "CANCELLED" And pxappt_count_cancel > 1 Then
                                'CANCELLED

                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that all your requested appointments for tomorrow have been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                           & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                           & "This is a system generated message." & vbNewLine & vbNewLine _
                           & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            ElseIf check_appointment = "CHANGE_STATS" Then


                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that your requested appointment for tomorrow at " & AppointmentScheduleTime & " has been cancelled based on your previous response to our SMS confirmation." & vbNewLine _
                             & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                             & "This is a system generated message." & vbNewLine & vbNewLine _
                             & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function

                            Else
                                GoTo Jump_on_case_final
                                'NO Appointment
                                SMS_out = "Good day, " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", we would like to inform you that you do not have any appointment for tomorrow." & vbNewLine _
                                & "Thank you and have a Belo Beautiful Day!" & vbNewLine & vbNewLine _
                                & "This is a system generated message." & vbNewLine & vbNewLine _
                                & "-" & BrnchInfo.Name & ", Belo Medical Group." & vbNewLine & vbNewLine _
                                & FooterInquiries

                                messages_sms_in_branch(SMS.Trim, brnchCode, pxInf.ID, smsSender)
                                messages_sms_out_branch(SMS_out, brnchCode, pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out, 0, brnchCode, pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & brnchCode & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function
                            End If
                        Else
                            GoTo Jump_on_case_1
                        End If

                    Case Else
                        GoTo Jump_on_case_1
                End Select

            Case Else
Jump_on_case_1:
                Dim strAppCode() As String
                AppCode = str(0)
                AppCode = AppCode.Trim

                'Split the SMS string
                strAppCode = StrConv(AppCode, VbStrConv.Uppercase).Split(" ")

                Select Case strAppCode.Length
                    Case 1
                        Dim BrnchInfo As BranchName_DB
                        Dim BranchDB As String = BranchCode(BrnchInfo, strAppCode(0))

                        If Len(BranchDB.Trim) > 12 Then

                            'SMS_out = "Hi " & PX_MrMs(pxInf.Gender) & pxInf.Name & "! Your message is acknowledged!" & vbNewLine _
                            '& "Thank you and have a Belo Beautiful Day! " & vbNewLine & vbNewLine _
                            '& "This is a system generated message. " & vbNewLine & vbNewLine _
                            '& "-" & BrnchInfo.Name & ", Belo Medical Group. " & vbNewLine & vbNewLine _
                            '& FooterInquiries

                            SMS_out = "Hi " & PX_MrMs(pxInf.Gender) & pxInf.Name & ", " & vbNewLine & vbNewLine & "This is to acknowledge the receipt of your message. Please expect a call within 24 hours from our Patient Care Specialist to confirm that " & _
                            "your request has been processed. " & vbNewLine & vbNewLine & "Thank you and have a Belo Beautiful Day!!!" & vbNewLine & vbNewLine _
                           & "This is a system generated message. " & vbNewLine & vbNewLine _
                           & "-" & BrnchInfo.Name & ", Belo Medical Group. " & vbNewLine & vbNewLine _
                           & FooterInquiries


                            'sms_body, branchcode,px_id As String, sms_sender
                            messages_sms_in_branch(SMS.Trim, strAppCode(0), pxInf.ID, smsSender)
                            messages_sms_out_branch(SMS_out.Trim, strAppCode(0), pxInf.ID, smsSender)

                            CommandXpertSMS(SMS_out.Trim, 0, strAppCode(0), pxInf.ID, smsSender)
                            UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & strAppCode(0) & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                            Exit Function
                        Else
                            Dim _footer As String = Department_reply_sms(strAppCode(0))

                            If Len(_footer) >= 8 Then
                                SMS_out = _footer
                                messages_sms_in_department(SMS.Trim, strAppCode(0), pxInf.ID, smsSender)
                                messages_sms_out_department(SMS_out.Trim, strAppCode(0), pxInf.ID, smsSender)

                                CommandXpertSMS(SMS_out.Trim, 0, strAppCode(0), pxInf.ID, smsSender)
                                UpdateSMS_ID("UPDATE `Messages` SET Stats='read',branch='" & strAppCode(0) & "', PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                                Exit Function
                            Else
                                GoTo Jump_on_case_final
                            End If

                        End If

                    Case Else
Jump_on_case_final:
                        SMS_out = "Sorry you may have entered an invalid keyword, Please reply HELP for more information." _
                        & vbNewLine & vbNewLine & FooterInquiries

                        'Invalid Function for in only
                        messages_sms_in_invalid(SMS.Trim, pxInf.ID, smsSender)      'Belo_Database (Messages_SMS)
                        messages_sms_out(SMS_out.Trim, "", pxInf.ID, smsSender)     'Belo_Database (Messages_SMS)

                        CommandXpertSMS(SMS_out.Trim, 1, "", pxInf.ID, smsSender)   'Message (Database : Messages)
                        UpdateSMS_ID("UPDATE `Messages` SET Stats='read', validity=1, PatientID='" & pxInf.ID & "' WHERE id='" & ID & "'")
                End Select

        End Select

    End Function
    Function Patient_ID(ByRef Destination As PatientInformation, ByVal pxMobile As String) As String
        Dim sql As String = ""

        Dim SMSpxMobile As String = Mid(pxMobile, 4, 10)

        sql = "SELECT Patient_ID ,CONCAT(first_name, ' ' ,last_name) pxName, gender FROM `patient` WHERE REPLACE(REPLACE(mobile_number,' ',''),'.','') LIKE '%" & Replace(SMSpxMobile, "'", "\'") & "%'"
        'MsgBox(sql)
        Dim connection As New MySqlConnection(connStrBMG)
        Dim cmd As New MySqlCommand(sql, connection)
        Dim reader As MySqlDataReader

        connection.Open()
        reader = cmd.ExecuteReader()
        'on Error Resume next
        If reader.HasRows = True Then
            While reader.Read
                Destination.ID = reader.Item("Patient_ID").ToString()
                Destination.IDNO = Destination.ID
                Patient_ID = Destination.ID
                Destination.Name = reader.Item("pxName").ToString()
                Destination.Gender = reader.Item("gender").ToString()
            End While
        Else
            Destination.ID = ""
            Patient_ID = Destination.ID
            Destination.Name = ""
            Destination.Gender = ""
            Destination.IDNO = ""
        End If

        connection.Close()
    End Function


    Function BranchCode(ByRef Destination As BranchName_DB, ByVal BrchCode As String) As String
        Try
            Dim sql As String = ""

            If BrchCode = "00" Then
                BranchCode = ""
                Exit Function
            End If

            BrchCode = Replace(BrchCode.Trim, "'", "\'")
            'REMOVED db_name,code
            'sql = "SELECT ip,username,password,port,code,name,db_name FROM ref_branch WHERE code='" & BrchCode & "'"
            sql = ""
            sql = "SELECT ip,username,password,port,id,name FROM branches WHERE id='" & BrchCode & "'"
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)
            Dim reader As MySqlDataReader
            Dim smsResult As String = ""
            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then
                While reader.Read
                    Destination.Code = reader.Item("id").ToString()
                    Destination.Name = reader.Item("name").ToString()
                    Destination.db_name = "appointment" 'reader.Item("db_name").ToString()

                    branch_ip = reader.Item("ip").ToString()
                    branch_un = reader.Item("username").ToString()
                    branch_pass = reader.Item("password").ToString()
                    branch_port = reader.Item("port").ToString()
                    BranchCode = Destination.db_name
                End While
            Else
                Destination.Code = ""
                Destination.Name = ""
                Destination.db_name = ""
                BranchCode = ""
            End If
            connection.Close()
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function AppointmentBranch(ByVal BranchDB_NAME As String, ByVal px_ID As String) As Integer
        Dim sql As String = ""
        'CHECK # OF APPOINTMENTS
        sql = "SELECT COUNT(id) AS id FROM " & BranchDB_NAME & " WHERE DAY(appointment_date)>=DAY(NOW()) AND MONTH(appointment_date)>=MONTH(NOW()) " _
            & " AND YEAR(appointment_date)>=YEAR(NOW()) AND patientid='" & px_ID & "'"
        'MsgBox(sql)
        Dim connection As New MySqlConnection(connStrBMG)
        Dim cmd As New MySqlCommand(sql, connection)
        Dim reader As MySqlDataReader

        connection.Open()
        reader = cmd.ExecuteReader()

        If reader.HasRows = True Then
            While reader.Read
                AppointmentBranch = reader.Item("id").ToString()
            End While
        Else
            AppointmentBranch = 0
        End If

        connection.Close()
    End Function

    Function messages_sms_in_department(ByVal sms_body As String, ByVal DeptKey As String, ByVal px_id As String, ByVal sms_sender As String) As Integer
        Dim sql As String = ""
        sms_body = Replace(sms_body, "'", "\'")
        Try
            sql = "INSERT INTO `messages_sms` SET Direction=1, sender='" & sms_sender & "',body='" & sms_body & "',PatientID='" & px_id & "',branch='" & DeptKey & "',DeptKey='" & DeptKey & "'"

            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function messages_sms_out_department(ByVal sms_body As String, ByVal DeptKey As String, ByVal px_id As String, ByVal sms_sender As String) As Integer
        Try
            Dim MBchk As String = sms_sender
            Dim sql As String = ""
            sms_body = Replace(sms_body, "'", "\'")

            MBchk = Mid(MBchk, 1, 4)
            If MBchk = "+639" Then
                sql = "INSERT INTO `messages_sms` SET Direction=2, Read_Stats=1, sender='" & sms_sender & "',body='" & sms_body & "',PatientID='" & px_id & "',branch='" & DeptKey & "',DeptKey='" & DeptKey & "',Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

                Dim rowsEffected As Integer = 0
                Dim connection As New MySqlConnection(connStrBMG)
                Dim cmd As New MySqlCommand(sql, connection)

                connection.Open()

                rowsEffected = cmd.ExecuteNonQuery()

                connection.Close()

                Return rowsEffected
            End If
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function messages_sms_in_branch(ByVal sms_body As String, ByVal branchcode As String, ByVal px_id As String, ByVal sms_sender As String) As Integer
        Dim sql As String = ""
        sms_body = Replace(sms_body, "'", "\'")
        Try
            sql = "INSERT INTO `messages_sms` SET Direction=1, sender='" & sms_sender & "',body='" & sms_body & "',PatientID='" & px_id & "',branch='" & branchcode & "',DeptKey='" & branchcode & "'"

            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function messages_sms_out_branch(ByVal sms_body As String, ByVal branchcode As String, ByVal px_id As String, ByVal sms_sender As String) As Integer
        Try
            Dim MBchk As String = sms_sender
            Dim sql As String = ""
            sms_body = Replace(sms_body, "'", "\'")
            MBchk = Mid(MBchk, 1, 4)

            If MBchk = "+639" or instr(1,MBchk,"639") Then
                sql = "INSERT INTO `messages_sms` SET Direction=2, Read_Stats=1, sender='" & sms_sender & "',body='" & sms_body & "',PatientID='" & px_id & "',branch='" & branchcode & "',DeptKey='" & branchcode & "',Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

                Dim rowsEffected As Integer = 0
                Dim connection As New MySqlConnection(connStrBMG)
                Dim cmd As New MySqlCommand(sql, connection)

                connection.Open()

                rowsEffected = cmd.ExecuteNonQuery()

                connection.Close()

                Return rowsEffected
            End If
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function messages_sms_in_invalid(ByVal sms_body As String, ByVal px_id As String, ByVal sms_sender As String) As Integer
        Dim sql As String = ""
        sms_body = Replace(sms_body, "'", "\'")
        Try
            sql = "INSERT INTO `messages_sms` SET Direction=1, invalid=1, sender='" & sms_sender & "',body='" & sms_body & "', PatientID='" & px_id & "',Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function messages_sms_in(ByVal sms_body As String, ByVal Branch_code As String, ByVal px_id As String, ByVal sms_sender As String) As Integer
        Dim sql As String = ""
        sms_body = Replace(sms_body, "'", "\'")
        Try
            sql = "INSERT INTO `messages_sms` SET Direction=1, sender='" & sms_sender & "',body='" & sms_body & "',branch='" & Branch_code & "', PatientID='" & px_id & "'"

            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function

    Function messages_sms_out(ByVal sms_body As String, ByVal Branch_code As String, ByVal px_id As String, ByVal sms_sender As String) As Integer
        Try
            Dim MBchk As String = sms_sender
            Dim sql As String = ""
            sms_body = Replace(sms_body, "'", "\'")

            MBchk = Mid(MBchk, 1, 4)
            If MBchk = "+639" or Mid(MBchk,1,3) = "639" Then                                 
                sql = "INSERT INTO `messages_sms` SET Direction=2, sender='" & sms_sender & "',body='" & sms_body & "',branch='" & Branch_code & "', PatientID='" & px_id & "', " _
                & " Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

                Dim rowsEffected As Integer = 0
                Dim connection As New MySqlConnection(connStrBMG)
                Dim cmd As New MySqlCommand(sql, connection)

                connection.Open()

                rowsEffected = cmd.ExecuteNonQuery()

                connection.Close()

                Return rowsEffected
            End If
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function

    Function SMS_Alert_ON(ByVal pxSMobile As String) As Integer
        Dim sql As String = ""

        Try
            sql = "UPDATE patient_info SET sms_alert=1 WHERE REPLACE(REPLACE(REPLACE(mobile,' ',''),'-',''),'.','') LIKE '%" & pxSMobile & "%'"
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            SMS_Alert_ON = rowsEffected

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function SMS_Alert_OFF(ByVal pxMobile As String) As Integer
        Dim sql As String = ""

        Try
            sql = "UPDATE patient_info SET sms_alert=0 WHERE REPLACE(REPLACE(REPLACE(mobile,' ',''),'-',''),'.','') LIKE '%" & pxMobile & "%'"

            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        lbTime.Text = Format(Now(), "ss")

        If Format(Now(), "HH:mm:ss") = "2:00:00" Then
            belo_module.SynchronizeDatabaseMessages()
            Exit Sub
        End If

        If Format(Now(), "HH:mm:ss") = "02:30:00" Then
            belo_module.SynchronizeDatabaseBeloMessages()
            Exit Sub
        End If

        If Format(Now(), "ss") = 15 Or Format(Now(), "ss") = 30 Or Format(Now(), "ss") = 45 Then
            'VIEW RECEIVED MESSAGES
            btViewAll_Click(Me, EventArgs.Empty)
            'SEND MESSAGE TO SENDER
            btResendAll_Click(Me, EventArgs.Empty)
            smS_no = ""
        Else
            If Format(Now(), "ss") = 1 And Format(Now(), "HH") >= 20 Then
                Try
                    FFIssuedCard.Show()
                    FFIssuedCard.Hide()
                    FFIssuedCard.BringToFront()
                Catch ex As Exception
                    FFIssuedCard = New frmIssuedCard()
                    FFIssuedCard.Show()
                    FFIssuedCard.Hide()
                    FFIssuedCard.BringToFront()
                End Try
            End If
        End If
        lvLogs.Items.Clear()
    End Sub

    Private Sub btRestartServiceSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRestartServiceSMS.Click
        Try
            Dim service As ServiceController = New ServiceController("AxSmsSvc", System.Net.Dns.GetHostName())

            btRestartServiceSMS.Enabled = False

            If service.Status.Equals(ServiceControllerStatus.Stopped) Or service.Status.Equals(ServiceControllerStatus.StopPending) Then
                ' Start the service
                service.Start()
            Else
                ' Stop the service
                service.Stop()
                Thread.Sleep(30000)
                ' Start the service
                service.Start()
            End If
            btRestartServiceSMS.Enabled = True
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
            btRestartServiceSMS.Enabled = True
        End Try
    End Sub

    Private Sub ToolStripSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSetting.Click
        If FFGSMSet.IsDisposed = True Then
            FFGSMSet = New frmGSMSetting()
            FFGSMSet.Show()
            FFGSMSet.BringToFront()
        Else
            If FFGSMSet.Visible = True Then
                FFGSMSet.BringToFront()
            Else
                FFGSMSet.Show()
                FFGSMSet.BringToFront()
            End If
        End If
    End Sub

    Private Sub btResendAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResendAll.Click
        If smS_no <> "" Then
            Timer1.Enabled = True
            Exit Sub
        End If

        Dim sql As String

        Timer1.Enabled = False
        'DELETE (  sysForwarded=0;sysGwReference='' ) doesn't Exist in Database
        sql = "UPDATE `messages` SET StatusDetailsID=200, STATUSID=1, ChannelID=0, MessageReference='', SentTimeSecs=0, ReceivedTimeSecs=0, ScheduledTimeSecs=0, " _
        & "LastUpdateSecs=0, BodyFormatID=0, CustomField1=0, CustomField2='', sysCreator=0, sysArchive=0, sysLock=0, sysHash='', Header='' WHERE DirectionID=2 AND TYPEID=2 AND STATUSID=3"
        SMS_UPDATE(sql)

        Timer1.Enabled = True

    End Sub
    Function Department_reply_sms(ByVal DeptKey As String) As String
        Try
            Dim sql As String = ""

            DeptKey = Replace(DeptKey, "'", "\'")

            sql = "SELECT IFNULL(dapertment_reply_sms,'Your message is acknowldeged! Thank you and have a Belo Beautiful Day!') dapertment_reply_sms FROM `ref_user_access` WHERE dapertment_enable=1 AND department_key='" & DeptKey & "'"
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)
            Dim reader As MySqlDataReader
            Dim smsResult As String = ""
            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then
                While reader.Read
                    Department_reply_sms = reader.Item("dapertment_reply_sms").ToString()
                End While
            Else
                Department_reply_sms = ""
            End If
            connection.Close()
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function
    Function SMS_queries_Key(ByVal SMStKey As String) As String
        Try
            Dim sql As String = ""

            SMStKey = Replace(SMStKey, "'", "\'")
            sql = "SELECT sms_description FROM sms_queries WHERE sms_key='" & SMStKey & "'"
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)
            Dim reader As MySqlDataReader
            Dim smsResult As String = ""
            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then
                While reader.Read
                    SMS_queries_Key = reader.Item("sms_description").ToString()
                End While
            Else
                SMS_queries_Key = ""
            End If
            connection.Close()
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function

    Function end_footer() As String
        Try
            Dim sql As String = ""

            sql = "SELECT end_footer FROM sms_autoreply_footer WHERE id=1"
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)
            Dim reader As MySqlDataReader
            Dim smsResult As String = ""
            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then
                While reader.Read
                    FooterInquiries = reader.Item("end_footer").ToString()
                    end_footer = FooterInquiries
                End While
            Else
                FooterInquiries = ""
                end_footer = FooterInquiries
            End If
            connection.Close()
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox(end_footer() & vbNewLine & vbNewLine & ClientHostName & vbNewLine & ClientHostIP, MsgBoxStyle.Information)
    End Sub

    Private Sub ErrorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ErrorsToolStripMenuItem.Click
        Try
            FFCatchError.Show()
            FFCatchError.BringToFront()
        Catch ex As Exception
            FFCatchError = New frmCatchError()
            FFCatchError.Show()
            FFCatchError.BringToFront()
        End Try
    End Sub

    Private Sub IssuedCardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IssuedCardToolStripMenuItem.Click
        Try
            FFIssuedCard.Show()
            FFIssuedCard.BringToFront()
        Catch ex As Exception
            FFIssuedCard = New frmIssuedCard()
            FFIssuedCard.Show()
            FFIssuedCard.BringToFront()
        End Try
    End Sub

    Private Sub ToolStripTextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox1.Click
        belo_module.SynchronizeDatabaseMessages()
        belo_module.SynchronizeDatabaseBeloMessages()
        MessageBox.Show("Done Synchronize Database", "Synchronization")
    End Sub

    Private Sub BeloToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeloToolStripMenuItem.Click
        belo_module.SynchronizeDatabaseBeloMessages()
        MessageBox.Show("Done Synchronize Database", "Synchronization-Belo Database")
    End Sub

    Private Sub MessagesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MessagesToolStripMenuItem.Click
        belo_module.SynchronizeDatabaseMessages()
        MessageBox.Show("Done Synchronize Database", "Synchronization-Messages")
    End Sub

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    'MOVE FORM WHEN PANEL MOVED
    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs)
        drag = True 'Sets the variable drag to true.
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs)
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs)
        drag = False
    End Sub



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        Try
            Me.Dispose()
            FFLogin.Show()
        Catch ex As Exception
            FFLogin = New frmLogin()
            FFLogin.Show()
            FFLogin.BringToFront()
        End Try
    End Sub



    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub



   
    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

  
  
End Class