# Thwimp

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

Thwimp is a Windows utility which allows users easily to view, to rip, and to encode Nintendo THP video files for Mario Kart Wii (and for other GCN/Wii games, to an extent). Written in Visual Basic (from Visual Studio 2010 IDE), the Thwimp application calls some FOSS and other command line tools **(not included)** "from arms length" via the Command Prompt to perform its tasks.

**Specifically, Thwimp uses:**

- FFMpeg for video processing
- FFplay for THP playback
- Irfanview for image conversion
- THPConv **(definitely not included)** for encoding JPG frames + wav file into THP videos

Thwimp can show the hard-coded information about how each THP file is formatted in Mario Kart Wii, Thwimp can also view MKWii's THP files, as well as convert them to everyday video files. It can crop the THP video files, and then convert them to MP4 (H.264 codec) video files, to WAV files (for videos with audio), and padding to BMP files.

The THP video files in Mario Kart Wii tend to be an array of multiple subvideo cells inside, with each cell playing back several other videos (multiplicities). As required by the file format specification, THP video files must have their dimensions be a multiple of 16px. Often times the size of the subvideo array will not be enough to meet the specification, so padding is added to the bottom of the THP video. This padding is not only used to meet the specification, but used for control information. For example, some of the videos shown during menus will have a white rectangle move at integer positions at each multiplicity. This white rectangle controls which row in a menu is highlighted during THP playback.

Thwimp can intelligently handle audio, subvideo array, multiplicity, and padding/control information when encoding THP files to replace ones in-game. It does this by accepting appropriately named, input mp4 video files for each subvideo cell in the array, and for each multiplicity. It will also accept a WAV audio file, and BMP image frames for each multiplicity padding/control signal as needed. After reading the input files, Thwimp will intelligently process and splice all of the files together appropriately in order to create a high-quality, properly formatted THP video replacement file!

## 2. Why was it created?

It was created in order to help MKWii modders to create high-quality, original THP video files for their custom track distributions and mods, using subvideos for each cell in the THP's array of subvideos. Specifically, it was created for the creation of more complex THP files, like **"battle\battle_cup_select.thp"** and for **"course\cup_select.thp"**.

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
    	- Right column - Trakcs from 2nd row of cups in menu layout
    	- Each cup has 4 tracks (4 multiplicities for the subvideos)
    	- Every 120 frames, the subvideos show the next track in each cup (goes to the next multiplicity)
    	- Video has some padding at bottom
    		- Padding has a white rectangle which moves to a different integer position at every multiplicity
    		- Position of white rectangle determines which row (which track) is highlighted in the menus when a cup is selected


After doing what this program does for my projects (manually and painfully via batch files), I created Thwimp in order to automate the THP creation process for complex videos like the ones above.



## 3. How to use it

### Prerequisites:

As mentioned earlier, Thwimp uses some FOSS and other Command Line tools for processing **(not included)**. You will need to download/install the following utilities before using Thwimp.

**Utilities:**

 - [FFmpeg (with FFplay)](https://ffmpeg.zeranoe.com/builds/)
 - [Irfanview (32-bit)](https://www.irfanview.com/main_download_engl.htm)
 - [THPConv or THPGUI](http://www.google.com)

### Setting up paths:

Before doing **anything** in the utility, goto the "Options" tab of the application and set your paths. Do this by clicking the "Browse" buttons beside each item and navigating to the appropriate places with the file dialog boxes that appear.

**Paths to set:**
1. THP Root
	- Path containing the original Nintendo THP video files for viewing/ripping
    - On a legally obtained, self-dumped Mario Kart Wii ISO from a copy of the disc that you own **(don't ask me where to get one)**, this should be the "root\thp" folder extracted from the 2nd Partition of the ISO.
    - Use Dolphin's ISO Filesystem browsing feature, or [Wiimm's ISO Tools](http://wiki.tockdom.com/wiki/Wiimms_ISO_Tools)

2. FFMPeg Exe Root
	- Points to the folder containing the executables of FFmpeg
    - Usually located at "[FFmpeg root]\bin", should contain
    	- ffmpeg.exe
        - ffplay.exe
3. IrfanView
	- Points to the irfanview executable (i_view32.exe)
4. THPConv Exe
	- Points to THPConv.exe
    - A command line tool used to encode jpg frames and a wav file to a Nintendo THP video. 
    - **Do not ask me where to obtain this!**

The "About" button displays information about the current build of this application. After setting all of your paths, the elements in the "THP" tab will be available!


### Using the THP tab:

At the top of the THP tab will be a dropdown box. Use this to select which THP file from MKWii to view, rip, or encode a new THP for. After selecting a valid THP file, the THP Viewer/Ripper and THP Encoder group boxes will be available for use.

#### THP Info:

The "THP Info" group box gives statistical information about each original, Nintendo THP video in Mario Kart Wii. You will need this information to create appropriately formatted input files for THP encoding!

- **Video**
 - Total dims
	- Width
		- Width of the entire THP video
	- Height
		- Height of the entire THP video

 - Video array info
	- Array
		- R
			- Amount of rows in the array of subvideos
		- C
			- Amount of columns in the array of subvideos
		- Subvids
			- Amount of subvideo cells in the array (S = R * C)
		
	- Subvideo Info
		- Mult
			- Multiplicity for each video
			- Example
				- Some videos may show several tracks within one subvideo cell 
				- "course\cup_select.thp" in MKWii
				- THP shows 4 tracks in each cell, so mult = 4 here
		- Tot. Subvids
			- Total amount of subvideos in the THP video (T = R * C * Mult)
		
	- Playback info
		- Subvid dims
			- Subv (width,height)
				- Width and height of each subvideo cell within the array
			- Pad (width, height)
				- Width and height of the padding for the THP video
				- THP video total dims must be a multiple of 16px
				- Padding used for 2 purposes
					- Pad total video dims to a multiple of 16px
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
				- Padding used for control purposes?
				
			- Ctrl Desc
				- Information about how the control signal is formatted within the padding
				
- **Audio**
	- Audio?
		- Does THP have audio?
	- Stereo?
		- Is the audio stereo?
	- Freq (Hz)
		- Frequency of the audio file
		- Usually 32000 Hz for MKWii
		
		
- **THP File Desc**
	- Gives description about
    	- How the THP video is formatted
        - THP file's usage/purpose in-game


#### THP Viewer/Ripper

The "THP Viewer/Ripper" group box allows one to view/convert the THP to audio/video files. This group box has the following settings/form elements:

- "DirectSound?" checkbox
	- Use DirectSound as the SDL_SOUNDDRIVER value for audio playback?
	- Fixes audio playback bug in current version of FFMPEG for Windows
	- Should be checked by default
	
- "Play" button
	- Plays currently selected THP file.
	- Use "DirectSound" option for audio playback as needed

- "Rip to [file formats]" checkbox
	- Unchecked states
		- "MP4"
			- Converts THP into an H.264 MP4 video file
		- "MP4+WAV"
			- For applicable videos with audio streams only
			- Converts THP into an H.264 MP4 video file and WAV audio file

	- Checked states
		- Only available for videos with padding
		- "MP4, Dummy"
			- Converts THP to MP4 video file cropped to the padding area
			- Adjusts the "Crop Settings" group box fields appropriately
			- Saves 1st frame from each mult. in the padding area to a BMP file
			- Use the BMP files for the control signal to encode a new THP
		- "MP4+WAV, Dummy"
			- For applicable videos that have both audio and padding
			- Same as "MP4, dummy", but also rips the audio stream as WAV file
			
- Crop Settings
	- Crops the converted THP video to a specific width and height starting at a point.
	- Point coordinates are relative to top-left corner of video (origin (0,0))
	- Fields
		- X-pos
			- x position to start crop
		- Y-pos
			- y position to start crop
		- Width
			- Width to crop video
		- Height
			- Height to crop video
		
- "Rip" button
	- Converts the THP video to the appropriate file format(s)
	- See "crop settings", "directsound", and "Rip to [file format]" options


Tweak the settings as appopriately and use the "View" or "Rip" buttons to play a THP or convert it, respectively. Pressing the "rip" button will ask the user to select an output directory and base filename for the converted files; by default, the path is at the location of the original THP, and the filename is "[thp_filename]". Depending on the "Rip to format" option, the extensions for the output files can be MP4 (H.264 codec), WAV, or BMP. The dummy frame files will be named "dummy_N.bmp", where "N" is the value of the multiplicity. It is advised to set the base filename to the naming conventions in the "THP Encoder" section if ripping a subvideo. (Fox example, if the THP is titled "battle_retro.thp", and you are using the crop settings to  rip the subvideo in the 1st row, 1st column, and 1st multiplicity, set the base filename to "battle_retro_A1_1".

#### THP Encoder

This group box allows the user to encode a new THP formatted as such to replace the original one currently selected. The left side of the group box has an array of checkboxes, which depict to the user which subvideo cells are in use in the video array and the naming convention that will be needed for each cell in the video's array of subvideos. If the checkbox is white and checked, that cell will be used for this THP video.

The input files have a base filename of [thp_filename].ext . Videos should have extensions of MP4 (H.264 codec), audio files as WAV, and padding frames as BMP files. The array cells are in Microsoft Excel A1N notation, where columns are letter digits (1st column="A", 2nd column="B", etc) and rows are numbers (1st row=1, 2nd row=2, etc). In Microsoft Excel A1N notation, "A1" would refer to the 1st row, 1st column. The array also has a box showing the multplicity range (1 to M), and two additional checkboxes: wav and dum. Wav signifies an audio WAV file is needed, and dum that padding frames will be needed.

Consider if we have a THP video file (named "test.thp") that is a 2x2 array of subvideos with 3 multplicities, padding, and audio. For the 1st multiplicity, the top left subvideo will be named "test_A1_1.mp4", the top right "test_B1_1.mp4", the bottom left "test_A2_1.mp4", and the bottom right "test_B2_1.mp4" . For the 2nd multiplicity, these same subvideos would be named "test_A1_2.mp4", "test_B1_2.mp4", and so on. The audio file is applied to the whole video, and would be named "test.wav". The padding frames are named slightly different. The frame for the 1st multplicity would be "dummy_1.bmp", the 2nd "dummy_2.bmp", and so on. The subvideos for each multiplicity should have the same dimensions as shown in the "subvid dims" fields and have the same, appropriate amount of frames. The padding should have the same dimensions as shown in the "subvid dims" fields, and be formatted as described in the "Ctrl desc" field.
  
- **THP Encoder settings:**
	- Trunc Frame
		- Truncates the frame length for the multiplicity of each subvideo
		- Example usage/purpose
			- Plan to encode a THP with each subvideo having a max frame length of 480 frames
			- Some of the input subvideos are slightly above 480 frames
			- This option will drop all frames in the multiplicity of each subvideo above 480 frames
		- Adjusting this value will automatically adjust Digs value

	- Digs
		- Amount of digits in the video's total frame length
        - = DigitsOf(trunc_frame value * multiplicity)
        - Used for formatting the sequential numbering in naming of the final .jpg frames used for THPConv
        	- E.g. Should jpg frame be named "frame_005.bmp" (3 digs) or "frame_0005.bmp" (4 digs)?
	- JPG Quality
		- JPG Compression quality for each JPG file used for THPConv (1-100)
			- Lower values = high compression, low quality (small THP filesize, blocky jpg artifacts)
			- Higher values = low compression, high quality (large THP filesize, less artifacts)

After setting your encoding settings, and clicking the "Encode" button, you will need to select the folder containing your appropriately named input files. The encoder will then begin processing your files; this will take some time. Make sure to have significant **free disk space**; some of the operations may produce a large amount of temporary files! After conversion, you will have a thp file in the input folder (named [thp_filename].thp), and all temporary files will be cleaned up!

## 4. Customization
The data for each THP from the original MKWii game is hard-coded and read from external files. These 4, parallel data files can be edited to improve the accuracy of the database, or could be completely modified for use in other GameCube/Wii games which use THP videos similarly. These file should always be placed right by the Thwimp executable; otherwise Thiwmp may brick. Poor Thwimp! These files can use certain special values as separators/dummy entries.

**Image file data:**

- **FileListing.txt**
	- Each line is a relative path from THP Root folder pointing to THP files
	- This is the list of files shown in the "THP Files" drop down box in "THP" tab
	- Dummy entries should be =============================

- **FileData.txt**
	- Each line has a list of values for all of the data for each THP file in FileListing.txt
	- Data fills in all of the numeric fields under the "THP Info" group box
	- Each entry in each line should be separate by commas ","
	- Each line should end with a semicolon (";")
	- Lines used for dummy entries should be all 0s
	- Data for each line
		- Width (Total dims)
		- Height (Total dims)
		- R
		- C
		- Subvids
		- Mult
		- Tot. Subvids
		- Width (subv)
		- Height (subv)	
		- Width (pad)
		- Height (pad)	
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
		- Text must be single-line for each file to be in parallel with FileListing.txt

## 5. Change Log

 - **v1.0: Initial Release!**
 	- Created THP and Options Tabs.
 	- Implemented THP info reading
 	- Implemented THP Viewing
 	- Implemented THP Ripping 
 	- Implemented THP Encoding 

Development of the 1st release was from October 19-23, 2018. It was based on my manually-crafted batch files I created for concatenating videos into arrays, which was used for creating THPs for my ["Hover Pack"](https://www.youtube.com/playlist?list=PL3N-ZrZe1CWJuHpqPSVBL894-P98kXEjz), a set of battle courses for MKWii from Microsoft Hover! (1995 original) courses.


## 6. Known Issues
 - Ripping the control frames from file "thp\battle\battle_cup_select.thp" is off by 1.
 	- Appears to be due to the 1st control multiplicity of the video having an extra 2 frames of playback, which throws off the math

 
 - Encoding THP videos with audio may be heavily desynced.
 	- Manually convert such videos with Diddy Kong's THPGUI tool for better results

 
## 7. Credits:

 - The creators of FFMPEG and FFPlay,
	- A complete, cross-platform solution to record, convert and stream audio and video. 
   	- [FFMPEG.org webpage](https://www.ffmpeg.org/)

 - Irfan Škiljan, the creator of IrfanView, 
 	- An image viewer, editor, organiser and converter program for Microsoft Windows
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