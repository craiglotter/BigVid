Imports System.Windows.Forms

Public Class FolderHistoryDialog

    Public fullname As String = ""
    Public choicetype As String = ""

    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            Me.TopMost = False
            If ex.Message.IndexOf("Thread was being aborted") < 0 Then
                Dim Display_Message1 As New Display_Message()
                Display_Message1.Message_Textbox.Text = "The Application encountered the following problem: " & vbCrLf & identifier_msg & ": " & ex.Message.ToString
                'Display_Message1.Message_Textbox.Text = "The Application encountered the following problem: " & vbCrLf & identifier_msg & ": " & ex.ToString
                Display_Message1.Timer1.Interval = 1000
                Display_Message1.ShowDialog()
                Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
                If dir.Exists = False Then
                    dir.Create()
                End If
                dir = Nothing
                Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
                filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ": " & ex.ToString)
                filewriter.WriteLine("")
                filewriter.Flush()
                filewriter.Close()
                filewriter = Nothing
            End If
            Me.TopMost = True
        Catch exc As Exception
            MsgBox("An error occurred in the application's error handling routine. The application will try to recover from this serious error." & vbCrLf & vbCrLf & exc.ToString, MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub Folder_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If My.Computer.FileSystem.DirectoryExists(sender.Tag) = True Then
                sender.ForeColor = Color.DarkRed
            Else
                sender.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "Folder/Drive Label Mouse Enter")
        End Try
    End Sub

    Private Sub Folder_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If My.Computer.FileSystem.DirectoryExists(sender.Tag) = True Then
                sender.ForeColor = Color.White
            Else
                sender.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "Folder/Drive Label Mouse Leave")
        End Try
    End Sub

    Private Sub File_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles File1.MouseEnter, File2.MouseEnter, File3.MouseEnter, File4.MouseEnter, File5.MouseEnter, File6.MouseEnter, File7.MouseEnter, File8.MouseEnter
        Try
            If My.Computer.FileSystem.FileExists(sender.Tag) = True Then
                sender.ForeColor = Color.DarkRed
            Else
                sender.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "File Label Mouse Enter")
        End Try
    End Sub

    Private Sub File_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles File1.MouseLeave, File2.MouseLeave, File3.MouseLeave, File4.MouseLeave, File5.MouseLeave, File6.MouseLeave, File7.MouseLeave, File8.MouseLeave
        Try
            If My.Computer.FileSystem.FileExists(sender.Tag) = True Then
                sender.ForeColor = Color.White
            Else
                sender.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "File Label Mouse Leave")
        End Try
    End Sub


    Private Sub File1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles File1.Click, File2.Click, File3.Click, File4.Click, File5.Click, File6.Click, File7.Click, File8.Click
        Try
            If My.Computer.FileSystem.FileExists(sender.tag) Then
                fullname = sender.tag
                choicetype = "File"
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            Error_Handler(ex, "File Click")
        End Try
    End Sub

    Private Sub Folder1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If My.Computer.FileSystem.DirectoryExists(sender.tag) Then
                fullname = sender.tag
                choicetype = "Folder"
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            Error_Handler(ex, "Folder Click")
        End Try
    End Sub
End Class
