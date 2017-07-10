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
      // opened file data
      private string savePath = null;
      private byte[] romData;
      private bool fileDataChanged = false;
      private int offset = 0;
      private string basename;

      // external palette data
      private string savePalettePath = null;
      private byte[] extPaletteData;
      private bool extPaletteChanged = false;

      // raw palette data (either reference to romData or external palette)
      private byte[] paletteData;
      // merged paletteData and split data
      private byte[] curPalette;
      private bool separatePalette = false;

      private N64Codec viewerCodec = N64Codec.RGBA16;

      // drag and drop data
      private string lastFilename;
      private bool validDragData;

      // list of viewers to update
      private List<GraphicsViewer> viewers = new List<GraphicsViewer>();

      // graphics viewer that is being hovered over for mouse wheel hooking
      private GraphicsViewer hoverGV = null;

      // graphics viewer that was right-clicked to bring up context menu
      private GraphicsViewer rightClickGV = null;

      // graphics viewer that was mouse down event occurred to detect mouse click
      private GraphicsViewer clickedGV = null;

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

      private bool pasteImage()
      {
         if (Clipboard.ContainsImage())
         {
            Bitmap bm = Clipboard.GetImage() as Bitmap;
            if (bm != null)
            {
               insertImage(bm);
               return true;
            }
         }
         return false;
      }

      protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
      {
         switch (keyData)
         {
            case (Keys.Control | Keys.B):
               bgColorButton.PerformClick();
               return true;
            case (Keys.Control | Keys.I):
               toolStripInsert.PerformClick();
               return true;
            case (Keys.Control | Keys.O):
               toolStripOpen.PerformClick();
               return true;
            case (Keys.Control | Keys.S):
               toolStripSave.PerformClick();
               return true;
            case (Keys.Control | Keys.V):
               // let Ctrl-V pass through if no image data
               if (pasteImage())
               {
                  return true;
               }
               break;
            case (Keys.Shift | Keys.F1):
               new TestForm().ShowDialog();
               return true;
            case (Keys.F1):
               toolStripAbout.PerformClick();
               return true;
         }
         return base.ProcessCmdKey(ref msg, keyData);
      }

      private void ImageForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (NeedsSaveCancel())
         {
            e.Cancel = true;
         }
      }

      private void SaveFiles()
      {
         if (fileDataChanged)
         {
            SaveBinFile(savePath, romData, 0, romData.Length);
         }
         if (extPaletteChanged)
         {
            SaveBinFile(savePalettePath, extPaletteData, 0, extPaletteData.Length);
         }
         toolStripSave.Enabled = false;
      }

      // returns true if save confirmation is canceled
      private bool NeedsSaveCancel()
      {
         if (extPaletteChanged || fileDataChanged)
         {
            string filename = Path.GetFileName(savePath);
            string message = String.Format("Save file \"{0}\" ?", filename);
            DialogResult result = MessageBox.Show(message, "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (result)
            {
               case DialogResult.Yes:
                  SaveFiles();
                  break;
               case DialogResult.Cancel:
                  return true;
            }
         }
         return false;
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
         numericOffset.Value = 0;
         if (!separatePalette)
         {
            paletteData = romData;
            numericPalette.Enabled = true;
            numericPalette.Maximum = paletteData.Length;
            numericSplitOffset.Maximum = paletteData.Length;
            numericPalette.Value = 0;
            numericSplitOffset.Value = 0;
         }
         foreach (GraphicsViewer gv in viewers)
         {
            gv.SetData(romData);
            gv.Invalidate();
         }
         UpdatePalette();
         statusStripFile.Text = String.Format("{0} [0x{1:X}]", filePath, romData.Length);
         statusStripFile.ForeColor = Color.Black;
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
         if (!NeedsSaveCancel())
         {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "All Files (*.*)|*.*|Common ROMs (.*64)|*.*64";
            ofd.FilterIndex = 1;

            DialogResult dresult = ofd.ShowDialog();

            if (dresult == DialogResult.OK)
            {
               readData(ofd.FileName);
               toolStripInsert.Enabled = true;
               fileDataChanged = false;
               toolStripSave.Enabled = false;
               foreach (GraphicsViewer gv in viewers)
               {
                  gv.Enabled = true;
               }
               gviewPalette.Enabled = true;
            }
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
            Bitmap bm = new Bitmap(ofd.FileName);
            insertImage(bm);
            toolStripSave.Enabled = true;
         }
      }

      private void toolStripSave_Click(object sender, EventArgs e)
      {
         SaveFiles();
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
                  gviewPalette.PixScale = 8;
                  break;
               case N64Codec.CI4:
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
               gv.Size = new System.Drawing.Size(pixScale * gv.GetPixelWidth(), pixScale * gv.GetPixelHeight());
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
         graphicsViewerCustom.PixSize = new Size((int)numericUpDownWidth.Value, graphicsViewerCustom.PixSize.Height);
         graphicsViewerCustom.Refresh();
      }

      private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
      {
         graphicsViewerCustom.PixSize = new Size(graphicsViewerCustom.PixSize.Width, (int)numericUpDownHeight.Value);
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

      private void checkExtPalette_CheckedChanged(object sender, EventArgs e)
      {
         separatePalette = checkExtPalette.Checked;
         loadPaletteButton.Enabled = separatePalette;
         if (separatePalette)
         {
            paletteData = extPaletteData;
         }
         else
         {
            paletteData = romData;
         }
         if (paletteData != null)
         {
            if (numericPalette.Value >= paletteData.Length)
            {
               numericPalette.Value = paletteData.Length - 1;
            }
            if (numericSplitOffset.Value >= paletteData.Length)
            {
               numericSplitOffset.Value = paletteData.Length - 1;
            }
            numericPalette.Maximum = paletteData.Length - 1;
            numericSplitOffset.Maximum = paletteData.Length - 1;
         }
         UpdatePalette();
      }

      private void numericPalette_ValueChanged(object sender, EventArgs e)
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
         if (validDragData)
         {
            if (!NeedsSaveCancel())
            {
               readData(lastFilename);
            }
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
         validDragData = GetFilename(out filename, e);
         if (validDragData)
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
            byte[] tmpData;
            try
            {
               tmpData = System.IO.File.ReadAllBytes(ofd.FileName);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error reading " + ofd.FileName);
               statusStripFile.Text = "Error reading " + ofd.FileName;
               statusStripFile.ForeColor = Color.Red;
               return;
            }

            if (tmpData != null)
            {
               extPaletteData = tmpData;
               paletteData = tmpData;
               numericPalette.Enabled = true;
               numericPalette.Maximum = paletteData.Length;
               numericSplitOffset.Maximum = paletteData.Length;
               UpdatePalette();
               paletteFileLabel.Text = Path.GetFileName(ofd.FileName);
               savePalettePath = ofd.FileName;
               extPaletteChanged = false;
               numericPalette.Value = 0;
               numericSplitOffset.Value = 0;
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
            offsetSize = N64Graphics.PixelsToBytes(gv.Codec, gv.PixSize.Width * gv.PixSize.Height);
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
            int width = gv.GetPixelWidth();
            int height = gv.GetPixelHeight();
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            N64Graphics.RenderTexture(g, romData, curPalette, offset, width, height, 1, gv.Codec);
            switch (sfd.FilterIndex)
            {
               case 1: bitmap.Save(sfd.FileName, ImageFormat.Png); break;
               case 2: bitmap.Save(sfd.FileName, ImageFormat.Jpeg); break;
               case 3: bitmap.Save(sfd.FileName, ImageFormat.Bmp); break;
            }
         }
      }

      private void graphicsViewer_MouseDown(object sender, MouseEventArgs e)
      {
         clickedGV = (GraphicsViewer)sender;
      }

      private void graphicsViewer_MouseUp(object sender, MouseEventArgs e)
      {
         GraphicsViewer gv = (GraphicsViewer)sender;
         if (gv == clickedGV)
         {
            switch (e.Button)
            {
               case System.Windows.Forms.MouseButtons.Left:
                  int pixelAmount = (e.X + e.Y * gv.GetPixelWidth()) / gv.PixScale;
                  advanceOffset(gv, 1, pixelAmount);
                  break;
               case System.Windows.Forms.MouseButtons.Right:
                  rightClickGV = gv;
                  break;
            }
         }
         clickedGV = null;
      }

      private void graphicsViewer_MouseEnter(object sender, EventArgs e)
      {
         hoverGV = (GraphicsViewer)sender;
      }

      private void graphicsViewer_MouseLeave(object sender, EventArgs e)
      {
         hoverGV = null;
      }

      private void graphicsViewer_MouseMove(object sender, MouseEventArgs e)
      {
         if (romData != null)
         {
            GraphicsViewer gv = (GraphicsViewer)sender;
            int pixOffset = (e.Y / gv.PixScale) * gv.GetPixelWidth() + e.X / gv.PixScale;
            int byteOffset = N64Graphics.PixelsToBytes(gv.Codec, pixOffset);
            int nibblesPerPix = 1;
            int select = 0;
            int selectBits = 0;
            switch (gv.Codec)
            {
               case N64Codec.RGBA32:
                  nibblesPerPix = 8;
                  pixOffset *= nibblesPerPix / 2;
                  break;
               case N64Codec.RGBA16:
               case N64Codec.IA16:
                  nibblesPerPix = 4;
                  pixOffset *= nibblesPerPix / 2;
                  break;
               case N64Codec.IA8:
               case N64Codec.I8:
               case N64Codec.CI8:
                  nibblesPerPix = 2;
                  break;
               case N64Codec.IA4:
               case N64Codec.I4:
               case N64Codec.CI4:
                  selectBits = 4;
                  select = pixOffset & 0x1;
                  pixOffset /= 2;
                  break;
               case N64Codec.ONEBPP:
                  selectBits = 1;
                  select = pixOffset & 0x7;
                  pixOffset /= 8;
                  break;
            }

            byte[] colorData;
            if (gv == gviewPalette)
            {
               colorData = curPalette;
            }
            else
            {
               byteOffset += offset;
               colorData = romData;
            }
            if (byteOffset >= 0 && (byteOffset + nibblesPerPix / 2) <= colorData.Length)
            {
               Color c = N64Graphics.MakeColor(colorData, curPalette, byteOffset, select, gv.Codec);
               int value = 0;
               for (int i = 0; i < (nibblesPerPix + 1) / 2; i++)
               {
                  value = (value << 8) | colorData[byteOffset + i];
               }
               switch (selectBits)
               {
                  case 4: value = (value >> ((1 - select) * 4)) & 0xf; break;
                  case 1: value = (value >> select) & 0x1; break;
               }
               string hexFormat = String.Format("Hex: {{0:X0{0}}}", nibblesPerPix);
               pictureBoxColor.BackColor = c;
               labelColorHex.Text = String.Format(hexFormat, value);
               labelColorR.Text = String.Format("R: {0}", c.R);
               labelColorG.Text = String.Format("G: {0}", c.G);
               labelColorB.Text = String.Format("B: {0}", c.B);
               labelColorA.Text = String.Format("A: {0}", c.A);
            }
            else
            {
               pictureBoxColor.BackColor = Color.Empty;
               labelColorHex.Text = "Hex:";
               labelColorR.Text = "R:";
               labelColorG.Text = "G:";
               labelColorB.Text = "B:";
               labelColorA.Text = "A:";
            }
         }
      }

      // this event is actually called for the entire Form and uses the hover state to determine how much to scroll
      private void graphicsViewer_MouseWheel(object sender, MouseEventArgs e)
      {
         if (hoverGV != null)
         {
            int pixelAmount = 0;
            int rowAmount = 4;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
               rowAmount = 0;
               pixelAmount = 1;
            }
            else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
               rowAmount = 1;
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
               rowAmount = hoverGV.GetPixelHeight();
            }
            int direction = -Math.Sign(e.Delta);
            pixelAmount += rowAmount * hoverGV.GetPixelWidth();
            advanceOffset(hoverGV, direction, pixelAmount);
         }
      }

      private void gviewPalette_MouseUp(object sender, MouseEventArgs e)
      {
         GraphicsViewer gv = (GraphicsViewer)sender;
         if (gv == clickedGV)
         {
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
                  rightClickGV = gv;
                  break;
            }
         }
         clickedGV = null;
      }

      private void insertImage(Bitmap bm)
      {
         if (romData != null)
         {
            byte[] imageData = null, paletteData = null;

            N64Graphics.Convert(ref imageData, ref paletteData, viewerCodec, bm);

            int copyLength = Math.Min(imageData.Length, romData.Length - offset);
            Array.Copy(imageData, 0, romData, offset, copyLength);
            fileDataChanged = true;

            if (paletteData != null)
            {
               byte[] paletteDest;
               if (separatePalette && extPaletteData != null)
               {
                  extPaletteChanged = true;
                  paletteDest = extPaletteData;
               }
               else
               {
                  paletteDest = romData;
               }
               int palOffset = (int)numericPalette.Value;
               copyLength = Math.Min(paletteData.Length, paletteDest.Length - palOffset);
               if (copyLength > 0)
               {
                  Array.Copy(paletteData, 0, paletteDest, palOffset, copyLength);
                  UpdatePalette();
               }
            }

            foreach (GraphicsViewer gv in viewers)
            {
               gv.Invalidate();
            }
         }
      }

      private void gvExport_Click(object sender, EventArgs e)
      {
         if (rightClickGV != null)
         {
            exportTexture(rightClickGV);
         }
      }

      private void gvCopy_Click(object sender, EventArgs e)
      {
         if (rightClickGV != null)
         {
            int width = rightClickGV.GetPixelWidth();
            int height = rightClickGV.GetPixelHeight();
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            N64Graphics.RenderTexture(g, romData, curPalette, offset, width, height, 1, rightClickGV.Codec);
            Clipboard.SetImage(bitmap);
         }
      }

      private void gvSetPaletteBefore_Click(object sender, EventArgs e)
      {
         int paletteBytes = N64Graphics.PixelsToBytes(gviewPalette.Codec, gviewPalette.PixSize.Width * gviewPalette.PixSize.Height);
         setPaletteOffset(offset - paletteBytes);
         checkExtPalette.Checked = false;
      }

      private void gvSetPaletteAfter_Click(object sender, EventArgs e)
      {
         int paletteBytes = N64Graphics.PixelsToBytes(rightClickGV.Codec, rightClickGV.PixSize.Width * rightClickGV.PixSize.Height);
         setPaletteOffset(offset + paletteBytes);
         checkExtPalette.Checked = false;
      }

   }
}
