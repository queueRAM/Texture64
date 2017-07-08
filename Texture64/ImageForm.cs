using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Texture64
{
   public partial class ImageForm : Form
   {
      private string savePath = null;
      private byte[] romData;
      private byte[] paletteData;
      private byte[] curPalette;
      private int offset = 0;
      private N64Codec viewerCodec = N64Codec.RGBA16;
      private bool separatePalette = false;
      private bool validData;
      private string basename;
      private string lastFilename;
      private List<GraphicsViewer> viewers = new List<GraphicsViewer>();

      public ImageForm()
      {
         InitializeComponent();
         viewers.Add(graphicsViewerMap);
         viewers.Add(graphicsViewer8x8);
         viewers.Add(graphicsViewer8x16);
         viewers.Add(graphicsViewer16x16);
         viewers.Add(graphicsViewer16x32);
         viewers.Add(graphicsViewer32x32);
         viewers.Add(graphicsViewer32x64);
         viewers.Add(graphicsViewer64x32);
         viewers.Add(graphicsViewer64x64);
         viewers.Add(graphicsViewerCustom);

         this.MouseWheel += new MouseEventHandler(this.graphicsViewer_MouseWheel);

         gviewPalette.Codec = N64Codec.RGBA16;

         toolStripCodec.SelectedIndex = 0;
         toolStripScale.SelectedIndex = 1;

         // bind the value of the scrollbar to the value of the offset box
         numericOffset.DataBindings.Add("Value", vScrollBarOffset, "Value", false, DataSourceUpdateMode.OnPropertyChanged);

         // handle arguments passed in the command line
         string[] args = Environment.GetCommandLineArgs();
         if (args.Length > 1)
         {
            readData(args[1]);
         }
      }

      private static bool SaveBinFile(string filePath, byte[] data, int start, int end)
      {
         try
         {
            FileStream outStream = File.OpenWrite(filePath);
            outStream.Write(data, start, end - start);
            outStream.Close();
            return true;
         }
         catch
         {
            return false;
         }
      }

      private void readData(String filePath)
      {
         basename = Path.GetFileNameWithoutExtension(filePath);
         basename = basename.TrimStart(new char[] { '0' });
         try
         {
            romData = System.IO.File.ReadAllBytes(filePath);
         }
         catch (Exception e)
         {
            MessageBox.Show(e.Message, "Error reading " + filePath);
            statusStripFile.Text = "Error reading " + filePath;
            statusStripFile.ForeColor = Color.Red;
            return;
         }
         savePath = filePath;
         numericOffset.Maximum = romData.Length - 1;
         vScrollBarOffset.Maximum = romData.Length - 1;
         if (!separatePalette)
         {
            paletteData = romData;
            numericPalette.Enabled = true;
            numericPalette.Maximum = paletteData.Length;
            numericSplitOffset.Maximum = paletteData.Length;
         }
         foreach (GraphicsViewer gv in viewers)
         {
            gv.SetData(romData);
            gv.Invalidate();
         }
         UpdatePalette();
         statusStripFile.Text = filePath;
         statusStripFile.ForeColor = Color.Black;
         statusStripLength.Text = String.Format("0x{0:X}", romData.Length);
         numericOffset.Enabled = true;
         vScrollBarOffset.Enabled = true;
      }

      private void UpdatePalette()
      {
         int palOffset = (int)numericPalette.Value;
         if (paletteData != null && palOffset < paletteData.Length)
         {
            curPalette = new byte[512];
            int length = curPalette.Length;
            int splitLength = 0;
            if (splitPaletteCheck.Checked)
            {
               splitLength = (int)numericSplitLength.Value;
               int splitOffset = (int)numericSplitOffset.Value;
               if (splitLength + splitOffset > paletteData.Length)
               {
                  splitLength = paletteData.Length - splitOffset;
               }
               Array.Copy(paletteData, splitOffset, curPalette, curPalette.Length - splitLength, splitLength);
            }
            length -= splitLength;
            if (length + palOffset > paletteData.Length)
            {
               length = paletteData.Length - palOffset;
            }
            Array.Copy(paletteData, palOffset, curPalette, 0, length);
            foreach (GraphicsViewer gv in viewers)
            {
               gv.SetPalette(curPalette);
               gv.Invalidate();
            }
            gviewPalette.SetData(curPalette);
            gviewPalette.Invalidate();
         }
      }

      private void toolStripOpen_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();

         ofd.Filter = "All Files (*.*)|*.*|Common ROMs (.*64)|*.*64";
         ofd.FilterIndex = 1;

         DialogResult dresult = ofd.ShowDialog();

         if (dresult == DialogResult.OK)
         {
            readData(ofd.FileName);
            toolStripInsert.Enabled = true;
            toolStripSave.Enabled = true;
         }
      }

      private void toolStripInsert_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();

         ofd.Filter = "Image files (*.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
         ofd.FilterIndex = 1;

         DialogResult dresult = ofd.ShowDialog();

         if (dresult == DialogResult.OK)
         {
            insertImageFile(ofd.FileName);
         }
      }

      private void toolStripSave_Click(object sender, EventArgs e)
      {
         SaveBinFile(savePath, romData, 0, romData.Length);
      }

      private void toolStripCodec_SelectedIndexChanged(object sender, EventArgs e)
      {
         N64Codec prevCodec = viewerCodec;
         viewerCodec = N64Codec.RGBA16;
         switch (toolStripCodec.SelectedIndex)
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
         if (prevCodec != viewerCodec)
         {
            foreach (GraphicsViewer gv in viewers)
            {
               gv.Codec = viewerCodec;
               gv.Invalidate();
            }
            switch (viewerCodec)
            {
               case N64Codec.CI8:
                  gviewPalette.PixHeight = 16;
                  gviewPalette.PixWidth = 16;
                  gviewPalette.PixScale = 8;
                  break;
               case N64Codec.CI4:
                  gviewPalette.PixHeight = 4;
                  gviewPalette.PixWidth = 4;
                  gviewPalette.PixScale = 32;
                  break;
            }
            gviewPalette.Invalidate();
         }
      }

      private void toolStripScale_SelectedIndexChanged(object sender, EventArgs e)
      {
         foreach (GraphicsViewer gv in viewers)
         {
            if (gv != graphicsViewerMap)
            {
               int pixScale = (1 + toolStripScale.SelectedIndex);
               // TODO: GraphicsViewer should resize itself when pixScale is changed, but it doesn't work for some reason
               gv.Size = new System.Drawing.Size(pixScale * gv.PixWidth, pixScale * gv.PixHeight);
               gv.PixScale = pixScale;
               gv.Invalidate();
            }
         }
      }

      private void bgColorButton_Click(object sender, EventArgs e)
      {
         if (colorDialogBg.ShowDialog() == DialogResult.OK)
         {
            BackColor = colorDialogBg.Color;
         }
      }

      private void toolStripAbout_Click(object sender, EventArgs e)
      {
         if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
         {
            new TestForm().ShowDialog();
         }
         else
         {
            new AboutBox().ShowDialog();
         }
      }

      private void numericOffset_ValueChanged(object sender, EventArgs e)
      {
         if (romData != null)
         {
            if (numericOffset.Value < romData.Length)
            {
               offset = (int)numericOffset.Value;
               if (offset < romData.Length)
               {
                  foreach (GraphicsViewer gv in viewers)
                  {
                     gv.SetOffset(offset);
                  }
               }
            }
         }
      }

      private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
      {
         graphicsViewerCustom.PixWidth = (int)numericUpDownWidth.Value;
         graphicsViewerCustom.Refresh();
      }

      private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
      {
         graphicsViewerCustom.PixHeight = (int)numericUpDownHeight.Value;
         graphicsViewerCustom.Refresh();
      }

      private void splitPaletteCheck_CheckedChanged(object sender, EventArgs e)
      {
         bool splitEnable = splitPaletteCheck.Checked;
         numericSplitLength.Enabled = splitEnable;
         numericSplitOffset.Enabled = splitEnable;
         splitMinusButton.Enabled = splitEnable;
         splitPlusButton.Enabled = splitEnable;
         UpdatePalette();
      }

      private void numericTextPalette_ValueChanged(object sender, EventArgs e)
      {
         UpdatePalette();
      }

      private void numericSplitLength_ValueChanged(object sender, EventArgs e)
      {
         UpdatePalette();
      }

      private void numericSplitOffset_ValueChanged(object sender, EventArgs e)
      {
         UpdatePalette();
      }

      private void offsetSplit(int delta)
      {
         int offset = (int)numericSplitOffset.Value + delta;
         offset = Math.Max(0, Math.Min(paletteData.Length - Math.Abs(delta), offset));
         numericSplitOffset.Value = offset;
      }

      private void splitMinusButton_Click(object sender, EventArgs e)
      {
         int delta = (int)numericSplitLength.Value;
         offsetSplit(-delta);
      }

      private void splitPlusButton_Click(object sender, EventArgs e)
      {
         int delta = (int)numericSplitLength.Value;
         offsetSplit(delta);
      }

      private void ImageForm_DragDrop(object sender, DragEventArgs e)
      {
         if (validData)
         {
            readData(lastFilename);
         }
      }

      protected bool GetFilename(out string filename, DragEventArgs e)
      {
         bool ret = false;
         filename = String.Empty;

         if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
         {
            Array data = ((IDataObject)e.Data).GetData("FileName") as Array;
            if (data != null)
            {
               if ((data.Length == 1) && (data.GetValue(0) is String))
               {
                  filename = ((string[])data)[0];
                  string ext = Path.GetExtension(filename).ToLower();
                  ret = true;
               }
            }
         }
         return ret;
      }

      private void ImageForm_DragEnter(object sender, DragEventArgs e)
      {
         string filename;
         validData = GetFilename(out filename, e);
         if (validData)
         {
            if (lastFilename != filename)
            {
               lastFilename = filename;
            }
            e.Effect = DragDropEffects.Copy;
         }
         else
         {
            e.Effect = DragDropEffects.None;
         }
      }

      private void loadPaletteButton_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();

         ofd.Filter = "All Files (*.*)|*.*|Common ROMs (.*64)|*.*64";
         ofd.FilterIndex = 1;

         DialogResult dresult = ofd.ShowDialog();

         if (dresult == DialogResult.OK)
         {
            byte[] tmpData = System.IO.File.ReadAllBytes(ofd.FileName);
            if (tmpData != null)
            {
               paletteData = tmpData;
               numericPalette.Enabled = true;
               numericPalette.Maximum = paletteData.Length;
               numericSplitOffset.Maximum = paletteData.Length;
               separatePalette = true;
               UpdatePalette();
               paletteFileLabel.Text = Path.GetFileName(ofd.FileName);
            }
         }
      }

      private void advanceOffset(GraphicsViewer gv, int direction, int advancePixels)
      {
         if (romData != null)
         {
            int offsetSize;
            if (advancePixels == 0)
            {
               offsetSize = N64Graphics.PixelsToBytes(gv.Codec, gv.GetPixelWidth() * gv.GetPixelHeight());
            }
            else
            {
               offsetSize = N64Graphics.PixelsToBytes(gv.Codec, advancePixels);
            }
            int change = direction * offsetSize;
            int newOffset = Math.Max(0, Math.Min(romData.Length - 1, offset + change));
            numericOffset.Value = newOffset;
         }
      }

      private void setPaletteOffset(int palOffset)
      {
         if (paletteData != null)
         {
            int newOffset = Math.Max(0, Math.Min(paletteData.Length - 1, palOffset));
            numericPalette.Value = newOffset;
         }
      }

      private void advancePaletteOffset(GraphicsViewer gv, int direction, int advancePixels)
      {
         int offsetSize;
         if (advancePixels == 0)
         {
            offsetSize = N64Graphics.PixelsToBytes(gv.Codec, gv.PixWidth * gv.PixHeight);
         }
         else
         {
            offsetSize = N64Graphics.PixelsToBytes(gv.Codec, advancePixels);
         }
         int palOffset = (int)numericPalette.Value;
         int change = direction * offsetSize;
         setPaletteOffset(palOffset + change);
      }

      private void exportTexture(GraphicsViewer gv)
      {
         SaveFileDialog sfd = new SaveFileDialog();
         sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
         sfd.Title = "Save as Image File";
         sfd.FileName = String.Format("{0}.{1:X05}.{2}.png", basename, offset, N64Graphics.CodecString(gv.Codec));
         DialogResult dResult = sfd.ShowDialog();

         if (dResult == DialogResult.OK)
         {
            Bitmap bitmap = new Bitmap(gv.PixWidth, gv.PixHeight, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            N64Graphics.RenderTexture(g, romData, curPalette, offset, gv.PixWidth, gv.GetPixelHeight(), 1, gv.Codec);
            switch (sfd.FilterIndex)
            {
               case 1: bitmap.Save(sfd.FileName, ImageFormat.Png); break;
               case 2: bitmap.Save(sfd.FileName, ImageFormat.Jpeg); break;
               case 3: bitmap.Save(sfd.FileName, ImageFormat.Bmp); break;
            }
         }
      }

      GraphicsViewer rightClickGv = null;

      private void graphicsViewerMap_MouseUp(object sender, MouseEventArgs e)
      {
         GraphicsViewer gv = (GraphicsViewer)sender;
         switch (e.Button)
         {
            case System.Windows.Forms.MouseButtons.Left:
               int pixelAmount = (e.X + e.Y * gv.Width) / gv.PixScale;
               advanceOffset(gv, 1, pixelAmount);
               break;
            case System.Windows.Forms.MouseButtons.Right:
               rightClickGv = gv;
               break;
         }
      }

      private void graphicsViewer_MouseUp(object sender, MouseEventArgs e)
      {
         GraphicsViewer gv = (GraphicsViewer)sender;
         switch (e.Button)
         {
            case System.Windows.Forms.MouseButtons.Left:
               int direction = 1;
               int pixelAmount = 0;
               if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
               {
                  direction = -1;
               }
               if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
               {
                  pixelAmount = 1;
               }
               advanceOffset(gv, direction, pixelAmount);
               break;
            case System.Windows.Forms.MouseButtons.Right:
               rightClickGv = gv;
               break;
         }
      }

      private GraphicsViewer hoverGV = null;

      private void graphicsViewer_MouseEnter(object sender, EventArgs e)
      {
         hoverGV = (GraphicsViewer)sender;
      }

      private void graphicsViewer_MouseLeave(object sender, EventArgs e)
      {
         hoverGV = null;
      }

      // this event is actually called for the entire Form and uses the hover state to determine how much to scroll
      private void graphicsViewer_MouseWheel(object sender, MouseEventArgs e)
      {
         if (hoverGV != null)
         {
            int direction = -Math.Sign(e.Delta);
            int pixelAmount = 4 * hoverGV.GetPixelWidth();
            advanceOffset(hoverGV, direction, pixelAmount);
         }
      }

      private void gviewPalette_MouseUp(object sender, MouseEventArgs e)
      {
         GraphicsViewer gv = (GraphicsViewer)sender;
         switch (e.Button)
         {
            case System.Windows.Forms.MouseButtons.Left:
               int direction = 1;
               int pixelAmount = 0;
               if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
               {
                  direction = -1;
               }
               if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
               {
                  pixelAmount = 1;
               }
               advancePaletteOffset(gv, direction, pixelAmount);
               break;
            case System.Windows.Forms.MouseButtons.Right:
               rightClickGv = gv;
               break;
         }
      }

      private void insertImageFile(string imageFile)
      {
         Bitmap bm = new Bitmap(imageFile);
         byte[] imageData = null, paletteData = null;
         N64Graphics.Convert(ref imageData, ref paletteData, viewerCodec, bm);
         for (int i = 0; i < imageData.Length; i++)
         {
            romData[offset + i] = imageData[i];
         }
         foreach (GraphicsViewer gv in viewers)
         {
            gv.Invalidate();
         }
      }

      private void gvExport_Click(object sender, EventArgs e)
      {
         if (rightClickGv != null)
         {
            exportTexture(rightClickGv);
         }
      }

      private void gvSetPaletteBefore_Click(object sender, EventArgs e)
      {
         int paletteBytes = N64Graphics.PixelsToBytes(gviewPalette.Codec, gviewPalette.PixWidth * gviewPalette.PixHeight);
         setPaletteOffset(offset - paletteBytes);
      }

      private void gvSetPaletteAfter_Click(object sender, EventArgs e)
      {
         int paletteBytes = N64Graphics.PixelsToBytes(rightClickGv.Codec, rightClickGv.PixWidth * rightClickGv.PixHeight);
         setPaletteOffset(offset + paletteBytes);
      }

   }
}
