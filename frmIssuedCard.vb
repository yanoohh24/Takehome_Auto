Imports MySql.Data.MySqlClient

Public Class frmIssuedCard
    Dim bpc_message As String
    Dim i As Integer = 60

    Private Sub ListViewPatientsContactSetting()
        lstIssuedCard.Columns.Clear()
        lstIssuedCard.Columns.Add("PID", 0)
        lstIssuedCard.Columns.Add("Gender", 55)
        lstIssuedCard.Columns.Add("Patient Name", 200)
        lstIssuedCard.Columns.Add("Mobile", 100)
        lstIssuedCard.Columns.Add("Activation Date", 100)
        lstIssuedCard.Columns.Add("Branch name", 100)
        lstIssuedCard.Columns.Add("Branch code", 80)
        lstIssuedCard.Columns.Add("SMS", 50)
        lstIssuedCard.Items.Clear()

    End Sub

    Private Sub frmIssuedCard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtMobile.Text = ""
        txtIssuedDate.Text = ""

        RowToolStrip.Text = "Rows: " & lstIssuedCard.Items.Count
        ListViewPatientsContactSetting()
        dpFrom.Value = Now()
        bpc_message = BPCMessage()

    End Sub

    Private Sub btView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btView.Click
        RetriveSMS()
    End Sub
    Public Sub RetriveSMS()
        Dim srchDate As String = Format(dpFrom.Value, "yyyy-MM-dd")
        ListViewPatientsContactSetting()

        Try
            Dim query As String

            query = "SELECT patientid," _
            & " (SELECT gender FROM patient_info WHERE patient_info.PatientID=loyalty_card_info.patientid) px_gender, " _
            & " (SELECT CONCAT(firstname,' ',lastname) FROM patient_info WHERE patient_info.PatientID=loyalty_card_info.patientid) px_name, " _
            & " (SELECT mobile FROM patient_info WHERE patient_info.PatientID=loyalty_card_info.patientid) px_mobile, " _
            & " (SELECT NAME FROM `ref_branch` WHERE CODE=issued_branch) Branch_name, issued_branch, card_status,activation_date, " _
            & " sms_stat FROM `loyalty_card_info` WHERE DATE(activation_date)= '" & srchDate & "' AND card_status LIKE 'issued%' AND sms_stat<1 ORDER BY activation_date DESC"

            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)
            Dim reader As MySqlDataReader

            connection.Open()
            reader = cmd.ExecuteReader()
            If reader.HasRows = True Then
                While reader.Read
                    Dim ls As New ListViewItem(reader.Item("PatientID").ToString.Trim())
                    ls.SubItems.Add(StrConv(reader.Item("px_gender").ToString.Trim(), VbStrConv.Uppercase))
                    ls.SubItems.Add(StrConv(reader.Item("px_name").ToString.Trim(), VbStrConv.ProperCase))
                    ls.SubItems.Add(Replace(Replace(Replace(reader.Item("px_mobile").ToString.Trim(), " ", ""), ".", ""), "-", ""))
                    ls.SubItems.Add(Format(reader.Item("activation_date"), "MMM dd, yyyy"))
                    ls.SubItems.Add(reader.Item("Branch_name").ToString.Trim())
                    ls.SubItems.Add(reader.Item("issued_branch").ToString.Trim())
                    ls.SubItems.Add(reader.Item("sms_stat").ToString.Trim())
                    lstIssuedCard.Items.Add(ls)
                End While
            End If
            connection.Close()
            RowToolStrip.Text = "Rows: " & lstIssuedCard.Items.Count
        Catch ex As Exception
            Console.WriteLine(ex.Message & "::RetriveSMS")
        End Try

    End Sub
    Public Sub RetrivePointsEarned()
        Dim srchDate As String = Format(dpFrom.Value, "yyyy-MM-dd")
        ListViewPatientsContactSetting()

        Try
            Dim query As String

            query = "SELECT patientid," _
            & " (SELECT gender FROM patient_info WHERE patient_info.PatientID=loyalty_card_info.patientid) px_gender, " _
            & " (SELECT CONCAT(TRIM(firstname),' ',TRIM(lastname)) FROM patient_info WHERE patient_info.PatientID=loyalty_card_info.patientid) px_name, " _
            & " (SELECT mobile FROM patient_info WHERE patient_info.PatientID=loyalty_card_info.patientid) px_mobile, " _
            & " (SELECT NAME FROM `ref_branch` WHERE CODE=issued_branch) Branch_name, issued_branch, card_status,activation_date, " _
            & " sms_stat FROM `loyalty_card_info` WHERE DATE(activation_date)= '" & srchDate & "' AND card_status LIKE 'issued%' AND sms_stat<1 ORDER BY activation_date DESC"

            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)
            Dim reader As MySqlDataReader

            connection.Open()
            reader = cmd.ExecuteReader()
            If reader.HasRows = True Then
                While reader.Read
                    Dim ls As New ListViewItem(reader.Item("PatientID").ToString.Trim())
                    ls.SubItems.Add(StrConv(reader.Item("px_gender").ToString.Trim(), VbStrConv.Uppercase))
                    ls.SubItems.Add(StrConv(reader.Item("px_name").ToString.Trim(), VbStrConv.ProperCase))
                    ls.SubItems.Add(Replace(Replace(Replace(reader.Item("px_mobile").ToString.Trim(), " ", ""), ".", ""), "-", ""))
                    ls.SubItems.Add(Format(reader.Item("activation_date"), "MMM dd, yyyy"))
                    ls.SubItems.Add(reader.Item("Branch_name").ToString.Trim())
                    ls.SubItems.Add(reader.Item("issued_branch").ToString.Trim())
                    ls.SubItems.Add(reader.Item("sms_stat").ToString.Trim())
                    lstIssuedCard.Items.Add(ls)
                End While
            End If
            connection.Close()
            RowToolStrip.Text = "Rows: " & lstIssuedCard.Items.Count
        Catch ex As Exception
            Console.WriteLine(ex.Message & "::RetriveSMS")
        End Try

    End Sub
    Private Sub btSendToAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSendToAll.Click
        Dim sql As String = ""

        bpc_message = BPCMessage()

        For Each item As ListViewItem In lstIssuedCard.Items
            Dim pxID As String = item.Text.Trim
            Dim pxGender As String = item.SubItems(1).Text.Trim
            Dim pxName As String = item.SubItems(2).Text.Trim
            Dim pxMobile As String = item.SubItems(3).Text.Trim
            Dim pxBranch As String = item.SubItems(5).Text.Trim
            Dim pxBranchCode As String = item.SubItems(6).Text.Trim
            Dim pxSMSStat As Integer = item.SubItems(7).Text.Trim

            pxName = PX_MrMs(pxGender) & pxName

            If Len(pxName) > 1 Then
                pxName = pxName
            End If

            px_mobile(pxMobile)

            If Mid(txtMobile.Text, 1, 4) = "+639" And pxSMSStat < 1 Then

                txtIssuedDate.Text = Replace(bpc_message, "<pxName>", pxName)

                messages_sms_out(txtIssuedDate.Text, pxID, "BPC", txtMobile.Text.Trim)
                CommandXpertSMS_BD(txtIssuedDate.Text.Trim, pxID, "BPC", txtMobile.Text.Trim)

                sql = "UPDATE `loyalty_card_info` SET sms_stat=1 WHERE Patient_id='" & pxId & "'"
                SMS_UPDATE_Loyalty(sql)

            End If

        Next

        Me.Dispose()

    End Sub
    Function CommandXpertSMS_BD(ByVal msg As String, ByVal px_id As String, ByVal px_branchCode As String, ByVal EmpMobile As String) As Integer
        Dim SMSmsg As String
        Dim query As String
        Dim MBchk As String = EmpMobile
        MBchk = Mid(MBchk, 1, 4)
        SMSmsg = Replace(msg, "'", "\'")
        'OLD DATABASE TYPE = 2 : SMS
        'NEW DATABASE TYPEID = 1 : SMS
        Try
            If MBchk = "+639" Then

                If Len(EmpMobile.Trim) = 13 Then

                    query = "INSERT INTO Messages SET DirectionID=2, TypeID=1, StatusDetailsID=200, StatusID=1, ChannelID=0, " _
                    & " Recipient='" & EmpMobile & "', Body='" & SMSmsg & "', branch='" & px_branchCode & "', PatientID='" & px_id & "', Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

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
            Console.WriteLine(ex.Message)
        End Try

    End Function
    Function px_mobile(ByVal mobile As String) As String

        Dim StrMobile As String

        mobile = Replace(Replace(mobile.Trim, " ", ""), "-", "")
        If Len(mobile) < 1 Then
            txtMobile.Text = ""
            Exit Function
        End If

        Select Case Mid(mobile.Trim, 1, 2)
            Case "09"
                If Len(mobile.Trim) >= 11 Then
                    StrMobile = Mid(mobile.Trim, 2, 10)
                    txtMobile.Text = "+63" & StrMobile
                    txtMobile.ForeColor = Color.Black
                Else
                    txtMobile.Text = ""
                    txtMobile.ForeColor = Color.Red
                End If
            Case "63"
                If Len(mobile.Trim) >= 12 Then
                    StrMobile = Mid(mobile.Trim, 1, 12)
                    txtMobile.Text = "+" & StrMobile
                    txtMobile.ForeColor = Color.Black
                Else
                    txtMobile.Text = ""
                    txtMobile.ForeColor = Color.Red
                End If
            Case Else
                Select Case Mid(mobile.Trim, 1, 3)
                    Case "+63"
                        If Len(mobile.Trim) >= 13 Then
                            StrMobile = Mid(mobile.Trim, 1, 13)
                            txtMobile.Text = StrMobile
                            txtMobile.ForeColor = Color.Black
                        Else
                            txtMobile.Text = ""
                            txtMobile.ForeColor = Color.Red
                        End If
                    Case Else
                        Select Case Mid(mobile.Trim, 1, 1)
                            Case "9"
                                If Len(mobile.Trim) >= 9 Then
                                    StrMobile = Mid(mobile.Trim, 1, 10)
                                    txtMobile.Text = "+63" & StrMobile
                                    txtMobile.ForeColor = Color.Black
                                Else
                                    txtMobile.Text = ""
                                    txtMobile.ForeColor = Color.Red
                                End If
                            Case Else
                                txtMobile.Text = ""
                                txtMobile.ForeColor = Color.Red
                        End Select
                End Select
        End Select
    End Function
    Function BPCMessage() As String
        Try
            Dim sql As String
            sql = "SELECT message FROM `sms_automation` WHERE id=1"

            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)
            Dim reader As MySqlDataReader
            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then
                While reader.Read
                    BPCMessage = reader.Item("message").ToString()
                End While

            Else
                BPCMessage = ""
            End If
            connection.Close()
        Catch ex As Exception
            Console.WriteLine(ex.Message & "::BPCMessage")
        End Try
    End Function

    Private Sub lstIssuedCard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstIssuedCard.Click
        Dim pxName As String
        Dim pxGender As String
        Dim pxMobile As String

        If lstIssuedCard.Items.Count > 0 Then

            pxGender = lstIssuedCard.SelectedItems(0).SubItems(1).Text.Trim()
            pxName = lstIssuedCard.SelectedItems(0).SubItems(2).Text.Trim()
            pxMobile = lstIssuedCard.SelectedItems(0).SubItems(3).Text.Trim()

            pxName = PX_MrMs(pxGender) & pxName

            If Len(pxName) > 1 Then
                pxName = pxName
            End If

            px_mobile(pxMobile)
            txtIssuedDate.Text = Replace(BPCMessage(), "<pxName>", pxName)

        End If

    End Sub

    Function messages_sms_out(ByVal sms_body As String, ByVal px_id As String, ByVal px_branchCode As String, ByVal sms_sender As String) As Integer
        Try
            Dim sql As String = ""

            sms_body = Replace(sms_body, "'", "\'")

            sql = "INSERT INTO `messages_sms` SET Direction=2, sender='" & sms_sender & "',body='" & sms_body & "',branch='" & px_branchCode & "', PatientID='" & px_id & "', Read_Stats='1', " _
            & " DeptKey='" & px_branchCode & "', Username='AutoSMS', UserHostName='" & ClientHostName & "', UserHostIP='" & ClientHostIP & "'"

            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(sql, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected

        Catch ex As Exception
            Console.WriteLine(ex.Message & "::messages_sms_out")
        End Try
    End Function

    Private Sub frmIssuedCard_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        dpFrom.Value = Now()

        btView.Enabled = False
        btSendToAll.Enabled = False

        btView_Click(Me, EventArgs.Empty)
        btSendToAll_Click(Me, EventArgs.Empty)

        btView.Enabled = True
        btSendToAll.Enabled = True

    End Sub
    Function SMS_UPDATE_Loyalty(ByVal query As String)

        Try
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Function

End Class