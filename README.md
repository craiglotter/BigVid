BigVid
======

BigVid is a simple video menu for use on analog television sets when using said television set as the primary monitor. The menu allows for simple folder browsing and lists all located video files (for which it generates thumbnail images using Snatch-It as well.)

Created by Craig Lotter, August 2008

*********************************

Project Details:

Coded in Visual Basic .NET using Visual Studio .NET 2008
Implements concepts such as File manipulation
Level of Complexity: Very Simple

*********************************

Update 20080826.02:

- Tracks last played for each visited folder

*********************************

Update 20080909.03:

- Brings BigVid screen back up after video file closed
- No longer clears history labels if file is no longer available
- Opens Media Player in Fullscreen Mode, automatically closes player after video completes
- fixed bugged where clicking on last played label was overwriting last played in folder label despite the last played being possibly in a different folder from the current folder
- Refreshes panel changes on folder load, meaning viewer can watch folder load progress sequentially
- Added video duration label
- Added video tracking bar: video start point based on trackbar value
- Estimates how long you've watched into a particular video and then adjusts track bar accordingly
- Cleans up unwanted files in the thumbnails directory
- Enlarged Error Encountered Dialog
- Added support for .asf videos
- Displays progress while loading folder in terms of files to go
- Added History Dialog, tracking drives, folders and files accessed
- Returns focus to last clicked button when exiting video play
- Clicking on main thumbnail image now plays video
- Added Folder File History which is accessed by clicking on the folder name label

*********************************

Update 20080923.04:

- Added Delete folder functionality
- Now deletes files to Recycle Bin
- Fixed bug on Show History dialog involving Media loaded from CDs
