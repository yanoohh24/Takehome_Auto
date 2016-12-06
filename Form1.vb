Imports System.IO

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'belo_module.SynchronizeDatabaseBeloMessages()

        'LogsCreate("beloe", "hdhdhdhhd")

        ' Open the file to read from.  
        'Using sr As StreamReader = File.OpenText(path)
        '    Do While sr.Peek() >= 0
        '        Console.WriteLine(sr.ReadLine())
        '    Loop
        'End Using
        Dim currentDate As New DateTime(2015, 7, 7)
        Dim startDate As New DateTime(1970, 1, 1)
        Dim noOfSeconds As Integer

        'noOfSeconds = (currentDate - startDate).TotalSeconds
        noOfSeconds = (currentDate - startDate).Hours

        Dim uTime As Integer
        Dim utext As String = "Sorry you may have entered an invalid keyword, Please reply HELP for more information. For other inquiries, you may call 819 - BELO (2356) or visit us at www.belomed.com" _
        & "Great news! Belo's newest app is now available on Google Play! Download the Belo App to book an appointment, find out the latest promos and updates, and learn more about our services. All that and more now within your fingertips! To unsubscribe, reply OFF"


        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        MessageBox.Show(UnixToTime(1435545290) & vbNewLine & TimeToUnix("06/29/2015 10:34:50 AM") & vbNewLine & utext.Length)
    End Sub

    Public Function UnixToTime(ByVal strUnixTime As String) As Date
        UnixToTime = DateAdd(DateInterval.Second, Val(strUnixTime), #1/1/1970#)
        If UnixToTime.IsDaylightSavingTime = True Then
            UnixToTime = DateAdd(DateInterval.Hour, 1, UnixToTime)
        End If
    End Function

    Public Function TimeToUnix(ByVal dteDate As Date) As String
        If dteDate.IsDaylightSavingTime = True Then
            dteDate = DateAdd(DateInterval.Hour, -1, dteDate)
        End If
        TimeToUnix = DateDiff(DateInterval.Second, #1/1/1970#, dteDate)
    End Function
    Function LogsCreate(ByVal dbname As String, ByVal context As String)
        Dim path_dir As String = "c:\temp\"
        If Not Directory.Exists(path_dir) Then
            Directory.CreateDirectory(path_dir)
        End If

        Dim path As String = path_dir + Format(Now(), "yyyy_MM_dd_HHmmss") + "-" + dbname + "_logs.txt"
        If Not File.Exists(path) Then
            ' Create a file to write to.  
            Using sw As StreamWriter = File.CreateText(path)
                sw.WriteLine(context)
                sw.WriteLine("And")
                sw.WriteLine("Welcome")
            End Using
        End If
        Return 1
    End Function
End Class