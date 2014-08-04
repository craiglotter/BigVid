<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Screen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Screen))
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Button_Exit = New System.Windows.Forms.Panel
        Me.Button_Shutdown = New System.Windows.Forms.Panel
        Me.CurrentFolderName = New System.Windows.Forms.Label
        Me.Button_VideoFolder = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.VideoCount = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.Label4 = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.LastPlayed = New System.Windows.Forms.Label
        Me.LastPlayedInFolder = New System.Windows.Forms.Label
        Me.Button_ShowHistory = New System.Windows.Forms.Panel
        Me.HistoryPlay = New System.Windows.Forms.Label
        Me.DeleteFolderLabel = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Button_Exit
        '
        Me.Button_Exit.BackColor = System.Drawing.Color.Transparent
        Me.Button_Exit.BackgroundImage = Global.BigVid.My.Resources.Resources.BigVid_ExitApplication
        Me.Button_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button_Exit.Location = New System.Drawing.Point(812, 628)
        Me.Button_Exit.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_Exit.Name = "Button_Exit"
        Me.Button_Exit.Size = New System.Drawing.Size(54, 55)
        Me.Button_Exit.TabIndex = 0
        '
        'Button_Shutdown
        '
        Me.Button_Shutdown.BackColor = System.Drawing.Color.Transparent
        Me.Button_Shutdown.BackgroundImage = Global.BigVid.My.Resources.Resources.BigVid_ShutdownPC
        Me.Button_Shutdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button_Shutdown.Location = New System.Drawing.Point(754, 628)
        Me.Button_Shutdown.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_Shutdown.Name = "Button_Shutdown"
        Me.Button_Shutdown.Size = New System.Drawing.Size(54, 55)
        Me.Button_Shutdown.TabIndex = 1
        '
        'CurrentFolderName
        '
        Me.CurrentFolderName.AutoEllipsis = True
        Me.CurrentFolderName.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurrentFolderName.Location = New System.Drawing.Point(48, 75)
        Me.CurrentFolderName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.CurrentFolderName.Name = "CurrentFolderName"
        Me.CurrentFolderName.Size = New System.Drawing.Size(484, 43)
        Me.CurrentFolderName.TabIndex = 2
        Me.CurrentFolderName.Text = "N/A"
        Me.CurrentFolderName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button_VideoFolder
        '
        Me.Button_VideoFolder.BackColor = System.Drawing.Color.Transparent
        Me.Button_VideoFolder.BackgroundImage = Global.BigVid.My.Resources.Resources.BigVid_OpenFolder
        Me.Button_VideoFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button_VideoFolder.Location = New System.Drawing.Point(122, 20)
        Me.Button_VideoFolder.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_VideoFolder.Name = "Button_VideoFolder"
        Me.Button_VideoFolder.Size = New System.Drawing.Size(90, 64)
        Me.Button_VideoFolder.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.BigVid.My.Resources.Resources.BigVid_Background_Video
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.DeleteFolderLabel)
        Me.Panel1.Controls.Add(Me.VideoCount)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.CurrentFolderName)
        Me.Panel1.Location = New System.Drawing.Point(64, 12)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(580, 671)
        Me.Panel1.TabIndex = 4
        '
        'VideoCount
        '
        Me.VideoCount.BackColor = System.Drawing.Color.Transparent
        Me.VideoCount.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoCount.ForeColor = System.Drawing.Color.White
        Me.VideoCount.Location = New System.Drawing.Point(125, 11)
        Me.VideoCount.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.VideoCount.Name = "VideoCount"
        Me.VideoCount.Size = New System.Drawing.Size(453, 31)
        Me.VideoCount.TabIndex = 9
        Me.VideoCount.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.TrackBar1)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.PictureBox3)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.PictureBox4)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Location = New System.Drawing.Point(34, 134)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(508, 479)
        Me.Panel4.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DimGray
        Me.Label5.Location = New System.Drawing.Point(342, 228)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 24)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "00:00:00"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label5.Visible = False
        '
        'TrackBar1
        '
        Me.TrackBar1.AutoSize = False
        Me.TrackBar1.Location = New System.Drawing.Point(12, 261)
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(333, 24)
        Me.TrackBar1.TabIndex = 9
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar1.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(460, 228)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 24)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "X"
        Me.Label4.Visible = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox3.Location = New System.Drawing.Point(342, 3)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(138, 108)
        Me.PictureBox3.TabIndex = 4
        Me.PictureBox3.TabStop = False
        Me.PictureBox3.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoEllipsis = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 288)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(463, 60)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Label3"
        Me.Label3.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Location = New System.Drawing.Point(21, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(315, 252)
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox4.Location = New System.Drawing.Point(342, 117)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(138, 108)
        Me.PictureBox4.TabIndex = 5
        Me.PictureBox4.TabStop = False
        Me.PictureBox4.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Silver
        Me.Label2.Location = New System.Drawing.Point(351, 261)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 24)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Play Video"
        Me.Label2.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(420, 17)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(189, 31)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Label1"
        Me.Label1.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.BigVid.My.Resources.Resources.BigVid_Background_Folder
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.Button_VideoFolder)
        Me.Panel2.Location = New System.Drawing.Point(664, 79)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(232, 543)
        Me.Panel2.TabIndex = 5
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(61, 31)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(54, 53)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.AutoScroll = True
        Me.Panel3.Location = New System.Drawing.Point(18, 99)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(194, 427)
        Me.Panel3.TabIndex = 4
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "BigVid_UpDirectoryBlue.gif")
        Me.ImageList1.Images.SetKeyName(1, "BigVid_UpDirectoryRed.gif")
        '
        'LastPlayed
        '
        Me.LastPlayed.AutoEllipsis = True
        Me.LastPlayed.BackColor = System.Drawing.Color.Transparent
        Me.LastPlayed.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LastPlayed.ForeColor = System.Drawing.Color.White
        Me.LastPlayed.Location = New System.Drawing.Point(79, 732)
        Me.LastPlayed.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LastPlayed.Name = "LastPlayed"
        Me.LastPlayed.Size = New System.Drawing.Size(845, 27)
        Me.LastPlayed.TabIndex = 8
        '
        'LastPlayedInFolder
        '
        Me.LastPlayedInFolder.AutoEllipsis = True
        Me.LastPlayedInFolder.BackColor = System.Drawing.Color.Transparent
        Me.LastPlayedInFolder.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LastPlayedInFolder.ForeColor = System.Drawing.Color.White
        Me.LastPlayedInFolder.Location = New System.Drawing.Point(79, 694)
        Me.LastPlayedInFolder.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LastPlayedInFolder.Name = "LastPlayedInFolder"
        Me.LastPlayedInFolder.Size = New System.Drawing.Size(845, 27)
        Me.LastPlayedInFolder.TabIndex = 9
        '
        'Button_ShowHistory
        '
        Me.Button_ShowHistory.BackColor = System.Drawing.Color.Transparent
        Me.Button_ShowHistory.BackgroundImage = Global.BigVid.My.Resources.Resources.BigVid_History
        Me.Button_ShowHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button_ShowHistory.Location = New System.Drawing.Point(696, 628)
        Me.Button_ShowHistory.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_ShowHistory.Name = "Button_ShowHistory"
        Me.Button_ShowHistory.Size = New System.Drawing.Size(54, 55)
        Me.Button_ShowHistory.TabIndex = 10
        '
        'HistoryPlay
        '
        Me.HistoryPlay.BackColor = System.Drawing.Color.Transparent
        Me.HistoryPlay.Location = New System.Drawing.Point(752, 57)
        Me.HistoryPlay.Name = "HistoryPlay"
        Me.HistoryPlay.Size = New System.Drawing.Size(10, 10)
        Me.HistoryPlay.TabIndex = 11
        Me.HistoryPlay.Visible = False
        '
        'DeleteFolderLabel
        '
        Me.DeleteFolderLabel.AutoSize = True
        Me.DeleteFolderLabel.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteFolderLabel.ForeColor = System.Drawing.Color.Black
        Me.DeleteFolderLabel.Location = New System.Drawing.Point(518, 616)
        Me.DeleteFolderLabel.Name = "DeleteFolderLabel"
        Me.DeleteFolderLabel.Size = New System.Drawing.Size(24, 24)
        Me.DeleteFolderLabel.TabIndex = 10
        Me.DeleteFolderLabel.Text = "X"
        '
        'Main_Screen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(27.0!, 51.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.BigVid.My.Resources.Resources.BigVid_Background_Screen
        Me.ClientSize = New System.Drawing.Size(1024, 746)
        Me.ControlBox = False
        Me.Controls.Add(Me.HistoryPlay)
        Me.Controls.Add(Me.Button_ShowHistory)
        Me.Controls.Add(Me.LastPlayedInFolder)
        Me.Controls.Add(Me.LastPlayed)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button_Shutdown)
        Me.Controls.Add(Me.Button_Exit)
        Me.Font = New System.Drawing.Font("Arial Black", 27.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.SteelBlue
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(14, 10, 14, 10)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main_Screen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Button_Exit As System.Windows.Forms.Panel
    Friend WithEvents Button_Shutdown As System.Windows.Forms.Panel
    Friend WithEvents CurrentFolderName As System.Windows.Forms.Label
    Friend WithEvents Button_VideoFolder As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents LastPlayed As System.Windows.Forms.Label
    Friend WithEvents VideoCount As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LastPlayedInFolder As System.Windows.Forms.Label
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button_ShowHistory As System.Windows.Forms.Panel
    Friend WithEvents HistoryPlay As System.Windows.Forms.Label
    Friend WithEvents DeleteFolderLabel As System.Windows.Forms.Label

End Class
