# Texture64
N64 texture ripper and editor

![Win10 Screenshot](https://user-images.githubusercontent.com/129774/103450795-122e3b80-4c70-11eb-856a-75b7d166ca0c.png "RGBA16 SM64 HUD Elements - Win10")

## Features
* Export or import N64 textures of the following formats:
   * RGBA16
   * RGBA32
   * IA16
   * IA8
   * IA4
   * I8
   * I4
   * CI8
   * CI4
   * 1bpp
* Load palette from separate file
* Split palette mode to point part of palette to non-contiguous area
* Multiplatform. Tested under Windows 10, Windows 7, and on Linux using [Mono](https://www.mono-project.com/)

### Usage ###
* Right-click image to bring up context menu
   * Export to image file (.png, .jpg, .bmp)
   * Assign palette offset relative to current texture view
* Left-click to change offset to clicked pixel
* Mouse wheel to scroll up/down by four rows of image
   * Hold Ctrl modifier to scroll by entire image
   * Hold Alt modifier to scroll by one row
   * Hold Shift modifier to scroll by one pixel
* Click "Open..." [Ctrl-O] or drag and drop to open a binary file
* Click "Insert..." [Ctrl-I] to import image at current offset (does not overwrite file)
* Click "Save" [Ctrl-S] to overwrite opened file
* Enable "Split Palette" to point end of palette to different offset
* Enable "External Palette" to use different file for CI palette

### Changelog ###

0.2: UI Updates and bug fixes
* Save settings for Form position, scale, BG color, custom viewer
* Only show palette controls if CI is selected
* Allow inserting images beyond the current file size
* Correct behavior of drag-and-dropped files
* Split file size text into its own status box
* Correct 1bpp bit order to be MSbit first
* Add alpha channel mode for I8 and I4 codecs

0.1.1: Bug fixes

0.1: Initial release
* Improve IA4 color scaling
* Add checkbox for external palette and write to palette file when saving
* Add save confirmation dialog during close/open
* Add keyboard shortcuts for toolbar buttons
* Add Copy to Clipboard context menu option and Ctrl-V paste shortcut
* Change mouse click and mouse wheel functionality

0.0.3: Beta test 3 release
* Add right-click context menu for exporting and setting palette offset
* Show hovered pixel color information
* Add Shift modifier for mouse wheel scrolling

0.0.2: Beta test 2 release
* Improve responsiveness of click events
* Add mouse wheel support for scrolling up/down rows
* Allow input binary to be passed on command line
* Add Scale option in toolbar

0.0.1: Beta test release
* Supports RGBA16, RGBA32, IA16, IA8, IA4, I8, I4, CI8, CI4, 1bpp
* Right-click export, insert to import
* Ctrl/Shift modifiers to advance/reverse offset
