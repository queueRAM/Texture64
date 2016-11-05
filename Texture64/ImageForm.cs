using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Texture64
{
   public partial class ImageForm : Form
   {
      private byte[] romData;
      private byte[] paletteData;
      private bool separatePalette = false;
      private bool validData;
      private string lastFilename;
      private List<GraphicsViewer> viewers = new List<GraphicsViewer>();
      public ImageForm()
      {
         InitializeComponent();
         viewers.Add(graphicsViewerMap);
         viewers.Add(graphicsViewer8);
         viewers.Add(graphicsViewer16);
         viewers.Add(graphicsViewer32);
         viewers.Add(graphicsViewer64);
         viewers.Add(graphicsViewerCustom);

         gviewPalette.SetCodec(GraphicsViewer.Codec.RGBA16);

         resizeMap();

         toolStripCodec.SelectedIndex = 0;
      }

      private int hexToInt(string hex)
      {
         int offset = 0;
         if (!String.IsNullOrWhiteSpace(hex))
         {
            try
            {
               offset = Convert.ToInt32(hex, 16);
            }
            catch (OverflowException)
            {
               offset = 0;
            }
            catch (FormatException)
            {
               offset = 0;
            }
         }
         return offset;
      }

      private void resizeMap()
      {
         graphicsViewerMap.PixWidth = graphicsViewerMap.Width / 2;
         graphicsViewerMap.PixHeight = graphicsViewerMap.Height / 2;
         graphicsViewerMap.Invalidate();
      }

      private void listFiles(String filePath)
      {
         string path = Path.GetDirectoryName(filePath);
         string file = Path.GetFileName(filePath);
         string[] dirList = Directory.GetFiles(path);
         fileListView.Items.Clear();
         foreach (string dirEntry in dirList)
         {
            string entryFile = Path.GetFileName(dirEntry);
            string extension = Path.GetExtension(entryFile);
            if (extension == ".bin")
            {
               ListViewItem lvi = new ListViewItem(entryFile);
               lvi.Tag = dirEntry;
               if (entryFile == file)
               {
                  lvi.Selected = true;
               }
               fileListView.Items.Add(lvi);
            }
         }
      }

      private void readData(String filePath)
      {
         string basename = Path.GetFileNameWithoutExtension(filePath);
         basename = basename.TrimStart(new char[] { '0' });
         romData = System.IO.File.ReadAllBytes(filePath);
         if (!separatePalette)
         {
            paletteData = romData;
         }
         foreach (GraphicsViewer gv in viewers)
         {
            gv.BaseFileName = basename;
            gv.SetData(romData);
            gv.Invalidate();
         }
         UpdatePalette();
         statusStripFile.Text = filePath;
         statusStripLength.Text = String.Format("0x{0:X}", romData.Length);
      }

      private void updateViewers()
      {
         int offset = hexToInt(textHex.Text);
         if (romData != null && offset < romData.Length)
         {
            foreach (GraphicsViewer gv in viewers)
            {
               gv.SetOffset(offset);
            }
         }
      }

      private void UpdatePalette()
      {
         int offset = hexToInt(textPalette.Text);
         if (paletteData != null && offset < paletteData.Length)
         {
            byte[] palette = new byte[512];
            int length = palette.Length;
            int splitLength = 0;
            if (splitPaletteCheck.Checked)
            {
               splitLength = hexToInt(splitLengthText.Text);
               int splitOffset = hexToInt(splitOffsetText.Text);
               if (splitLength + splitOffset > paletteData.Length)
               {
                  splitLength = paletteData.Length - splitOffset;
               }
               Array.Copy(paletteData, splitOffset, palette, palette.Length - splitLength, splitLength);
            }
            length -= splitLength;
            if (length + offset > paletteData.Length)
            {
               length = paletteData.Length - offset;
            }
            Array.Copy(paletteData, offset, palette, 0, length);
            foreach (GraphicsViewer gv in viewers)
            {
               gv.SetPalette(palette);
               gv.Invalidate();
            }
            gviewPalette.SetData(palette);
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
            listFiles(ofd.FileName);
         }
      }

      private void textHex_TextChanged(object sender, EventArgs e)
      {
         updateViewers();
      }

      private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
      {
         graphicsViewerCustom.PixWidth = (int)numericUpDownWidth.Value;
         graphicsViewerCustom.Invalidate();
      }

      private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
      {
         graphicsViewerCustom.PixHeight = (int)numericUpDownHeight.Value;
         graphicsViewerCustom.Invalidate();
      }

      private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
      {
         if (romData != null)
         {
            int offset = (vScrollBar1.Value * (romData.Length / vScrollBar1.Maximum)) & 0x7FFFFFF8;
            textHex.Text = String.Format("{0:X}", offset);
            updateViewers();
         }
      }

      private void toolStripCodec_SelectedIndexChanged(object sender, EventArgs e)
      {
         GraphicsViewer.Codec codec = GraphicsViewer.Codec.RGBA16;
         switch (toolStripCodec.SelectedIndex)
         {
            case 0: codec = GraphicsViewer.Codec.RGBA16; break;
            case 1: codec = GraphicsViewer.Codec.RGBA32; break;
            case 2: codec = GraphicsViewer.Codec.IA16; break;
            case 3: codec = GraphicsViewer.Codec.IA8; break;
            case 4: codec = GraphicsViewer.Codec.IA4; break;
            case 5: codec = GraphicsViewer.Codec.I8; break;
            case 6: codec = GraphicsViewer.Codec.I4; break;
            case 7: codec = GraphicsViewer.Codec.CI8; break;
            case 8: codec = GraphicsViewer.Codec.ONEBPP; break;
         }
         foreach (GraphicsViewer gv in viewers)
         {
            gv.SetCodec(codec);
            gv.Invalidate();
         }
      }

      private void ImageForm_ResizeEnd(object sender, EventArgs e)
      {
         resizeMap();
      }

      private int offsetSize()
      {
         int blockSize = (int)numericUpDownWidth.Value * (int)numericUpDownHeight.Value;
         switch (toolStripCodec.SelectedIndex)
         {
            case 0: blockSize *= 2; break;
            case 1: blockSize *= 4; break;
            case 2: blockSize *= 2; break;
            case 3: break;
            case 4: blockSize /= 2; break;
            case 5: break;
            case 6: blockSize /= 2; break;
            case 7: break;
            case 8: blockSize /= 8; break;
         }
         return blockSize;
      }

      private void offsetMinusButton_Click(object sender, EventArgs e)
      {
         int curOffset = hexToInt(textHex.Text);
         int change = offsetSize();
         curOffset = Math.Max(0, curOffset - change);
         textHex.Text = String.Format("{0:X}", curOffset);
      }

      private void offsetPlusButton_Click(object sender, EventArgs e)
      {
         int curOffset = hexToInt(textHex.Text);
         int change = offsetSize();
         curOffset = Math.Min(romData.Length - change, curOffset + change);
         textHex.Text = String.Format("{0:X}", curOffset);
      }

      private void palPlusButton_Click(object sender, EventArgs e)
      {
         int val = hexToInt(textPalette.Text);
         val = Math.Min(paletteData.Length - 0x200, val + 0x200);
         textPalette.Text = String.Format("{0:X}", val);
      }

      private void palMinusButton_Click(object sender, EventArgs e)
      {
         int val = hexToInt(textPalette.Text);
         val = Math.Max(0, val - 0x200);
         textPalette.Text = String.Format("{0:X}", val);
      }

      private void splitPaletteCheck_CheckedChanged(object sender, EventArgs e)
      {
         bool splitEnable = splitPaletteCheck.Checked;
         splitLengthText.Enabled = splitEnable;
         splitOffsetText.Enabled = splitEnable;
         splitMinusButton.Enabled = splitEnable;
         splitPlusButton.Enabled = splitEnable;
         UpdatePalette();
      }

      private void textPalette_TextChanged(object sender, EventArgs e)
      {
         UpdatePalette();
      }

      private void splitLengthText_TextChanged(object sender, EventArgs e)
      {
         UpdatePalette();
      }

      private void splitOffsetText_TextChanged(object sender, EventArgs e)
      {
         UpdatePalette();
      }

      private void bgColorButton_Click(object sender, EventArgs e)
      {
         if (colorDialog1.ShowDialog() == DialogResult.OK)
         {
            BackColor = colorDialog1.Color;
         }
      }

      private void offsetSplit(int delta)
      {
         int offset = hexToInt(splitOffsetText.Text) + delta;
         offset = Math.Max(0, Math.Min(paletteData.Length - Math.Abs(delta), offset));
         splitOffsetText.Text = String.Format("{0:X}", offset);
      }

      private void splitMinusButton_Click(object sender, EventArgs e)
      {
         int delta = hexToInt(splitLengthText.Text);
         offsetSplit(-delta);
      }

      private void splitPlusButton_Click(object sender, EventArgs e)
      {
         int delta = hexToInt(splitLengthText.Text);
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
               separatePalette = true;
               UpdatePalette();
               paletteFileLabel.Text = Path.GetFileName(ofd.FileName);
            }
         }
      }

      private void advanceFile(int direction)
      {
         if (fileListView.Items.Count > 0)
         {
            if (fileListView.SelectedIndices.Count > 0)
            {
               int idx = fileListView.SelectedIndices[0];
               fileListView.Items[idx].Selected = false;
               idx += direction;
               if (idx < 0)
               {
                  idx += fileListView.Items.Count;
               }
               if (idx >= fileListView.Items.Count)
               {
                  idx -= fileListView.Items.Count;
               }
               fileListView.Items[idx].Selected = true;
            }
            else
            {
               fileListView.Items[0].Selected = true;
            }
         }
      }

      private void prevFileButton_Click(object sender, EventArgs e)
      {
         advanceFile(-1);
      }

      private void nextFileButton_Click(object sender, EventArgs e)
      {
         advanceFile(1);
      }

      private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (fileListView.SelectedItems.Count > 0)
         {
            string filePath = (string)fileListView.SelectedItems[0].Tag;
            readData(filePath);
         }
      }
   }
}
