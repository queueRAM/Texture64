# Texture64
N64 texture ripper and editor

![ScreenShot](https://i.imgur.com/GlNDv7W.png)

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
* Right-click image to extract to image file (.png, .jpg, .bmp)
* Left-click to advance offset by the entire image
   * Hold Ctrl modifier to reverse by entire image
   * Hold Shift modifier to advance/reverse by one pixel (varies by format)
* Mouse wheel to scroll up/down by rows of image
* Click "Insert..." to import image in place at current offset (does not overwrite file)
* Click "Save" to overwrite opened file

### Changelog ###

0.0.1: Beta test release
* supports RGBA16, RGBA32, IA16, IA8, IA4, I8, I4, CI8, CI4, 1bpp
* right-click export, insert to import
* ctrl/shift modifiers to advance/reverse offset
