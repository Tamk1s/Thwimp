BreakGOLD Editor v1.1
By Tamkis/EagleSoft Ltd.
https://sites.google.com/site/eaglesoft92/
&
https://sites.google.com/site/eaglesoft92/pc/bg
==============

Table of contents:

1. What is it?
2. Why was it created?
3. How to use it
4. Customization
5. Change Log
6. Known Issues/Todo
7. Credits

==============

1. What is it?

BreakGOLD Editor is a utility
which can edit the internal .raw
art files and the .bgh High Score files
of the PC version of the indie breakout game
BreakGOLD. It can also create a difference patch
when creating a total conversion from the game,
can tweak certain UI settings of the game,
and can split/create .wav and .ogg files
for use in the game.

(See http://dualheights.se/breakgold/
for more information about the game.)

The .raw files are the image files used for
the "Internal art" within the game,
including built-in bricks, main menu
screens, etc. Most of the .raw files are,
in actuality, .bmp files with the
headers removed and encoded as 16-bit
RGB565 binary files. (There is one
file which is a 8bpp file.) The header files
of .bmp files contain the color depth,
the pixel length, and the pixel width
of the data, as well as other things.
These binary .raw files do not have this
information embedded inside of the file.

After much mathematical guesswork and reverse-engineering,
I have discovered and documented the hard-coded
dimensions for most of the .raw files in the game.
Some of the .raw image files have multiple sub-images
inside of them, with each image appended after one another
in a row as a spritesheet, which has also been documented.
---------

In the "Image Editor" tab of the utility, the
user can view the subimages inside each .raw file,
and rip each subimage individually or all at once as separate files.
The user can also create single .png spritesheet of each image.
Each file has a description
about its original usage and stats about the original
image too, including bytes per subimage, pixel 
width and height, number of subimages, if
transparency is enabled with the image, and the
bits per pixel format. Also
in this tab, one can load .bmp files and convert them
into .raw files.

In the "BGH Editor" tab of the utility, one
can either load or create a .bgh file.
The .bgh files in BreakGOLD are files
which save the user's high score.
Each file contains 10 entries which include
the name of the acheiver, the score,
and the amount of levels he completed.

In the "Audio Editor" tab, one can split
.wav audio files into smaller chunks
of x seconds, and can convert/re-encode
.wav/.ogg files into different formats
and with different audio paramets 
(frequency, 8/16 bit bitrates, mono/stero).
For each parameter seconds, BreakGOLD
can only play audio files of fixed rate
of x seconds long, hence the usefulness
of this utility to split long music/sfx
into smaller, streamable chunks. For example, 
.wav files of 22.050khz, 8-bit mono
can play files upto 14.20 seconds long. (This is
the best size vs. quality params I recommend
for streaming music chunks without a
huge drop in audio quality.)

In the "ROM Editor" tab, one can create a 
PPF 3.0 file differencing patch
between a clean, original BreakGOLD.exe
file and a hacked/modified BreakGOLD.exe.
Furthermore, the user can view and edit tweaks
of certain game settings

Lastly, in the "Options" tab, one MUST
load the Root directory of a copy of a 
PC version of BreakGOLD.

In the upcoming version 1.2,
there will be button to convert
a PC BreakGOLD total-conversion project
into a Android equivalent (for the 
Android version of BreakGOLD). Among other
things, the Android verison has a similar
yet slightly different file structure,
.ogg files in place of .wav files
for audio, and .png spritesheets in place
of multi-image .raw files. The Android version
still uses .bgh files for high scores
and the exact same .bgl level pack file!
==============

2. Why was it created?

It was created in order to promote
people to use the engine of this game
to create Breakout total-conversion fan games
or original works, such as with my Sonic CD Breakout
fan game (see EagleSoft Ltd
website at
https://sites.google.com/site/eaglesoft92/pc/sonic-cd-breakout), 
and to give users more customization power to the game!
The utility also serves as a front-end to apply PPF 3.0
file differencing patches, in order to facilitate
an easy (and more legal) way to distribute
BreakGold mods.

==============

3. How to use it

Before doing ANYTHING in the utility,
goto the Option tab,click the "Load"
button, and then in the file dialog box that appears,
select the Root directory of the BreakGOLD installation.
(You can download a demo of BreakGOLD from the DualHeights
website.) The "About" button displays information about
the current build of this application

Afterwards, you can then select either the "BGH Editor" or "Image Editor" tabs
in order to edit features

In the Image Editor, you can select images to view/rip/modify from the image
dropdown box. After selecting an image, a description, and stats about the
image will be displayed, including bytes per subimage, number of subimages,
pixel width and height, if black is a transparency color, and the bits per
pixel for the .raw image. The image
viewer allows the user to view subimages and to rip sub images to .bmp format,
either one at a time, or all at once (by toggling the Rip checkbox).
By toggling the AutoView checkbox, the user can allow the program
to automatically update the image in the image viewer when the subimage
ID is changed. The AutoName checkbox toggles
whether the image viewer will auto name ripped images. Images will be autonamed in the form
of [imagename].nnn.bmp, where [imagename] is the filename of the image,
and where nnn is the subimage number. Clicking the "Png" button
allows the user to rip all of the sub images into a single .png spritesheet
(for use in the Android version of the game or personal use.)

The BMP2RAW converter allows one to convert bmp files to the internal
.raw image format. By checking the Single/Batch checkbox, the user
can convert either one image or multiple subimages for the same
main image. The AutoName checkbox toggles whether the image viewer
will auto name ripped images. Images will be autonamed in the form
of [imagename].nnn.bmp, where [imagename] is the filename of the image,
and where nnn is the subimage number.

In the BGH Editor, you can enter the acheivers' names, high scores,
and highest level reached individually in the masked textboxes, and then
click "add" to add the data to the 3 parallel arrays of listboxes. Each 
BGH file MUST have 10 entries. Once 10 entries are submitted, click "Create!"
in order to select the filepath and filename of the .bgh file to make. Click
"Remove" to erase the entire listboxes. Also, the user can click "Load" in order
to view the data in an existing .bgh file. By clicking the check box
on this tab, you can toggle whether to sort the data by descending order of scores
or not when saving

In the Audio editor, under the "Audio File(s)" group box, click the checkbox
in order to toggle single or batch audio file selection, and then click
the "Load" button in order to load audio file(s). Under the "splitter"
group box, input the amount of seconds to split for each audio
chunk, and then click "split". Under the "Converter" group box,
toggle whether you want to manually set the audio parameters
or to automatically do a file format conversion (.wav<->.ogg).
Enter freqency, bit rate, and mono/stereo if wanted, and then
click "Convert."

In the "Rom Editor" tab, click the "Load Orig" and "Load Mod"
buttons in order to load the original BreakGOLD game
and the modified binary, respectively. Afterwards, you can
click the "Make PPF" button in order to create a PPF patch
for your mod, or click "Apply PPF", in order to create a mod
from an original BreakGOLD binary. After selecting the original
binary, you can then make tweaks to the ORGINAL binary in the 
"Tweaks" group box. Select a tweak from the numeric up-down,
type in new data in the databox, select the end-of-line option
(none, or null-terminator for game's LED string), and then click
"overwrite" button to make changes to the rom.
==============

4. Customization:
Due to not having all of the stats of the internal .raw images 
documented, the
applications reads the hard-coded
image data from external files. These 3, parallel data files
can be edited to improve the accuracy of the
database, or can be completely modified for
use in games whose art is organized in a similar
manner to BreakGOLD. Also, other tweaks can be
added to data files

Image file data:

FileData.txt:
Each line consists of a comma-separated
list of 6 entries, followed by a semicolon (;)
to end each line. Each line is parallel 
with the data from the other data files

The entries correspond as such:

1. Bytes per image
2. # of subimages
3. Pixel width
4. Pixel height
5. -1=special alpha stuff, 0=not transparent, 1=transparency with black
6. Bits per pixel, legal values are 16 (for 16bpp RGB565) and 8 (for 8bpp mono)

FileDesc.txt:

Each line contains a brief description of the use
of the image, and what the subimage animation are for.
Entries marked with equal sign horizontal lines are separators

FileListing.txt:

Each line contains a filepath for each image, starting from
the root directory of your BreakGOLD installation.
----------

Tweak datafiles:

Tweak Data:

Each line consists of a comma-separated
list of 2 entries, ended by a semicolon (;)
for each line. Each line is parallel 
with the data from the other data files.

The entries correspond as such:

1. Tweak start address in ROM (hexadecimal)
2. Tweak End address in ROM (hexadecimal)

TweakDesc.txt:

Each line contains a brief description of the contents
of the memory address for the tweak

==============

5. Change Log:

v1.1: Overhauled release!

*Created NEW "Audio Editor" and "Rom Editor" tabs and features

*Created database files for Tweaks
Added new code to handle ripping image files individually or all at once,
and as Android .png spritesheet

*Added partial 8bbp support, especially for LEDFont.raw file

*Bug fixes

v1.0: Initial Release!
Created Image Editor, BGH Editor, and Options
tabs. Also utilizes 3 parallel data files
for the image editor

Development began in December of 2013, but then
halted around February of 2014, due to university work,
and having issues with file handles not closing.

Bugs were finally fixed in July 2014, and the application was released!
==============

6. Known Issues:

NONE?

Todo (v1.2):

   1. Add code handling to enable
      a feature to convert a PC BreakGold
      project to an Android counterpart (for the Android
      version of the game)
==============

7. Credits:

Creating this application was a learning
process with properly doing hexadecimal operations,
loading and displaying images, and with file managment.
Some external dependecies and code examples were used to
get this application to run

*Icarus/Paradox, for creating PPF 3.0 specs and MakePPF/ApplyPPF
command-line programs. http://www.romhacking.net/utilities/353/

*The creators of Sox, a "swiss-army" command-line program
for handling audio files. http://sox.sourceforge.net/


*Special thanks to an unknown coder, for creating the command-line
application BMP2BIN. (I lost the URL for the site :< )

It converts 24-bit .bmp files
into RGR565 headerless .bmp/binary files. I hex-edited
the program to output files with a .raw extension
instead of a .bin extension, and renamed the application
"bmp2raw" :P.

*Members in a thread at VBForums.com, for writing
a subroutine for reading RGB565 headerless .bmp files
(http://www.vbforums.com/showthread.php?370291-Convert-bin-file-to-bmp-file)

*KJell Andersson himself, for creating the BreakGOLD engine, and for
actually encouraging my "experimental spirit" of reverse engineering
small parts of the game and for me pushing the game's engine
to its limits with Sonic CD Breakout development and completely
re-doing the internal art of the game :).
==============

[EOF]