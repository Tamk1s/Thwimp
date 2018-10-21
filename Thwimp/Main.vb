Imports System.IO
Imports System.Runtime.InteropServices

Public Class Main

    'Global constants

    'Characters
    Shared strBAK As String = "\"                           'Backslash symbol
    Shared strQUOT As String = Chr(34)                      'Quote Symbol

    Shared strPATH As String = Application.StartupPath      'Get the directory of the executable
    Const LISTING As String = "FileListing.txt"             'File containing the    file listing for BreakGOLD's image files
    Const CDESC As String = "FileCDesc.txt"                   '~                    description info for the control signal
    Const DESC As String = "FileDesc.txt"                   '~                      description info for the image files
    Const DATA As String = "FileData.txt"                   '~                      hard-coded image data for the image files

    Const exeFMPeg As String = "ffmpeg.exe"
    Const exeFPlay As String = "ffplay.exe"

    Shared strFMPackPath As String = ""
    Shared strThpConvPath As String = ""

    'Image data
    Shared THPs(255) As THPData                           'Array containing all of the THPData
    Const BADENTRY As SByte = -2                            'Flag for signifying that the data for empty THPs is invalid (prevents error)

    'Generic structure for image dims (width and height)
    Structure Dims
        Dim width As UShort
        Dim height As UShort
    End Structure

    'Structure for THP Video array info
    Structure THPArr
        Dim row As Byte             'Amount of  rows
        Dim col As Byte             '~          cols
        Dim subV As Byte            '           subvideos (=rows*cols)
        Dim mult As Byte            '           Multiplicity for each cell
        Dim multOpt As Boolean      'Is multiplicity optional?
        Dim subVT As Byte           'Total amount of subvidoes (subvideos*multiplicity)
    End Structure

    'Generic structure for frame info
    Structure Frame
        Dim subframes As UShort     'Frames in a subvideo
        Dim totframes As UShort     'Total frames for THP file
    End Structure

    'Audio info for THP files
    Structure Audio
        Dim has As Boolean          'Does THP have audio stream?
        Dim Stereo As Boolean       'Is audio stereo?
        Dim freq As UShort          'Frequency of audio stream (Hz)
    End Structure

    'Video info for THP files
    Structure Video
        Dim TDim As Dims            'Total dimensions for THP file
        Dim THPinfo As THPArr       'THP array info
        Dim SDim As Dims            'Dimensions for subvideo
        Dim Frames As Frame         'Frames in video (sub/whole video)
        Dim Padding As Dims         'Dimenions of the padding (if any)
        Dim FPS As Single           'FPS of file
        Dim Ctrl As Boolean         'Does THP have a control signal in the padding?
        Dim CDesc As String         'Description of the ctrl signal
    End Structure

    'Structure representing THP info
    Structure THPData
        Dim visual As Video                                 'Video data of THP
        Dim audial As Audio                                 'Audio data of THP
        Dim Desc As String                                  'Desc: Description of usage of file (in words)
        Dim File As String                                  'File: Directory of the file, relative to the THP root
    End Structure
    '========================

    'APP Setup code

    'Application setup code
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Initialize the application with some setup code

        'Disable form elements in THP tab (until the data in the "Options" tab is filled out)
        'lblTHPFile.Visible = False
        'cmbTHP.Visible = False
        'grpTHPInfo.Visible = False
        'grpTHPDec.Visible = False
        'grpTHPEnc.Visible = False
        'grpTHPDec_Crop.Visible = False
        InitTHPData()
    End Sub

    'Subroutine to retrieve the hard-coded THP data from the four files
    Public Sub InitTHPData()

        Dim xFileData As StreamReader   'Streamreader object for reading the data from the files
        Dim strEntry As String          'String data read from the [File].ReadLine() method
        Dim bytItems As Byte            'Number of items in the parallel arrays
        Dim bytCtr1 As Byte             'Generic Counter variable #1
        Dim bytCtr2 As Byte             'Generic Counter variable #2

        'Variables for processing the DATA file
        Const SEP As String = ","       'Constant variable for the ASCII character separating the sub entries per entry
        Const SEP2 As String = ";"      'Constant variable ~                        ending the entries
        Dim strVar As String            'String containing stringified numeric data (for processing the entries in the DATA file)
        Dim bytStart As Byte            'Start position in the string for extracting a subentry
        Dim bytEnd As Byte              'End position ~
        Dim bytLen As Byte              'The length of the string to extract
        Const bytDataEnt As Byte = 19
        'Dim bytErrFlag As Byte          'Counts the number of invalid entries, if = 5 bad entries, mark .BPP as BADENTRY to prevent errors

        bytItems = 0
        xFileData = File.OpenText(strPATH & strBAK & LISTING)   'Open the LISTING file

        'Count the amount of items
        While xFileData.EndOfStream() <> True
            strEntry = xFileData.ReadLine()                     'Read a line from the file
            bytItems += 1
        End While
        ReDim THPs(bytItems)                                  'Redim the array appropriately

        'Close the LISTING file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing

        'Load all relative file paths into THPs(#).File
        xFileData = File.OpenText(strPATH & strBAK & LISTING)   'Open the LISTING file
        For bytCtr1 = 1 To bytItems Step 1
            strEntry = xFileData.ReadLine()                     'Read a line
            THPs(bytCtr1).File = strEntry                     'Dump paths into appropriate array entry
            cmbTHP.Items.Add(THPs(bytCtr1).File)            'Dump data into Combo box
        Next bytCtr1

        'Close the LISTING file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing

        'Load all descriptions into THPs(#).Desc array
        xFileData = File.OpenText(strPATH & strBAK & DESC)      'Open the DESC file
        For bytCtr1 = 1 To bytItems Step 1
            strEntry = xFileData.ReadLine()                     'Read a line
            THPs(bytCtr1).Desc = strEntry                     'Dump the data into the array slots
        Next bytCtr1
        'Close the DESC file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing

        'Load all ctrl descriptions into THPs(#).visual.cdesc array
        xFileData = File.OpenText(strPATH & strBAK & CDESC)      'Open the CDESC file
        For bytCtr1 = 1 To bytItems Step 1
            strEntry = xFileData.ReadLine()                     'Read a line
            THPs(bytCtr1).visual.CDesc = strEntry
        Next bytCtr1
        'Close the DESC file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing

        '-------------------------
        'Get data from the DATA file

        'Load all AV data into THPs(#).visual and THPs(#).audial
        xFileData = File.OpenText(strPATH & strBAK & DATA)      'Load the DATA file
        For bytCtr1 = 1 To bytItems Step 1
            strEntry = xFileData.ReadLine()                     'Read a line

            bytStart = 1                                        'Init start pos
            'Parse each of the 19 data pieces for each line
            For bytCtr2 = 1 To bytDataEnt Step 1

                'If not the last data entry in line, find the position of the SEP character (,), else SEP2 character (;)
                If bytCtr2 <> bytDataEnt Then bytEnd = InStr(bytStart, strEntry, SEP) Else bytEnd = InStr(bytStart, strEntry, SEP2) 'Record the position of the next SEP1 character,
                bytLen = (bytEnd - bytStart)             'Get the length of the sub entry (subtract End from Start)
                strVar = Mid(strEntry, bytStart, bytLen) 'Extract the sub entry via MID command
                'If Val(strVar) = 0 Then bytErrFlag += 1 'If the entry is 0, increment error counter

                'Allocate the extracted value into the appropriate array data fields
                Select Case bytCtr2
                    Case 1
                        THPs(bytCtr1).visual.TDim.width = Val(strVar)
                    Case 2
                        THPs(bytCtr1).visual.TDim.height = Val(strVar)

                    Case 3
                        THPs(bytCtr1).visual.THPinfo.row = Val(strVar)
                    Case 4
                        THPs(bytCtr1).visual.THPinfo.col = Val(strVar)
                    Case 5
                        THPs(bytCtr1).visual.THPinfo.subV = Val(strVar)
                    Case 6
                        THPs(bytCtr1).visual.THPinfo.mult = Val(strVar)
                    Case 7
                        THPs(bytCtr1).visual.THPinfo.multOpt = BitToBool(Val(strVar))
                    Case 8
                        THPs(bytCtr1).visual.THPinfo.subVT = Val(strVar)

                    Case 9
                        THPs(bytCtr1).visual.SDim.width = Val(strVar)
                    Case 10
                        THPs(bytCtr1).visual.SDim.height = Val(strVar)
                    Case 11
                        THPs(bytCtr1).visual.Frames.subframes = Val(strVar)
                    Case 12
                        THPs(bytCtr1).visual.Frames.totframes = Val(strVar)
                    Case 13
                        THPs(bytCtr1).visual.Padding.width = Val(strVar)
                    Case 14
                        THPs(bytCtr1).visual.Padding.height = Val(strVar)
                    Case 15
                        THPs(bytCtr1).visual.FPS = Val(strVar)

                    Case 16
                        THPs(bytCtr1).visual.Ctrl = BitToBool(Val(strVar))
                    Case 17
                        THPs(bytCtr1).audial.has = BitToBool(Val(strVar))
                    Case 18
                        THPs(bytCtr1).audial.Stereo = BitToBool(Val(strVar))
                    Case 19
                        THPs(bytCtr1).audial.freq = Val(strVar)
                End Select

                bytStart = bytEnd + 1 'Increment the start position to 1 past the located SEP1 character
            Next bytCtr2 'Repeat for the other subentries in the line
        Next bytCtr1 'Repeat for all entries

        'Close the DATA file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing
    End Sub

    '===========================
    'Options Tab stuff

    'Handles clicking of About button
    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbout.Click
        My.Computer.Audio.Play(My.Resources.EagleSoft, AudioPlayMode.Background)    'Play "EagleSoft Ltd"
        About.ShowDialog()  'Show the about box
    End Sub

    'Subroutine to load the root directory (with ofd)
    Private Sub btnLoadRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseRoot.Click
        'Load the LoadBreakGOLDDir Load Dialog Box, user selects root directory of BreakGOLD
        If LoadTHPRoot.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        txtRoot.Text = LoadTHPRoot.SelectedPath    'Dump the path into the textbox, for later retrieval
        CheckPathsSet()
    End Sub
    Private Sub btnBrowseFFMpeg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFFMpeg.Click
        'Load the LoadBreakGOLDDir Load Dialog Box, user selects root directory of BreakGOLD
        If LoadFMPegRoot.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        txtFFMpeg.Text = LoadFMPegRoot.SelectedPath    'Dump the path into the textbox, for later retrieval
        CheckPathsSet()
    End Sub
    Private Sub btnBrowseTHPConv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseTHPConv.Click
        'Load the LoadiView ofd, user selects iview exe
        If LoadTHPConv.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'Dump the path into the textbox, for later retrieval
        txtTHPConv.Text = LoadTHPConv.FileName
        CheckPathsSet()
    End Sub

    Private Sub CheckPathsSet()
        If (txtRoot.Text <> Nothing) And (txtFFMpeg.Text <> Nothing) And (txtTHPConv.Text <> Nothing) Then
            'Make everything in the THP tab visible now
            lblTHPFile.Visible = True
            cmbTHP.Visible = True
            grpTHPInfo.Visible = True
        End If
    End Sub

    Private Sub cmbTHP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTHP.SelectedIndexChanged
        Dim bytEntry As Byte = (cmbTHP.SelectedIndex) + 1     'The array entry ID#
        Dim bytNum As Byte                                      'The amount of SubTHPs in the file

        'Set THPEnc and THPDec group boxes to true on runonce
        lblTE_D.Visible = True
        grpTHPDec.Visible = True

        'Update stats for current THP
        txtTDims_W.Text = THPs(bytEntry).visual.TDim.width.ToString()
        txtTDims_H.Text = THPs(bytEntry).visual.TDim.height.ToString()

        txtArr_R.Text = THPs(bytEntry).visual.THPinfo.row.ToString()
        txtArr_C.Text = THPs(bytEntry).visual.THPinfo.col.ToString()
        txtArr_S.Text = THPs(bytEntry).visual.THPinfo.subV.ToString()

        txtVM_M.Text = THPs(bytEntry).visual.THPinfo.mult.ToString()
        txtVM_O.Text = THPs(bytEntry).visual.THPinfo.multOpt.ToString()

        txtV_TSubs.Text = THPs(bytEntry).visual.THPinfo.subVT.ToString()

        txtVS_W.Text = THPs(bytEntry).visual.SDim.width.ToString()
        txtVS_H.Text = THPs(bytEntry).visual.SDim.height.ToString()
        txtVP_W.Text = THPs(bytEntry).visual.Padding.width.ToString()
        txtVP_H.Text = THPs(bytEntry).visual.Padding.height.ToString()

        txtVF_S.Text = THPs(bytEntry).visual.Frames.subframes.ToString()
        txtVF_T.Text = THPs(bytEntry).visual.Frames.totframes.ToString()

        txtVC_F.Text = THPs(bytEntry).visual.FPS.ToString("f")
        txtA_A.Text = THPs(bytEntry).audial.has.ToString
        txtA_S.Text = THPs(bytEntry).audial.Stereo.ToString
        txtA_F.Text = THPs(bytEntry).audial.freq.ToString
        txtVC_C.Text = THPs(bytEntry).visual.Ctrl.ToString()
        txtVC_D.Text = THPs(bytEntry).visual.CDesc

        txtFDesc.Text = THPs(bytEntry).Desc

        'Prepare THPDec field
        txtTD_CX.Text = 0
        txtTD_CY.Text = 0
        txtTD_CW.Text = THPs(bytEntry).visual.TDim.width.ToString()
        txtTD_CH.Text = THPs(bytEntry).visual.TDim.height.ToString()
        txtTE_D.Text = txtVF_T.Text.Length.ToString()
        HandleArrState()
    End Sub

    Private Sub HandleArrState()
        'Prepare THPEnc field
        Dim Boxes(6, 2) As System.Windows.Forms.CheckBox
        Dim Dum As System.Windows.Forms.CheckBox
        Boxes(1, 1) = chkTE_A1
        Boxes(2, 1) = chkTE_A2
        Boxes(3, 1) = chkTE_A3
        Boxes(4, 1) = chkTE_A4
        Boxes(5, 1) = chkTE_A5
        Boxes(6, 1) = chkTE_A6
        Boxes(1, 2) = chkTE_B1
        Boxes(2, 2) = chkTE_B2
        Boxes(3, 2) = chkTE_B3
        Boxes(4, 2) = chkTE_B4
        Boxes(5, 2) = chkTE_B5
        Boxes(6, 2) = chkTE_B6
        Dum = chkTE_Dum

        Dim i As Byte = 0
        Dim j As Byte = 0
        Dim r As Byte = Val(txtArr_R.Text)
        Dim c As Byte = Val(txtArr_C.Text)
        Dim m As Byte = Val(txtVM_M.Text)
        For i = 1 To 6 Step 1
            For j = 1 To 2 Step 1
                If i = 0 And j = 0 Then
                    Boxes(i, j).Checked = False
                    Boxes(i, j).Enabled = False
                Else
                    If i <= r And j <= c Then
                        Boxes(i, j).Checked = True
                        Boxes(i, j).Enabled = True
                    Else
                        Boxes(i, j).Checked = False
                        Boxes(i, j).Enabled = False
                    End If
                End If
            Next
        Next

        Dim d As Dims
        d.width = Val(txtVP_W.Text)
        d.height = Val(txtVP_H.Text)
        If d.width <> 0 And d.height <> 0 Then
            Dum.Checked = True
            Dum.Enabled = True
        Else
            Dum.Checked = False
            Dum.Enabled = False
        End If

        If m = 1 Then
            txtTE_M.Text = "_1"
        Else
            txtTE_M.Text = "_1" & Environment.NewLine & "to" & Environment.NewLine & "_" & m.ToString()
        End If
        txtTE_F.Text = txtVF_S.Text
    End Sub

    '---------------------
    'DOS file path functions
    'For the following functions, pass a complete filename path strPath (ie: "C:\Windows\aFile.ext")
    'They all pass back a string with DOS file paths

    'Function gets directory of file from file path
    Public Function FileDir(ByVal strPath As String) As String
        Dim strFile As String  'The file itself (ie, File.ext)
        strFile = FileAndExt(strPath)   'Get the file+extension
        'From the full file path, replace the file+ext with nothing, to get file directory
        FileDir = Replace(strPath, strFile, vbNullChar)
    End Function

    'Function changes the file extension
    Public Function FileChangeExt(ByVal strPath As String, ByVal strOldExt As String, ByVal strNewExt As String)
        'Get the file+ext from the file path, replace old extension with new extension
        FileChangeExt = Replace(FileAndExt(strPath), strOldExt, strNewExt)
    End Function

    'Function gets the file+extension from a file path
    Public Function FileAndExt(ByVal strPath As String) As String
        Dim shtPos(255) As UShort   'The recorded positions of the strBAK character(s) in strPath
        Dim shtStart As UShort      'The start position inside strPath
        Dim blnFound As Boolean     'Flag which determines if a strBAK character was found
        Dim bytItems As Byte        'The amount of strBAK characters found

        Dim shtLen As UShort        'The length of the strPath
        Dim shtFileLen As UShort    'The length of the file+ext

        bytItems = 1
        blnFound = False
        shtStart = 1

        Do
            shtPos(bytItems) = InStr(shtStart, strPath, strBAK) 'Find the next strBAK character, record its position in array
            If shtPos(bytItems) <> 0 Then
                'If strBAK is found
                blnFound = True
                shtStart = shtPos(bytItems) + 1 'Set shtStart to one past the position of the found strBAK character
                bytItems += 1                   'Increment the amount of strBAK characters found
            Else
                'if strBAK NOT found, trigger flag to exit loop
                blnFound = False
            End If
        Loop Until blnFound = False 'Loop until strBAK is NOT found

        shtLen = Len(strPath) 'Get the length of the filepath
        shtFileLen = shtLen - shtPos(bytItems - 1)  'Set the length of the file+ext
        FileAndExt = Mid(strPath, (shtPos(bytItems - 1)) + 1, shtFileLen) 'Extract the file+ext
    End Function

    Private Function BitToBool(ByVal inp As Byte) As Boolean
        Dim outp As Boolean = False
        If inp = 1 Then outp = True
        Return outp
    End Function

    Private Sub ChkString(ByVal T As String, ByVal F As String, ByRef box As System.Windows.Forms.CheckBox)
        Dim v As String
        If box.Checked = True Then
            v = T
        Else
            v = F
        End If
        box.Text = v
    End Sub

    Private Sub ChkStringTri(ByVal Unchk As String, ByVal Indet As String, ByVal Chk As String, ByRef Box As System.Windows.Forms.CheckBox)
        Dim v As String = ""
        If Box.CheckState = CheckState.Unchecked Then
            v = Unchk
        ElseIf Box.CheckState = CheckState.Checked Then
            v = Chk
        Else
            v = Indet
        End If
        Box.Text = v
    End Sub

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click        
        Dim startInfo As ProcessStartInfo
        startInfo = New ProcessStartInfo
        If chkDSound.Checked = True Then
            'https://social.msdn.microsoft.com/Forums/vstudio/en-US/a18210d7-44f4-4895-8bcc-d3d1d26719e5/setting-environment-variable-from-vbnet?forum=netfxbcl
            'set SDL_AUDIODRIVER=directsound            
            startInfo.EnvironmentVariables("SDL_AUDIODRIVER") = "directsound"
        End If

        Dim cmd As String = strQUOT & txtFFMpeg.Text & strBAK & exeFPlay & strQUOT
        Dim args As String = strQUOT & txtRoot.Text & cmbTHP.Text & strQUOT
        startInfo.FileName = cmd & args
        startInfo.UseShellExecute = False

        Dim shell As Process
        shell = New Process
        shell.StartInfo = startInfo
        shell.Start()
        shell.WaitForExit()
        shell.Close()
    End Sub

    Private Sub btnRip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRip.Click
        'Load the ofdRip, user selects path/base filename
        Dim inFile As String = txtRoot.Text & cmbTHP.Text
        Dim initDir As String = FileDir(inFile)
        Dim newFile As String = FileAndExt(cmbTHP.Text)
        newFile = newFile.Replace(".thp", "")

        ofdRip.InitialDirectory = initDir
        ofdRip.FileName = newFile
        If ofdRip.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim outFile As String = ofdRip.FileName
        Dim outPath As String = FileDir(outFile)
        Dim outFilename As String = FileAndExt(outFile)

        'https://video.stackexchange.com/questions/4563/how-can-i-crop-a-video-with-ffmpeg
        'Video Conv: ffmpeg -i input_video.mp4 output.avi
        'Video Crop: ffmpeg -i in.mp4 -filter:v "crop=out_w:out_h:x:y" out.mp4

        'https://www.bugcodemaster.com/article/extract-audio-video-using-ffmpeg
        'Audio Ext: ffmpeg -i input_video.mp4 -vn output_audio.mp3

        'https://forums.creativecow.net/docs/forums/post.php?forumid=291&postid=219&univpostid=219&pview=t
        'ffmpeg -i input.mov -pix_fmt yuvj422p -acodec pcm -vcodec rawvideo -y output.avi        

        Dim cmd As String = ""
        Dim x As String = txtTD_CX.Text
        Dim y As String = txtTD_CY.Text
        Dim w As String = txtTD_CW.Text
        Dim h As String = txtTD_CH.Text

        'AVI
        'Extract video as AVI
        cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT
        '-i input.thp -pix_fmt yuvj422p -vcodec rawvideo -y -filter:v "crop=out_w:out_h:x:y" output.avi
        cmd &= " -i " & strQUOT & inFile & strQUOT & " -pix_fmt yuvj422p -vcodec rawvideo -y -filter:v " & strQUOT & "crop=" & w & ":" & h & ":" & x & ":" & y & strQUOT & " " & strQUOT & outFile & strQUOT

        Dim startInfo As ProcessStartInfo
        startInfo = New ProcessStartInfo        
        startInfo.FileName = cmd
        startInfo.UseShellExecute = False        
        Dim shell As Process
        shell = New Process
        shell.StartInfo = startInfo
        shell.Start()
        shell.WaitForExit()

        'Extract audio as wav (if any)
        Dim has As String = txtVC_C.Text
        Dim hasAudio As Boolean = False
        If has = "True" Then
            hasAudio = True
        End If

        If hasAudio Then
            If chkDSound.Checked = True Then
                'set SDL_AUDIODRIVER=directsound
                startInfo.EnvironmentVariables("SDL_AUDIODRIVER") = "directsound"
            End If
            cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT
            cmd &= " -i " & strQUOT & inFile & strQUOT & " -vn " & strQUOT & outFile.Replace(".avi", ".wav") & strQUOT
            startInfo.FileName = cmd
            shell.StartInfo = startInfo
            shell.Start()
            shell.WaitForExit()            
        End If
        shell.Close()
    End Sub

    Private Sub chkTE_A1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_A1.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_A2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_A2.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_A3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_A3.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_A4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_A4.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_A5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_A5.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_A6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_A6.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_B1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_B1.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_B2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_B2.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_B3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_B3.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_B4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_B4.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_B5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_B5.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_B6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_B6.CheckedChanged
        HandleArrState()
    End Sub
    Private Sub chkTE_Dum_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_Dum.CheckedChanged
        HandleArrState()
    End Sub

    Private Sub btnTE_Enc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTE_Enc.Click
        'https://unix.stackexchange.com/questions/233832/merge-two-video-clips-into-one-placing-them-next-to-each-other
        'ffmpeg -i top.mp4 -i bot.mp4 -filter_complex vstack output.mp4
        'ffmpeg -i left.mp4 -i right.mp4 -filter_complex hstack output.mp4

        'https://stackoverflow.com/questions/11552565/vertically-or-horizontally-stack-several-videos-using-ffmpeg/33764934#33764934
        'ffmpeg -i input0 -i input1 -i input2 -filter_complex "[0:v][1:v][2:v]vstack=inputs=3[v]" -map "[v]" output

        'https://stackoverflow.com/questions/7333232/how-to-concatenate-two-mp4-files-using-ffmpeg
        'ffmpeg -i opening.mkv -i episode.mkv -i ending.mkv -filter_complex concat output.mkv"

        If ofdOutput.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim startInfo As ProcessStartInfo
        startInfo = New ProcessStartInfo        
        startInfo.UseShellExecute = False        
        Dim shell As Process
        shell = New Process

        Dim path As String = ofdOutput.SelectedPath
        Dim filename As String = FileAndExt(cmbTHP.Text)
        Dim file As String = ""
        Dim file2 As String = ""
        filename = filename.Replace(".thp", "")
        Dim cmd As String = ""

        Dim i As Byte = 1
        Dim j As Byte = 1
        Dim k As Byte = 1
        Dim r As Byte = Val(txtArr_R.Text)
        Dim c As Byte = Val(txtArr_C.Text)
        Dim m As Byte = Val(txtVM_M.Text)
        Dim suffix As String
        Dim frames As String = txtTE_F.Text
        Dim parms(6) As String
        Dim parm As String
        Dim d As Dims
        Dim max As UShort
        Dim suffixes(6, 2) As String
        suffixes(1, 1) = "_A1"
        suffixes(2, 1) = "_A2"
        suffixes(3, 1) = "_A3"
        suffixes(4, 1) = "_A4"
        suffixes(5, 1) = "_A5"
        suffixes(6, 1) = "_A6"
        suffixes(1, 2) = "_B1"
        suffixes(2, 2) = "_B2"
        suffixes(3, 2) = "_B3"
        suffixes(4, 2) = "_B4"
        suffixes(5, 2) = "_B5"
        suffixes(6, 2) = "_B6"
        Dim Files(6) As String        
        Dim hasPad As Boolean = False

        d.width = Val(txtVP_W.Text)
        d.height = Val(txtVP_H.Text)
        If d.width <> 0 And d.height <> 0 Then
            hasPad = True
        End If

        CleanUp(path, filename, r, c, m, hasPad, False)

        For k = 1 To m Step 1
            If hasPad Then
                'If THP has some dummy/control frames, create a video from the dummy image for future h/vstacking                
                'Copy image file to duplicate frames
                max = Val(txtTE_F.Text)
                Dim dg As Byte = txtTE_F.Text.Length
                Dim dgs As String = StrDup(dg, "0")
                Dim cnt As UShort = 0
                For cnt = 1 To max
                    file = path & strBAK & "dummy_" & k.ToString() & ".bmp"
                    file2 = path & strBAK & "dummy_" & k.ToString() & "_" & cnt.ToString(dgs) & ".bmp"
                    My.Computer.FileSystem.CopyFile(file, file2)
                Next cnt

                'ffmpeg -f image2 -framerate FPS -i image_%03d.bmp test.avi
                cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y"
                Dim rate As Single = Val(txtVC_F.Text)
                cmd &= " -f image2 -framerate " & rate
                file = strQUOT & path & strBAK & "dummy_" & k.ToString() & "_%0" & dg.ToString() & "d.bmp" & strQUOT
                cmd &= " -i " & file
                file = strQUOT & path & strBAK & "dummy_" & k.ToString() & ".avi" & strQUOT
                cmd &= " " & file
                startInfo.FileName = cmd
                shell.StartInfo = startInfo
                shell.Start()
                shell.WaitForExit()
                CleanUp(path, filename, r, c, m, hasPad, True)
            End If

            For j = 1 To c Step 1
                'ffmpeg -i input0 -i input1 -i input2 -filter_complex "[0:v][1:v][2:v]vstack=inputs=3[v]" -map "[v]" output
                parm = ""
                ReDim parms(r)
                cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y "

                For i = 1 To r Step 1
                    suffix = suffixes(i, j) & "_" & k.ToString()
                    file = strQUOT & path & strBAK & filename & suffix & ".avi" & strQUOT

                    cmd &= "-i " & file
                    cmd &= " "
                    parms(i) = "[" & (i - 1).ToString() + ":v]"
                    parm &= parms(i)
                Next i
                file = strQUOT & path & strBAK & "c" & j.ToString() & ".avi" & strQUOT
                If r > 1 Then
                    cmd &= "-filter_complex " & strQUOT
                    cmd &= parm & "vstack=inputs=" & r.ToString() & "[v]" & strQUOT & " -map " & strQUOT & "[v]" & strQUOT
                    cmd &= " " & file
                Else
                    cmd &= file
                End If

                startInfo.FileName = cmd
                shell.StartInfo = startInfo
                shell.Start()
                shell.WaitForExit()
            Next j

            '!@ This doesn't always truncate frames? (Test with battle_retro.thp)
            For j = 1 To c Step 1
                cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y "
                file = strQUOT & path & strBAK & "c" & j.ToString() & ".avi" & strQUOT
                cmd &= "-i " & file
                file = strQUOT & path & strBAK & "d" & j.ToString() & ".avi" & strQUOT
                cmd &= " -filter_complex trim=start_frame=0:end_frame=" & frames & " " & file
                startInfo.FileName = cmd
                shell.StartInfo = startInfo
                shell.Start()
                shell.WaitForExit()
            Next j

            parm = ""
            ReDim parms(c)
            cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y "
            For j = 1 To c Step 1
                file = strQUOT & path & strBAK & "d" & j.ToString() & ".avi" & strQUOT
                cmd &= "-i " & file
                cmd &= " "
                parms(j) = "[" & (j - 1).ToString() + ":v]"
                parm &= parms(j)
            Next j
            file = strQUOT & path & strBAK & "m" & k.ToString() & ".avi" & strQUOT

            If c > 1 Then
                cmd &= "-filter_complex " & strQUOT
                cmd &= parm & "hstack=inputs=" & c.ToString() & "[v]" & strQUOT & " -map " & strQUOT & "[v]" & strQUOT
                cmd &= " " & file
            Else
                cmd &= file
            End If

            startInfo.FileName = cmd
            shell.StartInfo = startInfo
            shell.Start()
            shell.WaitForExit()
        Next k

        'https://stackoverflow.com/questions/5415006/ffmpeg-combine-merge-multiple-mp4-videos-not-working-output-only-contains-the
        'ffmpeg -f concat -i inputs.txt -vcodec copy -acodec copy Mux1.mp4
        If m > 1 Then
            cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y -f concat -i "
            ReDim Files(m - 1)
            'For k = 1 To m Step 1
            '        'ffmpeg -i opening.mkv -i episode.mkv -i ending.mkv -filter_complex concat output.mkv"
            'file = strQUOT & path & strBAK & "m" & k.ToString() & ".avi" & strQUOT
            'cmd &= "-i " & file
            'cmd &= " "
            'Next k
            For k = 1 To m Step 1
                Files(k - 1) = "m" & k.ToString() & ".avi"
            Next k
            WriteTxtFile(path, Files)
            file = strQUOT & path & strBAK & "File.txt" & strQUOT
            cmd &= file & " -vcodec copy -acodec copy " & strQUOT & path & strBAK & filename & ".avi" & strQUOT

            startInfo.FileName = cmd
            startInfo.WorkingDirectory = path
            shell.StartInfo = startInfo
            shell.Start()
            shell.WaitForExit()
            shell.Close()
        Else
            file = path & strBAK & "m1.avi"
            My.Computer.FileSystem.CopyFile(file, path & strBAK & filename & ".avi")
        End If

        'If we have dummy padding, concatenate each of the dummy_*.avi files into dummy.avi,
        'then vstack filename.avi with dummy.avi for final.avi. Rename final.avi to filename.avi and replace
        If hasPad Then
            'Concatenate dummy*.avi
            If m > 1 Then
                cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y -f concat -i "
                ReDim Files(m - 1)
                For k = 1 To m Step 1
                    Files(k - 1) = "dummy_" & k.ToString() & ".avi"
                Next k
                WriteTxtFile(path, Files)
                file = strQUOT & path & strBAK & "File.txt" & strQUOT
                cmd &= file & " -vcodec copy -acodec copy " & strQUOT & path & strBAK & "dummy.avi" & strQUOT
                startInfo.FileName = cmd
                startInfo.WorkingDirectory = path
                shell.StartInfo = startInfo
                shell.Start()
                shell.WaitForExit()
                shell.Close()
            Else
                file = path & strBAK & "dummy_1.avi"
                file2 = path & strBAK & "dummy.avi"
                My.Computer.FileSystem.MoveFile(file, file2, True)
            End If

            'vstack filename.avi with dummy.avi into final.avi
            'ffmpeg -i top.mp4 -i bot.mp4 -filter_complex vstack output.mp4
            cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y"
            file = strQUOT & path & strBAK & filename & ".avi" & strQUOT
            cmd &= " -i " & file
            file = strQUOT & path & strBAK & "dummy.avi" & strQUOT
            cmd &= " -i " & file
            cmd &= " -filter_complex vstack "
            file = strQUOT & path & strBAK & "final.avi" & strQUOT
            cmd &= file
            startInfo.FileName = cmd
            shell.StartInfo = startInfo
            shell.Start()
            shell.WaitForExit()
            shell.Close()

            file = path & strBAK & "final.avi"
            file2 = path & strBAK & filename & ".avi"
            My.Computer.FileSystem.MoveFile(file, file2, True)
        End If

            'Output to .jpg frames
            i = Val(txtTE_D.Text)
            cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y "
            file = strQUOT & path & strBAK & filename & ".avi" & strQUOT
            cmd &= "-i " & file
            file = strQUOT & path & strBAK & "frame_%0" & i.ToString() & "d.jpg" & strQUOT
            cmd &= " " & file
            startInfo.FileName = cmd
            shell.StartInfo = startInfo
            shell.Start()
            shell.WaitForExit()
            shell.Close()

            MsgBox("The JPG frames have been rendered! Make sure the proper amount of frames have been rendered. If more than necessary have been created, delete the extra frames. Then click OK to convert to a THP!", MsgBoxStyle.OkOnly, "THP Rendering")

            Dim hasAudio As Boolean = False
            If txtA_A.Text.ToString() = "True" Then
                hasAudio = True
            End If

            If hasAudio = False Then                
            cmd = strQUOT & txtTHPConv.Text & strQUOT
            file = "-j " & strQUOT & path & strBAK & "*.jpg" & strQUOT
            cmd &= " " & file & " -d"
            file = strQUOT & path & strBAK & filename & ".thp" & strQUOT
            cmd &= " " & file
            Else
            cmd = strQUOT & txtTHPConv.Text & strQUOT
            file = "-j " & strQUOT & path & strBAK & "*.jpg" & strQUOT
            cmd &= " " & file
                file = strQUOT & path & strBAK & filename & ".wav" & strQUOT
                cmd &= " -s " & file
                file = strQUOT & path & strBAK & filename & ".thp" & strQUOT
                cmd &= " -d " & file
            End If
            startInfo.FileName = cmd
            shell.StartInfo = startInfo
            shell.Start()
            shell.WaitForExit()
            shell.Close()

            MsgBox("THP rendered! Now cleaning up...", MsgBoxStyle.OkOnly, "Success!")
        CleanUp(path, filename, r, c, m, hasPad, False)
            MsgBox("Done!", MsgBoxStyle.OkOnly, "Tada!")
    End Sub

    Private Sub CleanUp(ByVal Path As String, ByVal filename As String, ByVal r As Byte, ByVal c As Byte, ByVal m As Byte, ByVal Haspad As Boolean, ByVal justBMPs As Boolean)
        'Cleanup
        Dim i As Byte
        Dim j As Byte
        Dim k As Byte
        Dim File As String

        If justBMPs = False Then
            File = Path & strBAK & filename & ".avi"
            If System.IO.File.Exists(File) Then
                My.Computer.FileSystem.DeleteFile(File)
            End If

            For j = 1 To c Step 1
                File = Path & strBAK & "c" & j.ToString() & ".avi"
                If System.IO.File.Exists(File) Then
                    My.Computer.FileSystem.DeleteFile(File)
                End If

                File = Path & strBAK & "d" & j.ToString() & ".avi"
                If System.IO.File.Exists(File) Then
                    My.Computer.FileSystem.DeleteFile(File)
                End If
            Next j

            For k = 1 To m Step 1
                File = Path & strBAK & "m" & k.ToString() & ".avi"
                If System.IO.File.Exists(File) Then
                    My.Computer.FileSystem.DeleteFile(File)
                End If
            Next k
            DeleteFilesFromFolder(Path, "*.jpg")
        End If

        If Haspad Then
            For k = 1 To m Step 1
                Dim srch As String = "dummy_" & k.ToString() & "_*.bmp"
                DeleteFilesFromFolder(Path, srch)
            Next k
            If justBMPs = False Then
                DeleteFilesFromFolder(Path, "dummy*.avi")
                File = Path & strBAK & "final.avi"
                If System.IO.File.Exists(File) Then
                    My.Computer.FileSystem.DeleteFile(File)
                End If
            End If

        End If

        If justBMPs = False Then
            File = Path & strBAK & "File.txt"
            If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
        End If
    End Sub

    Private Sub WriteTxtFile(ByVal Path As String, ByRef Files() As String)
        Dim TextFile As String = Path & strBAK & "File.txt"
        If My.Computer.FileSystem.FileExists(TextFile) Then
            My.Computer.FileSystem.DeleteFile(TextFile)
        End If

        Dim xFileData As StreamWriter 'Streamreader object for reading the data from the files
        xFileData = File.CreateText(TextFile)

        Dim i As Byte = 0
        Dim count As Byte = Files.Length - 1
        Dim line As String = ""
        For i = 0 To count Step 1
            line = "file " & Files(i)
            xFileData.WriteLine(line)
        Next i
        xFileData.Close()
        xFileData.Dispose()
    End Sub

    'https://stackoverflow.com/questions/25429791/how-do-i-delete-all-files-of-a-particular-type-from-a-folder
    Private Sub DeleteFilesFromFolder(ByVal Folder As String, ByVal type As String)
        If Directory.Exists(Folder) Then
            For Each _file As String In Directory.GetFiles(Folder, type)
                File.Delete(_file)
            Next _file
        End If
    End Sub

    Private Sub txtTE_F_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTE_F.Leave
        UpdateTEDigs()
    End Sub
    Private Sub txtTE_F_TextChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTE_F.TextChanged
        UpdateTEDigs()
    End Sub

    Private Sub UpdateTEDigs()
        Dim m As Byte = Val(txtVM_M.Text)
        Dim cnt As UShort = Val(txtTE_F.Text)
        Dim max As UShort = cnt * m
        Dim digs As Byte = max.ToString().Length
        txtTE_D.Text = digs.ToString()
    End Sub
End Class