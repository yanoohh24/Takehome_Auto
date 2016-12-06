Option Explicit On
Imports MySql.Data.MySqlClient

Module MySQLconnectionSetting
    Public FFAutoSMS As New frmAutoSMSReply()
    Public FFLogin As New frmLogin()
    Public FFGSMSet As New frmGSMSetting()
    Public FFCatchError As New frmCatchError()
    Public FFIssuedCard As New frmIssuedCard()

    Public smS_no As String = ""
    Public ClientHostName As String
    Public ClientHostIP As String

    Public t As String = ""

    Public S1 As String = "00:00:00"
    Public S2 As String = "00:00:00"
    Public S3 As String = "00:00:00"
    Public S4 As String = "00:00:00"
    Public S5 As String = "00:00:00"
    Public S6 As String = "00:00:00"
    Public S7 As String = "00:00:00"
    Public S8 As String = "00:00:00"
    Public Stats As String = ""

    Public Host As String = "192.168.100.250"
    Public UserName As String = "admin"
    Public Password As String = "webdeveoper"

    Public DatabaseMFC As String = "mfc"

    Public connStrMFC As String = "Database=mfc;Data Source=" & Host & ";User Id=" & UserName & ";Password=" & Password & ";UseCompression=True;Connection Timeout=28800"
    Public connStrASC As String = "Database=asc;Data Source=" & Host & ";User Id=" & UserName & ";Password=" & Password & ";UseCompression=True;Connection Timeout=28800"

    Public connStrBMG As String = "Database=belo_database;Data Source=" & Host & ";User Id=" & UserName & ";Password=" & Password & ";UseCompression=True;Connection Timeout=28800"
    Public connStrSMS As String = "Database=Messages;Data Source=" & Host & ";User Id=" & UserName & ";Password=" & Password & ";UseCompression=True;Connection Timeout=28800"
    Public connStrAND As String = "Database=androidbelo;Data Source=" & Host & ";User Id=" & UserName & ";Password=" & Password & ";UseCompression=True;Connection Timeout=28800;Convert Zero Datetime=True"

    Public smsFormat As String = "To inquire, please reply <HELP> for more information."

    Function UpdateRecord(ByVal query As String) As Integer
        Try
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrMFC)
            Dim cmd As New MySqlCommand(query, connection)

            connection.Open()

            rowsEffected = cmd.ExecuteNonQuery()

            connection.Close()

            Return rowsEffected

        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try

    End Function
    Function InsertRecord(ByVal query As String) As Integer

        Try
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrMFC)
            Dim cmd As New MySqlCommand(query, connection)
            connection.Open()
            rowsEffected = cmd.ExecuteNonQuery()
            connection.Close()
            Return rowsEffected
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try

    End Function

    Function LastDownloadLogs(ByVal query As String) As Integer
        Try
            Dim rowsEffected As Integer = 0
            Dim connection As New MySqlConnection(connStrMFC)
            Dim cmd As New MySqlCommand(query, connection)
            connection.Open()
            rowsEffected = cmd.ExecuteNonQuery()
            connection.Close()
            Return rowsEffected
        Catch ex As Exception
            FFCatchError.txtCatchErrors.Text = FFCatchError.txtCatchErrors.Text & ex.Message & vbNewLine
        End Try
    End Function

    Function UpdateSMS_ID(ByVal query As String) As Integer
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

    Public Sub Delay(ByVal Milliseconds As Integer)
        Dim SW2 As New Stopwatch
        SW2.Start()
        Do
            'Do not use this section to process code
            'We need this to check the elapsing time, as it
            'elapses without interuption
        Loop Until SW2.ElapsedMilliseconds >= Milliseconds
    End Sub

End Module

