using System.Drawing;

namespace Texture64
{
   public enum N64Codec { RGBA16, RGBA32, IA16, IA8, IA4, I8, I4, CI8, CI4, ONEBPP };

   class N64Graphics
   {
      private static int SCALE_5_8(int val)
      {
         return (val * 0xFF) / 0x1F;
      }

      private static byte SCALE_8_5(byte val)
      {
         return (byte)((((val) + 4) * 0x1F) / 0xFF);
      }

      private static byte SCALE_8_4(byte val)
      {
         return (byte)(val / 0x11);
      }

      private static byte SCALE_8_3(byte val)
      {
         return (byte)(val / 0x24);
      }

      public static Color RGBA16Color(byte c0, byte c1)
      {
         int r = SCALE_5_8((c0 & 0xF8) >> 3);
         int g = SCALE_5_8(((c0 & 0x07) << 2) | ((c1 & 0xC0) >> 6));
         int b = SCALE_5_8((c1 & 0x3E) >> 1);
         int a = ((c1 & 0x1) > 0) ? 255 : 0;
         return Color.FromArgb(a, r, g, b);
      }

      public static Color RGBA16Color(byte[] data, int pixOffset)
      {
         byte c0 = data[pixOffset];
         byte c1 = data[pixOffset + 1];

         return RGBA16Color(c0, c1);
      }

      public static Color RGBA32Color(byte[] data, int pixOffset)
      {
         int r, g, b, a;
         r = data[pixOffset];
         g = data[pixOffset + 1];
         b = data[pixOffset + 2];
         a = data[pixOffset + 3];
         return Color.FromArgb(a, r, g, b);
      }

      public static Color IA16Color(byte[] data, int pixOffset)
      {
         int i = data[pixOffset];
         int a = data[pixOffset + 1];
         return Color.FromArgb(a, i, i, i);
      }

      public static Color IA8Color(byte[] data, int pixOffset)
      {
         int i, a;
         byte c = data[pixOffset];
         i = (c >> 4) * 0x11;
         a = (c & 0xF) * 0x11;
         return Color.FromArgb(a, i, i, i);
      }

      public static Color IA4Color(byte[] data, int pixOffset, int nibble)
      {
         int shift = (1 - nibble) * 4;
         int i, a;
         int val = (data[pixOffset] >> shift) & 0xF;
         i = (val >> 1) * 0x24;
         a = (val & 0x1) > 0 ? 0xFF : 0x00;
         return Color.FromArgb(a, i, i, i);
      }

      public static Color I8Color(byte[] data, int pixOffset)
      {
         int ia = data[pixOffset];
         return Color.FromArgb(ia, ia, ia, ia);
      }

      public static Color I4Color(byte[] data, int pixOffset, int nibble)
      {
         int shift = (1 - nibble) * 4;
         int ia = (data[pixOffset] >> shift) & 0xF;
         ia *= 0x11;
         return Color.FromArgb(ia, ia, ia, ia);
      }

      public static Color CI8Color(byte[] data, byte[] palette, int pixOffset)
      {
         byte c0, c1;
         int palOffset = 2 * data[pixOffset];
         c0 = palette[palOffset];
         c1 = palette[palOffset + 1];

         return RGBA16Color(c0, c1);
      }

      public static Color CI4Color(byte[] data, byte[] palette, int pixOffset, int nibble)
      {
         byte c0, c1;
         int shift = (1 - nibble) * 4;
         int palOffset = 2 * ((data[pixOffset] >> shift) & 0xF);
         c0 = palette[palOffset];
         c1 = palette[palOffset + 1];

         return RGBA16Color(c0, c1);
      }

      public static Color BPPColor(byte[] data, int pixOffset, int bit)
      {
         int i, a;
         int val = (data[pixOffset] >> bit) & 0x1;
         i = a = 0xFF * val;
         return Color.FromArgb(a, i, i, i);
      }

      // return number of bytes needed to encode numPixels using codec
      public static int PixelsToBytes(N64Codec codec, int numPixels)
      {
         int numBytes = 0;
         switch (codec)
         {
            case N64Codec.RGBA16: numBytes = numPixels * 2; break;
            case N64Codec.RGBA32: numBytes = numPixels * 4; break;
            case N64Codec.IA16:   numBytes = numPixels * 2; break;
            case N64Codec.IA8:    numBytes = numPixels;  break;
            case N64Codec.IA4:    numBytes = numPixels / 2; break;
            case N64Codec.I8:     numBytes = numPixels;  break;
            case N64Codec.I4:     numBytes = numPixels / 2; break;
            case N64Codec.CI8:    numBytes = numPixels;  break;
            case N64Codec.CI4:    numBytes = numPixels / 2; break;
            case N64Codec.ONEBPP: numBytes = numPixels / 8; break;
         }
         return numBytes;
      }

      public static string CodecString(N64Codec codec)
      {
         switch (codec)
         {
            case N64Codec.RGBA16: return "rgba16";
            case N64Codec.RGBA32: return "rgba32";
            case N64Codec.IA16: return "ia16";
            case N64Codec.IA8: return "ia8";
            case N64Codec.IA4: return "ia4";
            case N64Codec.I8: return "i8";
            case N64Codec.I4: return "i4";
            case N64Codec.CI8: return "ci8";
            case N64Codec.CI4: return "ci4";
            case N64Codec.ONEBPP: return "1bpp";
         }
         return "unk";
      }

      public static Color MakeColor(byte[] data, byte[] palette, int offset, int select, N64Codec codec)
      {
         Color color;
         switch (codec)
         {
            case N64Codec.RGBA16:
               color = RGBA16Color(data, offset);
               break;
            case N64Codec.RGBA32:
               color = RGBA32Color(data, offset);
               break;
            case N64Codec.IA16:
               color = IA16Color(data, offset);
               break;
            case N64Codec.IA8:
               color = IA8Color(data, offset);
               break;
            case N64Codec.IA4:
               color = IA4Color(data, offset, select);
               break;
            case N64Codec.I8:
               color = I8Color(data, offset);
               break;
            case N64Codec.I4:
               color = I4Color(data, offset, select);
               break;
            case N64Codec.CI8:
               color = CI8Color(data, palette, offset);
               break;
            case N64Codec.CI4:
               color = CI4Color(data, palette, offset, select);
               break;
            case N64Codec.ONEBPP:
               color = BPPColor(data, offset, select);
               break;
            default:
               color = RGBA16Color(data, offset);
               break;
         }
         return color;
      }

      public static void RenderTexture(Graphics g, byte[] data, byte[] palette, int offset, int width, int height, int scale, N64Codec codec)
      {
         Brush brush;
         for (int h = 0; h < height; h++)
         {
            for (int w = 0; w < width; w++)
            {
               int pixOffset = (h * width + w);
               int bytesPerPix = 1;
               int select = 0;
               switch (codec)
               {
                  case N64Codec.RGBA16: bytesPerPix = 2; pixOffset *= bytesPerPix; break;
                  case N64Codec.RGBA32: bytesPerPix = 4; pixOffset *= bytesPerPix; break;
                  case N64Codec.IA16: bytesPerPix = 2; pixOffset *= bytesPerPix; break;
                  case N64Codec.IA8: break;
                  case N64Codec.IA4:
                     select = pixOffset & 0x1;
                     pixOffset /= 2;
                     break;
                  case N64Codec.I8: break;
                  case N64Codec.I4:
                  case N64Codec.CI4:
                     select = pixOffset & 0x1;
                     pixOffset /= 2;
                     break;
                  case N64Codec.CI8: break;
                  case N64Codec.ONEBPP:
                     select = pixOffset & 0x7;
                     pixOffset /= 8;
                     break;
               }
               pixOffset += offset;
               if (data.Length > pixOffset + bytesPerPix - 1)
               {
                  brush = new SolidBrush(MakeColor(data, palette, pixOffset, select, codec));
                  g.FillRectangle(brush, w * scale, h * scale, scale, scale);
               }
            }
         }
      }

      // return palette index of matching c0/c1 16-bit dataset, or -1 if not found
      private static int paletteIndex(byte[] pal, int palCount, byte c0, byte c1)
      {
         for (int i = 0; i < palCount; i++)
         {
            if (pal[2 * i] == c0 && pal[2 * i + 1] == c1)
            {
               return i;
            }
         }
         return -1;
      }

      public static void Convert(ref byte[] imageData, ref byte[] paletteData, N64Codec codec, Bitmap bm)
      {
         int numPixels = bm.Width * bm.Height;
         imageData = new byte[PixelsToBytes(codec, numPixels)];
         int palCount = 0;
         switch (codec)
         {
            case N64Codec.RGBA16:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     byte r, g, b;
                     r = SCALE_8_5(col.R);
                     g = SCALE_8_5(col.G);
                     b = SCALE_8_5(col.B);
                     byte c0 = (byte)((r << 3) | (g >> 2));
                     byte c1 = (byte)(((g & 0x3) << 6) | (b << 1) | ((col.A > 0) ? 1 : 0));
                     int idx = 2 * (y * bm.Width + x);
                     imageData[idx + 0] = c0;
                     imageData[idx + 1] = c1;
                  }
               }
               break;
            case N64Codec.RGBA32:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     int idx = 4 * (y * bm.Width + x);
                     imageData[idx + 0] = col.R;
                     imageData[idx + 1] = col.G;
                     imageData[idx + 2] = col.B;
                     imageData[idx + 3] = col.A;
                  }
               }
               break;
            case N64Codec.IA16:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     int sum = col.R + col.G + col.B;
                     byte intensity = (byte)(sum / 3);
                     byte alpha = col.A;
                     int idx = 2 * (y * bm.Width + x);
                     imageData[idx + 0] = intensity;
                     imageData[idx + 1] = alpha;
                  }
               }
               break;
            case N64Codec.IA8:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     int sum = col.R + col.G + col.B;
                     byte intensity = SCALE_8_4((byte)(sum / 3));
                     byte alpha = SCALE_8_4(col.A);
                     int idx = y * bm.Width + x;
                     imageData[idx] = (byte)((intensity << 4) | alpha);
                  }
               }
               break;
            case N64Codec.IA4:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     int sum = col.R + col.G + col.B;
                     byte intensity = SCALE_8_3((byte)(sum / 3));
                     byte alpha = (byte)(col.A > 0 ? 1 : 0);
                     int idx = y * bm.Width + x;
                     byte old = imageData[idx / 2];
                     if ((idx % 2) > 0)
                     {
                        imageData[idx / 2] = (byte)((old & 0xF0) | (intensity << 1) | alpha);
                     }
                     else
                     {
                        imageData[idx / 2] = (byte)((old & 0x0F) | (((intensity << 1) | alpha) << 4));
                     }
                  }
               }
               break;
            case N64Codec.I8:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     int sum = col.R + col.G + col.B;
                     byte intensity = (byte)(sum / 3);
                     int idx = y * bm.Width + x;
                     imageData[idx] = intensity;
                  }
               }
               break;
            case N64Codec.I4:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     int sum = col.R + col.G + col.B;
                     byte intensity = SCALE_8_4((byte)(sum / 3));
                     int idx = y * bm.Width + x;
                     byte old = imageData[idx / 2];
                     if ((idx % 2) > 0)
                     {
                        imageData[idx / 2] = (byte)((old & 0xF0) | intensity);
                     }
                     else
                     {
                        imageData[idx / 2] = (byte)((old & 0x0F) | (intensity << 4));
                     }
                  }
               }
               break;
            case N64Codec.CI4:
               paletteData = new byte[16 * 2];
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     byte r, g, b;
                     r = SCALE_8_5(col.R);
                     g = SCALE_8_5(col.G);
                     b = SCALE_8_5(col.B);
                     byte c0 = (byte)((r << 3) | (g >> 2));
                     byte c1 = (byte)(((g & 0x3) << 6) | (b << 1) | ((col.A > 0) ? 1 : 0));
                     int idx = y * bm.Width + x;
                     int palIdx = paletteIndex(paletteData, palCount, c0, c1);
                     if (palIdx < 0)
                     {
                        if (palCount < paletteData.Length / 2)
                        {
                           palIdx = palCount;
                           paletteData[2 * palCount] = c0;
                           paletteData[2 * palCount + 1] = c1;
                           palCount++;
                        }
                        else
                        {
                           palIdx = 0;
                           // TODO: out of palette entries. error or pick closest?
                        }
                     }
                     byte old = imageData[idx / 2];
                     if ((idx % 2) > 0)
                     {
                        imageData[idx / 2] = (byte)((old & 0xF0) | (byte)palIdx);
                     }
                     else
                     {
                        imageData[idx / 2] = (byte)((old & 0x0F) | ((byte)palIdx << 4));
                     }
                  }
               }
               break;
            case N64Codec.CI8:
               paletteData = new byte[256 * 2];
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     byte r, g, b;
                     r = SCALE_8_5(col.R);
                     g = SCALE_8_5(col.G);
                     b = SCALE_8_5(col.B);
                     byte c0 = (byte)((r << 3) | (g >> 2));
                     byte c1 = (byte)(((g & 0x3) << 6) | (b << 1) | ((col.A > 0) ? 1 : 0));
                     int idx = y * bm.Width + x;
                     int palIdx = paletteIndex(paletteData, palCount, c0, c1);
                     if (palIdx < 0)
                     {
                        if (palCount < paletteData.Length / 2)
                        {
                           palIdx = palCount;
                           paletteData[2 * palCount] = c0;
                           paletteData[2 * palCount + 1] = c1;
                           palCount++;
                        }
                        else
                        {
                           palIdx = 0;
                           // TODO: out of palette entries. error or pick closest?
                        }
                     }
                     imageData[idx] = (byte)palIdx;
                  }
               }
               break;
            case N64Codec.ONEBPP:
               for (int y = 0; y < bm.Height; y++)
               {
                  for (int x = 0; x < bm.Width; x++)
                  {
                     Color col = bm.GetPixel(x, y);
                     int sum = col.R + col.G + col.B;
                     byte intensity = (sum > 0) ? (byte)1 : (byte)0;
                     int idx = y * bm.Width + x;
                     byte old = imageData[idx / 8];
                     int bit = idx % 8;
                     int mask = ~(1 << bit);
                     imageData[idx / 8] = (byte)((old & mask) | (intensity << bit));
                  }
               }
               break;
         }
      }
   }
}
