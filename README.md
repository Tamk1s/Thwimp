# Thwimp

![Thwimp logo](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/Thwimp/Resources/thwimp.png)

By [Tamkis/EagleSoft Ltd.](http://www.eaglesoftltd.com)

[Thwimp Project Page](http://www.eaglesoftltd.com/retro/Nintendo-Wii/thwimp)

## Table of contents

1. What is it?
2. Why was it created?
3. How to use it
4. Customization
5. Change Log
6. Known Issues/Todo
7. Credits


## 1. What is it?

Thwimp is a Windows utility which allows users easily to view, to rip, and to encode [Nintendo THP](http://wiki.tockdom.com/wiki/THP_(File_Format)) video files for Mario Kart Wii (and for other GCN/Wii games, to an extent). Written in Visual Basic (from Visual Studio 2010 IDE), the Thwimp application calls some FOSS and other command line tools **(not included)** "from arms length" via the Command Prompt to perform its tasks.

**Specifically, Thwimp uses:**

- FFMPEG for video processing
- FFPlay for THP playback
- Irfanview for image conversion
- THPConv **(definitely not included)** for encoding JPG frames + wav file into THP videos

![Utility logos](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/Utils.png)

Thwimp can show the hard-coded information about how each THP file is formatted in Mario Kart Wii. Thwimp can also view MKWii's THP files, as well as convert them to everyday video files. It can crop the THP video files, and then convert them to MP4 (H.264 codec) video files, to WAV files (for videos with audio), and padding to BMP files.

The THP video files in Mario Kart Wii tend to be an array of multiple subvideo cells inside, with each cell playing back several other videos (multiplicities). As required by the file format specification, THP video files must have their dimensions be a multiple of 16px. Often times the size of the subvideo array will not be enough to meet the specification, so padding is added to the bottom of the THP video array. This padding is not only used to meet the specification, but used for control information. For example, some of the videos shown during menus will have a white rectangle move at integer positions at each multiplicity. This white rectangle controls which row in a menu is highlighted during THP playback.

Thwimp can intelligently handle audio, subvideo array, multiplicity, and padding/control information when encoding THP files to replace ones in-game. It does this by accepting appropriately named, input mp4 video files for each subvideo cell in the array, and for each multiplicity. It will also accept a WAV audio file, and BMP image frames for each multiplicity for a padding/control signal as needed. After reading the input files, Thwimp will intelligently process and splice all of the files together appropriately in order to create a high-quality, properly formatted THP video replacement file!

## 2. Why was it created?

It was created in order to help MKWii modders to create high-quality, original THP video files for their custom track distributions and mods, using subvideos for each cell in the THP's array of subvideos. Specifically, it was created for the creation of more complex THP files, such as **"battle\battle_cup_select.thp"** and for **"course\cup_select.thp"**.

### Examples of complex THPs used in MKWii, which Thwimp helps create

- **"battle\battle_cup_select.thp"**
	- Shows CAD animated walkthroughs of each track in both the Wii and Retro battle cups    
    - When a battle cup is highlighted (but not selected), the subvideos for each track are shown for the current cup
    - Video layout
    	- Array of 2x1 subvideos
    	- Whole THP video lasts 600 frames long
    	- Top row = Wii battle courses
    	- Bot row = Retro battle courses
    	- Each cup has 5 courses (5 multiplicities per subvideo)
    	- Every 120 frames, the subvideos show the next track in the cup (goes to the next multiplicity)
    	- Video has some padding at bottom
    		- Padding has a white rectangle which moves to a different integer position at every multiplicity
    		- Position of white rectangle determines which row (which track) is highlighted in the menus

![battle_cup_select.thp](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/battle_cup_select.png)
- **"course\cup_select.thp"**
    - Shows the CAD animated walkthrough for each individual track in the game (for racing modes).
    - Game has 8 cups, each with 4 tracks (32 tracks)
    - Track menu layout
    	- Array of 2x4 cups
    	- Each cup has 4 tracks
	- Video layout
    	- Array of 4x2 subvideos
    	- Whole THP video lasts 480 frames long
    	- Left column = Tracks from 1st row of cups in menu layout
    	- Right column - Tracks from 2nd row of cups in menu layout
    	- Each cup has 4 tracks (4 multiplicities for the subvideos)
    	- Every 120 frames, the subvideos show the next track in each cup (goes to the next multiplicity)
    	- Video has some padding at bottom
    		- Padding has a white rectangle which moves to a different integer position at every multiplicity
    		- Position of white rectangle determines which row (which track) is highlighted in the menus when a cup is selected

![cup_select.thp](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/cup_select.png)

After doing what this program does for my projects (manually and painfully via batch files), I created Thwimp in order to automate the THP creation process for complex videos like the ones above. The application was used to create the custom THPs in my [Hover Pack project](http://wiki.tockdom.com/wiki/Hover_Pack)


## 3. How to use it

### Prerequisites:

As mentioned earlier, Thwimp uses some FOSS and other Command Line tools for processing **(not included)**. You will need to download/install the following utilities before using Thwimp.

**Utilities:**

 - [FFMPEG (with FFPlay)](https://ffmpeg.zeranoe.com/builds/)
 - [Irfanview (32-bit)](https://www.irfanview.com/main_download_engl.htm)
 - [THPConv or THPGUI](http://www.google.com)

![Utility logos](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/Utils.png)
### Setting up paths:

Before doing **anything** in the utility, goto the "Options" tab of the application and set your paths. Do this by clicking the "Browse" buttons beside each item and navigating to the appropriate places with the file dialog boxes that appear.

![Options](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/Options.png)

**Paths to set:**
1. THP Root
	- Path containing the original Nintendo THP video files for viewing/ripping
    - On a legally obtained, self-dumped Mario Kart Wii ISO from a copy of the disc that you own **(don't ask me where to get one)**, this should be the ["root\thp"](http://wiki.tockdom.com/wiki/Filesystem/thp) folder extracted from the Partition 1 (0-based ID) of the ISO.
    - Use Dolphin's ISO Filesystem browsing feature, or [Wiimm's ISO Tools](http://wiki.tockdom.com/wiki/Wiimms_ISO_Tools)

2. FFMPEG Exe Root
	- Points to the folder containing the executables of FFMPEG
    - Usually located at "[FFMPEG root]\bin", should contain
    	- ffmpeg.exe
        - ffplay.exe
3. IrfanView
	- Points to the IrfanView executable (i_view32.exe)
4. THPConv Exe
	- Points to THPConv.exe
    - A command line tool used to encode jpg frames and a wav file into a Nintendo THP video. 
    - **Do not ask me where to obtain this!**

The "About" button displays information about the current build of this application. After setting all of your paths, the elements in the "THP" tab will be available!

![About](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/About.png)

### Using the THP tab:

At the top of the THP tab will be a dropdown box. Use this to select which THP file from MKWii to view, to rip, or to encode a new THP for. After selecting a valid THP file, the THP Viewer/Ripper and THP Encoder group boxes will be available for use.

#### THP Info:

The "THP Info" group box gives statistical information about each original, Nintendo THP video in Mario Kart Wii. You will need this information in order to create appropriately formatted input files for THP encoding!

![THP Info](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/THP.png)

**Video:**
 - Total dims
	- Width
		- Width of the entire THP video (in px)
	- Height
		- Height of the entire THP video (in px)

 - Video array info
	- Array
		- R
			- Amount of rows in the array of subvideos
		- C
			- Amount of columns in the array of subvideos
		- Subv
			- Amount of subvideo cells in the array (S = R * C)
		
	- Subvideo Info
		- Multi
			- Multiplicity for each video
			- Example
				- Some videos may show several tracks within one subvideo cell 
				- "course\cup_select.thp" in MKWii
				- THP shows 4 tracks in each cell, so mult = 4 here
		- Tot. Subv
			- Total amount of subvideos in the THP video (T = R * C * Mult)
		
	- Playback info
		- Subvid dims
			- Subv (Width, Height)
				- Width and height of each subvideo cell within the array (in px)
			- Pad (Width, Height)
				- Width and height of the padding for the THP video (in px)
				- THP video total dims must be a multiple of 16px
				- Padding used for 2 purposes
					- Pad total video dims to nearest multiple of 16px
					- Control signal. See THP video's "Control" group box for usage information
					
		- Number of frames 
			- Subv (mult)
				- Amount of video frames for each multiplicity of each subvideo
			- Total
				- Total amount of video frames in the THP file (T = Subv_mult * mult)
			
		- Control
			- FPS
				- **F**rames **P**er **S**econd playback speed of the THP
				- Default values
					- 59.94 Hz (NTSC)
					- 50 Hz (PAL)
				
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

#### THP Viewer/Ripper

The "THP Viewer/Ripper" group box allows one to view/convert the THP to audio/video files. This group box has the following settings/form elements:

![THP Viewer/Ripper](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/THP.png)

- "DirectSound?" checkbox
	- Use DirectSound as the SDL_SOUNDDRIVER value for audio playback?
	- Fixes [audio playback bug](https://forum.videohelp.com/threads/388189-ffplay-WASAPI-can-t-initialize-audio-client-error) in current version of FFMPEG for Windows
	- Should be checked by default
	
- "Play" button
	- Plays currently selected THP file.
	- Use "DirectSound" option for audio playback as needed
	
- "Rip" button
	- Converts the THP video to the appropriate file format(s)
			
- Crop Settings
	- Crops the converted THP video to a specific size, starting at a point, and from a starting video frame to an ending video frame
	- Point coordinates are relative to top-left corner of video (origin (0,0))
	- Fields
		- Position
			- xpos
				- x position to start crop (0-based, in px)
			- ypos
				- y position to start crop (0-based, in px)
		- Size
			- Width
				- Width to crop video (1-based, in px)
			- Height
				- Height to crop video (1-based, in px)
		- Time
			- Start
				- Starting frame value to begin ripping (1-based)
			- End
				- Ending frame value to begin ripping (1-based)
		
	- Subvideo array
		- Cells
			- Cells A1 to B6
				- Select a particular subvideo cell to crop to
				- Updates the crop setting fields appropriately
			- "All" radio button
				- Rip the entire array of subvideos into a single video file
			- "Dum" radio button
				- Rips the dummy padding/control signal area of video (if any)
				- If multiplicity = 0, and start/end frames are set to the min/max within THP video, then the unique dummy frames will be ripped as BMP files, named to the proper naming convention for re-encoding with a replacement THP video
		- Multiplicity
			- Mult numeric up/down control
				- Changes the multiplicity to rip in the selected subvideo cell.
				- A multiplicity of 0 will rip all multiplicities
				- This updates the start/end frame values accordingly
			- Multiplicity checkbox
				- Indicates if a "single" (unchecked) or "multiple" (checked, when the multiplicity=0) multiplicities will be ripped
			- Dum Frames
				- Indicates if unique dummy frames will be ripped as BMP files
				- Only enabled when the "Dum" radio button is selected

Tweak the settings as appropriately and use the "View" or "Rip" buttons to play a THP or convert it, respectively. Pressing the "rip" button will ask the user to select an output directory and base filename for the converted files; by default, the path is at the location of the original THP, and the filename is "[thp_filename]_suffix", where suffix is the appropriate one based on the A1N naming notation for the input files used for encoding (see "THP Encoder section"). If the original THP video has an audio stream, it will automatically be ripped with an crop setting option and named appropriately. Dummy/control frames will be ripped automatically and named appropriately if the THP video has padding/control frames, if the "Dum" radio button is selected, if the multiplicity is set to 0, and if the start/end frame values are set to the original video's min/max values.

**Files are ripped from THP video files in the following formats:**

- Video
	- MP4 (H.264 encoded) video file
	- Named as [thp_filename]_suffix.mp4
- Audio
	- WAV audio file
	- Named as [thp_filename].wav
- Dummy/padding frames
	- BMP image frames
	- Named as "dummy_N.bmp", where "N" is the value of the multiplicity

For the file naming, for example, if the THP is titled "battle_retro.thp", and you are using the crop settings to rip the subvideo in the 1st row, 1st column, and 1st multiplicity, the default filename would be "battle_retro_A1_1".

#### THP Encoder

This group box allows the user to encode a new THP formatted as such to replace the original one currently selected in the THP File combo box. The left side of the group box has an array of checkboxes, which depict to the user which subvideo cells are in use in the video array and the naming convention that will be needed for each cell in the video's array of subvideos. If the checkbox is white and checked, that cell will be used for this THP video.

The input files have a base filename of [thp_filename].ext . Videos should have extensions of mp4 (H.264 encoded), audio files as wav, and padding frames as bmp files. The array cells are in Microsoft Excel A1N notation, where columns are letter digits (1st column="A", 2nd column="B", etc) and rows are numbers (1st row=1, 2nd row=2, etc). In Microsoft Excel A1N notation, "A1" would refer to the 1st row, 1st column. The array also has a box showing the multiplicity range ("Multi", 1 to M), and two additional checkboxes: "wav" and "dum". "wav" signifies an audio WAV file is needed, and "dum" signifies that padding frames will be needed.

Consider if we have a THP video file (named "test.thp") that is a 2x2 array of subvideos with 3 multiplicities, padding, and audio. For the 1st multiplicity, the top left subvideo will be named "test_A1_1.mp4", the top right "test_B1_1.mp4", the bottom left "test_A2_1.mp4", and the bottom right "test_B2_1.mp4" . For the 2nd multiplicity, these same subvideos would be named "test_A1_2.mp4", "test_B1_2.mp4", and so on. The audio file is applied to the entire THP video, and would be named "test.wav" . The padding frames are named slightly different. The frame for the 1st multiplicity would be "dummy_1.bmp", the 2nd "dummy_2.bmp", and so on. The subvideos for each multiplicity should have the same dimensions as shown in the "subvid dims" fields and have the same, appropriate amount of frames. The padding should have the same dimensions as shown in the "Subvid dims" group box, and be formatted as described in the "Ctrl desc" field.

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
        	- E.g. Should jpg frame be named "frame_005.bmp" (3 digs) or "frame_0005.bmp" (4 digs)?
	- JPG Qual
		- JPG Compression quality for each JPG file used for THPConv (1-100)
			- Lower values
				- High compression
				- Low visual quality
				- High streaming bandwidth quality
				- (Small THP filesize, blocky jpg artifacts)
			- Higher values
				- Low compression
				- High visual quality
				- Low streaming bandwidth quality
				- (Large THP filesize, less artifacts)

After setting your encoding settings, and clicking the "Encode" button, you will need to select the folder containing your appropriately named input files. The encoder will then begin processing your files; this will take some time. Make sure to have **significant amount free disk space** (a few GBs should be safe); some of the operations may produce a **large** amount of temporary files! After conversion, you will have a thp file in the input folder (named [thp_filename].thp), and all temporary files will be cleaned up!

##### **Streaming bandwidth notes:**
A **very** important note about the JPG quality, final THP filesize, and video streaming. Odds are the THPs you will be creating will be used for either a [Riivolution](http://wiki.tockdom.com/wiki/Riivolution) filepatch or a [MKWiiki "My Stuff" mod](http://wiki.tockdom.com/wiki/My_Stuff) from the SD Card. Experimentation has shown that streaming THP videos from SD Card has a much smaller bandwidth than when streaming from DVD. You may notice when playing a THP video from SD Card that the video will stutter (both audibly and visually) when there is too much action during certain frames of video playback. (To test this fact out yourself, copy MKWii's original title.thp file into your "My Stuff" folder, in order to patch the DVD game to read that file from SD Card instead. You will notice video playback for the title video will stutter and lag when too much action occurs , due to lower data bandwidth on the SD Card. This experiment was tested with a [4GB Lexar SDHC Card](https://www.digitalcamerawarehouse.com.au/lexar-sdhc-card-4gb-class-4), the type that come with Original Nintendo 3DS XLs).

In order to reduce lagging from too much bandwidth, use the JPG Quality field to generate JPG video frames with a good balance between visual quality and compression. Higher compression will lead to smaller overall THP video sizes, and to less data bandwidth being used to stream the video file (and thus smoother playback). You may also be able to get better playback quality with higher performance SD cards that are compatible with the Wii; however, do try to create THP files that would play well on lower performance SD cards, in order to cover high-quality playback for the least common denominator. For better performance, you can try burning your mod files and THPs to a DVD (via an [ISO Patcher](http://wiki.tockdom.com/wiki/ISO_Patcher)). Also of importance is keeping the THP filesize down. Some THP files from the original game will only accept replacement files so large (for example, during the creation of my Hover Pack, battle_retro.thp crashed in-game when it was ~300MB large). Most replacement THP files should work with a total frame size > than the original to an extent. Some with a total frame length < than the original may need padded or looped to the original frame count. (Example, title.thp must be the same frame count; using a shorter video than the original will loop, and a longer one will cut out prematurely.)

## 4. Customization
The data for each THP from the original MKWii game is hard-coded and read from external files. These 4, parallel data files can be edited to improve the accuracy of the database, or could be completely modified for use in other GameCube/Wii games which use THP videos similarly. These file should always be placed right by the Thwimp executable; otherwise Thwimp may brick. Poor Thwimp! These files can use certain special values as separators/dummy entries. The 4 parallel files are FileListing.txt, FileData.txt, FileDesc.txt, and FileCDesc.txt.

![Data Files](https://raw.githubusercontent.com/Tamk1s/Thwimp/master/readme/DataFiles.png)

**Image file data:**

- **FileListing.txt**
	- Each line is a relative path from THP Root folder pointing to THP files
	- This is the list of files shown in the "THP Files" drop down box in "THP" tab
	- Dummy entries should be listed as "============================="

- **FileData.txt**
	- Each line has a list of values for all of the data for each THP file in FileListing.txt
	- Data fills in all of the numeric fields under the "THP Info" group box
	- Each entry in each line should be separate by commas (",")
	- Each line should end with a semicolon (";")
	- Lines used for dummy entries should be all zeroes (0s)
	- Data for each line (see "THP Info" in Section 3.)
		- Width (Total dims)
		- Height (Total dims)
		- R
		- C
		- Subv
		- Multi
		- Tot. Subv
		- Subv Width
		- Subv Height
		- Pad Width
		- Pad Height
		- Subv (mult) frames
		- Total frames
		- FPS
		- Ctrl?
		- Audio?
		- Stereo?
		- Freq (Hz)
	- Corresponding Visual Basic data type for each entry
		- [UShort](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/ushort-data-type)
		- UShort
		- [Byte](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/byte-data-type)
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
		- [Single](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/data-types/single-data-type)
		- Byte as Boolean Bit (0=false, 1=true)
		- Byte as Boolean Bit
		- Byte as Boolean Bit
		- UShort

	- **FileDesc.txt and FileCDesc.txt**
		- Former used as a text file describing the usage/formatting of each THP video
		- Latter used as a text file describing the usage/formatting of the control signal in the padding
		- Both should use "n/a" if unused/for dummy entries
		- Text must be single-line for each THP file in order to be in parallel with FileListing.txt

## 5. Change Log

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


## 6. Known Issues
 - Encoding THP videos may get desynced
 	- Read "Streaming bandwidth notes" subsection in Section 3 (How to use it) about this issue.
 - Trimming a THP video and then ripping it with audio will not have the audio file trimmed.
	- Feature not planned to be implemented. (Not easy to do with FFMPEG)
 
## 7. Credits:

 - The creators of FFMPEG and FFPlay,
	- A complete, cross-platform solution to record, convert and stream audio and video. 
   	- [FFMPEG.org webpage](https://www.ffmpeg.org/)

 - Irfan Škiljan, the creator of IrfanView, 
 	- An image viewer, editor, organizer and converter program for Microsoft Windows
 	- [Irfanview webpage](https://www.irfanview.com/)

 - "Diddy Kong" the creator of THPGUI (from the Super Smash Bros. Brawl forums)
	- [Do a Google Search](http://www.google.com)
   
 - Tock, Wiimm, the MKWii modding community, et. al.
	- For all of their technical information and for the MKWiiki
	- [MKWiiki](http://wiki.tockdom.com/wiki/Main_Page)
   
 - The Paper NES Guy (DeviantArt)
 	- [Thwimp artwork](https://www.deviantart.com/the-papernes-guy/art/Thwomps-Thwomps-Thwomps-186879685)
 - Asneter (DeviantArt)
 	- [Filmstrip art](https://www.deviantart.com/asneter/art/Filmstrip-stock-349762273)
 - Nintendo
 	- Nintendo Wii
    - Mario Kart Wii
    - Super Mario franchise
    - Etc.
