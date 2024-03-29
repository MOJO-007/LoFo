﻿Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Text
Imports System.IO
Imports System.Windows
Imports System.Data.SQLite
Imports System.Collections.ObjectModel
Imports System.Diagnostics.Tracing
Imports System.Text.RegularExpressions

Public Class PostItem
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;Journal Mode=WAL"
    Public connection As New SQLiteConnection(connectionString)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim img1 As OpenFileDialog = New OpenFileDialog()
        img1.Filter = "choose image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif"
        If img1.ShowDialog() = DialogResult.OK Then

            Label3.Text = img1.FileName
            PictureBox1.ImageLocation = Label3.Text
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim command As New SQLiteCommand("INSERT INTO found_items(item_type, item_description, location_found, date_found, contact_phone, photo_path, username, item_title) VALUES (@item_type, @item_description, @location_found, @date_found, @contact_phone, @photo_path, @username, @item_title)", connection)
        If (Label3.Text = "Image Location") Then
            MessageBox.Show("Please upload an image!")
        Else
            If ValidateMobileNumber(TextBox4.Text) Then
                command.Parameters.AddWithValue("@item_title", TextBox1.Text)
                command.Parameters.AddWithValue("@item_type", ComboBox1.Text)
                command.Parameters.AddWithValue("@item_description", TextBox2.Text)
                command.Parameters.AddWithValue("@location_found", TextBox3.Text)
                command.Parameters.AddWithValue("@date_found", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@contact_phone", TextBox4.Text)
                command.Parameters.AddWithValue("@photo_path", Label3.Text)
                command.Parameters.AddWithValue("@username", Login.Label3.Text)
                connection.Open()
                Dim rc As Integer = command.ExecuteNonQuery()
                If (rc > 0) Then
                    MessageBox.Show("Item is Uploaded, Thank you for your efforts!")
                    Me.Close()
                    UHome.Show()
                    connection.Close()
                Else
                    MessageBox.Show("Oops, Item is not Uploaded.")
                    Me.Refresh()
                End If
            Else
                MessageBox.Show("Entered Mobile number is incorrect.")
            End If
        End If

    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        UHome.Show()
    End Sub



    Public Function ValidateMobileNumber(ByVal mobileNumber As String) As Boolean
        ' Validate the mobile number using a regular expression pattern
        Dim mobileRegex As New Regex("^(\+[0-9]{1,3})?[0-9]{10}$")
        Dim mobileMatch As Match = mobileRegex.Match(mobileNumber)
        Dim isMobileValid As Boolean = mobileMatch.Success

        ' Return True if the mobile number is valid, False otherwise
        Return isMobileValid
    End Function

    Private Sub PostItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Label3.Show()
    End Sub
End Class