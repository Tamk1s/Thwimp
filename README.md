# Thwimp v1.2

![Thwimp logo](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/Thwimp/Resources/thwimp.png)

By [Tamkis/EagleSoft Ltd.](http://www.eaglesoftltd.com)

[Thwimp Project Page](http://www.eaglesoftltd.com/retro/Nintendo-Wii/thwimp)

[MKWiiki article](http://wiki.tockdom.com/wiki/Thwimp)

[Video playlist](https://www.youtube.com/playlist?list=PL3N-ZrZe1CWKxEOHgq64HNUm0rHZEQx3X)

## Table of contents

1. What is it?
2. Why was it created?
3. How to use it (GUI)
4. How to use it (CLI)
5. Customization
6. Change Log
7. Known Issues/Todo
8. Credits


## 1. What is it?

Thwimp is a hybrid GUI/CLI **Windows** utility which allows users easily to view, to rip, and to encode [Nintendo THP](http://wiki.tockdom.com/wiki/THP_(File_Format)) video files for Mario Kart Wii (and for other GCN/Wii games, to an extent). Written in Visual Basic (from Visual Studio 2010 IDE), the Thwimp application calls some FOSS and other command line tools **(not included)** "from arms length" via the Command Prompt to perform its tasks.

**Specifically, Thwimp uses:**

- **FFMPEG**
	- Video processing
- **FFPlay**
	- THP playback
- **Irfanview**
	- Image conversion
	- BMP to JPG conversion
		- JPG Quality setting applied
		- Non-progressive JPG files
- **THPConv**
	- **Definitely not included!**
	- Encoding JPG frames + WAV file into THP videos

![Utility logos](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/Utils.png)

Thwimp can show the hard-coded information about how each THP file is formatted in Mario Kart Wii. Thwimp can also view MKWii's THP files, as well as convert them to everyday video files. It can crop the THP video files (both physically and in terms of time length), and then convert them to MP4 (H.264 codec) video files, to WAV files (for videos with audio), and padding to BMP files.

The THP video files in Mario Kart Wii tend to be an array of multiple subvideo cells inside, with each cell playing back several other videos in multiple time chunks ("multiplicities"). As required by the file format specification, THP video files must have their dimensions be a multiple of 16px. Often times the size of the subvideo array will **not** be enough to meet the specification, so padding is added to the bottom of the THP video array. This padding is not only used to meet the specification, but sometimes used for control information. For example, some of the videos shown during menus will have a white rectangle move at integral positions at each multiplicity within this padding space. This white rectangle controls which row in a menu is highlighted during THP playback, for synchronization purposes.

Thwimp can intelligently handle audio, subvideo array, multiplicity, and padding/control information when encoding THP files to replace ones in-game. It does this by accepting appropriately named, input mp4 video files for each subvideo cell in the array, and for each multiplicity. Wher applicable, it will also accept a WAV audio file, and BMP image frames for each multiplicity for creating an appropriate padding area as needed. After reading the input files, Thwimp will intelligently process and composite all of the files together appropriately, in order to create a high-quality, properly formatted THP video replacement file!

## 2. Why was it created?

It was created in order to help MKWii modders to create high-quality, original THP video files for their custom track distributions and mods, using multiplicity videos for each cell in the THP's array of subvideos. Specifically, it was created for the creation of more complex THP files, such as **"battle\battle_cup_select.thp"** and for **"course\cup_select.thp"**.

### Examples of complex THPs used in MKWii, which Thwimp helps create

- **"battle\battle_cup_select.thp"**
	- Shows CAD animated walkthroughs of each track in both the Wii and Retro battle cups    
    - When a battle cup is highlighted (but not selected), the subvideos for each track are shown for the current cup
    - Video layout
    	- Array of 2x1 subvideo cells
    	- Entire THP video lasts 600 frames long
    	- Top row = Wii battle courses
    	- Bot row = Retro battle courses
    	- Each cup has 5 courses (5 multiplicities per subvideo cell)
    	- Every 120 frames, the subvideo cells show the next track in the cup (goes to the next multiplicity)
    	- Video has some padding at bottom
    		- Padding has a white rectangle which moves to a different integral position at every multiplicity
    		- Position of white rectangle determines which row (which track) is highlighted in the menus, for synchronization

![battle_cup_select.thp](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/battle_cup_select.png)
- **"course\cup_select.thp"**
    - Shows the CAD animated walkthrough for each individual track in the game (for racing modes).
    - Game has 8 cups, each with 4 tracks (32 tracks)
    - Track menu layout
    	- Array of 2x4 cups
    	- Each cup has 4 tracks
    - Video layout
    	- Array of 4x2 subvideos cells (one for each of the 8 cups)
    	- Each cup has 4 tracks (4 multiplicities for the subvideo cells)
    	- Whole THP video lasts 480 frames long
    	- Left column = Tracks from 1st row of cups in menu layout
    	- Right column = Tracks from 2nd row of cups in menu layout
    	- Every 120 frames, the subvideos show the next track in each cup (goes to the next multiplicity)
    	- Video has some padding at bottom
    		- Padding has a white rectangle which moves to a different integral position at every multiplicity
    		- Position of white rectangle determines which row (which track) is highlighted in the menus when a cup is selected, for synchronization

![cup_select.thp](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/cup_select.png)

After doing what this program does for my projects (manually and painfully via batch files), I developed Thwimp in order to automate the THP creation process for complex videos like the ones above. This application was used to create the high-quality, custom THPs in my own [Hover Pack project](http://wiki.tockdom.com/wiki/Hover_Pack).


## 3. How to use it (GUI)

### Prerequisites:

As mentioned earlier, Thwimp uses some FOSS and other command line tools for processing **(not included)**. You will need to download/install the following utilities **before** using Thwimp.

**Utilities:**

 - [FFMPEG (with FFPlay)](https://ffmpeg.zeranoe.com/builds/)
 - [Irfanview (32-bit)](https://www.irfanview.com/main_download_engl.htm)
 - [THPConv or THPGUI](http://www.google.com)

![Utility logos](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/Utils.png)
### Options tab:

![Options](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/Options.png)

#### Setting up paths:

Before doing **anything** in the utility, goto the "Options" tab of the application and set your paths. Do this by clicking the "Browse" buttons beside each item and navigating to the appropriate places with the file dialog boxes that appear.

**Paths to set:**
1. THP Root
	- Path containing the original Nintendo THP video files for viewing/ripping
	- On a legally obtained, self-dumped Mario Kart Wii ISO from a copy of the disc that you own **(don't ask me where to get one)**, this should be the ["root\thp"](http://wiki.tockdom.com/wiki/Filesystem/thp) folder extracted from  Partition 1 (0-based ID) of the DVD/ISO.	
	- File extraction utilities
		- [Dolphin's](https://dolphin-emu.org/) [ISO Filesystem browsing feature](https://wiki.vg-resource.com/Dolphin)
		- [Wiimm's ISO Tools](http://wiki.tockdom.com/wiki/Wiimms_ISO_Tools)
		- Extract files using [WiiXplorer](https://wiibrew.org/wiki/WiiXplorer) from an original MKWii DVD to SD Card (real Wii hardware with Homebrew Channel)

2. FFMPEG Exe Root
	- Points to the folder containing the executables of FFMPEG
    	- Usually located at "[FFMPEG root]\bin", should contain
    		- ffmpeg.exe
        	- ffplay.exe

3. FFPlay WorkDir
	- Directory to place some temporary, intermediary files when using the THP Viewer
	- Especially used when time cropping a THP video with an audio stream

4. IrfanView
	- Points to the IrfanView executable (i_view32.exe)
	- Must be the **32-bit version**

5. THPConv Exe
	- Points to THPConv.exe
	- Command line tool used for encoding JPG frames (and optionally a WAV file) into a Nintendo THP video
	- **Do not ask me where to obtain this!**

6. Data File Dir
	- Points to a folder containing the Thwimp data fileset for the game you wish to handle THPs from
	- Default MKWii data fileset should be within the Thwimp exectuable's folder, others elsewhere
	- Metadata information about the loaded fileset will appear in the "Data Fileset Info" groupbox
		- Read the **Data Fileset Info subsection** for more information
	- See **Section 5 Customization** for more information	
	
   After setting all of your paths, the elements in the "THP" tab will be available! You can also quickly load your options from an INI file (after saving one). See **Load/Save Settings" subsection** for more information.

#### Option flags:

  Underneath the paths in the Options tab are various checkboxes. These are various option flags which tweak how Thwimp will operate.
  
**Option flags:**

1. Less MsgBox
	- During THP encoding, a few informational message boxes will appear during encoding
	- Checking this box will suppress them, allowing for encoding without interruption

2. Full Logs
	- If checked, will log **everything** (including stdout from FOSS co-utilities) into the Logger form in the THP tab
	- Use this for debugging issues/sending detailed error reports that occur when the THP Viewer, Ripper, or Encoder fail to work from FFMPEG/FFPlay commands that are called from Thwimp
	- **Required to be checked when operating from CLI mode!**

3. Audio
	- If checked, will play audio clips on certain events
		- Super Mario Bros 1. flagpole victory
			- THP Ripper success
			- THP Encoder success
			- Cmdline help box
		- EagleSoft Ltd branding
			- About box
		- Super Mario Bros 1. death
			- Errors, failure
	- Must be checked to enabled Elevator Music feature

4. Elevator Music
	- If Audio is checked and this, then will playback some "Elevator Music" during THP encoding
	- Plays back song.wav (located at Thwimp executable directory) looped
	- Use this feature as
		- An audial notifier that Encoder is still running
		OR
		- Is done when it stops
		- Entertainment

#### Load/Save Settings:

  Within the option flags area are also two buttons: "Load Settings" and "Save Settings". Click "Save Settings" to save your current settings to an INI file, or "Load Settings" to restore settings from one. It is recommended to save your machine's default settings at the Thwimp executable folder, for easier and quicker usage of Thwimp. Use this feature to quickly load common options, rather than tweaking them every launch of the application. **CLI mode requires** the usage of a Thwimp INI file, so make sure to save your desired settings when using a particular data fileset to a specific INI file!
  
#### Data Fileset Info:

   Underneath the Options' paths, flags, and Load/Save Settings buttons is another group box titled "Data Fileset Info". This section lists metadata about the loaded data fileset (from FileSet.txt)
   
**FileSet metadata:**
- Game
	- Game this fileset is meant for
- Description
	- A quick description about this fileset
- Author
	- Who created this fileset
- Version
	- Version string of this fileset
- Date
	- Release date of this version

#### Help/Resources:

  At the bottom of the Options tab is a group box labelled "Help/Resources". As its name suggests, it has various buttons to  launch website links in your default web browser, and display data for resources about Thwimp.
  
**Buttons:**
- Webpage
	- Opens up the [EagleSoft Ltd. Thwimp webpage](http://www.eaglesoftltd.com/retro/Nintendo-Wii/thwimp)
- About
	- Displays information about the current build of this application.
	- ![About](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/About.png)
- MKWiiki article
	- Opens up the [Thwimp MKWiiki article](http://wiki.tockdom.com/wiki/Thwimp)
- Manual (Github)
	- Opens up [this readme document](https://github.com/Tamk1s/Thwimp/blob/master/README.md)
- Latest Release
	- Opens up the [Thwimp Github releases page](https://github.com/Tamk1s/Thwimp/releases)
	- Use this to check for newer releases
- Cmdline
	- Opens up another form, displaying the current CLI syntax
	- ![CLI.png](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/CLI.png)

### Using the THP tab:

After loading valid settings, the THP tab will be available. At the top of the THP tab will be a dropdown box. Use this to select which THP file from MKWii to view, to rip, or to encode a new THP for. The "THP File" label will be updated with the current selectedIndex ID of the dropdown box; use this ID value in CLI operations using this fileset. After selecting a valid THP file, the THP Viewer/Ripper and THP Encoder group boxes will be available for use.

#### THP Info:

The "THP Info" group box gives statistical information about each original, Nintendo THP video in Mario Kart Wii. You will need this information in order to create appropriately formatted input files for THP encoding!

![THP Info](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/THP.png)

**Video:**
 - **Total dims**
	- Width
		- Width of the entire THP video (in px)
	- Height
		- Height of the entire THP video (in px)

 - **Video array info**
	- **Array**
		- R
			- Amount of rows in the array of subvideo cells
		- C
			- Amount of columns in the array of subvideo cells
		- Subv
			- Amount of subvideo cells in the array (S = R * C)
		
	- **Subvideo Info**
		- Multi
			- Number of multiplicities for each subvideo cells
			- Example
				- Some videos may show several tracks within one subvideo cell 
				- "course\cup_select.thp" in MKWii
				- THP shows 4 tracks in each subvideo cell, so mult = 4 here
		- Tot. Subv
			- Total amount of subvideos cells in the THP video, taking into account multiplicities (T = R * C * Mult)
			- This value will indicate the amount of MP4 video files you will need when encoding
		
	- **Playback info**
		- **Subvid dims**
			- Subv (Width, Height)
				- Width and height of each subvideo cell within the array (in px)
			- Pad (Width, Height)
				- Width and height of the padding for the THP video (in px)
				- THP video total dims must be a multiple of 16px
				- Padding used for 2 purposes
					- Pad total video dims to nearest multiple of 16px
						- Requirement of THP format filespec
					- Control signal
						- See THP video's "Control" group box for usage information
					
		- **# of frames**
			- Subv (mult)
				- Amount of video frames for each multiplicity for each subvideo cell
			- Total
				- Total amount of video frames in the THP file (T = Subv_mult * mult)
			
		- **Control**
			- FPS
				- **F**rames **P**er **S**econd playback speed of the THP
				- Default values
					- 59.94 Hz **(NTSC)**
					- 50 Hz **(PAL)**
				
			- Ctrl?
				- Is padding used for control purposes?
				
			- Ctrl Desc
				- Information about how the control signal is formatted within the padding
				
**Audio:**
 - Audio?
	- Does THP have audio?
 - Stereo?
	- Is the audio stereo?
 - Freq (Hz)
	- Frequency of the audio file
	- Usually 32000 Hz for MKWii
		
**THP File Desc:**
 - Gives description about
	- How the THP video is formatted
	- THP file's usage/purpose in-game

#### Log:

At the bottom of the THP tab, is a group box labelled "Log". This consists of a textbox, an icon, and 2 buttons: "Cls" and "Save". The textbox will display log messages, warnings, and any errors that occur when running Thwimp. If "Full Log" option is set in the options, it will also log output from the FOSS applications called at-arms-length by Thwimp. This logger will also log data from the progress bars, as well as MsgBoxes (the icon will display the same one that the MsgBox had when it was displayed).

The "Cls" button will clear the logger, while the "Save" button will dump the logger's contents into a text file. Use this for better inspections of issues, or sending me error reports for debugging!

#### Progress bars:

To the right of the Logger group box are 2 sets of Progress bar data.

**Each progress bar set consists of:**
- Textbox
	- Displays messages
- Progress bar
	- The progress bar itself
- Percentage label
	- Displays the current progress as a percentage

The upper set is for total progress, while the lower set is used for current progress (think Installshield Wizard total/current progress bar sets from Windows 9X era). These progress bars are used by the THP Viewer, Ripper, and Encoder for showing progress of the current operation and about what it is currently doing.

#### THP Viewer/Ripper

The "THP Viewer/Ripper" group box allows one to view a THP video, or convert the THP to various applicable asset files (video, audio, video frames). This group box has the following settings/form elements:

![THP Viewer/Ripper](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/THP.png)

- "DirectSound?" checkbox
	- Use DirectSound as the SDL_SOUNDDRIVER value for audio playback?
	- Fixes [audio playback bug](https://forum.videohelp.com/threads/388189-ffplay-WASAPI-can-t-initialize-audio-client-error) in current version of FFMPEG for Windows
	- Should be checked by default
	
- "Play" button
	- Plays currently selected THP file.
	- Use "DirectSound" option for proper audio playback as needed
	
- "Rip" button
	- Converts the THP video to the appropriate file format(s)
			
- Crop Settings
	- Crops the converted THP video to a specific physical size and to a particular timeframe, starting at a point, and from a starting video frame to an ending video frame
	- Point coordinates are relative to top-left corner of video (origin (0,0))
	- **Fields**
		- **Position**
			- X-pos
				- x position to start crop (0-based, in px)
			- Y-pos
				- y position to start crop (0-based, in px)
		- **Size**
			- Width
				- Width to crop video (1-based, in px)
			- Height
				- Height to crop video (1-based, in px)
		- **Time**
			- Start
				- Starting frame value to begin ripping (1-based)
			- End
				- Ending frame value to begin ripping (1-based)
		
	- **Preset (Row/Column)**
		- **Cells**
			- Cells A1 to B6
				- Select a particular subvideo cell to crop to
				- Updates the crop setting fields appropriately
		- **Special Presets**
			- "All" radio button
				- Rip the entire array of subvideos into a single video file for a particular multiplicity
				- Select "All" special preset and Mult of 0 to rip the whole video as a single file
			- "Dum" radio button
				- Rips the dummy padding/control signal area of video (if any)
				- If multiplicity = 0, and start/end frames are set to the min/max within THP video, then the unique dummy frames will be ripped as BMP files, named to the proper naming convention for re-encoding with a replacement THP video
	- **Time/Info**
		- Mult
			- Multiplicity numeric up/down control
				- Changes the multiplicity to rip in the selected subvideo cell.
				- A multiplicity of 0 will rip all multiplicities
				- This updates the start/end time frame values accordingly
		- Multiplicity checkbox
			- Indicates if a "single" (unchecked) or "multiple" (checked, when the multiplicity=0) multiplicities will be ripped
		- Dum Frames
			- Indicates if unique dummy frames will be ripped as BMP files
			- Only enabled when the "Dum" radio button is selected

Tweak the settings as appropriately and use the "View" or "Rip" buttons to play a THP or convert it to appropriate assets, respectively. Pressing the "Rip" button will ask the user to select an output directory and base filename for the converted files; by default, the path is at the location of the original THP, and the filename is "[thp_filename]_suffix", where suffix is the appropriate one based on the A1_N naming notation for the input files used for encoding (see "THP Encoder section"). If the original THP video has an audio stream, it will automatically be ripped with the crop time setting option and named appropriately. Dummy/control frames will be ripped automatically and named appropriately if the THP video has padding/control frames, if the "Dum" radio button is selected, if the multiplicity is set to 0, and if the start/end frame values are set to the original video's min/max values.

**Files are ripped from THP video files in the following formats:**

- **Video**
	- MP4 (H.264 encoded) video file
	- Named as [thp_filename]_suffix.mp4
- **Audio**
	- WAV audio file
	- Named as [thp_filename].wav
- **Dummy/padding frames**
	- BMP image frames
	- Named as "dummy_N.bmp", where "N" is the value of the multiplicity

For the file naming, for example, if the THP is titled "battle_retro.thp", and you are using the crop settings to rip the subvideo in the 1st row, 1st column, and 1st multiplicity, the default filename would be "battle_retro_A1_1".

When either ripping or viewing a THP file, progress and detail about the current operation shall be shown in the progress bar sets.

#### THP Encoder

This group box allows the user to encode a new THP formatted as such to replace the original one currently selected in the THP File combo box. The left side of the group box has an array of checkboxes, which depict to the user which subvideo cells are in use in the video array and the naming convention that will be needed for each cell in the video's array of subvideos. If the checkbox is white and checked, that cell will be used for this THP video.

The input files have a base filename of [thp_filename].ext . Videos should have extensions of MP4 (H.264 encoded), audio files as WAV, and padding frames as BMP files. The array cells are in Microsoft Excel A1N notation, where columns are letter digits (1st column="A", 2nd column="B", etc) and rows are numbers (1st row=1, 2nd row=2, etc). In Microsoft Excel A1N notation, "A1" would refer to the 1st row, 1st column. The array also has a textbox showing the multiplicity range ("Multi", _1 to _M), and two additional checkboxes: "wav" and "dum". "wav" signifies an audio WAV file is needed, and "dum" signifies that padding BMP frames will be needed.

Consider if we have a THP video file (named "test.thp") that is a 2x2 array of subvideos with 3 multiplicities, padding, and audio. For the 1st multiplicity, the top left subvideo will be named "test_A1_1.mp4", the top right "test_B1_1.mp4", the bottom left "test_A2_1.mp4", and the bottom right "test_B2_1.mp4" . For the 2nd multiplicity, these same subvideos would be named "test_A1_2.mp4", "test_B1_2.mp4", and so on. The audio file is applied to the entire THP video, and would be named "test.wav" . The padding frames are named slightly different. The frame for the 1st multiplicity would be "dummy_1.bmp", the 2nd "dummy_2.bmp", and so on. The subvideo cells for each multiplicity should have the same dimensions as shown in THP Info group boxe's "Subvid dims" fields and have the same, appropriate amount of frames. The padding should have the same dimensions as shown in the "Subvid dims" group box, and be formatted as described in the "Ctrl desc" field.

![Naming Convention scheme](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/naming_test_thp.png)

![THP Encoder](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/THP.png)
  
- **THP Encoder settings:**
	- Trunc Frame
		- Truncates the frame length for the multiplicity of each subvideo to the inputted value
		- Example usage/purpose
			- Plan to encode a THP with each subvideo having a max frame length of 480 frames
			- Some of the input subvideos are slightly above 480 frames
			- This option will drop all frames in the multiplicity of each subvideo above 480 frames
		- Adjusting this value will automatically adjust "Digs" value

	- Digs
		- Amount of digits in the video's total frame length
        	- = DigitsOf(trunc_frame value * multiplicity)
        	- Used for formatting the sequential numbering in naming of the final .jpg frames used for THPConv
        		- E.g. Should jpg frames be named such as "frame_005.bmp" (3 digs) or "frame_0005.bmp" (4 digs)?
	- JPG Qual
		- JPG Compression quality precentage for each JPG file used for THPConv (1%-100%)
			- Lower values
				- Higher compression
				- Lower visual quality
				- Higher streaming bandwidth quality
				- (Smaller THP filesize, but more blocky jpg artifacts)
			- Higher values
				- Lower compression
				- Higher visual quality
				- Lower streaming bandwidth quality
				- (Larger THP filesize, but fewer artifacts)

After setting your encoding settings, and clicking the "Encode" button, you will need to select the folder containing your appropriately named input files. The encoder will then begin processing your files; this will take some time. Make sure to have **significant amount of free disk space** available (a few GBs should be safe); some of the operations may produce a **large** amount of temporary files! During conversion, progress and details of the work shall be shown in the progress bar sections, as well as the Elevator Music playing if the option is set. After successful conversion, you will have a THP  video file in the input folder (named [thp_filename].thp), and all temporary files will be cleaned up!

##### **Streaming bandwidth notes:**

A **very** important note about the JPG quality, final THP filesize, and video streaming. Odds are the THPs you will be creating will be used for either a [Riivolution](http://wiki.tockdom.com/wiki/Riivolution) patch or a [MKWiiki "My Stuff" mod](http://wiki.tockdom.com/wiki/My_Stuff) from the SD Card. Experimentation has shown that streaming THP videos from SD Card has a much smaller bandwidth than when streaming from DVD. You may notice when playing a THP video from SD Card that the video will stutter (both audibly and visually) when there is too much action during certain frames of video playback. (To test this fact out yourself, copy MKWii's original title.thp file into your "My Stuff" folder, in order to patch the DVD game to read that file from SD Card instead. You will notice video playback for the title video will stutter and lag when too much action occurs , due to lower data bandwidth on the SD Card. This experiment was tested with a [4GB Lexar SDHC Card](https://www.digitalcamerawarehouse.com.au/lexar-sdhc-card-4gb-class-4), the type that come with Original Nintendo 3DS XLs).

In order to reduce lagging from too much bandwidth, use the **JPG Quality** field to generate JPG video frames with a good balance between visual quality and compression. Higher compression will lead to smaller overall THP video sizes, and to less data bandwidth being used to stream the video file (and thus smoother playback). You may also be able to get better playback quality with higher performance SD cards that are compatible with the Wii; however, do try to create THP files that would play well on lower performance SD cards, in order to cover high-quality playback for the least common denominator. For better performance, you can try burning your mod files and THPs to a DVD (via an [ISO Patcher](http://wiki.tockdom.com/wiki/ISO_Patcher)). Also of importance is keeping the THP filesize down. Some THP files from the original game will only accept replacement files so large before the game will crash. (For example, during the creation of my Hover Pack, battle_retro.thp crashed in-game when it was ~300MB large). Most replacement THP files should work with a total frame size > than the original to an extent. Some with a total frame length < than the original may need padded or looped to the original frame count. (Example, title screen videos from the Title folder **must** be the same frame count; using a shorter video than the original will loop it, and using a video longer will cut it out prematurely.)

## 4. How to use it (CLI)

Please refer to **section #3 (How to use it (GUI))** for basics on how to use Thwimp.

**The CLI has the same main actions as with GUI mode:**
- View
- Rip
- Encode THP files
- Help
	- Displays the same CLI arguments as from the Cmdline button from the Options tab

![CLI.png](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/CLI.png)

The main difference with CLI mode vs. GUI mode is with how some actions are accessed. The options and wanted Thwimp data fileset are set by loading in an INI settings file (via /s switch, e.g. /s="C:\Folder\Thwimp.ini"), and a THP file is selected by an ID vs. the THP combo box (via /t=NNN ID switch, e.g. "/t=001"). (This ID can be found on the label when in GUI mode). For the THP Viewer/Ripper, a subvideo and multiplicity preset is selected by a string (via /p switch, e.g. "/p=A2_1", to use subvideo cell A2, 1st multiplicity), **or** by manually setting the crop values in a csv list of values (via /c switch, e.g. "/c=0,0,320,240,1,600", to crop a 320x240 px video from the top left corner orgin, from time frame 1 to 600).

For **Rip** and **Encode** actions, instead of using a File dialog box to select the output folder and filename, it is just set by the /o output folder switch. The output folder switch is required for rip mode, optional for encode (it will place output files in the input folder with no switch). Simiarly, **Encode** mode needs the /i input folder switch to define where the input files for encoding are located. All path switches **should** enclose the path in "quotes", in order to handle paths with spaces in them!

For **Encode** action, the Trunc Frame and digits can be set by the /f switch as csv list, with the digits optional. (E.g. "/f=600,3" for 600 frames, 3 digits; or just "/f=600" for 600 frames and auto-calculated digits.) The JPG Quality setting is set by the /q switch, and is a value between 1-100 (e.g. "/q=85" for JPG Quality of 85%).

When running in CLI mode, it is **highly recommended** to enable Full Log option for the Thwimp INI settings file loaded! This will show all logging, progress bar information, msgboxes, and stdout from the FOSS co-utilities, all within the command prompt. Any errors or successful operations that occur will close Thwimp CLI mode and restore control back to the user.

**Examples calls for Thwimp CLI mode:**
- **Example scenario assumptions**
	- For the /s INI setting file, these examples will assume the default Thwimp data fileset for MKWii is loaded
	- Also assume that all paths are set correctly for the FOSS co-utilities in the INI file
	- The command prompt's current directory is set to the Thwimp executable

- **Examples**
	- **Encoding**
		- thwimp.exe /me /s="E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini" /t=002 /f=480,3 /q=85 /i="C:\Users\Anthony\Downloads\Output\TEST"
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles encoding a new file for ID=002 ("\battle\battle_retro.thp" video)
			- Trunc frame to 480 frames, digits to 3
			- JPG Quality to 85%
			- Input files for encoding located at "C:\Users\Anthony\Downloads\Output\TEST"

		- thwimp.exe /me /s="E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini" /t=002 /f=480 /q=85 /i="C:\Users\Anthony\Downloads\Output\TEST"
			- Same as above, but for /f switch, only Trunc Frames of 480 set, and digits auto-calculated


	- **Viewing**
		- thwimp.exe /mv /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /p=A2_2 /n=1
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles viewing THP file for ID=001 ("\battle\battle_cup_select.thp" video)
			- View preset subvideo cell A2, multiplicity 2
			- DirectSound enabled

		- thwimp.exe /mv /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /p=All_0 /n=1
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles viewing THP file for ID=001 ("\battle\battle_cup_select.thp" video)
			- View special preset All, multiplicity 0 (entire video)
			- DirectSound enabled

		- thwimp.exe /mv /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /p=Dum_2 /n=1
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles viewing THP file for ID=001 ("\battle\battle_cup_select.thp" video)
			- View special preset Dum, multiplicity 2
			- DirectSound enabled

		- thwimp.exe /mv /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /c=0,0,336,250,1,600 /n=1
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles viewing THP file for ID=001 ("\battle\battle_cup_select.thp" video)
			- View the area defined by following crop settings
				- Point at X-Pos = 0, Y-Pos = 0 (top left corner)
				- Physical video viewport of 336x250 px from that point
				- Time segment from frame 1 to frame 600
			- DirectSound enabled


	- **Ripping**
		- thwimp.exe /mr /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /p=A2_2 /n=1 /o="C:\Users\Anthony\Downloads\Output\TEST\Battle_Cup_Select_A2_2.mp4"
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles ripping assets from THP file for ID=001 ("\battle\battle_cup_select.thp" video)
			- Rip the area at preset subvideo A2, multiplicity 2
			- DirectSound enabled
			- Output file to Battle_Cup_Select_A2_2.mp4

		- thwimp.exe /mr /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /p=All_0 /n=1 /o="C:\Users\Anthony\Downloads\Output\TEST\Battle_Cup_Select.mp4"
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles ripping assets from THP file for ID=001 ("\battle\battle_cup_select.thp" video)
			- Rip the area at special preset All, multiplicity 0 (so entire video as one video)
			- DirectSound enabled
			- Output file to Battle_Cup_Select.mp4

		- thwimp.exe /mr /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /p=Dum_0 /n=1 /o="C:\Users\Anthony\Downloads\Output\TEST\Dummy.mp4"
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles ripping assets from THP file for ID=001 ("\battle\battle_cup_select.thp" video)			
			- DirectSound enabled
			- Create a video (Dummy.mp4) just for the entire Dummy padding section, rip each unique frame as BMP Frames (Dummy_1.bmp to Dummy_4.bmp)

		- thwimp.exe /mr /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=001 /c=0,0,336,250,1,600 /n=1 /o="C:\Users\Anthony\Downloads\Output\TEST\Battle_Cup_Select_A1_1.mp4"
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles ripping assets from THP file for ID=001 ("\battle\battle_cup_select.thp" video)
			- Rip the area defined by following crop settings
				- Point at X-Pos = 0, Y-Pos = 0 (top left corner)
				- Physical video viewport of 336x250 px from that point
				- Time segment from frame 1 to frame 600
			- DirectSound enabled
			- Output file to Battle_Cup_Select_A1_1.mp4

		- thwimp.exe /mr /s=E:\GC_Wii\Hacking\Tools\Video\Thwimp\Thwimp\bin\Release\thwimp.ini /t=027 /p=A1_1 /n=1 /o="C:\Users\Anthony\Downloads\Output\TEST\Title_A1_1.mp4"
			- Loads Thwimp.ini settings from the Thwimp executable folder
			- Handles ripping assets from THP file for ID=027 ("\title\title.thp" video)
			- Rip the area at preset subvideo A1, multiplicity 1.
			- DirectSound enabled
			- Output files
				- Video to Title_A1_1.mp4
				- Audio to Title_A1_1.wav
## 5. Customization
The data for each THP from the original MKWii game is hard-coded and read from external files. These 5, parallel data files can be edited to improve the accuracy of the database, give metadata information about the fileset, or could be completely modified for use in other GameCube/Wii games which use THP videos similarly. The default fileset should be placed in the Thwimp executable's directory, while those for other games should be placed into their own directory. Upon loading Thwimp, the wanted fileset is loaded by pointing it to its directory from the Data File Dir path option (or directly from the INI settings file). These files can use certain special values as separators/dummy entries. The 5 parallel files are FileListing.txt, FileData.txt, FileDesc.txt, FileCDesc.txt, and FileSet.txt

![Data Files](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/DataFiles.png)

**Image file data:**

- **FileListing.txt**
	- Each line is a relative path from the THP Root folder pointing to THP files
	- This is the list of files shown in the "THP Files" drop down box in "THP" tab	
	- Dummy entries/separators should be listed as "============================="
	- 0th entry **must** be a dummy entry

- **FileData.txt**
	- Each line has a list of values for all of the data for each THP file in FileListing.txt
	- Data fills in all of the numeric fields under the "THP Info" group box
	- Each entry in each line should be separate by commas (",")
	- Each line should end with a semicolon (";")
	- Lines used for dummy entries should be all zeroes (0s)

	- **Data for each line (see _"THP Info" in Section 3_)**
		- Width (Total dims)
		- Height (Total dims)
		- R (Array)
		- C (Array)
		- Subv (Array)
		- Multi (Subvideo Info)
		- Tot. Subv (Subvideo Info)
		- Subv Width (Subv dims)
		- Subv Height (Subv dims)
		- Pad Width (Subv dims)
		- Pad Height (Subv dims)
		- Subv (mult) frames (# of frames)
		- Total frames (# of frames)
		- FPS (Control)
		- Ctrl? (Control)
		- Audio? (Audio)
		- Stereo? (Audio)
		- Freq (Hz) (Audio)

	- **Corresponding Visual Basic data type for each entry**
		- **[UShort](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/ushort-data-type)**
		- UShort
		- **[Byte](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/byte-data-type)**
		- Byte
		- Byte
		- Byte
		- Byte
		- UShort
		- UShort
		- UShort
		- UShort
		- UShort
		- UShort
		- **[Single](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/single-data-type)**
			- This is parsed with invariant (EN-US) culture, and is expected to be in the format of ww.dd (dot character for fractional separator)
			- w = whole part
			- d = decimal part
		- **Byte as Boolean Bit (0=false, 1=true)**
		- Byte as Boolean Bit
		- Byte as Boolean Bit
		- UShort

	- **FileDesc.txt and FileCDesc.txt**
		- Former used as a text file describing the usage/formatting of each THP video
		- Latter used as a text file describing the usage/formatting of the control signal in the padding	
		- Each line is a [String](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/string-data-type) data type
		- Both should use "n/a" if unused/for dummy entries
		- Text must be single-line for each THP file, in order to be in parallel with FileListing.txt

	- **FileSet.txt**
		- Contains metadata to display in the Options tab, gives basic information about this fileset
		- **Data for each line**
			- Game
			- Description
			- Author
			- Version
			- Date
		- **Corresponding Visual Basic data type for each entry**
			- [String](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/string-data-type)
			- String
			- String
			- String
			- String

**Song.wav**

  If the "Elevator Music" option is set, Twhimp will playback a wav file (looped) during the long THP encoding process. This is used to indicate to the user that the encoding procedure is still running, and that it has ended when it stops. This file must be placed in the Thwimp exectuable's directory, and should be loopable.
  
**INI Settings file**

  Options can be saved/loaded from/into the application, using the "Load/Save Settings" buttons within the Options Tab. When saving, Thwimp will generate a valid INI settings file that it will understand.
  
![INI.png](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/INI.png)

**Thwimp INI file format:**
- **Basic info**
	- INI file starts with a magic header, also indicating the version of Thwimp that generated it
	- All other entries define options data
	- Data entry format
		- String marker
		- Assignment operator " = "
		- Data
			- Paths are always **absolute** paths
			- Rest of data are **bits (0=false, 1=true)** for checkboxes
- **Header info**
	- File **must** start with valid header!
	- Consists of string [Thwimp.ini v(version number)]
	 Version number should be in the format a.b.c.d
		- Version will be used in the future for backwards compatibility/legacy purposes, if INI format changes
- **Data**
	- Markers and their usage (**must** be in this order)
		- THPRoot
			- Path to THP root directory
		- FFMpegDir
			- Directory to FFMPEG\bin installation folder (with FFMPEG.exe and FFPLAY.exe)
		- FFplay_wdir
			- Working directory FFPlay
		- irfanview
			- Path to Irfanview executable (i_view32.exe)
		- thpconv
			- Path to THPConv.exe executable
		- dataDir
			- Path to folder with the game's fileset data defintiion files
			- If path is to default fileset (in Thwimp executable's directory), then use 0 instead
		- audio
			- Audio checkbox option (bit)
		- audio_bgm
			- Elevator music checkbox option (bit)
		- log_msgBox
			- Less MsgBox checkbox option  (bit)
		- log_Full
			- Full Log checkbox option (bit)

## 6. Change Log
 - **v1.2: 2nd revision, bugfixes, enhancements, CLI support (08/01/20)**
	- **Bugfixes**
		- **[Issue #5](https://github.com/Tamk1s/Thwimp/issues/5): Irfanview path bug/Issue, [Issue #6](https://github.com/Tamk1s/Thwimp/issues/6): JPG Quality Bug**
			- Fixed serious, application-breaking bug where pointing to a real i_view32.exe executable would cause the THP Encoder process to convert the ripped BMP frames from the final generated video to Progressive JPG files
			- This bug also fixes Issue #6 (same bug)
			- THPConv cannot handle Progressive JPG markers (SOF2 header of $FFC2), so this bug would cause **THP conversion to fail**
			- Bug cause
				- Pointing to a symlink of i_view32.exe does not read i_view32.ini option files when calling Irfanview command-line commands, and thus ignores JPEG Save Progressive option
				- Pointing to a real i_view32.exe install directory does read i_view32.ini option file, and thus may apply JPEG Save Progressive option (and break THP conversion)
			- Bugfix method (HackINIFile() function))
				- Read i_view32.ini file (located at exe path)
				- Locate the options i_view32.ini pointed to from within (usually located at %APPDATA%\Irfanview)
				- Copy that options i_view32.ini file to the THP Encoding working directory
				- Hack that INI file with changes
					- Save Progressive=0 (false)
					- Save Quality=[User-defined JPEG Quality setting]
					- Run BMP -> JPG converion via Irfanview commandline during THP Encoding, using hacked INI file options and /JPGQ setting, in order to ensure no Progressive JPG output and proper JPG Quality setting
					
        	- **[Issue #7](https://github.com/Tamk1s/Thwimp/issues/7): THP encoding optimization**
            	- Issue related to encoding THPs that require dummy/padding frames
            	- Issue would cause the conversion of each multiplicities' dummy frame (dummy_N.bmp) into a dummy multiplicity video (dummy_N.mp4) to be poor, with their framecounts exceeding their intended value
            	- Issue would snowball further along the THP encoding process, and cause the final video to be much larger in framecount than expected
            	- This would, in turn, slow down the process of ripping BMP frames and converting to JPG frames for the actual THP encoding
            		- Application deletes all JPG files above the framecount limit ("Frame Trunc" * number_of_multiplicities), so many CPU cycles would be wasted without the bugfix
            	- Bug fixed with a better FFMPEG cmdline call, which properly converts the dummy BMP frames into a correct length dummy video
			- Fixed bug with THP Encoder "Trunc Frame" field not updating always when changing the selected THP video (THP combo box)
			- Made application start in Options Tab					
			
		- **[Issue #11](https://github.com/Tamk1s/Thwimp/issues/11): Error parsing string into Single**
			- Fixed meat of TryParseErr_Single() function
				- Made parsing of strings into Single be culture-invariant (EN-US culture)
			- Fixes issue where FPS value from the data fileset wouldn't be read properly in other cultures (such as EN-GB)
				- Single values must be defined as "ww.dd"
				- w = whole value
				- d=decimal values
				
		- **[Issue #12](https://github.com/Tamk1s/Thwimp/issues/12): KeepInRange parsing bugs**
			- Fixed issue where, in the THP Viewer/Ripper section, "Error Parsing String into [T]" errors will throw under the following conditions
				- If a MaskedTextBox is focused on
				- If MaskedTextBox cleared to empty (a bunch of underscores "_")
				- If the user focuses elsewhere
			- Fixed by less over-zealous error-trapping, to prevent false positives

		- **[Issue #13](https://github.com/Tamk1s/Thwimp/issues/13): Path Separator Char**
			- Split the constants used for "\\" (backslash path separator char) and "/" (FFMPEG cropping char) into 2 separate constants
			- Refactored code to use proper const refs
			- Allows for easier porting of code to other platforms
				- MacOS
				- Linux
				- Wrt handling the proper path separtor char
				
		- **[Issue #14](https://github.com/Tamk1s/Thwimp/issues/14): Lingering File I/O from StreamReaders/Writers**
			- Added proper closing of StreamReaders/Writers, regardless of whether successful usage or failure due to Try-Catch errors
			- This properly
				- Closes file I/O handles on files, in case of error and application is then quit
				- Unlocks them for everyday user file operations afterwards
				
		- Added an additional dummy entry as 0th entry in default MKWii data files

	- **Enhancements**
		- **[Issue #9](https://github.com/Tamk1s/Thwimp/issues/9): Thwimp CLI**
			- Implemented command-line interface into Thwimp!
			- Redirected all stdoutput from the application's console into the calling Command Prompt
		- **[Issue #8](https://github.com/Tamk1s/Thwimp/issues/8): THP Viewer/Ripper enhancements**
			- Modified the THP Viewer playback so that it
				- Applies the Crop Settings (both physical and time) to the playback
				- Now properly trims any audio streams for playback of applicable THP videos!
					- This is done via ripping audio and video streams separately (temporary MP4 files with only one stream type), trimming both, and remerging both back into a final temp mp4 video file
					- Playback final temp mp4 video file with the trimming applied
					- Temporary files generated in FFPlay WorkDir (new Options directory setting)
			- Modified THP Ripper so that it now properly trims any ripped audio streams for applicable THP videos!			

		- **New Options**
			- **Fileset Data**
				- New **"FileSet.txt"** file
					- Informational file which lists this fileset's
						- Game
						- Desc
						- Author
						- Version
						- Creation date
					- Data is displayed in new fields within Options tab
				- Updated stock MKWii Fileset with the new FileSet.txt file

			- **Paths**
				- FFPlay WorkDir
					- Working Directory for FFPlay temporary files
					- Used when viewing/ripping THP files with both Audio and Video streams, when cropping time
                			- Directory used **must** have read/write access!
				- DataFile Dir
					- Directory to use containing data files to use for Thwimp (e.g. customization to use for other games)

			- **Option flags** (in Options tab)				
				- "Audio" checkbox
					- Toggles audio
				- "Elevator Music" checkbox
					- Toggle THP Encoder elevator music
				- "Less MsgBox" checkbox
					- Supresses most informational MsgBoxes during THP Encoding processes
					- Prevents interruption for this long process
				- "Full Logs" checkbox
					- Logs **everything** (including stdout from Process cmdline calls)	
				- **Settings INI file**
					- Buttons to save/load a Thwimp INI settings file
					- Allows for saving/loading of settings

			- **Help/Resources**
				- Added new section with various buttons to launch URLs in default browser with Thwimp resources
				- Buttons
					- Webpage
						- [EagleSoft Ltd. Thwimp webpage](http://www.eaglesoftltd.com/retro/Nintendo-Wii/thwimp)
					- About
						- Application about box
					- MKWiiki Article
						- [MKWiiki Thwimp article webpage](http://wiki.tockdom.com/wiki/Thwimp)
					- Manual (Github)
						-  [Thwimp's readme.md webpage on this Github repo](https://github.com/Tamk1s/Thwimp/blob/master/README.md)
					- Latest Release
						-  [Releases webpage on this Github repo](https://github.com/Tamk1s/Thwimp/releases)
					- Cmdline
						- Details valid command-line options for application (for CLI mode)				

		- **Added audio to application**
			- Custom error sfx (SMB1 Mario death)
			- Custom success sfx (SMB1 flagpole)
			- Elevator Music
				- Looping song bgm to play during long THP Encoding process
				- Song.wav at application directory
				- Default application [song.wav](https://github.com/Tamk1s/Thwimp/blob/master/Thwimp/Resources/song.wav) is my own [MKWii Menu (SMPS32x) song](https://www.youtube.com/watch?v=9IgTsuiI_ig)
				- Audio toggle via new "Elevator Music" checkbox in Options tab
			- Audio toggle via new "Audio" checkbox in Options tab
		- **Progress bars/application logging**
			- **Progress bars**
				- Added a "Progress" section in THP tab, showing status of processes
				- 2 progress bar groups
					- Total
					- Current
						- Progress for a particular chunk in total progress
						- Think of those old Installshield installer wizards from Windows 95, with multiple progress bars
					- Elements within a progress bar group
						- Textbox (logs progress messages)
						- Progressbar (self-explanatory)
						- Label (display current progress percentage)
				- **Refactored and improved THP Encoding process**
                	- Display total/current encoding progress

                    - Log informational progress messages
			- **Application logging**
				- Added new "Log" group box section
				- Logs various mesages
					- Application messages
					- Total/Current progress messages
					- MsgBox messages (and icons)
				- Control buttons
					- Cls
						- Clears various log elements
							- Total/current progress bar elements
								- Progress bar
								- Messages
								- Progress percentage
							- Main Log box
								- Log text
								- MsgBox icon
					- Save
						- Saves log text to a file (for error reporting/debugging)
 
 - **v1.1: 1st revision** (01/06/19)
	- Bugfixes
		- Fixed a bug with proper framerate not being applied to encoded THP videos for replacement
		- Fixed a bug with ripping of some control frames from certain THP videos (especially file "thp\battle\battle_cup_select.thp") would be off by 1.
	- Enhancements
		- Made labels and group box headers **bold**
		- THP Decoder
			- Added nud, and array of radio buttons to select a particular multiplicity/subvideo cell to crop to
			- Added start/end fields for clipping a video to a certain time period
			- Removed "Rip to [format]" checkbox
				- Audio file is automatically ripped if original THP has one
				- Dummy frames (if exist) are ripped if "dum" radio button is selected, multiplicity=0, and the start/end frame values = video's min/max frame values
			- Made Rip button use the appropriate default filename based on the subvideo/multiplicity being cropped
	- Readme
		- Updated THP Decoder section		
		- Added important information about streaming bandwidth from SD Card vs. DVD
		- Images
		- Additional external links
		- Grammar/spelling checking
 

 - **v1.0: Initial Release!** (10/24/18)
 	- Created THP and Options Tabs.
 	- Implemented THP info reading
 	- Implemented THP Viewing
 	- Implemented THP Ripping 
 	- Implemented THP Encoding 

Development of the 1st release was from October 19-23, 2018. It was based on my manually-crafted batch files I created for concatenating videos into arrays, which was used for creating THPs for my [Hover Pack](http://wiki.tockdom.com/wiki/Hover_Pack), a set of battle courses for MKWii from Microsoft Hover! (1995 original) courses.


## 7. Known Issues
 - Encoded THP videos may lag or stutter on real hardware (especially when streaming modded files from SD card via Riivolution)
 	- Read **"Streaming bandwidth notes" subsection** in **Section 3 How to use it** about this issue.
 
## 8. Credits:

- **Special thanks (v1.2 Beta Testers, error reporters, etc)**
	- Application would still be broken and usuable from v1.1 version without their helpful error reports!
	- [H1KOUSEN](https://www.youtube.com/channel/UC_a_CsLJGAcUHKUFoIhukwQ/videos)
		- For pointing out that v1.1 was broken and unusable
		- Lead to discovery and fixing of [issue #5](https://github.com/Tamk1s/Thwimp/issues/5) and [Issue #6](https://github.com/Tamk1s/Thwimp/issues/6)
	- [Moukrea](https://github.com/Moukrea)
		- For discovering and reorting [Issue #11](https://github.com/Tamk1s/Thwimp/issues/11) when parsing strings as single in a foreign culture

- **Co-utilities**
	- The creators of FFMPEG and FFPlay,
		- A complete, cross-platform solution to record, convert and stream audio and video. 
   		- [FFMPEG.org webpage](https://www.ffmpeg.org/)

 	- Irfan Škiljan, the creator of IrfanView, 
 		- An image viewer, editor, organizer and converter program for Microsoft Windows
 		- [Irfanview webpage](https://www.irfanview.com/)

 	- "Diddy Kong" the creator of THPGUI (from the Super Smash Bros. Brawl forums)
		- [Do a Google Search](http://www.google.com)   
	
- **Artwork assets**
 	- The Paper NES Guy (DeviantArt)
 		- [Thwimp artwork](https://www.deviantart.com/the-papernes-guy/art/Thwomps-Thwomps-Thwomps-186879685)
 	- Asneter (DeviantArt)
	 	- [Filmstrip art](https://www.deviantart.com/asneter/art/Filmstrip-stock-349762273)
 	- Flipfloppery (DeviantArt)
 		- [Lakitu art](https://www.deviantart.com/flipfloppery/art/Piras-kart-445108302)
 	- ariarts258 (DeviantArt)
	 	- [MKWii Riding art](https://www.deviantart.com/ariarts258/art/mario-kart-wii-244952252)	

- **General**
	- Tock, Wiimm, the MKWii modding community, et. al.
		- For all of their technical information and for the MKWiiki
		- [MKWiiki](http://wiki.tockdom.com/wiki/Main_Page)

 	- Nintendo
		- Nintendo Wii
    		- Mario Kart Wii
    		- Super Mario franchise
    		- Etc.
