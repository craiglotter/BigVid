Imports System.IO
Imports System.Net

Public Class Main_Screen

    Private busyworking As Boolean = False
    Private AutoUpdate As Boolean = False

    'Private precountArtists As Long = 0
    'Private precountAlbums As Long = 0
    'Private countArtists As Long = 0
    'Private countAlbums As Long = 0
    'Private countAlbumsSorted As Long = 0
    'Private countTracks As Long = 0
    'Private countAlbumArt As Long = 0
    'Private countSize As Long = 0
    'Private countMP3 As Long = 0
    'Private countWMA As Long = 0

    Private CurrentFolderLastPlayed As String = ""
    Private VideoFolder As String = ""

    Private FolderFileList As ArrayList

    Private HistoryDrives As ArrayList = New ArrayList(3)
    Private HistoryFolders As ArrayList = New ArrayList(5)
    Private HistoryFiles As ArrayList = New ArrayList(8)
    Private FolderHistoryFiles As ArrayList = New ArrayList(10)


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

    Private Sub Activity_Handler(ByVal message As String)
        Try
            Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs")
            If dir.Exists = False Then
                dir.Create()
            End If
            dir = Nothing
            Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & Format(Now(), "yyyyMMdd") & "_Activity_Log.txt", True)
            filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & message)
            filewriter.WriteLine("")
            filewriter.Flush()
            filewriter.Close()
            filewriter = Nothing

        Catch ex As Exception
            Error_Handler(ex, "Activity Handler")
        End Try
    End Sub

    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Control.CheckForIllegalCrossThreadCalls = False
            Me.Text = My.Application.Info.ProductName & " (" & Format(My.Application.Info.Version.Major, "0000") & Format(My.Application.Info.Version.Minor, "00") & Format(My.Application.Info.Version.Build, "00") & "." & Format(My.Application.Info.Version.Revision, "00") & ")"
            loadSettings()
            FolderFileList = New ArrayList
            LoadFolder()
        Catch ex As Exception
            Error_Handler(ex, "Application Loading")
        End Try
    End Sub

    Private Sub Main_Screen_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            SaveSettings()
            If AutoUpdate = True Then
                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\AutoUpdate.exe").Replace("\\", "\")) = True Then
                    Dim startinfo As ProcessStartInfo = New ProcessStartInfo
                    startinfo.FileName = (Application.StartupPath & "\AutoUpdate.exe").Replace("\\", "\")
                    startinfo.Arguments = "force"
                    startinfo.CreateNoWindow = False
                    Process.Start(startinfo)
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Closing Application")
        End Try
    End Sub

    Private Sub loadSettings()
        Try
            Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")
            If My.Computer.FileSystem.FileExists(configfile) Then
                Dim reader As StreamReader = New StreamReader(configfile)
                Dim lineread As String
                Dim variablevalue As String
                While reader.Peek <> -1
                    lineread = reader.ReadLine
                    If lineread.IndexOf("=") <> -1 Then
                        variablevalue = lineread.Remove(0, lineread.IndexOf("=") + 1)
                        If lineread.StartsWith("VideoFolder=") Then
                            If My.Computer.FileSystem.DirectoryExists(variablevalue) = True Then
                                VideoFolder = variablevalue
                            End If
                        End If
                        If lineread.StartsWith("LastPlayed=") Then
                            If variablevalue.Length > 0 Then
                                LastPlayed.Tag = variablevalue
                                Dim finfo As FileInfo = New FileInfo(variablevalue)
                                LastPlayed.Text = "Last Video Played: " & finfo.Name
                                finfo = Nothing
                                If My.Computer.FileSystem.FileExists(variablevalue) = True Then
                                    LastPlayed.ForeColor = Color.White
                                Else
                                    LastPlayed.ForeColor = Color.Silver
                                End If
                            End If
                        End If
                        If lineread.StartsWith("HistoryDrives=") Then
                            HistoryDrives.Add(variablevalue)
                        End If
                        If lineread.StartsWith("HistoryFolders=") Then
                            HistoryFolders.Add(variablevalue)
                        End If
                        If lineread.StartsWith("HistoryFiles=") Then
                            HistoryFiles.Add(variablevalue)
                        End If
                    End If
                End While
                reader.Close()
                reader = Nothing

            End If
        Catch ex As Exception
            Error_Handler(ex, "Load Settings")
        End Try
    End Sub

    Private Sub SaveSettings()
        Try
            Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")
            Dim writer As StreamWriter = New StreamWriter(configfile, False)
            writer.WriteLine("VideoFolder=" & VideoFolder)
            writer.WriteLine("LastPlayed=" & LastPlayed.Tag)
            For Each stringitem As String In HistoryDrives
                writer.WriteLine("HistoryDrives=" & stringitem)
            Next
            For Each stringitem As String In HistoryFolders
                writer.WriteLine("HistoryFolders=" & stringitem)
            Next
            For Each stringitem As String In HistoryFiles
                writer.WriteLine("HistoryFiles=" & stringitem)
            Next
            writer.Flush()
            writer.Close()
            writer = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Save Settings")
        End Try
    End Sub

    Private Sub LoadFolder()
        Try
            FolderFileList.Clear()
            If My.Computer.FileSystem.DirectoryExists(VideoFolder) = False Then
                FolderBrowserDialog1.Description = "Please Select Video Base Folder"
                FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop
                FolderBrowserDialog1.SelectedPath = Environment.SpecialFolder.Desktop
                FolderBrowserDialog1.ShowNewFolderButton = False
                Me.TopMost = False
                If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    VideoFolder = FolderBrowserDialog1.SelectedPath
                End If
                Me.TopMost = True
            End If
            If My.Computer.FileSystem.DirectoryExists(VideoFolder) = True Then
                Dim dinfo As DirectoryInfo = New DirectoryInfo(VideoFolder)
                CurrentFolderName.Text = dinfo.Name
                CurrentFolderName.Tag = dinfo.FullName
                CurrentFolderName.ForeColor = Color.DarkRed
                CurrentFolderName.Refresh()
                Dim currenttop As Integer = 0
                Panel3.Controls.Clear()
                For Each subdir As DirectoryInfo In dinfo.GetDirectories
                    Dim folderlabel As Label = New Label
                    folderlabel.Font = Label1.Font
                    folderlabel.Text = subdir.Name
                    folderlabel.Tag = subdir.FullName
                    folderlabel.AutoEllipsis = True
                    folderlabel.AutoSize = False
                    folderlabel.Width = 174
                    folderlabel.Height = 24
                    folderlabel.Left = 8
                    folderlabel.Top = currenttop
                    currenttop = currenttop + 32
                    AddHandler folderlabel.MouseEnter, AddressOf DirectoryHover
                    AddHandler folderlabel.MouseLeave, AddressOf DirectoryLeave
                    AddHandler folderlabel.Click, AddressOf DirectoryClick
                    Panel3.Controls.Add(folderlabel)
                    subdir = Nothing
                Next
                Panel3.Refresh()
                If Not dinfo.Parent Is Nothing Then
                    PictureBox1.Image = ImageList1.Images.Item(0)
                    DeleteFolderLabel.Visible = True
                Else
                    If Not PictureBox1.Image Is Nothing Then
                        PictureBox1.Image.Dispose()
                        PictureBox1.Image = Nothing
                    End If
                    DeleteFolderLabel.Visible = False
                End If
                currenttop = 0
                Panel4.Controls.Clear()
                Dim acceptabletypes As ArrayList = New ArrayList()
                acceptabletypes.Add(".avi")
                acceptabletypes.Add(".rm")
                acceptabletypes.Add(".rmvb")
                acceptabletypes.Add(".mpg")
                acceptabletypes.Add(".mpeg")
                acceptabletypes.Add(".wmv")
                acceptabletypes.Add(".mkv")
                acceptabletypes.Add(".ogm")
                acceptabletypes.Add(".asf")
                acceptabletypes.Add(".mp4")
                Dim generatethumbs As Boolean = True
                Dim askedalready As Boolean = False
                Dim VideoCountInt As Integer = 0
                Dim VideoCountName = "0000"
                VideoCount.Text = VideoCountInt & " Videos"
                Dim videoprogresscount As Integer = 0
                For Each finfo As FileInfo In dinfo.GetFiles
                    If (acceptabletypes.IndexOf(finfo.Extension.ToLower)) <> -1 Then
                        videoprogresscount = videoprogresscount + 1
                    End If
                    finfo = Nothing
                Next
                For Each finfo As FileInfo In dinfo.GetFiles
                    Try
                        If (acceptabletypes.IndexOf(finfo.Extension.ToLower)) <> -1 Then

                            Dim thumbnailfolder As String = ""

                            If Not dinfo.Parent Is Nothing Then
                                thumbnailfolder = (Application.StartupPath & "\Thumbnails\" & (dinfo.Parent.Name & "_" & dinfo.Name).Replace(":\", "")).Replace("\\", "\")
                            Else
                                thumbnailfolder = (Application.StartupPath & "\Thumbnails\" & ("Root" & dinfo.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                            End If
                            If My.Computer.FileSystem.DirectoryExists(thumbnailfolder) = False Then
                                My.Computer.FileSystem.CreateDirectory(thumbnailfolder)
                            End If

                            'Panel4.Refresh()
                            VideoCountInt = VideoCountInt + 1

                            If videoprogresscount <> 1 Then
                                VideoCount.Text = "Processing " & VideoCountInt & " of " & videoprogresscount & " Videos"
                            Else
                                VideoCount.Text = "Processing " & VideoCountInt & " of " & videoprogresscount & " Video"
                            End If
                            VideoCount.Refresh()
                            VideoCountName = VideoCountInt.ToString
                            While VideoCountName.Length < 4
                                VideoCountName = "0" & VideoCountName
                            End While
                            Dim deletelabel As Label = New Label
                            deletelabel.Name = VideoCountName & "_DeleteLabel"
                            deletelabel.Font = Label4.Font
                            deletelabel.ForeColor = Color.Black
                            deletelabel.Text = Label4.Text
                            deletelabel.Tag = finfo.FullName
                            deletelabel.AutoEllipsis = Label4.AutoEllipsis
                            deletelabel.AutoSize = Label4.AutoSize
                            deletelabel.Width = Label4.Width
                            deletelabel.Height = Label4.Height
                            deletelabel.Left = 460
                            deletelabel.Top = currenttop + 225
                            AddHandler deletelabel.MouseEnter, AddressOf DeletelabelHover
                            AddHandler deletelabel.MouseLeave, AddressOf DeletelabelLeave
                            AddHandler deletelabel.Click, AddressOf DeleteClick
                            Panel4.Controls.Add(deletelabel)
                            Dim durationlabel As Label = New Label
                            durationlabel.Name = VideoCountName & "_DurationLabel"
                            durationlabel.Font = Label5.Font
                            durationlabel.ForeColor = Label5.ForeColor
                            durationlabel.Text = "00:00:00"
                            durationlabel.AutoEllipsis = Label5.AutoEllipsis
                            durationlabel.AutoSize = Label5.AutoSize
                            durationlabel.TextAlign = Label5.TextAlign
                            durationlabel.Width = Label5.Width
                            durationlabel.Height = Label5.Height
                            durationlabel.Left = Label5.Left
                            durationlabel.Top = currenttop + Label5.Top

                            Dim durationfile As String
                            Dim readduration As String
                            If Not finfo.Directory.Parent Is Nothing Then
                                durationfile = (Application.StartupPath & "\Thumbnails\" & (finfo.Directory.Parent.Name & "_" & finfo.Directory.Name).Replace(":\", "")).Replace("\\", "\")
                            Else
                                durationfile = (Application.StartupPath & "\Thumbnails\" & ("Root" & finfo.Directory.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                            End If
                            durationfile = (durationfile & "\" & finfo.Name & ".duration.txt").Replace("\\", "\")
                            FolderFileList.Add(durationfile)
                            Dim durationinseconds As Double
                            If My.Computer.FileSystem.FileExists(durationfile) = True Then
                                readduration = My.Computer.FileSystem.ReadAllText(durationfile)
                                durationlabel.Text = readduration
                                durationinseconds = (CInt(readduration.Substring(0, 2)) * 60 * 60) + (CInt(readduration.Substring(3, 2)) * 60) + (CInt(readduration.Substring(6, 2)))
                            Else
                                '--------------------------------------
                                Try


                                    'Get Video Duration
                                    If My.Computer.FileSystem.FileExists((Application.StartupPath & "\results.txt").Replace("\\", "\")) = True Then
                                        My.Computer.FileSystem.DeleteFile((Application.StartupPath & "\results.txt").Replace("\\", "\"), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                    End If
                                    DosShellCommand("""" & (Application.StartupPath & "\MediaInfo.exe").Replace("\\", "\") & """ """ & finfo.FullName & """ > """ & (Application.StartupPath & "\results.txt").Replace("\\", "\") & """")

                                    If My.Computer.FileSystem.FileExists((Application.StartupPath & "\results.txt").Replace("\\", "\")) = True Then
                                        Dim reader As StreamReader = New StreamReader((Application.StartupPath & "\results.txt").Replace("\\", "\"))
                                        Dim linetoread As String
                                        While reader.Peek <> -1
                                            linetoread = reader.ReadLine
                                            If linetoread.StartsWith("PlayTime") Then
                                                linetoread = linetoread.Remove(0, linetoread.LastIndexOf(":") + 1)
                                                Dim hr, mn, ss As String
                                                hr = "00"
                                                mn = "00"
                                                ss = "00"
                                                Dim rs As String() = linetoread.Split(" ")
                                                For Each sr As String In rs
                                                    If sr.EndsWith("mn") Then
                                                        mn = sr.Replace("mn", "")
                                                        While mn.Length < 2
                                                            mn = "0" & mn
                                                        End While
                                                    End If
                                                    If sr.EndsWith("s") Then
                                                        ss = sr.Replace("s", "")
                                                        While ss.Length < 2
                                                            ss = "0" & ss
                                                        End While
                                                    End If
                                                    If sr.EndsWith("h") Then
                                                        hr = sr.Replace("h", "")
                                                        While hr.Length < 2
                                                            hr = "0" & hr
                                                        End While
                                                    End If
                                                Next
                                                durationlabel.Text = hr & ":" & mn & ":" & ss
                                                durationinseconds = (CInt(hr) * 60 * 60) + (CInt(mn) * 60) + (CInt(ss))
                                                Exit While
                                            End If
                                        End While
                                        reader.DiscardBufferedData()
                                        reader.Close()
                                        reader = Nothing
                                        My.Computer.FileSystem.DeleteFile((Application.StartupPath & "\results.txt").Replace("\\", "\"), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                    End If
                                    My.Computer.FileSystem.WriteAllText(durationfile, durationlabel.Text, False)
                                Catch exb As Exception
                                    Error_Handler(exb, "Get Duration for: " & finfo.Name)
                                End Try
                                '--------------------------------------
                            End If
                            Panel4.Controls.Add(durationlabel)
                            Dim starttrackbar As TrackBar = New TrackBar
                            starttrackbar.Name = VideoCountName & "_StartTrackBar"
                            starttrackbar.AutoSize = False
                            starttrackbar.Enabled = True
                            starttrackbar.Visible = True
                            starttrackbar.Height = TrackBar1.Height
                            starttrackbar.Width = TrackBar1.Width
                            starttrackbar.TickStyle = TickStyle.None
                            starttrackbar.Left = 12
                            starttrackbar.Top = currenttop + 261
                            starttrackbar.Maximum = durationinseconds
                            starttrackbar.Tag = finfo.FullName

                            Dim valuefile As String
                            Dim readvalue As String
                            If Not finfo.Directory.Parent Is Nothing Then
                                valuefile = (Application.StartupPath & "\Thumbnails\" & (finfo.Directory.Parent.Name & "_" & finfo.Directory.Name).Replace(":\", "")).Replace("\\", "\")
                            Else
                                valuefile = (Application.StartupPath & "\Thumbnails\" & ("Root" & finfo.Directory.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                            End If
                            valuefile = (valuefile & "\" & finfo.Name & ".txt").Replace("\\", "\")
                            FolderFileList.Add(valuefile)
                            If My.Computer.FileSystem.FileExists(valuefile) = True Then
                                readvalue = My.Computer.FileSystem.ReadAllText(valuefile)
                                If IsNumeric(readvalue) Then
                                    If Long.Parse(readvalue) < starttrackbar.Maximum Then
                                        starttrackbar.Value = Long.Parse(readvalue)
                                    Else
                                        starttrackbar.Value = 0
                                    End If
                                Else
                                    starttrackbar.Value = 0
                                End If
                            Else
                                starttrackbar.Value = 0
                            End If
                            AddHandler starttrackbar.ValueChanged, AddressOf TrackBar_ValueChanged
                            Panel4.Controls.Add(starttrackbar)
                            Dim playlabel As Label = New Label
                            playlabel.Name = VideoCountName & "_PlayLabel"
                            playlabel.Font = Label2.Font
                            playlabel.ForeColor = Color.Silver
                            playlabel.Text = Label2.Text
                            playlabel.Tag = VideoCountName & finfo.FullName
                            playlabel.AutoEllipsis = Label2.AutoEllipsis
                            playlabel.AutoSize = Label2.AutoSize
                            playlabel.Width = Label2.Width
                            playlabel.Height = Label2.Height
                            playlabel.Left = 351
                            playlabel.Top = currenttop + 261
                            AddHandler playlabel.MouseEnter, AddressOf PlaylabelHover
                            AddHandler playlabel.MouseLeave, AddressOf PlaylabelLeave
                            AddHandler playlabel.Click, AddressOf PlayClick
                            Panel4.Controls.Add(playlabel)
                            Dim namelabel As Label = New Label
                            namelabel.Name = VideoCountName & "_NameLabel"
                            namelabel.Font = Label3.Font
                            namelabel.ForeColor = Color.SteelBlue
                            namelabel.Text = finfo.Name
                            namelabel.Tag = VideoCountName & finfo.FullName
                            namelabel.AutoEllipsis = Label3.AutoEllipsis
                            namelabel.AutoSize = Label3.AutoSize
                            namelabel.Width = Label3.Width
                            namelabel.Height = Label3.Height
                            namelabel.Left = 16
                            namelabel.Top = currenttop + 288
                            AddHandler namelabel.MouseEnter, AddressOf DirectoryHover
                            AddHandler namelabel.MouseLeave, AddressOf DirectoryLeave
                            AddHandler namelabel.Click, AddressOf PlayClick
                            Panel4.Controls.Add(namelabel)
                            Dim mainpic As PictureBox = New PictureBox
                            mainpic.Name = VideoCountName & "_MainPic"
                            mainpic.Tag = VideoCountName & finfo.FullName
                            mainpic.BorderStyle = BorderStyle.FixedSingle
                            mainpic.Width = PictureBox2.Width
                            mainpic.Height = PictureBox2.Height
                            mainpic.SizeMode = PictureBoxSizeMode.StretchImage
                            mainpic.Left = 20
                            mainpic.Top = currenttop
                            AddHandler mainpic.Click, AddressOf PlayClick
                            Panel4.Controls.Add(mainpic)
                            Dim toprightpic As PictureBox = New PictureBox
                            toprightpic.BorderStyle = BorderStyle.FixedSingle
                            toprightpic.Width = PictureBox3.Width
                            toprightpic.Height = PictureBox3.Height
                            toprightpic.SizeMode = PictureBoxSizeMode.StretchImage
                            toprightpic.Left = 341
                            toprightpic.Top = currenttop
                            Panel4.Controls.Add(toprightpic)
                            Dim bottomrightpic As PictureBox = New PictureBox
                            bottomrightpic.BorderStyle = BorderStyle.FixedSingle
                            bottomrightpic.Width = PictureBox4.Width
                            bottomrightpic.Height = PictureBox4.Height
                            bottomrightpic.SizeMode = PictureBoxSizeMode.StretchImage
                            bottomrightpic.Left = 341
                            bottomrightpic.Top = currenttop + 114



                            CurrentFolderLastPlayed = thumbnailfolder
                            Try
                                FolderFileList.Add((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "3.jpg").Replace("\\", "\"))
                                FolderFileList.Add((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "4.jpg").Replace("\\", "\"))
                                FolderFileList.Add((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "5.jpg").Replace("\\", "\"))

                                If My.Computer.FileSystem.FileExists((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "3.jpg").Replace("\\", "\")) = False Then
                                    If askedalready = False Then
                                        Me.TopMost = False
                                        If GenerateThumbnails_Dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                                            generatethumbs = True
                                        Else
                                            generatethumbs = False
                                        End If
                                        Me.TopMost = True
                                        askedalready = True
                                    End If

                                    If generatethumbs = True Then
                                        'Me.TopMost = False
                                        Me.Refresh()
                                        Dim apptorun As String
                                        apptorun = ("""" & Application.StartupPath & "\SnatchIt.exe"" –thumbs 5 """ & finfo.FullName & """ """ & thumbnailfolder & """").Replace("\\", "\")
                                        ApplicationLauncher(apptorun)
                                        'Me.TopMost = True
                                    End If
                                End If
                            Catch exa As Exception
                                Error_Handler(exa, "Generate Thumbs: " & finfo.Name)
                            End Try

                            If My.Computer.FileSystem.FileExists((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "3.jpg").Replace("\\", "\")) = True Then
                                mainpic.Image = Image.FromFile((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "3.jpg").Replace("\\", "\"))
                            End If
                            If My.Computer.FileSystem.FileExists((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "4.jpg").Replace("\\", "\")) = True Then
                                toprightpic.Image = Image.FromFile((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "4.jpg").Replace("\\", "\"))
                            End If
                            If My.Computer.FileSystem.FileExists((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "5.jpg").Replace("\\", "\")) = True Then
                                bottomrightpic.Image = Image.FromFile((thumbnailfolder & "\" & finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length) & "5.jpg").Replace("\\", "\"))
                            End If

                            Panel4.Controls.Add(bottomrightpic)
                            currenttop = currenttop + 354
                            'mainpic.Focus()
                        End If
                        finfo = Nothing

                    Catch ex1 As Exception
                        Error_Handler(ex1, "Load Folder")
                    End Try
                Next
                dinfo = Nothing

                If VideoCountInt <> 1 Then
                    VideoCount.Text = VideoCountInt & " Videos"
                Else
                    VideoCount.Text = VideoCountInt & " Video"
                End If
                VideoCount.Refresh()
            End If

            Dim foldertoclean As String = CurrentFolderLastPlayed
            CurrentFolderLastPlayed = (CurrentFolderLastPlayed & "\lastvid.txt").Replace("\\", "\")
            LastPlayedInFolder.Text = ""
            LastPlayedInFolder.Tag = ""
            If My.Computer.FileSystem.FileExists(CurrentFolderLastPlayed) = True Then
                Dim filetoremember As String = ""
                filetoremember = My.Computer.FileSystem.ReadAllText(CurrentFolderLastPlayed)
                'If My.Computer.FileSystem.FileExists(filetoremember) = False Then
                'LastPlayedInFolder.Text = ""
                'LastPlayedInFolder.Tag = ""
                'Else
                Dim inf As FileInfo = New FileInfo(filetoremember)
                LastPlayedInFolder.Text = "Last Accessed in this Folder: " & inf.Name
                LastPlayedInFolder.Tag = inf.FullName
                inf = Nothing
                If My.Computer.FileSystem.FileExists(filetoremember) = False Then
                    LastPlayedInFolder.ForeColor = Color.Silver
                Else
                    LastPlayedInFolder.ForeColor = Color.White
                End If
            End If
            If Not LastPlayed.Tag Is Nothing Then
                If LastPlayed.Tag.length > 0 Then
                    If My.Computer.FileSystem.FileExists(LastPlayed.Tag) = False Then
                        LastPlayed.ForeColor = Color.Silver
                    Else
                        LastPlayed.ForeColor = Color.White
                    End If
                Else
                    LastPlayed.ForeColor = Color.Silver
                End If
            Else
                LastPlayed.ForeColor = Color.Silver
            End If
            If My.Computer.FileSystem.DirectoryExists(foldertoclean) = True Then
                CleanFolder(foldertoclean, FolderFileList)
            End If


            CurrentFolderName.ForeColor = Color.SteelBlue
            CurrentFolderName.Refresh()
        Catch ex As Exception
            Error_Handler(ex, "Load Folder")
        End Try
    End Sub

    Private Sub DirectoryClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            VideoFolder = sender.tag
            LoadFolder()
        Catch ex As Exception
            Error_Handler(ex, "Directory Click")
        End Try
    End Sub

    Private Sub DirectoryLeave(ByVal sender As Object, ByVal e As EventArgs)
        Try
            sender.ForeColor = Color.SteelBlue
        Catch ex As Exception
            Error_Handler(ex, "Directory Leave")
        End Try
    End Sub

    Private Sub DirectoryHover(ByVal sender As Object, ByVal e As EventArgs)
        Try
            sender.ForeColor = Color.DarkRed
        Catch ex As Exception
            Error_Handler(ex, "Directory Hover")
        End Try
    End Sub


    Private Sub DeletelabelLeave(ByVal sender As Object, ByVal e As EventArgs)
        Try
            sender.ForeColor = Color.Black
        Catch ex As Exception
            Error_Handler(ex, "Directory Leave")
        End Try
    End Sub

    Private Sub DeletelabelHover(ByVal sender As Object, ByVal e As EventArgs)
        Try
            sender.ForeColor = Color.Gray
        Catch ex As Exception
            Error_Handler(ex, "Directory Hover")
        End Try
    End Sub

    Private Sub PlaylabelLeave(ByVal sender As Object, ByVal e As EventArgs)
        Try
            sender.ForeColor = Color.Silver
        Catch ex As Exception
            Error_Handler(ex, "Directory Leave")
        End Try
    End Sub

    Private Sub PlaylabelHover(ByVal sender As Object, ByVal e As EventArgs)
        Try
            sender.ForeColor = Color.Gray
        Catch ex As Exception
            Error_Handler(ex, "Directory Hover")
        End Try
    End Sub

    Private Sub PlayClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            PlayClick(sender)
        Catch ex As Exception
            Error_Handler(ex, "Play Click Dummy Function")
        End Try
    End Sub

    Private Sub PlayClick(ByVal sender As Object)
        Try
            Dim sendertag As String = ""
            Dim checktrackbar As Boolean = True
            Dim trackbarvariable As String = ""
            If IsNumeric(sender.tag.ToString.Substring(0, 4)) = True Then
                sendertag = sender.tag.ToString.Remove(0, 4)
                trackbarvariable = sender.tag.ToString.Substring(0, 4)
                checktrackbar = True
            Else
                sendertag = sender.tag
                trackbarvariable = ""
                checktrackbar = False
            End If



            If My.Computer.FileSystem.FileExists(sendertag) = True Then
                Me.WindowState = FormWindowState.Minimized
                Dim apptorun As String
                Dim argumentstorun


                Dim finfo As FileInfo = New FileInfo(sendertag)
                LastPlayed.Text = "Last Video Played: " & finfo.Name
                LastPlayed.ForeColor = Color.White
                LastPlayed.Tag = finfo.FullName

                Dim thumbnailfolder As String
                If Not finfo.Directory.Parent Is Nothing Then
                    thumbnailfolder = (Application.StartupPath & "\Thumbnails\" & (finfo.Directory.Parent.Name & "_" & finfo.Directory.Name).Replace(":\", "")).Replace("\\", "\")
                Else
                    thumbnailfolder = (Application.StartupPath & "\Thumbnails\" & ("Root" & finfo.Directory.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                End If
                thumbnailfolder = (thumbnailfolder & "\lastvid.txt").Replace("\\", "\")
                If CurrentFolderLastPlayed = thumbnailfolder Then
                    My.Computer.FileSystem.WriteAllText(CurrentFolderLastPlayed, finfo.FullName, False)
                    LastPlayedInFolder.ForeColor = Color.White
                    LastPlayedInFolder.Text = "Last Accessed in this Folder: " & finfo.Name
                    LastPlayedInFolder.Tag = finfo.FullName
                End If
                Dim millisecondstostart As Double = 0
                Dim trk As TrackBar
                If checktrackbar = True Then
                    If Not Panel4.Controls(trackbarvariable & "_StartTrackBar") Is Nothing Then
                        trk = Panel4.Controls(trackbarvariable & "_StartTrackBar")
                        millisecondstostart = (trk.Value * 1000)
                    End If
                Else
                    Dim valuefile As String
                    Dim readvalue As String
                    If Not finfo.Directory.Parent Is Nothing Then
                        valuefile = (Application.StartupPath & "\Thumbnails\" & (finfo.Directory.Parent.Name & "_" & finfo.Directory.Name).Replace(":\", "")).Replace("\\", "\")
                    Else
                        valuefile = (Application.StartupPath & "\Thumbnails\" & ("Root" & finfo.Directory.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                    End If
                    valuefile = (valuefile & "\" & finfo.Name & ".txt").Replace("\\", "\")
                    If My.Computer.FileSystem.FileExists(valuefile) = True Then
                        readvalue = My.Computer.FileSystem.ReadAllText(valuefile)
                        If IsNumeric(readvalue) Then
                            millisecondstostart = readvalue * 1000
                        Else
                            millisecondstostart = 0
                        End If
                    Else
                        millisecondstostart = 0
                    End If
                End If

                '------------------------------
                'History(Records)

                Dim hdriveinfo As DirectoryInfo = finfo.Directory
                While Not hdriveinfo.Parent Is Nothing
                    hdriveinfo = hdriveinfo.Parent
                End While
                Dim temporaryarraylist1 As ArrayList = New ArrayList(3)
                temporaryarraylist1.Add(hdriveinfo.FullName)
                For Each element As String In HistoryDrives
                    If element <> hdriveinfo.FullName Then
                        temporaryarraylist1.Add(element)
                    End If
                    If temporaryarraylist1.Count = 3 Then
                        Exit For
                    End If
                Next
                HistoryDrives.Clear()
                For Each element As String In temporaryarraylist1
                    HistoryDrives.Add(element)
                Next
                temporaryarraylist1.Clear()
                temporaryarraylist1 = Nothing
                hdriveinfo = Nothing

                Dim temporaryarraylist2 As ArrayList = New ArrayList(5)
                temporaryarraylist2.Add(finfo.Directory.FullName)
                For Each element As String In HistoryFolders
                    If element <> finfo.Directory.FullName Then
                        temporaryarraylist2.Add(element)
                    End If
                    If temporaryarraylist2.Count = 5 Then
                        Exit For
                    End If
                Next
                HistoryFolders.Clear()
                For Each element As String In temporaryarraylist2
                    HistoryFolders.Add(element)
                Next
                temporaryarraylist2.Clear()
                temporaryarraylist2 = Nothing

                Dim temporaryarraylist3 As ArrayList = New ArrayList(8)
                temporaryarraylist3.Add(finfo.FullName)
                For Each element As String In HistoryFiles
                    If element <> finfo.FullName Then
                        temporaryarraylist3.Add(element)
                    End If
                    If temporaryarraylist3.Count = 8 Then
                        Exit For
                    End If
                Next
                HistoryFiles.Clear()
                For Each element As String In temporaryarraylist3
                    HistoryFiles.Add(element)
                Next
                temporaryarraylist3.Clear()
                temporaryarraylist3 = Nothing
                '------------------------------

                '------------------------------
                'Folder History Records


                '------------------------------
                'Folder History Records
                Dim folderfilehistory As String



                If Not finfo.Directory.Parent Is Nothing Then
                    folderfilehistory = (Application.StartupPath & "\Thumbnails\" & (finfo.Directory.Parent.Name & "_" & finfo.Directory.Name).Replace(":\", "")).Replace("\\", "\")
                Else
                    folderfilehistory = (Application.StartupPath & "\Thumbnails\" & ("Root" & finfo.Directory.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                End If
                folderfilehistory = (folderfilehistory & "\" & "folderhistoryfiles.txt").Replace("\\", "\")
                FolderHistoryFiles.Clear()
                If My.Computer.FileSystem.FileExists(folderfilehistory) = True Then


                    Dim readerfh As StreamReader = New StreamReader(folderfilehistory)
                    Dim addcounter As Integer = 0
                    While readerfh.Peek <> -1
                        FolderHistoryFiles.Add(readerfh.ReadLine)
                        addcounter = addcounter + 1
                        If addcounter = 10 Then
                            Exit While
                        End If
                    End While
                    readerfh.Close()
                    readerfh = Nothing
                End If
                '------------------------------

                Dim temporaryarraylist4 As ArrayList = New ArrayList(10)
                temporaryarraylist4.Add(finfo.FullName)
                For Each element As String In FolderHistoryFiles
                    If element <> finfo.FullName Then
                        temporaryarraylist4.Add(element)
                    End If
                    If temporaryarraylist4.Count = 10 Then
                        Exit For
                    End If
                Next
                FolderHistoryFiles.Clear()
                For Each element As String In temporaryarraylist4
                    FolderHistoryFiles.Add(element)
                Next
                temporaryarraylist4.Clear()
                temporaryarraylist4 = Nothing


                Dim writer As StreamWriter = New StreamWriter(folderfilehistory, False)
                For Each element As String In FolderHistoryFiles
                    writer.WriteLine(element)
                Next
                writer.Flush()
                writer.Close()
                writer = Nothing
                '------------------------------


                apptorun = ("""" & Application.StartupPath & "\mplayerc.exe""").Replace("\\", "\")
                argumentstorun = ("""" & finfo.FullName & """ /play /close /fullscreen /start " & millisecondstostart & "").Replace("\\", "\")



                Dim starttime, stoptime As DateTime
                starttime = Now
                ApplicationLauncher(apptorun, argumentstorun)
                stoptime = Now
                Dim elapsedseconds As Long = DateDiff(DateInterval.Second, starttime, stoptime)
                If elapsedseconds > 5 Then
                    elapsedseconds = elapsedseconds - 5
                End If
                If checktrackbar = True Then
                    If Not trk Is Nothing Then
                        If trk.Value + elapsedseconds < trk.Maximum Then
                            trk.Value = trk.Value + elapsedseconds
                        Else
                            trk.Value = 0
                        End If

                    End If
                Else
                    Dim valuefiletowrite As String
                    If Not finfo.Directory.Parent Is Nothing Then
                        valuefiletowrite = (Application.StartupPath & "\Thumbnails\" & (finfo.Directory.Parent.Name & "_" & finfo.Directory.Name).Replace(":\", "")).Replace("\\", "\")
                    Else
                        valuefiletowrite = (Application.StartupPath & "\Thumbnails\" & ("Root" & finfo.Directory.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                    End If
                    valuefiletowrite = (valuefiletowrite & "\" & finfo.Name & ".txt").Replace("\\", "\")
                    My.Computer.FileSystem.WriteAllText(valuefiletowrite, (millisecondstostart / 1000) + elapsedseconds, False)
                End If
                finfo = Nothing

                Me.WindowState = FormWindowState.Maximized
                Try
                    sender.focus()
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            Error_Handler(ex, "Play Click")
        End Try
    End Sub

    Private Sub LastPlayed_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastPlayed.MouseEnter
        Try
            If My.Computer.FileSystem.FileExists(LastPlayed.Tag) = True Then
                LastPlayed.ForeColor = Color.DarkRed
            Else
                LastPlayed.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "Last Played Mouse Enter")
        End Try
    End Sub

    Private Sub LastPlayed_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastPlayed.MouseLeave
        Try
            If My.Computer.FileSystem.FileExists(LastPlayed.Tag) = True Then
                LastPlayed.ForeColor = Color.White
            Else
                LastPlayed.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "Last Played Mouse Leave")
        End Try
    End Sub

    Private Sub LastPlayed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastPlayed.Click
        Try
            PlayClick(sender, e)
        Catch ex As Exception
            Error_Handler(ex, "Last Played Click")
        End Try
    End Sub

    Private Sub DeleteClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If My.Computer.FileSystem.FileExists(sender.tag) = True Then
                Me.TopMost = False
                If DeleteFile_Dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                    My.Computer.FileSystem.DeleteFile(sender.tag, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    LoadFolder()
                End If
                Me.TopMost = True
            End If
            If My.Computer.FileSystem.FileExists(LastPlayed.Tag) = True Then
                LastPlayed.ForeColor = Color.White
            Else
                LastPlayed.ForeColor = Color.Silver
            End If
            If My.Computer.FileSystem.FileExists(LastPlayedInFolder.Tag) = True Then
                LastPlayedInFolder.ForeColor = Color.White
            Else
                LastPlayedInFolder.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "Delete Click")
        End Try
    End Sub

    Private Function ApplicationLauncher(ByVal AppToRun As String, ByVal ArgumentsToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process
            myProcess.StartInfo.UseShellExecute = False
            Dim sErr As StreamReader
            Dim sOut As StreamReader
            Dim sIn As StreamWriter
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.StartInfo.RedirectStandardOutput = True
            myProcess.StartInfo.RedirectStandardError = True

            myProcess.StartInfo.FileName = AppToRun
            myProcess.StartInfo.Arguments = ArgumentsToRun

            myProcess.Start()
            sIn = myProcess.StandardInput
            sIn.AutoFlush = True

            sOut = myProcess.StandardOutput()
            sErr = myProcess.StandardError

            sIn.Write(AppToRun & System.Environment.NewLine)
            sIn.Write("exit" & System.Environment.NewLine)
            s = sOut.ReadToEnd()

            If Not myProcess.HasExited Then
                myProcess.Kill()
            End If

            sIn.Close()
            sOut.Close()
            sErr.Close()
            myProcess.Close()

        Catch ex As Exception
            Error_Handler(ex, "ApplicationLauncher")
        End Try
        Return s
    End Function

    Private Function ApplicationLauncher(ByVal AppToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process
            myProcess.StartInfo.UseShellExecute = False
            Dim sErr As StreamReader
            Dim sOut As StreamReader
            Dim sIn As StreamWriter
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.StartInfo.RedirectStandardOutput = True
            myProcess.StartInfo.RedirectStandardError = True
            myProcess.StartInfo.FileName = AppToRun

            myProcess.Start()
            sIn = myProcess.StandardInput
            sIn.AutoFlush = True

            sOut = myProcess.StandardOutput()
            sErr = myProcess.StandardError

            sIn.Write(AppToRun & System.Environment.NewLine)
            sIn.Write("exit" & System.Environment.NewLine)
            s = sOut.ReadToEnd()

            If Not myProcess.HasExited Then
                myProcess.Kill()
            End If

            sIn.Close()
            sOut.Close()
            sErr.Close()
            myProcess.Close()

        Catch ex As Exception
            Error_Handler(ex, "ApplicationLauncher")
        End Try
        Return s
    End Function

    Private Sub Button_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Exit.Click
        Try
            Dim gn As GenericDialog = New GenericDialog
            gn.Text = "Do You Wish to Exit BigVid?"
            gn.Label1.Text = "Do you wish to exit BigVid?"
            Me.TopMost = False
            If gn.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.Close()
            End If
            Me.TopMost = True
        Catch ex As Exception
            Error_Handler(ex, "Application Shutdown")
        End Try

    End Sub

    Private Sub Button_Shutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Shutdown.Click
        Try
            Dim gn As GenericDialog = New GenericDialog
            gn.Text = "Do You Wish to Shutdown?"
            gn.Label1.Text = "Do you wish to shutdown this computer?"
            Me.TopMost = False
            If gn.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.Hide()
                Dim ProcID As Integer
                ProcID = Shell("shutdown -s -f -t 2", AppWinStyle.NormalFocus, True, 30000)
                Me.Close()
            End If
            Me.TopMost = True
        Catch ex As Exception
            Error_Handler(ex, "PC Shutdown")
        End Try
    End Sub

    Private Sub Panel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_VideoFolder.Click
        Try
            FolderBrowserDialog1.Description = "Please Select Video Base Folder"
            If My.Computer.FileSystem.DirectoryExists(VideoFolder) = False Then
                FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop
                FolderBrowserDialog1.SelectedPath = Environment.SpecialFolder.Desktop
            Else
                FolderBrowserDialog1.SelectedPath = VideoFolder
            End If
            FolderBrowserDialog1.ShowNewFolderButton = False
            Me.TopMost = False
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                VideoFolder = FolderBrowserDialog1.SelectedPath
                LoadFolder()
            End If
            Me.TopMost = True
        Catch ex As Exception
            Error_Handler(ex, "Select New Base Folder")
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Try
            Dim dinfo As DirectoryInfo = New DirectoryInfo(VideoFolder)
            If Not dinfo.Parent Is Nothing Then
                VideoFolder = dinfo.Parent.FullName
                LoadFolder()
            End If
            dinfo = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Directory Up Click")
        End Try
    End Sub

    Private Sub PictureBox1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseEnter
        Try
            If Not PictureBox1.Image Is Nothing Then
                PictureBox1.Image = ImageList1.Images.Item(1)
            End If
        Catch ex As Exception
            Error_Handler(ex, "Directory Up Hover")
        End Try
    End Sub

    Private Sub PictureBox1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        Try
            If Not PictureBox1.Image Is Nothing Then
                PictureBox1.Image = ImageList1.Images.Item(0)
            End If
        Catch ex As Exception
            Error_Handler(ex, "Directory Up Leave")
        End Try
    End Sub


    Private Sub LastPlayedInFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastPlayedInFolder.Click
        PlayClick(sender, e)
    End Sub

    Private Sub LastPlayedInFolder_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastPlayedInFolder.MouseEnter
        Try
            If My.Computer.FileSystem.FileExists(LastPlayedInFolder.Tag) = True Then
                LastPlayedInFolder.ForeColor = Color.DarkRed
            Else
                LastPlayedInFolder.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "Last Played Mouse Enter")
        End Try
    End Sub

    Private Sub LastPlayedInFolder_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastPlayedInFolder.MouseLeave
        Try
            If My.Computer.FileSystem.FileExists(LastPlayedInFolder.Tag) = True Then
                LastPlayedInFolder.ForeColor = Color.White
            Else
                LastPlayedInFolder.ForeColor = Color.Silver
            End If
        Catch ex As Exception
            Error_Handler(ex, "Last Played Mouse Leave")
        End Try
    End Sub

    Private Function DosShellCommand(ByVal AppToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process

            myProcess.StartInfo.FileName = "cmd.exe"
            myProcess.StartInfo.UseShellExecute = False
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.StartInfo.RedirectStandardOutput = True
            myProcess.StartInfo.RedirectStandardError = True
            myProcess.Start()
            Dim sIn As StreamWriter = myProcess.StandardInput
            sIn.AutoFlush = True

            Dim sOut As StreamReader = myProcess.StandardOutput
            Dim sErr As StreamReader = myProcess.StandardError
            sIn.Write(AppToRun & _
               System.Environment.NewLine)
            sIn.Write("exit" & System.Environment.NewLine)
            s = sOut.ReadToEnd()
            If Not myProcess.HasExited Then
                myProcess.Kill()
            End If

            'MessageBox.Show("The 'dir' command window was closed at: " & myProcess.ExitTime & "." & System.Environment.NewLine & "Exit Code: " & myProcess.ExitCode)

            sIn.Close()
            sOut.Close()
            sErr.Close()
            myProcess.Close()
            'MessageBox.Show(s)
        Catch ex As Exception
            Error_Handler(ex, "DOS Shell Command")
        End Try
        Return s
    End Function

    Private Sub CleanFolder(ByVal FolderToClean As String, ByVal filesthatshouldbehere As ArrayList)
        Try
            Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderToClean)
            If dinfo.Exists = True Then
                For Each finfo As FileInfo In dinfo.GetFiles
                    If Not finfo.Name = "lastvid.txt" And Not finfo.Name = "folderhistoryfiles.txt" Then
                        If filesthatshouldbehere.IndexOf(finfo.FullName) < 0 Then
                            Try
                                finfo.Delete()
                            Catch ex As Exception
                                'ignore errors
                            End Try
                        End If
                    End If
                    finfo = Nothing
                Next
            End If
            dinfo = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Cleaning Folder: " & FolderToClean)
        End Try
    End Sub
   
    Private Sub TrackBar_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If My.Computer.FileSystem.FileExists(sender.tag.ToString) = True Then
                Dim finfo As FileInfo = New FileInfo(sender.tag.ToString)
                Dim thumbnailfolder As String
                If Not finfo.Directory.Parent Is Nothing Then
                    thumbnailfolder = (Application.StartupPath & "\Thumbnails\" & (finfo.Directory.Parent.Name & "_" & finfo.Directory.Name).Replace(":\", "")).Replace("\\", "\")
                Else
                    thumbnailfolder = (Application.StartupPath & "\Thumbnails\" & ("Root" & finfo.Directory.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
                End If
                thumbnailfolder = (thumbnailfolder & "\" & finfo.Name & ".txt").Replace("\\", "\")
                My.Computer.FileSystem.WriteAllText(thumbnailfolder, sender.value, False)
                finfo = Nothing
            End If
        Catch ex As Exception
            Error_Handler(ex, "TrackBar Value Changed")
        End Try
    End Sub
   
  
    Private Sub Button_ShowHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ShowHistory.Click
        Try
            Dim gn As HistoryDialog = New HistoryDialog
            Me.TopMost = False
            Dim counter As Integer = 1
            For Each element As String In HistoryDrives
                Try
                    gn.Controls("Drive" & counter).Tag = HistoryDrives.Item(counter - 1)
                    Dim description As String = ""
                    For Each drv As DriveInfo In My.Computer.FileSystem.Drives
                        Try
                            If drv.Name = HistoryDrives.Item(counter - 1) Then
                                description = " (" & drv.VolumeLabel & ")"
                                drv = Nothing
                                Exit For
                            End If
                            drv = Nothing
                        Catch ex As Exception
                            'do nothing
                        End Try
                    Next
                    gn.Controls("Drive" & counter).Text = HistoryDrives.Item(counter - 1) & description
                    If My.Computer.FileSystem.DirectoryExists(HistoryDrives.Item(counter - 1)) = True Then
                        gn.Controls("Drive" & counter).ForeColor = Color.White
                    Else
                        gn.Controls("Drive" & counter).ForeColor = Color.Silver
                    End If
                Catch ex As Exception
                    'do nothing
                End Try
                counter = counter + 1
            Next
            counter = 1
            For Each element As String In HistoryFolders
                gn.Controls("Folder" & counter).Tag = HistoryFolders.Item(counter - 1)
                Dim dinfo As DirectoryInfo = New DirectoryInfo(HistoryFolders.Item(counter - 1))
                If dinfo.Parent Is Nothing Then
                    gn.Controls("Folder" & counter).Text = HistoryFolders.Item(counter - 1)
                Else
                    gn.Controls("Folder" & counter).Text = dinfo.Parent.Name & "\" & dinfo.Name & " (" & HistoryFolders.Item(counter - 1).substring(0, 3) & ")"
                End If
                dinfo = Nothing
                If My.Computer.FileSystem.DirectoryExists(HistoryFolders.Item(counter - 1)) = True Then
                    gn.Controls("Folder" & counter).ForeColor = Color.White
                Else
                    gn.Controls("Folder" & counter).ForeColor = Color.Silver
                End If
                counter = counter + 1
            Next
            counter = 1
            For Each element As String In HistoryFiles
                gn.Controls("File" & counter).Tag = HistoryFiles.Item(counter - 1)
                Dim dinfo As FileInfo = New FileInfo(HistoryFiles.Item(counter - 1))
                gn.Controls("File" & counter).Text = dinfo.Name & " (" & HistoryFiles.Item(counter - 1).substring(0, 3) & ")"
                dinfo = Nothing
                If My.Computer.FileSystem.FileExists(HistoryFiles.Item(counter - 1)) = True Then
                    gn.Controls("File" & counter).ForeColor = Color.White
                Else
                    gn.Controls("File" & counter).ForeColor = Color.Silver
                End If
                counter = counter + 1
            Next

            If gn.ShowDialog = Windows.Forms.DialogResult.OK Then
                Select Case gn.choicetype
                    Case "Folder"
                        If My.Computer.FileSystem.DirectoryExists(gn.fullname) Then
                            Try
                                If VideoFolder <> gn.fullname Then
                                    FolderBrowserDialog1.SelectedPath = gn.fullname
                                    VideoFolder = FolderBrowserDialog1.SelectedPath
                                    LoadFolder()
                                End If
                            Catch ex As Exception
                                Error_Handler(ex, "Select New Base Folder")
                            End Try
                        End If
                    Case "File"
                        If My.Computer.FileSystem.FileExists(gn.fullname) Then
                            Try
                                HistoryPlay.Tag = gn.fullname
                                PlayClick(HistoryPlay)
                            Catch ex As Exception
                                Error_Handler(ex, "Play Video")
                            End Try
                        End If
                End Select

            End If
            gn.Dispose()
            gn = Nothing
            
            Me.TopMost = True
        Catch ex As Exception
            Error_Handler(ex, "Show History")
        End Try
    End Sub


    Private Sub CurrentFolderName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrentFolderName.Click
        Try

            '------------------------------
            'Folder History Records
            Dim folderfilehistory As String
            Dim dinfo As DirectoryInfo = New DirectoryInfo(CurrentFolderName.Tag)


            If Not dinfo.Parent Is Nothing Then
                folderfilehistory = (Application.StartupPath & "\Thumbnails\" & (dinfo.Parent.Name & "_" & dinfo.Name).Replace(":\", "")).Replace("\\", "\")
            Else
                folderfilehistory = (Application.StartupPath & "\Thumbnails\" & ("Root" & dinfo.Name.Substring(0, 1)).Replace(":\", "")).Replace("\\", "\")
            End If
            folderfilehistory = (folderfilehistory & "\" & "folderhistoryfiles.txt").Replace("\\", "\")
            FolderHistoryFiles.Clear()

            Dim addcounter As Integer = 0
            If My.Computer.FileSystem.FileExists(folderfilehistory) = True Then
                Dim readerfh As StreamReader = New StreamReader(folderfilehistory)
                While readerfh.Peek <> -1
                    FolderHistoryFiles.Add(readerfh.ReadLine)
                    addcounter = addcounter + 1
                    If addcounter = 10 Then
                        Exit While
                    End If
                End While
                readerfh.Close()
                readerfh = Nothing
            End If
            '------------------------------


            'Show Folder Played History
            Dim gn As FolderHistoryDialog = New FolderHistoryDialog
            gn.Label12.Text = "Last Files Accessed in " & CurrentFolderName.Text
            Me.TopMost = False
            Dim counter As Integer = 1
            For Each element As String In FolderHistoryFiles
                gn.Controls("File" & counter).Tag = FolderHistoryFiles.Item(counter - 1)
                Dim finfo As FileInfo = New FileInfo(FolderHistoryFiles.Item(counter - 1))
                gn.Controls("File" & counter).Text = finfo.Name
                finfo = Nothing
                If My.Computer.FileSystem.FileExists(FolderHistoryFiles.Item(counter - 1)) = True Then
                    gn.Controls("File" & counter).ForeColor = Color.White
                Else
                    gn.Controls("File" & counter).ForeColor = Color.Silver
                End If
                counter = counter + 1
            Next

            If gn.ShowDialog = Windows.Forms.DialogResult.OK Then
                Select Case gn.choicetype
                    Case "File"
                        If My.Computer.FileSystem.FileExists(gn.fullname) Then
                            Try
                                HistoryPlay.Tag = gn.fullname
                                PlayClick(HistoryPlay)
                            Catch ex As Exception
                                Error_Handler(ex, "Play Video")
                            End Try
                        End If
                End Select
            End If
            gn.Dispose()
            gn = Nothing

            Me.TopMost = True
        Catch ex As Exception
            Error_Handler(ex, "Show Folder History")
        End Try
    End Sub

    Private Sub DeleteFolderLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteFolderLabel.Click
        Try
            Dim gn As GenericDialog = New GenericDialog
            gn.Text = "Do You Wish to Delete this Folder?"
            gn.Label1.Text = "Do you wish to delete this folder?" & vbCrLf & VideoFolder
            Me.TopMost = False
            If gn.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim dinfo As DirectoryInfo = New DirectoryInfo(VideoFolder)
                My.Computer.FileSystem.DeleteDirectory(VideoFolder, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                VideoFolder = dinfo.Parent.FullName
                dinfo = Nothing
                LoadFolder()
            End If
            Me.TopMost = True
        Catch ex As Exception
            Error_Handler(ex, "Delete Folder Click")
        End Try
    End Sub
End Class
