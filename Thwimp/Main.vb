Imports System.IO
Imports System.Runtime.InteropServices

Public Class Main

    'Global constants

    'Characters
    Shared strBAK As String = "\"                           'Backslash symbol
    Shared strQUOT As String = Chr(34)                      'Quote Symbol
    Shared strNL As String = Environment.NewLine            'Newline symbol

    Shared strPATH As String = Application.StartupPath      'Directory of the exe
    Const LISTING As String = "FileListing.txt"             'File containing the    file listing for BreakGOLD's image files
    Const CDESC As String = "FileCDesc.txt"                   '~                    description info for the control signal
    Const DESC As String = "FileDesc.txt"                   '~                      description info for the image files
    Const DATA As String = "FileData.txt"                   '~                      hard-coded image data for the image files

    'Exe utils used by app
    Shared strFMPackPath As String = ""                     'Path to FFMPEG exes (ffmpeg, ffplay)
    Const exeFMPeg As String = "ffmpeg.exe"                 'FFMPEG. Used for THP de/encoding
    Const exeFPlay As String = "ffplay.exe"                 'FFPlay. Used for viewing THP files

    'THP data
    Shared THPs(255) As THPData                           'Array containing all of the THPData
    Const BADENTRY As SByte = -2                          'Flag for signifying that the data for empty THPs is invalid (prevents error). !@ Leftover from BreakGold Editor; try utilizing it to prevent errors on loading of dummy THP in combo box?

    ''' <summary>
    ''' Generic structure for image dims (width and height).
    ''' </summary>
    ''' <remarks>Like Vector2 in Unity3D</remarks>
    Structure Dims
        Dim width As UShort
        Dim height As UShort
    End Structure

    ''' <summary>
    ''' Structure for THP Video array info
    ''' </summary>
    ''' <remarks></remarks>
    Structure THPArr
        Dim row As Byte             'Amount of  rows
        Dim col As Byte             '~          cols
        Dim subV As Byte            '           subvideos (=rows*cols)
        Dim mult As Byte            '           Multiplicity for each cell
        Dim multOpt As Boolean      'Is multiplicity optional? !@ Needs removed from data files, code, form, defaults restored in data files
        Dim subVT As Byte           'Total amount of subvidoes (subvideos*multiplicity)
    End Structure

    ''' <summary>
    ''' Generic structure for frame info
    ''' </summary>
    ''' <remarks></remarks>
    Structure Frame
        Dim subframes As UShort     'Frames in a subvideo
        Dim totframes As UShort     'Total frames for THP file
    End Structure

    ''' <summary>
    ''' Audio info for THP files
    ''' </summary>
    ''' <remarks></remarks>
    Structure Audio
        Dim has As Boolean          'Does THP have audio stream?
        Dim Stereo As Boolean       'Is audio stereo?
        Dim freq As UShort          'Frequency of audio stream (Hz)
    End Structure

    ''' <summary>
    ''' Video info for THP files
    ''' </summary>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Super-structure for representing THP info
    ''' </summary>
    ''' <remarks></remarks>
    Structure THPData
        Dim visual As Video                                 'Video data of THP
        Dim audial As Audio                                 'Audio data of THP
        Dim Desc As String                                  'Desc: Description of usage of file (in words)
        Dim File As String                                  'File: Directory of the file, relative to the THP root
    End Structure
    '========================

    'APP Setup code

    ''' <summary>
    ''' Application setup code onLoad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Initialize the application with some setup code

        'Disable form elements in THP tab (until the data in the "Options" tab is filled out)
        'Hide THPFile lable and combo box, THP Info Group box, THP Dec/Encoder boxes
        lblTHPFile.Visible = False
        cmbTHP.Visible = False
        grpTHPInfo.Visible = False
        grpTHPDec.Visible = False
        grpTHPEnc.Visible = False

        'Load the THP combo box data from the ext. files
        InitTHPData()
    End Sub

    ''' <summary>
    ''' Retrieve the hard-coded THP data from the four data files, dumps into RAM (THPs array)
    ''' </summary>
    ''' <remarks>Similar to InitIMGData in setup from BreakGold Editor; same purpose</remarks>
    Public Sub InitTHPData()
        Dim xFileData As StreamReader   'Streamreader object for reading the data from the files
        Dim strEntry As String          'String data read from the [File].ReadLine() method
        Dim bytItems As Byte            'Number of items in the parallel arrays

        Dim bytCtr1 As Byte             'Generic Counter variable #1
        Dim bytCtr2 As Byte             'Generic Counter variable #2

        'Variables for processing the DATA file
        Const SEP As String = ","       'Constant variable for the ASCII character separating the sub entries per line
        Const SEP2 As String = ";"      'Constant variable ~                        ending the line
        Dim strVar As String            'String containing stringified numeric data (for processing the entries in the DATA file)
        Dim bytStart As Byte            'Start position in the string for extracting a subentry
        Dim bytEnd As Byte              'End position ~
        Dim bytLen As Byte              'The length of the string to extract
        Const bytDataEnt As Byte = 19   'Amount of entries per line

        '!@ Use me to prevent errors/handle dummy THP entries!
        'Dim bytErrFlag As Byte          'Counts the number of invalid entries, if = 5 bad entries, mark .BPP as BADENTRY to prevent errors

        bytItems = 0
        xFileData = File.OpenText(strPATH & strBAK & LISTING)   'Open the LISTING file

        'Count the amount of items
        While xFileData.EndOfStream() <> True
            strEntry = xFileData.ReadLine()                   'Read a line from the file
            bytItems += 1
        End While
        ReDim THPs(bytItems)                                  'Redim the THPs array appropriately
        'Close the LISTING file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing

        'Load all relative file paths into THPs(#).File
        xFileData = File.OpenText(strPATH & strBAK & LISTING)   'Open the LISTING file
        For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all of the lines
            strEntry = xFileData.ReadLine()                     'Read a line
            THPs(bytCtr1).File = strEntry                       'Dump file paths into appropriate array entry
            cmbTHP.Items.Add(THPs(bytCtr1).File)                'Dump data into Combo box
        Next bytCtr1
        'Close the LISTING file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing

        'Load all descriptions into THPs(#).Desc array
        xFileData = File.OpenText(strPATH & strBAK & DESC)      'Open the DESC file
        For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all lines
            strEntry = xFileData.ReadLine()                     'Read a line
            THPs(bytCtr1).Desc = strEntry                       'Dump the data into the array slots
        Next bytCtr1
        'Close the DESC file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing

        'Load all ctrl descriptions into THPs(#).visual.cdesc array
        xFileData = File.OpenText(strPATH & strBAK & CDESC)     'Open the CDESC file
        For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all lines
            strEntry = xFileData.ReadLine()                     'Read a line
            THPs(bytCtr1).visual.CDesc = strEntry               'Dump the data into the array slots
        Next bytCtr1
        'Close the DESC file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing
        '-------------------------
        'Get data from the DATA file

        'Load all AV data into THPs(#).visual and THPs(#).audial struct elements
        xFileData = File.OpenText(strPATH & strBAK & DATA)      'Load the DATA file
        For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all lines
            strEntry = xFileData.ReadLine()                     'Read a line

            bytStart = 1                                        'Init start pos
            'Parse each of the entries in each line
            For bytCtr2 = 1 To bytDataEnt Step 1                'Iterate through all entries in line

                'If not the last data entry in line, find the position of the SEP character (,), else SEP2 character (;)
                If bytCtr2 <> bytDataEnt Then bytEnd = InStr(bytStart, strEntry, SEP) Else bytEnd = InStr(bytStart, strEntry, SEP2) 'Record the position of the next SEP1 character,
                bytLen = (bytEnd - bytStart)             'Get the length of the sub entry (subtract End from Start)
                strVar = Mid(strEntry, bytStart, bytLen) 'Extract the sub entry via MID command

                '!@
                'If Val(strVar) = 0 Then bytErrFlag += 1 'If the entry is 0, increment error counter

                'Allocate the extracted value into the appropriate array data fields based on index
                Select Case bytCtr2

                    'Total THP video width/height
                    Case 1
                        THPs(bytCtr1).visual.TDim.width = Val(strVar)
                    Case 2
                        THPs(bytCtr1).visual.TDim.height = Val(strVar)

                    'THP subvideo array info
                        'Row, Col, R*C, Multiplicity, mult optional?, r*c*m
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

                        'Subvideo info
                        'Subvideo width and height
                    Case 9
                        THPs(bytCtr1).visual.SDim.width = Val(strVar)
                    Case 10
                        THPs(bytCtr1).visual.SDim.height = Val(strVar)

                        'Frame counts for each subvideo, total THP video
                    Case 11
                        THPs(bytCtr1).visual.Frames.subframes = Val(strVar)
                    Case 12
                        THPs(bytCtr1).visual.Frames.totframes = Val(strVar)

                        'Width and height of padding
                    Case 13
                        THPs(bytCtr1).visual.Padding.width = Val(strVar)
                    Case 14
                        THPs(bytCtr1).visual.Padding.height = Val(strVar)
                    Case 15
                        'FPS as single
                        THPs(bytCtr1).visual.FPS = Val(strVar)

                        'Control/Audio info
                        'Has control signal?, has audio?, is stereo?, audio freq
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
            Next bytCtr2 'Repeat for the other entries in the line
        Next bytCtr1 'Repeat for all lines

        'Close the DATA file
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing
    End Sub

    '===========================
    'Options Tab stuff

    ''' <summary>
    ''' 'Handles clicking of About button, showing the box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbout.Click
        My.Computer.Audio.Play(My.Resources.EagleSoft, AudioPlayMode.Background)    'Play "EagleSoft Ltd"
        About.ShowDialog()                                                          'Show the about box
    End Sub

    ''' <summary>
    ''' Hnadles loading the THP root dir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoadRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseRoot.Click
        'Load the LoadTHPRoot Load Dialog Box, user selects root directory of THP
        If LoadTHPRoot.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        txtRoot.Text = LoadTHPRoot.SelectedPath    'Dump the path into the textbox, for later retrieval
        CheckPathsSet()                             'Handle enabling THP Tab
    End Sub

    ''' <summary>
    ''' Handles loading the FFMPeg exe path
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseFFMpeg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFFMpeg.Click
        'Load the LoadFMPegRoot Load Dialog Box, user selects root directory of FFMpeg exes
        If LoadFMPegRoot.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        txtFFMpeg.Text = LoadFMPegRoot.SelectedPath    'Dump the path into the textbox, for later retrieval
        CheckPathsSet()                             'Handle enabling THP Tab
    End Sub
    ''' <summary>
    ''' Handles loading the THPConv exe file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseTHPConv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseTHPConv.Click
        'Load the LoadTHPConv ofd, user selects thpconv.exe
        If LoadTHPConv.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub        
        txtTHPConv.Text = LoadTHPConv.FileName      'Dump the path into the textbox, for later retrieval
        CheckPathsSet()                             'Handle enabling THP Tab
    End Sub

    ''' <summary>
    ''' If the options have been filled in, enable elements in THP tab
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckPathsSet()
        'Options det to be filled if something
        If (txtRoot.Text <> Nothing) And (txtFFMpeg.Text <> Nothing) And (txtTHPConv.Text <> Nothing) Then
            'Make everything in the THP tab visible now (THPFile lable and combo box, whole THP Info group box)
            lblTHPFile.Visible = True
            cmbTHP.Visible = True
            grpTHPInfo.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Handles dumping the data from the data files (now in RAM) into the appropriate fields for the THPInfo Group box, when the combo box has been changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbTHP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTHP.SelectedIndexChanged
        Dim bytEntry As Byte = (cmbTHP.SelectedIndex) + 1       'Index in the THP combo box

        'Set THPEnc and THPDec group boxes to visible
        grpTHPEnc.Visible = True
        grpTHPDec.Visible = True

        'Update stats for current THP
        'Total video width and height
        txtTDims_W.Text = THPs(bytEntry).visual.TDim.width.ToString()
        txtTDims_H.Text = THPs(bytEntry).visual.TDim.height.ToString()

        'THP Array info: row, column, amount of subvids (=r*c)
        txtArr_R.Text = THPs(bytEntry).visual.THPinfo.row.ToString()
        txtArr_C.Text = THPs(bytEntry).visual.THPinfo.col.ToString()
        txtArr_S.Text = THPs(bytEntry).visual.THPinfo.subV.ToString()

        'Video multiplicity info: Amount of mult, optional? 
        txtVM_M.Text = THPs(bytEntry).visual.THPinfo.mult.ToString()
        txtVM_O.Text = THPs(bytEntry).visual.THPinfo.multOpt.ToString()

        'Total amount of subvideos (r*c*multiplicity)
        txtV_TSubs.Text = THPs(bytEntry).visual.THPinfo.subVT.ToString()

        'Sizes of the subvideos and padding (width x height in px)
        txtVS_W.Text = THPs(bytEntry).visual.SDim.width.ToString()
        txtVS_H.Text = THPs(bytEntry).visual.SDim.height.ToString()
        txtVP_W.Text = THPs(bytEntry).visual.Padding.width.ToString()
        txtVP_H.Text = THPs(bytEntry).visual.Padding.height.ToString()

        'Number of frames in the video: frames in each subvideo (each m), and total for the THP file (t=s*m)
        txtVF_S.Text = THPs(bytEntry).visual.Frames.subframes.ToString()
        txtVF_T.Text = THPs(bytEntry).visual.Frames.totframes.ToString()

        'Other playback info. FPS, does video have control signal?, Text desc of the control signal usage
        txtVC_F.Text = THPs(bytEntry).visual.FPS.ToString("f")
        txtVC_C.Text = THPs(bytEntry).visual.Ctrl.ToString()
        txtVC_D.Text = THPs(bytEntry).visual.CDesc

        'Audio info. Has audio?, Stereo?, frequency of audio (usually 32kHz)
        txtA_A.Text = THPs(bytEntry).audial.has.ToString
        txtA_S.Text = THPs(bytEntry).audial.Stereo.ToString
        txtA_F.Text = THPs(bytEntry).audial.freq.ToString        

        'Text desc about the THP video file
        txtFDesc.Text = THPs(bytEntry).Desc

        'Prepare THPEnc/Dec fields
        HandleArrState()                                'Show naming conventions for this THP file
        chkRipString()                                  'Update THPEnc rip type text
        chkRipValues()                                  'Update the default THPEnc Crop values based on the chkRip type value
        txtTE_D.Text = txtVF_T.Text.Length.ToString()   'Set default value in THPEnc for digits, based on the string.length of the video's total frames
    End Sub

    ''' <summary>
    ''' Handles the checkbox array depiction of naming conventions for THP encoding
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleArrState()
        Dim Boxes(6, 2) As System.Windows.Forms.CheckBox    'Array of 6x2 check boxes
        Dim Dum As System.Windows.Forms.CheckBox            'Dummy check box (for padding)
        Dim Wav As System.Windows.Forms.CheckBox            'Wav check box (for audio wav file)

        'Init the array. In A1N MS Excel notation, Alpha=row, Number=Col
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
        'Wav and dummy boxes
        Dum = chkTE_Dum
        Wav = chkTE_wav

        'Generic iterators
        Dim i As Byte = 0
        Dim j As Byte = 0

        'Update the checked and enabled states of the array based on the video data 
        'Amount of rows in video, columns, and multiplicity
        Dim r As Byte = Val(txtArr_R.Text)
        Dim c As Byte = Val(txtArr_C.Text)
        Dim m As Byte = Val(txtVM_M.Text)
        Dim state As Boolean = False                    'Generic bool

        For i = 1 To 6 Step 1                           'Iterate through all rows (1-6)
            For j = 1 To 2 Step 1                       'Iterate through all cols (1-2)
                If r = 0 And c = 0 Then
                    'If a dummy entry in combo box was selected, then r & c of array will be 0. Set all boxes to unchecked/disabled
                    state = False
                Else
                    If i <= r And j <= c Then
                        'If i and j iterators (row/col) are within
                        'the amount of rows and col for this video, 
                        'then cell is used. Check & enable the boxes
                        state = True
                    Else
                        'Otherwise unused, uncheck and disable
                        state = False
                    End If
                End If

                'Update the checked/enabled states as appropriately
                Boxes(i, j).Checked = state
                Boxes(i, j).Enabled = state
            Next j
        Next i

        'Handle dummy checkbox states
        'Get dimensions of video padding
        Dim d As Dims
        d.width = Val(txtVP_W.Text)
        d.height = Val(txtVP_H.Text)        
        state = True
        'If the dims are both 0, then no padding.
        If d.width = 0 And d.height = 0 Then state = False
        'Update the checked/enabled states
        Dum.Checked = state
        Dum.Enabled = state

        'Handle wav checkbox states
        state = BoolStrToBool(txtA_A.Text)    'If "True" then true
        'Update the wav checkbox states
        Wav.Checked = state
        Wav.Enabled = state

        'Handle the multiplicity box (the m values)
        'If only m=1, then "_1", else "_1\nto\nM"
        If m = 1 Then txtTE_M.Text = "_1" Else txtTE_M.Text = "_1" & strNL & "to" & strNL & "_" & m.ToString()
        'Update the text
        txtTE_F.Text = txtVF_S.Text
    End Sub

    '---------------------
    'DOS file path functions

    ''' <summary>
    ''' Given a full file path, returns the directory
    ''' </summary>
    ''' <param name="strPath">Full file path</param>
    ''' <returns>File path</returns>
    ''' <remarks></remarks>
    Public Function FileDir(ByVal strPath As String) As String
        Dim strOut As String = ""       'Output
        Dim strFile As String = ""      'The file itself (ie, File.ext)        
        strFile = FileAndExt(strPath)   'Get the file+extension

        'From the full file path, replace the file+ext with nothing, to get file directory; return
        strOut = Replace(strPath, strFile, "")
        Return strOut
    End Function

    ''' <summary>
    ''' Given a full file path, returns filepath with changed file extension
    ''' </summary>
    ''' <param name="strPath">Full file path</param>
    ''' <param name="strOldExt">Old extension</param>
    ''' <param name="strNewExt">New extension</param>
    ''' <returns>Full file with new extension</returns>
    ''' <remarks></remarks>
    Public Function FileChangeExt(ByVal strPath As String, ByVal strOldExt As String, ByVal strNewExt As String)
        'Get the file+ext from the file path, replace old extension with new extension
        FileChangeExt = Replace(FileAndExt(strPath), strOldExt, strNewExt)
    End Function

    ''' <summary>
    ''' Given a full file path, returns the filename+ext
    ''' </summary>
    ''' <param name="strPath">Full file path</param>
    ''' <returns>Filename+ext</returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Writes File.txt at Path with list of Files (rel dirs).
    ''' </summary>
    ''' <param name="Path">Directory</param>
    ''' <param name="Files">Array of filenames</param>
    ''' <remarks>Used for -i param for ffmpeg.exe</remarks>
    Private Sub WriteTxtFile(ByVal Path As String, ByRef Files() As String)

        Dim TextFile As String = Path & strBAK & "File.txt" 'The filepath to write
        'If the textfile exists, remove it for clean slate
        If My.Computer.FileSystem.FileExists(TextFile) Then My.Computer.FileSystem.DeleteFile(TextFile)

        Dim xFileData As StreamWriter           'Streamwriter object to write File.txt
        xFileData = File.CreateText(TextFile)   'Create File.txt

        Dim i As Byte = 0                       'Generic iterator
        Dim count As Byte = Files.Length - 1    'Count of files in list (0-based)
        Dim line As String = ""                 'Line to write to file

        'Iterate through the files, 0 to count
        For i = 0 To count Step 1
            line = "file " & Files(i)   'Line = "file myFilename.blah"
            xFileData.WriteLine(line)   'Write the line
        Next i
        'Close and dispose the SW
        xFileData.Close()
        xFileData.Dispose()
        xFileData = Nothing
    End Sub

    ''' <summary>
    ''' Deletes files from a folder based on a DOS search spec
    ''' </summary>
    ''' <param name="Folder">Folder to search</param>
    ''' <param name="type">Search spec</param>
    ''' <remarks>
    ''' Like del cmd. For stuff like del *.pdf.
    ''' https://stackoverflow.com/questions/25429791/how-do-i-delete-all-files-of-a-particular-type-from-a-folder
    ''' </remarks>
    Private Sub DeleteFilesFromFolder(ByVal Folder As String, ByVal type As String)
        'If folder exists
        If Directory.Exists(Folder) Then
            'Iterate through all files that match spec, delete them
            For Each _file As String In Directory.GetFiles(Folder, type)
                File.Delete(_file)
            Next _file
        End If
    End Sub

    '================
    'Cast helper funcs

    Private Function BitToBool(ByVal inp As Byte) As Boolean
        Dim outp As Boolean = False
        If inp = 1 Then outp = True
        Return outp
    End Function

    'Converts String with "True" or "False" into appropriate boolean value
    'In = Boolean String
    'Out = Boolean value
    Private Function BoolStrToBool(ByVal inp As String) As Boolean
        Dim outp As Boolean = False      'Output value
        If inp = "True" Then outp = True 'If "True" then true
        Return outp
    End Function

    ''' <summary>
    ''' Changes a boolean checkbox's string based on state
    ''' </summary>
    ''' <param name="T">True string</param>
    ''' <param name="F">False string</param>
    ''' <param name="box">Checkbox</param>
    ''' <remarks></remarks>
    Private Sub ChkString(ByVal T As String, ByVal F As String, ByRef box As System.Windows.Forms.CheckBox)
        Dim v As String
        If box.Checked = True Then v = T Else v = F
        box.Text = v
    End Sub

    '================

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click        
        Dim startInfo As ProcessStartInfo
        startInfo = New ProcessStartInfo
        If chkRip_DSound.Checked = True Then
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
        Dim file As String = ""
        Dim type As Boolean = chkRip_Type.Checked
        newFile = newFile.Replace(".thp", "")
        ofdRip.FileName = newFile
        ofdRip.InitialDirectory = initDir

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
        Dim hasAudio As Boolean = BoolStrToBool(has)

        If hasAudio Then
            If chkRip_DSound.Checked = True Then
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

        If type = True Then            
            cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y "

            'Output to .jpg frames
            Dim d As String = ""
            Dim dgs As UShort = 0
            dgs = Val(txtVF_T.Text.Length)
            d = "%0" & dgs.ToString() & "d"
            cmd &= "-i " & strQUOT & outFile & strQUOT
            file = strQUOT & FileDir(outFile) & "dummyTemp_" & d & ".bmp" & strQUOT
            cmd &= " " & file
            startInfo.FileName = cmd
            shell.StartInfo = startInfo
            shell.Start()
            shell.WaitForExit()

            'Rename the appropriate frames to dummyN.bmp"
            Dim i As Byte = 0
            Dim j As UShort = 0
            Dim frames As UShort = Val(txtVF_S.Text)
            Dim m As Byte = Val(txtVM_M.Text) - 1
            For i = 0 To m Step 1
                j = i * frames
                j += 1
                d = "_" & j.ToString(StrDup(dgs, "0")) & ".bmp"
                file = "dummy_" & (i + 1).ToString() & ".bmp"
                file = FileDir(outFile) & FileAndExt(file)
                My.Computer.FileSystem.MoveFile(FileDir(outFile) & "dummyTemp" & d, file, True)
            Next i
            DeleteFilesFromFolder(FileDir(outFile), "dummyTemp*.bmp")
        End If
        shell.Close()
    End Sub

    'If any of the checkboxes in the THP_Enc group box array have been changed, maintain the current state for the THP file.
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
    Private Sub chkTE_wav_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTE_wav.CheckedChanged
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

        Dim hasAudio As Boolean = BoolStrToBool(txtA_A.Text.ToString())

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

    ''' <summary>
    ''' Cleans up temporary files during THP encoding
    ''' </summary>
    ''' <param name="Path">Path to working dir</param>
    ''' <param name="filename">Filename for the THP</param>
    ''' <param name="r">Amount of rows in subvideo array</param>
    ''' <param name="c">Amount of cols in subvideo array</param>
    ''' <param name="m">Multiplicity for subvids</param>
    ''' <param name="Haspad">Does video use padding?</param>
    ''' <param name="justBMPs">Just cleanup bmps for dummy video encoding?</param>
    ''' <remarks></remarks>
    Private Sub CleanUp(ByVal Path As String, ByVal filename As String, ByVal r As Byte, ByVal c As Byte, ByVal m As Byte, ByVal Haspad As Boolean, ByVal justBMPs As Boolean)

        'Generic iterators
        Dim i As Byte       'Rows
        Dim j As Byte       'Cols
        Dim k As Byte       'Multiplicity
        Dim File As String  'File to check against/to delete/to whatever

        If justBMPs = False Then
            'Delete thpfilename.avi (final avi without padding) if exists
            File = Path & strBAK & filename & ".avi"
            If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)

            'Delete column videos
            'Iterate cols from 1 to c
            For j = 1 To c Step 1
                'if cN.avi or dN.avi exists, delete
                File = Path & strBAK & "c" & j.ToString() & ".avi"
                If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                File = Path & strBAK & "d" & j.ToString() & ".avi"
                If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
            Next j

            'Delete multiplicity videos
            'Iterate mult from 1 to m
            For k = 1 To m Step 1
                'if mN.avi exists, delete
                File = Path & strBAK & "m" & k.ToString() & ".avi"
                If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
            Next k

            'Delete all jpg frames (used for THP video stream)
            DeleteFilesFromFolder(Path, "*.jpg")
        End If

        'If video has padding, delete temp files for dummy padding
        If Haspad Then
            'Delete BMP frames from dummy videos (dummy_N_%0Nd_.bmp)
            'Iterate multiplicity from 1 to m
            For k = 1 To m Step 1
                Dim srch As String = "dummy_" & k.ToString() & "_*.bmp"
                DeleteFilesFromFolder(Path, srch)
            Next k

            If justBMPs = False Then
                'Delete dummy_N.avi files
                DeleteFilesFromFolder(Path, "dummy*.avi")

                'Delete final.avi if exists
                File = Path & strBAK & "final.avi"
                If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
            End If
        End If

        If justBMPs = False Then
            'Delete file.txt if exists, a list of files used for -i in ffmpeg.exe
            File = Path & strBAK & "File.txt"
            If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
        End If
    End Sub

    'Update the THP Encoder digits on leav/change of digits textbox
    Private Sub txtTE_F_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTE_F.Leave
        UpdateTEDigs()
    End Sub
    Private Sub txtTE_F_TextChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTE_F.TextChanged
        UpdateTEDigs()
    End Sub

    ''' <summary>
    ''' Auto-updates the THP Encoder digits box for the 0-padding for the output JPEG frames.
    ''' Based on the amount of frames to limit each subvideo * multiplicity
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateTEDigs()
        Dim m As Byte = Val(txtVM_M.Text)           'The multiplicity in the THP
        Dim cnt As UShort = Val(txtTE_F.Text)       'The amount of frames to limit each subvideo
        Dim max As UShort = cnt * m                 'Max amount of frames in new THP
        Dim digs As Byte = max.ToString().Length    'Get the amount of digits in the max value
        txtTE_D.Text = digs.ToString()              'Update the digits text
    End Sub

    Private Sub chkRip_Type_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRip_Type.CheckedChanged
        'Update the rip type string and crop values onCheck of chkRip
        chkRipString()
        chkRipValues()
    End Sub

    ''' <summary>
    ''' Changes the default rip values for the Crop box based on the chkRip type checkbox state
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkRipValues()
        If chkRip_Type.Checked = True Then
            'If ripping AVI(+WAV) and dummies
            Dim size As Dims                                    'Size of the crop area
            Dim pos As Dims                                     'Position of the TL corner of crop area
            size.width = Val(txtVP_W.Text)                      'Width = Padding width
            size.height = Val(txtVP_H.Text)                     'Height = Padding height
            pos.width = 0                                       'Start at x=0
            pos.height = Val(txtTDims_H.Text) - size.height     'Start at y=Total video height - padding height

            'Set the values into the text boxes as appropriately (xpos, ypos, width, height
            txtTD_CX.Text = pos.width
            txtTD_CY.Text = pos.height
            txtTD_CW.Text = size.width
            txtTD_CH.Text = size.height
        Else
            'If ripping just AVI(+wav)
            'Set the values into the text boxes as appropriately (xpos=0, ypos=0, width=total vid width, height=total vid height).
            'Rips whole video, no cropping
            txtTD_CX.Text = 0
            txtTD_CY.Text = 0
            txtTD_CW.Text = txtTDims_W.Text
            txtTD_CH.Text = txtTDims_H.Text
        End If
    End Sub

    ''' <summary>
    ''' Changes text of Rip type check box as appropriately
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkRipString()
        Dim NL As String = strNL                                              'New line char
        Dim D As Dims                                                                       'Dims for video padding
        D.width = Val(txtVP_W.Text)                                                         'Get padding width
        D.height = Val(txtVP_H.Text)                                                        'Get height width

        Dim hasPad As Boolean = False                                                       'Does THP video has padding?
        If D.width <> 0 And D.height <> 0 Then hasPad = True 'If Padding dims aren't zero, it does have padding

        'If doesn't have padding and the chkbox is checked, force box to unset (just AVI/WAV)
        If hasPad = False And chkRip_Type.Checked = True Then chkRip_Type.Checked = False

        Dim hasAudio As Boolean = BoolStrToBool(txtA_A.Text)            'Does the video have audio? (Is its field "True"?)
        Dim strFalse As String = "Rip to" & NL & "AVI"                  'String to use when check box is false ("Rip to\nAVI" by default)
        If hasAudio = True Then strFalse = "Rip to" & NL & "AVI+WAV" '  'If video has audio, change false string to "Rip to\nAVI+WAV"
        Dim strTrue As String = strFalse & "," & NL & "dummy"           'String to use when check box is true. This will be the false string + ",\nDummy" for dummy ripping7
        ChkString(strTrue, strFalse, chkRip_Type)                       'Change the checkbox text as appropriately based on state
    End Sub
End Class