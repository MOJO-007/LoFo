﻿
Imports System.Data.SQLite
Imports System.Text.RegularExpressions

Public Class LogItem
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;Journal Mode=WAL"
    Public connection As New SQLiteConnection(connectionString)


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (IsValidEmail(TextBox2.Text) AndAlso Not String.IsNullOrWhiteSpace(TextBox1.Text)) Then
            Dim command As New SQLiteCommand("INSERT INTO logs(log_desc, log_mail) VALUES(@desc, @mail)", connection)
            command.Parameters.AddWithValue("@desc", TextBox1.Text)
            command.Parameters.AddWithValue("@mail", TextBox2.Text)
            connection.Open()
            Dim rc As Integer = command.ExecuteNonQuery()
            If (rc > 0) Then
                MessageBox.Show("Log is Uploaded, We Will Get Back to you!")
                Me.Close()
                UHome.Show()
                connection.Close()
            Else
                MessageBox.Show("Oops, Log is not Uploaded.")
                Me.Refresh()
            End If
        Else
            MessageBox.Show("Please fill all the fields")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        UHome.Show()
    End Sub

    Private Sub LogItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.PlaceholderText = "Enter details : " & Environment.NewLine & " Item Description" & Environment.NewLine & " How you lost it" & Environment.NewLine & " etc."
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Label3.Focus()
    End Sub

    Public Function IsValidEmail(ByVal email As String) As Boolean
        Dim emailRegex As New Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
        Dim emailMatch As Match = emailRegex.Match(email)
        Return emailMatch.Success
    End Function
End Class
