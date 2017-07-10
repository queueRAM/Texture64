# Texture64
N64 texture ripper and editor

![ScreenShot](https://i.imgur.com/g8H8IVc.png "RGBA16 SM64 HUD Elements - Win7")
![ScreenShot](https://i.imgur.com/hCGskoi.png "CI8 MK64 Bombs - Linux")

## Current Features
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
* Multiplatform. Tested under Windows 7 and under mono on Linux

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
