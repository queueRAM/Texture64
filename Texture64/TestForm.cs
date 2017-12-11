using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Texture64
{
   public partial class TestForm : Form
   {
      private static int NUM_TESTS = 10;
      PictureBox[] picBoxes = new PictureBox[2 * NUM_TESTS];
      public TestForm()
      {
         InitializeComponent();
         comboBoxTest.SelectedIndex = 0;
         for (int i = 0; i < NUM_TESTS; i++)
         {
            picBoxes[2 * i + 0] = new PictureBox();
            picBoxes[2 * i + 1] = new PictureBox();
            picBoxes[2 * i + 0].Location = new Point(12 + 38 * i, 37);
            picBoxes[2 * i + 1].Location = new Point(12 + 38 * i, 75);
            picBoxes[2 * i + 0].Size = new Size(32, 32);
            picBoxes[2 * i + 1].Size = new Size(32, 32);
            this.Controls.Add(picBoxes[2 * i + 0]);
            this.Controls.Add(picBoxes[2 * i + 1]);
         }
      }

      private static N64Codec WhichCodec(int idx)
      {
         N64Codec viewerCodec = N64Codec.RGBA16;
         switch (idx)
         {
            case 0: viewerCodec = N64Codec.RGBA16; break;
            case 1: viewerCodec = N64Codec.RGBA32; break;
            case 2: viewerCodec = N64Codec.IA16; break;
            case 3: viewerCodec = N64Codec.IA8; break;
            case 4: viewerCodec = N64Codec.IA4; break;
            case 5: viewerCodec = N64Codec.I8; break;
            case 6: viewerCodec = N64Codec.I4; break;
            case 7: viewerCodec = N64Codec.CI8; break;
            case 8: viewerCodec = N64Codec.CI4; break;
            case 9: viewerCodec = N64Codec.ONEBPP; break;
         }
         return viewerCodec;
      }

      private void runTest(int idx, bool verbose, PictureBox pbIn, PictureBox pbOut)
      {
         int width = 32;
         int height = 32;
         byte[] test = new byte[width * height * 4]; // worst case
         byte[] result = null;
         byte[] pal = new byte[16 * 16 * 2];
         byte[] resultPal = null;
         int errorCount = 0;

         StringBuilder sb = new StringBuilder();

         N64Codec testCodec = WhichCodec(idx);

         // randomize inputs
         Random rng = new Random();
         rng.NextBytes(test);
         rng.NextBytes(pal);

         // if alpha bit clear, clear all bits
         switch (testCodec)
         {
            case N64Codec.RGBA16:
               for (int i = 0; i < test.Length; i += 2)
               {
                  if ((test[i+1] & 0x1) == 0)
                  {
                     test[i] = 0;
                     test[i + 1] = 0;
                  }
               }
               break;
            case N64Codec.IA16:
               for (int i = 0; i < test.Length; i += 2)
               {
                  if ((test[i + 1]) == 0)
                  {
                     test[i] = 0;
                  }
               }
               break;
            case N64Codec.IA8:
               for (int i = 0; i < test.Length; i++)
               {
                  if ((test[i] & 0x0F) == 0)
                  {
                     test[i] = 0;
                  }
               }
               break;
            case N64Codec.IA4:
               for (int i = 0; i < test.Length; i++)
               {
                  if ((test[i] & 0x10) == 0)
                  {
                     test[i] &= 0x0F;
                  }
                  if ((test[i] & 0x01) == 0)
                  {
                     test[i] &= 0xF0;
                  }
               }
               break;
            case N64Codec.CI4:
            case N64Codec.CI8:
               for (int i = 0; i < pal.Length; i += 2)
               {
                  if ((pal[i + 1] & 0x1) == 0)
                  {
                     pal[i] = 0;
                     pal[i + 1] = 0;
                  }
               }
               break;
         }

         Bitmap b = new Bitmap(width, height, PixelFormat.Format32bppArgb);
         Graphics g = Graphics.FromImage(b);
         
         N64Graphics.RenderTexture(g, test, pal, 0, width, height, 1, testCodec, N64IMode.AlphaCopyIntensity);
         N64Graphics.Convert(ref result, ref resultPal, testCodec, b);
         Bitmap bres = new Bitmap(width, height, PixelFormat.Format32bppArgb);
         N64Graphics.RenderTexture(Graphics.FromImage(bres), result, resultPal, 0, width, height, 1, testCodec, N64IMode.AlphaCopyIntensity);
         for (int i = 0; i < result.Length; i++)
         {
            if (test[i] != result[i])
            {
               errorCount++;
            }
            if (verbose)
            {
               sb.AppendFormat("{0:X03} {1:X02} {2:X02}\r\n", i, test[i], result[i]);
            }
         }
         if (resultPal != null)
         {
            for (int i = 0; i < resultPal.Length; i++)
            {
               if (pal[i] != resultPal[i])
               {
                  errorCount++;
               }
               if (verbose)
               {
                  sb.AppendFormat("P {0:X03} {1:X02} {2:X02}\r\n", i, pal[i], resultPal[i]);
               }
            }
         }

         // publish results
         sb.AppendFormat("Test[{0}] {1}: {2} errors\r\n", idx, N64Graphics.CodecString(testCodec), errorCount);
         textBoxLog.AppendText(sb.ToString());
         pbIn.Image = b;
         pbOut.Image = bres;
      }

      private void buttonRun_Click(object sender, EventArgs e)
      {
         
         textBoxLog.Clear();
         if (comboBoxTest.SelectedIndex == 0)
         {
            for (int i = 0; i < NUM_TESTS; i++)
            {
               runTest(i, false, picBoxes[2 * i], picBoxes[2 * i + 1]);
            }
         }
         else
         {
            for (int i = 0; i < 2 * NUM_TESTS; i++)
            {
               picBoxes[i].Image = null;
               picBoxes[i].Refresh();
            }
            runTest(comboBoxTest.SelectedIndex - 1, true, picBoxes[0], picBoxes[1]);
         }
      }
   }
}
