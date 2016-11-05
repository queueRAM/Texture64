using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Texture64
{
   public partial class GraphicsViewer : UserControl
   {
      private byte[] data;
      private byte[] palette;
      public int PixWidth { get; set; }
      public int PixHeight { get; set; }
      public int PixScale { get; set; }
      public string BaseFileName { get; set; }
      private int offset;
      public enum Codec { RGBA16, RGBA32, IA16, IA8, IA4, I8, I4, CI8, ONEBPP };
      private Codec codec;

      private static Color RGBA16Color(byte[] data, int pixOffset)
      {
         int r, g, b, a;
         byte c1 = data[pixOffset];
         byte c2 = data[pixOffset + 1];
         r = ((c1 >> 3) * 0xFF) / 0x1F;
         g = ((((c1 << 2) & 0x1C) | (c2 >> 6)) * 0xFF) / 0x1F;
         b = (((c2 >> 1) & 0x1F) * 0xFF) / 0x1F;
         a = ((c2 & 0x1) > 0) ? 255 : 0;
         return Color.FromArgb(a, r, g, b);
      }

      private static Color RGBA32Color(byte[] data, int pixOffset)
      {
         int r, g, b, a;
         r = data[pixOffset];
         g = data[pixOffset + 1];
         b = data[pixOffset + 2];
         a = data[pixOffset + 3];
         return Color.FromArgb(a, r, g, b);
      }

      private static Color IA16Color(byte[] data, int pixOffset)
      {
         int i = data[pixOffset];
         int a = data[pixOffset + 1];
         return Color.FromArgb(a, i, i, i);
      }

      private static Color IA8Color(byte[] data, int pixOffset)
      {
         int i, a;
         byte c = data[pixOffset];
         i = (c >> 4) * 0x11;
         a = (c & 0xF) * 0x11;
         return Color.FromArgb(a, i, i, i);
      }

      private static Color IA4Color(byte[] data, int pixOffset, int nibble)
      {
         int shift = (1 - nibble) * 4;
         int i, a;
         int val = (data[pixOffset] >> shift) & 0xF;
         i = (val >> 1) * 0x24;
         a = (val & 0x1) > 0 ? 0xFF : 0x00;
         return Color.FromArgb(a, i, i, i);
      }

      private static Color I8Color(byte[] data, int pixOffset)
      {
         int ia = data[pixOffset];
         return Color.FromArgb(ia, ia, ia, ia);
      }

      private static Color I4Color(byte[] data, int pixOffset, int nibble)
      {
         int shift = (1-nibble) * 4;
         int ia = (data[pixOffset] >> shift) & 0xF;
         ia *= 0x11;
         return Color.FromArgb(ia, ia, ia, ia);
      }

      private static Color CI8Color(byte[] data, byte[] palette, int pixOffset)
      {
         Color color;
         byte c1, c2;
         int r, g, b, a;
         int palOffset = 2*data[pixOffset];
         c1 = palette[palOffset];
         c2 = palette[palOffset + 1];

         r = ((c1 >> 3) * 0xFF) / 0x1F;
         g = ((((c1 << 2) & 0x1C) | (c2 >> 6)) * 0xFF) / 0x1F;
         b = (((c2 >> 1) & 0x1F) * 0xFF) / 0x1F;
         a = ((c2 & 0x1) > 0) ? 255 : 0;

         color = Color.FromArgb(a, r, g, b);

         return color;
      }

      private static Color BPPColor(byte[] data, int pixOffset, int bit)
      {
         int i, a;
         int val = (data[pixOffset] >> bit) & 0x1;
         i = a = 0xFF * val;
         return Color.FromArgb(a, i, i, i);
      }

      public GraphicsViewer()
      {
         InitializeComponent();
         DoubleBuffered = true;
         PixHeight = 16;
         PixWidth = 16;
         PixScale = 2;
      }

      public void SetData(byte[] rawData)
      {
         data = rawData;
      }

      public void SetPalette(byte[] rawPalette)
      {
         palette = rawPalette;
      }

      public void SetOffset(int offset)
      {
         if (this.offset != offset)
         {
            this.offset = offset;
            Invalidate();
         }
      }

      public void SetCodec(Codec codec)
      {
         this.codec = codec;
      }

      private void GraphicsViewer_Paint(object sender, PaintEventArgs e)
      {
         renderTexture(e.Graphics, PixScale);
         e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, PixWidth * PixScale - 1, PixHeight * PixScale - 1);
      }

      private void renderTexture(Graphics g, int scale)
      {
         Brush brush;
         if (data != null)
         {
            for (int h = 0; h < PixHeight; h++)
            {
               for (int w = 0; w < PixWidth; w++)
               {
                  int pixOffset = (h * PixWidth + w);
                  int bytesPerPix = 1;
                  int select = 0;
                  switch (codec)
                  {
                     case Codec.RGBA16: bytesPerPix = 2; pixOffset *= bytesPerPix; break;
                     case Codec.RGBA32: bytesPerPix = 4; pixOffset *= bytesPerPix; break;
                     case Codec.IA16: bytesPerPix = 2; pixOffset *= bytesPerPix; break;
                     case Codec.IA8: break;
                     case Codec.IA4:
                        select = pixOffset & 0x1;
                        pixOffset /= 2;
                        break;
                     case Codec.I8: break;
                     case Codec.I4:
                        select = pixOffset & 0x1;
                        pixOffset /= 2;
                        break;
                     case Codec.CI8: break;
                     case Codec.ONEBPP:
                        select = pixOffset & 0x7;
                        pixOffset /= 8;
                        break;
                  }
                  pixOffset += offset;
                  if (data.Length > pixOffset + bytesPerPix - 1)
                  {
                     switch (codec)
                     {
                        case Codec.RGBA16:
                           brush = new SolidBrush(RGBA16Color(data, pixOffset));
                           break;
                        case Codec.RGBA32:
                           brush = new SolidBrush(RGBA32Color(data, pixOffset));
                           break;
                        case Codec.IA16:
                           brush = new SolidBrush(IA16Color(data, pixOffset));
                           break;
                        case Codec.IA8:
                           brush = new SolidBrush(IA8Color(data, pixOffset));
                           break;
                        case Codec.IA4:
                           brush = new SolidBrush(IA4Color(data, pixOffset, select));
                           break;
                        case Codec.I8:
                           brush = new SolidBrush(I8Color(data, pixOffset));
                           break;
                        case Codec.I4:
                           brush = new SolidBrush(I4Color(data, pixOffset, select));
                           break;
                        case Codec.CI8:
                           brush = new SolidBrush(CI8Color(data, palette, pixOffset));
                           break;
                        case Codec.ONEBPP:
                           brush = new SolidBrush(BPPColor(data, pixOffset, select));
                           break;
                        default:
                           brush = new SolidBrush(RGBA16Color(data, pixOffset));
                           break;
                     }
                     g.FillRectangle(brush, w * scale, h * scale, scale, scale);
                  }
               }
            }
         }
      }

      private string codecString(Codec codec)
      {
         switch(codec)
         {
            case Codec.RGBA16: return "rgba16";
            case Codec.RGBA32: return "rgba32";
            case Codec.IA16: return "ia16";
            case Codec.IA8: return "ia8";
            case Codec.IA4: return "ia4";
            case Codec.I8: return "i8";
            case Codec.I4: return "i4";
            case Codec.CI8: return "ci8";
            case Codec.ONEBPP: return "1bpp";
         }
         return "unk";
      }

      private void GraphicsViewer_Click(object sender, EventArgs e)
      {
         SaveFileDialog sfd = new SaveFileDialog();
         sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
         sfd.Title = "Save as Image File";
         sfd.FileName = String.Format("{0}.{1:X05}.{2}.png", BaseFileName, offset, codecString(codec));
         DialogResult dResult = sfd.ShowDialog();

         if (dResult == DialogResult.OK)
         {
            Bitmap bitmap = new Bitmap(PixWidth, PixHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            renderTexture(g, 1);
            switch (sfd.FilterIndex)
            {
               case 1: bitmap.Save(sfd.FileName, ImageFormat.Png); break;
               case 2: bitmap.Save(sfd.FileName, ImageFormat.Jpeg); break;
               case 3: bitmap.Save(sfd.FileName, ImageFormat.Bmp); break;
            }
         }
      }
   }
}
