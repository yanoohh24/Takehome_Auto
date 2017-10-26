Imports MySql.Data.MySqlClient
Imports System.IO

Module belo_module


    Public branch_ip As String = ""
    Public branch_un As String
    Public branch_pass As String = ""
    Public branch_port As String = ""

    Public AboutBelo As String = "For 24 years, Belo Medical Group remains the number 1 medical aesthetic ambulatory clinic in the Philippines.It started in 1990, when founder, Dr. Vicki Belo opened her first 44 sqm clinic in Medical Towers, " _
    & "Makati. Belo Medical group has pioneered in so many beauty breakthroughs such as Liposuction, surgical and non-surgical beauty procedures, lasers and more. - See more at: http://www.belomed.com/about"
    Public AppointmentSchedule As String = ""
    Public AppointmentScheduleTime As String = ""

    Function PX_MrMs(ByVal px_gender As String) As String
        Select Case StrConv(px_gender.Trim, VbStrConv.Uppercase)
            Case "MALE"
                PX_MrMs = "Mr. "
            Case "FEMALE"
                PX_MrMs = "Ms. "
            Case Else
                PX_MrMs = ""
        End Select
    End Function
    Public SMS_brnchWord As String
    Dim strArr() As String
    Public Sub SMS_brnchWords(yesno)
        strArr = yesno.Trim.Split(" ")
        SMS_brnchWord = strArr(1)
    End Sub
    Function SMS_branchCode(ByVal sms As String) As String
        Select Case StrConv(sms.Trim, VbStrConv.Uppercase)
            Case "01 YES", "01 NO", "01 ACCEPT", "01 DENY"
                SMS_branchCode = "01"
                SMS_brnchWords(sms)
            Case "02 YES", "02 NO", "02 ACCEPT", "02 DENY"
                SMS_branchCode = "02"
                SMS_brnchWords(sms)
            Case "03 YES", "03 NO", "03 ACCEPT", "03 DENY"
                SMS_branchCode = "03"
                SMS_brnchWords(sms)
            Case "04 YES", "04 NO", "04 ACCEPT", "04 DENY"
                SMS_branchCode = "04"
                SMS_brnchWords(sms)
            Case "05 YES", "05 NO", "05 ACCEPT", "05 DENY"
                SMS_branchCode = "05"
                SMS_brnchWords(sms)
            Case "06 YES", "06 NO", "06 ACCEPT", "06 DENY"
                SMS_branchCode = "06"
                SMS_brnchWords(sms)
            Case "07 YES", "07 NO", "07 ACCEPT", "07 DENY"
                SMS_branchCode = "07"
                SMS_brnchWords(sms)
            Case "08 YES", "08 NO", "08 ACCEPT", "08 DENY"
                SMS_branchCode = "08"
                SMS_brnchWords(sms)
            Case "09 YES", "09 NO", "09 ACCEPT", "09 DENY"
                SMS_branchCode = "09"
                SMS_brnchWords(sms)
            Case "10 YES", "10 NO", "10 ACCEPT", "10 DENY"
                SMS_branchCode = "10"
                SMS_brnchWords(sms)
            Case "11 YES", "11 NO", "11 ACCEPT", "11 DENY"
                SMS_branchCode = "11"
                SMS_brnchWords(sms)
            Case "12 YES", "12 NO", "12 ACCEPT", "12 DENY"
                SMS_branchCode = "12"
                SMS_brnchWords(sms)
            Case "13 YES", "13 NO", "13 ACCEPT", "13 DENY"
                SMS_branchCode = "13"
                SMS_brnchWords(sms)
            Case "14 YES", "14 NO", "14 ACCEPT", "14 DENY"
                SMS_branchCode = "14"
                SMS_brnchWords(sms)
            Case "15 YES", "15 NO", "15 ACCEPT", "15 DENY"
                SMS_branchCode = "15"
                SMS_brnchWords(sms)
            Case "22 YES", "22 NO", "22 ACCEPT", "22 DENY"
                SMS_branchCode = "22"
                SMS_brnchWords(sms)
            Case Else
                SMS_branchCode = ""
        End Select
    End Function

    Dim yesaccept As String = ""
    Function searchAppointment(ByVal sender As String, ByVal YesNo As Boolean, ByVal PxIdNumber As String)

        yesaccept = "yes"

        Dim returnData As String = ""
        Dim pxId2 As String = ""
        Dim pxBranch2 As String = ""
        Dim pxSender2 As String
        Dim pxAppointment2 As String = ""
        Dim pxAppointmentDate2 As String = ""
        Dim pxAppointmentTime2 As String = ""
        Dim pxAppointmentRemarks2 As String
        Dim AppointmentDatabaseName As String = ""
        Dim appoinmentStatus As String = ""
        Dim confirmationStatus As String = ""
        Dim appoinmentBranch As String = ""
        Dim query As String = ""

        'yes
        Try


            'Dim querys As String = "SELECT PatientID,branch,Sender,appointment_id,DATE_FORMAT(appointment_date,'%Y-%m-%d') as appointment_date, appointment_time, remarks FROM `messages_sms` WHERE Sender LIKE '%" & sender & "%' AND direction=2 AND " _
            '& " DATE(doc)=DATE(NOW()) AND DATE(appointment_date)=DATE(NOW()) AND appointment_id>0 AND remarks LIKE '%today%' AND appointment_id=(SELECT MAX(appointment_id) FROM messages_sms WHERE PatientID=" & PxIdNumber & ") GROUP BY PatientID"

            ' query = "SELECT PatientID,branch,Sender,appointment_id,DATE_FORMAT(appointment_date,'%Y-%m-%d') as appointment_date, appointment_time, remarks FROM `messages_sms` WHERE Sender LIKE '%" & sender & "%' AND direction=2 AND " _
            '& " DATE(doc)=DATE(NOW()) AND DATE(appointment_date)=DATE(NOW()) AND appointment_id>0 AND remarks LIKE '%today%' GROUP BY PatientID"


            query = "SELECT PatientID,branch,Sender,appointment_id,DATE_FORMAT(appointment_date,'%Y-%m-%d') as appointment_date, appointment_time, remarks FROM `messages_sms` WHERE Sender LIKE '%" & sender & "%' AND direction=2 AND " _
       & " DATE(doc)=DATE(NOW()) AND DATE(appointment_date)=DATE(NOW()) AND appointment_id>0 AND remarks LIKE '%today%'"


            Dim pxId As String = ""
            Dim pxBranch As String = ""
            Dim pxSender As String
            Dim pxAppointment As String = ""
            Dim pxAppointmentDate As String = ""
            Dim pxAppointmentTime As String = ""
            Dim pxAppointmentRemarks As String

            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)
            Dim reader As MySqlDataReader

            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then
                While reader.Read
                    pxId = reader.Item("PatientID").ToString()
                    pxBranch = reader.Item("branch").ToString()
                    pxSender = reader.Item("Sender").ToString()
                    pxAppointment = reader.Item("appointment_id").ToString()
                    pxAppointmentDate = reader.Item("appointment_date").ToString()
                    pxAppointmentTime = reader.Item("appointment_time").ToString()
                    pxAppointmentRemarks = reader.Item("remarks").ToString()
                End While
                connection.Close()

                If YesNo = True Then
                    If Not pxBranch Is Nothing Then
                        AppointmentSchedule = pxAppointmentDate & " at " & pxAppointmentTime
                        AppointmentScheduleTime = pxAppointmentTime
                        returnData = appointmentBranch_database_confirm(pxBranch, pxAppointment, pxAppointmentDate, pxId, "yes")
                    End If
                Else
                    If Not pxBranch Is Nothing Then
                        AppointmentSchedule = pxAppointmentDate & " at " & pxAppointmentTime
                        AppointmentScheduleTime = pxAppointmentTime
                        returnData = appointmentBranch_database_cancelled(pxBranch, pxAppointment, pxAppointmentDate, pxId)
                    End If
                End If
                reader.Close()
            Else


                query = "SELECT PatientID,branch,Sender,appointment_id,DATE_FORMAT(appointment_date,'%Y-%m-%d') AS appointment_date, appointment_time, remarks,doc FROM `messages_sms` WHERE Sender LIKE '%" & sender & "%' AND direction=2 AND DATE(appointment_date) =CURDATE() + INTERVAL 1 DAY AND appointment_id>0 AND remarks LIKE '%tomorrow%'"

                Dim connection2 As New MySqlConnection(connStrBMG)
                Dim cmd2 As New MySqlCommand(query, connection2)
                Dim reader2 As MySqlDataReader

                connection2.Open()
                reader2 = cmd2.ExecuteReader()
                If reader2.HasRows = True Then
                    AppointmentSchedule = ""
                    returnData = "invalid_keyword"
                Else
                    AppointmentSchedule = ""
                    returnData = "x"
                End If
                connection.Close()
            End If
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try


confirmed:
        Return returnData

    End Function
    Public yestoaccept As Boolean = False
    Public pxappt_date As String = ""
    Public pxappt_count As Integer = 0
    Public pxappt_count_cancel As Integer = 0
    Public pxappt_count_cancel3 As Integer = 0

    Function searchAppointmentTomorrow(ByVal sender As String, ByVal YesNo As Boolean, ByVal PxIdNumber As String)
        Dim returnData As String = ""
        yesaccept = "accept"
        Try
            'Dim querys As String = "SELECT PatientID,branch,Sender,appointment_id,DATE_FORMAT(appointment_date,'%Y-%m-%d') as appointment_date, appointment_time, remarks FROM `messages_sms` WHERE Sender LIKE '%" & sender & "%' AND direction=2 AND " _
            '& " DATE(doc)=DATE(NOW()) AND appointment_id>0 AND remarks LIKE '%tomorrow%' AND appointment_id=(SELECT MAX(appointment_id) FROM messages_sms WHERE PatientID=" & PxIdNumber & ") GROUP BY PatientID"

            Dim query As String = "SELECT PatientID,branch,Sender,appointment_id,DATE_FORMAT(appointment_date,'%Y-%m-%d') as appointment_date, appointment_time, remarks FROM `messages_sms` WHERE Sender LIKE '%" & sender & "%' AND direction=2 AND " _
          & " DATE(doc)=DATE(NOW()) AND appointment_id>0 AND remarks LIKE '%tomorrow%'"

            Dim pxId As String = ""
            Dim pxBranch As String = ""
            Dim pxSender As String
            Dim pxAppointmentId As String = ""
            Dim pxAppointmentDate As String = ""
            Dim pxAppointmentTime As String = ""
            Dim pxAppointmentRemarks As String = ""

            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)
            Dim reader As MySqlDataReader

            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then
                While reader.Read
                    pxId = reader.Item("PatientID").ToString()
                    pxBranch = reader.Item("branch").ToString()
                    pxSender = reader.Item("Sender").ToString()
                    pxAppointmentId = reader.Item("appointment_id").ToString()
                    pxAppointmentDate = reader.Item("appointment_date").ToString()
                    pxappt_date = reader.Item("appointment_date").ToString()
                    pxAppointmentTime = reader.Item("appointment_time").ToString()
                    pxAppointmentRemarks = reader.Item("remarks").ToString()
                End While
                connection.Close()

                If YesNo = True Then
                    If Not pxBranch Is Nothing Then
                        AppointmentSchedule = pxAppointmentDate & " at " & pxAppointmentTime
                        AppointmentScheduleTime = pxAppointmentTime
                        returnData = appointmentBranch_database_confirm(pxBranch, pxAppointmentId, pxAppointmentDate, pxId, "accept")
                    End If
                Else
                    If Not pxBranch Is Nothing Then
                        AppointmentSchedule = pxAppointmentDate & " at " & pxAppointmentTime
                        AppointmentScheduleTime = pxAppointmentTime
                        returnData = appointmentBranch_database_cancelled(pxBranch, pxAppointmentId, pxAppointmentDate, pxId)
                    End If
                End If

            Else

                query = "SELECT PatientID,branch,Sender,appointment_id,DATE_FORMAT(appointment_date,'%Y-%m-%d') AS appointment_date, appointment_time, remarks,doc FROM `messages_sms` WHERE Sender LIKE '%" & sender & "%' AND direction=2 AND DATE(appointment_date) =CURDATE() AND appointment_id>0 AND remarks LIKE '%today%'"
                   
                Dim connection2 As New MySqlConnection(connStrBMG)
                Dim cmd2 As New MySqlCommand(query, connection2)
                Dim reader2 As MySqlDataReader

                connection2.Open()
                reader2 = cmd2.ExecuteReader()
                If reader2.HasRows = True Then
                    AppointmentSchedule = ""
                    returnData = "invalid_keyword"
                    connection.Close()
                Else
                    AppointmentSchedule = ""
                    returnData = "x"
                End If
             
                connection.Close()
            End If

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try

        Return returnData

    End Function


    Function appointmentBranch_database_confirm(ByVal branchCode As String, ByVal AppointmentId As String, ByVal AppointmentDate As String, ByVal pxId As String, ByVal msg As String) As String

        Dim AppointmentDatabaseName As String = ""
        Dim appoinmentStatus As String = ""
        Dim confirmationStatus As String = ""
        Dim appoinmentBranch As String = ""
        Dim query As String = ""
        Dim tomorrow As String = CDate(FormatDateTime(DateTime.Now.AddDays(+1), DateFormat.ShortDate)).ToString("yyyy-MM-dd")
        Select Case StrConv(branchCode.Trim, VbStrConv.Uppercase)
            Case "01"
                AppointmentDatabaseName = "appointment_alabang"
            Case "02"
                AppointmentDatabaseName = "appointment_cebu"
            Case "03"
                AppointmentDatabaseName = "appointment_greenbelt"
            Case "04"
                AppointmentDatabaseName = "appointment_greenhills"
            Case "05"
                AppointmentDatabaseName = "appointment_megamall"
            Case "06"
                AppointmentDatabaseName = "appointment_tomas_morato"
            Case "07"
                AppointmentDatabaseName = "appointment_medical_plaza"
            Case "08"
                AppointmentDatabaseName = "appointment_fort"
            Case "09"
                AppointmentDatabaseName = "appointment_trinoma"
            Case "10"
                AppointmentDatabaseName = "appointment_rockwell"
            Case "11"
                AppointmentDatabaseName = "appointment_davao"
            Case "12"
                AppointmentDatabaseName = "appointment_the_block"
            Case "13"
                AppointmentDatabaseName = "appointment_belo_concierge"
            Case "14"
                AppointmentDatabaseName = "appointment_medical_towers"
            Case "15"
                AppointmentDatabaseName = "appointment_shangrila"
            Case "22"
                AppointmentDatabaseName = "appointment_moa"
            Case Else
                AppointmentDatabaseName = "x"
        End Select

        Try


            If yesaccept = "yes" Then
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & AppointmentDate & "' AND `appointment_status` = 'Cancelled' "
            Else
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & tomorrow & "' AND `appointment_status` = 'Cancelled' "
            End If

            Dim connection3 As New MySqlConnection(connStrBMG)
            Dim cmd3 As New MySqlCommand(query, connection3)
            Dim reader3 As MySqlDataReader
            Dim sql3 As String = ""

            connection3.Open()
            reader3 = cmd3.ExecuteReader()

            If reader3.HasRows = True Then

                While reader3.Read
                    pxappt_count_cancel = reader3.Item(0).ToString()
                End While
                connection3.Close()
            Else
                connection3.Close()
            End If


            If yesaccept = "yes" Then
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & AppointmentDate & "' AND `appointment_status` <> 'Cancelled'"
            Else
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & tomorrow & "' AND `appointment_status` <> 'Cancelled'"
            End If

            Dim connection2 As New MySqlConnection(connStrBMG)
            Dim cmd2 As New MySqlCommand(query, connection2)
            Dim reader2 As MySqlDataReader
            Dim sql2 As String = ""

            connection2.Open()
            reader2 = cmd2.ExecuteReader()

            If reader2.HasRows = True Then

                While reader2.Read
                    pxappt_count = reader2.Item(0).ToString()
                End While
                connection2.Close()
            Else
                connection2.Close()
            End If

            If yesaccept = "yes" Then
                query = "SELECT appointment_status, confirmation_status,branch,date_updated,sts FROM " & AppointmentDatabaseName & " WHERE id=" & AppointmentId & " ORDER BY id DESC"
            Else
                query = "SELECT appointment_status, confirmation_status,branch,date_updated,sts FROM " & AppointmentDatabaseName & " WHERE id=" & AppointmentId & " and appointment_date = '" & tomorrow & "' ORDER BY id DESC"
            End If

            Dim today As String = CDate(FormatDateTime(DateTime.Now, DateFormat.ShortDate)).ToString("yyyy-MM-dd")
            Dim sts As String = ""
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)
            Dim reader As MySqlDataReader
            Dim sql As String = ""

            connection.Open()
            reader = cmd.ExecuteReader()
            Dim date_updated As Date
            If reader.HasRows = True Then

                While reader.Read
                    appoinmentStatus = reader.Item("appointment_status").ToString()
                    confirmationStatus = reader.Item("confirmation_status").ToString()
                    appoinmentBranch = reader.Item("branch").ToString()
                    Try
                        sts = reader.Item("sts").ToString()
                        date_updated = reader.Item("date_updated")
                    Catch ex As Exception

                    End Try

                End While

                connection.Close()

                Select Case StrConv(appoinmentStatus.Trim, VbStrConv.Uppercase)
                    Case "ARRIVED", "COMPLETED", "IN PROGRESS"
                        appoinmentStatus = "CONFIRMED_3"
                    Case "CANCELLED"
                        Dim onlinedigital_as As String
                        'If msg = "yes" Then
                        '    query = "SELECT appointment_status, confirmation_status,branch,date_updated,sts FROM " & AppointmentDatabaseName & " WHERE patientid= '" & pxId & "' and appointment_date = '" & today & "' and appointment_status <> 'Cancelled'  and appointment_status <> 'Completed'"

                        'ElseIf msg = "accept" Then
                        '    query = "SELECT appointment_status, confirmation_status,branch,date_updated,sts FROM " & AppointmentDatabaseName & " WHERE patientid= '" & pxId & "' and appointment_date = '" & tomorrow & "' and appointment_status <> 'Cancelled'  and appointment_status <> 'Completed'"
                        'End If

                        'Dim connection_check As New MySqlConnection(connStrBMG)
                        'Dim cmd_check As New MySqlCommand(query, connection_check)
                        'Dim reader_check As MySqlDataReader
                        'Dim sql_check As String = ""
                        'connection_check.Open()
                        'reader_check = cmd_check.ExecuteReader()

                        'If reader_check.HasRows = True Then
                        '    appoinmentStatus = "CHANGE_STATS"
                        '    GoTo change_stats
                        'Else
                        '    'no appointment
                        '    appoinmentStatus = "CANCELLED"
                        '    GoTo noappt
                        'End If
                        appoinmentStatus = "CANCELLED"
                        connection.Close()
noappt:
                    Case Else

                        Select Case StrConv(confirmationStatus.Trim, VbStrConv.Uppercase)
                            Case "CONFIRMED"

                                If date_updated = today Then
                                    appoinmentStatus = "CONFIRMED_2"

                                ElseIf sts = "Confirmed" Then
                                    appoinmentStatus = "CONFIRMED_1"
                                    GoTo updte
                                Else
                                    appoinmentStatus = "CONFIRMED_2"

                                End If

                            Case Else
updte:
                                sql = "UPDATE " & AppointmentDatabaseName & " SET confirmation_status='Confirmed', updated_by='Belo SMS', date_updated=DATE(NOW()), time_updated=DATE_FORMAT(NOW(),'%h:%i:%s %p') WHERE Patientid='" & pxId & "' AND DATE(appointment_date)='" & AppointmentDate & "' and appointment_status <> 'Cancelled'  and appointment_status <> 'Completed'"
                                BMG_UPDATE(sql)

                                checkBranch(branchCode, sql)
                                If yesaccept = "accept" Then
                                    sql = "UPDATE " & AppointmentDatabaseName & " SET sts = 'Confirmed' WHERE Patientid='" & pxId & "' AND DATE(appointment_date)='" & AppointmentDate & "' and appointment_status <> 'Cancelled'  and appointment_status <> 'Completed'"
                                    'sql = "UPDATE messages_sms SET sts = 'Confirmed' WHERE appointment_id = '" & AppointmentId & "' and remarks = 'tomorrow'"
                                    BMG_UPDATE(sql)
                                    checkBranch(branchCode, sql)
                                End If

                                sql = "INSERT INTO `appointment_logs` SET appointment_id='" & AppointmentId & "', branch='" & appoinmentBranch & "', original_status='" & confirmationStatus.Trim & "', " _
                                & " new_status='Confirmed', updated_by='Belo SMS', time_created=DATE_FORMAT(NOW(),'%l:%i:%s %p'), date_created=DATE(NOW())"
                                BMG_UPDATE(sql)

                                appoinmentStatus = "CONFIRMED_1"
                        End Select
                End Select
            Else
                appoinmentStatus = "x"
                connection.Close()
            End If

        Catch ex As Exception

            'MsgBox("Function appointmentBranch_database:::belo_module.vb" & vbNewLine & ex.Message.ToString, MsgBoxStyle.Critical, "Belo Module")
            appoinmentStatus = "x"

        End Try
change_stats:
        Return appoinmentStatus

    End Function

    Public Sub checkBranch(branchcode As String, sql As String)
        Dim TextLine As String
        Dim objReader As New System.IO.StreamReader(Application.StartupPath & "\branches.dll")

        Do While objReader.Peek() <> -1
            TextLine = objReader.ReadLine().Trim & vbNewLine

            If branchcode = TextLine.Trim Then
                BMG_UPDATE_BRANCH(Sql)
            End If
        Loop

    End Sub
    Function appointmentBranch_database_cancelled(ByVal branchCode As String, ByVal AppointmentId As String, ByVal AppointmentDate As String, ByVal pxId As String) As String
        Dim tomorrow As String = CDate(FormatDateTime(DateTime.Now.AddDays(+1), DateFormat.ShortDate)).ToString("yyyy-MM-dd")
        Dim AppointmentDatabaseName As String = ""
        Dim appoinmentStatus As String = ""
        Dim appoinmentBranch As String = ""
        Dim query As String = ""

        Select Case StrConv(branchCode.Trim, VbStrConv.Uppercase)
            Case "01"
                AppointmentDatabaseName = "appointment_alabang"
            Case "02"
                AppointmentDatabaseName = "appointment_cebu"
            Case "03"
                AppointmentDatabaseName = "appointment_greenbelt"
            Case "04"
                AppointmentDatabaseName = "appointment_greenhills"
            Case "05"
                AppointmentDatabaseName = "appointment_megamall"
            Case "06"
                AppointmentDatabaseName = "appointment_tomas_morato"
            Case "07"
                AppointmentDatabaseName = "appointment_medical_plaza"
            Case "08"
                AppointmentDatabaseName = "appointment_fort"
            Case "09"
                AppointmentDatabaseName = "appointment_trinoma"
            Case "10"
                AppointmentDatabaseName = "appointment_rockwell"
            Case "11"
                AppointmentDatabaseName = "appointment_davao"
            Case "12"
                AppointmentDatabaseName = "appointment_the_block"
            Case "13"
                AppointmentDatabaseName = "appointment_belo_concierge"
            Case "14"
                AppointmentDatabaseName = "appointment_medical_towers"
            Case "15"
                AppointmentDatabaseName = "appointment_shangrila"
            Case "22"
                AppointmentDatabaseName = "appointment_moa"
            Case Else
                AppointmentDatabaseName = "x"
        End Select

        Try


            If yesaccept = "yes" Then
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & AppointmentDate & "' AND `appointment_status` = 'Cancelled' "
            Else
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & tomorrow & "' AND `appointment_status` = 'Cancelled' "
            End If
            Dim connection4 As New MySqlConnection(connStrBMG)
            Dim cmd4 As New MySqlCommand(query, connection4)
            Dim reader4 As MySqlDataReader
            Dim sql4 As String = ""

            connection4.Open()
            reader4 = cmd4.ExecuteReader()

            If reader4.HasRows = True Then
                While reader4.Read
                    pxappt_count_cancel = reader4.Item(0).ToString()
                End While
                connection4.Close()
            Else
                connection4.Close()
            End If


            'query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid=" & pxId & " and appointment_date = '" & AppointmentDate & "' AND `appointment_status` = 'ARRIVED' or  Patientid=" & pxId & " and appointment_date = '" & AppointmentDate & "' AND `appointment_status` = 'COMPLETED' or  Patientid=" & pxId & " and appointment_date = '" & AppointmentDate & "' AND `appointment_status` = 'IN PROGRESS'"
            'Dim connection3 As New MySqlConnection(connStrBMG)
            'Dim cmd3 As New MySqlCommand(query, connection3)
            'Dim reader3 As MySqlDataReader
            'Dim sql3 As String = ""

            'connection3.Open()
            'reader3 = cmd3.ExecuteReader()

            'If reader3.HasRows = True Then
            '    While reader3.Read
            '        pxappt_count_cancel3 = reader3.Item(0).ToString()
            '    End While
            '    connection3.Close()
            'End If

            If yesaccept = "yes" Then
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & AppointmentDate & "' AND `appointment_status` <> 'Cancelled' "
            Else
                query = "SELECT count(*) FROM " & AppointmentDatabaseName & " WHERE Patientid='" & pxId & "' and appointment_date = '" & tomorrow & "' AND `appointment_status` <> 'Cancelled' "
            End If
            Dim connection2 As New MySqlConnection(connStrBMG)
            Dim cmd2 As New MySqlCommand(query, connection2)
            Dim reader2 As MySqlDataReader
            Dim sql2 As String = ""

            connection2.Open()
            reader2 = cmd2.ExecuteReader()

            If reader2.HasRows = True Then
                While reader2.Read
                    pxappt_count = reader2.Item(0).ToString()
                End While
                connection2.Close()
            Else
                connection2.Close()
            End If


            If yesaccept = "yes" Then
                query = "SELECT appointment_status,branch FROM " & AppointmentDatabaseName & " WHERE id=" & AppointmentId & ""
            Else
                query = "SELECT appointment_status,branch FROM " & AppointmentDatabaseName & " WHERE id=" & AppointmentId & " and appointment_date = '" & tomorrow & "'"
            End If
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)
            Dim reader As MySqlDataReader
            Dim sql As String = ""

            connection.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then

                While reader.Read
                    appoinmentStatus = reader.Item("appointment_status").ToString()
                    appoinmentBranch = reader.Item("branch").ToString()
                End While
                connection.Close()

                Select Case StrConv(appoinmentStatus.Trim, VbStrConv.Uppercase)
                    Case "ARRIVED", "COMPLETED", "IN PROGRESS"
                        appoinmentStatus = "CANCELLED_3"
                    Case "CANCELLED"
                        appoinmentStatus = "CANCELLED_2"
                    Case Else

                        If yesaccept = "accept" Then
                            sql = "UPDATE messages_sms SET sts = 'Cancelled' WHERE appointment_id = '" & AppointmentId & "' and remarks = 'tomorrow'"
                            BMG_UPDATE(sql)
                            checkBranch(branchCode, sql)
                        End If


                        sql = "UPDATE " & AppointmentDatabaseName & " SET appointment_status='Cancelled', updated_by='Belo SMS', date_updated=DATE(NOW()), time_updated=DATE_FORMAT(NOW(),'%h:%i:%s %p') WHERE Patientid='" & pxId & "' AND DATE(appointment_date)='" & AppointmentDate & "' and appointment_status <> 'Cancelled' "
                        BMG_UPDATE(sql)
                     
                        checkBranch(branchCode, sql)

                        sql = "INSERT INTO `appointment_logs` SET appointment_id='" & AppointmentId & "', branch='" & appoinmentBranch & "', original_status='" & appoinmentStatus & "', " _
                        & " new_status='Cancelled', updated_by='Belo SMS', time_created=DATE_FORMAT(NOW(),'%l:%i:%s %p'), date_created=DATE(NOW())"
                        BMG_UPDATE(sql)

                        appoinmentStatus = "CANCELLED_1"

                End Select
            Else
                appoinmentStatus = "x"
                connection.Close()
            End If

        Catch ex As Exception

            'MsgBox("Function appointmentBranch_database:::belo_module.vb" & vbNewLine & ex.Message.ToString, MsgBoxStyle.Critical, "Belo Module")
            appoinmentStatus = "x"

        End Try

        Return appoinmentStatus

    End Function
    Function SMS_UPDATE(ByVal query As String)

        Try
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrSMS)
            Dim cmd As New MySqlCommand(query, connection)

            connection.Open()
            rowsEffected = cmd.ExecuteNonQuery()
            connection.Close()

            Return rowsEffected
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try

    End Function
    Function BMG_UPDATE(ByVal query As String)

        Try
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrBMG)
            Dim cmd As New MySqlCommand(query, connection)

            connection.Open()
            rowsEffected = cmd.ExecuteNonQuery()
            connection.Close()

            Return rowsEffected
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try

    End Function



    Function BMG_UPDATE_BRANCH(ByVal query As String)

        Try
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection("Database=belo_database;Data Source= " & branch_ip & ";User Id=" & branch_un & " ;Password= " & branch_pass & " ;Port=" & branch_port & ";UseCompression=True;Connection Timeout=28800;Convert Zero Datetime=True")
            Dim cmd As New MySqlCommand(query, connection)

            connection.Open()
            rowsEffected = cmd.ExecuteNonQuery()
            connection.Close()

            Return rowsEffected
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try

    End Function

    Public Sub SynchronizeDatabaseMessages()
        Dim Qone As String = "INSERT INTO messages_archive (ID, Direction, TYPE, StatusDetails,STATUS,ChannelID,MessageReference,SentTimeSecs,ReceivedTimeSecs," _
        & " ScheduledTimeSecs,LastUpdateSecs,Sender,Recipient,SUBJECT,BodyFormat,CustomField1,CustomField2,sysCreator,sysArchive,sysLock,sysHash,sysForwarded, " _
        & " sysGwReference,Header,Body,Trace,Stats,validity,branch,PatientID,Username,UserHostName,UserHostIP,doc,appointment_id,appointment_branch,appointment_date,remarks) " _
        & " SELECT ID, Direction, TYPE, StatusDetails,STATUS,ChannelID,MessageReference,SentTimeSecs,ReceivedTimeSecs," _
        & " ScheduledTimeSecs,LastUpdateSecs,Sender,Recipient,SUBJECT,BodyFormat,CustomField1,CustomField2,sysCreator,sysArchive,sysLock,sysHash,sysForwarded, " _
        & " sysGwReference,Header,Body,Trace,Stats,validity,branch,PatientID,Username,UserHostName,UserHostIP,doc,appointment_id,appointment_branch,appointment_date,remarks FROM `messages` WHERE DATE(doc)<DATE(NOW()) AND id NOT IN (SELECT id FROM `messages_archive` WHERE DATE(doc)<DATE(NOW()))"

        On Error Resume Next

        Dim rowsEffected As Integer = 0
        Dim connection As New MySqlConnection(connStrSMS)
        Dim cmd As New MySqlCommand(Qone, connection)
        connection.Open()
        rowsEffected = cmd.ExecuteNonQuery()
        connection.Close()

        If rowsEffected > 0 Then
            SMS_UPDATE("DELETE FROM `messages` WHERE DATE(doc) <= (SELECT DATE_SUB(MAX(DATE(doc)), INTERVAL 20 DAY) FROM `messages_archive`)")
        End If

        LogsCreate("messages", rowsEffected)
    End Sub

    Public Sub SynchronizeDatabaseBeloMessages()
        Dim Qone As String = "INSERT INTO messages_sms_archive (id, Direction, invalid, Sender,Recipient,Body,branch,PatientID,PatientName,Username, " _
        & " UserHostName,UserHostIP,Read_Stats,DeptKey,user_access_group,doc,appointment_id,appointment_branch,appointment_date,remarks) SELECT * FROM `messages_sms` WHERE DATE(doc)<DATE(NOW()) " _
        & " AND id NOT IN (SELECT id FROM `messages_sms_archive` WHERE  DATE(doc)<DATE(NOW()))"

        On Error Resume Next

        Dim rowsEffected As Integer = 0
        Dim connection As New MySqlConnection(connStrBMG)
        Dim cmd As New MySqlCommand(Qone, connection)
        connection.Open()
        rowsEffected = cmd.ExecuteNonQuery()
        connection.Close()

        If rowsEffected > 0 Then
            BMG_UPDATE("DELETE FROM `messages_sms` WHERE DATE(doc) <= (SELECT DATE_SUB(MAX(DATE(doc)), INTERVAL 20 DAY) FROM `messages_sms_archive`)")
        End If
        LogsCreate("belo", rowsEffected)
    End Sub

    Function LogsCreate(ByVal dbname As String, ByVal context As String)
        Dim path_dir As String = "c:\temp\"
        If Not Directory.Exists(path_dir) Then
            Directory.CreateDirectory(path_dir)
        End If

        Dim path_file As String = path_dir + Format(Now(), "yyyy_MM_dd_HHmmss") + "-" + dbname + "_logs.txt"
        If Not File.Exists(path_file) Then
            ' Create a file to write to.  
            Using sw As StreamWriter = File.CreateText(path_file)
                sw.WriteLine(context)
                'sw.WriteLine("And")
                'sw.WriteLine("Welcome")
            End Using
        End If
    End Function
End Module
