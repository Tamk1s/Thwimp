<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.LoadTHPRoot = New System.Windows.Forms.FolderBrowserDialog()
        Me.TabTHP = New System.Windows.Forms.TabPage()
        Me.grpTHPEnc_Prog = New System.Windows.Forms.GroupBox()
        Me.lblTHPEnc_Prg_Cur = New System.Windows.Forms.Label()
        Me.lblTHPEnc_Prg_Ttl = New System.Windows.Forms.Label()
        Me.txtTHPEnc_Prg_Cur = New System.Windows.Forms.TextBox()
        Me.txtTHPEnc_Prg_Ttl = New System.Windows.Forms.TextBox()
        Me.prgCur = New System.Windows.Forms.ProgressBar()
        Me.prgTotal = New System.Windows.Forms.ProgressBar()
        Me.grpLog = New System.Windows.Forms.GroupBox()
        Me.btnLogSave = New System.Windows.Forms.Button()
        Me.btnLogClear = New System.Windows.Forms.Button()
        Me.picLog = New System.Windows.Forms.PictureBox()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.grpTHPEnc = New System.Windows.Forms.GroupBox()
        Me.lblTE_jpgq = New System.Windows.Forms.Label()
        Me.nudTE_jpgq = New System.Windows.Forms.NumericUpDown()
        Me.txtTE_D = New System.Windows.Forms.MaskedTextBox()
        Me.txtTE_F = New System.Windows.Forms.MaskedTextBox()
        Me.chkTE_wav = New System.Windows.Forms.CheckBox()
        Me.chkTE_Dum = New System.Windows.Forms.CheckBox()
        Me.lblTE_D = New System.Windows.Forms.Label()
        Me.btnTE_Enc = New System.Windows.Forms.Button()
        Me.chkTE_A1 = New System.Windows.Forms.CheckBox()
        Me.chkTE_A2 = New System.Windows.Forms.CheckBox()
        Me.lblTE_F = New System.Windows.Forms.Label()
        Me.chkTE_A3 = New System.Windows.Forms.CheckBox()
        Me.chkTE_A4 = New System.Windows.Forms.CheckBox()
        Me.chkTE_A5 = New System.Windows.Forms.CheckBox()
        Me.txtTE_M = New System.Windows.Forms.TextBox()
        Me.chkTE_A6 = New System.Windows.Forms.CheckBox()
        Me.lblTE_M = New System.Windows.Forms.Label()
        Me.chkTE_B1 = New System.Windows.Forms.CheckBox()
        Me.lblTE_C = New System.Windows.Forms.Label()
        Me.chkTE_B2 = New System.Windows.Forms.CheckBox()
        Me.lblTE_R = New System.Windows.Forms.Label()
        Me.chkTE_B3 = New System.Windows.Forms.CheckBox()
        Me.chkTE_B6 = New System.Windows.Forms.CheckBox()
        Me.chkTE_B4 = New System.Windows.Forms.CheckBox()
        Me.chkTE_B5 = New System.Windows.Forms.CheckBox()
        Me.grpTHPDec = New System.Windows.Forms.GroupBox()
        Me.grpTHPDec_Crop = New System.Windows.Forms.GroupBox()
        Me.chkRipDumF = New System.Windows.Forms.CheckBox()
        Me.radTD_All = New System.Windows.Forms.RadioButton()
        Me.radTD_Dum = New System.Windows.Forms.RadioButton()
        Me.txtTD_FE = New System.Windows.Forms.MaskedTextBox()
        Me.txtTD_FS = New System.Windows.Forms.MaskedTextBox()
        Me.lblTD_FE = New System.Windows.Forms.Label()
        Me.lblTD_FS = New System.Windows.Forms.Label()
        Me.radTD_B6 = New System.Windows.Forms.RadioButton()
        Me.txtTD_CH = New System.Windows.Forms.MaskedTextBox()
        Me.radTD_B5 = New System.Windows.Forms.RadioButton()
        Me.txtTD_CY = New System.Windows.Forms.MaskedTextBox()
        Me.chkRipM = New System.Windows.Forms.CheckBox()
        Me.radTD_B4 = New System.Windows.Forms.RadioButton()
        Me.txtTD_CW = New System.Windows.Forms.MaskedTextBox()
        Me.lblTD_FM = New System.Windows.Forms.Label()
        Me.lblTD_CH = New System.Windows.Forms.Label()
        Me.nudTD_M = New System.Windows.Forms.NumericUpDown()
        Me.radTD_B1 = New System.Windows.Forms.RadioButton()
        Me.txtTD_CX = New System.Windows.Forms.MaskedTextBox()
        Me.radTD_A2 = New System.Windows.Forms.RadioButton()
        Me.radTD_A3 = New System.Windows.Forms.RadioButton()
        Me.lblTD_CY = New System.Windows.Forms.Label()
        Me.radTD_B3 = New System.Windows.Forms.RadioButton()
        Me.lblTD_CW = New System.Windows.Forms.Label()
        Me.radTD_A1 = New System.Windows.Forms.RadioButton()
        Me.lblTD_CX = New System.Windows.Forms.Label()
        Me.radTD_A4 = New System.Windows.Forms.RadioButton()
        Me.radTD_B2 = New System.Windows.Forms.RadioButton()
        Me.radTD_A5 = New System.Windows.Forms.RadioButton()
        Me.radTD_A6 = New System.Windows.Forms.RadioButton()
        Me.btnRip = New System.Windows.Forms.Button()
        Me.chkRip_DSound = New System.Windows.Forms.CheckBox()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.lblTHPFile = New System.Windows.Forms.Label()
        Me.grpTHPInfo = New System.Windows.Forms.GroupBox()
        Me.grpVideo = New System.Windows.Forms.GroupBox()
        Me.grpVTDims = New System.Windows.Forms.GroupBox()
        Me.lblTDims_H = New System.Windows.Forms.Label()
        Me.lblTDims_W = New System.Windows.Forms.Label()
        Me.txtTDims_W = New System.Windows.Forms.TextBox()
        Me.txtTDims_H = New System.Windows.Forms.TextBox()
        Me.grpVPlayback = New System.Windows.Forms.GroupBox()
        Me.gVCtrl = New System.Windows.Forms.GroupBox()
        Me.lblVC_D = New System.Windows.Forms.Label()
        Me.txtVC_D = New System.Windows.Forms.TextBox()
        Me.txtVC_C = New System.Windows.Forms.TextBox()
        Me.lblVC_C = New System.Windows.Forms.Label()
        Me.txtVC_F = New System.Windows.Forms.TextBox()
        Me.lblVC_F = New System.Windows.Forms.Label()
        Me.grpVSubDims = New System.Windows.Forms.GroupBox()
        Me.lblVS_P = New System.Windows.Forms.Label()
        Me.txtVP_H = New System.Windows.Forms.TextBox()
        Me.txtVP_W = New System.Windows.Forms.TextBox()
        Me.lblVS_H = New System.Windows.Forms.Label()
        Me.txtVS_H = New System.Windows.Forms.TextBox()
        Me.lblVS_W = New System.Windows.Forms.Label()
        Me.txtVS_W = New System.Windows.Forms.TextBox()
        Me.lblVS_S = New System.Windows.Forms.Label()
        Me.grpVFrames = New System.Windows.Forms.GroupBox()
        Me.lblVF_T = New System.Windows.Forms.Label()
        Me.lblVF_S = New System.Windows.Forms.Label()
        Me.txtVF_S = New System.Windows.Forms.TextBox()
        Me.txtVF_T = New System.Windows.Forms.TextBox()
        Me.grpVArrInfo = New System.Windows.Forms.GroupBox()
        Me.grpVArr = New System.Windows.Forms.GroupBox()
        Me.lblArr_S = New System.Windows.Forms.Label()
        Me.txtArr_S = New System.Windows.Forms.TextBox()
        Me.txtArr_C = New System.Windows.Forms.TextBox()
        Me.txtArr_R = New System.Windows.Forms.TextBox()
        Me.lblArr_C = New System.Windows.Forms.Label()
        Me.lblArr_R = New System.Windows.Forms.Label()
        Me.gVMult = New System.Windows.Forms.GroupBox()
        Me.lblVM_M = New System.Windows.Forms.Label()
        Me.txtV_TSubs = New System.Windows.Forms.TextBox()
        Me.txtVM_M = New System.Windows.Forms.TextBox()
        Me.lblV_TSubs = New System.Windows.Forms.Label()
        Me.lblFDesc = New System.Windows.Forms.Label()
        Me.grpAudio = New System.Windows.Forms.GroupBox()
        Me.txtA_F = New System.Windows.Forms.TextBox()
        Me.lblA_F = New System.Windows.Forms.Label()
        Me.txtA_S = New System.Windows.Forms.TextBox()
        Me.lblA_S = New System.Windows.Forms.Label()
        Me.txtA_A = New System.Windows.Forms.TextBox()
        Me.lblA_A = New System.Windows.Forms.Label()
        Me.txtFDesc = New System.Windows.Forms.TextBox()
        Me.cmbTHP = New System.Windows.Forms.ComboBox()
        Me.TabOptions = New System.Windows.Forms.TabPage()
        Me.grpHelp = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btnCmdline = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnRelease = New System.Windows.Forms.Button()
        Me.btnWiki = New System.Windows.Forms.Button()
        Me.btnWeb = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.grpOptions = New System.Windows.Forms.GroupBox()
        Me.txtDataDir = New System.Windows.Forms.TextBox()
        Me.btnDataDir = New System.Windows.Forms.Button()
        Me.lblDataDir = New System.Windows.Forms.Label()
        Me.chkLogFull = New System.Windows.Forms.CheckBox()
        Me.lblRoot = New System.Windows.Forms.Label()
        Me.chkAudio = New System.Windows.Forms.CheckBox()
        Me.picOptions = New System.Windows.Forms.PictureBox()
        Me.chkEMusic = New System.Windows.Forms.CheckBox()
        Me.txtRoot = New System.Windows.Forms.TextBox()
        Me.chkMsg = New System.Windows.Forms.CheckBox()
        Me.btnBrowseRoot = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.lblFFMPEG = New System.Windows.Forms.Label()
        Me.btnLoadSettings = New System.Windows.Forms.Button()
        Me.txtFFMPEG = New System.Windows.Forms.TextBox()
        Me.lblFFPlayTemp = New System.Windows.Forms.Label()
        Me.btnBrowseFFMPEG = New System.Windows.Forms.Button()
        Me.btnBrowseFFPlayTemp = New System.Windows.Forms.Button()
        Me.txtTHPConv = New System.Windows.Forms.TextBox()
        Me.txtFFPlayTemp = New System.Windows.Forms.TextBox()
        Me.btnBrowseTHPConv = New System.Windows.Forms.Button()
        Me.btniView = New System.Windows.Forms.Button()
        Me.txtiView = New System.Windows.Forms.TextBox()
        Me.lblTHPConv = New System.Windows.Forms.Label()
        Me.lbliView = New System.Windows.Forms.Label()
        Me.tabApp = New System.Windows.Forms.TabControl()
        Me.LoadFFMPEGRoot = New System.Windows.Forms.FolderBrowserDialog()
        Me.LoadTHPConv = New System.Windows.Forms.OpenFileDialog()
        Me.ofdRip = New System.Windows.Forms.OpenFileDialog()
        Me.ofdOutput = New System.Windows.Forms.FolderBrowserDialog()
        Me.LoadiView = New System.Windows.Forms.OpenFileDialog()
        Me.LoadFFPlayWork = New System.Windows.Forms.FolderBrowserDialog()
        Me.ofdLoadSettings = New System.Windows.Forms.OpenFileDialog()
        Me.LoadDataDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.ofdSaveSettings = New System.Windows.Forms.OpenFileDialog()
        Me.SaveLog = New System.Windows.Forms.OpenFileDialog()
        Me.grpTHPDec_Pre = New System.Windows.Forms.GroupBox()
        Me.grpTHPDec_Time = New System.Windows.Forms.GroupBox()
        Me.grpTHPDec_Settings = New System.Windows.Forms.GroupBox()
        Me.lblTD_SpPre = New System.Windows.Forms.Label()
        Me.TabTHP.SuspendLayout()
        Me.grpTHPEnc_Prog.SuspendLayout()
        Me.grpLog.SuspendLayout()
        CType(Me.picLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTHPEnc.SuspendLayout()
        CType(Me.nudTE_jpgq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTHPDec.SuspendLayout()
        Me.grpTHPDec_Crop.SuspendLayout()
        CType(Me.nudTD_M, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTHPInfo.SuspendLayout()
        Me.grpVideo.SuspendLayout()
        Me.grpVTDims.SuspendLayout()
        Me.grpVPlayback.SuspendLayout()
        Me.gVCtrl.SuspendLayout()
        Me.grpVSubDims.SuspendLayout()
        Me.grpVFrames.SuspendLayout()
        Me.grpVArrInfo.SuspendLayout()
        Me.grpVArr.SuspendLayout()
        Me.gVMult.SuspendLayout()
        Me.grpAudio.SuspendLayout()
        Me.TabOptions.SuspendLayout()
        Me.grpHelp.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOptions.SuspendLayout()
        CType(Me.picOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabApp.SuspendLayout()
        Me.grpTHPDec_Pre.SuspendLayout()
        Me.grpTHPDec_Time.SuspendLayout()
        Me.grpTHPDec_Settings.SuspendLayout()
        Me.SuspendLayout()
        '
        'LoadTHPRoot
        '
        Me.LoadTHPRoot.Description = "Select the root directory for THP files"
        Me.LoadTHPRoot.ShowNewFolderButton = False
        '
        'TabTHP
        '
        Me.TabTHP.Controls.Add(Me.grpTHPEnc_Prog)
        Me.TabTHP.Controls.Add(Me.grpLog)
        Me.TabTHP.Controls.Add(Me.grpTHPEnc)
        Me.TabTHP.Controls.Add(Me.grpTHPDec)
        Me.TabTHP.Controls.Add(Me.lblTHPFile)
        Me.TabTHP.Controls.Add(Me.grpTHPInfo)
        Me.TabTHP.Controls.Add(Me.cmbTHP)
        Me.TabTHP.Location = New System.Drawing.Point(4, 25)
        Me.TabTHP.Margin = New System.Windows.Forms.Padding(4)
        Me.TabTHP.Name = "TabTHP"
        Me.TabTHP.Padding = New System.Windows.Forms.Padding(4)
        Me.TabTHP.Size = New System.Drawing.Size(577, 951)
        Me.TabTHP.TabIndex = 1
        Me.TabTHP.Text = "THP"
        Me.TabTHP.UseVisualStyleBackColor = True
        '
        'grpTHPEnc_Prog
        '
        Me.grpTHPEnc_Prog.Controls.Add(Me.lblTHPEnc_Prg_Cur)
        Me.grpTHPEnc_Prog.Controls.Add(Me.lblTHPEnc_Prg_Ttl)
        Me.grpTHPEnc_Prog.Controls.Add(Me.txtTHPEnc_Prg_Cur)
        Me.grpTHPEnc_Prog.Controls.Add(Me.txtTHPEnc_Prg_Ttl)
        Me.grpTHPEnc_Prog.Controls.Add(Me.prgCur)
        Me.grpTHPEnc_Prog.Controls.Add(Me.prgTotal)
        Me.grpTHPEnc_Prog.Location = New System.Drawing.Point(341, 614)
        Me.grpTHPEnc_Prog.Name = "grpTHPEnc_Prog"
        Me.grpTHPEnc_Prog.Size = New System.Drawing.Size(227, 329)
        Me.grpTHPEnc_Prog.TabIndex = 48
        Me.grpTHPEnc_Prog.TabStop = False
        Me.grpTHPEnc_Prog.Text = "Progress"
        '
        'lblTHPEnc_Prg_Cur
        '
        Me.lblTHPEnc_Prg_Cur.AutoSize = True
        Me.lblTHPEnc_Prg_Cur.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTHPEnc_Prg_Cur.Location = New System.Drawing.Point(150, 309)
        Me.lblTHPEnc_Prg_Cur.Name = "lblTHPEnc_Prg_Cur"
        Me.lblTHPEnc_Prg_Cur.Size = New System.Drawing.Size(71, 17)
        Me.lblTHPEnc_Prg_Cur.TabIndex = 50
        Me.lblTHPEnc_Prg_Cur.Text = "100.00%"
        '
        'lblTHPEnc_Prg_Ttl
        '
        Me.lblTHPEnc_Prg_Ttl.AutoSize = True
        Me.lblTHPEnc_Prg_Ttl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTHPEnc_Prg_Ttl.Location = New System.Drawing.Point(150, 149)
        Me.lblTHPEnc_Prg_Ttl.Name = "lblTHPEnc_Prg_Ttl"
        Me.lblTHPEnc_Prg_Ttl.Size = New System.Drawing.Size(71, 17)
        Me.lblTHPEnc_Prg_Ttl.TabIndex = 49
        Me.lblTHPEnc_Prg_Ttl.Text = "100.00%"
        '
        'txtTHPEnc_Prg_Cur
        '
        Me.txtTHPEnc_Prg_Cur.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTHPEnc_Prg_Cur.Location = New System.Drawing.Point(2, 178)
        Me.txtTHPEnc_Prg_Cur.Multiline = True
        Me.txtTHPEnc_Prg_Cur.Name = "txtTHPEnc_Prg_Cur"
        Me.txtTHPEnc_Prg_Cur.ReadOnly = True
        Me.txtTHPEnc_Prg_Cur.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTHPEnc_Prg_Cur.Size = New System.Drawing.Size(219, 124)
        Me.txtTHPEnc_Prg_Cur.TabIndex = 7
        Me.txtTHPEnc_Prg_Cur.TabStop = False
        '
        'txtTHPEnc_Prg_Ttl
        '
        Me.txtTHPEnc_Prg_Ttl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTHPEnc_Prg_Ttl.Location = New System.Drawing.Point(3, 19)
        Me.txtTHPEnc_Prg_Ttl.Multiline = True
        Me.txtTHPEnc_Prg_Ttl.Name = "txtTHPEnc_Prg_Ttl"
        Me.txtTHPEnc_Prg_Ttl.ReadOnly = True
        Me.txtTHPEnc_Prg_Ttl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTHPEnc_Prg_Ttl.Size = New System.Drawing.Size(218, 124)
        Me.txtTHPEnc_Prg_Ttl.TabIndex = 6
        Me.txtTHPEnc_Prg_Ttl.TabStop = False
        '
        'prgCur
        '
        Me.prgCur.Location = New System.Drawing.Point(2, 305)
        Me.prgCur.Name = "prgCur"
        Me.prgCur.Size = New System.Drawing.Size(141, 23)
        Me.prgCur.Step = 1
        Me.prgCur.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgCur.TabIndex = 1
        '
        'prgTotal
        '
        Me.prgTotal.Location = New System.Drawing.Point(3, 149)
        Me.prgTotal.Name = "prgTotal"
        Me.prgTotal.Size = New System.Drawing.Size(135, 23)
        Me.prgTotal.Step = 1
        Me.prgTotal.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgTotal.TabIndex = 0
        '
        'grpLog
        '
        Me.grpLog.Controls.Add(Me.btnLogSave)
        Me.grpLog.Controls.Add(Me.btnLogClear)
        Me.grpLog.Controls.Add(Me.picLog)
        Me.grpLog.Controls.Add(Me.txtLog)
        Me.grpLog.Location = New System.Drawing.Point(23, 773)
        Me.grpLog.Name = "grpLog"
        Me.grpLog.Size = New System.Drawing.Size(290, 171)
        Me.grpLog.TabIndex = 44
        Me.grpLog.TabStop = False
        Me.grpLog.Text = "Log"
        '
        'btnLogSave
        '
        Me.btnLogSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogSave.Location = New System.Drawing.Point(161, 140)
        Me.btnLogSave.Name = "btnLogSave"
        Me.btnLogSave.Size = New System.Drawing.Size(68, 24)
        Me.btnLogSave.TabIndex = 50
        Me.btnLogSave.Text = "&Save"
        Me.btnLogSave.UseVisualStyleBackColor = True
        '
        'btnLogClear
        '
        Me.btnLogClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogClear.Location = New System.Drawing.Point(86, 141)
        Me.btnLogClear.Name = "btnLogClear"
        Me.btnLogClear.Size = New System.Drawing.Size(68, 24)
        Me.btnLogClear.TabIndex = 49
        Me.btnLogClear.Text = "&Cls"
        Me.btnLogClear.UseVisualStyleBackColor = True
        '
        'picLog
        '
        Me.picLog.Image = CType(resources.GetObject("picLog.Image"), System.Drawing.Image)
        Me.picLog.Location = New System.Drawing.Point(47, 138)
        Me.picLog.Margin = New System.Windows.Forms.Padding(4)
        Me.picLog.Name = "picLog"
        Me.picLog.Size = New System.Drawing.Size(32, 32)
        Me.picLog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLog.TabIndex = 43
        Me.picLog.TabStop = False
        '
        'txtLog
        '
        Me.txtLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLog.Location = New System.Drawing.Point(6, 21)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(278, 114)
        Me.txtLog.TabIndex = 43
        Me.txtLog.TabStop = False
        '
        'grpTHPEnc
        '
        Me.grpTHPEnc.Controls.Add(Me.lblTE_jpgq)
        Me.grpTHPEnc.Controls.Add(Me.nudTE_jpgq)
        Me.grpTHPEnc.Controls.Add(Me.txtTE_D)
        Me.grpTHPEnc.Controls.Add(Me.txtTE_F)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_wav)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_Dum)
        Me.grpTHPEnc.Controls.Add(Me.lblTE_D)
        Me.grpTHPEnc.Controls.Add(Me.btnTE_Enc)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_A1)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_A2)
        Me.grpTHPEnc.Controls.Add(Me.lblTE_F)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_A3)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_A4)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_A5)
        Me.grpTHPEnc.Controls.Add(Me.txtTE_M)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_A6)
        Me.grpTHPEnc.Controls.Add(Me.lblTE_M)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_B1)
        Me.grpTHPEnc.Controls.Add(Me.lblTE_C)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_B2)
        Me.grpTHPEnc.Controls.Add(Me.lblTE_R)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_B3)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_B6)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_B4)
        Me.grpTHPEnc.Controls.Add(Me.chkTE_B5)
        Me.grpTHPEnc.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTHPEnc.Location = New System.Drawing.Point(340, 411)
        Me.grpTHPEnc.Name = "grpTHPEnc"
        Me.grpTHPEnc.Size = New System.Drawing.Size(228, 197)
        Me.grpTHPEnc.TabIndex = 42
        Me.grpTHPEnc.TabStop = False
        Me.grpTHPEnc.Text = "THP Encoder"
        '
        'lblTE_jpgq
        '
        Me.lblTE_jpgq.AutoSize = True
        Me.lblTE_jpgq.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTE_jpgq.Location = New System.Drawing.Point(145, 107)
        Me.lblTE_jpgq.Name = "lblTE_jpgq"
        Me.lblTE_jpgq.Size = New System.Drawing.Size(77, 17)
        Me.lblTE_jpgq.TabIndex = 47
        Me.lblTE_jpgq.Text = "JPG Qual"
        '
        'nudTE_jpgq
        '
        Me.nudTE_jpgq.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudTE_jpgq.Location = New System.Drawing.Point(170, 127)
        Me.nudTE_jpgq.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudTE_jpgq.Name = "nudTE_jpgq"
        Me.nudTE_jpgq.Size = New System.Drawing.Size(52, 22)
        Me.nudTE_jpgq.TabIndex = 46
        Me.nudTE_jpgq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudTE_jpgq.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'txtTE_D
        '
        Me.txtTE_D.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTE_D.Location = New System.Drawing.Point(202, 79)
        Me.txtTE_D.Mask = "0"
        Me.txtTE_D.Name = "txtTE_D"
        Me.txtTE_D.Size = New System.Drawing.Size(20, 22)
        Me.txtTE_D.TabIndex = 1
        '
        'txtTE_F
        '
        Me.txtTE_F.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTE_F.Location = New System.Drawing.Point(174, 52)
        Me.txtTE_F.Mask = "0000"
        Me.txtTE_F.Name = "txtTE_F"
        Me.txtTE_F.Size = New System.Drawing.Size(48, 22)
        Me.txtTE_F.TabIndex = 0
        '
        'chkTE_wav
        '
        Me.chkTE_wav.AutoSize = True
        Me.chkTE_wav.Font = New System.Drawing.Font("Microsoft Sans Serif", 4.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_wav.Location = New System.Drawing.Point(4, 148)
        Me.chkTE_wav.Name = "chkTE_wav"
        Me.chkTE_wav.Size = New System.Drawing.Size(39, 17)
        Me.chkTE_wav.TabIndex = 45
        Me.chkTE_wav.TabStop = False
        Me.chkTE_wav.Text = "wav"
        Me.chkTE_wav.UseVisualStyleBackColor = True
        '
        'chkTE_Dum
        '
        Me.chkTE_Dum.AutoSize = True
        Me.chkTE_Dum.Font = New System.Drawing.Font("Microsoft Sans Serif", 4.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_Dum.Location = New System.Drawing.Point(4, 174)
        Me.chkTE_Dum.Name = "chkTE_Dum"
        Me.chkTE_Dum.Size = New System.Drawing.Size(40, 17)
        Me.chkTE_Dum.TabIndex = 44
        Me.chkTE_Dum.TabStop = False
        Me.chkTE_Dum.Text = "dum"
        Me.chkTE_Dum.UseVisualStyleBackColor = True
        '
        'lblTE_D
        '
        Me.lblTE_D.AutoSize = True
        Me.lblTE_D.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTE_D.Location = New System.Drawing.Point(160, 82)
        Me.lblTE_D.Name = "lblTE_D"
        Me.lblTE_D.Size = New System.Drawing.Size(40, 17)
        Me.lblTE_D.TabIndex = 42
        Me.lblTE_D.Text = "Digs"
        '
        'btnTE_Enc
        '
        Me.btnTE_Enc.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTE_Enc.Location = New System.Drawing.Point(150, 153)
        Me.btnTE_Enc.Name = "btnTE_Enc"
        Me.btnTE_Enc.Size = New System.Drawing.Size(72, 41)
        Me.btnTE_Enc.TabIndex = 2
        Me.btnTE_Enc.Text = "&Encode"
        Me.btnTE_Enc.UseVisualStyleBackColor = True
        '
        'chkTE_A1
        '
        Me.chkTE_A1.AutoSize = True
        Me.chkTE_A1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_A1.Location = New System.Drawing.Point(47, 40)
        Me.chkTE_A1.Name = "chkTE_A1"
        Me.chkTE_A1.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_A1.TabIndex = 26
        Me.chkTE_A1.TabStop = False
        Me.chkTE_A1.Text = "A1"
        Me.chkTE_A1.UseVisualStyleBackColor = True
        '
        'chkTE_A2
        '
        Me.chkTE_A2.AutoSize = True
        Me.chkTE_A2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_A2.Location = New System.Drawing.Point(47, 65)
        Me.chkTE_A2.Name = "chkTE_A2"
        Me.chkTE_A2.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_A2.TabIndex = 27
        Me.chkTE_A2.TabStop = False
        Me.chkTE_A2.Text = "A2"
        Me.chkTE_A2.UseVisualStyleBackColor = True
        '
        'lblTE_F
        '
        Me.lblTE_F.AutoSize = True
        Me.lblTE_F.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTE_F.Location = New System.Drawing.Point(174, 15)
        Me.lblTE_F.Name = "lblTE_F"
        Me.lblTE_F.Size = New System.Drawing.Size(53, 34)
        Me.lblTE_F.TabIndex = 41
        Me.lblTE_F.Text = "Trunc" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Frame"
        '
        'chkTE_A3
        '
        Me.chkTE_A3.AutoSize = True
        Me.chkTE_A3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_A3.Location = New System.Drawing.Point(47, 92)
        Me.chkTE_A3.Name = "chkTE_A3"
        Me.chkTE_A3.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_A3.TabIndex = 28
        Me.chkTE_A3.TabStop = False
        Me.chkTE_A3.Text = "A3"
        Me.chkTE_A3.UseVisualStyleBackColor = True
        '
        'chkTE_A4
        '
        Me.chkTE_A4.AutoSize = True
        Me.chkTE_A4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_A4.Location = New System.Drawing.Point(47, 119)
        Me.chkTE_A4.Name = "chkTE_A4"
        Me.chkTE_A4.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_A4.TabIndex = 29
        Me.chkTE_A4.TabStop = False
        Me.chkTE_A4.Text = "A4"
        Me.chkTE_A4.UseVisualStyleBackColor = True
        '
        'chkTE_A5
        '
        Me.chkTE_A5.AutoSize = True
        Me.chkTE_A5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_A5.Location = New System.Drawing.Point(47, 146)
        Me.chkTE_A5.Name = "chkTE_A5"
        Me.chkTE_A5.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_A5.TabIndex = 30
        Me.chkTE_A5.TabStop = False
        Me.chkTE_A5.Text = "A5"
        Me.chkTE_A5.UseVisualStyleBackColor = True
        '
        'txtTE_M
        '
        Me.txtTE_M.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTE_M.Location = New System.Drawing.Point(4, 92)
        Me.txtTE_M.Multiline = True
        Me.txtTE_M.Name = "txtTE_M"
        Me.txtTE_M.ReadOnly = True
        Me.txtTE_M.Size = New System.Drawing.Size(34, 48)
        Me.txtTE_M.TabIndex = 33
        Me.txtTE_M.TabStop = False
        Me.txtTE_M.Text = "_1 to _4"
        '
        'chkTE_A6
        '
        Me.chkTE_A6.AutoSize = True
        Me.chkTE_A6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_A6.Location = New System.Drawing.Point(47, 173)
        Me.chkTE_A6.Name = "chkTE_A6"
        Me.chkTE_A6.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_A6.TabIndex = 31
        Me.chkTE_A6.TabStop = False
        Me.chkTE_A6.Text = "A6"
        Me.chkTE_A6.UseVisualStyleBackColor = True
        '
        'lblTE_M
        '
        Me.lblTE_M.AutoSize = True
        Me.lblTE_M.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTE_M.Location = New System.Drawing.Point(3, 66)
        Me.lblTE_M.Name = "lblTE_M"
        Me.lblTE_M.Size = New System.Drawing.Size(42, 17)
        Me.lblTE_M.TabIndex = 40
        Me.lblTE_M.Text = "Multi"
        '
        'chkTE_B1
        '
        Me.chkTE_B1.AutoSize = True
        Me.chkTE_B1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_B1.Location = New System.Drawing.Point(97, 38)
        Me.chkTE_B1.Name = "chkTE_B1"
        Me.chkTE_B1.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_B1.TabIndex = 32
        Me.chkTE_B1.TabStop = False
        Me.chkTE_B1.Text = "B1"
        Me.chkTE_B1.UseVisualStyleBackColor = True
        '
        'lblTE_C
        '
        Me.lblTE_C.AutoSize = True
        Me.lblTE_C.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTE_C.Location = New System.Drawing.Point(60, 18)
        Me.lblTE_C.Name = "lblTE_C"
        Me.lblTE_C.Size = New System.Drawing.Size(61, 17)
        Me.lblTE_C.TabIndex = 39
        Me.lblTE_C.Text = "Column"
        '
        'chkTE_B2
        '
        Me.chkTE_B2.AutoSize = True
        Me.chkTE_B2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_B2.Location = New System.Drawing.Point(97, 65)
        Me.chkTE_B2.Name = "chkTE_B2"
        Me.chkTE_B2.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_B2.TabIndex = 33
        Me.chkTE_B2.TabStop = False
        Me.chkTE_B2.Text = "B2"
        Me.chkTE_B2.UseVisualStyleBackColor = True
        '
        'lblTE_R
        '
        Me.lblTE_R.AutoSize = True
        Me.lblTE_R.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTE_R.Location = New System.Drawing.Point(3, 41)
        Me.lblTE_R.Name = "lblTE_R"
        Me.lblTE_R.Size = New System.Drawing.Size(38, 17)
        Me.lblTE_R.TabIndex = 38
        Me.lblTE_R.Text = "Row"
        '
        'chkTE_B3
        '
        Me.chkTE_B3.AutoSize = True
        Me.chkTE_B3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_B3.Location = New System.Drawing.Point(97, 92)
        Me.chkTE_B3.Name = "chkTE_B3"
        Me.chkTE_B3.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_B3.TabIndex = 34
        Me.chkTE_B3.TabStop = False
        Me.chkTE_B3.Text = "B3"
        Me.chkTE_B3.UseVisualStyleBackColor = True
        '
        'chkTE_B6
        '
        Me.chkTE_B6.AutoSize = True
        Me.chkTE_B6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_B6.Location = New System.Drawing.Point(97, 173)
        Me.chkTE_B6.Name = "chkTE_B6"
        Me.chkTE_B6.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_B6.TabIndex = 37
        Me.chkTE_B6.TabStop = False
        Me.chkTE_B6.Text = "B6"
        Me.chkTE_B6.UseVisualStyleBackColor = True
        '
        'chkTE_B4
        '
        Me.chkTE_B4.AutoSize = True
        Me.chkTE_B4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_B4.Location = New System.Drawing.Point(97, 119)
        Me.chkTE_B4.Name = "chkTE_B4"
        Me.chkTE_B4.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_B4.TabIndex = 35
        Me.chkTE_B4.TabStop = False
        Me.chkTE_B4.Text = "B4"
        Me.chkTE_B4.UseVisualStyleBackColor = True
        '
        'chkTE_B5
        '
        Me.chkTE_B5.AutoSize = True
        Me.chkTE_B5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTE_B5.Location = New System.Drawing.Point(97, 146)
        Me.chkTE_B5.Name = "chkTE_B5"
        Me.chkTE_B5.Size = New System.Drawing.Size(47, 21)
        Me.chkTE_B5.TabIndex = 36
        Me.chkTE_B5.TabStop = False
        Me.chkTE_B5.Text = "B5"
        Me.chkTE_B5.UseVisualStyleBackColor = True
        '
        'grpTHPDec
        '
        Me.grpTHPDec.Controls.Add(Me.grpTHPDec_Crop)
        Me.grpTHPDec.Controls.Add(Me.btnRip)
        Me.grpTHPDec.Controls.Add(Me.chkRip_DSound)
        Me.grpTHPDec.Controls.Add(Me.btnPlay)
        Me.grpTHPDec.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTHPDec.Location = New System.Drawing.Point(7, 411)
        Me.grpTHPDec.Name = "grpTHPDec"
        Me.grpTHPDec.Size = New System.Drawing.Size(327, 356)
        Me.grpTHPDec.TabIndex = 25
        Me.grpTHPDec.TabStop = False
        Me.grpTHPDec.Text = "THP Viewer/Ripper"
        '
        'grpTHPDec_Crop
        '
        Me.grpTHPDec_Crop.Controls.Add(Me.grpTHPDec_Settings)
        Me.grpTHPDec_Crop.Controls.Add(Me.grpTHPDec_Time)
        Me.grpTHPDec_Crop.Controls.Add(Me.grpTHPDec_Pre)
        Me.grpTHPDec_Crop.Location = New System.Drawing.Point(6, 48)
        Me.grpTHPDec_Crop.Name = "grpTHPDec_Crop"
        Me.grpTHPDec_Crop.Size = New System.Drawing.Size(315, 302)
        Me.grpTHPDec_Crop.TabIndex = 26
        Me.grpTHPDec_Crop.TabStop = False
        Me.grpTHPDec_Crop.Text = "Crop Settings"
        '
        'chkRipDumF
        '
        Me.chkRipDumF.AutoCheck = False
        Me.chkRipDumF.AutoSize = True
        Me.chkRipDumF.Enabled = False
        Me.chkRipDumF.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRipDumF.Location = New System.Drawing.Point(9, 87)
        Me.chkRipDumF.Name = "chkRipDumF"
        Me.chkRipDumF.Size = New System.Drawing.Size(77, 38)
        Me.chkRipDumF.TabIndex = 65
        Me.chkRipDumF.TabStop = False
        Me.chkRipDumF.Text = "Dum" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Frames"
        Me.chkRipDumF.UseVisualStyleBackColor = True
        '
        'radTD_All
        '
        Me.radTD_All.AutoSize = True
        Me.radTD_All.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_All.Location = New System.Drawing.Point(115, 102)
        Me.radTD_All.Name = "radTD_All"
        Me.radTD_All.Size = New System.Drawing.Size(44, 21)
        Me.radTD_All.TabIndex = 63
        Me.radTD_All.TabStop = True
        Me.radTD_All.Text = "All"
        Me.radTD_All.UseVisualStyleBackColor = True
        '
        'radTD_Dum
        '
        Me.radTD_Dum.AutoSize = True
        Me.radTD_Dum.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_Dum.Location = New System.Drawing.Point(116, 129)
        Me.radTD_Dum.Name = "radTD_Dum"
        Me.radTD_Dum.Size = New System.Drawing.Size(58, 21)
        Me.radTD_Dum.TabIndex = 62
        Me.radTD_Dum.TabStop = True
        Me.radTD_Dum.Text = "Dum"
        Me.radTD_Dum.UseVisualStyleBackColor = True
        '
        'txtTD_FE
        '
        Me.txtTD_FE.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTD_FE.Location = New System.Drawing.Point(174, 76)
        Me.txtTD_FE.Mask = "0000"
        Me.txtTD_FE.Name = "txtTD_FE"
        Me.txtTD_FE.Size = New System.Drawing.Size(46, 22)
        Me.txtTD_FE.TabIndex = 32
        '
        'txtTD_FS
        '
        Me.txtTD_FS.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTD_FS.Location = New System.Drawing.Point(67, 76)
        Me.txtTD_FS.Mask = "0000"
        Me.txtTD_FS.Name = "txtTD_FS"
        Me.txtTD_FS.Size = New System.Drawing.Size(46, 22)
        Me.txtTD_FS.TabIndex = 31
        '
        'lblTD_FE
        '
        Me.lblTD_FE.AutoSize = True
        Me.lblTD_FE.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_FE.Location = New System.Drawing.Point(133, 79)
        Me.lblTD_FE.Name = "lblTD_FE"
        Me.lblTD_FE.Size = New System.Drawing.Size(41, 17)
        Me.lblTD_FE.TabIndex = 33
        Me.lblTD_FE.Text = "End:"
        '
        'lblTD_FS
        '
        Me.lblTD_FS.AutoSize = True
        Me.lblTD_FS.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_FS.Location = New System.Drawing.Point(16, 79)
        Me.lblTD_FS.Name = "lblTD_FS"
        Me.lblTD_FS.Size = New System.Drawing.Size(48, 17)
        Me.lblTD_FS.TabIndex = 30
        Me.lblTD_FS.Text = "Start:"
        '
        'radTD_B6
        '
        Me.radTD_B6.AutoSize = True
        Me.radTD_B6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_B6.Location = New System.Drawing.Point(60, 156)
        Me.radTD_B6.Name = "radTD_B6"
        Me.radTD_B6.Size = New System.Drawing.Size(46, 21)
        Me.radTD_B6.TabIndex = 61
        Me.radTD_B6.TabStop = True
        Me.radTD_B6.Text = "B6"
        Me.radTD_B6.UseVisualStyleBackColor = True
        '
        'txtTD_CH
        '
        Me.txtTD_CH.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTD_CH.Location = New System.Drawing.Point(174, 52)
        Me.txtTD_CH.Mask = "0000"
        Me.txtTD_CH.Name = "txtTD_CH"
        Me.txtTD_CH.Size = New System.Drawing.Size(46, 22)
        Me.txtTD_CH.TabIndex = 3
        '
        'radTD_B5
        '
        Me.radTD_B5.AutoSize = True
        Me.radTD_B5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_B5.Location = New System.Drawing.Point(60, 129)
        Me.radTD_B5.Name = "radTD_B5"
        Me.radTD_B5.Size = New System.Drawing.Size(46, 21)
        Me.radTD_B5.TabIndex = 60
        Me.radTD_B5.TabStop = True
        Me.radTD_B5.Text = "B5"
        Me.radTD_B5.UseVisualStyleBackColor = True
        '
        'txtTD_CY
        '
        Me.txtTD_CY.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTD_CY.Location = New System.Drawing.Point(174, 24)
        Me.txtTD_CY.Mask = "0000"
        Me.txtTD_CY.Name = "txtTD_CY"
        Me.txtTD_CY.Size = New System.Drawing.Size(46, 22)
        Me.txtTD_CY.TabIndex = 1
        '
        'chkRipM
        '
        Me.chkRipM.AutoCheck = False
        Me.chkRipM.AutoSize = True
        Me.chkRipM.Enabled = False
        Me.chkRipM.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRipM.Location = New System.Drawing.Point(9, 66)
        Me.chkRipM.Name = "chkRipM"
        Me.chkRipM.Size = New System.Drawing.Size(69, 21)
        Me.chkRipM.TabIndex = 48
        Me.chkRipM.TabStop = False
        Me.chkRipM.Text = "Single"
        Me.chkRipM.UseVisualStyleBackColor = True
        '
        'radTD_B4
        '
        Me.radTD_B4.AutoSize = True
        Me.radTD_B4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_B4.Location = New System.Drawing.Point(60, 102)
        Me.radTD_B4.Name = "radTD_B4"
        Me.radTD_B4.Size = New System.Drawing.Size(46, 21)
        Me.radTD_B4.TabIndex = 59
        Me.radTD_B4.TabStop = True
        Me.radTD_B4.Text = "B4"
        Me.radTD_B4.UseVisualStyleBackColor = True
        '
        'txtTD_CW
        '
        Me.txtTD_CW.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTD_CW.Location = New System.Drawing.Point(67, 52)
        Me.txtTD_CW.Mask = "0000"
        Me.txtTD_CW.Name = "txtTD_CW"
        Me.txtTD_CW.Size = New System.Drawing.Size(46, 22)
        Me.txtTD_CW.TabIndex = 2
        '
        'lblTD_FM
        '
        Me.lblTD_FM.AutoSize = True
        Me.lblTD_FM.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_FM.Location = New System.Drawing.Point(15, 20)
        Me.lblTD_FM.Name = "lblTD_FM"
        Me.lblTD_FM.Size = New System.Drawing.Size(38, 17)
        Me.lblTD_FM.TabIndex = 49
        Me.lblTD_FM.Text = "Mult"
        '
        'lblTD_CH
        '
        Me.lblTD_CH.AutoSize = True
        Me.lblTD_CH.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_CH.Location = New System.Drawing.Point(114, 55)
        Me.lblTD_CH.Name = "lblTD_CH"
        Me.lblTD_CH.Size = New System.Drawing.Size(60, 17)
        Me.lblTD_CH.TabIndex = 29
        Me.lblTD_CH.Text = "Height:"
        '
        'nudTD_M
        '
        Me.nudTD_M.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudTD_M.Location = New System.Drawing.Point(9, 40)
        Me.nudTD_M.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudTD_M.Name = "nudTD_M"
        Me.nudTD_M.Size = New System.Drawing.Size(52, 22)
        Me.nudTD_M.TabIndex = 48
        Me.nudTD_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudTD_M.Value = New Decimal(New Integer() {11, 0, 0, 0})
        '
        'radTD_B1
        '
        Me.radTD_B1.AutoSize = True
        Me.radTD_B1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_B1.Location = New System.Drawing.Point(60, 21)
        Me.radTD_B1.Name = "radTD_B1"
        Me.radTD_B1.Size = New System.Drawing.Size(46, 21)
        Me.radTD_B1.TabIndex = 56
        Me.radTD_B1.TabStop = True
        Me.radTD_B1.Text = "B1"
        Me.radTD_B1.UseVisualStyleBackColor = True
        '
        'txtTD_CX
        '
        Me.txtTD_CX.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTD_CX.Location = New System.Drawing.Point(67, 24)
        Me.txtTD_CX.Mask = "0000"
        Me.txtTD_CX.Name = "txtTD_CX"
        Me.txtTD_CX.Size = New System.Drawing.Size(46, 22)
        Me.txtTD_CX.TabIndex = 0
        '
        'radTD_A2
        '
        Me.radTD_A2.AutoSize = True
        Me.radTD_A2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_A2.Location = New System.Drawing.Point(6, 48)
        Me.radTD_A2.Name = "radTD_A2"
        Me.radTD_A2.Size = New System.Drawing.Size(46, 21)
        Me.radTD_A2.TabIndex = 51
        Me.radTD_A2.TabStop = True
        Me.radTD_A2.Text = "A2"
        Me.radTD_A2.UseVisualStyleBackColor = True
        '
        'radTD_A3
        '
        Me.radTD_A3.AutoSize = True
        Me.radTD_A3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_A3.Location = New System.Drawing.Point(6, 75)
        Me.radTD_A3.Name = "radTD_A3"
        Me.radTD_A3.Size = New System.Drawing.Size(46, 21)
        Me.radTD_A3.TabIndex = 52
        Me.radTD_A3.TabStop = True
        Me.radTD_A3.Text = "A3"
        Me.radTD_A3.UseVisualStyleBackColor = True
        '
        'lblTD_CY
        '
        Me.lblTD_CY.AutoSize = True
        Me.lblTD_CY.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_CY.Location = New System.Drawing.Point(119, 27)
        Me.lblTD_CY.Name = "lblTD_CY"
        Me.lblTD_CY.Size = New System.Drawing.Size(55, 17)
        Me.lblTD_CY.TabIndex = 28
        Me.lblTD_CY.Text = "Y-pos:"
        '
        'radTD_B3
        '
        Me.radTD_B3.AutoSize = True
        Me.radTD_B3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_B3.Location = New System.Drawing.Point(60, 75)
        Me.radTD_B3.Name = "radTD_B3"
        Me.radTD_B3.Size = New System.Drawing.Size(46, 21)
        Me.radTD_B3.TabIndex = 58
        Me.radTD_B3.TabStop = True
        Me.radTD_B3.Text = "B3"
        Me.radTD_B3.UseVisualStyleBackColor = True
        '
        'lblTD_CW
        '
        Me.lblTD_CW.AutoSize = True
        Me.lblTD_CW.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_CW.Location = New System.Drawing.Point(10, 55)
        Me.lblTD_CW.Name = "lblTD_CW"
        Me.lblTD_CW.Size = New System.Drawing.Size(54, 17)
        Me.lblTD_CW.TabIndex = 2
        Me.lblTD_CW.Text = "Width:"
        '
        'radTD_A1
        '
        Me.radTD_A1.AutoSize = True
        Me.radTD_A1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_A1.Location = New System.Drawing.Point(6, 21)
        Me.radTD_A1.Name = "radTD_A1"
        Me.radTD_A1.Size = New System.Drawing.Size(46, 21)
        Me.radTD_A1.TabIndex = 50
        Me.radTD_A1.TabStop = True
        Me.radTD_A1.Text = "A1"
        Me.radTD_A1.UseVisualStyleBackColor = True
        '
        'lblTD_CX
        '
        Me.lblTD_CX.AutoSize = True
        Me.lblTD_CX.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_CX.Location = New System.Drawing.Point(9, 27)
        Me.lblTD_CX.Name = "lblTD_CX"
        Me.lblTD_CX.Size = New System.Drawing.Size(55, 17)
        Me.lblTD_CX.TabIndex = 1
        Me.lblTD_CX.Text = "X-pos:"
        '
        'radTD_A4
        '
        Me.radTD_A4.AutoSize = True
        Me.radTD_A4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_A4.Location = New System.Drawing.Point(6, 102)
        Me.radTD_A4.Name = "radTD_A4"
        Me.radTD_A4.Size = New System.Drawing.Size(46, 21)
        Me.radTD_A4.TabIndex = 53
        Me.radTD_A4.TabStop = True
        Me.radTD_A4.Text = "A4"
        Me.radTD_A4.UseVisualStyleBackColor = True
        '
        'radTD_B2
        '
        Me.radTD_B2.AutoSize = True
        Me.radTD_B2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_B2.Location = New System.Drawing.Point(60, 48)
        Me.radTD_B2.Name = "radTD_B2"
        Me.radTD_B2.Size = New System.Drawing.Size(46, 21)
        Me.radTD_B2.TabIndex = 57
        Me.radTD_B2.TabStop = True
        Me.radTD_B2.Text = "B2"
        Me.radTD_B2.UseVisualStyleBackColor = True
        '
        'radTD_A5
        '
        Me.radTD_A5.AutoSize = True
        Me.radTD_A5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_A5.Location = New System.Drawing.Point(6, 129)
        Me.radTD_A5.Name = "radTD_A5"
        Me.radTD_A5.Size = New System.Drawing.Size(46, 21)
        Me.radTD_A5.TabIndex = 54
        Me.radTD_A5.TabStop = True
        Me.radTD_A5.Text = "A5"
        Me.radTD_A5.UseVisualStyleBackColor = True
        '
        'radTD_A6
        '
        Me.radTD_A6.AutoSize = True
        Me.radTD_A6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radTD_A6.Location = New System.Drawing.Point(6, 156)
        Me.radTD_A6.Name = "radTD_A6"
        Me.radTD_A6.Size = New System.Drawing.Size(46, 21)
        Me.radTD_A6.TabIndex = 55
        Me.radTD_A6.TabStop = True
        Me.radTD_A6.Text = "A6"
        Me.radTD_A6.UseVisualStyleBackColor = True
        '
        'btnRip
        '
        Me.btnRip.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRip.Location = New System.Drawing.Point(197, 25)
        Me.btnRip.Name = "btnRip"
        Me.btnRip.Size = New System.Drawing.Size(75, 24)
        Me.btnRip.TabIndex = 3
        Me.btnRip.Text = "&Rip"
        Me.btnRip.UseVisualStyleBackColor = True
        '
        'chkRip_DSound
        '
        Me.chkRip_DSound.AutoSize = True
        Me.chkRip_DSound.Checked = True
        Me.chkRip_DSound.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRip_DSound.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRip_DSound.Location = New System.Drawing.Point(5, 21)
        Me.chkRip_DSound.Name = "chkRip_DSound"
        Me.chkRip_DSound.Size = New System.Drawing.Size(116, 21)
        Me.chkRip_DSound.TabIndex = 0
        Me.chkRip_DSound.Text = "DirectSound?"
        Me.chkRip_DSound.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlay.Location = New System.Drawing.Point(122, 25)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(75, 24)
        Me.btnPlay.TabIndex = 2
        Me.btnPlay.Text = "&Play"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'lblTHPFile
        '
        Me.lblTHPFile.AutoSize = True
        Me.lblTHPFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTHPFile.Location = New System.Drawing.Point(4, 14)
        Me.lblTHPFile.Name = "lblTHPFile"
        Me.lblTHPFile.Size = New System.Drawing.Size(70, 17)
        Me.lblTHPFile.TabIndex = 0
        Me.lblTHPFile.Text = "THP File"
        '
        'grpTHPInfo
        '
        Me.grpTHPInfo.Controls.Add(Me.grpVideo)
        Me.grpTHPInfo.Controls.Add(Me.lblFDesc)
        Me.grpTHPInfo.Controls.Add(Me.grpAudio)
        Me.grpTHPInfo.Controls.Add(Me.txtFDesc)
        Me.grpTHPInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTHPInfo.Location = New System.Drawing.Point(7, 37)
        Me.grpTHPInfo.Name = "grpTHPInfo"
        Me.grpTHPInfo.Size = New System.Drawing.Size(563, 368)
        Me.grpTHPInfo.TabIndex = 0
        Me.grpTHPInfo.TabStop = False
        Me.grpTHPInfo.Text = "THP Info"
        '
        'grpVideo
        '
        Me.grpVideo.Controls.Add(Me.grpVTDims)
        Me.grpVideo.Controls.Add(Me.grpVPlayback)
        Me.grpVideo.Controls.Add(Me.grpVArrInfo)
        Me.grpVideo.Location = New System.Drawing.Point(21, 21)
        Me.grpVideo.Name = "grpVideo"
        Me.grpVideo.Size = New System.Drawing.Size(511, 240)
        Me.grpVideo.TabIndex = 0
        Me.grpVideo.TabStop = False
        Me.grpVideo.Text = "Video"
        '
        'grpVTDims
        '
        Me.grpVTDims.Controls.Add(Me.lblTDims_H)
        Me.grpVTDims.Controls.Add(Me.lblTDims_W)
        Me.grpVTDims.Controls.Add(Me.txtTDims_W)
        Me.grpVTDims.Controls.Add(Me.txtTDims_H)
        Me.grpVTDims.Location = New System.Drawing.Point(6, 36)
        Me.grpVTDims.Name = "grpVTDims"
        Me.grpVTDims.Size = New System.Drawing.Size(106, 78)
        Me.grpVTDims.TabIndex = 3
        Me.grpVTDims.TabStop = False
        Me.grpVTDims.Text = "Total dims"
        '
        'lblTDims_H
        '
        Me.lblTDims_H.AutoSize = True
        Me.lblTDims_H.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTDims_H.Location = New System.Drawing.Point(50, 18)
        Me.lblTDims_H.Name = "lblTDims_H"
        Me.lblTDims_H.Size = New System.Drawing.Size(49, 15)
        Me.lblTDims_H.TabIndex = 5
        Me.lblTDims_H.Text = "Height"
        '
        'lblTDims_W
        '
        Me.lblTDims_W.AutoSize = True
        Me.lblTDims_W.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTDims_W.Location = New System.Drawing.Point(6, 18)
        Me.lblTDims_W.Name = "lblTDims_W"
        Me.lblTDims_W.Size = New System.Drawing.Size(43, 15)
        Me.lblTDims_W.TabIndex = 4
        Me.lblTDims_W.Text = "Width"
        '
        'txtTDims_W
        '
        Me.txtTDims_W.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDims_W.Location = New System.Drawing.Point(6, 42)
        Me.txtTDims_W.Name = "txtTDims_W"
        Me.txtTDims_W.ReadOnly = True
        Me.txtTDims_W.Size = New System.Drawing.Size(41, 22)
        Me.txtTDims_W.TabIndex = 0
        Me.txtTDims_W.TabStop = False
        Me.txtTDims_W.Text = "1234"
        '
        'txtTDims_H
        '
        Me.txtTDims_H.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDims_H.Location = New System.Drawing.Point(53, 42)
        Me.txtTDims_H.Name = "txtTDims_H"
        Me.txtTDims_H.ReadOnly = True
        Me.txtTDims_H.Size = New System.Drawing.Size(41, 22)
        Me.txtTDims_H.TabIndex = 1
        Me.txtTDims_H.TabStop = False
        Me.txtTDims_H.Text = "1234"
        '
        'grpVPlayback
        '
        Me.grpVPlayback.Controls.Add(Me.gVCtrl)
        Me.grpVPlayback.Controls.Add(Me.grpVSubDims)
        Me.grpVPlayback.Controls.Add(Me.grpVFrames)
        Me.grpVPlayback.Location = New System.Drawing.Point(6, 122)
        Me.grpVPlayback.Name = "grpVPlayback"
        Me.grpVPlayback.Size = New System.Drawing.Size(498, 112)
        Me.grpVPlayback.TabIndex = 19
        Me.grpVPlayback.TabStop = False
        Me.grpVPlayback.Text = "Playback info"
        '
        'gVCtrl
        '
        Me.gVCtrl.Controls.Add(Me.lblVC_D)
        Me.gVCtrl.Controls.Add(Me.txtVC_D)
        Me.gVCtrl.Controls.Add(Me.txtVC_C)
        Me.gVCtrl.Controls.Add(Me.lblVC_C)
        Me.gVCtrl.Controls.Add(Me.txtVC_F)
        Me.gVCtrl.Controls.Add(Me.lblVC_F)
        Me.gVCtrl.Location = New System.Drawing.Point(292, 13)
        Me.gVCtrl.Name = "gVCtrl"
        Me.gVCtrl.Size = New System.Drawing.Size(200, 93)
        Me.gVCtrl.TabIndex = 20
        Me.gVCtrl.TabStop = False
        Me.gVCtrl.Text = "Control"
        '
        'lblVC_D
        '
        Me.lblVC_D.AutoSize = True
        Me.lblVC_D.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVC_D.Location = New System.Drawing.Point(102, 9)
        Me.lblVC_D.Name = "lblVC_D"
        Me.lblVC_D.Size = New System.Drawing.Size(77, 17)
        Me.lblVC_D.TabIndex = 15
        Me.lblVC_D.Text = "Ctrl desc."
        '
        'txtVC_D
        '
        Me.txtVC_D.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVC_D.Location = New System.Drawing.Point(88, 29)
        Me.txtVC_D.Multiline = True
        Me.txtVC_D.Name = "txtVC_D"
        Me.txtVC_D.ReadOnly = True
        Me.txtVC_D.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtVC_D.Size = New System.Drawing.Size(106, 54)
        Me.txtVC_D.TabIndex = 14
        Me.txtVC_D.TabStop = False
        '
        'txtVC_C
        '
        Me.txtVC_C.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVC_C.Location = New System.Drawing.Point(42, 52)
        Me.txtVC_C.Name = "txtVC_C"
        Me.txtVC_C.ReadOnly = True
        Me.txtVC_C.Size = New System.Drawing.Size(45, 22)
        Me.txtVC_C.TabIndex = 13
        Me.txtVC_C.TabStop = False
        Me.txtVC_C.Text = "False"
        '
        'lblVC_C
        '
        Me.lblVC_C.AutoSize = True
        Me.lblVC_C.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVC_C.Location = New System.Drawing.Point(2, 54)
        Me.lblVC_C.Name = "lblVC_C"
        Me.lblVC_C.Size = New System.Drawing.Size(42, 17)
        Me.lblVC_C.TabIndex = 12
        Me.lblVC_C.Text = "Ctrl?"
        '
        'txtVC_F
        '
        Me.txtVC_F.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVC_F.Location = New System.Drawing.Point(42, 29)
        Me.txtVC_F.Name = "txtVC_F"
        Me.txtVC_F.ReadOnly = True
        Me.txtVC_F.Size = New System.Drawing.Size(45, 22)
        Me.txtVC_F.TabIndex = 10
        Me.txtVC_F.TabStop = False
        Me.txtVC_F.Text = "12.34"
        '
        'lblVC_F
        '
        Me.lblVC_F.AutoSize = True
        Me.lblVC_F.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVC_F.Location = New System.Drawing.Point(6, 33)
        Me.lblVC_F.Name = "lblVC_F"
        Me.lblVC_F.Size = New System.Drawing.Size(37, 17)
        Me.lblVC_F.TabIndex = 11
        Me.lblVC_F.Text = "FPS"
        '
        'grpVSubDims
        '
        Me.grpVSubDims.Controls.Add(Me.lblVS_P)
        Me.grpVSubDims.Controls.Add(Me.txtVP_H)
        Me.grpVSubDims.Controls.Add(Me.txtVP_W)
        Me.grpVSubDims.Controls.Add(Me.lblVS_H)
        Me.grpVSubDims.Controls.Add(Me.txtVS_H)
        Me.grpVSubDims.Controls.Add(Me.lblVS_W)
        Me.grpVSubDims.Controls.Add(Me.txtVS_W)
        Me.grpVSubDims.Controls.Add(Me.lblVS_S)
        Me.grpVSubDims.Location = New System.Drawing.Point(6, 21)
        Me.grpVSubDims.Name = "grpVSubDims"
        Me.grpVSubDims.Size = New System.Drawing.Size(130, 85)
        Me.grpVSubDims.TabIndex = 17
        Me.grpVSubDims.TabStop = False
        Me.grpVSubDims.Text = "Subvid dims"
        '
        'lblVS_P
        '
        Me.lblVS_P.AutoSize = True
        Me.lblVS_P.Font = New System.Drawing.Font("Microsoft Sans Serif", 4.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVS_P.Location = New System.Drawing.Point(1, 63)
        Me.lblVS_P.Name = "lblVS_P"
        Me.lblVS_P.Size = New System.Drawing.Size(24, 12)
        Me.lblVS_P.TabIndex = 13
        Me.lblVS_P.Text = "Pad"
        '
        'txtVP_H
        '
        Me.txtVP_H.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVP_H.Location = New System.Drawing.Point(83, 63)
        Me.txtVP_H.Name = "txtVP_H"
        Me.txtVP_H.ReadOnly = True
        Me.txtVP_H.Size = New System.Drawing.Size(41, 22)
        Me.txtVP_H.TabIndex = 11
        Me.txtVP_H.TabStop = False
        Me.txtVP_H.Text = "1234"
        '
        'txtVP_W
        '
        Me.txtVP_W.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVP_W.Location = New System.Drawing.Point(36, 63)
        Me.txtVP_W.Name = "txtVP_W"
        Me.txtVP_W.ReadOnly = True
        Me.txtVP_W.Size = New System.Drawing.Size(41, 22)
        Me.txtVP_W.TabIndex = 10
        Me.txtVP_W.TabStop = False
        Me.txtVP_W.Text = "1234"
        '
        'lblVS_H
        '
        Me.lblVS_H.AutoSize = True
        Me.lblVS_H.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVS_H.Location = New System.Drawing.Point(80, 21)
        Me.lblVS_H.Name = "lblVS_H"
        Me.lblVS_H.Size = New System.Drawing.Size(43, 13)
        Me.lblVS_H.TabIndex = 9
        Me.lblVS_H.Text = "Height"
        '
        'txtVS_H
        '
        Me.txtVS_H.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVS_H.Location = New System.Drawing.Point(83, 38)
        Me.txtVS_H.Name = "txtVS_H"
        Me.txtVS_H.ReadOnly = True
        Me.txtVS_H.Size = New System.Drawing.Size(41, 22)
        Me.txtVS_H.TabIndex = 7
        Me.txtVS_H.TabStop = False
        Me.txtVS_H.Text = "1234"
        '
        'lblVS_W
        '
        Me.lblVS_W.AutoSize = True
        Me.lblVS_W.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVS_W.Location = New System.Drawing.Point(39, 22)
        Me.lblVS_W.Name = "lblVS_W"
        Me.lblVS_W.Size = New System.Drawing.Size(38, 13)
        Me.lblVS_W.TabIndex = 8
        Me.lblVS_W.Text = "Width"
        '
        'txtVS_W
        '
        Me.txtVS_W.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVS_W.Location = New System.Drawing.Point(36, 38)
        Me.txtVS_W.Name = "txtVS_W"
        Me.txtVS_W.ReadOnly = True
        Me.txtVS_W.Size = New System.Drawing.Size(41, 22)
        Me.txtVS_W.TabIndex = 6
        Me.txtVS_W.TabStop = False
        Me.txtVS_W.Text = "1234"
        '
        'lblVS_S
        '
        Me.lblVS_S.AutoSize = True
        Me.lblVS_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 4.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVS_S.Location = New System.Drawing.Point(1, 44)
        Me.lblVS_S.Name = "lblVS_S"
        Me.lblVS_S.Size = New System.Drawing.Size(31, 12)
        Me.lblVS_S.TabIndex = 12
        Me.lblVS_S.Text = "Subv"
        '
        'grpVFrames
        '
        Me.grpVFrames.Controls.Add(Me.lblVF_T)
        Me.grpVFrames.Controls.Add(Me.lblVF_S)
        Me.grpVFrames.Controls.Add(Me.txtVF_S)
        Me.grpVFrames.Controls.Add(Me.txtVF_T)
        Me.grpVFrames.Location = New System.Drawing.Point(139, 21)
        Me.grpVFrames.Name = "grpVFrames"
        Me.grpVFrames.Size = New System.Drawing.Size(149, 85)
        Me.grpVFrames.TabIndex = 18
        Me.grpVFrames.TabStop = False
        Me.grpVFrames.Text = "# of frames"
        '
        'lblVF_T
        '
        Me.lblVF_T.AutoSize = True
        Me.lblVF_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVF_T.Location = New System.Drawing.Point(92, 25)
        Me.lblVF_T.Name = "lblVF_T"
        Me.lblVF_T.Size = New System.Drawing.Size(45, 17)
        Me.lblVF_T.TabIndex = 9
        Me.lblVF_T.Text = "Total"
        '
        'lblVF_S
        '
        Me.lblVF_S.AutoSize = True
        Me.lblVF_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVF_S.Location = New System.Drawing.Point(3, 25)
        Me.lblVF_S.Name = "lblVF_S"
        Me.lblVF_S.Size = New System.Drawing.Size(91, 17)
        Me.lblVF_S.TabIndex = 8
        Me.lblVF_S.Text = "Subv (mult)"
        '
        'txtVF_S
        '
        Me.txtVF_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVF_S.Location = New System.Drawing.Point(28, 45)
        Me.txtVF_S.Name = "txtVF_S"
        Me.txtVF_S.ReadOnly = True
        Me.txtVF_S.Size = New System.Drawing.Size(53, 22)
        Me.txtVF_S.TabIndex = 6
        Me.txtVF_S.TabStop = False
        Me.txtVF_S.Text = "123456"
        '
        'txtVF_T
        '
        Me.txtVF_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVF_T.Location = New System.Drawing.Point(87, 45)
        Me.txtVF_T.Name = "txtVF_T"
        Me.txtVF_T.ReadOnly = True
        Me.txtVF_T.Size = New System.Drawing.Size(53, 22)
        Me.txtVF_T.TabIndex = 7
        Me.txtVF_T.TabStop = False
        Me.txtVF_T.Text = "123456"
        '
        'grpVArrInfo
        '
        Me.grpVArrInfo.Controls.Add(Me.grpVArr)
        Me.grpVArrInfo.Controls.Add(Me.gVMult)
        Me.grpVArrInfo.Location = New System.Drawing.Point(127, 15)
        Me.grpVArrInfo.Name = "grpVArrInfo"
        Me.grpVArrInfo.Size = New System.Drawing.Size(377, 106)
        Me.grpVArrInfo.TabIndex = 16
        Me.grpVArrInfo.TabStop = False
        Me.grpVArrInfo.Text = "Video array info"
        '
        'grpVArr
        '
        Me.grpVArr.Controls.Add(Me.lblArr_S)
        Me.grpVArr.Controls.Add(Me.txtArr_S)
        Me.grpVArr.Controls.Add(Me.txtArr_C)
        Me.grpVArr.Controls.Add(Me.txtArr_R)
        Me.grpVArr.Controls.Add(Me.lblArr_C)
        Me.grpVArr.Controls.Add(Me.lblArr_R)
        Me.grpVArr.Location = New System.Drawing.Point(46, 20)
        Me.grpVArr.Name = "grpVArr"
        Me.grpVArr.Size = New System.Drawing.Size(133, 80)
        Me.grpVArr.TabIndex = 9
        Me.grpVArr.TabStop = False
        Me.grpVArr.Text = "Array"
        '
        'lblArr_S
        '
        Me.lblArr_S.AutoSize = True
        Me.lblArr_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArr_S.Location = New System.Drawing.Point(79, 23)
        Me.lblArr_S.Name = "lblArr_S"
        Me.lblArr_S.Size = New System.Drawing.Size(44, 17)
        Me.lblArr_S.TabIndex = 12
        Me.lblArr_S.Text = "Subv"
        '
        'txtArr_S
        '
        Me.txtArr_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArr_S.Location = New System.Drawing.Point(82, 43)
        Me.txtArr_S.Name = "txtArr_S"
        Me.txtArr_S.ReadOnly = True
        Me.txtArr_S.Size = New System.Drawing.Size(29, 22)
        Me.txtArr_S.TabIndex = 0
        Me.txtArr_S.TabStop = False
        Me.txtArr_S.Text = "12"
        '
        'txtArr_C
        '
        Me.txtArr_C.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArr_C.Location = New System.Drawing.Point(47, 43)
        Me.txtArr_C.Name = "txtArr_C"
        Me.txtArr_C.ReadOnly = True
        Me.txtArr_C.Size = New System.Drawing.Size(29, 22)
        Me.txtArr_C.TabIndex = 8
        Me.txtArr_C.TabStop = False
        Me.txtArr_C.Text = "12"
        '
        'txtArr_R
        '
        Me.txtArr_R.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArr_R.Location = New System.Drawing.Point(12, 43)
        Me.txtArr_R.Name = "txtArr_R"
        Me.txtArr_R.ReadOnly = True
        Me.txtArr_R.Size = New System.Drawing.Size(29, 22)
        Me.txtArr_R.TabIndex = 2
        Me.txtArr_R.TabStop = False
        Me.txtArr_R.Text = "12"
        '
        'lblArr_C
        '
        Me.lblArr_C.AutoSize = True
        Me.lblArr_C.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArr_C.Location = New System.Drawing.Point(47, 23)
        Me.lblArr_C.Name = "lblArr_C"
        Me.lblArr_C.Size = New System.Drawing.Size(18, 17)
        Me.lblArr_C.TabIndex = 11
        Me.lblArr_C.Text = "C"
        '
        'lblArr_R
        '
        Me.lblArr_R.AutoSize = True
        Me.lblArr_R.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArr_R.Location = New System.Drawing.Point(9, 23)
        Me.lblArr_R.Name = "lblArr_R"
        Me.lblArr_R.Size = New System.Drawing.Size(19, 17)
        Me.lblArr_R.TabIndex = 10
        Me.lblArr_R.Text = "R"
        '
        'gVMult
        '
        Me.gVMult.Controls.Add(Me.lblVM_M)
        Me.gVMult.Controls.Add(Me.txtV_TSubs)
        Me.gVMult.Controls.Add(Me.txtVM_M)
        Me.gVMult.Controls.Add(Me.lblV_TSubs)
        Me.gVMult.Location = New System.Drawing.Point(185, 20)
        Me.gVMult.Name = "gVMult"
        Me.gVMult.Size = New System.Drawing.Size(148, 80)
        Me.gVMult.TabIndex = 10
        Me.gVMult.TabStop = False
        Me.gVMult.Text = "Subvideo Info"
        '
        'lblVM_M
        '
        Me.lblVM_M.AutoSize = True
        Me.lblVM_M.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVM_M.Location = New System.Drawing.Point(15, 23)
        Me.lblVM_M.Name = "lblVM_M"
        Me.lblVM_M.Size = New System.Drawing.Size(42, 17)
        Me.lblVM_M.TabIndex = 12
        Me.lblVM_M.Text = "Multi"
        '
        'txtV_TSubs
        '
        Me.txtV_TSubs.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtV_TSubs.Location = New System.Drawing.Point(74, 43)
        Me.txtV_TSubs.Name = "txtV_TSubs"
        Me.txtV_TSubs.ReadOnly = True
        Me.txtV_TSubs.Size = New System.Drawing.Size(43, 22)
        Me.txtV_TSubs.TabIndex = 15
        Me.txtV_TSubs.TabStop = False
        Me.txtV_TSubs.Text = "12"
        '
        'txtVM_M
        '
        Me.txtVM_M.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVM_M.Location = New System.Drawing.Point(18, 43)
        Me.txtVM_M.Name = "txtVM_M"
        Me.txtVM_M.ReadOnly = True
        Me.txtVM_M.Size = New System.Drawing.Size(25, 22)
        Me.txtVM_M.TabIndex = 11
        Me.txtVM_M.TabStop = False
        Me.txtVM_M.Text = "12"
        '
        'lblV_TSubs
        '
        Me.lblV_TSubs.AutoSize = True
        Me.lblV_TSubs.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblV_TSubs.Location = New System.Drawing.Point(58, 23)
        Me.lblV_TSubs.Name = "lblV_TSubs"
        Me.lblV_TSubs.Size = New System.Drawing.Size(78, 17)
        Me.lblV_TSubs.TabIndex = 15
        Me.lblV_TSubs.Text = "Tot. Subv"
        '
        'lblFDesc
        '
        Me.lblFDesc.AutoSize = True
        Me.lblFDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFDesc.Location = New System.Drawing.Point(309, 264)
        Me.lblFDesc.Name = "lblFDesc"
        Me.lblFDesc.Size = New System.Drawing.Size(116, 17)
        Me.lblFDesc.TabIndex = 22
        Me.lblFDesc.Text = "THP File Desc."
        '
        'grpAudio
        '
        Me.grpAudio.Controls.Add(Me.txtA_F)
        Me.grpAudio.Controls.Add(Me.lblA_F)
        Me.grpAudio.Controls.Add(Me.txtA_S)
        Me.grpAudio.Controls.Add(Me.lblA_S)
        Me.grpAudio.Controls.Add(Me.txtA_A)
        Me.grpAudio.Controls.Add(Me.lblA_A)
        Me.grpAudio.Location = New System.Drawing.Point(22, 267)
        Me.grpAudio.Name = "grpAudio"
        Me.grpAudio.Size = New System.Drawing.Size(200, 91)
        Me.grpAudio.TabIndex = 21
        Me.grpAudio.TabStop = False
        Me.grpAudio.Text = "Audio"
        '
        'txtA_F
        '
        Me.txtA_F.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtA_F.Location = New System.Drawing.Point(130, 52)
        Me.txtA_F.Name = "txtA_F"
        Me.txtA_F.ReadOnly = True
        Me.txtA_F.Size = New System.Drawing.Size(52, 22)
        Me.txtA_F.TabIndex = 5
        Me.txtA_F.TabStop = False
        Me.txtA_F.Text = "12345"
        '
        'lblA_F
        '
        Me.lblA_F.AutoSize = True
        Me.lblA_F.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblA_F.Location = New System.Drawing.Point(124, 32)
        Me.lblA_F.Name = "lblA_F"
        Me.lblA_F.Size = New System.Drawing.Size(67, 15)
        Me.lblA_F.TabIndex = 4
        Me.lblA_F.Text = "Freq (Hz)"
        '
        'txtA_S
        '
        Me.txtA_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtA_S.Location = New System.Drawing.Point(69, 52)
        Me.txtA_S.Name = "txtA_S"
        Me.txtA_S.ReadOnly = True
        Me.txtA_S.Size = New System.Drawing.Size(52, 22)
        Me.txtA_S.TabIndex = 3
        Me.txtA_S.TabStop = False
        Me.txtA_S.Text = "False"
        '
        'lblA_S
        '
        Me.lblA_S.AutoSize = True
        Me.lblA_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblA_S.Location = New System.Drawing.Point(66, 32)
        Me.lblA_S.Name = "lblA_S"
        Me.lblA_S.Size = New System.Drawing.Size(57, 15)
        Me.lblA_S.TabIndex = 2
        Me.lblA_S.Text = "Stereo?"
        '
        'txtA_A
        '
        Me.txtA_A.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtA_A.Location = New System.Drawing.Point(12, 52)
        Me.txtA_A.Name = "txtA_A"
        Me.txtA_A.ReadOnly = True
        Me.txtA_A.Size = New System.Drawing.Size(52, 22)
        Me.txtA_A.TabIndex = 1
        Me.txtA_A.TabStop = False
        Me.txtA_A.Text = "False"
        '
        'lblA_A
        '
        Me.lblA_A.AutoSize = True
        Me.lblA_A.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblA_A.Location = New System.Drawing.Point(12, 32)
        Me.lblA_A.Name = "lblA_A"
        Me.lblA_A.Size = New System.Drawing.Size(51, 15)
        Me.lblA_A.TabIndex = 0
        Me.lblA_A.Text = "Audio?"
        '
        'txtFDesc
        '
        Me.txtFDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFDesc.Location = New System.Drawing.Point(224, 284)
        Me.txtFDesc.Multiline = True
        Me.txtFDesc.Name = "txtFDesc"
        Me.txtFDesc.ReadOnly = True
        Me.txtFDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFDesc.Size = New System.Drawing.Size(305, 74)
        Me.txtFDesc.TabIndex = 16
        Me.txtFDesc.TabStop = False
        Me.txtFDesc.Text = resources.GetString("txtFDesc.Text")
        '
        'cmbTHP
        '
        Me.cmbTHP.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTHP.FormattingEnabled = True
        Me.cmbTHP.Location = New System.Drawing.Point(80, 11)
        Me.cmbTHP.Name = "cmbTHP"
        Me.cmbTHP.Size = New System.Drawing.Size(490, 24)
        Me.cmbTHP.TabIndex = 0
        Me.cmbTHP.Text = "============================="
        '
        'TabOptions
        '
        Me.TabOptions.Controls.Add(Me.grpHelp)
        Me.TabOptions.Controls.Add(Me.grpOptions)
        Me.TabOptions.Location = New System.Drawing.Point(4, 25)
        Me.TabOptions.Margin = New System.Windows.Forms.Padding(4)
        Me.TabOptions.Name = "TabOptions"
        Me.TabOptions.Padding = New System.Windows.Forms.Padding(4)
        Me.TabOptions.Size = New System.Drawing.Size(577, 951)
        Me.TabOptions.TabIndex = 0
        Me.TabOptions.Text = "Options"
        Me.TabOptions.UseVisualStyleBackColor = True
        '
        'grpHelp
        '
        Me.grpHelp.Controls.Add(Me.PictureBox2)
        Me.grpHelp.Controls.Add(Me.btnCmdline)
        Me.grpHelp.Controls.Add(Me.PictureBox1)
        Me.grpHelp.Controls.Add(Me.btnManual)
        Me.grpHelp.Controls.Add(Me.btnRelease)
        Me.grpHelp.Controls.Add(Me.btnWiki)
        Me.grpHelp.Controls.Add(Me.btnWeb)
        Me.grpHelp.Controls.Add(Me.btnAbout)
        Me.grpHelp.Location = New System.Drawing.Point(19, 309)
        Me.grpHelp.Name = "grpHelp"
        Me.grpHelp.Size = New System.Drawing.Size(540, 635)
        Me.grpHelp.TabIndex = 54
        Me.grpHelp.TabStop = False
        Me.grpHelp.Text = "Help/Resources"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(4, 22)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(273, 192)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 51
        Me.PictureBox2.TabStop = False
        '
        'btnCmdline
        '
        Me.btnCmdline.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCmdline.Location = New System.Drawing.Point(411, 148)
        Me.btnCmdline.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCmdline.Name = "btnCmdline"
        Me.btnCmdline.Size = New System.Drawing.Size(120, 56)
        Me.btnCmdline.TabIndex = 8
        Me.btnCmdline.Text = "&Cmdline"
        Me.btnCmdline.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(105, 272)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(324, 304)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 41
        Me.PictureBox1.TabStop = False
        '
        'btnManual
        '
        Me.btnManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManual.Location = New System.Drawing.Point(411, 21)
        Me.btnManual.Margin = New System.Windows.Forms.Padding(4)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(120, 56)
        Me.btnManual.TabIndex = 7
        Me.btnManual.Text = "Ma&nual (Github)"
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'btnRelease
        '
        Me.btnRelease.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRelease.Location = New System.Drawing.Point(411, 84)
        Me.btnRelease.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRelease.Name = "btnRelease"
        Me.btnRelease.Size = New System.Drawing.Size(120, 56)
        Me.btnRelease.TabIndex = 6
        Me.btnRelease.Text = "La&test Release"
        Me.btnRelease.UseVisualStyleBackColor = True
        '
        'btnWiki
        '
        Me.btnWiki.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWiki.Location = New System.Drawing.Point(282, 148)
        Me.btnWiki.Margin = New System.Windows.Forms.Padding(4)
        Me.btnWiki.Name = "btnWiki"
        Me.btnWiki.Size = New System.Drawing.Size(120, 56)
        Me.btnWiki.TabIndex = 5
        Me.btnWiki.Text = "M&KWiiki article"
        Me.btnWiki.UseVisualStyleBackColor = True
        '
        'btnWeb
        '
        Me.btnWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWeb.Location = New System.Drawing.Point(282, 21)
        Me.btnWeb.Name = "btnWeb"
        Me.btnWeb.Size = New System.Drawing.Size(120, 56)
        Me.btnWeb.TabIndex = 4
        Me.btnWeb.Text = "Web&page"
        Me.btnWeb.UseVisualStyleBackColor = True
        '
        'btnAbout
        '
        Me.btnAbout.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbout.Location = New System.Drawing.Point(282, 84)
        Me.btnAbout.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(120, 56)
        Me.btnAbout.TabIndex = 3
        Me.btnAbout.Text = "&About"
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'grpOptions
        '
        Me.grpOptions.Controls.Add(Me.txtDataDir)
        Me.grpOptions.Controls.Add(Me.btnDataDir)
        Me.grpOptions.Controls.Add(Me.lblDataDir)
        Me.grpOptions.Controls.Add(Me.chkLogFull)
        Me.grpOptions.Controls.Add(Me.lblRoot)
        Me.grpOptions.Controls.Add(Me.chkAudio)
        Me.grpOptions.Controls.Add(Me.picOptions)
        Me.grpOptions.Controls.Add(Me.chkEMusic)
        Me.grpOptions.Controls.Add(Me.txtRoot)
        Me.grpOptions.Controls.Add(Me.chkMsg)
        Me.grpOptions.Controls.Add(Me.btnBrowseRoot)
        Me.grpOptions.Controls.Add(Me.btnSaveSettings)
        Me.grpOptions.Controls.Add(Me.lblFFMPEG)
        Me.grpOptions.Controls.Add(Me.btnLoadSettings)
        Me.grpOptions.Controls.Add(Me.txtFFMPEG)
        Me.grpOptions.Controls.Add(Me.lblFFPlayTemp)
        Me.grpOptions.Controls.Add(Me.btnBrowseFFMPEG)
        Me.grpOptions.Controls.Add(Me.btnBrowseFFPlayTemp)
        Me.grpOptions.Controls.Add(Me.txtTHPConv)
        Me.grpOptions.Controls.Add(Me.txtFFPlayTemp)
        Me.grpOptions.Controls.Add(Me.btnBrowseTHPConv)
        Me.grpOptions.Controls.Add(Me.btniView)
        Me.grpOptions.Controls.Add(Me.txtiView)
        Me.grpOptions.Controls.Add(Me.lblTHPConv)
        Me.grpOptions.Controls.Add(Me.lbliView)
        Me.grpOptions.Location = New System.Drawing.Point(16, 7)
        Me.grpOptions.Name = "grpOptions"
        Me.grpOptions.Size = New System.Drawing.Size(543, 296)
        Me.grpOptions.TabIndex = 53
        Me.grpOptions.TabStop = False
        Me.grpOptions.Text = "Options"
        '
        'txtDataDir
        '
        Me.txtDataDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDataDir.Location = New System.Drawing.Point(108, 198)
        Me.txtDataDir.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDataDir.Name = "txtDataDir"
        Me.txtDataDir.ReadOnly = True
        Me.txtDataDir.Size = New System.Drawing.Size(324, 22)
        Me.txtDataDir.TabIndex = 55
        Me.txtDataDir.TabStop = False
        '
        'btnDataDir
        '
        Me.btnDataDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDataDir.Location = New System.Drawing.Point(440, 193)
        Me.btnDataDir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDataDir.Name = "btnDataDir"
        Me.btnDataDir.Size = New System.Drawing.Size(97, 27)
        Me.btnDataDir.TabIndex = 54
        Me.btnDataDir.Text = "Brows&e"
        Me.btnDataDir.UseVisualStyleBackColor = True
        '
        'lblDataDir
        '
        Me.lblDataDir.AutoSize = True
        Me.lblDataDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDataDir.Location = New System.Drawing.Point(3, 200)
        Me.lblDataDir.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDataDir.Name = "lblDataDir"
        Me.lblDataDir.Size = New System.Drawing.Size(99, 17)
        Me.lblDataDir.TabIndex = 56
        Me.lblDataDir.Text = "Data File Dir"
        '
        'chkLogFull
        '
        Me.chkLogFull.AutoSize = True
        Me.chkLogFull.Location = New System.Drawing.Point(75, 260)
        Me.chkLogFull.Name = "chkLogFull"
        Me.chkLogFull.Size = New System.Drawing.Size(96, 21)
        Me.chkLogFull.TabIndex = 53
        Me.chkLogFull.Text = "&Full Logs"
        Me.chkLogFull.UseVisualStyleBackColor = True
        '
        'lblRoot
        '
        Me.lblRoot.AutoSize = True
        Me.lblRoot.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoot.Location = New System.Drawing.Point(27, 20)
        Me.lblRoot.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRoot.Name = "lblRoot"
        Me.lblRoot.Size = New System.Drawing.Size(78, 17)
        Me.lblRoot.TabIndex = 0
        Me.lblRoot.Text = "THP Root"
        '
        'chkAudio
        '
        Me.chkAudio.AutoSize = True
        Me.chkAudio.Checked = True
        Me.chkAudio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAudio.Location = New System.Drawing.Point(403, 233)
        Me.chkAudio.Name = "chkAudio"
        Me.chkAudio.Size = New System.Drawing.Size(71, 21)
        Me.chkAudio.TabIndex = 52
        Me.chkAudio.Text = "A&udio"
        Me.chkAudio.UseVisualStyleBackColor = True
        '
        'picOptions
        '
        Me.picOptions.Image = CType(resources.GetObject("picOptions.Image"), System.Drawing.Image)
        Me.picOptions.Location = New System.Drawing.Point(5, 225)
        Me.picOptions.Margin = New System.Windows.Forms.Padding(4)
        Me.picOptions.Name = "picOptions"
        Me.picOptions.Size = New System.Drawing.Size(63, 56)
        Me.picOptions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picOptions.TabIndex = 26
        Me.picOptions.TabStop = False
        '
        'chkEMusic
        '
        Me.chkEMusic.AutoSize = True
        Me.chkEMusic.Checked = True
        Me.chkEMusic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEMusic.Location = New System.Drawing.Point(402, 260)
        Me.chkEMusic.Name = "chkEMusic"
        Me.chkEMusic.Size = New System.Drawing.Size(136, 21)
        Me.chkEMusic.TabIndex = 51
        Me.chkEMusic.Text = "Elevator &Music"
        Me.chkEMusic.UseVisualStyleBackColor = True
        '
        'txtRoot
        '
        Me.txtRoot.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoot.Location = New System.Drawing.Point(108, 17)
        Me.txtRoot.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRoot.Name = "txtRoot"
        Me.txtRoot.ReadOnly = True
        Me.txtRoot.Size = New System.Drawing.Size(324, 22)
        Me.txtRoot.TabIndex = 0
        Me.txtRoot.TabStop = False
        Me.txtRoot.Text = "C:\Users\Anthony\Downloads\Output"
        '
        'chkMsg
        '
        Me.chkMsg.AutoSize = True
        Me.chkMsg.Checked = True
        Me.chkMsg.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMsg.Location = New System.Drawing.Point(75, 233)
        Me.chkMsg.Name = "chkMsg"
        Me.chkMsg.Size = New System.Drawing.Size(124, 21)
        Me.chkMsg.TabIndex = 50
        Me.chkMsg.Text = "Less MsgBo&x"
        Me.chkMsg.UseVisualStyleBackColor = True
        '
        'btnBrowseRoot
        '
        Me.btnBrowseRoot.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseRoot.Location = New System.Drawing.Point(440, 12)
        Me.btnBrowseRoot.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowseRoot.Name = "btnBrowseRoot"
        Me.btnBrowseRoot.Size = New System.Drawing.Size(97, 27)
        Me.btnBrowseRoot.TabIndex = 0
        Me.btnBrowseRoot.Text = "&Browse"
        Me.btnBrowseRoot.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveSettings.Location = New System.Drawing.Point(286, 233)
        Me.btnSaveSettings.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(72, 56)
        Me.btnSaveSettings.TabIndex = 49
        Me.btnSaveSettings.Text = "Sa&ve Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'lblFFMPEG
        '
        Me.lblFFMPEG.AutoSize = True
        Me.lblFFMPEG.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFFMPEG.Location = New System.Drawing.Point(32, 46)
        Me.lblFFMPEG.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFFMPEG.Name = "lblFFMPEG"
        Me.lblFFMPEG.Size = New System.Drawing.Size(73, 34)
        Me.lblFFMPEG.TabIndex = 35
        Me.lblFFMPEG.Text = "FFMpeg" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Exe Root"
        '
        'btnLoadSettings
        '
        Me.btnLoadSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadSettings.Location = New System.Drawing.Point(206, 233)
        Me.btnLoadSettings.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLoadSettings.Name = "btnLoadSettings"
        Me.btnLoadSettings.Size = New System.Drawing.Size(72, 56)
        Me.btnLoadSettings.TabIndex = 48
        Me.btnLoadSettings.Text = "&Load Settings"
        Me.btnLoadSettings.UseVisualStyleBackColor = True
        '
        'txtFFMPEG
        '
        Me.txtFFMPEG.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFFMPEG.Location = New System.Drawing.Point(108, 58)
        Me.txtFFMPEG.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFFMPEG.Name = "txtFFMPEG"
        Me.txtFFMPEG.ReadOnly = True
        Me.txtFFMPEG.Size = New System.Drawing.Size(324, 22)
        Me.txtFFMPEG.TabIndex = 36
        Me.txtFFMPEG.TabStop = False
        Me.txtFFMPEG.Text = "C:\Program Files (x86)\Media\Video\ffmpeg\ffmpeg-20181015-c27c7b4-win64-static\bi" & _
            "n"
        '
        'lblFFPlayTemp
        '
        Me.lblFFPlayTemp.AutoSize = True
        Me.lblFFPlayTemp.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFFPlayTemp.Location = New System.Drawing.Point(34, 85)
        Me.lblFFPlayTemp.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFFPlayTemp.Name = "lblFFPlayTemp"
        Me.lblFFPlayTemp.Size = New System.Drawing.Size(66, 34)
        Me.lblFFPlayTemp.TabIndex = 47
        Me.lblFFPlayTemp.Text = "FFPlay" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "WorkDir"
        '
        'btnBrowseFFMPEG
        '
        Me.btnBrowseFFMPEG.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseFFMPEG.Location = New System.Drawing.Point(440, 53)
        Me.btnBrowseFFMPEG.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowseFFMPEG.Name = "btnBrowseFFMPEG"
        Me.btnBrowseFFMPEG.Size = New System.Drawing.Size(97, 27)
        Me.btnBrowseFFMPEG.TabIndex = 1
        Me.btnBrowseFFMPEG.Text = "B&rowse"
        Me.btnBrowseFFMPEG.UseVisualStyleBackColor = True
        '
        'btnBrowseFFPlayTemp
        '
        Me.btnBrowseFFPlayTemp.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseFFPlayTemp.Location = New System.Drawing.Point(440, 85)
        Me.btnBrowseFFPlayTemp.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowseFFPlayTemp.Name = "btnBrowseFFPlayTemp"
        Me.btnBrowseFFPlayTemp.Size = New System.Drawing.Size(97, 27)
        Me.btnBrowseFFPlayTemp.TabIndex = 45
        Me.btnBrowseFFPlayTemp.Text = "Br&owse"
        Me.btnBrowseFFPlayTemp.UseVisualStyleBackColor = True
        '
        'txtTHPConv
        '
        Me.txtTHPConv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTHPConv.Location = New System.Drawing.Point(108, 163)
        Me.txtTHPConv.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTHPConv.Name = "txtTHPConv"
        Me.txtTHPConv.ReadOnly = True
        Me.txtTHPConv.Size = New System.Drawing.Size(324, 22)
        Me.txtTHPConv.TabIndex = 38
        Me.txtTHPConv.TabStop = False
        Me.txtTHPConv.Text = "E:\GC_Wii\Hacking\Tools\Video\THPGUI\THP\THPConv.exe"
        '
        'txtFFPlayTemp
        '
        Me.txtFFPlayTemp.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFFPlayTemp.Location = New System.Drawing.Point(108, 90)
        Me.txtFFPlayTemp.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFFPlayTemp.Name = "txtFFPlayTemp"
        Me.txtFFPlayTemp.ReadOnly = True
        Me.txtFFPlayTemp.Size = New System.Drawing.Size(324, 22)
        Me.txtFFPlayTemp.TabIndex = 46
        Me.txtFFPlayTemp.TabStop = False
        Me.txtFFPlayTemp.Text = "C:\Users\Anthony\Downloads\Output"
        '
        'btnBrowseTHPConv
        '
        Me.btnBrowseTHPConv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseTHPConv.Location = New System.Drawing.Point(440, 158)
        Me.btnBrowseTHPConv.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowseTHPConv.Name = "btnBrowseTHPConv"
        Me.btnBrowseTHPConv.Size = New System.Drawing.Size(97, 27)
        Me.btnBrowseTHPConv.TabIndex = 2
        Me.btnBrowseTHPConv.Text = "Brow&se"
        Me.btnBrowseTHPConv.UseVisualStyleBackColor = True
        '
        'btniView
        '
        Me.btniView.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btniView.Location = New System.Drawing.Point(440, 123)
        Me.btniView.Margin = New System.Windows.Forms.Padding(4)
        Me.btniView.Name = "btniView"
        Me.btniView.Size = New System.Drawing.Size(97, 27)
        Me.btniView.TabIndex = 42
        Me.btniView.Text = "Bro&wse"
        Me.btniView.UseVisualStyleBackColor = True
        '
        'txtiView
        '
        Me.txtiView.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtiView.Location = New System.Drawing.Point(108, 128)
        Me.txtiView.Margin = New System.Windows.Forms.Padding(4)
        Me.txtiView.Name = "txtiView"
        Me.txtiView.ReadOnly = True
        Me.txtiView.Size = New System.Drawing.Size(324, 22)
        Me.txtiView.TabIndex = 44
        Me.txtiView.TabStop = False
        Me.txtiView.Text = "C:\Program Files (x86)\Accessories\IrfanView\i_view32.exe"
        '
        'lblTHPConv
        '
        Me.lblTHPConv.AutoSize = True
        Me.lblTHPConv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTHPConv.Location = New System.Drawing.Point(3, 165)
        Me.lblTHPConv.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTHPConv.Name = "lblTHPConv"
        Me.lblTHPConv.Size = New System.Drawing.Size(106, 17)
        Me.lblTHPConv.TabIndex = 40
        Me.lblTHPConv.Text = "THPConv Exe"
        '
        'lbliView
        '
        Me.lbliView.AutoSize = True
        Me.lbliView.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbliView.Location = New System.Drawing.Point(34, 131)
        Me.lbliView.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbliView.Name = "lbliView"
        Me.lbliView.Size = New System.Drawing.Size(72, 17)
        Me.lbliView.TabIndex = 43
        Me.lbliView.Text = "Irfanview"
        '
        'tabApp
        '
        Me.tabApp.Controls.Add(Me.TabTHP)
        Me.tabApp.Controls.Add(Me.TabOptions)
        Me.tabApp.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabApp.Location = New System.Drawing.Point(2, 1)
        Me.tabApp.Margin = New System.Windows.Forms.Padding(4)
        Me.tabApp.Name = "tabApp"
        Me.tabApp.SelectedIndex = 0
        Me.tabApp.Size = New System.Drawing.Size(585, 980)
        Me.tabApp.TabIndex = 0
        Me.tabApp.TabStop = False
        '
        'LoadFFMPEGRoot
        '
        Me.LoadFFMPEGRoot.Description = "Select the root exe directory for FFMpeg"
        Me.LoadFFMPEGRoot.ShowNewFolderButton = False
        '
        'LoadTHPConv
        '
        Me.LoadTHPConv.DefaultExt = "exe"
        Me.LoadTHPConv.FileName = "thpconv.exe"
        Me.LoadTHPConv.Filter = "THPConv Executable|thpconv.exe"
        Me.LoadTHPConv.InitialDirectory = "C:\Program Files (x86)"
        '
        'ofdRip
        '
        Me.ofdRip.CheckFileExists = False
        Me.ofdRip.DefaultExt = "mp4"
        Me.ofdRip.FileName = "Please select path and base file name for output"
        Me.ofdRip.Filter = "MP4 Video |*.mp4"
        '
        'ofdOutput
        '
        Me.ofdOutput.Description = "Select the directory with the appropriately named input files"
        Me.ofdOutput.ShowNewFolderButton = False
        '
        'LoadiView
        '
        Me.LoadiView.DefaultExt = "exe"
        Me.LoadiView.FileName = "i_view32.exe"
        Me.LoadiView.Filter = "Irfanview Executable|i_view32.exe"
        '
        'LoadFFPlayWork
        '
        Me.LoadFFPlayWork.Description = "Select a working directory for FFPlay playback"
        Me.LoadFFPlayWork.ShowNewFolderButton = False
        '
        'ofdLoadSettings
        '
        Me.ofdLoadSettings.DefaultExt = "ini"
        Me.ofdLoadSettings.FileName = "thwimp.ini"
        Me.ofdLoadSettings.Filter = "Thwimp Settings|*.ini"
        '
        'LoadDataDir
        '
        Me.LoadDataDir.Description = "Select the root directory for Data Files to use"
        Me.LoadDataDir.ShowNewFolderButton = False
        '
        'ofdSaveSettings
        '
        Me.ofdSaveSettings.CheckFileExists = False
        Me.ofdSaveSettings.DefaultExt = "ini"
        Me.ofdSaveSettings.FileName = "thwimp.ini"
        Me.ofdSaveSettings.Filter = "Thwimp Settings|*.ini"
        '
        'SaveLog
        '
        Me.SaveLog.CheckFileExists = False
        Me.SaveLog.DefaultExt = "log"
        Me.SaveLog.Filter = "Log file|*.log"
        '
        'grpTHPDec_Pre
        '
        Me.grpTHPDec_Pre.Controls.Add(Me.lblTD_SpPre)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_Dum)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_All)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_A1)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_A6)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_A5)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_B2)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_A4)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_B3)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_A3)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_A2)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_B6)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_B1)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_B4)
        Me.grpTHPDec_Pre.Controls.Add(Me.radTD_B5)
        Me.grpTHPDec_Pre.Location = New System.Drawing.Point(110, 7)
        Me.grpTHPDec_Pre.Name = "grpTHPDec_Pre"
        Me.grpTHPDec_Pre.Size = New System.Drawing.Size(196, 180)
        Me.grpTHPDec_Pre.TabIndex = 51
        Me.grpTHPDec_Pre.TabStop = False
        Me.grpTHPDec_Pre.Text = "Preset (Row/Column)"
        '
        'grpTHPDec_Time
        '
        Me.grpTHPDec_Time.Controls.Add(Me.lblTD_FM)
        Me.grpTHPDec_Time.Controls.Add(Me.nudTD_M)
        Me.grpTHPDec_Time.Controls.Add(Me.chkRipDumF)
        Me.grpTHPDec_Time.Controls.Add(Me.chkRipM)
        Me.grpTHPDec_Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTHPDec_Time.Location = New System.Drawing.Point(4, 40)
        Me.grpTHPDec_Time.Name = "grpTHPDec_Time"
        Me.grpTHPDec_Time.Size = New System.Drawing.Size(100, 133)
        Me.grpTHPDec_Time.TabIndex = 66
        Me.grpTHPDec_Time.TabStop = False
        Me.grpTHPDec_Time.Text = "Time/Info"
        '
        'grpTHPDec_Settings
        '
        Me.grpTHPDec_Settings.Controls.Add(Me.lblTD_CX)
        Me.grpTHPDec_Settings.Controls.Add(Me.lblTD_CW)
        Me.grpTHPDec_Settings.Controls.Add(Me.lblTD_CY)
        Me.grpTHPDec_Settings.Controls.Add(Me.txtTD_FE)
        Me.grpTHPDec_Settings.Controls.Add(Me.txtTD_CX)
        Me.grpTHPDec_Settings.Controls.Add(Me.txtTD_FS)
        Me.grpTHPDec_Settings.Controls.Add(Me.lblTD_CH)
        Me.grpTHPDec_Settings.Controls.Add(Me.lblTD_FE)
        Me.grpTHPDec_Settings.Controls.Add(Me.txtTD_CW)
        Me.grpTHPDec_Settings.Controls.Add(Me.lblTD_FS)
        Me.grpTHPDec_Settings.Controls.Add(Me.txtTD_CY)
        Me.grpTHPDec_Settings.Controls.Add(Me.txtTD_CH)
        Me.grpTHPDec_Settings.Location = New System.Drawing.Point(39, 186)
        Me.grpTHPDec_Settings.Name = "grpTHPDec_Settings"
        Me.grpTHPDec_Settings.Size = New System.Drawing.Size(232, 109)
        Me.grpTHPDec_Settings.TabIndex = 67
        Me.grpTHPDec_Settings.TabStop = False
        Me.grpTHPDec_Settings.Text = "Settings"
        '
        'lblTD_SpPre
        '
        Me.lblTD_SpPre.AutoSize = True
        Me.lblTD_SpPre.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTD_SpPre.Location = New System.Drawing.Point(112, 57)
        Me.lblTD_SpPre.Name = "lblTD_SpPre"
        Me.lblTD_SpPre.Size = New System.Drawing.Size(63, 34)
        Me.lblTD_SpPre.TabIndex = 48
        Me.lblTD_SpPre.Text = "Special" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Presets"
        Me.lblTD_SpPre.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 986)
        Me.Controls.Add(Me.tabApp)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.Text = "Thwimp"
        Me.TabTHP.ResumeLayout(False)
        Me.TabTHP.PerformLayout()
        Me.grpTHPEnc_Prog.ResumeLayout(False)
        Me.grpTHPEnc_Prog.PerformLayout()
        Me.grpLog.ResumeLayout(False)
        Me.grpLog.PerformLayout()
        CType(Me.picLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTHPEnc.ResumeLayout(False)
        Me.grpTHPEnc.PerformLayout()
        CType(Me.nudTE_jpgq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTHPDec.ResumeLayout(False)
        Me.grpTHPDec.PerformLayout()
        Me.grpTHPDec_Crop.ResumeLayout(False)
        CType(Me.nudTD_M, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTHPInfo.ResumeLayout(False)
        Me.grpTHPInfo.PerformLayout()
        Me.grpVideo.ResumeLayout(False)
        Me.grpVTDims.ResumeLayout(False)
        Me.grpVTDims.PerformLayout()
        Me.grpVPlayback.ResumeLayout(False)
        Me.gVCtrl.ResumeLayout(False)
        Me.gVCtrl.PerformLayout()
        Me.grpVSubDims.ResumeLayout(False)
        Me.grpVSubDims.PerformLayout()
        Me.grpVFrames.ResumeLayout(False)
        Me.grpVFrames.PerformLayout()
        Me.grpVArrInfo.ResumeLayout(False)
        Me.grpVArr.ResumeLayout(False)
        Me.grpVArr.PerformLayout()
        Me.gVMult.ResumeLayout(False)
        Me.gVMult.PerformLayout()
        Me.grpAudio.ResumeLayout(False)
        Me.grpAudio.PerformLayout()
        Me.TabOptions.ResumeLayout(False)
        Me.grpHelp.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOptions.ResumeLayout(False)
        Me.grpOptions.PerformLayout()
        CType(Me.picOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabApp.ResumeLayout(False)
        Me.grpTHPDec_Pre.ResumeLayout(False)
        Me.grpTHPDec_Pre.PerformLayout()
        Me.grpTHPDec_Time.ResumeLayout(False)
        Me.grpTHPDec_Time.PerformLayout()
        Me.grpTHPDec_Settings.ResumeLayout(False)
        Me.grpTHPDec_Settings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LoadTHPRoot As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents TabTHP As System.Windows.Forms.TabPage
    Friend WithEvents TabOptions As System.Windows.Forms.TabPage
    Friend WithEvents btnBrowseRoot As System.Windows.Forms.Button
    Friend WithEvents txtRoot As System.Windows.Forms.TextBox
    Friend WithEvents lblRoot As System.Windows.Forms.Label
    Friend WithEvents tabApp As System.Windows.Forms.TabControl
    Friend WithEvents picOptions As System.Windows.Forms.PictureBox
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents lblTHPConv As System.Windows.Forms.Label
    Friend WithEvents btnBrowseTHPConv As System.Windows.Forms.Button
    Friend WithEvents txtTHPConv As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseFFMPEG As System.Windows.Forms.Button
    Friend WithEvents txtFFMPEG As System.Windows.Forms.TextBox
    Friend WithEvents lblFFMPEG As System.Windows.Forms.Label
    Friend WithEvents cmbTHP As System.Windows.Forms.ComboBox
    Friend WithEvents LoadFFMPEGRoot As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents LoadTHPConv As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblTHPFile As System.Windows.Forms.Label
    Friend WithEvents grpTHPInfo As System.Windows.Forms.GroupBox
    Friend WithEvents grpVideo As System.Windows.Forms.GroupBox
    Friend WithEvents grpVTDims As System.Windows.Forms.GroupBox
    Friend WithEvents lblTDims_H As System.Windows.Forms.Label
    Friend WithEvents lblTDims_W As System.Windows.Forms.Label
    Friend WithEvents txtTDims_W As System.Windows.Forms.TextBox
    Friend WithEvents txtTDims_H As System.Windows.Forms.TextBox
    Friend WithEvents grpVPlayback As System.Windows.Forms.GroupBox
    Friend WithEvents gVCtrl As System.Windows.Forms.GroupBox
    Friend WithEvents lblVC_D As System.Windows.Forms.Label
    Friend WithEvents txtVC_D As System.Windows.Forms.TextBox
    Friend WithEvents txtVC_C As System.Windows.Forms.TextBox
    Friend WithEvents lblVC_C As System.Windows.Forms.Label
    Friend WithEvents txtVC_F As System.Windows.Forms.TextBox
    Friend WithEvents lblVC_F As System.Windows.Forms.Label
    Friend WithEvents grpVSubDims As System.Windows.Forms.GroupBox
    Friend WithEvents lblVS_H As System.Windows.Forms.Label
    Friend WithEvents txtVS_H As System.Windows.Forms.TextBox
    Friend WithEvents lblVS_W As System.Windows.Forms.Label
    Friend WithEvents txtVS_W As System.Windows.Forms.TextBox
    Friend WithEvents grpVFrames As System.Windows.Forms.GroupBox
    Friend WithEvents lblVF_T As System.Windows.Forms.Label
    Friend WithEvents lblVF_S As System.Windows.Forms.Label
    Friend WithEvents txtVF_S As System.Windows.Forms.TextBox
    Friend WithEvents txtVF_T As System.Windows.Forms.TextBox
    Friend WithEvents grpVArrInfo As System.Windows.Forms.GroupBox
    Friend WithEvents grpVArr As System.Windows.Forms.GroupBox
    Friend WithEvents lblArr_S As System.Windows.Forms.Label
    Friend WithEvents txtArr_S As System.Windows.Forms.TextBox
    Friend WithEvents txtArr_C As System.Windows.Forms.TextBox
    Friend WithEvents txtArr_R As System.Windows.Forms.TextBox
    Friend WithEvents lblArr_C As System.Windows.Forms.Label
    Friend WithEvents lblArr_R As System.Windows.Forms.Label
    Friend WithEvents txtV_TSubs As System.Windows.Forms.TextBox
    Friend WithEvents gVMult As System.Windows.Forms.GroupBox
    Friend WithEvents lblVM_M As System.Windows.Forms.Label
    Friend WithEvents txtVM_M As System.Windows.Forms.TextBox
    Friend WithEvents lblV_TSubs As System.Windows.Forms.Label
    Friend WithEvents lblFDesc As System.Windows.Forms.Label
    Friend WithEvents grpAudio As System.Windows.Forms.GroupBox
    Friend WithEvents txtA_F As System.Windows.Forms.TextBox
    Friend WithEvents lblA_F As System.Windows.Forms.Label
    Friend WithEvents txtA_S As System.Windows.Forms.TextBox
    Friend WithEvents lblA_S As System.Windows.Forms.Label
    Friend WithEvents txtA_A As System.Windows.Forms.TextBox
    Friend WithEvents lblA_A As System.Windows.Forms.Label
    Friend WithEvents txtFDesc As System.Windows.Forms.TextBox
    Friend WithEvents grpTHPDec As System.Windows.Forms.GroupBox
    Friend WithEvents grpTHPDec_Crop As System.Windows.Forms.GroupBox
    Friend WithEvents lblTD_CH As System.Windows.Forms.Label
    Friend WithEvents lblTD_CY As System.Windows.Forms.Label
    Friend WithEvents lblTD_CW As System.Windows.Forms.Label
    Friend WithEvents lblTD_CX As System.Windows.Forms.Label
    Friend WithEvents btnRip As System.Windows.Forms.Button
    Friend WithEvents chkRip_DSound As System.Windows.Forms.CheckBox
    Friend WithEvents btnPlay As System.Windows.Forms.Button
    Friend WithEvents txtTE_M As System.Windows.Forms.TextBox
    Friend WithEvents lblTE_M As System.Windows.Forms.Label
    Friend WithEvents lblTE_C As System.Windows.Forms.Label
    Friend WithEvents lblTE_R As System.Windows.Forms.Label
    Friend WithEvents chkTE_B6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_B5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_B4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_B3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_B2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_B1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_A6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_A5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_A4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_A3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_A2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTE_A1 As System.Windows.Forms.CheckBox
    Friend WithEvents grpTHPEnc As System.Windows.Forms.GroupBox
    Friend WithEvents btnTE_Enc As System.Windows.Forms.Button
    Friend WithEvents lblTE_F As System.Windows.Forms.Label
    Friend WithEvents lblVS_P As System.Windows.Forms.Label
    Friend WithEvents txtVP_H As System.Windows.Forms.TextBox
    Friend WithEvents txtVP_W As System.Windows.Forms.TextBox
    Friend WithEvents lblVS_S As System.Windows.Forms.Label
    Friend WithEvents ofdRip As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ofdOutput As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblTE_D As System.Windows.Forms.Label
    Friend WithEvents chkTE_Dum As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkTE_wav As System.Windows.Forms.CheckBox
    Friend WithEvents txtTE_D As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtTE_F As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtTD_CH As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtTD_CY As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtTD_CW As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtTD_CX As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btniView As System.Windows.Forms.Button
    Friend WithEvents txtiView As System.Windows.Forms.TextBox
    Friend WithEvents lbliView As System.Windows.Forms.Label
    Friend WithEvents lblTE_jpgq As System.Windows.Forms.Label
    Friend WithEvents nudTE_jpgq As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTD_FE As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtTD_FS As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTD_FE As System.Windows.Forms.Label
    Friend WithEvents lblTD_FS As System.Windows.Forms.Label
    Friend WithEvents lblTD_FM As System.Windows.Forms.Label
    Friend WithEvents nudTD_M As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkRipM As System.Windows.Forms.CheckBox
    Friend WithEvents radTD_B6 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_B5 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_B4 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_B1 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_A2 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_A3 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_B3 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_A1 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_A4 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_B2 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_A5 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_A6 As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_Dum As System.Windows.Forms.RadioButton
    Friend WithEvents radTD_All As System.Windows.Forms.RadioButton
    Friend WithEvents chkRipDumF As System.Windows.Forms.CheckBox
    Friend WithEvents lblFFPlayTemp As System.Windows.Forms.Label
    Friend WithEvents btnBrowseFFPlayTemp As System.Windows.Forms.Button
    Friend WithEvents txtFFPlayTemp As System.Windows.Forms.TextBox
    Friend WithEvents LoadFFPlayWork As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents grpTHPEnc_Prog As System.Windows.Forms.GroupBox
    Friend WithEvents txtTHPEnc_Prg_Cur As System.Windows.Forms.TextBox
    Friend WithEvents txtTHPEnc_Prg_Ttl As System.Windows.Forms.TextBox
    Friend WithEvents prgCur As System.Windows.Forms.ProgressBar
    Friend WithEvents prgTotal As System.Windows.Forms.ProgressBar
    Friend WithEvents picLog As System.Windows.Forms.PictureBox
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents grpLog As System.Windows.Forms.GroupBox
    Friend WithEvents lblTHPEnc_Prg_Cur As System.Windows.Forms.Label
    Friend WithEvents lblTHPEnc_Prg_Ttl As System.Windows.Forms.Label
    Friend WithEvents chkMsg As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents btnLoadSettings As System.Windows.Forms.Button
    Friend WithEvents chkEMusic As System.Windows.Forms.CheckBox
    Friend WithEvents btnLogClear As System.Windows.Forms.Button
    Friend WithEvents chkAudio As System.Windows.Forms.CheckBox
    Friend WithEvents grpHelp As System.Windows.Forms.GroupBox
    Friend WithEvents btnWeb As System.Windows.Forms.Button
    Friend WithEvents grpOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkLogFull As System.Windows.Forms.CheckBox
    Friend WithEvents btnCmdline As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnRelease As System.Windows.Forms.Button
    Friend WithEvents btnWiki As System.Windows.Forms.Button
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnLogSave As System.Windows.Forms.Button
    Friend WithEvents ofdLoadSettings As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtDataDir As System.Windows.Forms.TextBox
    Friend WithEvents btnDataDir As System.Windows.Forms.Button
    Friend WithEvents lblDataDir As System.Windows.Forms.Label
    Friend WithEvents LoadDataDir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ofdSaveSettings As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LoadiView As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveLog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents grpTHPDec_Pre As System.Windows.Forms.GroupBox
    Friend WithEvents grpTHPDec_Settings As System.Windows.Forms.GroupBox
    Friend WithEvents grpTHPDec_Time As System.Windows.Forms.GroupBox
    Friend WithEvents lblTD_SpPre As System.Windows.Forms.Label
End Class