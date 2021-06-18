'Thwimp - GUI/CLI FOSS utility for ripping, viewing, and creating THP video files for Mario Kart Wii
'Copyright (C) 2018-2020 Tamkis

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
'Email: tamkis[at]eaglesoftltd[dot]com

Imports System.IO
Imports System.Runtime.InteropServices

Public Class Main    
    'Global constants

    'Debug mode?
    'If enabled, don't hide THP tab, and use current contents for quick debugging)
    'If disabled, clear options tab items and forcibly set them every run
    Shared DEBUG As Boolean = False


    'Formatting, parsing, and bugfix stuff

    ''' <summary>
    ''' Single_ToString function enum value conversion types
    ''' </summary>
    ''' <remarks></remarks>
    Enum Single_ToString_Types
        PERCENT = -1    '2-digit percentage (nn.dd%)
        _SINGLE = 0     '2-digit single (nn.dd)
        FIXED = 1       'G9 fixed point (for FFMPEG audio conversion)
    End Enum

    'Invariant culture object info
    Shared culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture

    'Command-Line Interface Mode?
    'This flag will be set if called from CommandLine with args, and will change the runtime behavior for errors etc.
    'http://www.csharp411.com/console-output-from-winforms-application/
    Shared CLI_MODE As Boolean = False
    Shared CLI_Attached As Boolean = False                  'Has application hooked into calling Command Prompt? (For Console.WriteLine redirection)
    Private Const ATTACH_PARENT_PROCESS As Integer = -1     'Constant for AttachConsole Kernel32.dll PInvoke

    'Characters
    Shared strPATHSEP As String = Path.DirectorySeparatorChar 'Path separator symbol
    Shared strBAK As String = "\"                             'Backslash. Needed for some FFMPEG cmds
    Shared strQUOT As String = Chr(34)                        'Quote Symbol
    Shared strNL As String = Environment.NewLine              'Newline symbol

    Shared strPATH As String = Application.StartupPath      'Directory of the exe
    Shared SONG As String = strPATH & strPATHSEP & "Song.wav"   'Elevator music song file
    Const LISTING As String = "FileListing.txt"             'File containing the    file listing for BreakGOLD's image files
    Const CDESC As String = "FileCDesc.txt"                 '~                      description info for the control signal
    Const DESC As String = "FileDesc.txt"                   '~                      description info for the image files
    Const DATA As String = "FileData.txt"                   '~                      hard-coded image data for the image files
    Const INFO As String = "FileSet.txt"                    '~                      fileset info
    Const TEMPFLOG As String = "full_log.txt"               'Temporary file for log file

    'Exe utils used by app
    Shared strFMPackPath As String = ""                     'Path to FFMPEG exes (ffmpeg, ffplay)
    Const exeFMPeg As String = "ffmpeg.exe"                 'FFMPEG. Used for THP de/encoding
    Const exeFPlay As String = "ffplay.exe"                 'FFPlay. Used for viewing THP files

    'THP data
    Shared THPs(255) As THPData                           'Array containing all of the THPData

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
        Dim Bad As Boolean                                  'Is this a dummy, bad THP entry?
    End Structure
    '========================

    'APP Setup code/THP Combo Box/Important THP struct stuff

    ''' <summary>
    ''' Application setup code onLoad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Initialize the application with some setup code
        Dim CLI As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs    'Get the cmdline args
        If CLI.Count <> 0 Then CLI_MODE = True 'If params exist, then CLI MODE

        'Init log info
        Dim info As String = Application.ProductName & ": " & Application.ProductVersion        

        'Load Options tab
        tabApp.SelectedIndex = 1

        'if not debug mode, disable form elements in THP tab (until the data in the "Options" tab is filled out)        
        If DEBUG = False Then HideTHPData()

        'No longer do this; due to bugs it cawses (see issue #11)

        'Auto-assign DataFile directory to this exe's (default dir)
        'txtDataDir.Text = strPATH

        'Load the THP combo box data from the ext. files
        'InitTHPData()

        'Handle CLI arguments (if CLI_MODE)
        Try
            If CLI_MODE Then
                CLI_Attached = AttachConsole(ATTACH_PARENT_PROCESS) 'Attach Console to calling Command Prompt, return success result
                If CLI_Attached Then SetConsoleTitle("Thwimp CLI") 'Set the title on the console window (if attached)                
                Log(info, MsgBoxStyle.Information, True)            'Init log, with current build version, time, date
                Dim found(2) As Boolean             'Array of states, indicating if a specified param string key was found
                Dim success As Boolean = False
                Dim param(2) As String              'Array of params to check for
                param(0) = ""
                param(1) = ""

                Dim mode As String = ""             'CLI action (Encode, View, Rip)

                'Variables to hold argument values
                Dim INIFile As String = ""          'Inifile path
                Dim THP_ID As Byte = 0              'THP ID
                Dim preset As String = ""           'Preset (A1_N string)
                Dim crop As String = ""             'Crop CSV string
                Dim sound As Boolean = False        'Sound?
                'Dim _Log As Boolean = False
                Dim InputFolder As String = ""      'Input folder path
                Dim OutputFolder As String = ""     'Output folder path
                Dim TruncFrame As Integer = 0       'TruncFrame value
                Dim Digs As Integer = 0             '# of digits
                Dim JPGQual As Integer = 0          'JPG Quality

                Const SEP As String = ","           'CSV separator char (comma)

                'Argument strings
                'CLI Mode strings
                Const CMD_Mode_Rip As String = "/mr"        'Rip
                Const CMD_Mode_View As String = "/mv"       'View
                Const CMD_Mode_Encode As String = "/me"     'Encode

                'Various enums for                          Help mode
                Const CMD_Mode_Help1 As String = "/help"
                Const CMD_Mode_Help2 As String = "/h"
                Const CMD_Mode_Help3 As String = "/?"
                Const CMD_Mode_Help4 As String = "/mh"

                'Individual switches for Rip, View, Encode modes
                Const CMD_Setting As String = "/s="         'Settings file
                Const CMD_THP_ID As String = "/t="          'THP ID
                Const CMD_Preset As String = "/p="          'A1_N Preset
                Const CMD_Crop As String = "/c="            'Crop params
                Const CMD_Sound As String = "/n="           'Enable SDL_Audiodrive sound flag?
                Const CMD_Output As String = "/o="          'Output folder
                'Const CMD_Log As String = "/L="             'Log stuff?
                Const CMD_Frame As String = "/f="           'TruncFrame value
                Const CMD_JPGQ As String = "/q="            'JPG Quality (0-100)
                Const CMD_Input As String = "/i="           'Input folder

                mode = CLI(0)                               'Get 0th argument; should be the mode
                Select Case mode
                    'Handle RIP and View modes
                    Case CMD_Mode_Rip, CMD_Mode_View
                        'Rip or View
                        'thwimp.exe /mr /s=options.txt /tNNN [[/p=A1_N_preset] || [/c=csv_cropvals]] /n=snd_bit /o=output_folder
                        'thwimp.exe /mv /s=options.txt /tNNN [[/p=A1_N_preset] || [/c=csv_cropvals]] /n=snd_bit

                        'Check if next param is settings flag, pop param into param(0); otherwise throw error
                        found(0) = ParseParamOrder(CMD_Setting, CLI(1), param(0), -1)
                        If found(0) = False Then Throw New System.Exception("Expected 2nd argument as " & CMD_Setting & "options.txt")
                        INIFile = param(0)                  'Set INI file to the param
                        success = LoadSettings2(INIFile)    'Load INI file
                        'If failure to load INI file, throw error
                        If success = False Then Throw New System.Exception("Failed to properly handle " & CMD_Setting & "options INI file!")

                        'Switch to THP tab
                        tabApp.SelectTab(0)

                        'Handle /t THP ID switch
                        'Check if next param is THP ID flag, pop param into param(0); otherwise throw error
                        found(0) = ParseParamOrder(CMD_THP_ID, CLI(2), param(0))
                        If found(0) = False Then Throw New System.Exception("Expected 3rd argument as " & CMD_THP_ID & "NNN")
                        'Parse argument as byte, then set the THP ID
                        THP_ID = TryParseErr_Byte(param(0))
                        'Check if THP_ID is within range of current fileset; if not, throw error
                        Dim max As Byte = cmbTHP.Items.Count - 1
                        If THP_ID < 0 Or THP_ID > max Then Throw New System.Exception("THP ID selected is out of range for the current Data fileset!")
                        cmbTHP.SelectedIndex = THP_ID
                        'Also check if THP ID is BAD dummy entry.
                        'This needs to be done AFTER selecting the ID, so we can determine if it's a BAD dummy entry or not
                        If THPs(THP_ID + 1).Bad = True Then Throw New System.Exception("THP ID selected is a bad, dummy entry!")

                        'Handle /p preset or /c cropvals
                        'Check if next param is either THP preset flag or crop flag, pop param into param(0); otherwise throw errors
                        found(0) = ParseParamOrder(CMD_Preset, CLI(3), param(0))
                        found(1) = ParseParamOrder(CMD_Crop, CLI(3), param(1))
                        If found(0) = False And found(1) = False Then
                            'Neither argument was found; throw error
                            Throw New System.Exception("Expected 4th argument to be either " & CMD_Preset & "NNN preset or " & CMD_Crop & "csv_cropvals")
                        Else
                            If found(0) Then
                                'If preset argument, set preset
                                preset = param(0)

                                'Select the A1_N preset from the string; if unsuccessful, throw error
                                success = SelectA1N_Preset(preset)
                                If success = False Then Throw New System.Exception("Failed to propery handle " & CMD_Preset & "preset")
                            ElseIf found(1) Then
                                'If crop argument, set crop strings
                                crop = param(1)
                                CLI_HandleCropCSV(crop)
                            End If
                        End If

                        'Change DirectSound option
                        'Check if next param is Directsound flag, pop param into param(0); otherwise throw errors
                        found(0) = ParseParamOrder(CMD_Sound, CLI(4), param(0))
                        If found(0) = False Then Throw New System.Exception("Expected 4th argument as " & CMD_Sound & "DirectSound option")
                        'Convert bit to bool, set checkbox flag
                        sound = BitToBool(TryParseErr_Byte(param(0)))
                        chkRip_DSound.Checked = sound

                        'thwimp.exe /mr /s=options.txt /tNNN [[/p=A1_N_preset] || [/c=csv_cropvals]] /n=snd_bit /o=output_folder
                        'thwimp.exe /mv /s=options.txt /tNNN [[/p=A1_N_preset] || [/c=csv_cropvals]] /n=snd_bit
                        If mode = CMD_Mode_Rip Then
                            'If Rip mode, get output folder
                            'Check if next param is output folder flag, pop param into param(0); otherwise throw errors
                            found(0) = ParseParamOrder(CMD_Output, CLI(5), param(0), -1)
                            If found(0) = False Then Throw New System.Exception("Expected 5th argument as /o Output folder")
                            'Set output folde 
                            OutputFolder = param(0)

                            'Do the rip!
                            Rip(OutputFolder)
                        Else
                            'If view mode, no more params; just play it back!
                            'Perform click to view
                            btnPlay.PerformClick()
                        End If

                    Case CMD_Mode_Encode
                        'Encode
                        'thwimp.exe /me /s=options.txt /tNNN /f=trunc_frame_val[,digs] /q=jpgqual /i=input_folder

                        'Check if next param is settings flag, pop param into param(0); otherwise throw errors
                        found(0) = ParseParamOrder(CMD_Setting, CLI(1), param(0), -1)
                        If found(0) = False Then Throw New System.Exception("Expected 2nd argument as " & CMD_Setting & "options.txt")
                        'Load the INI File
                        INIFile = param(0)
                        success = LoadSettings2(INIFile)
                        'Throw error if unsuccessful
                        If success = False Then Throw New System.Exception("Failed to properly handle " & CMD_Setting & "options INI file!")

                        'Switch to THP tab
                        tabApp.SelectTab(0)

                        'Handle /t THP ID switch
                        'Check if next param is THP ID flag, pop param into param(0); otherwise throw error
                        found(0) = ParseParamOrder(CMD_THP_ID, CLI(2), param(0))
                        If found(0) = False Then Throw New System.Exception("Expected 3rd argument as " & CMD_THP_ID & "NNN")
                        'Parse argument as byte, then set the THP ID
                        THP_ID = TryParseErr_Byte(param(0))
                        cmbTHP.SelectedIndex = THP_ID


                        'Handle /f Trunc_Frame switch and d digs (comma delim'd)
                        'Check if next param is Frame/Digs csv, pop param into param(0); otherwise throw error
                        found(0) = ParseParamOrder(CMD_Frame, CLI(3), param(0))
                        If found(0) = False Then Throw New System.Exception("Expected 4th argument as " & CMD_Frame & "_trunc_frame_val[,digs]")
                        Dim delim As Boolean = param(0).Contains(SEP)
                        If delim Then
                            'See if param has a comma (if so, then digs param too)
                            Dim delimPos As Byte = InStr(param(0), SEP) 'Find the position of the SEP char
                            param(1) = Mid(param(0), delimPos + 1)      'Set param(1) digs to as string from SEP to EOL
                            param(0) = Mid(param(0), 1, delimPos - 1)       'Set param(0) trunc_frame from start to SEP char 
                        End If

                        TruncFrame = TryParseErr_UShort(param(0))       'Try parsing trunc_frame as short
                        txtTE_F.Text = param(0)                         'Update text box
                        If delim Then
                            'If delim char, then handle digs 
                            Digs = TryParseErr_Byte(param(1))           'Parse digs as byte
                            txtTE_D.Text = param(1)                     'Update text box
                        End If


                        'Handle /q JPG Qual switch
                        'Check if next param is JPG Qual, pop param into param(0); otherwise throw error
                        found(0) = ParseParamOrder(CMD_JPGQ, CLI(4), param(0))
                        If found(0) = False Then Throw New System.Exception("Expected 5th argument as " & CMD_JPGQ & "JPG_Quality")
                        JPGQual = TryParseErr_Byte(param(0))                        'TryParse value as byte
                        'Make sure JPG quality is between 0-100%; else clamp
                        If JPGQual < 0 Then
                            JPGQual = 0
                        ElseIf JPGQual > 100 Then
                            JPGQual = 100
                        End If
                        'Update the value
                        nudTE_jpgq.Value = JPGQual


                        'Handle /i Input folder
                        'Check if next param is Input folder, pop param into param(0); otherwise throw error
                        found(0) = ParseParamOrder(CMD_Input, CLI(5), param(0), -1)
                        If found(0) = False Then Throw New System.Exception("Expected 6th argument as " & CMD_Input & "input_folder")
                        'Set the inputFolder
                        InputFolder = param(0)

                        'Do the encoding!
                        Encode(InputFolder)

                    Case CMD_Mode_Help1, CMD_Mode_Help2, CMD_Mode_Help3, CMD_Mode_Help4
                        'If Help mode, display help
                        CmdHelp()
                    Case Else
                        'If anything else, display help
                        Log_MsgBox(Nothing, "Invalid arguments/Thwimp action mode!", MsgBoxStyle.Exclamation, "Syntax error", True)
                        CmdHelp()
                End Select
                CloseApp_CLI()
            Else
                'Init log, with current build version, time, date
                Log(info, MsgBoxStyle.Information, True)
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error parsing CLI arguments!", True)
            CloseApp_CLI()
        End Try
    End Sub


    'Various Kernel32.dll PInvokes for Console stuff

    ''' <summary>
    ''' Allocate a Console
    ''' </summary>
    ''' <returns>Handle/error code</returns>
    ''' <remarks>https://www.pinvoke.net/default.aspx/kernel32.AllocConsole</remarks>
    Private Declare Function AllocConsole Lib "kernel32" () As Long
    ''' <summary>
    ''' Free handle from console
    ''' </summary>
    ''' <returns>Handle/error code</returns>
    ''' <remarks>https://www.pinvoke.net/default.aspx/kernel32.FreeConsole</remarks>
    Private Declare Function FreeConsole Lib "kernel32" () As Long
    ''' <summary>
    ''' Attached console to command prompt that called this app (if any)
    ''' </summary>
    ''' <param name="dwProcessId">Constant for process ID</param>
    ''' <returns>Succesfully attached?</returns>
    ''' <remarks>https://www.pinvoke.net/default.aspx/kernel32.AttachConsole</remarks>
    Private Declare Function AttachConsole Lib "kernel32" (ByVal dwProcessId As Integer) As Boolean
    ''' <summary>
    ''' Set the title for the console
    ''' </summary>
    ''' <param name="lpConsoleTitle">Title</param>
    ''' <returns>Handle/Error code</returns>
    ''' <remarks>https://www.pinvoke.net/default.aspx/kernel32.SetConsoleTitle</remarks>
    Private Declare Function SetConsoleTitle Lib "kernel32" Alias "SetConsoleTitleA" (ByVal lpConsoleTitle As String) As Long

    ''' <summary>
    ''' Parse command line argument string, check if param has argument, and then return argument from it
    ''' </summary>
    ''' <param name="param">Commandline arg switch</param>
    ''' <param name="cmd">Full Argument to check against</param>
    ''' <param name="arg">Argument from full string, returned ByRef</param>
    ''' <param name="QUOTEnclose">Hnadles enclosing quotes. -1=remove quotes, 0=ignore, 1=enclose in quotes. Used when expecting to handle paths as the args</param>
    ''' <returns>Argument found?</returns>
    ''' <remarks></remarks>
    Private Function ParseParamOrder(ByVal param As String, ByVal cmd As String, ByRef arg As String, Optional ByVal QUOTEnclose As SByte = 0) As Boolean
        Dim result As Boolean = False       'Argument found?        
        QUOTEnclose = Math.Sign(QUOTEnclose)
        'If param string contains argument, then replace switch to get argument, return ByRef, and return a found result
        If cmd.Contains(param) Then
            arg = cmd.Replace(param, "")
            If QUOTEnclose = 1 Then
                Dim _sub As String = Mid(arg, 1)
                If _sub <> strQUOT Then arg = strQUOT & arg
                Dim length = arg.Length
                _sub = Mid(arg, length)
                If _sub <> strQUOT Then arg = arg & strQUOT
            ElseIf QUOTEnclose = -1 Then
                arg = arg.Replace(strQUOT, "")
            End If
            result = True
        End If
        Return result
    End Function

    ''' <summary>
    ''' Given an A1N string from CLI, select the proper preset
    ''' </summary>
    ''' <param name="preset">Preset string</param>
    ''' <returns>Succesful parsing?</returns>
    ''' <remarks></remarks>
    Private Function SelectA1N_Preset(ByVal preset As String) As Boolean
        Dim result As Boolean = False                                           'Succesful?
        Try
            Const maxColumns As Byte = 2                                        'Max amount of preset columns
            Const maxRows As Byte = 6                                           'Max amount of preset rows
            Const maxSpecial As Byte = 2                                        'Max amount of special presets
            Const All As String = "All"                                         '"All" special preset string
            Const Dum As String = "Dum"                                         '"Dum" special preset string
            Const underscore As String = "_"                                    'Underscore separator (cell_time)
            'Const maxPresets As Byte = (maxColumns * maxRows) + maxSpecial     'Total amount of presets

            'Array of row/columns of preset radio button refs
            Dim Rad(maxRows, maxColumns) As System.Windows.Forms.RadioButton
            Rad(0, 0) = radTD_A1    'Row 0, Column 0
            Rad(0, 1) = radTD_B1    'Row 0, Column 1
            'Etc
            Rad(1, 0) = radTD_A2
            Rad(1, 1) = radTD_B2
            Rad(2, 0) = radTD_A3
            Rad(2, 1) = radTD_B3
            Rad(3, 0) = radTD_A4
            Rad(3, 1) = radTD_B4
            Rad(4, 0) = radTD_A5
            Rad(4, 1) = radTD_B5
            Rad(5, 0) = radTD_A6
            Rad(5, 1) = radTD_B6

            'Array of 2 special prsets, and refs
            Dim RadS(maxSpecial) As System.Windows.Forms.RadioButton
            RadS(0) = radTD_All 'All
            RadS(1) = radTD_Dum 'Dum

            Dim time As String = ""         'Time multiplicty, as string
            Dim t As Byte = 0               'Time value
            Dim underPos As Byte = 0        'Underscore pos
            Dim length = 0                  'Generic length of strings
            Dim preset2 = ""                'Generalized prset type (no underscore). 
            'Used to differentiate between normal presets (A1_N) and special (Dum/All)

            'If preset <> All Then
            underPos = InStr(preset, underscore)    'Find position of underscore
            length = underPos - 1                   'Lenght = pos-1
            preset2 = Mid(preset, 1, length)        'Get everything before underscore
            'Else
            'preset2 = All
            'End If

            'Handle preset strings
            Select Case preset2
                Case All
                    'Special all case
                    RadS(0).Select()                        'Select all

                    length = preset.Length                  'Get length of string
                    length = length - underPos              'Subtract length from startPos
                    time = Mid(preset, underPos + 1, length) 'Get mid string after underscore
                    t = TryParseErr_Byte(time)              'Convert to byte
                    'If t is out of range for THP_ID, throw error
                    If t < nudTD_M.Minimum Or t > nudTD_M.Maximum Then Throw New System.Exception("Time mulitplicity out of range for selected THP ID!")
                    nudTD_M.Value = t                       'Set multiplicity to t
                    result = True                           'Success!

                Case Dum
                    'Special dummy case
                    If RadS(1).Enabled = False Then Throw New System.Exception("Selected THP ID does not support Dum preset!")
                    RadS(1).Select()                        'Select dum

                    'Get the time mult
                    length = preset.Length                  'Get length of string
                    length = length - underPos              'Subtract length from startPos
                    time = Mid(preset, underPos + 1, length) 'Get mid string after underscore
                    t = TryParseErr_Byte(time)              'Convert to byte
                    'If t is out of range for THP_ID, throw error
                    If t < nudTD_M.Minimum Or t > nudTD_M.Maximum Then Throw New System.Exception("Time mulitplicity out of range for selected THP ID!")
                    nudTD_M.Value = t                       'Set multiplicity to t
                    result = True                           'Success!

                Case Else
                    'All other normal presets ("A1_N")                    
                    'Dim cell As String = Mid(preset, 0, 2)
                    'Dim time As String = Mid(preset, 2, 1)

                    'Dim col As String = Mid(cell, 0, 1)
                    'Dim row As String = Mid(cell, 1, 1)
                    'Dim c As Byte = TryParseErr_Byte(col)
                    'Dim r As Byte = TryParseErr_Byte(row)
                    'Dim t As Byte = TryParseErr_Byte(time)

                    underPos = InStr(preset, underscore)                'Find position of underscore
                    length = underPos - 1                               'Lenght = pos-1

                    Dim cell As String = Mid(preset, 1, length)         'Cell = everything before underscore
                    time = Mid(preset, underPos + 1)                    'Time = everything after

                    Dim col As String = Mid(cell, 1, 1)                 'Column of row should be 1st char
                    length = cell.Length - 1                            'Make length 0-based
                    Dim row As String = Mid(cell, 2, length)               'Row = everything else

                    'Convert strings to data
                    Dim c As Byte = 0                                   'Column value
                    Dim AplphaSuccess As Boolean = SelectA1N_ConvAlpha(col, c)  'Convert alphabet char to value, get successful result
                    'If couldn't convert alphabet char to value, throw error
                    If AplphaSuccess = False Then Throw New System.Exception("Failed to parse alphabet letter for cell column!")
                    Dim r As Byte = TryParseErr_Byte(row)               'Convert row to byte
                    t = TryParseErr_Byte(time)                          'Convert time to byte
                    'Row and column should be 0-based for array indices
                    r = r - 1
                    c = c - 1

                    'If button is disabled (invalid), throw error
                    If Rad(r, c).Enabled = False Then Throw New System.Exception("Selected THP ID does not support that row/column combination!")
                    Rad(r, c).Select()                                  'Select the proper radio button ref
                    'If t is out of range for THP_ID, throw error
                    If t < nudTD_M.Minimum Or t > nudTD_M.Maximum Then Throw New System.Exception("Time mulitplicity out of range for selected THP ID!")
                    nudTD_M.Value = t                                   'Set the multiplicity value to t
                    result = True                                       'Success!
            End Select
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, Microsoft.VisualBasic.MsgBoxStyle.Critical, "Preset parsing error!", True)
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Given a Alphabet letter from A1N notation, returns its value
    ''' </summary>
    ''' <param name="letter">Alphabet letter</param>
    ''' <param name="value">Its value, 1-based, returned ByRef</param>
    ''' <returns>Successful parsing?</returns>
    ''' <remarks></remarks>
    Private Function SelectA1N_ConvAlpha(ByVal letter As String, ByRef value As Byte) As Boolean
        Const Alphabet As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" 'Alphabet chars, in order
        Dim result As Boolean = False                           'Success?
        value = InStr(Alphabet, letter)                         'Find 1st/only occurrence of letter; return value ByRef
        If value <> 0 Then result = True 'If found, then success
        Return result
    End Function

    ''' <summary>
    ''' Parse crop settings CSV from Commandline
    ''' </summary>
    ''' <param name="crop">Crop CSV string</param>
    ''' <remarks></remarks>
    Private Sub CLI_HandleCropCSV(ByVal crop As String)
        Try
            'Initalize crop settings to minimum first, 
            'in order to prevent fighting against default preset and input validation
            txtTD_CX.Text = "0"
            txtTD_CY.Text = "0"
            txtTD_CW.Text = "1"
            txtTD_CH.Text = "1"
            txtTD_FS.Text = "0"
            txtTD_FE.Text = "1"

            'Array of MaskedTextBos refs, for each CSV value
            Dim boxes(5) As System.Windows.Forms.MaskedTextBox
            boxes(0) = txtTD_CX
            boxes(1) = txtTD_CY
            boxes(2) = txtTD_CW
            boxes(3) = txtTD_CH
            boxes(4) = txtTD_FS
            boxes(5) = txtTD_FE

            Const SEP As String = ","                       'Comma separate char
            Dim value As String = "'"                       'CSV entry
            Dim index As Byte = 0                           'CSV index
            Dim Pos As Byte = 1                             'Current string startpos
            Dim SEPPos As Byte = 0                          'Position of next SEP char
            Dim length As Byte = 0                          'Length for substring processing

            'Iterate through all CSV inices
            For index = 0 To 5 Step 1
                SEPPos = InStr(Pos, crop, SEP)                   'Find the pos of the SEP char
                If SEPPos = 0 Then SEPPos = crop.Length + 1 'If none found, assume EOL
                length = SEPPos - Pos                       'Length between pos and SEP char
                value = crop.Substring(Pos - 1, length)        'Fetch value (start at pos, get length)
                boxes(index).Text = value                   'Update the textbox ref with value
                Pos = SEPPos + 1                            'Set current pos to SEP char pos +1
            Next index
        Catch ex As Exception
            Log_MsgBox(ex, "Error parsing crop argument CSV in CLI_HandleCropCSV()!", Microsoft.VisualBasic.MsgBoxStyle.Critical, "Crop argument parsing error!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Hides THPFile label and combo box, THP Info Group box, THP Dec/Encoder boxes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideTHPData()
        txtRoot.Text = Nothing
        txtFFMPEG.Text = Nothing
        txtFFPlayTemp.Text = Nothing
        txtiView.Text = Nothing
        txtTHPConv.Text = Nothing

        lblTHPFile.Visible = False
        cmbTHP.Visible = False
        grpTHPInfo.Visible = False
        grpTHPDec.Visible = False
        grpTHPEnc.Visible = False
        'grpLog.Visible = False
    End Sub

    ''' <summary>
    ''' Retrieve the hard-coded THP data from the four data files, dumps into RAM (THPs array)
    ''' </summary>
    ''' <remarks>Similar to InitIMGData in setup from BreakGold Editor; same purpose</remarks>
    Public Sub InitTHPData()
        InitTHP_FSInfo()                         'Load fileset info

        Dim xFileData As StreamReader = Nothing  'Streamreader object for reading the data from the files
        Dim DataPath As String = txtDataDir.Text 'Path for set of data files
        Dim strEntry As String                   'String data read from the [File].ReadLine() method
        Dim bytItems As Byte                     'Number of items in the parallel arrays

        Dim bytCtr1 As Byte             'Generic Counter variable #1
        Dim bytCtr2 As Byte             'Generic Counter variable #2

        'Variables for processing the DATA file
        Const SEP As String = ","       'Constant variable for the ASCII character separating the sub entries per line
        Const SEP2 As String = ";"      'Constant variable ~                        ending the line
        Dim strVar As String            'String containing stringified numeric data (for processing the entries in the DATA file)
        Dim bytStart As Byte            'Start position in the string for extracting a subentry
        Dim bytEnd As Byte              'End position ~
        Dim bytLen As Byte              'The length of the string to extract
        Const bytDataEnt As Byte = 18   'Amount of entries per line

        Dim bytErrFlag As Byte = 0      'Counts the number of invalid entries, if = bytDataEnt bad entries, disable THPDec, ThpRip, and ThpEnc group boxes

        Try
            cmbTHP.Items.Clear()        'Clear all cmbThp items, in case of error/in order to reset when loading new THP data

            bytItems = 0
            xFileData = File.OpenText(DataPath & strPATHSEP & LISTING)   'Open the LISTING file

            'Count the amount of items
            While xFileData.EndOfStream() <> True
                strEntry = xFileData.ReadLine()                   'Read a line from the file
                bytItems += 1
            End While
            ReDim THPs(bytItems)                                  'Redim the THPs array appropriately            
            KillStream(xFileData)                                 'Close the LISTING file

            'Load all relative file paths into THPs(#).File
            xFileData = File.OpenText(DataPath & strPATHSEP & LISTING) 'Open the LISTING file
            For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all of the lines
                strEntry = xFileData.ReadLine()                     'Read a line
                THPs(bytCtr1).File = strEntry                       'Dump file paths into appropriate array entry
                cmbTHP.Items.Add(THPs(bytCtr1).File)                'Dump data into Combo box
            Next bytCtr1
            KillStream(xFileData)                                   'Close the LISTING file

            'Load all descriptions into THPs(#).Desc array
            xFileData = File.OpenText(DataPath & strPATHSEP & DESC) 'Open the DESC file
            For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all lines
                strEntry = xFileData.ReadLine()                     'Read a line
                THPs(bytCtr1).Desc = strEntry                       'Dump the data into the array slots
            Next bytCtr1
            KillStream(xFileData)                                   'Close the DESC file

            'Load all ctrl descriptions into THPs(#).visual.cdesc array
            xFileData = File.OpenText(DataPath & strPATHSEP & CDESC) 'Open the CDESC file
            For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all lines
                strEntry = xFileData.ReadLine()                     'Read a line
                THPs(bytCtr1).visual.CDesc = strEntry               'Dump the data into the array slots
            Next bytCtr1
            KillStream(xFileData)                                   'Close the DESC file
            '-------------------------
            'Get data from the DATA file

            'Load all AV data into THPs(#).visual and THPs(#).audial struct elements
            xFileData = File.OpenText(DataPath & strPATHSEP & DATA) 'Load the DATA file
            For bytCtr1 = 1 To bytItems Step 1                      'Iterate through all lines
                strEntry = xFileData.ReadLine()                     'Read a line

                bytStart = 1                                        'Init start pos
                'Parse each of the entries in each line
                bytErrFlag = 0
                For bytCtr2 = 1 To bytDataEnt Step 1                'Iterate through all entries in line

                    'If not the last data entry in line, find the position of the SEP character (,), else SEP2 character (;)
                    If bytCtr2 <> bytDataEnt Then bytEnd = InStr(bytStart, strEntry, SEP) Else bytEnd = InStr(bytStart, strEntry, SEP2) 'Record the position of the next SEP1 character,
                    bytLen = (bytEnd - bytStart)             'Get the length of the sub entry (subtract End from Start)
                    strVar = Mid(strEntry, bytStart, bytLen) 'Extract the sub entry via MID command

                    'If the entry is 0, increment error counter
                    If bytCtr2 = 14 Then
                        If TryParseErr_Single(strVar) = 0 Then bytErrFlag += 1
                    Else
                        If TryParseErr_UShort(strVar) = 0 Then bytErrFlag += 1
                    End If

                    'Allocate the extracted value into the appropriate array data fields based on index
                    Select Case bytCtr2

                        'Total THP video width/height
                        Case 1
                            THPs(bytCtr1).visual.TDim.width = TryParseErr_UShort(strVar)
                        Case 2
                            THPs(bytCtr1).visual.TDim.height = TryParseErr_UShort(strVar)

                            'THP subvideo array info
                            'Row, Col, R*C, Multiplicity, mult optional?, r*c*m
                        Case 3
                            THPs(bytCtr1).visual.THPinfo.row = TryParseErr_Byte(strVar)
                        Case 4
                            THPs(bytCtr1).visual.THPinfo.col = TryParseErr_Byte(strVar)
                        Case 5
                            THPs(bytCtr1).visual.THPinfo.subV = TryParseErr_Byte(strVar)
                        Case 6
                            THPs(bytCtr1).visual.THPinfo.mult = TryParseErr_Byte(strVar)
                        Case 7
                            THPs(bytCtr1).visual.THPinfo.subVT = TryParseErr_Byte(strVar)

                            'Subvideo info
                            'Subvideo width and height
                        Case 8
                            THPs(bytCtr1).visual.SDim.width = TryParseErr_UShort(strVar)
                        Case 9
                            THPs(bytCtr1).visual.SDim.height = TryParseErr_UShort(strVar)

                            'Frame counts for each subvideo, total THP video
                        Case 10
                            THPs(bytCtr1).visual.Frames.subframes = TryParseErr_UShort(strVar)
                        Case 11
                            THPs(bytCtr1).visual.Frames.totframes = TryParseErr_UShort(strVar)

                            'Width and height of padding
                        Case 12
                            THPs(bytCtr1).visual.Padding.width = TryParseErr_UShort(strVar)
                        Case 13
                            THPs(bytCtr1).visual.Padding.height = TryParseErr_UShort(strVar)
                        Case 14
                            'FPS as single                            
                            THPs(bytCtr1).visual.FPS = TryParseErr_Single(strVar)

                            'Control/Audio info
                            'Has control signal?, has audio?, is stereo?, audio freq
                        Case 15
                            THPs(bytCtr1).visual.Ctrl = BitToBool(TryParseErr_Byte(strVar))
                        Case 16
                            THPs(bytCtr1).audial.has = BitToBool(TryParseErr_Byte(strVar))
                        Case 17
                            THPs(bytCtr1).audial.Stereo = BitToBool(TryParseErr_Byte(strVar))
                        Case 18
                            THPs(bytCtr1).audial.freq = TryParseErr_UShort(strVar)
                    End Select

                    bytStart = bytEnd + 1 'Increment the start position to 1 past the located SEP1 character
                Next bytCtr2 'Repeat for the other entries in the line

                'Set bad flag as appropriately
                If bytErrFlag = bytDataEnt Then
                    THPs(bytCtr1).Bad = True
                Else
                    THPs(bytCtr1).Bad = False
                End If

            Next bytCtr1                'Repeat for all lines
            KillStream(xFileData)       'Close the DATA file

            'Set index to 0, manually fire a SelectedIndexChanged
            'Since 1st ID is (supposed to be) a dummy entry, will hide some invalid controls.
            'When user selects a valid entry, will be restored etc
            cmbTHP.SelectedIndex = 0
            cmbTHP_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            KillStream(xFileData)       'Kill any lingering streams
            HideTHPData()               'Due to failure to load THP Data fully, hide THP Data form
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Data file parsing/ I/O error!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Retrieves FileSet info for this set of THPs
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitTHP_FSInfo()
        Dim xFileData As StreamReader = Nothing                         'Streamreader object for reading the data from the files
        Dim DataPath As String = txtDataDir.Text                        'Path for set of data files
        Dim strEntry As String                                          'String data read from the [File].ReadLine() method

        Const bytItems As Byte = 5                                      'Max amount of fields
        Dim fields(bytItems) As System.Windows.Forms.TextBox            'Array of field textboxes (in order)
        fields(0) = txtFS_Game
        fields(1) = txtFS_Desc
        fields(2) = txtFS_Author
        fields(3) = txtFS_Ver
        fields(4) = txtFS_Date

        Dim bytCtr1 As Byte                                             'Generic Counter variable #1
        Try
            'Iterate through all fields (0-based), reset text
            For bytCtr1 = 0 To (bytItems - 1) Step 1
                fields(bytCtr1).Text = ""
            Next bytCtr1

            xFileData = File.OpenText(DataPath & strPATHSEP & INFO)     'Open the INFO file

            'Loop while not EOF
            bytCtr1 = 0
            While xFileData.EndOfStream() = False
                strEntry = xFileData.ReadLine()                         'Read a line from the file
                fields(bytCtr1).Text = strEntry                         'Update text
                bytCtr1 += 1                                            'Increase iterator
            End While
            KillStream(xFileData)                                       'Kill lingering stream
        Catch ex As Exception
            KillStream(xFileData)                                       'Kill any lingering streams
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "FileSet info file parsing/ I/O error!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles dumping the data from the data files (now in RAM) into the appropriate fields for the THPInfo Group box, when the combo box has been changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbTHP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTHP.SelectedIndexChanged
        Try
            'Index in the THP combo box

            'Update the THP File label with current ID
            lblTHPFile.Text = "THP File" & strNL & "(ID = " & cmbTHP.SelectedIndex.ToString("D3") & ")"
            Dim bytEntry As Byte = cmbTHP.SelectedIndex + 1

            'Set THPEnc and THPDec group boxes to visible as appropriately, depending on bad state
            Dim state As Boolean = THPs(bytEntry).Bad
            state = Not (state)
            grpTHPEnc.Visible = state
            grpTHPDec.Visible = state

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

            'Convert FPS properly (bugfix for issue #16)
            Dim fps As Single = THPs(bytEntry).visual.FPS                                       'Get FPS single value
            Dim fps_string As String = Single_ToString(fps, Single_ToString_Types._SINGLE)      'Convert to string properly
            txtVC_F.Text = fps_string                                                           'Update text
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
            HandleRipTimeMasks()                            'Updates the masks for the start/end lengths for ripping (time)
            txtTE_D.Text = txtVF_T.Text.Length.ToString()   'Set default value in THPEnc for digits, based on the string.length of the video's total frames
            ReselectPreset()
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in cmbTHP_SelectedIndexChanged!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Helper function for cmbTHP_SelectedIndexChanged which reselects the current preset when changing the cmbTHP index
    ''' This updates the crop settings with the current preset
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReselectPreset()
        Const maxColumns As Byte = 2                                        'Max amount of preset columns
        Const maxRows As Byte = 6                                           'Max amount of preset rows
        Const maxSpecial As Byte = 2                                        'Max amount of special presets
        Const maxPresets As Byte = (maxColumns * maxRows) + maxSpecial      'Total amount of presets

        'Array of preset radio buttons
        Dim Rad(maxPresets) As System.Windows.Forms.RadioButton
        Rad(0) = radTD_A1
        Rad(1) = radTD_A2
        Rad(2) = radTD_A3
        Rad(3) = radTD_A4
        Rad(4) = radTD_A5
        Rad(5) = radTD_A6

        Rad(6) = radTD_B1
        Rad(7) = radTD_B2
        Rad(8) = radTD_B3
        Rad(9) = radTD_B4
        Rad(10) = radTD_B5
        Rad(11) = radTD_B6

        Rad(12) = radTD_All
        Rad(13) = radTD_Dum

        'Iterate through all radio buttons
        Dim i As Byte = 0
        For i = 0 To (maxPresets - 1) Step 1

            'Get this current checked state
            Dim state As Boolean = Rad(i).Checked
            If state = True Then
                'If true, set to false then true, to force a checked change event
                Rad(i).Checked = False
                Rad(i).Checked = True
                Exit For
            End If
        Next i
    End Sub

    ''' <summary>
    ''' Does this THP have padding?
    ''' </summary>
    ''' <returns>Padding?</returns>
    ''' <remarks></remarks>
    Private Function THPHasPad() As Boolean
        Dim outp As Boolean = False                 'Output
        Try
            Dim d As Dims                               'Dims
            d.width = TryParseErr_UShort(txtVP_W.Text)  'Width=Video padding width
            d.height = TryParseErr_UShort(txtVP_H.Text) 'Height=Video padding height

            'If both dims are not zero, then hasPadding
            If d.width <> 0 And d.height <> 0 Then outp = True
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in THPHasPad()", True)
        End Try
        Return outp
    End Function

    ''' <summary>
    ''' Does this THP have audio?
    ''' </summary>
    ''' <returns>Audio?</returns>
    ''' <remarks></remarks>
    Private Function THPHasAudio() As Boolean
        Dim has As String = txtA_A.Text                 'Get hasAudio field as string
        Dim hasAudio As Boolean = BoolStrToBool(has)    'Does vid have audio? (Convert has to bool)
        Return hasAudio
    End Function

    '===========================
    'Options Tab stuff

    ''' <summary>
    ''' Hnadles loading the THP root dir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoadRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseRoot.Click
        Try
            'Load the LoadTHPRoot Load Dialog Box, user selects root directory of THP
            AssignSelPath_FBD(LoadTHPRoot, txtRoot)
            If LoadTHPRoot.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            txtRoot.Text = LoadTHPRoot.SelectedPath    'Dump the path into the textbox, for later retrieval
            CheckPathsSet()                             'Handle enabling THP Tab
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnLoadRoot_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles loading Irfanview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btniView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btniView.Click
        'Load the LoadiView ofd, user selects i_view32.exe
        Try
            'Set initial directory to path if set; else to either "C:\Program Files (x86)" or "C:\Program Files".
            'Alt path used for compatibility with older, 32-bit Windows
            AssignInitDir_OFD(LoadiView, txtiView, "C:\Program Files (x86)", "C:\Program Files")
            If LoadiView.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            txtiView.Text = LoadiView.FileName      'Dump the path into the textbox, for later retrieval
            CheckPathsSet()                         'Handle enabling THP Tab
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btniView_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles loading the FFMPeg exe path
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseFFMPEG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFFMPEG.Click
        Try
            'Load the LoadFMPegRoot Load Dialog Box, user selects root directory of FFMpeg exes
            AssignSelPath_FBD(LoadFFMPEGRoot, txtFFMPEG, My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
            If LoadFFMPEGRoot.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            txtFFMPEG.Text = LoadFFMPEGRoot.SelectedPath    'Dump the path into the textbox, for later retrieval
            CheckPathsSet()                             'Handle enabling THP Tab
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnBrowseFFMpeg_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles loading the FFPlay working directory
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseFFPlayTemp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFFPlayTemp.Click
        Dim DL_Folder As String = GetDownloadsFolder()
        Try
            'Load the LoadFFPlayWork Load Dialog Box, user selects working directory            
            AssignSelPath_FBD(LoadFFPlayWork, txtFFPlayTemp, DL_Folder)
            If LoadFFPlayWork.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            txtFFPlayTemp.Text = LoadFFPlayWork.SelectedPath    'Dump the path into the textbox, for later retrieval
            CheckPathsSet()                                     'Handle enabling THP Tab
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnBrowseFFPlayTemp_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles loading the THPConv exe file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseTHPConv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseTHPConv.Click
        'Load the LoadTHPConv ofd, user selects thpconv.exe
        Try
            'Set initial directory to path if set; else to either "C:\Program Files (x86)" or "C:\Program Files".
            'Alt path used for compatibility with older, 32-bit Windows
            AssignInitDir_OFD(LoadTHPConv, txtTHPConv, "C:\Program Files (x86)", "C:\Program Files")
            If LoadTHPConv.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            txtTHPConv.Text = LoadTHPConv.FileName      'Dump the path into the textbox, for later retrieval
            CheckPathsSet()                             'Handle enabling THP Tab
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnBrowseTHPConv_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles loading the directory for the DB datafiles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDataDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataDir.Click
        Try
            'Load the LoadDataDir Load Dialog Box, user selects root directory for data files                        
            AssignSelPath_FBD(LoadDataDir, txtDataDir, strPATH)
            If LoadDataDir.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            txtDataDir.Text = LoadDataDir.SelectedPath  'Dump the path into the textbox, for later retrieval
            InitTHPData()                               'Load THP data from new data dir
            CheckPathsSet()                             'Handle enabling THP Tab
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnDataDir_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' If the correpsonding path text box for a FolderBrowserDialog is set, use it's selectedpath as initial directory; else set selectedPath to a specific directory
    ''' </summary>
    ''' <param name="fbd">FolderBrowserDialog ref</param>
    ''' <param name="dir">Corresponding textbox with path</param>
    ''' <param name="defaultDir">Default directory (if any) if not set. Default is root</param>
    ''' <remarks></remarks>
    Private Sub AssignSelPath_FBD(ByRef fbd As System.Windows.Forms.FolderBrowserDialog, ByVal dir As System.Windows.Forms.TextBox, Optional ByVal defaultDir As String = "")
        'If the textbox text set (NOT nothing and NOT empty), then set selectedPath to it; else set to String.Empty
        If dir.Text <> Nothing And dir.Text <> String.Empty Then
            fbd.SelectedPath = dir.Text
        Else
            'If deafultDir is "", then set to String.Empty (root dir); else whatever
            If defaultDir = "" Then defaultDir = String.Empty
            fbd.SelectedPath = defaultDir
        End If
    End Sub

    ''' <summary>
    ''' If the correpsonding filepath text box for an OpenFileDialog is set, use it's path as initial directory; else set initial directory (main or alt if that doesn't exist)
    ''' </summary>
    ''' <param name="ofd">OpenFileDialog ref</param>
    ''' <param name="selPath">Corresponding path textbox</param>
    ''' <param name="initDir">Initial directory to use if path is not set</param>
    ''' <param name="initDirAlt">Alternate to initDir if DNE</param>
    ''' <remarks></remarks>
    Private Sub AssignInitDir_OFD(ByRef ofd As System.Windows.Forms.OpenFileDialog, ByVal selPath As System.Windows.Forms.TextBox, ByVal initDir As String, ByVal initDirAlt As String)
        If selPath.Text <> Nothing And selPath.Text <> String.Empty Then
            'If the textbox text set (NOT nothing and NOT empty), then set InitialDirectory to it            
            ofd.InitialDirectory = Path.GetDirectoryName(selPath.Text)
        Else
            'If the textbox not set, then set InitialDirectory to InitDir. If that DNE, then set to initDirAlt
            Dim exists As Boolean = False
            exists = My.Computer.FileSystem.DirectoryExists(initDir)
            If exists = True Then ofd.InitialDirectory = initDir Else ofd.InitialDirectory = initDirAlt
        End If
    End Sub

    'Valid Thwimp.ini format:
    '[Thwimp.ini va.b.c.e]   Magic header, a,b,c,d = version numbers (future compatibility purps)

    'Followed by variable name = value

    'Variable name  Usage                       Var type    Default value
    'THPRoot        THP Root Path               String      Whatever
    'FFMpegDir      FFMPEG Exe Path             String      Whatever
    'FFplay_wdir    FFPlay Working Directory    String      Whatever
    'irfanview      Irfanview Exe               String      C:\Dir\i_view32.exe (MUST be i_view32.exe)
    'thpconv        THPConv exe                 String      C:\Dir\thpconv.exe (MUST be thpconv.exe)
    'dataDir        Data files dir              String      0 (=exe path)
    'audio          Audio?                      Bit string  1
    'audio_bgm      Elevator music?             Bit string  1
    'log_msgBox     Ignore nfo mbox during Enc? Bit string  1
    'log_Full       Full logs (include cmds?)   Bit string  0

    ''' <summary>
    ''' Handles loading the INI settings file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoadSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadSettings.Click
        Try
            'Load the ofdLoadSettings ofd, user selects thwimp.ini
            ofdLoadSettings.InitialDirectory = strPATH  'InitialDirectory is exe path
            If ofdLoadSettings.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            LoadSettings2(ofdLoadSettings.FileName)
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnLoadSettings_Click/LoadSettings!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Entrypoint to actually load the settings, given an INI file path
    ''' </summary>
    ''' <param name="File">INI File path</param>
    ''' <returns>Success?</returns>
    ''' <remarks></remarks>
    Private Function LoadSettings2(ByVal File As String) As Boolean
        'Parse its contents; if succesful loading, then Handle enabling THP Tab and InitTHPData from new data dir path obtained
        Dim success As Boolean = False
        Try
            success = LoadSettings(File)
            If success Then
                InitTHPData()
                CheckPathsSet()
            Else
                Throw New System.Exception("Failed to load INI file!")
            End If
        Catch ex As Exception
            If CLI_MODE Then Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnLoadSettings_Click/LoadSettings!", True)
        End Try
        Return success
    End Function

    ''' <summary>
    ''' Parses the INI settings file, initalizes settings
    ''' </summary>
    ''' <param name="_file">INI file</param>
    ''' <returns>Success?</returns>
    ''' <remarks></remarks>
    Private Function LoadSettings(ByVal _file As String)
        Dim Success As Boolean = False  'Succesful parsing?
        Dim xFileData As StreamReader = Nothing 'Streamreader object for parsing the INI file
        Dim _error As String            'Text for logs/errors
        Dim line As Integer = 1         'Current line number in INI text file (used for logging)
        Try
            Dim strEntry As String      'ReadLine from INI file            
            Dim version As String       'Thwimp INI version
            Dim var As String           'Options variable
            Const SEP As String = "="   'Separator delimitor char between variable/value (=)
            Dim sepPt As Integer = 0    'Position SEP in line
            Dim valStr As String        'Variable value interpreted as string
            Dim valBln As Boolean       'Variable value interpreted as boolean

            'Open the INI file
            Log("Loading INI settings: " & _file, MsgBoxStyle.Information)
            xFileData = File.OpenText(_file)

            'Read first line (should be Thwimp INI magic header;
            'see block-comment structure notes above ("Valid Thwimp.ini format")
            strEntry = xFileData.ReadLine()

            'Does the first line contain magic header? (Contains "[Thwimp.ini v" & "]"?)            
            If ((strEntry.Contains("[Thwimp.ini v")) And (strEntry.Contains("]"))) Then
                'If so, get version from header (remove everything except version from string)
                _error = "Line " & line & ": Found magic header, " & strEntry
                Log(_error, MsgBoxStyle.Information)
                version = strEntry.Replace("[Thwimp.ini v", "")
                version = version.Replace("]", "")
            Else
                'If does NOT, throw error; invalid INI file!
                Throw New System.Exception("Invalid INI file! Does not contain valid magic header.")
            End If

            '!@ Do future enuming/whatever compatibility stuff here based on the version parts found,
            'as this INI structure evolves
            Log("INI version: " & version, MsgBoxStyle.Information)

            'Read all other lines until EOF
            While xFileData.EndOfStream = False

                'Read this line, increment line
                strEntry = xFileData.ReadLine()
                line += 1

                'If it does NOT contain and SEP, then not a variable definition; throw error
                If strEntry.Contains(SEP) = False Then Throw New System.Exception("Syntax error: variable assignemnt not found!")

                'Has SEP, get variable and its value definition
                sepPt = strEntry.IndexOf(SEP)       'Find position of SEP char

                'Get variable
                var = Mid(strEntry, 1, sepPt)       'Get variable name as everything before SEP
                var = Trim(var)                     'Remove leading/trailing spaces

                'Get value
                valStr = Mid(strEntry, sepPt + 2)   'Get value as string (everything after SEP sign)
                valStr = Trim(valStr)               'Remove leading/trailing spaces

                'Handle each valid variable type;
                'parse its value and dump into appropriate application variables for settings initz
                'See block-comment structure notes above ("Valid Thwimp.ini format") for valid variables and value meanings

                'Is this a valid variable/syntax?
                Dim valid As Boolean = True
                Select Case var
                    'These are abs directory/file paths
                    Case "THPRoot"
                        txtRoot.Text = valStr
                    Case "FFMpegDir"
                        txtFFMPEG.Text = valStr
                    Case "FFplay_wdir"
                        txtFFPlayTemp.Text = valStr
                    Case "irfanview"
                        txtiView.Text = valStr
                    Case "thpconv"
                        txtTHPConv.Text = valStr
                    Case "dataDir"
                        'Data Directory needs special handling; if "0", then use exe path; else use value
                        If valStr = "0" Then
                            txtDataDir.Text = strPATH
                        Else
                            txtDataDir.Text = valStr
                        End If

                        'These are boolean options
                        'Parse each string value as byte, then convert its bit value to bool
                    Case "audio"
                        valBln = BitToBool(TryParseErr_Byte(valStr))
                        chkAudio.Checked = valBln
                    Case "audio_bgm"
                        valBln = BitToBool(TryParseErr_Byte(valStr))
                        chkEMusic.Checked = valBln
                    Case "log_msgBox"
                        valBln = BitToBool(TryParseErr_Byte(valStr))
                        chkMsg.Checked = valBln
                    Case "log_Full"
                        valBln = BitToBool(TryParseErr_Byte(valStr))
                        chkLogFull.Checked = valBln

                    Case Else
                        'Everything else invalid, ignore, but log syntax error as warning
                        valid = False
                        _error = "Line " & line & ": variable syntax error (" & strEntry & ")"
                        Log(_error, MsgBoxStyle.Exclamation)
                End Select

                'Log this line if valid
                If valid Then
                    _error = "Line " & line.ToString() & ": " & strEntry
                    Log(_error, MsgBoxStyle.Information)
                End If
            End While

            'Successful parsing!
            KillStream(xFileData)
            Success = True
        Catch ex As Exception
            'If error, close lingering stream, log error message + INI Line number that threw error
            KillStream(xFileData)
            _error = ex.Message & strNL & "INI line: " & line
            Log_MsgBox(ex, _error, MsgBoxStyle.Critical, "Error loading settings INI file!", True)
        End Try

        'Return success
        Return Success
    End Function

    ''' <summary>
    ''' Handles saving the INI settings file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSaveSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSettings.Click
        Try
            'Load the ofdSaveSettings ofd, user selects thwimp.ini
            ofdSaveSettings.InitialDirectory = strPATH  'Set inital dir to exe path
            If ofdSaveSettings.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

            'Save settings; if success, then display msgbox
            Dim success As Boolean = SaveSettings(ofdSaveSettings.FileName)
            If success Then Log_MsgBox(Nothing, "Succesfully saved INI settings to " & ofdSaveSettings.FileName & "!", MsgBoxStyle.Information, "INI saved", True)
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnSaveSettings_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Dumps settings into valid INI file
    ''' </summary>
    ''' <param name="_file">INI file</param>
    ''' <returns>Success?</returns>
    ''' <remarks></remarks>
    Private Function SaveSettings(ByVal _file As String)
        Dim Success As Boolean = False              'Successful saving?
        Dim xFileData As StreamWriter = Nothing     'Streamwriter object for writing INI data 
        Try
            Dim strEntry As String      'Line to write
            Dim val As Byte             'Value, as byte
            Dim version As String       'INI version
            Const sep As String = " = " 'Variable assignment separator

            'StreamWriter for (over)writing INI settings (as ASCII)
            xFileData = New StreamWriter(_file, False, System.Text.Encoding.ASCII)

            'Version of this exe
            version = ProductVersion

            'Write magic header ("[Thwimp.ini va.b.c.d]")
            strEntry = "[Thwimp.ini v" & version & "]"
            xFileData.WriteLine(strEntry)

            '!@ Do future enuming/whatever compat stuff here based on the version parts found

            'Write variables
            'File/Directory strings
            SaveSettings_WriteVar(xFileData, "THPRoot", sep, txtRoot.Text)
            SaveSettings_WriteVar(xFileData, "FFMpegDir", sep, txtFFMPEG.Text)
            SaveSettings_WriteVar(xFileData, "FFplay_wdir", sep, txtFFPlayTemp.Text)
            SaveSettings_WriteVar(xFileData, "irfanview", sep, txtiView.Text)
            SaveSettings_WriteVar(xFileData, "thpconv", sep, txtTHPConv.Text)

            'Data directory is special:
            'If it is the exe path, write "0"; else path
            If txtDataDir.Text = strPATH Then
                strEntry = "0"
            Else
                strEntry = txtDataDir.Text
            End If
            SaveSettings_WriteVar(xFileData, "dataDir", sep, strEntry)

            'Bits
            SaveSettings_Bool(xFileData, "audio", sep, chkAudio.Checked)
            SaveSettings_Bool(xFileData, "audio_bgm", sep, chkEMusic.Checked)
            SaveSettings_Bool(xFileData, "log_msgBox", sep, chkMsg.Checked)
            SaveSettings_Bool(xFileData, "log_Full", sep, chkLogFull.Checked)

            'Sucessful writing!
            KillStream(xFileData, False, _file)
            Success = True
        Catch ex As Exception
            'Delete corrupted INI file if still exists onFailure
            KillStream(xFileData, True, _file)
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error loading settings INI file!", True)
        End Try
        Return Success
    End Function

    ''' <summary>
    ''' Helper function for SaveSettings, which writes a variable definition entry
    ''' </summary>
    ''' <param name="xFileData">StreamWrite for file</param>
    ''' <param name="var">Variable string</param>
    ''' <param name="sep">Assignment char/sep</param>
    ''' <param name="val">Variable value string</param>
    ''' <remarks></remarks>
    Private Sub SaveSettings_WriteVar(ByRef xFileData As StreamWriter, ByVal var As String, ByVal sep As String, ByVal val As String)
        'Line to write: variable + separator + value
        Dim line As String = var & sep & val
        xFileData.WriteLine(line)
    End Sub

    ''' <summary>
    ''' Helper function for SaveSettings, which writes a variable definition entry for a boolean variable
    ''' </summary>
    ''' <param name="xFileData">StreamWrite for file</param>
    ''' <param name="var">Variable string</param>
    ''' <param name="sep">Assignment char/sep</param>
    ''' <param name="val">Variable boolean value</param>
    ''' <remarks></remarks>
    Private Sub SaveSettings_Bool(ByRef xFileData As StreamWriter, ByVal var As String, ByVal sep As String, ByVal val As Boolean)
        Dim v As Byte = BoolToBit(val)          'Convert bool to bit
        Dim strEntry As String = v.ToString()   'Convert bit to string
        'Write variable
        SaveSettings_WriteVar(xFileData, var, sep, strEntry)
    End Sub

    ''' <summary>
    ''' On checking of chkAudio, toggle chkEMusic.Enabled
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkAudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAudio.CheckedChanged
        chkEMusic.Enabled = chkAudio.Checked
    End Sub

    ''' <summary>
    ''' If the options have been filled in, enable elements in THP tab
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckPathsSet()
        'Options det to be filled if something
        If (txtRoot.Text <> Nothing) And (txtFFMPEG.Text <> Nothing) And (txtiView.Text <> Nothing) And (txtTHPConv.Text <> Nothing) And (txtFFPlayTemp.Text <> Nothing) And (txtDataDir.Text <> Nothing) Then
            'Make everything in the THP tab visible now (THPFile label and combo box, whole THP Info group box, Log group)
            btnLogClear.PerformClick()
            lblTHPFile.Visible = True
            cmbTHP.Visible = True
            grpTHPInfo.Visible = True
            'grpLog.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' 'Handles clicking of About button, showing the box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbout.Click
        Try
            If chkAudio.Checked Then My.Computer.Audio.Play(My.Resources.EagleSoft, AudioPlayMode.Background) 'Play "EagleSoft Ltd"
            About.ShowDialog()                                                          'Show the about box
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error with showing About box!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles btnWeb click (goto EagleSoft Ltd. Thwimp webpage)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWeb.Click
        NavigateWebURL("http://www.eaglesoftltd.com/retro/Nintendo-Wii/thwimp")
    End Sub

    ''' <summary>
    ''' Handles btnWiki click (goto MKWiiki Thwimp article)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnWiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWiki.Click
        NavigateWebURL("http://wiki.tockdom.com/wiki/Thwimp")
    End Sub

    ''' <summary>
    ''' Handles btnManual click (goto Thwimp Github readme)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        NavigateWebURL("https://github.com/Tamk1s/Thwimp/blob/master/README.md")
    End Sub

    ''' <summary>
    ''' Handles btnRelease click (goto Github releases page)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        NavigateWebURL("https://github.com/Tamk1s/Thwimp/releases")
    End Sub

    ''' <summary>
    ''' Properly loads the default browser and goto URL
    ''' </summary>
    ''' <param name="URL">URL to load</param>
    ''' <remarks>
    ''' https://faithlife.codes/blog/2008/01/using_processstart_to_link_to/
    ''' https://stackoverflow.com/a/15192260
    ''' </remarks>
    Private Sub NavigateWebURL(ByVal URL As String)
        Try
            'Show AppStarting cursor, to prevent application hang; then goto URL
            Me.Cursor = Cursors.AppStarting
            Process.Start(URL)
        Catch ex As Exception
            'Catch but don't handle fake errors (see blog article)
        End Try
        'Restore default cursor
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Handles btnCmdline click (displays command line options, saves to text file for ref)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCmdline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCmdline.Click
        CmdHelp()
    End Sub

    ''' <summary>
    ''' Generates text for CLI explanation. If CLI_MODE, just Log text; else show Help form and dump text into it
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CmdHelp()
        Dim msg As System.Text.StringBuilder = New System.Text.StringBuilder

        msg.Append("Thwimp CLI syntax:")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("Main actions:")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("Rip:" & strNL)
        msg.Append("thwimp.exe rip /s=options.ini /t=NNN [[/p=A1_N_preset] || [/c=csv_cropvals]] /n=snd_bit /o=Output_folder")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("View:" & strNL)
        msg.Append("thwimp.exe view /s=options.ini /t=NNN [[/p=A1_N_preset] || [/c=csv_cropvals]] /n=snd_bit")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("Encode:" & strNL)
        msg.Append("thwimp.exe encode /s=options.ini /t=NNN /f=trunc_frame_val[,digs] /q=jpgqual /i=input_folder [/o=output_folder]")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("Help:" & strNL)
        msg.Append("thwimp.exe /mh" & strNL)
        msg.Append("thwimp.exe /help" & strNL)
        msg.Append("thwimp.exe /h" & strNL)
        msg.Append("thwimp.exe /?" & strNL)
        msg.Append("thwimp.exe [invalid args]" & strNL)
        msg.AppendLine() : msg.AppendLine()

        msg.Append("Param values:")
        msg.AppendLine() : msg.AppendLine()
        msg.Append("/s=options.ini" & strNL)
        msg.Append("INI file with Options paths (CRLF delimited). See Thwimp manual about INI data format")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("/t=NNN" & strNL)
        msg.Append("ID of THP files to use from datafiles" & strNL)
        msg.Append("ID From THP file combo box" & strNL)
        msg.Append("0-based, 3-digit ID")
        msg.AppendLine() : msg.AppendLine() : msg.AppendLine()

        msg.Append("/p=A1_N_preset" & strNL)
        msg.Append("Valid preset string ID to use from Crop Setting section in THP Viewer/Ripper section")
        msg.AppendLine() : msg.AppendLine()
        msg.Append("Valid A1_N values" & strNL)
        msg.Append("A1_N to A6_N, B1_N to B6_N" & strNL)
        msg.Append("MS Excel A1N notation for cells, _N = multiplicity ID for each cell (0 to max)")
        msg.AppendLine() : msg.AppendLine()
        msg.Append("Special values" & strNL)
        msg.Append("All_N = All special preset" & strNL)
        msg.Append("Dum_N = Dummy special preset")
        msg.AppendLine() : msg.AppendLine() : msg.AppendLine()

        msg.Append("/c=csv_cropvals" & strNL)
        msg.Append("Manual crop settings values (csv, no spaces)")
        msg.AppendLine() : msg.AppendLine()
        msg.Append("xpos" & strNL)
        msg.Append("ypos" & strNL)
        msg.Append("width" & strNL)
        msg.Append("height" & strNL)
        msg.Append("time_start" & strNL)
        msg.Append("time_end")
        msg.AppendLine() : msg.AppendLine() : msg.AppendLine()

        msg.Append("/n=snd_bit" & strNL)
        msg.Append("Use DirectSound? (0=false, 1=true)")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("/o=Output_folder" & strNL)
        msg.Append("Output folder to place ripped/generated files" & strNL)
        msg.Append("May be optional based on the Thwimp CLI action" & strNL)
        msg.Append("Required for rip mode. Rip mode expects a folder path + MP4 filename (" & strQUOT & "C:\Folder\filename.mp4" & strQUOT & ")")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("/f=trunc_frame_val[,digs]" & strNL)
        msg.Append("Number of frames to truncate to (per multiplicity), Number of digits for JPG filenaming (optional)" & strNL)
        msg.Append("If digits unused, will use digsOf(trunc_frame * multiplicity)")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("/q=jpgqual" & strNL)
        msg.Append("JPG Quality as percent (0-100, no % sign)")
        msg.AppendLine() : msg.AppendLine()

        msg.Append("/i=input_folder" & strNL)
        msg.Append("Input folder for THP Encoding input files")

        Dim mess As String = msg.ToString()

        If CLI_MODE Then
            'Dump help into log if CLI mode
            Log_MsgBox(Nothing, mess, MsgBoxStyle.Information, "Thwimp CLI", True)
        Else
            'If NOT CLI MODE, then dump text into form, show it
            Help.helpBox.Text = mess
            Help.Show()
        End If
    End Sub

    '===========================
    'THP Viewer/Ripper group box

    ''' <summary>
    ''' Handle THP playback
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click
        Try
            'Total/current progress for progress bar.
            'Index(0)=current progress, Index(1)=max progress
            Dim TtlPrg(2) As Single
            Dim CurPrg(2) As Single

            'Dim startInfo As ProcessStartInfo
            'startInfo = New ProcessStartInfo
            Dim Envvar(1) As String             'Envvar array
            Dim Envval(1) As String             'Corresponding value array

            'Bug info: https://forum.videohelp.com/threads/388189-ffplay-WASAPI-can-t-initialize-audio-client-error
            'If DirectSound option checked, set SDL_AUDIODRIVE = directsound. This is a workaround to a ffmpeg bug to allow audio
            If chkRip_DSound.Checked = True Then
                'https://social.msdn.microsoft.com/Forums/vstudio/en-US/a18210d7-44f4-4895-8bcc-d3d1d26719e5/setting-environment-variable-from-vbnet?forum=netfxbcl
                'set SDL_AUDIODRIVER=directsound
                'startInfo.EnvironmentVariables("SDL_AUDIODRIVER") = "directsound"
                Envvar(1) = "SDL_AUDIODRIVER"
                Envval(1) = "directsound"
            Else
                Envvar = Nothing
                Envval = Nothing
            End If

            'audio/video filter data
            Dim type As Boolean = chkRipDumF.Checked            'Type of ripping to do. True=Rip dummy frames
            Dim x As String = txtTD_CX.Text                     'Crop xpos
            Dim y As String = txtTD_CY.Text                     'Crop ypos
            Dim w As String = txtTD_CW.Text                     'Crop width
            Dim h As String = txtTD_CH.Text                     'Crop height            

            'Audio info. In seconds (so start/end pt /FPS = seconds)
            Dim _start As UShort = TryParseErr_UShort(txtTD_FS.Text)    'frame start
            Dim _end As UShort = TryParseErr_UShort(txtTD_FE.Text)      'frame end
            Dim FPS As Single = TryParseErr_Single(txtVC_F.Text)        'FPS

            Dim _aStart As Single = _start / FPS                'audio start
            Dim _aEnd As Single = _end / FPS                    'audio end

            'Note ToString("G9") format is the recommended one for "RoundTripping" a single
            'Audio start/end values as G9 string format
            Const FixedType As Single_ToString_Types = Single_ToString_Types.FIXED
            Dim _gaStart As String = Single_ToString(_aStart, FixedType)
            Dim _gaEnd As String = Single_ToString(_aEnd, FixedType)

            'User-error:
            'If chkRipDumF is checked (therefore, multiplicity=0 and Dum radio button),
            'and start/end frames do not match THP vids' min/max, throw error.
            'This will force users to set those values as such, to ensure ripping of dummy frames work
            'I'm lazy :P
            If type = True Then
                Dim min As UShort = 1
                Dim max As UShort = TryParseErr_UShort(txtVF_T.Text)
                If _start <> min Or _end <> max Then
                    Throw New System.Exception("When ripping the dummy frames with a multiplicity of 0, please ensure the start/end timeframe values equal the THP video's min/max frame values! This will allow proper ripping of each unique dummy frame for each multiplicity.")
                End If
            End If

            Dim cmd As String   'EXE cmd
            'File reg 1-3
            Dim file As String
            Dim file2 As String
            Dim file3 As String
            'Arguments
            Dim args As String
            'Shell process
            'Dim shell As Process

            Dim hasAudio As Boolean = THPHasAudio()
            If hasAudio = False Then
                'If no audio, then just 0/100% states for current/total progress
                TtlPrg(0) = 0
                TtlPrg(1) = 1
                CurPrg(0) = 0
                CurPrg(1) = 1
                UpdateProg_Ttl(TtlPrg, "Apply physical and time crop settings, playback THP file.")
                UpdateProg_Cur(CurPrg, "THP video does NOT use audio! Apply crop settings...", True, True)

                'If THP does not have audio

                'Just Playback file using crop settings and time frame settings
                cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFPlay & strQUOT  'FFPlay command: "C:\FDIR\ffplay.exe"
                file = " " & strQUOT & txtRoot.Text & cmbTHP.Text & strQUOT   'input file: "C:\THPDIR\file.THP"

                'Arguments: input_file + -vf "crop=w:h:x:y,select=between(n\,start_frame\,end_frame),setpts=PTS-STARTPTS" & strQUOT
                args = file & " -vf " & strQUOT & "crop=" & w & ":" & h & ":" & x & ":" & y & ",select=between(n" & strBAK & "," & _start & strBAK & "," & _end & "),setpts=PTS-STARTPTS" & strQUOT

                'Run the cmd+args
                'startInfo.FileName = cmd & args
                'startInfo.UseShellExecute = False
                'shell = New Process
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd & args, Envvar, Envval)

                'Max out prog bars
                TtlPrg(0) += 1
                CurPrg(0) += 1
                UpdateProg_Ttl(TtlPrg, "Done!")
                UpdateProg_Cur(CurPrg, "THP Playback finished!", False, False)
            Else
                'If THP has audio, we must rip/convert audio and video streams as MP4
                'with crop/time frame settings applied, and then re-merge for playback

                'Has to be done like this, due to no FOSS THP encoder in FFMPEG/PLAY available
                '(Thanks, N1nt3nd0 :( )

                'Precise synchronized AV cutting:
                'https://superuser.com/q/866144

                'Playback steps:

                'Step 1: Rip video only as mp4 (FFMPEG)
                'Step 2: Rip audio only as mp4 (FFMPEG)
                'Step 3: Merge both mp4 streams (FFMPEG)
                'Step 4: playback final temp video (FFPLAY)

                'Step 1: Rip video only as mp4 (FFMPEG)
                'ffmpeg -i video.thp -y -an -vcodec h264 -vf "crop=w:h:x:y, select=between(n\,start_Frame\,end_frame),setpts=PTS-STARTPTS" video.mp4
                'Total Prog = 25% delta, current prog = 0/100% states
                TtlPrg(0) = 0
                TtlPrg(1) = 4
                CurPrg(0) = 0
                CurPrg(1) = 1
                UpdateProg_Ttl(TtlPrg, "Step 1: Rip THP video only as MP4")
                UpdateProg_Cur(CurPrg, "THP video DOES use audio! Ripping video only as MP4...", True, False)
                cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT                    'FFMPEG command: "C:\FDIR\ffmpeg.exe"
                file = strQUOT & txtRoot.Text & cmbTHP.Text & strQUOT                           'input file: "C:\THPDIR\file.THP"
                file2 = strQUOT & txtFFPlayTemp.Text & strPATHSEP & "video.mp4" & strQUOT           'Output file: "C:\THPPlayWorkDir\video.mp4"

                'Args: -i input_file -y -an -vcodec h264 -vf "crop=w:h:x:y, select=between(n\,start_Frame\,end_frame),setpts=PTS-STARTPTS" output_File
                args = " -i " & file & " -y -an -vcodec h264 -vf " & strQUOT & "crop=" & w & ":" & h & ":" & x & ":" & y & ",select=between(n" & strBAK & "," & _start & strBAK & "," & _end & "),setpts=PTS-STARTPTS" & strQUOT & " " & file2

                'Run the cmd+args
                'startInfo.FileName = cmd & args
                'startInfo.UseShellExecute = False
                'shell = New Process
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd & args, Envvar, Envval)

                'Step 2: Rip audio only as mp4 (FFMPEG)
                'ffmpeg -i video.thp -vn -ss audio_Start -to audio_End audio.mp4                
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg, "MP4 video ripped!", False, True)

                CurPrg(0) = 0
                TtlPrg(0) += 1
                UpdateProg_Ttl(TtlPrg, "Step 2: Rip THP audio only as MP4")
                UpdateProg_Cur(CurPrg, "Ripping audio only as MP4...", True, False)
                file2 = strQUOT & txtFFPlayTemp.Text & strPATHSEP & "audio.mp4" & strQUOT   'output file: "C:\THPPlayWorkDir\audio.mp4"
                'Args: -i input_file -vn -ss audio_Start -to audio_End output_file
                args = " -i " & file & " -y -vn -ss " & _gaStart & " -to " & _gaEnd & " " & file2

                'Run the cmd+args
                'startInfo.FileName = cmd & args
                'startInfo.UseShellExecute = False
                'shell = New Process
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd & args, Envvar, Envval)

                'Step 3: Merge both mp4 streams (FFMPEG)
                'ffmpeg -i video.mp4 -i audio.mp4 -c:v copy -c:a copy temp.mp4
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg, "MP4 audio ripped!", False, True)

                CurPrg(0) = 0
                TtlPrg(0) += 1
                UpdateProg_Ttl(TtlPrg, "Step 3: Merge both MP4 A/V streams")
                UpdateProg_Cur(CurPrg, "Merging audio and video MP4 streams...", True, False)
                file = strQUOT & txtFFPlayTemp.Text & strPATHSEP & "video.mp4" & strQUOT        'Input video file: "C:\FFPlayWorkDir\video.mp4"
                file2 = strQUOT & txtFFPlayTemp.Text & strPATHSEP & "audio.mp4" & strQUOT       'Input audio file: "C:\FFPlayWorkDir\audio.mp4"
                file3 = strQUOT & txtFFPlayTemp.Text & strPATHSEP & "temp.mp4" & strQUOT        'Output final video file: "C:\FFPlayWorkDir\temp.mp4"
                args = " -i " & file & " -i " & file2 & " -y -c:v copy -c:a copy " & file3  'Args: -i video_input -i audio_input -c:v copy -c:a copy output_file

                'startInfo.FileName = cmd & args
                'startInfo.UseShellExecute = False
                'shell = New Process
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd & args, Envvar, Envval)

                'Step 4: playback final video
                'ffplay.exe "temp.mp4"
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg, "MP4 A/V streams merged!", False, True)

                CurPrg(0) = 0
                TtlPrg(0) += 1
                UpdateProg_Ttl(TtlPrg, "Step 4: Playback final video")
                UpdateProg_Cur(CurPrg, "Playing back final THP video...", True, False)
                cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFPlay & strQUOT                'FFPlay command: "C:\FDIR\ffplay.exe"
                file = " " & strQUOT & txtFFPlayTemp.Text & strPATHSEP & "temp.mp4" & strQUOT   'Playback file: "C:\FFPlayWorkDir\temp.mp4"

                'startInfo.FileName = cmd & file
                'startInfo.UseShellExecute = False
                'shell = New Process
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd & file, Envvar, Envval)

                TtlPrg(0) += 1
                UpdateProg_Ttl(TtlPrg, "Done!")
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg, "Edited THP video succesfully played back!", True, True)
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error during playback!", True)
        End Try

        'Cleanup temp playback files
        CleanUp_Playback()
    End Sub

    ''' <summary>
    ''' Cleanus up leftover FFPlay temp conversion playback files
    ''' </summary>
    ''' <param name="track">Track CurPrg (THPEnc)?</param>
    ''' <param name="track_stringS">Start string to display (tracking)</param>
    ''' <param name="track_stringE">End string to display (tracking)</param>
    ''' <remarks></remarks>
    Private Sub CleanUp_Playback(Optional ByVal track As Boolean = False, Optional ByVal track_stringS As String = "", Optional ByVal track_stringE As String = "")
        'Current progress
        Dim CurPrg(2) As Single
        Try
            'If tracking, set current progress to 0, max progress to 3, display start string at 0% (set/don't wait)
            If track Then
                CurPrg(0) = 0
                CurPrg(1) = 3
                UpdateProg_Cur(CurPrg, track_stringS, True, False)
            End If

            'Delete "C:\FFPlayWorkDir\video.mp4", "\audio.mp4", and "temp.mp4" if exist
            Dim FFPlayRoot As String = txtFFPlayTemp.Text & strPATHSEP              'FFPlay directory
            Dim File As String = strQUOT & FFPlayRoot & "video.mp4" & strQUOT   'Abs path to file to try deleting
            If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
            'If tracking, update progress
            If track Then
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg)
            End If

            File = strQUOT & FFPlayRoot & "audio.mp4" & strQUOT
            If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
            'If tracking, update progress
            If track Then
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg)
            End If

            File = strQUOT & FFPlayRoot & "temp.mp4" & strQUOT
            If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
            'If tracking, update progress
            If track Then
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg)

                'Set to 100%, display end string (append/wait)
                CurPrg(0) = CurPrg(1)
                UpdateProg_Cur(CurPrg, track_stringE, False, True)
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "CleanUp_Playback error!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles ripping a THP to MP4(+WAV) and dummy frames for padding
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRip.Click
        Rip()
    End Sub

    ''' <summary>
    ''' Actually rips THP data
    ''' </summary>
    ''' <param name="pathOverride">Optional path to placed ripped assets.</param>
    ''' <remarks>Param used for CLI_MODE</remarks>
    Private Sub Rip(Optional ByVal pathOverride As String = "")
        'Open ofdRip, user selects path/base filename
        Try
            'Total/current progress for progress bar.
            'Index(0)=current progress, Index(1)=max progress
            Dim TtlPrg(2) As Single
            Dim CurPrg(2) As Single
            'Text reg for prog msg
            Dim Text As String = ""

            Dim envvar(1) As String                             'Envvar array
            Dim envval(1) As String                             'Corresponding value array

            Dim inFile As String = txtRoot.Text & cmbTHP.Text   'Input file. C:\PathToTHP\DIRtoTHP\file.thp"
            Dim initDir As String = FileDir(inFile)             'Initial directory. Directory of inFile
            Dim newFile As String = FileAndExt(cmbTHP.Text)     'New file. "Filename.thp" from inFile
            Dim file As String = ""                             'Generic file register 1
            Dim file2 As String = ""                            'Generic file register 2
            Dim type As Boolean = chkRipDumF.Checked            'Type of ripping to do. True=Rip dummy frames
            newFile = newFile.Replace(".thp", "")               'Remove extension from newFile, just get filename-ext

            Dim suffix As String = GetCellFrameName()           'Suffix for cell name (if any)
            ofdRip.FileName = newFile & suffix                  'Set ofd box filename to newFile & suffix
            ofdRip.InitialDirectory = initDir                   'Set ofd init dir to initDir

            'Show the DBox if not CLI_Mode; else use path override
            Dim outFile As String = ""
            If CLI_MODE = False Then
                If ofdRip.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
                outFile = ofdRip.FileName                 'Output file. C:\PathToFile\file.mp4
            Else
                outFile = pathOverride
            End If
            Dim tempFile As String = FileDir(outFile) & "temp.mp4"  'Temp file
            Dim outPath As String = FileDir(outFile)                'Output path. Path of outFile
            Dim outFilename As String = FileAndExt(outFile)         'Output filename. Filename.mp4

            'Video Conv: ffmpeg -i input_video.mp4 output.mp4
            'https://video.stackexchange.com/questions/4563/how-can-i-crop-a-video-with-ffmpeg
            'Video Crop: ffmpeg -i in.mp4 -filter:v "crop=out_w:out_h:x:y" out.mp4
            'https://www.bugcodemaster.com/article/extract-audio-video-using-ffmpeg
            'Audio Extraction: ffmpeg -i input_video.mp4 -vn output_audio.mp3

            'https://superuser.com/questions/459313/how-to-cut-at-exact-frames-using-ffmpeg
            'Cut video from start to end frame: -vf select="between(n\,200\,300),setpts=PTS-STARTPTS"

            Dim cmd As String = ""                                      'Command to run
            Dim x As String = txtTD_CX.Text                             'Crop xpos
            Dim y As String = txtTD_CY.Text                             'Crop ypos
            Dim w As String = txtTD_CW.Text                             'Crop width
            Dim h As String = txtTD_CH.Text                             'Crop height

            Dim _start As UShort = TryParseErr_UShort(txtTD_FS.Text)    'frame start
            Dim _end As UShort = TryParseErr_UShort(txtTD_FE.Text)      'frame end
            Dim FPS As Single = TryParseErr_Single(txtVC_F.Text)        'FPS

            'Audio info. In seconds (so start/end pt /FPS = seconds)
            Dim _aStart As Single = _start / FPS                        'audio start
            Dim _aEnd As Single = _end / FPS                            'audio end

            'Note ToString("G9") format is the recommended one for "RoundTripping" a single
            'Audio start/end values as G9 string format
            Const FixedType As Single_ToString_Types = Single_ToString_Types.FIXED
            Dim _gaStart As String = Single_ToString(_aStart, FixedType)
            Dim _gaEnd As String = Single_ToString(_aEnd, FixedType)

            'User-error:
            'If chkRipDumF is checked (therefore, multiplicity=0 and Dum radio button),
            'and start/end frames do not match THP vids' min/max, throw error.
            'This will force users to set those values as such, to ensure ripping of dummy frames work
            'I'm lazy :P
            If type = True Then
                Dim min As UShort = 1
                Dim max As UShort = TryParseErr_UShort(txtVF_T.Text)
                If _start <> min Or _end <> max Then
                    Throw New System.Exception("When ripping the dummy frames with a multiplicity of 0, please ensure the start/end timeframe values equal the THP video's min/max frame values! This will allow proper ripping of each unique dummy frame for each multiplicity.")
                End If
            End If


            'Rip steps:
            'Step 1: Convert THP to temp MP4. Encode THP to H264 MP4 with crop filter
            'Step 2: Convert temp mp4 to final MP4, cutting between start and end frames
            'Step 3: Extract audio as wav (if any) with time trimming
            'Step 4: If ripping dummy ctrl frames, convert the cropped MP4 file (cropped to the ctrl area) to bmp frames, keep only 1st frame for each multiplicity
            'Step 5: Cleanup temporary files


            'Step 1: Convert THP to temp MP4. Encode THP to H264 MP4 with crop filter
            'Total prog = 5 steps, current prog = variable
            TtlPrg(0) = 0
            TtlPrg(1) = 5
            CurPrg(0) = 0
            CurPrg(1) = 1
            Text = "Ripping THP video elements." & strNL & "Step 1: Convert THP to temp MP4. Encode THP to H264 MP4 with crop filter"
            UpdateProg_Ttl(TtlPrg, Text)
            UpdateProg_Cur(CurPrg, "Converting...", True, False)
            '"C:\FFMPegPath\ffmpeg.exe"
            cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT
            ' -i C:\PathToTHP\DIRtoTHP\file.thp -vcodec h264 -y -filter:v "crop=out_w:out_h:x:y" "C:\OutputDir\output.mp4"
            cmd &= " -y -i " & strQUOT & inFile & strQUOT & " -vcodec h264 -filter:v " & strQUOT & "crop=" & w & ":" & h & ":" & x & ":" & y & strQUOT & " " & strQUOT & tempFile & strQUOT

            'Run the cmd
            'Dim startInfo As ProcessStartInfo
            'startInfo = New ProcessStartInfo
            'startInfo.FileName = cmd
            'startInfo.UseShellExecute = False
            'Dim shell As Process
            'shell = New Process
            'shell.StartInfo = startInfo
            'shell.Start()
            'shell.WaitForExit()
            RunProcess(cmd)
            CurPrg(0) += 1
            UpdateProg_Cur(CurPrg, "Temp, physically cropped MP4 video created!", False, True)

            'Step 2: Convert temp mp4 to final MP4, cutting between start and end frames
            '"C:\FFMPegPath\ffmpeg.exe"
            CurPrg(0) = 0
            TtlPrg(0) += 1
            UpdateProg_Ttl(TtlPrg, "Step 2: Convert temp mp4 to final MP4, cutting between start and end frames")
            UpdateProg_Cur(CurPrg, "Converting...", True, False)
            cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT
            ' -y -i C:\PathToTHP\DIRtoTHP\file.thp -vcodec h264 -an -vf select="between(n\,start_frame\,end_frame),setpts=PTS-STARTPTS" "C:\OutputDir\output.mp4"
            cmd &= " -y -i " & strQUOT & tempFile & strQUOT & " -vcodec h264 -an -vf select=" & strQUOT & "between(n" & strBAK & "," & _start & strBAK & "," & _end & "),setpts=PTS-STARTPTS" & strQUOT
            cmd &= " " & strQUOT & outFile & strQUOT

            'Run the cmd
            'startInfo = New ProcessStartInfo
            'startInfo.FileName = cmd
            'startInfo.UseShellExecute = False
            'shell = New Process
            'shell.StartInfo = startInfo
            'shell.Start()
            'shell.WaitForExit()
            RunProcess(cmd)
            CurPrg(0) += 1
            UpdateProg_Cur(CurPrg, "Final, time cropped MP4 video created!", False, True)

            'Step 3: Extract audio as wav (if any) with time trimming
            CurPrg(0) = 0
            TtlPrg(0) += 1
            UpdateProg_Ttl(TtlPrg, "Step 3: Extract audio as wav (if any) with time trimming")
            Dim hasAudio As Boolean = THPHasAudio()
            If hasAudio Then
                Text = "Video DOES have an audio stream!" & strNL & "Extracting time-trimmed audio stream..."
                UpdateProg_Cur(CurPrg, Text, True, False)
                'If THP has audio

                'If DirectSound checked, do SDL driver workaround
                If chkRip_DSound.Checked = True Then
                    'set SDL_AUDIODRIVER=directsound
                    'startInfo.EnvironmentVariables("SDL_AUDIODRIVER") = "directsound"
                    envvar(1) = "SDL_AUDIODRIVER"
                    envval(1) = "directsound"
                Else
                    envvar = Nothing
                    envval = Nothing
                End If

                'ffmpeg.exe -y -i video.thp -vn -ss audio_Start -to audio_End "C:\OutputDir\file.wav" 
                Dim wav As String = ""  'Output file for wav

                '"C:\FFMPegPath\FFMPEG.exe"
                cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT
                '-y -i "C:\PathToTHP\DIRtoTHP\file.thp" -vn -ss audio_Start -to audio_End output_file
                cmd &= " -y -i " & strQUOT & inFile & strQUOT & " -vn -ss " & _gaStart & " -to " & _gaEnd & " "

                'If not override flag, then replace inFile.thp with .wav; else replace outFIle.mp4 with .wav
                If pathOverride = "" Then
                    '"C:\OutputDir\file.wav"
                    wav = strQUOT & outPath & FileAndExt(inFile).Replace(".thp", ".wav") & strQUOT
                Else
                    'file.wav
                    wav = strQUOT & outPath & FileAndExt(outFile).Replace(".mp4", ".wav") & strQUOT
                End If
                cmd &= wav

                'Run the cmd
                'startInfo.FileName = cmd
                'Shell.StartInfo = startInfo
                'Shell.Start()
                'Shell.WaitForExit()
                RunProcess(cmd, envvar, envval)
                CurPrg(0) += 1
            Else
                UpdateProg_Cur(CurPrg, "Video does NOT have an audio stream!", True, False)
                CurPrg(0) += 1
            End If
                UpdateProg_Cur(CurPrg, "Audio stream extraction done!", False, True)

                'Step 4: If ripping dummy ctrl frames, convert the cropped MP4 file (cropped to the ctrl area) to bmp frames, keep only 1st frame for each multiplicity
                CurPrg(0) = 0
                TtlPrg(0) += 1
                UpdateProg_Ttl(TtlPrg, "Step 4: If ripping dummy ctrl frames, convert the cropped MP4 file (cropped to the ctrl area) to bmp frames, keep only 1st frame for each multiplicity.")
                If type = True Then
                    Dim m As Byte = TryParseErr_Byte(txtVM_M.Text)             '0-based multiplicity value
                    m -= 1

                    'If ripping dummy ctrl frames.
                    'Convert the cropped MP4 file (cropped to the ctrl area) to bmp frames ("dummyTemp_%0Nd.bmp"),
                    'Keep only 1st frame for each multiplicty, rename to "dummy_N.bmp", delete excess frames

                    'Set max current progress to 2 + # of mults
                    CurPrg(1) = 2 + m
                    Text = "Video HAS dummy frames!" & strNL & "Ripping all bmp frames..."
                    UpdateProg_Cur(CurPrg, Text, True, False)

                    '"C:\FFMPegPath\FFMPEG.exe" -y 
                    cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y "

                    'Output ctrl MP4 to .bmp frames
                    Dim d As String = ""                                                    'Printf digit formatter thingy (pad to N digits)
                    Dim dgs As UShort = 0                                                   'Amount of digits for printf formatter thingy            
                    dgs = TryParseErr_UShort(txtVF_T.Text.Length)                           'Set digits to the amount of digits for the total amount of frames in the video
                    d = "%0" & dgs.ToString() & "d"                                         'Set the printf digit formatter to "dgs" digits
                    cmd &= "-i " & strQUOT & outFile & strQUOT                              '-i "C:\OutputDir\file.mp4"

                    '"C:\OutputDir\dummyTemp_%0Nd.bmp"
                    file = strQUOT & FileDir(outFile) & "dummyTemp_" & d & ".bmp" & strQUOT
                    cmd &= " " & file

                    'Run cmd
                    'startInfo.FileName = cmd
                    'Shell.StartInfo = startInfo
                    'Shell.Start()
                    'Shell.WaitForExit()
                    RunProcess(cmd)
                    UpdateProg_Cur(CurPrg, "All BMP frames ripped!", False, True)
                    UpdateProg_Cur(CurPrg, "Finding and keeping appropriate BMP frames...")

                    'Rename the appropriate frames to "dummy_N.bmp", remove the others 
                    Dim i As Byte = 0                           'Generic iterator
                    Dim j As UShort = 0                         'Frame value
                    Dim frames As UShort = TryParseErr_UShort(txtVF_S.Text)    'The amount of frames per subvideo                

                    'Iterate through the mults (0-based)
                    For i = 0 To m Step 1
                        CurPrg(0) += 1                                      'Increment current prog foreach mult
                        Text = "Mult " & (i + 1).ToString()                 'Log "Mult M"
                        UpdateProg_Cur(CurPrg, Text)

                        j = i * frames                                      'Frame ID = multiplicity ID * amount of frames. This gets 1st frame for each multplicity.
                        j += 1                                              'Make FrameID 1-based
                        d = "_" & j.ToString(StrDup(dgs, "0")) & ".bmp"     'Set d as the frame ID string "_%0Nd.bmp"
                        file = "dummy_" & (i + 1).ToString() & ".bmp"       'File = "dummy_N.bmp"

                        'Move file "C:\OutputDir\dummyTemp_ID.bmp" to "C:\OutputDir\dummy_N.bmp"
                        file = FileDir(outFile) & FileAndExt(file)          'File = "C:\OutputDir\dummy_N.bmp"
                        file2 = FileDir(outFile) & "dummyTemp" & d          'File2 = "C:\OutputDir\dummyTemp_ID.bmp"
                        My.Computer.FileSystem.MoveFile(file2, file, True)
                    Next i

                    CurPrg(0) = CurPrg(1)
                    UpdateProg_Cur(CurPrg, "All appropriate BMP frames found and kept!", False, True)

                    'Step 5: Cleanup temporary files (Delete all extra "dummyTemp_%0Nd.bmp" files)
                    TtlPrg(0) += 1
                    CurPrg(0) = 0
                    CurPrg(1) = 1
                    UpdateProg_Ttl(TtlPrg, "Step 5: Cleanup temporary files")
                    UpdateProg_Cur(CurPrg, "", True, False)
                    file = FileDir(outFile)                                                         'file = C:\WorkingDir
                    file2 = "dummyTemp*.bmp"                                                        'file2 = dummyTemp*.bmp
                    DeleteFilesFromFolder(file, file2, True, "Cleaning up files...", True, False)   'Delete files (with logging)
                Else
                    CurPrg(1) = 1
                    Text = "Video does NOT have dummy frames!" & strNL
                    UpdateProg_Cur(CurPrg, Text, True, False)
                    CurPrg(0) += 1
                    UpdateProg_Cur(CurPrg, "Dummy frame extraction done!", False, True)

                    TtlPrg(0) += 1
                    UpdateProg_Ttl(TtlPrg, "Step 5: Cleanup temporary files")
                End If

                'Delete temp.mp4
                DeleteFilesFromFolder(FileDir(outFile), "temp.mp4")
                TtlPrg(0) = TtlPrg(1)
                CurPrg(0) = CurPrg(1)
                UpdateProg_Ttl(TtlPrg, "Done!")
                UpdateProg_Cur(CurPrg, "Cleanup done!", True, True)

                'Thwimp kicks dat Koopa shell away!
                'Shell.Close()
                If chkAudio.Checked Then My.Computer.Audio.Play(My.Resources.success, AudioPlayMode.Background)
                Log_MsgBox(Nothing, "Video ripped!", MsgBoxStyle.Information, "Success!", True)
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error during ripping!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Runs a process, with full logging of output (esp FFMPEG)
    ''' </summary>
    ''' <param name="cmd">Cmd to run</param>
    ''' <param name="envvar">Array of envvars (optional; if none, nothing)</param>
    ''' <param name="envval">Array of envvals (optional; if none, nothing)</param>
    ''' <param name="Special">Special callback? (Optional, false)</param>
    ''' <param name="shell2">Shell byref (optional; nothing)</param>
    ''' <remarks>FFMPEG Capture: https://www.codeproject.com/Questions/492381/StartInfo-RedirectStandardOutp</remarks>
    Private Sub RunProcess(ByVal cmd As String, Optional ByRef envvar() As String = Nothing, Optional ByRef envval() As String = Nothing, Optional ByVal Special As Boolean = False, Optional ByRef shell As Process = Nothing, Optional ByVal workDir As String = "")
        Const _CMD As String = "cmd.exe"                                        'Command prompt exe name
        Dim logg = ""                                                           'Redirected log text
        Dim xFileData As StreamReader = Nothing                                 'StreamReader for temp log file
        Dim _file As String = txtDataDir.Text & strPATHSEP & TEMPFLOG           'Templog file (C:\WordDir\full_log.txt)
        Try
            'If eitehr envvar or envval array refs are null, then created new 1D array with string.empty
            If IsNothing(envvar) = True Then
                ReDim envvar(1)
                envvar(1) = ""
            End If
            If IsNothing(envval) = True Then
                ReDim envval(1)
                envval(1) = ""
            End If

            Dim full As Boolean = chkLogFull.Checked                                        'Full error logging?
            Dim arg As String = "/c " & strQUOT & cmd & strQUOT                             'Cmd.exe argument. Std is cmd.exe /c "[cmds}"

            'StartInfo
            Dim startInfo As ProcessStartInfo
            startInfo = New ProcessStartInfo(_CMD)                                          'Create new Cmd.exe process
            If workDir <> "" Then startInfo.WorkingDirectory = workDir

            'If full logging AND not special flag, then cmd.exe /c "[cmds] 1>temp_file 2>&1" for proper redirection
            'https://stackoverflow.com/a/20789248
            If full Then
                If Special = False Then
                    arg = "/c " & strQUOT & cmd & " 1>" & strQUOT & _file & strQUOT & " 2>&1" & strQUOT
                    startInfo.CreateNoWindow = True                                         'Disable annoying window
                    Log(strNL & strNL & _CMD & " " & arg, MsgBoxStyle.Information, False)   'Log the command
                End If
            End If
            startInfo.Arguments = arg                                                       'Set the args

            'Setup envvar/val pairs if exist
            'Make sure matching dictionary for var/values; else throw error
            If envvar.Count <> envval.Count Then Throw New System.Exception("Dictionary size mismatch for Envvar/val pairs!")
            Dim i As Byte = 1                   'Generic iterator
            Dim max As Byte = envval.Count()    'Max amount of pairs
            max -= 1                            '0-based
            'Iterate between all pairs, set variable to value
            For i = 0 To max
                If envvar(i) <> "" And envval(i) <> "" Then
                    startInfo.EnvironmentVariables(envvar(i)) = envval(i)
                End If
            Next i
            'startInfo.FileName = cmd                                                       'Setup cmd
            startInfo.UseShellExecute = False                                               'DON'T use shell

            'Setup the shell process, run it
            If IsNothing(shell) Then shell = New Process()
            shell.StartInfo = startInfo
            shell.Start()

            'if NOT special flag, then waitforexit, and then log temp file to the log
            If Special = False Then
                shell.WaitForExit()
                If full Then
                    xFileData = File.OpenText(_file)                                    'Open temp file as streamreader
                    logg = xFileData.ReadToEnd()                                        'Read it all into logg
                    logg &= (strNL & strNL)                                             'Append CRFL CRLF
                    KillStream(xFileData, True, _file)                                  'Kill streamreader
                    Log(logg, Microsoft.VisualBasic.MsgBoxStyle.Information, False)     'Log it!
                End If
                shell.Close()
                shell.Dispose()
                shell = Nothing
            End If
            'If special flag, then you'll manually need to handle stdout redir and shell (returned ByRef) OUTSIDE of func
        Catch ex As System.Exception
            'If error, kill streamreader, and shell2
            KillStream(xFileData, True, _file)
            If IsNothing(shell) = False And Special = False Then
                shell.Close()
                shell.Dispose()
                shell = Nothing
            End If

            'Log cmdline stuff
            Dim text As String = ex.Message
            text &= strNL & strNL & "Arguments:" & strNL & strNL
            text &= "cmd: " & cmd & strNL
            text &= "specialCawback: " & Special.ToString() & strNL
            text &= "envars: " & envvar.ToString & strNL
            text &= "envals: " & envval.ToString & strNL
            text &= "workdir: " & workDir.ToString & strNL
            Log_MsgBox(ex, text, MsgBoxStyle.Critical, "RunProcess cmdline error!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Keeps txtTD_CX in range for total vid width
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTD_CX_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTD_CX.Validated
        Try
            Dim xmin As UShort = 0                                      'xmin = 0
            Dim xmax As UShort = TryParseErr_UShort(txtTDims_W.Text)    'xmax = Total vid width
            xmax -= 1                                                   'Make it 0-based. (Can't do a crop at xpos=xmax)
            txtTD_CX.Text = KeepInRange(txtTD_CX.Text, xmin, xmax)      'Set string within numeric range
            KeepWInRange()                                              'Keep W in range
        Catch ex As Exception
            'Log_MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in txtTD_CX_Validated!", True)
        End Try
    End Sub
    ''' <summary>
    ''' Keeps txtTD_CY in range for total vid height
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTD_CY_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTD_CY.Validated
        Try
            Dim ymin As UShort = 0                                      'ymin = 0
            Dim ymax As UShort = TryParseErr_UShort(txtTDims_H.Text)    'ymax = Total vid height
            ymax -= 1                                                   'Make it 0-based. (Can't do a crop at ypos=ymax)
            txtTD_CY.Text = KeepInRange(txtTD_CY.Text, ymin, ymax)      'Set string within numeric range
            KeepHInRange()                                              'Keep H in range
        Catch ex As Exception
            'Log_MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in txtTD_CY_Validated!", True)
        End Try
    End Sub
    ''' <summary>
    ''' Keeps txtTD_CW in range for x offset and total vid width
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTD_CW_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTD_CW.Validated
        KeepWInRange()
    End Sub
    ''' <summary>
    ''' Keeps txtTD_CH in range for y offset and total vid height
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTD_CH_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTD_CH.Validated
        KeepHInRange()
    End Sub

    ''' <summary>
    ''' Keeps W of crop value in range
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KeepWInRange()
        Try
            Dim wmin As UShort = 0                                  'wmin = 0
            Dim w1 As UShort = TryParseErr_UShort(txtTD_CX.Text)    'a = xpos
            Dim w2 As UShort = TryParseErr_UShort(txtTDims_W.Text)  'b = total video width
            Dim wmax As UShort = w2 - w1                            'Get dif of b-a as wmax
            txtTD_CW.Text = KeepInRange(txtTD_CW.Text, wmin, wmax)  'Keep w in range
        Catch ex As Exception
            'Log_MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in KeepWInRange()", True)
        End Try
    End Sub

    ''' <summary>
    ''' Keeps H of crop value in range
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KeepHInRange()
        Try
            Dim hmin As UShort = 0                                  'wmin = 0
            Dim h1 As UShort = TryParseErr_UShort(txtTD_CY.Text)    'a = ypos
            Dim h2 As UShort = TryParseErr_UShort(txtTDims_H.Text)  'b = total video height
            Dim hmax As UShort = h2 - h1                            'Get dif of b-a as hmax
            txtTD_CH.Text = KeepInRange(txtTD_CH.Text, hmin, hmax)  'Keep h in range
        Catch ex As Exception
            'Log_MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in KeepHInRange()", True)
        End Try
    End Sub

    ''' <summary>
    ''' Updates the start/end time frame masks, based on the THP total frame length
    ''' </summary>
    Private Sub HandleRipTimeMasks()
        Try
            Dim length As Byte = txtVF_T.Text.Length    'Get length as byte for total frames in video
            Dim mask As String = StrDup(length, "0")    'The mask, set to length of 0s
            'Update the start/end masks
            txtTD_FS.Mask = mask
            txtTD_FE.Mask = mask

            'Force the mult NUD to 0, to fire an event to update the default start/end frame values for ripping whole video
            'This doesn't seem to always want to fire; forcibly call the function
            nudTD_M.Value = 0
            nudTD_M_ChangeMe()
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in HandleRipTimeMasks()!", True)
        End Try
    End Sub

    Private Sub nudTD_M_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudTD_M.ValueChanged
        nudTD_M_ChangeMe()
    End Sub

    ''' <summary>
    ''' Updates the default start/end frame rip values when the multiplicity NUD is changed, the chkRipM value, and the chkRipDumF value
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub nudTD_M_ChangeMe()
        Try
            Dim m As Byte = TryParseErr_Byte(nudTD_M.Value)         'Current multiplicity index
            Dim mult As UShort = TryParseErr_UShort(txtVF_S.Text)   'Amount of frames in a subvideo
            Dim _start As UShort = 1                                'Start frame
            Dim _end As UShort = 2                                  'End frame
            Dim ma As Byte = 0                                      'Start multiplicity index
            Dim mb As Byte = 1                                      'End multiplicity index

            'Flag to indicate bugfix. THPs with only one frame can use either m=0 (all frames, file named [file].mp4),
            'or m=1 (1st and only frame, file named "file_A1_1.mp4").
            'Flag inidicates to fix an off-by-one error if m=1 for this special case for the end time frame            
            Dim SingleBugfix As Boolean = False
            'If nud has a min of 0 and max of 1, then special bugfix
            If nudTD_M.Minimum = 0 And nudTD_M.Maximum = 1 Then SingleBugfix = True

            'Zero is special case meaning to rip all frames (frame 1 to total)
            Dim singleM As Boolean = True                           'Rip only one multiplicity?
            Dim dumF As Boolean = False                             'Rip dummy frames?
            If m = 0 Then singleM = False 'If m=0, ripping multiple Ms
            If singleM = False Then
                'Set range from 1 to final frame
                _start = 1
                _end = TryParseErr_UShort(txtVF_T.Text)
                If radTD_Dum.Checked = True Then dumF = True 'If dummy rad is checked, then set dumF flag
            Else
                mb = nudTD_M.Value                                  'mult index in box
                ma = mb - 1                                         'index-- (0-based mult index)
                _start = ma * mult                                  'start = (start index * frame mult)
                If _start = 0 Then _start = 1 '1st frame is one-based; set to 1 if 0
                _end = mb * mult                                    'end = (end index * frame mult) - 1

                'Only decrement by one if not the special bugfix
                If SingleBugfix = False Then _end = _end - 1
            End If

            'Update the start/end frame text values, chkRipM state/text, chkRipDumF
            chkRipM_Change(singleM)
            chkRipDumF.Checked = dumF
            txtTD_FS.Text = _start.ToString()
            txtTD_FE.Text = _end.ToString()
        Catch ex As Exception
            'Log_MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in nudTD_M_ValueChanged()!", true)
        End Try
    End Sub

    ''' <summary>
    ''' Changes checked value and text of chkRipM
    ''' </summary>
    ''' <param name="val">New state</param>
    ''' <remarks></remarks>
    Private Sub chkRipM_Change(ByVal val As Boolean)
        chkRipM.Checked = val
        ChkString("Single", "All", chkRipM)
    End Sub

    ''' <summary>
    ''' Keeps the frame start rip value within range (start={start|(1≤start≤total ∩ start&lt;end})
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTD_FS_ValidatedByVal(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTD_FS.Validated
        Try
            '1st statement
            Dim smin As UShort = 1                                      'smin = 1
            Dim smax As UShort = TryParseErr_UShort(txtVF_T.Text)       'smax = Total amt of frames in vid
            Dim _end As UShort = TryParseErr_UShort(txtTD_FE.Text)      'end frame length
            txtTD_FS.Text = KeepInRange(txtTD_FS.Text, smin, smax)      'Set string within numeric range

            '2nd statement
            smin = 1
            smax = _end - 1
            txtTD_FS.Text = KeepInRange(txtTD_FS.Text, smin, smax)      'Set string within numeric range
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in txtTD_FS_Validated!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Keeps the frame end rip value within range (end={end|(1≤end≤total ∩ end&gt;start})
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTD_FE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTD_FE.Validated
        Try
            '1st statement
            Dim emin As UShort = 1                                      'emin = 1
            Dim emax As UShort = TryParseErr_UShort(txtVF_T.Text)       'emax = Total amt of frames in vid
            Dim _start As UShort = TryParseErr_UShort(txtTD_FS.Text)    'start frame length
            txtTD_FE.Text = KeepInRange(txtTD_FE.Text, emin, emax)      'Set string within numeric range

            '2nd statement
            emin = _start + 1
            txtTD_FE.Text = KeepInRange(txtTD_FE.Text, emin, emax)      'Set string within numeric range
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in txtTD_FE_Validated!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Update the pos/size of the crop box params based on the video cell selected for THP Decoding
    ''' </summary>
    Private Sub radTD_A1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_A1.CheckedChanged
        If radTD_A1.Checked = True Then HandleTimeFrameCell(1, 1, 0)
    End Sub
    Private Sub radTD_A2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_A2.CheckedChanged
        If radTD_A2.Checked = True Then HandleTimeFrameCell(2, 1, 0)
    End Sub
    Private Sub radTD_A3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_A3.CheckedChanged
        If radTD_A3.Checked = True Then HandleTimeFrameCell(3, 1, 0)
    End Sub
    Private Sub radTD_A4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_A4.CheckedChanged
        If radTD_A4.Checked = True Then HandleTimeFrameCell(4, 1, 0)
    End Sub
    Private Sub radTD_A5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_A5.CheckedChanged
        If radTD_A5.Checked = True Then HandleTimeFrameCell(5, 1, 0)
    End Sub
    Private Sub radTD_A6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_A6.CheckedChanged
        If radTD_A6.Checked = True Then HandleTimeFrameCell(6, 1, 0)
    End Sub
    Private Sub radTD_B1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_B1.CheckedChanged
        If radTD_B1.Checked = True Then HandleTimeFrameCell(1, 2, 0)
    End Sub
    Private Sub radTD_B2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_B2.CheckedChanged
        If radTD_B2.Checked = True Then HandleTimeFrameCell(2, 2, 0)
    End Sub
    Private Sub radTD_B3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_B3.CheckedChanged
        If radTD_B3.Checked = True Then HandleTimeFrameCell(3, 2, 0)
    End Sub
    Private Sub radTD_B4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_B4.CheckedChanged
        If radTD_B4.Checked = True Then HandleTimeFrameCell(4, 2, 0)
    End Sub
    Private Sub radTD_B5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_B5.CheckedChanged
        If radTD_B5.Checked = True Then HandleTimeFrameCell(5, 2, 0)
    End Sub
    Private Sub radTD_B6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_B6.CheckedChanged
        If radTD_B6.Checked = True Then HandleTimeFrameCell(6, 2, 0)
    End Sub
    Private Sub radTD_All_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_All.CheckedChanged
        If radTD_All.Checked = True Then HandleTimeFrameCell(0, 0, 1)
    End Sub

    Private Sub radTD_Dum_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTD_Dum.CheckedChanged
        If radTD_Dum.Checked = True Then HandleTimeFrameCell(0, 0, -1)
        nudTD_M_ChangeMe()  'Additionally force-run this, to set the chkRipDumF box if necessary
    End Sub

    ''' <summary>
    ''' Function which handles updating the start/end frame counts based on the radio button array for individual THP cells
    ''' </summary>
    ''' <param name="row">1-based ID of row</param>
    ''' <param name="col">1-based ID of col</param>
    ''' <param name="special">Special radio button. -1=dum, 0=use r/c pair, 1=All</param>
    ''' <remarks></remarks>
    Private Sub HandleTimeFrameCell(ByVal row As Byte, ByVal col As Byte, ByVal special As SByte)
        Try
            'Total size of the THP video
            Dim VidTSize As Dims
            VidTSize.width = TryParseErr_UShort(txtTDims_W.Text)    'Tot Width
            VidTSize.height = TryParseErr_UShort(txtTDims_H.Text)   'Tot Height

            'Frame size of the THP subvideo cells
            Dim VidFSize As Dims
            VidFSize.width = TryParseErr_UShort(txtVS_W.Text)    'Frame Width
            VidFSize.height = TryParseErr_UShort(txtVS_H.Text)   'Frame Height

            'Padding size of the THP video cells
            Dim PadSize As Dims
            PadSize.width = TryParseErr_UShort(txtVP_W.Text)     'Pad Width
            PadSize.height = TryParseErr_UShort(txtVP_H.Text)    'Pad Height

            'Crop box params to change
            Dim pos As Dims                                     'X/Y Position for cropping. This is zero-based!
            Dim size As Dims                                    'Size for cropping

            If special = 0 Then
                'Handle row/col pair

                'Set appropriate pos
                pos.width = (col - 1) * VidFSize.width          'Width = 0-based_col * frame_width
                pos.height = (row - 1) * VidFSize.height        'Width = 0-based_row * frame_height
                'Set appropriate size. Always frame size for this option
                size.width = VidFSize.width
                size.height = VidFSize.height
            ElseIf special = -1 Then
                'Handle dummy

                pos.width = 0                                   'Start at x=0
                pos.height = VidTSize.height - PadSize.height   'Start at y=Total video height - padding height
                'Set size to padsize
                size.width = PadSize.width
                size.height = PadSize.height
            Else
                'Handle All. At origin, full vid size

                pos.width = 0
                pos.height = 0
                'Set size to total video size
                size.width = VidTSize.width
                size.height = VidTSize.height
            End If

            'Update the text boxes as appropriate with the new, adjusted params
            txtTD_CX.Text = pos.width.ToString()
            txtTD_CY.Text = pos.height.ToString()
            txtTD_CW.Text = size.width.ToString()
            txtTD_CH.Text = size.height.ToString()
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in HandleTimeFrameCell()!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the suffix name and mult for the current selected frame cell in the decoder/ripper group box.
    ''' Used for setting the default filename when ripping
    ''' </summary>
    ''' <returns>Suffix for checked video cell/special</returns>
    Private Function GetCellFrameName() As String
        Dim nameResult As String = ""                       'String result of cell name (cell+suffix)
        Dim suffix As String = ""                           'Multiplicity suffix
        Try
            'Array of radio buttons (A1_N notation)
            Dim Rads(6, 2) As System.Windows.Forms.RadioButton
            Rads(1, 1) = radTD_A1
            Rads(2, 1) = radTD_A2
            Rads(3, 1) = radTD_A3
            Rads(4, 1) = radTD_A4
            Rads(5, 1) = radTD_A5
            Rads(6, 1) = radTD_A6
            Rads(1, 2) = radTD_B1
            Rads(2, 2) = radTD_B2
            Rads(3, 2) = radTD_B3
            Rads(4, 2) = radTD_B4
            Rads(5, 2) = radTD_B5
            Rads(6, 2) = radTD_B6
            Dim Rad_Dum = radTD_Dum 'Dummy radio button
            Dim Rad_All = radTD_All 'All radio button

            'Corresponding Names for each radio button
            Dim names(6, 2) As String
            names(1, 1) = "_A1"
            names(2, 1) = "_A2"
            names(3, 1) = "_A3"
            names(4, 1) = "_A4"
            names(5, 1) = "_A5"
            names(6, 1) = "_A6"
            names(1, 2) = "_B1"
            names(2, 2) = "_B2"
            names(3, 2) = "_B3"
            names(4, 2) = "_B4"
            names(5, 2) = "_B5"
            names(6, 2) = "_B6"
            Dim name_dum As String = "_Dum" 'Dummy name
            'No suffix for "All"

            Dim c As Byte = 1               'Column iterator
            Dim r As Byte = 1               'Row iterator
            Dim result As Boolean = False   'Result escape flag

            'Iterate through all columns then rows, until find a checked button; then escape
            For c = 1 To 2
                For r = 1 To 6
                    result = Rads(r, c).Checked
                    If result = True Then Exit For
                Next r
                If result = True Then Exit For
            Next c

            'If no results, then check dummy radio button
            If result = False Then
                If Rad_Dum.Checked = True Then nameResult = name_dum 'If dummy checked, set to dummy cell name
                If nudTD_M.Value <> 0 Then suffix = "_" & TryParseErr_Byte(nudTD_M.Value)
                'If check fails, assume "All" button, which is null cell name
            Else
                'If a normal array result was found, fetch appropriate name
                nameResult = names(r, c)
                If nudTD_M.Value <> 0 Then suffix = "_" & TryParseErr_Byte(nudTD_M.Value)
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in GetCellFrameName()!", True)
        End Try
        nameResult = nameResult & suffix
        Return nameResult
    End Function

    ''' <summary>
    ''' Given a numeric text string, keeps it within range between min and max values
    ''' </summary>
    ''' <param name="inp">Numeric string</param>
    ''' <param name="min">Min value</param>
    ''' <param name="max">Max value</param>
    ''' <returns>Numeric string in range</returns>
    ''' <remarks></remarks>
    Private Function KeepInRange(ByVal inp As String, ByVal min As UShort, ByVal max As UShort) As String
        Dim outp As String = inp                            'Output
        Try
            Dim val As UShort = TryParseErr_UShort(inp)     'Get numeric value of input
            Dim newVal As UShort = 0                        'New value to apply

            'Keep newval between min and max, and if outside, use either newval=min or max appropriately
            'If in range, newval=val
            If val < min Then
                newVal = min
            ElseIf val > max Then
                newVal = max
            Else
                newVal = val
            End If
            outp = newVal.ToString()                        'Set output as strinng of newval
        Catch ex As Exception
            'Function can be overzealous with the error checking here. Don't log error messaging, but use current inp param as newVal if TryParseErr_UShort fails, due to maskbox null stuff
            'Log_MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in KeepInRange func!", True) Then
        End Try
        Return outp
    End Function



    '===========================
    'THP Encoder group box

    ''' <summary>
    ''' Handles encoding many input subvideos, a wav file, and dummy padding frames into a composite THP file.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This the main feature of the program, and quite schmancy</remarks>
    Private Sub btnTE_Enc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTE_Enc.Click
        Encode()
    End Sub

    Private Sub Encode(Optional ByVal pathOverride As String = "")
        'Psuedo code of encoding process. Assume an array of subvideos with a multiplicity.
        'During these steps, MP4s with an -avcodec of h264 are used, in order to preserve loseless but compressed quality output during the multiple passes.
        'Ideally, I would use AVIs with -avcodec of rawvideo, but AVIs don't support video dimensions that aren't a mult of 16 without speedloss

        '0.  If video has padding, convert the appropriate dummy bmp file ("dummy_N.bmp") to a video of appropriate frame length ("dummy_N.mp4") for the current multiplicity
        '1.  All subvideos in a column are vstacked ("cN.mp4"). Do for all columns
        '2.  All subvideos in a column are then limited to F frames ("dN.mp4"). Do for all columns
        '3.  HStack all frame-limited column videos ("dN.mp4" in step 2) to create a composite video with all subvideos included for the current multiplicity. ("mN.mp4", where N is the current multiplicity)
        '4.  Repeat steps 0-3 for each multiplicity

        '5.  Concatenate each composite multiplicity video (all "mN.mp4" files in step 3) to a nearly-final mp4 file ("filename.mp4")

        '6.  If video has padding, concatenate all dummy video multiplicities ("dummy_N.mp4" in step 0) to a composite dummy video ("dummy.mp4")
        '7.  If video has padding, vstack the video in step 5 ("filename.mp4") with the composite dummy
        '    video in step 6 ("dummy.mp4") into a file called "final.mp4".
        '    MoveFile "final.mp4"->"filename.mp4"                
        'REMOVED 7.2 Convert filename.mp4 to yuv420p format

        '8.  Convert final video ("filename.mp4") into BMP frames, padded to N digits ("frame_%0Nd.bmp")
        '8.1 Find, copy, and hack Irfanview advanced options INI file
        '8.2 Convert BMP frames into JPG frames, using irfanview, and the JPG Quality value
        '9.  Check the output directory, and delete any extra jpg frames past the framelimit (frames * m).
        '10. The jpg files and the audio file (if applicable, "filename.wav") are converted into "filename.thp" with THPConv at proper framerate
        '11. Cleanup() is run to delete all temporary files from steps 0-10 during the conversion
        '12. Done!

        'Imagine having to do the above steps manually with specially-crafted batch scripts
        'for the specific configuration of each THP file you want encoded,
        ' or worse, without scripts and with a video edtior x_x!

        'Thwimp tackles this proplem headon, and in a very automated fashion!

        'Naming conventions:
        'The working directory for conversion needs the following input files:
        '*  MP4 video files for each subvideo, and for each multiplicity. These MP4s should be encoded with -vcodec h264
        '   Named as "filename_AX_Y.mp4", where "A" is a letter indicating the row ID in the array,
        '   where "X" is the column ID as a number, and "Y" is the multiplicity ID. A and X are setup in MS Excel A1_N notation
        '*  If video has audio, "filename.wav" for the audio stream
        '*  If video has dummy padding, a single BMP image file for each multiplicity
        '   ("dummy_N.bmp", where N is the current mult ID).
        '   This will be converted into a video of fixed framelength and used during the processing

        'In the THP Encoding group box, an array of checkboxes indicates what files will be needed
        'to fulfill the array, and also if a file will be needed for padding or audio

        'The THP Encoding group box has 2 user inputs:
        '*"Trunc Frame" - Amount of frames to truncate subvideos to.
        '   This is used to ensure all subvideos have the same framelength
        '   (e.g., if bad video mastering made, say 255 frames vs. target 250 for all videos)
        '*"Digs"- The amount of digits in the "Trunc Frame" value.
        '   Used for some filenaming formating.
        ' They should match appropriately!

        'BEGIN CODE!

        'FFMPEG NOTES:
        'Vertical and horizontal stacking (2 files)
        'https://unix.stackexchange.com/questions/233832/merge-two-video-clips-into-one-placing-them-next-to-each-other
        'ffmpeg -i top.mp4 -i bot.mp4 -filter_complex vstack output.mp4
        'ffmpeg -i left.mp4 -i right.mp4 -filter_complex hstack output.mp4

        'V/HStacking for N videos
        'https://stackoverflow.com/questions/11552565/vertically-or-horizontally-stack-several-videos-using-ffmpeg/33764934#33764934
        'ffmpeg -i input0 -i input1 -i input2 -filter_complex "[0:v][1:v][2:v]vstack=inputs=3[v]" -map "[v]" output

        'Concatenate N videos. This usually requires using -i ListOfFiles.txt to work properly :(.
        'https://stackoverflow.com/questions/7333232/how-to-concatenate-two-mp4-files-using-ffmpeg
        'ffmpeg -i opening.mkv -i episode.mkv -i ending.mkv -filter_complex concat output.mkv"

        'Show ofdOutput, let user select the working directory with the input files
        Dim shell As Process = New Process()
        Try
            'If not CLI_MODE, then show dialog/handle cancel
            If CLI_MODE = False Then
                If ofdOutput.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            End If

            'Total/current progress for progress bar.
            'Index(0)=current progress, Index(1)=max progress
            'Min progress always = 0
            Dim TtlPrg(2) As Single
            Dim CurPrg(2) As Single
            'Generic text register
            Dim text As String

            'Dim startInfo As ProcessStartInfo
            'startInfo = New ProcessStartInfo
            'startInfo.UseShellExecute = False
            'Dim shell As Process
            'shell = New Process

            Dim path As String

            'If NOT CLI_MODE, then get path from dialog box; else use path override
            If CLI_MODE = False Then
                path = ofdOutput.SelectedPath         'The working directory with our input files.
            Else
                path = pathOverride
            End If

            Dim filename As String = FileAndExt(cmbTHP.Text)    '"filename.thp" we want to create
            Dim file As String = ""                             'Generic file string
            Dim file2 As String = ""                            'Generic file string2
            filename = filename.Replace(".thp", "")             'Remove extension from filename var
            Dim cmd As String = ""                              'String with commands to run in a Process/Shell

            'Generic iterators
            Dim i As UShort = 1   'Usually rows
            Dim j As UShort = 1   '~       cols
            Dim k As UShort = 1   '~       multiplicity
            Dim cnt As UShort = 0

            'THP Array dims        
            Dim r As Byte = TryParseErr_Byte(txtArr_R.Text)         'Amount of rows
            Dim c As Byte = TryParseErr_Byte(txtArr_C.Text)         'Amount of cols
            Dim m As Byte = TryParseErr_Byte(txtVM_M.Text)          'Amount of mult
            Dim suffix As String                                    'The suffix to use to meet array naming conventions        

            Dim parms(6) As String                                  'Array of generic string parameters for cmd string building. Usually used for v/hstacking N videos.
            Dim parm As String                                      'Usually the concatenation of the elements in the parms array
            Dim frames As UShort = TryParseErr_UShort(txtTE_F.Text) 'The amount of frames to limit each subvideo to
            Dim FPS As Single = TryParseErr_Single(txtVC_F.Text)    'The framerate FPS as single
            Dim FPS_string As String = Single_ToString(FPS, Single_ToString_Types._SINGLE)

            'Array of suffixes for the naming conventions in MS Excel A1_N notation (Row, Column)
            'It is hardcoded to 6x2, since the components of each length dimension don't go any larger than this
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

            Dim Files(6) As String          'Array of input files for ffmpeg. Written to File.txt, used as input listing file
            Dim hasPad As Boolean = THPHasPad()

            CleanUp(path, filename, r, c, m, hasPad, False)         'Cleanup leftover temp files at working dir

            'Play elevator music (if allowed)            
            If (chkAudio.Checked And chkEMusic.Checked) Then My.Computer.Audio.Play(SONG, AudioPlayMode.BackgroundLoop)

            'BEGIN PROCESSING

            'Init progress
            HideApp_notTHPEnc(False)        'Hide everything not related to THP Encoding (saves rendering CPU cycles)
            btnLogClear.PerformClick()      'Click btnLogClear (clear all progress/log logs)

            'Total progress: current = 0, max = 9 steps (0-based = 10 steps)
            TtlPrg(0) = 0
            TtlPrg(1) = 9
            UpdateProg_Ttl(TtlPrg, "Create separate composite videos for each multiplicity (Steps 1-4, " & m.ToString() & " " & Plural(m, "multiplicity", "multiplicities") & ")")

            'Steps 1-4
            'Current progress: current = 0, max = 2m(c+1); see factored algebra
            CurPrg(0) = 0
            'm[1+c+c+1]
            'm[2c+2]
            '2m[c+1]
            CurPrg(1) = (2 * m) * (c + 1)

            'Iterate through all multiplicities from 1 to m
            For k = 1 To m Step 1
                UpdateProg_Cur(CurPrg, "Mult = " + k.ToString(), True, False)
                If hasPad Then
                    UpdateProg_Cur(CurPrg, "Step 0: Video has padding!" & strNL & "Convert dummy bmp file for this mult (dummy_" & k.ToString() & ".bmp) to video of appropriate frame length (dummy_" & k.ToString() & ".mp4)")

                    'Do Step 0 if padding
                    Dim dg As Byte = frames.ToString().Length   'The amount of frames to limit to in digits
                    Dim dgs As String = StrDup(dg, "0")         'A .ToString() format string, limiting to N digits
                    cnt = 0                       'Generic iterator

                    'Convert dummy still images for the current multiplicity to a video.
                    'Do this by copying the image to many sequentially named files,
                    'then render all frames as .mp4 video

                    'Iterate through all frames from 1 to Frames
                    For cnt = 1 To frames
                        file = path & strPATHSEP & "dummy_" & k.ToString() & ".bmp"                             'file =     "C:\WorkingDir\dummy_N.bmp"
                        file2 = path & strPATHSEP & "dummy_" & k.ToString() & "_" & cnt.ToString(dgs) & ".bmp"  'file2 =    "C:\WorkingDir\dummy_N_FFF.bmp"
                        My.Computer.FileSystem.CopyFile(file, file2)                                        'Copy file to file2
                    Next cnt

                    'Properly convert bmp files to MP4: ffmpeg -y -f image2 -framerate FPS -i dummy_N_%03d.bmp out.mp4
                    cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT                                            '"C:\FFMPegPath\FFMPeg.exe"
                    cmd &= " -y -f image2 -framerate " & FPS_string                                                             ' -y -f image2 -framerate FPS

                    file = strQUOT & path & strPATHSEP & "dummy_" & k.ToString() & "_%0" & dg.ToString() & "d.bmp" & strQUOT
                    cmd &= " -i " & file                                                                                    ' -i "C:\WorkingDir\dummy_M_%0Nd.bmp"

                    file = strQUOT & path & strPATHSEP & "dummy_" & k.ToString() & ".mp4" & strQUOT
                    cmd &= (" " & file)                                                                                     ' "C:\WorkingDir\out.mp4"

                    'Run cmd
                    'startInfo.FileName = cmd
                    'shell.StartInfo = startInfo
                    'shell.Start()
                    'shell.WaitForExit()
                    RunProcess(cmd)

                    'Cleanup all of the BMP frames
                    CleanUp(path, filename, r, c, m, hasPad, True)
                Else
                    UpdateProg_Cur(CurPrg, "Step 0: Video does NOT have padding for this multiplicity; skip")
                End If
                'shell.Close()
                CurPrg(0) += 1

                'Do step 1
                'Iterate through columns 1 to C
                UpdateProg_Cur(CurPrg, "Step 1: Vstack all subvideos in a column into giant columns (cN.mp4). Do for all columns (" & c.ToString() & " " & Plural(c, "column", "columns") & ")")
                For j = 1 To c Step 1
                    'ffmpeg -i input0 -i input1 -i input2 -filter_complex "[0:v][1:v][2:v]vstack=inputs=3[v]" -map "[v]" output
                    UpdateProg_Cur(CurPrg, "VStack Column " + j.ToString())
                    parm = ""                                                                                                       'Clear parm string
                    ReDim parms(r)                                                                                                  'Redim parm array to the amount of rows
                    cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y "                                           '"C:\FFMPegPath\FFMPeg.exe" -y

                    'Iterate through all rows 1 to r
                    'Concatenate all input file args ("-i filename") onto cmd, build input pads
                    For i = 1 To r Step 1
                        suffix = suffixes(i, j) & "_" & k.ToString()                                                                'Get appropriate video cell suffix ("_AX_Y")
                        file = strQUOT & path & strPATHSEP & filename & suffix & ".mp4" & strQUOT
                        cmd &= "-i " & file                                                                                         '-i "C:\WorkingDir\filename_AX_Y.mp4"
                        cmd &= " "
                        parms(i) = "[" & (i - 1).ToString() + ":v]"                                                                 'Generate input pad for element in array ("[N:v]")
                        parm &= parms(i)                                                                                            'Concatenate index onto parm
                    Next i

                    file = strQUOT & path & strPATHSEP & "c" & j.ToString() & ".mp4" & strQUOT                                          'Filename for output column video ("cN.mp4"). "C:\WorkingDir\cN.mp4"

                    If r > 1 Then
                        'If multiple rows
                        '-filter_complex "([0:v] to [r:v])vstack=inputs=r[v]" -map "[v]" -vcodec h264 "C:\WorkingDir\cN.mp4"
                        cmd &= "-filter_complex " & strQUOT
                        cmd &= parm & "vstack=inputs=" & r.ToString() & "[v]" & strQUOT & " -map " & strQUOT & "[v]" & strQUOT
                        cmd &= " -vcodec h264 " & file
                    Else
                        'If one row, just set output to "C:\WorkingDir\cN.mp4"
                        'Final cmd will be
                        '"C:\FFMPegDir\ffmpeg.exe" -y -i "C:\WorkingDir\title_AX_Y.mp4" -vcodec h264 "C:\WorkingDir\cN.mp4"
                        cmd &= "-vcodec h264 " & file
                    End If

                    'Run cmd
                    'startInfo.FileName = cmd
                    'shell.StartInfo = startInfo
                    'shell.Start()
                    'shell.WaitForExit()
                    RunProcess(cmd)
                    CurPrg(0) += 1
                Next j
                'shell.Close()

                'Do Step 2
                'Iterate through columns 1 to C                
                UpdateProg_Cur(CurPrg, "Step 2: Limit each giant vstacked column to " & (TryParseErr_UShort(frames)).ToString() & " frames (dN.mp4). Do for all columns (" & c.ToString() & " " & Plural(c, "column", "columns") & ")")
                For j = 1 To c Step 1
                    UpdateProg_Cur(CurPrg, "Frame limit column " & j.ToString() & " (c" & j.ToString() & ".mp4 to d" & j.ToString() & ".mp4)")
                    cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y "           '"C:\FFMPegDir\FFMPeg.exe" -y
                    file = strQUOT & path & strPATHSEP & "c" & j.ToString() & ".mp4" & strQUOT
                    cmd &= "-i " & file                                                             '-i "C:\WorkingDir\cN.mp4"
                    file = strQUOT & path & strPATHSEP & "d" & j.ToString() & ".mp4" & strQUOT
                    'End frame is exclusive; add one to frame count for value to use
                    cmd &= " -filter_complex trim=start_frame=0:end_frame=" & (TryParseErr_UShort(frames) + 1).ToString() & " -vcodec h264 " & file   ' -filter_complex trim=start_frame=X:end_frame=Y -vcodec h264 "C:\WorkingDir\dN.mp4"

                    '"-filter complex trim=start_frame=X:end_frame=Y" only renders frames X-Y for a video
                    'Run cmd
                    'startInfo.FileName = cmd
                    'shell.StartInfo = startInfo
                    'shell.Start()
                    'shell.WaitForExit()
                    RunProcess(cmd)
                    CurPrg(0) += 1
                Next j
                'shell.Close()

                'Do Step 3
                UpdateProg_Cur(CurPrg, "Step 3: Combine each giant, frame-limited column (dN.mp4) into a near-final composite video for this multiplicity (m" & k.ToString() & ".mp4) . Do for all columns (" & c.ToString() & " " & Plural(c, "column", "columns") & ")")
                parm = ""                                                               'Clear parm string
                ReDim parms(c)                                                          'ReDim parms to amount of columns
                cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y "   '"C:\FFMPegDir\FFMPeg.exe" - y
                'Iterate through all columns 1 to c
                'Concatenate all input file args ("-i dN.mp4") onto cmd, build input pads. Similar to Step 1 with vstack
                For j = 1 To c Step 1
                    file = strQUOT & path & strPATHSEP & "d" & j.ToString() & ".mp4" & strQUOT
                    cmd &= "-i " & file
                    cmd &= " "
                    parms(j) = "[" & (j - 1).ToString() + ":v]"
                    parm &= parms(j)
                Next j
                file = strQUOT & path & strPATHSEP & "m" & k.ToString() & ".mp4" & strQUOT

                If c > 1 Then
                    'If multiple columns
                    '-filter_complex "([0:v] to [c:v])hstack=inputs=c[v]" -map "[v]" -vcodec h264 "C:\WorkingDir\mN.mp4"
                    cmd &= "-filter_complex " & strQUOT
                    cmd &= parm & "hstack=inputs=" & c.ToString() & "[v]" & strQUOT & " -map " & strQUOT & "[v]" & strQUOT
                    cmd &= " -vcodec h264 " & file
                Else
                    'If one col, just set output to "C:\WorkingDir\mN.mp4"
                    'Final cmd will be
                    '"C:\FFMPegDir\ffmpeg.exe" -y -i "C:\WorkingDir\d1.mp4" -vcodec h264 "C:\WorkingDir\mN.mp4"
                    cmd &= " -vcodec h264 " & file
                End If

                'Run cmd
                'startInfo.FileName = cmd
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd)
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg, "Step 4: Repeat Steps 1-3 for each multiplicity")
            Next k  'Do Step 4
            CurPrg(0) = CurPrg(1)
            UpdateProg_Cur(CurPrg, "Steps 0-4 completed!", False, True)

            'Do Step 5
            TtlPrg(0) += 1
            CurPrg(0) = 0
            CurPrg(1) = 1
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 5: Concatenate each composite multiplicity video (all 'mN.mp4' files in step 3) to a nearly-final mp4 file ('" & filename & ".mp4')")

            'https://stackoverflow.com/questions/5415006/ffmpeg-combine-merge-multiple-mp4-videos-not-working-output-only-contains-the
            'ffmpeg -f concat -i inputs.txt -vcodec h264 Mux1.mp4
            If m > 1 Then
                'If video has multiplicity
                UpdateProg_Cur(CurPrg, "Video has multiplicity! Creating near-final composite video...", True, False)

                '"C:\FFMPegDir\FFMPeg.exe" -y -f concat -i
                cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y -f concat -i "

                'Redim files to 0-based multiplicity
                ReDim Files(m - 1)
                'Iterate multiplicity from 1 to m
                For k = 1 To m Step 1
                    '0-based file index = "mN.mp4", where "N" is 1-based
                    Files(k - 1) = "m" & k.ToString() & ".mp4"
                Next k
                WriteTxtFile(path, Files)                                                                               'Write file list (File.txt) to WorkingDir
                file = strQUOT & path & strPATHSEP & "File.txt" & strQUOT                                               'That file is located at "C:\WorkingDir\File.Txt"
                cmd &= file & " -vcodec h264 " & strQUOT & path & strPATHSEP & filename & ".mp4" & strQUOT '"C:\WorkingDir\File.Txt" -vcodec h264 "C:\WorkingDir\filename.mp4"

                'Run cmd
                'startInfo.FileName = cmd
                'startInfo.WorkingDirectory = path
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd, , , , , path)
            Else
                'If video has no multiplicity, just copy "C:\WorkingDir\m1.mp4" to "C:\WorkingDir\filename.mp4"
                UpdateProg_Cur(CurPrg, "Video does NOT have multiplicity! Use 1st/only multiplicity as near-final composite video...", True, False)
                file = path & strPATHSEP & "m1.mp4"             '"C:\WorkingDir\m1.mp4"
                file2 = path & strPATHSEP & filename & ".mp4"   '"C:\WorkingDir\filename.mp4"
                My.Computer.FileSystem.CopyFile(file, file2)
            End If
            CurPrg(0) += 1
            UpdateProg_Cur(CurPrg, "Near-final " & filename & ".mp4 composite video created!", False, True)


            'If we have dummy padding, concatenate each of the dummy_*.mp4 files into dummy.mp4,
            'then vstack filename.mp4 with dummy.mp4 for final.mp4. Rename final.mp4 to filename.mp4 and replace
            TtlPrg(0) += 1
            CurPrg(0) = 0
            CurPrg(1) = 2
            UpdateProg_Cur(CurPrg)
            text = "Step 6: If video has padding, concatenate all dummy video multiplicities ('dummy_N.mp4' in step 0) to a composite dummy video ('dummy.mp4')" & strNL
            text &= "Step 7: If video has padding, vstack the video in step 5 ('" & filename & ".mp4') with the composite dummy video in step 6 ('dummy.mp4') into a file called 'final.mp4', then rename as final '" & filename & ".mp4'"
            UpdateProg_Ttl(TtlPrg, text)
            If hasPad Then
                'If padding, Do Step 6
                UpdateProg_Cur(CurPrg, "Video has padding; do steps 6 & 7!", True, False)
                If m > 1 Then
                    UpdateProg_Cur(CurPrg, "Video has multiplicity; concatenating all dummy videos to compositie (dummy.mp4)")
                    'If multiplicity, concatenate all dummy_N.mp4 to dummy.mp4

                    'Setup similar to Step 5
                    '"C:\FFMPegDir\FFMPeg.exe" -y -f concat -i
                    cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y -f concat -i "
                    'Redim files to 0-based multiplicity
                    ReDim Files(m - 1)
                    'Iterate multiplicity from 1 to m
                    For k = 1 To m Step 1
                        '0-based file index = "dummy_N.mp4", where "N" is 1-based
                        Files(k - 1) = "dummy_" & k.ToString() & ".mp4"
                    Next k
                    WriteTxtFile(path, Files)                                                                       'Write file list (File.txt) to WorkingDir
                    file = strQUOT & path & strPATHSEP & "File.txt" & strQUOT                                           'That file is located at "C:\WorkingDir\File.Txt"
                    cmd &= file & " -vcodec h264 " & strQUOT & path & strPATHSEP & "dummy.mp4" & strQUOT   '"C:\WorkingDir\File.Txt" -vcodec h264 "C:\WorkingDir\dummy.mp4"

                    'Run cmd
                    'startInfo.FileName = cmd
                    'startInfo.WorkingDirectory = path
                    'shell.StartInfo = startInfo
                    'shell.Start()
                    'shell.WaitForExit()
                    'shell.Close()
                    RunProcess(cmd, , , , , path)
                Else
                    'If no multiplicity, copy "C:\WorkingDir\dummy_1.mp4" to "C:\WorkingDir\dummy.mp4"
                    UpdateProg_Cur(CurPrg, "Video does NOT have multiplicity; just use only dummy video (dummy_1.mp4) as compositie (dummy.mp4)")
                    file = path & strPATHSEP & "dummy_1.mp4"
                    file2 = path & strPATHSEP & "dummy.mp4"
                    My.Computer.FileSystem.MoveFile(file, file2, True)
                End If
                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg, "Composite dummy video (dummy.mp4) created!", False, True)


                'Do Step 7
                'vstack filename.mp4 with dummy.mp4 into final.mp4
                'ffmpeg -i top.mp4 -i bot.mp4 -filter_complex vstack output.mp4
                UpdateProg_Ttl(TtlPrg, "Step 7: Vstack the video in step 5 ('" & filename & ".mp4') with the composite dummy video in step 6 ('dummy.mp4') into a file called 'final.mp4', then rename as final '" & filename & ".mp4'")
                UpdateProg_Cur(CurPrg, "VStacking composite dummy video (dummy.mp4) to bottom of base composite video (" & filename & ".mp4)...", True, False)

                cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y"    '"C:\FFMPegDir\FFMPeg.exe" -y
                file = strQUOT & path & strPATHSEP & filename & ".mp4" & strQUOT
                cmd &= " -i " & file                                                    ' -i "C:\WorkingDir\filename.mp4"
                file = strQUOT & path & strPATHSEP & "dummy.mp4" & strQUOT

                cmd &= " -i " & file
                cmd &= " -filter_complex vstack -vcodec h264 "
                file = strQUOT & path & strPATHSEP & "final.mp4" & strQUOT
                cmd &= file                                                             ' -i "C:\WorkingDir\dummy.mp4 -filter_complex vstack -vcodec h264 "C:\WorkingDir\final.mp4""

                'Run cmd
                'startInfo.FileName = cmd
                'shell.StartInfo = startInfo
                'shell.Start()
                'shell.WaitForExit()
                'shell.Close()
                RunProcess(cmd)

                'MoveFile("C:\WorkingDir\final.mp4"->"C:\WorkingDir\filename.mp4")
                file = path & strPATHSEP & "final.mp4"
                file2 = path & strPATHSEP & filename & ".mp4"
                My.Computer.FileSystem.MoveFile(file, file2, True)

                CurPrg(0) += 1
                UpdateProg_Cur(CurPrg, "Final composite video (" & filename & ".mp4) created!", False, True)
            Else
                CurPrg(0) = CurPrg(1)
                UpdateProg_Cur(CurPrg, "Video does NOT have padding; skip steps 6 & 7!", False, True)
            End If

            'Do Step 7.2: Convert filename.mp4 to yuv420p format
            'cmd = strQUOT & txtFFMpeg.Text & strBAK & exeFMPeg & strQUOT & " -y"    '"C:\FFMPegDir\FFMPeg.exe" -y 
            'file = strQUOT & path & strBAK & filename & ".mp4" & strQUOT            ' -i "C:\WorkingDir\filename.mp4"
            'cmd &= " -i " & file
            'file = strQUOT & path & strBAK & "final.mp4" & strQUOT                  ' -pix_fmt yuvj420p -vcodec h264 "C:\WorkingDir\final.mp4"
            'cmd &= " -pix_fmt yuvj420p -vcodec h264 " & file
            'Run cmd
            'startInfo.FileName = cmd
            'shell.StartInfo = startInfo
            'shell.Start()
            'shell.WaitForExit()
            'shell.Close()

            'MoveFile C:\WorkingDir\final.mp4 -> C:\WorkingDir\filename.mp4
            'file = path & strBAK & "final.mp4"                      'C:\WorkingDir\final.mp4
            'file2 = path & strBAK & filename & ".mp4"               'C:\WorkingDir\filename.mp4
            'My.Computer.FileSystem.MoveFile(file, file2, True)


            'Do Step 8: Output to .bmp frames            
            i = TryParseErr_Byte(txtTE_D.Text)                                              'Set i to amount of digits in framelimit
            j = TryParseErr_UShort(txtTE_F.Text)                                              'Set j to amount of frames
            TtlPrg(0) += 1
            CurPrg(0) = 0
            CurPrg(1) = j * m
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 8: Convert final video ('" & filename & ".mp4') into BMP frames, padded to " & i.ToString() & " digits ('frame_%0Nd.bmp')")
            UpdateProg_Cur(CurPrg, "Generating ~" & CurPrg(1).ToString() & " BMP frames. Please wait; this shall take some time...", True, False)

            cmd = strQUOT & txtFFMPEG.Text & strPATHSEP & exeFMPeg & strQUOT & " -y "           '"C:\FFMPegDir\FFMPeg.exe" -y 
            file = strQUOT & path & strPATHSEP & filename & ".mp4" & strQUOT
            cmd &= "-i " & file                                                             '-i "C:\WorkingDir\filename.mp4"
            file = strQUOT & path & strPATHSEP & "frame_%0" & i.ToString() & "d.bmp" & strQUOT
            cmd &= " " & file                                                               ' "C:\WorkingDir\frame_%0Nd.bmp"

            'Run cmd
            'startInfo.FileName = cmd
            'shell.StartInfo = startInfo
            'shell.Start()
            RunProcess(cmd, Nothing, Nothing, True, shell)
            'shell.WaitForExit()
            'Loop while cmd is still running
            While shell.HasExited = False
                'Count current amount of frame_*.bmp frames, set as current progress, then updated
                k = CountFilesFromFolder(path, "frame_*.bmp")
                CurPrg(0) = k
                UpdateProg_Cur(CurPrg)
            End While
            shell.Close()
            shell.Dispose()
            shell = Nothing
            CurPrg(0) = CurPrg(1)
            UpdateProg_Cur(CurPrg, "All BMP frames ripped!", False, True)


            'Do Step 8.1: Hack INI file, throw error if failure
            TtlPrg(0) += 1
            CurPrg(0) = 0
            CurPrg(1) = 1
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 8.1: Find, copy, and hack Irfanview advanced options INI file")
            UpdateProg_Cur(CurPrg, "Hack Irfanview INI file...", True, False)
            Dim success As Boolean = HackINIFile(path)
            If success = False Then Throw New System.Exception("Irfanview INI hack failed!")
            CurPrg(0) = CurPrg(1)
            UpdateProg_Cur(CurPrg, "Irfanview INI hack successful!", False, True)
            Log_MsgBox(Nothing, "Disabled Progressive JPG for conversions!", MsgBoxStyle.Information, "Irfanview settings INI Hack successful!")


            'Do Step 8.2: Convert .bmp frames to .jpg frames            
            Dim startInfo As ProcessStartInfo
            startInfo = New ProcessStartInfo
            startInfo.UseShellExecute = False
            shell = New Process()

            TtlPrg(0) += 1
            cnt = TryParseErr_Byte(txtTE_D.Text)                                                    'Get amount of digits            
            j = CountFilesFromFolder(path, "frame_*.bmp")                                           'Count amount of BMP frames
            CurPrg(0) = 0
            CurPrg(1) = j
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 8.2: Convert BMP frames into JPG frames, using Irfanview and the JPG Quality value (" & nudTE_jpgq.Value.ToString() & "%)")
            UpdateProg_Cur(CurPrg, "Generating " & j.ToString() & " JPG " & Plural(j, "frame", "frames") & ". Please wait; this shall take some time...", True, False)
            Log_MsgBox(Nothing, "Generating JPG frames; please wait!", MsgBoxStyle.Information, "JPG Rendering")
            For i = 1 To j                                                                          'Iterate frames from 1 to j
                cmd = strQUOT & txtiView.Text & strQUOT                                             '"C:\iView32\iView32.exe"
                file2 = StrDup(cnt, "0")                                                            '"0Nd". Create ToString dig formatter
                file = strQUOT & path & strPATHSEP & "frame_" & i.ToString(file2) & ".bmp" & strQUOT
                cmd &= " " & file                                                                   ' "C:\WorkingDir\frames_%0Nd.bmp
                'cmd &= " /jpgq=" & nudTE_jpgq.Value.ToString() & " /convert="                      ' /jpgq=N /convert-
                cmd &= " /ini /jpgq=" & nudTE_jpgq.Value.ToString() & "/convert="                                                            '/ini /convert=
                file = strQUOT & path & strPATHSEP & "frame_" & i.ToString(file2) & ".jpg" & strQUOT
                cmd &= file                                                                   ' "C:\WorkingDir\frames_%0Nd.jpg

                'Manually run cmd. We do NOT want to use RunProcess here, due to Irfanview working differently from FFMPEG suite!
                startInfo.FileName = cmd
                shell.StartInfo = startInfo
                shell.Start()
                shell.WaitForExit()
                CurPrg(0) = i
                UpdateProg_Cur(CurPrg)
            Next i
            shell.Close()
            shell.Dispose()
            shell = Nothing
            CurPrg(0) = CurPrg(1)
            UpdateProg_Cur(CurPrg, "All JPG frames generated!", False, True)


            'Do Step 9
            TtlPrg(0) += 1
            CurPrg(0) = 0
            CurPrg(1) = 1
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 9: Check the output directory, delete any extra JPG frames past the framelimit (" & (frames * m).ToString() & "), and then cleanup all BMP files")
            file = "frame_*.jpg"
            DeleteExtraFilesFromFolder(path, file, frames * m)
            CurPrg(1) = 1
            CurPrg(0) = CurPrg(1)
            UpdateProg_Cur(CurPrg, "Deleted all extra JPG files!", False, True)
            file = "frame_*.bmp"
            DeleteFilesFromFolder(path, file, True, "Deleting all BMP frames; please wait...", True, False)
            CurPrg(1) = 1
            CurPrg(0) = CurPrg(1)
            UpdateProg_Cur(CurPrg, "Deleted all BMP frames!", False, True)

            'Do Step 10
            TtlPrg(0) += 1
            CurPrg(0) = 0
            CurPrg(1) = 1
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 10: Feed the JPG frames and any audio files ('" & filename & ".wav') into THPConv.exe to generate " & filename & ".thp (FPS=" & FPS_string & ")")
            UpdateProg_Cur(CurPrg, "Generating THP...", True, False)
            Dim hasAudio As Boolean = THPHasAudio()
            If hasAudio = False Then
                'If no audio, just convert jpg frames into THP using THPConv at appropriate framerate
                UpdateProg_Cur(CurPrg, "Video does NOT have audio!")

                '"C:\THPConvDir\THPConv.exe" -j "C:\WorkingDir\*.jpg" -r RR.RR -d "C:\WorkingDir\filename.thp"
                cmd = strQUOT & txtTHPConv.Text & strQUOT
                file = "-j " & strQUOT & path & strPATHSEP & "*.jpg" & strQUOT
                cmd &= " " & file
                cmd &= " -r " & FPS_string
                file = strQUOT & path & strPATHSEP & filename & ".thp" & strQUOT
                cmd &= " -d " & file
            Else
                'If audio, convert jpg frames and add audio file at appropriate framerate
                UpdateProg_Cur(CurPrg, "Video has audio!")

                '"C:\THPConvDir\THPConv.exe" -j "C:\WorkingDir\*.jpg" -s "C:\WorkingDir\filename.wav" -r RR.RR -d "C:\WorkingDir\filename.thp"
                cmd = strQUOT & txtTHPConv.Text & strQUOT
                file = "-j " & strQUOT & path & strPATHSEP & "*.jpg" & strQUOT
                cmd &= " " & file
                file = strQUOT & path & strPATHSEP & filename & ".wav" & strQUOT
                cmd &= " -s " & file
                cmd &= " -r " & FPS_string
                file = strQUOT & path & strPATHSEP & filename & ".thp" & strQUOT
                cmd &= " -d " & file
            End If

            'Run cmd
            'startInfo.FileName = cmd
            'shell.StartInfo = startInfo
            'shell.Start()
            'shell.WaitForExit()
            'shell.Close()
            RunProcess(cmd)
            CurPrg(0) += 1
            UpdateProg_Cur(CurPrg, "THP (hopefully) generated!", False, True)
            Log_MsgBox(Nothing, "THP rendered! Now cleaning up...", MsgBoxStyle.Information, "Success!")

            'Step 11
            TtlPrg(0) += 1
            CurPrg(0) = 0
            CurPrg(1) = 1
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 11: Cleanup all temporary files")
            Dim textS() As String =
            {
                "Delete padless final video (" & filename & ".mp4)",
                "Delete vstacked column videos(cN.mp4, dN.mp4)",
                "Delete all composite multiplicity videos (mN.mp4)",
                "Delete all JPG frames",
                "Delete all dummy BMP extension frames for all multiplcities",
                "Delete all dummy videos",
                "Delete final composite video (with padding, final.mp4)",
                "Delete File.txt (Irfanview merge file), and i_view32.ini/i_view32_temp.ini (hacked Irfanview INI files)"
            }
            Dim textE() As String =
            {
                filename & ".mp4 deleted!",
                "cN and dN.mp4 columns deleted!",
                "mN.mp4 multiplicity composite videos deleted!",
                "All JPG frames deleted!",
                "All dummy BMP extension frames deleted!",
                "All dummy videos deleted!",
                "final.mp4 composite video deleted!",
                "File.txt, i_view32(_temp).ini deleted!"
            }
            CleanUp(path, filename, r, c, m, hasPad, False, True, textS, textE)

            'Step 12: Done!
            TtlPrg(0) += 1
            CurPrg(1) = 1
            CurPrg(0) = CurPrg(1)
            UpdateProg_Cur(CurPrg)
            UpdateProg_Ttl(TtlPrg, "Step 12: DONE")
            UpdateProg_Cur(CurPrg, "THP Encoding finished!", True, True)

            'Stop elevator music (if allowed)
            If chkAudio.Checked Then
                My.Computer.Audio.Stop()
                My.Computer.Audio.Play(My.Resources.success, AudioPlayMode.Background)
            End If
            Log_MsgBox(Nothing, "Done!", MsgBoxStyle.Information, "Tada!", True)
        Catch ex As Exception
            If IsNothing(shell) = False Then
                shell.Close()
                shell.Dispose()
                shell = Nothing
            End If
            Me.Cursor = Cursors.Default
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error during Encoding!", True)
        End Try
        HideApp_notTHPEnc(True) 'Restore main form
    End Sub

    ''' <summary>
    ''' Toggles visibility of all groups boxes in THP tab OTHER than grpLog and grpTHPEnc
    ''' </summary>
    ''' <param name="enabled">Visible?</param>
    ''' <remarks></remarks>
    Private Sub HideApp_notTHPEnc(ByVal enabled As Boolean)
        grpTHPInfo.Visible = enabled
        grpTHPDec.Visible = enabled
        grpHelp.Visible = enabled
        grpOptions.Visible = enabled
    End Sub

    ''' <summary>
    ''' Given a regular english noun and a count, returns its singular/plural counterpart
    ''' </summary>
    ''' <param name="noun">Nounr</param>
    ''' <param name="count">Count</param>
    ''' <returns>Singular/plural form</returns>
    ''' <remarks></remarks>
    Private Function Plural(ByVal count As UShort, ByVal singular As String, ByVal _plural As String)
        Dim noun As String
        If count = 1 Then
            noun = singular
        Else
            noun = _plural
        End If
        Return noun
    End Function

    ''' <summary>
    ''' Update Total progress bar
    ''' </summary>
    ''' <param name="value">array(2), 0=current value, 1=max progress</param>
    ''' <param name="text">Message to display</param>
    ''' <remarks></remarks>
    Private Sub UpdateProg_Ttl(ByRef value() As Single, Optional ByVal text As String = Nothing)
        'Update progress for Total, use values, display message, set text, don't wait
        UpdateProg(True, value, text, True, False)
    End Sub

    ''' <summary>
    ''' Update current progress bar
    ''' </summary>
    ''' <param name="value">array(2), 0=current value, 1=max progress</param>
    ''' <param name="text">Message to display</param>
    ''' <param name="_set">Set text?</param>
    ''' <param name="_wait">Wait?</param>
    ''' <remarks></remarks>
    Private Sub UpdateProg_Cur(ByRef value() As Single, Optional ByVal text As String = Nothing, Optional ByVal _set As Boolean = False, Optional ByVal _wait As Boolean = True)
        UpdateProg(False, value, text, _set, _wait)
    End Sub

    ''' <summary>
    ''' General meat of updating a progress bar
    ''' </summary>
    ''' <param name="type">Type of progress bar (false = current, true = total)</param>
    ''' <param name="value">array(2), 0=current value, 1=max progress</param>
    ''' <param name="text">Message to display</param>
    ''' <param name="_set">Set text?</param>
    ''' <param name="_wait">Wait?</param>
    ''' <remarks></remarks>
    Private Sub UpdateProg(ByVal type As Boolean, ByRef value() As Single, ByVal text As String, ByVal _set As Boolean, ByVal _wait As Boolean)
        'Logging data
        Dim prog As Single      'Progress% for this bar
        Dim prog2 As Single     'Progress% for other bar
        Dim logText As String   'Text for logging

        Dim str_SingleA As String = ""                  '1st percentage value.toString register for text logging
        Dim str_SingleB As String = ""                  '2nd percentage value.toString register for text logging
        Const pcent_type As Single_ToString_Types = Single_ToString_Types.PERCENT   'Constant enum value for percent Single conv type

        Dim prg As System.Windows.Forms.ProgressBar     'Obj ref to this prog bar
        Dim prg2 As System.Windows.Forms.ProgressBar    'Obj ref to other prog bar
        Dim lbl As System.Windows.Forms.Label           'Obj ref to this prog bar's text percentage progress
        Dim txt As System.Windows.Forms.TextBox         'Obj ref to this prog bar's log textbox

        'Handle proper refs
        If type = False Then
            'If current progress
            prg = prgCur                                'Set this progbar ref to current
            prg2 = prgTotal                             'Set other progbar ref to total
            lbl = lblTHPEnc_Prg_Cur                     'Set progbar lbl ref to current's
            txt = txtTHPEnc_Prg_Cur                     'Set progbar txtlog ref to current's
        Else
            'If total progress
            prg = prgTotal                              'Set this progbar ref to total
            prg2 = prgCur                               'Set other progbar ref to current
            lbl = lblTHPEnc_Prg_Ttl                     'Set progbar lbl ref to total's
            txt = txtTHPEnc_Prg_Ttl                     'Set progbar txtlog ref to total's
        End If

        'Set this progbar minimum to 0, max to value(1)
        prg.Minimum = 0
        prg.Maximum = value(1)
        'Force a progressbar update for fast rendering
        prg.Update()

        'If value(1) max progress is 0, set current value to 0, max to 1
        'This prevents DivByZero error a few blocks below, and will force 0% progress (instead of 0 out of 0 = 100%)
        If value(1) = 0 Then
            value(0) = 0
            value(1) = 1
        End If

        'Prevent OOB errors with progressbar.value
        If value(0) > value(1) Then
            'If current value > max, then set current to max
            value(0) = value(1)
        ElseIf value(0) < 0 Then
            'If current value < min 0, then set current to min
            value(0) = 0
        End If
        prg.Value = value(0)                    'Set this progress bar current progress
        prog = value(0) / value(1)              'Get this progress bar's progress as %

        prog2 = prg2.Value / prg2.Maximum       'Get other progress bar's progress as %
        lbl.Text = Single_ToString(prog, pcent_type)  'Display progress as 2-dig% ("iii.dd%")
        lbl.Update()                            'Force a lbl update for fast rendering

        'Handle message (if not null)
        If IsNothing(text) = False Then
            If _set Then
                'If set, then set text
                txt.Text = text & strNL
            Else
                'If not set, then append text + CRLF
                txt.AppendText(text & strNL)
            End If
            txt.Update()                    'Force a txt update for fast rendering

            'If text not empty, log progresses and message
            If text <> String.Empty Then
                'Log:
                'THPEnc progress (ttl, cur) = (ttl%,cur%):
                'Message
                If type = False Then
                    str_SingleA = Single_ToString(prog2, pcent_type)
                    str_SingleB = Single_ToString(prog, pcent_type)
                Else
                    str_SingleA = Single_ToString(prog, pcent_type)
                    str_SingleB = Single_ToString(prog2, pcent_type)
                End If
                logText = "THPEnc progress (ttl, cur) = (" & str_SingleA & "," & str_SingleB & "):" & strNL & text
                Log(logText)
            End If

            'If wait flag, stall app by 3s
            If _wait Then Threading.Thread.Sleep(3000)
        Else
            'If text is null BUT CLI_MODE, log progress. This allows showing of intermediary progres in CLI_MODE
            If type = False Then
                str_SingleA = Single_ToString(prog2, pcent_type)
                str_SingleB = Single_ToString(prog, pcent_type)
            Else
                str_SingleA = Single_ToString(prog, pcent_type)
                str_SingleB = Single_ToString(prog2, pcent_type)
            End If
            logText = "THPEnc progress (ttl, cur) = (" & str_SingleA & "," & str_SingleB & ")"
            Log(logText)
        End If
    End Sub

    ''' <summary>
    ''' Handles btnLog click (reset progress bars, clear all logs)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLogClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogClear.Click
        Dim value(2) As Single  'array(2), 0=current progress value, 1=max progress    
        'Set progress to 0/1 (0%)
        value(0) = 0
        value(1) = 1

        'Set log to empty, display null icon
        Log("", MsgBoxStyle.MsgBoxHelp, True)

        'Set total progress, set text to empty
        UpdateProg_Ttl(value, "")
        'Set current progress, set text to empty, no wait
        UpdateProg_Cur(value, "", True, False)
    End Sub

    ''' <summary>
    ''' Handles btnLogSave click (save log file)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLogSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogSave.Click
        'Load the SaveLog ofd, user selects whatever.log

        'Default filename stuff

        Const ext As String = ".log"
        Const name As String = "thwimp"   'Base filename
        Const dateFormat As String = "MMddyyyy_HHmmss"
        Dim _datetime As String = ""        'Current date
        Dim _file As String = ""        'Final filename

        'Try-Catch-block in case of stupid Y2K-like bug in the far future
        Try
            _datetime = DateTime.Now.ToString(dateFormat)
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Exclamation, "Date/Time error in btnLogSave_Click! (Future Y2K-like bug?)", True)
        End Try

        If _datetime <> String.Empty Then
            'If date and time strings are not null, then filename is "thwimp_MMddyyyy_HHmmss.log"
            _file = name & "_" & _datetime & ext
        Else
            'If date or time strings are null (in case of future Y2K-like bug), then filename is "thwimp.log"
            _file = name & ext
        End If

        Try
            'Set initial directory to app dir, and defualt filename
            SaveLog.FileName = _file
            SaveLog.InitialDirectory = strPATH
            If SaveLog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            SaveLogFile(SaveLog.FileName)
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in btnLogSave_Click!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Save log file
    ''' </summary>
    ''' <param name="_file">Filename</param>
    ''' <remarks></remarks>
    Private Sub SaveLogFile(ByVal _file As String)
        Dim success = False 'Success?
        Dim xFileData As StreamWriter = Nothing
        Try
            xFileData = New StreamWriter(_file, False, System.Text.Encoding.ASCII)
            Dim strEntry As String = txtLog.Text
            xFileData.Write(strEntry)
            KillStream(xFileData, False, _file)
            success = True
        Catch ex As Exception
            'Kill lingering stream (but DON'T delete; corrupted error log file is better than nothing...)
            KillStream(xFileData, False, _file)
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error saving log file!")
        End Try

        'If success writing, then clear textboxes
        If success Then btnLogClear.PerformClick()
    End Sub

    ''' <summary>
    ''' Logs text
    ''' </summary>
    ''' <param name="text">Text</param>
    ''' <param name="style">Icon to display (if any; see remarks)</param>
    ''' <param name="_Set">Set text?</param>
    ''' <remarks>
    ''' Valid styles/icon pairs:
    ''' MsgBoxStyle.OkOnly = don't change
    ''' MsgBoxStyle.Critical = SystemIcons.Error
    ''' MsgBoxStyle.Exclamation = SystemIcons.Exclamation
    ''' MsgBoxStyle.Information = SystemIcons.Information
    ''' MsgBoxStyle.Question = SystemIcons.Question
    ''' Other = NullIcon
    ''' </remarks>
    Private Sub Log(ByVal text As String, Optional ByVal style As MsgBoxStyle = MsgBoxStyle.OkOnly, Optional ByVal _Set As Boolean = False)
        'Bitmap of icon to display (if any)
        Dim bitmap As System.Drawing.Bitmap = Nothing

        'If set flag, set; else append text+CRLF
        If _Set = True Then
            txtLog.Text = text
        Else
            text = text & strNL
            txtLog.AppendText(text)
        End If
        'Force a text update for fast rendering
        txtLog.Update()

        'Handle valid styles for valid icons
        Select Case style
            'Corresponding, appropriate styles vs. icons
            Case MsgBoxStyle.Critical
                bitmap = System.Drawing.SystemIcons.Error.ToBitmap()
            Case MsgBoxStyle.Exclamation
                bitmap = System.Drawing.SystemIcons.Exclamation.ToBitmap()
            Case MsgBoxStyle.Information
                bitmap = System.Drawing.SystemIcons.Information.ToBitmap()
            Case MsgBoxStyle.Question
                bitmap = System.Drawing.SystemIcons.Question.ToBitmap()
            Case MsgBoxStyle.OkOnly
                'NOP; don't handle                
            Case Else
                'Anthing else = nullicon
                bitmap = My.Resources.nullIcon
        End Select

        'if bitmap not null, then update log icon
        If IsNothing(bitmap) = False Then
            picLog.Image = bitmap
            'Force a pic update for fast rendering
            picLog.Update()
        End If

        'Spit output to CMD Prompt if CLI_MODE and attached
        If CLI_MODE And CLI_Attached Then Console.WriteLine(text)
    End Sub

    ''' <summary>
    ''' Log-based MsgBox() wrapper which logs the message, title, and icon to the log box,
    ''' as well as handles chkMsgBox flag
    ''' </summary>    
    ''' <param name="ex">Exception (for line numbering)</param>
    ''' <param name="msg">Message</param>
    ''' <param name="style">New icon</param>
    ''' <param name="title">Title</param>
    ''' <param name="msgBox_override">Forcibly show msgBox, despite chkMsg flag?</param>
    ''' <remarks>Also suppresses some annoying, informational based msgboxes in THP Encoding process</remarks>
    Private Sub Log_MsgBox(ByVal ex As System.Exception, ByVal msg As String, ByVal style As MsgBoxStyle, ByVal title As String, Optional ByVal msgBox_override As Boolean = False)
        'Text to log:

        'MsgBox:
        'title: title
        'msg: msg
        'icon: iconStyle  
        '[optional]Line number: ###

        'Get line number (if any) of error
        'http://www.vbforums.com/showthread.php?645850-RESOLVED-How-to-get-exact-error-line-number
        Dim line As String = ""                                                                         'Line string
        If IsNothing(ex) = False Then                                                                   'If ex exists
            Dim trace As System.Diagnostics.StackTrace = New System.Diagnostics.StackTrace(ex, True)    'Get strack trace
            Dim number As String = trace.GetFrame(0).GetFileLineNumber().ToString()                     'Get stack trace number
            line = "Line number: " & number                                                             'Create string
        End If

        Dim text As String = strNL & "MsgBox:" & strNL & "title: " & title & strNL & "msg: " & msg & strNL & "icon: " & style.ToString() & strNL & line

        'Handle some style-specific stuff
        If style = MsgBoxStyle.Information Then
            'If informational

            'If NOT CLI mode
            If CLI_MODE = False Then
                'If show all message boxes OR override flag, then display the message box
                If chkMsg.Checked = False Or msgBox_override = True Then MsgBox(msg, style, title)
            End If
        Else
            'If Critical (error) and Audio enabled
            If style = MsgBoxStyle.Critical And chkAudio.Checked Then
                'Stop all audio (esp elevator bgm)
                My.Computer.Audio.Stop()
                'Play error sound
                My.Computer.Audio.Play(My.Resources._error, AudioPlayMode.Background)
            End If

            'If NOT CLI mode, always display errors
            If CLI_MODE = False Then
                MsgBox(msg, style, title)
            End If
        End If

        'Log the msgBox text
        Log(text, style)
    End Sub

    ''' <summary>
    ''' Closes app if CLI_MODE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseApp_CLI()
        If CLI_MODE Then
            If CLI_Attached Then FreeConsole() 'Destroy the console if attached
            Me.Close()                         'Close app!
        End If
    End Sub

    ''' <summary>
    ''' Cleans up temporary files during THP encoding, optionally logs current progress
    ''' </summary>
    ''' <param name="Path">Path to working dir</param>
    ''' <param name="filename">Filename for the THP</param>
    ''' <param name="r">Amount of rows in subvideo array</param>
    ''' <param name="c">Amount of cols in subvideo array</param>
    ''' <param name="m">Multiplicity for subvids</param>
    ''' <param name="Haspad">Does video use padding?</param>
    ''' <param name="justBMPs">Just cleanup bmps for dummy video encoding?</param>
    ''' <param name="track">Current progress tracking?</param>
    ''' <param name="track_stringS">Array of start strings for tracking</param>
    ''' <param name="track_stringE">Array of end strings for tracking</param>
    ''' <remarks>Array of start/end strings = 8; see code for index usage</remarks>
    Private Sub CleanUp(ByVal Path As String, ByVal filename As String, ByVal r As Byte, ByVal c As Byte, ByVal m As Byte, ByVal Haspad As Boolean, ByVal justBMPs As Boolean, Optional ByVal track As Boolean = False, Optional ByVal track_stringS() As String = Nothing, Optional ByVal track_stringE() As String = Nothing)
        'array(2), 0=current progress, 1=max progress
        Dim CurPrg(2) As Single

        'If either start/end string arrays are null, then redim to 8.
        'Prevents further processing issues with optional params
        If IsNothing(track_stringS) Or IsNothing(track_stringE) Then
            ReDim track_stringS(8)
            ReDim track_stringE(8)
        End If

        Try
            'Generic iterators
            'Dim i As UShort       'Rows
            Dim j As UShort       'Cols
            Dim k As UShort       'Multiplicity
            Dim File As String    'File to check against/to delete/to whatever

            If justBMPs = False Then
                'Delete thpfilename.mp4 (final mp4 without padding) if exists
                File = Path & strPATHSEP & filename & ".mp4"
                If track = True Then
                    CurPrg(0) = 0
                    CurPrg(1) = 1
                    UpdateProg_Cur(CurPrg, track_stringS(0), True, False)
                    If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                    CurPrg(0) = 1
                    UpdateProg_Cur(CurPrg, track_stringE(0), False, True)
                Else
                    If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                End If


                'Delete column videos
                'Iterate cols from 1 to c
                k = 0
                If track = True Then
                    CurPrg(0) = 0
                    CurPrg(1) = c * 2
                    UpdateProg_Cur(CurPrg, track_stringS(1), True, False)
                End If

                For j = 1 To c Step 1
                    'if cN.mp4 or dN.mp4 exists, delete
                    File = Path & strPATHSEP & "c" & j.ToString() & ".mp4"
                    If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                    If track = True Then
                        k += 1
                        CurPrg(0) = k
                        UpdateProg_Cur(CurPrg)
                    End If

                    File = Path & strPATHSEP & "d" & j.ToString() & ".mp4"
                    If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                    If track = True Then
                        k += 1
                        CurPrg(0) = k
                        UpdateProg_Cur(CurPrg)
                    End If
                Next j

                If track Then
                    CurPrg(0) = CurPrg(1)
                    UpdateProg_Cur(CurPrg, track_stringE(1), False, True)
                End If


                'Delete multiplicity videos
                'Iterate mult from 1 to m
                If track = True Then
                    CurPrg(0) = 0
                    CurPrg(1) = m
                    UpdateProg_Cur(CurPrg, track_stringS(2), True, False)
                End If

                For k = 1 To m Step 1
                    'if mN.mp4 exists, delete
                    File = Path & strPATHSEP & "m" & k.ToString() & ".mp4"
                    If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                    If track = True Then
                        CurPrg(0) = k
                        UpdateProg_Cur(CurPrg)
                    End If
                Next k

                If track = True Then
                    CurPrg(0) = CurPrg(1)
                    UpdateProg_Cur(CurPrg, track_stringE(2), False, True)
                End If


                'Delete all jpg frames (used for THP video stream)
                DeleteFilesFromFolder(Path, "*.jpg", track, track_stringS(3), True, False)
                CurPrg(1) = 1
                CurPrg(0) = CurPrg(1)
                UpdateProg_Cur(CurPrg, track_stringE(3), False, True)
            End If


            'If video has padding, delete temp files for dummy padding
            If Haspad Then
                'Delete BMP frames from dummy videos (dummy_N_%0Nd_.bmp)
                'Iterate multiplicity from 1 to m                
                CurPrg(0) = 0
                CurPrg(0) = 1
                UpdateProg_Cur(CurPrg, track_stringS(4), True, False)
                For k = 1 To m Step 1
                    Dim srch As String = "dummy_" & k.ToString() & "_*.bmp"
                    Dim text As String = "mult=" & k.ToString()
                    DeleteFilesFromFolder(Path, srch, track, text)
                    CurPrg(1) = 1
                    CurPrg(0) = CurPrg(1)
                    UpdateProg_Cur(CurPrg, "mult=" & k.ToString() & " deleted!")
                Next k
                If track Then
                    CurPrg(1) = 1
                    CurPrg(0) = CurPrg(1)
                    UpdateProg_Cur(CurPrg, track_stringE(4), False, True)
                End If


                If justBMPs = False Then
                    'Delete dummy_N.mp4 files
                    DeleteFilesFromFolder(Path, "dummy*.mp4", track, track_stringS(5), True, False)
                    If track Then
                        CurPrg(1) = 1
                        CurPrg(0) = CurPrg(1)
                        UpdateProg_Cur(CurPrg, track_stringE(5), False, True)
                    End If


                    'Delete final.mp4 if exists
                    If track Then
                        CurPrg(0) = 0
                        CurPrg(1) = 1
                        UpdateProg_Cur(CurPrg, track_stringS(6), True, False)
                    End If
                    File = Path & strPATHSEP & "final.mp4"
                    If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                    If track Then
                        CurPrg(0) = CurPrg(1)
                        UpdateProg_Cur(CurPrg, track_stringE(6), False, True)
                    End If
                End If
            End If


            If justBMPs = False Then
                If track Then
                    CurPrg(0) = 0
                    CurPrg(1) = 3
                    UpdateProg_Cur(CurPrg, track_stringS(7), True, False)
                End If

                'Delete file.txt if exists, a list of files used for -i in ffmpeg.exe
                File = Path & strPATHSEP & "File.txt"
                If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                If track Then
                    CurPrg(0) += 1
                    UpdateProg_Cur(CurPrg)
                End If

                'Also delete Irfanview JPG INI files
                File = Path & strPATHSEP & "i_view32.ini"
                If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                If track Then
                    CurPrg(0) += 1
                    UpdateProg_Cur(CurPrg)
                End If

                File = Path & strPATHSEP & "i_view32_temp.ini"
                If System.IO.File.Exists(File) Then My.Computer.FileSystem.DeleteFile(File)
                If track Then
                    CurPrg(0) += 1
                    UpdateProg_Cur(CurPrg, track_stringE(7), False, True)
                End If

                'Cleanup FFPLay playback
                CleanUp_Playback()
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in CleanUp!", True)
        End Try
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
        Try
            Dim m As Byte = TryParseErr_Byte(txtVM_M.Text)          'The multiplicity in the THP
            Dim cnt As UShort = TryParseErr_UShort(txtTE_F.Text)    'The amount of frames to limit each subvideo
            Dim max As UShort = cnt * m                             'Max amount of frames in new THP
            Dim digs As Byte = max.ToString().Length                'Get the amount of digits in the max value
            txtTE_D.Text = digs.ToString()                          'Update the digits text
        Catch ex As Exception
            'Log_MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in UpdateTEDigs()!", true)
        End Try
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

    ''' <summary>
    ''' Handles the checkbox array depiction of naming conventions for THP encoding, and radio buttons for cells for THP Decoding
    ''' Also updates multiplicity NUD for ripping (time)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleArrState()
        'Encoding array
        Dim Enc_Boxes(6, 2) As System.Windows.Forms.CheckBox    'Array of 6x2 check boxes for THP encoding
        Dim Enc_Dum As System.Windows.Forms.CheckBox            'Dummy check box (for padding)
        Dim Enc_Wav As System.Windows.Forms.CheckBox            'Wav check box (for audio wav file)

        'Decoding array
        Dim Dec_Rads(6, 2) As System.Windows.Forms.RadioButton  'Array of 6x2 radio buttons for THP decoding
        Dim Dec_Dum As System.Windows.Forms.RadioButton         'Dummy radio button (for padding)
        Dim Dec_All As System.Windows.Forms.RadioButton         'Radio button for all

        'Init the encoding array. In A1_N MS Excel notation, Alpha=row, Number=Col
        Enc_Boxes(1, 1) = chkTE_A1
        Enc_Boxes(2, 1) = chkTE_A2
        Enc_Boxes(3, 1) = chkTE_A3
        Enc_Boxes(4, 1) = chkTE_A4
        Enc_Boxes(5, 1) = chkTE_A5
        Enc_Boxes(6, 1) = chkTE_A6
        Enc_Boxes(1, 2) = chkTE_B1
        Enc_Boxes(2, 2) = chkTE_B2
        Enc_Boxes(3, 2) = chkTE_B3
        Enc_Boxes(4, 2) = chkTE_B4
        Enc_Boxes(5, 2) = chkTE_B5
        Enc_Boxes(6, 2) = chkTE_B6
        'Wav and dummy boxes
        Enc_Dum = chkTE_Dum
        Enc_Wav = chkTE_wav

        'Init the decoding array. In A1_N MS Excel notation, Alpha=row, Number=Col
        Dec_Rads(1, 1) = radTD_A1
        Dec_Rads(2, 1) = radTD_A2
        Dec_Rads(3, 1) = radTD_A3
        Dec_Rads(4, 1) = radTD_A4
        Dec_Rads(5, 1) = radTD_A5
        Dec_Rads(6, 1) = radTD_A6
        Dec_Rads(1, 2) = radTD_B1
        Dec_Rads(2, 2) = radTD_B2
        Dec_Rads(3, 2) = radTD_B3
        Dec_Rads(4, 2) = radTD_B4
        Dec_Rads(5, 2) = radTD_B5
        Dec_Rads(6, 2) = radTD_B6
        'Dummy and all chks
        Dec_Dum = radTD_Dum
        Dec_All = radTD_All

        'Generic iterators
        Dim i As Byte = 0
        Dim j As Byte = 0

        'Handle Encoding stuff
        Try
            'Update the checked and enabled states of the array based on the video data 
            'Amount of rows in video, columns, and multiplicity            
            Dim r As Byte = TryParseErr_Byte(txtArr_R.Text)
            Dim c As Byte = TryParseErr_Byte(txtArr_C.Text)
            Dim m As Byte = TryParseErr_Byte(txtVM_M.Text)
            Dim state As Boolean = False                    'Generic bool

            For i = 1 To 6 Step 1                           'Iterate through all rows (1-6)
                For j = 1 To 2 Step 1                       'Iterate through all cols (1-2)
                    If r = 0 And c = 0 Then
                        'If a dummy entry in combo box was selected, then r & c of array will be 0. Set all chks/boxes to unchecked/disabled
                        state = False
                    Else
                        If i <= r And j <= c Then
                            'If i and j iterators (row/col) are within
                            'the amount of rows and col for this video, 
                            'then cell is used. Check & enable
                            state = True
                        Else
                            'Otherwise unused, uncheck and disable
                            state = False
                        End If
                    End If

                    'Update the checked/enabled states as appropriately for the Enc boxes
                    Enc_Boxes(i, j).Checked = state
                    Enc_Boxes(i, j).Enabled = state

                    'Always set the radio buttons as unchecked
                    Dec_Rads(i, j).Checked = False
                    Dec_Rads(i, j).Enabled = state
                Next j
            Next i
            '"All radio" button is always enabled and checked by default
            Dec_All.Checked = True
            Dec_All.Enabled = True


            'Handle dummy checkbox/chk states
            state = THPHasPad()
            Enc_Dum.Checked = state
            Enc_Dum.Enabled = state
            Dec_Dum.Checked = False 'Dummy is never set by default
            Dec_Dum.Enabled = state

            'Handle wav checkbox states
            state = BoolStrToBool(txtA_A.Text)    'If "True" then true
            'Update the wav checkbox states
            Enc_Wav.Checked = state
            Enc_Wav.Enabled = state

            'Handle the multiplicity box (the m values), and mult for time ripping
            'If only m=1, then "_1", else "_1 to\n_M"
            If m = 1 Then
                txtTE_M.Text = "_1"

                'Set range to 0 to 1. 0=all frames (no suffixes), 1=only frame (_1 suffix, for naming convention nitpickery)
                nudTD_M.Minimum = 0
                nudTD_M.Maximum = 1
            Else
                'Update the text multi box
                txtTE_M.Text = "_1 to" & strNL & "_" & m.ToString()

                'Update the time rip NUD
                nudTD_M.Minimum = 0
                nudTD_M.Maximum = m
            End If
            txtTE_F.Text = txtVF_S.Text
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in HandleArrState()!", True)
        End Try
    End Sub



    '===========================
    'DOS file path/stream functions

    ''' <summary>
    ''' Given a full file path, returns the directory
    ''' </summary>
    ''' <param name="strPath">Full file path</param>
    ''' <returns>File path</returns>
    ''' <remarks></remarks>
    Public Function FileDir(ByVal strPath As String) As String
        Dim strOut As String = ""       'Output
        Try
            Dim strFile As String = ""      'The file itself (ie, File.ext)        
            strFile = FileAndExt(strPath)   'Get the file+extension

            'From the full file path, replace the file+ext with nothing, to get file directory; return
            strOut = Replace(strPath, strFile, "")
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in FileDir()!", True)
        End Try
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
        Dim outp As String = ""

        Try
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
                shtPos(bytItems) = InStr(shtStart, strPath, strPATHSEP) 'Find the next strPATHSEP character, record its position in array
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
            outp = Mid(strPath, (shtPos(bytItems - 1)) + 1, shtFileLen) 'Extract the file+ext
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error in FileAndExt", True)
        End Try
        Return outp
    End Function

    ''' <summary>
    ''' Writes File.txt at Path with list of Files (rel dirs).
    ''' </summary>
    ''' <param name="Path">Directory</param>
    ''' <param name="Files">Array of filenames</param>
    ''' <remarks>Used for -i param for ffmpeg.exe</remarks>
    Private Sub WriteTxtFile(ByVal Path As String, ByRef Files() As String)
        Dim xFileData As StreamWriter = Nothing                 'Streamwriter object to write File.txt
        Dim TextFile As String = Path & strPATHSEP & "File.txt" 'The filepath to write
        Try
            'If the textfile exists, remove it for clean slate
            If My.Computer.FileSystem.FileExists(TextFile) Then My.Computer.FileSystem.DeleteFile(TextFile)
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
            KillStream(xFileData, False, TextFile)
        Catch ex As Exception
            KillStream(xFileData, False, TextFile)
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "File I/O error in WriteTxtFile!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Deletes files from a folder based on a DOS search spec, with optional current progress tracking
    ''' </summary>
    ''' <param name="Folder">Folder to search</param>
    ''' <param name="type">Search spec</param>
    ''' <param name="track">Current progres tracking?</param>
    ''' <param name="startText">Start message</param>
    ''' <param name="_Set">Set start message?</param>
    ''' <param name="_Wait">Wait?</param>
    ''' <remarks>
    ''' Like del cmd. For stuff like del *.pdf.
    ''' https://stackoverflow.com/questions/25429791/how-do-i-delete-all-files-of-a-particular-type-from-a-folder
    ''' </remarks>
    Private Sub DeleteFilesFromFolder(ByVal Folder As String, ByVal type As String, Optional ByVal track As Boolean = False, Optional ByVal startText As String = "", Optional ByVal _Set As Boolean = False, Optional ByVal _Wait As Boolean = False)
        'array(2), 0=current value, 1=max progress
        Dim CurPrg(2) As Single
        Try
            'If folder exists
            If Directory.Exists(Folder) Then
                'Iterate through all files that match spec, delete them
                Dim i As Integer = 0                                        'Generic iterator
                Dim Files() As String = Directory.GetFiles(Folder, type)    'All files
                Dim max As Integer = Files.Count                            'Max amt of files

                'If tracking, set current progress between 0-max file count, and current progress to 0, display any text with set/wait flags
                CurPrg(0) = 0
                CurPrg(1) = max
                If track Then UpdateProg_Cur(CurPrg, startText, _Set, _Wait)
                For Each _file As String In Files
                    File.Delete(_file)
                    i += 1

                    'If tracking, increment the current progress
                    If track Then
                        CurPrg(0) = i
                        UpdateProg_Cur(CurPrg)
                    End If
                Next _file
            Else
                Throw New System.Exception("Could not find directory " & Folder & "!")
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "File I/O error in DeleteFilesFromFolder!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Like DeleteFIlesFromFolder, but for sequentially named files, deletes files past a limit
    ''' </summary>
    ''' <param name="Folder">Folder to search</param>
    ''' <param name="type">Search spec</param>
    ''' <param name="limit">Index limit</param>
    ''' <remarks></remarks>
    Private Sub DeleteExtraFilesFromFolder(ByVal Folder As String, ByVal type As String, ByVal limit As UShort)
        'If folder exists
        'Current progress tracking
        Dim CurPrg(2) As Single

        Try
            If Directory.Exists(Folder) Then
                Dim i As Integer = 1                                             'Generic counter                
                Dim Files() As String = Directory.GetFiles(Folder, type)         'Array of files
                Dim max As Integer = Files.Count                                 'Count of files

                'Track between 0 and max file count, current = 0
                CurPrg(0) = 0
                CurPrg(1) = max
                'Set start text without wait
                UpdateProg_Cur(CurPrg, "Deleting extra JPG files; please wait...", True, False)
                For Each _file As String In Files
                    If i > limit Then File.Delete(_file) '                       If iterator is above limit (extra file), delete it                    
                    i += 1                                                      'Increment

                    'Update progress
                    CurPrg(0) = i
                    UpdateProg_Cur(CurPrg)
                Next _file
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "File I/O error in DeleteExtraFilesFromFolder!", True)
        End Try
    End Sub

    ''' <summary>
    ''' Counts amount of files in a folder meeting a search criteria
    ''' </summary>
    ''' <param name="Folder">Folder to search</param>
    ''' <param name="type">Search spec</param>
    ''' <returns></returns>
    ''' <remarks>Similar to inputs in DeleteFilesFromFolder</remarks>
    Private Function CountFilesFromFolder(ByVal Folder As String, ByVal type As String) As UShort
        Dim cnt As UShort = 0                                               'Amount of files
        Try
            'If folder exists
            If Directory.Exists(Folder) Then
                Dim Files() As String = Directory.GetFiles(Folder, type)    'Get array of files meeting spec
                cnt = Files.Count()                                         'Get count of files meeting spec
            End If
        Catch ex As Exception
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "File I/O error in CountFilesFromFolder!", True)
        End Try
        Return cnt
    End Function


    ''' <summary>
    ''' DLL Import func to get an (extra) special folder path
    ''' Accessing DL folder: https://www.codeproject.com/Questions/393318/Accessing-the-User-Downloads-Folder-in-VB-NET
    ''' Syntax sugar from [DLLImports("foo")] to Declare Func Foo Lib "Foobar.dll"(stuff): https://stackoverflow.com/a/3081728
    ''' </summary>
    ''' <param name="rfid">SHblabla param1</param>
    ''' <param name="dwFlags">SHblabla param2</param>
    ''' <param name="hToken">SHblabla param3</param>
    ''' <param name="pszPath">SHblabla param4</param>
    ''' <returns>Stuff</returns>
    ''' <remarks>Orig DLL func: https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shgetknownfolderpath </remarks>
    Private Declare Function SHGetKnownFolderPath Lib "Shell32.dll" (<MarshalAs(UnmanagedType.LPStruct)> ByVal rfid As Guid, ByVal dwFlags As UInt32, ByVal hToken As IntPtr, ByRef pszPath As IntPtr) As Int32

    ''' <summary>
    ''' Function to get extra special DL folder path
    ''' Accessing DL folder: https://www.codeproject.com/Questions/393318/Accessing-the-User-Downloads-Folder-in-VB-NET
    ''' </summary>
    ''' <returns>Folder path or "" if DLL failure (legacy versions of Windows)</returns>
    ''' <remarks></remarks>
    Public Function GetDownloadsFolder() As String
        Dim sResult As String = ""
        Dim ppszPath As IntPtr
        Dim gGuid As Guid = New Guid("{374DE290-123F-4565-9164-39C4925E467B}")
        Try
            If SHGetKnownFolderPath(gGuid, 0, 0, ppszPath) = 0 Then
                sResult = Marshal.PtrToStringUni(ppszPath)
                Marshal.FreeCoTaskMem(ppszPath)
            End If
        Catch ex As Exception
            sResult = ""
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Exclamation, "DLL func call failure within GetDownloadsFolder()! Using root directory...", True)
        End Try
        Return sResult
    End Function

    ''' <summary>
    ''' Macro function to kill a lingering StreamReader (if something, close, dispose, set to nothing).
    ''' Also can delete file being read afterwards
    ''' </summary>
    ''' <param name="xFileData">StreamReaderto handle</param>
    ''' <param name="Delete">Delete file?</param>
    ''' <param name="_file">File to delete</param>
    ''' <remarks></remarks>
    Private Sub KillStream(ByVal xFileData As StreamReader, Optional ByVal Delete As Boolean = False, Optional ByVal _file As String = "")
        If IsNothing(xFileData) = False Then
            xFileData.Close()
            xFileData.Dispose()
            xFileData = Nothing

            'If delete flag, AND if file exists, delete it
            If Delete = True Then
                If My.Computer.FileSystem.FileExists(_file) Then My.Computer.FileSystem.DeleteFile(_file)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Macro function to kill a lingering StreamWriter (if something, close, dispose, set to nothing).
    ''' Also can delete file being written to
    ''' </summary>
    ''' <param name="xFileData">StreamWriter to handle</param>
    ''' <param name="Delete">Delete file?</param>
    ''' <param name="_file">File to delete</param>
    ''' <remarks></remarks>
    Private Sub KillStream(ByVal xFileData As StreamWriter, ByVal Delete As Boolean, ByVal _file As String)
        If IsNothing(xFileData) = False Then
            xFileData.Close()
            xFileData.Dispose()
            xFileData = Nothing

            'If delete flag, AND if file exists, delete it
            If Delete = True Then
                If My.Computer.FileSystem.FileExists(_file) Then My.Computer.FileSystem.DeleteFile(_file)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Copies i_view32.ini APPDATA file pointed to from INI_Folder var within i_view32.ini (at i_view32.exe folder) to THP working directory, hacks JPG "Save Progressive" value to 0 and changes "Save Quality" setting.
    ''' This file will be used later for JPG conversion, to ensure NO Progressive JPG (fixes THPConv encoding bugs)
    ''' 
    ''' This is a bugfix for Bug #5.
    ''' Bug: Using a spaceless symlink to iview creates non-progressive JPG files which work with THPConv
    ''' (due to inability to find i_view32.ini file which usually has JPG "Save Progressive" option as true,
    ''' while using a real path within Program Files breaks (because it finds the INI files and enables option)
    ''' </summary>
    ''' <param name="workDir">THP working directory</param>
    ''' <returns>Sucessful INI hack?</returns>
    Private Function HackINIFile(ByVal workDir As String)
        Dim success As Boolean = False
        Const INI As String = "i_view32.ini"                            'File name for Irfanview INI files
        Const INITEMP As String = "i_view32_temp.ini"                   'File name for temp Irfanview INI file
        Dim iViewINI2 As String = workDir & strPATHSEP & INI            '2nd options INI file (at %Appdata%/whatever)
        Dim iViewINI2Temp As String = workDir & strPATHSEP & INITEMP    'Same as iViewINI2, but temporary INI that will be hacked

        Dim xrINIData As StreamReader = Nothing                         'Streamreader object for reading the i_view32.ini files
        Dim xwINIData As StreamWriter = Nothing                         'Streamwriter object for hacked INI file
        Try
            Dim iView As String = txtiView.Text                         'irfanview exe path
            Dim iViewPath As String = Path.GetDirectoryName(iView)      'path of irfanview exe
            Dim iViewINI As String                                      '1st INI file (at exe dir)

            iViewPath &= (strPATHSEP & INI)                             'Get the 1st INI file (exe folder\INI const)
            Dim strEntry As String                                      'Line from INI file
            Dim blnFound As Boolean = False                             'String match found?
            Dim chrInd As Integer                                       'Index of a char in line

            xrINIData = File.OpenText(iViewPath)                        'Open the INI file

            'Read each line, until find "[Others]" marker
            blnFound = False
            While xrINIData.EndOfStream() <> True
                strEntry = xrINIData.ReadLine()                         'Read a line from the file

                'If marker found, flag as found and exit loop
                If strEntry.Contains("[Others]") Then
                    blnFound = True
                    Exit While
                End If
            End While

            'If marker was not found, throw custom error
            If blnFound = False Then Throw New System.Exception("Failed to find '[Others]' marker in i_view32.ini file within Irfanview exe folder")

            'Read next line from the file
            strEntry = xrINIData.ReadLine()
            'If next line is not INI_Folder variable, then throw custom error
            If strEntry.Contains("INI_Folder") = False Then Throw New System.Exception("Failed to find 'INI_Folder' variable in i_view32.ini file within Irfanview exe folder")

            chrInd = strEntry.IndexOf("=")                              'Find index of '=' char in variable line
            chrInd += 2                                                 'Increment by 2. !@ This may be wrong?
            iViewINI = Mid(strEntry, chrInd)                            'Get substring of everything after = sign
            iViewINI = LTrim(iViewINI)                                  'Left trim it
            iViewINI &= (strPATHSEP & INI)                              'Add "\INI" to EOP
            iViewINI = Environment.ExpandEnvironmentVariables(iViewINI) 'Expand any environ vars within (default INI usually has %APPDATA% envvar)

            'Close INI file, copy over INI file, then load it for reading/writing to new temp file
            KillStream(xrINIData)
            My.Computer.FileSystem.CopyFile(iViewINI, iViewINI2, True)
            xrINIData = File.OpenText(iViewINI2)                                        'Open the INI2 file
            xwINIData = My.Computer.FileSystem.OpenTextFileWriter(iViewINI2Temp, False) 'Open a new INI2Temp file for writing

            'Read each line, write each to other file, read until find "[JPEG]" marker
            blnFound = False
            While xrINIData.EndOfStream() <> True
                strEntry = xrINIData.ReadLine()                    'Read a line from the file
                xwINIData.WriteLine(strEntry)

                'If marker found, flag as found and exit loop
                If strEntry.Contains("[JPEG]") Then
                    blnFound = True
                    Exit While
                End If
            End While

            'If marker was not found, throw custom error
            If blnFound = False Then Throw New System.Exception("Failed to find '[JPEG]' marker in " & strQUOT & iViewINI2 & strQUOT & " file.")

            'Keep reading lines and writing to other file until EOF
            'If found "Save Progressive=BIT" line, change BIT to 0 (this is the INI hack)
            'If Found "Save Quality" line, replace value with JPG quality (the other hack)
            blnFound = False
            While xrINIData.EndOfStream() <> True
                strEntry = xrINIData.ReadLine()                    'Read a line from the file

                'If Save Progressive var found, replace 1 to 0 bit
                If strEntry.Contains("Save Progressive") Then
                    blnFound = (blnFound Or True)
                    strEntry = strEntry.Replace("1", "0")
                End If

                'If Save Quality var found, replace with new quality
                If strEntry.Contains("Save Quality") Then
                    blnFound = (blnFound Or True)
                    strEntry = "Save Quality=" & nudTE_jpgq.Value.ToString()
                End If

                'Write line
                xwINIData.WriteLine(strEntry)
            End While

            'If markers were not found, throw error
            If blnFound = False Then Throw New System.Exception("Failed to find 'Save Progressive' or 'Save Quality' settings under '[JPEG]' marker in " & strQUOT & iViewINI2 & strQUOT & " file. Irfanview INI hack was unsuccessful, and thus ripped JPG files used for THP Encoding may be created as wrong progressive JPG types or wrong JPG quality applied. These errors shall cause THP encoding to fail!")

            'Close all files, delete iViewINI2, rename iViewINI2TEMP to iViewINI2, show success
            KillStream(xrINIData)
            KillStream(xwINIData, False, iViewINI2Temp)
            My.Computer.FileSystem.DeleteFile(iViewINI2)
            My.Computer.FileSystem.RenameFile(iViewINI2Temp, Path.GetFileName(iViewINI2))
            success = True
        Catch ex As Exception
            'Close any lingering streams
            KillStream(xrINIData)
            KillStream(xwINIData, False, iViewINI2Temp)
            Log_MsgBox(ex, ex.Message, MsgBoxStyle.Critical, "Error finding, copying, and/or hacking Irfanview INI file!", True)
            success = False
        End Try

        Return success
    End Function

    '================
    'Cast helper funcs

    ''' <summary>
    ''' Converts standard bits (0/1) to bool
    ''' </summary>
    ''' <param name="inp">Bit input</param>
    ''' <returns>Bool</returns>
    ''' <remarks></remarks>
    Private Function BitToBool(ByVal inp As Byte) As Boolean
        Dim outp As Boolean = False
        If inp = 1 Then outp = True
        Return outp
    End Function

    ''' <summary>
    ''' Converts bool to bit
    ''' </summary>
    ''' <param name="value">Bool to convert</param>
    ''' <param name="_false">False bit value</param>
    ''' <param name="_true">True bit value</param>
    ''' <returns>Bit value</returns>
    ''' <remarks></remarks>
    Private Function BoolToBit(ByVal value As Boolean, Optional ByVal _false As Byte = 0, Optional ByVal _true As Byte = 1) As Byte
        Dim result As Byte = _false
        If value = True Then
            result = _true
        End If
        Return result
    End Function

    ''' <summary>
    ''' Converts String with "True" or "False" into appropriate boolean value
    ''' </summary>
    ''' <param name="inp">Boolean String</param>
    ''' <returns>Boolean value</returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Try parsing a string as byte; if fail, throw error
    ''' </summary>
    ''' <param name="inp">String</param>
    ''' <returns>Byte</returns>
    ''' <remarks></remarks>
    Private Function TryParseErr_Byte(ByVal inp As String) As Byte
        Dim outp As Byte = Byte.MaxValue
        Dim result As Boolean = Byte.TryParse(inp, outp)
        If result = False Then
            Throw New System.Exception("Error parsing string into Byte")
        End If
        Return outp
    End Function
    ''' <summary>
    ''' Try parsing a string as UShort; if fail, throw error
    ''' </summary>
    ''' <param name="inp">String</param>
    ''' <returns>Byte</returns>
    ''' <remarks></remarks>
    Private Function TryParseErr_UShort(ByVal inp As String) As UShort
        Dim outp As UShort = UShort.MaxValue
        Dim result As Boolean = UShort.TryParse(inp, outp)
        If result = False Then
            Throw New System.Exception("Error parsing string into UShort")
        End If
        Return outp
    End Function

    ''' <summary>
    ''' Try parsing a string as Single; if fail, throw error
    ''' </summary>
    ''' <param name="inp">String</param>
    ''' <returns>Byte</returns>
    ''' <remarks></remarks>
    Private Function TryParseErr_Single(ByVal inp As String) As Single
        Dim outp As Single = Single.PositiveInfinity

        'Bugfix for culture stuff (2nd bug of Issue #11)
        'https://stackoverflow.com/a/12165465

        'TryParse single on a Float using InvariantCulture
        Dim style As System.Globalization.NumberStyles = System.Globalization.NumberStyles.Float
        Dim result As Boolean = Single.TryParse(inp, style, culture, outp)
        If result = False Then Throw New System.Exception("Error parsing string into Single")
        Return outp
    End Function

    ''' <summary>
    ''' Given a single datatype value, returns its string form as a particular format (culture invariant)
    ''' Function for various related bugfixes for issue #16
    ''' https://github.com/Tamk1s/Thwimp/issues/16
    ''' </summary>
    ''' <param name="inp">Single value</param>
    ''' <param name="type">Single_ToString_Types conversion type; see enum definition</param>
    ''' <returns>Properly formatted single value (culture invariant string)</returns>
    ''' <remarks></remarks>
    Private Function Single_ToString(ByVal inp As Single, ByVal type As Single_ToString_Types) As String
        ''Bugfix for single ToString culture invariance:
        'https://stackoverflow.com/questions/164926/how-do-i-display-a-decimal-value-to-2-decimal-places#comment96097523_1907832

        Dim outp As String = ""                                                                                 'Output string

        'Format string to use for Single_ToString_Types type
        Dim formatString As String = ""
        Select Case type
            Case Single_ToString_Types.PERCENT
                'If percent, format as 2-digit percent (nn.dd%)
                formatString = "P2"
            Case Single_ToString_Types._SINGLE
                'If single, format as 2-digit single (nn.dd)
                formatString = "F2"
            Case Single_ToString_Types.FIXED
                'If fixed, format as 9-digit format for FFMPEG Audio conversion round-tripping stuff
                formatString = "G9"
        End Select

        'Convert single to appropriate format (culture-invariant); return
        outp = inp.ToString(formatString, culture)
        Return outp
    End Function
End Class