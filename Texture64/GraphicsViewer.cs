using System.Drawing;
using System.Windows.Forms;

namespace Texture64
{
   public partial class GraphicsViewer : UserControl
   {
      private byte[] data;
      private byte[] palette;
      private int offset;

      public bool AutoPixelSize { get; set; }
      private Size myPixSize;
      public Size PixSize
      {
         get
         {
            return myPixSize;
         }
         set
         {
            myPixSize = value;
            this.Size = new Size(value.Width * PixScale, value.Height * PixScale);
         }
      }
      public int PixScale { get; set; }

      public N64Codec Codec { get; set; }

      public GraphicsViewer()
      {
         InitializeComponent();
         DoubleBuffered = true;
      }

      public void SetData(byte[] rawData)
      {
         data = rawData;
         Invalidate();
      }

      public void SetPalette(byte[] rawPalette)
      {
         palette = rawPalette;
         Invalidate();
      }

      public void SetOffset(int offset)
      {
         if (this.offset != offset)
         {
            this.offset = offset;
            Refresh();
         }
      }

      public int GetPixelHeight()
      {
         return AutoPixelSize ? Height / PixScale : PixSize.Height;
      }

      public int GetPixelWidth()
      {
         return AutoPixelSize ? Width / PixScale : PixSize.Width;
      }

      private void GraphicsViewer_Paint(object sender, PaintEventArgs e)
      {
         if (data != null)
         {
            N64Graphics.RenderTexture(e.Graphics, data, palette, offset, GetPixelWidth(), GetPixelHeight(), PixScale, Codec);
         }
         e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, GetPixelWidth() * PixScale - 1, GetPixelHeight() * PixScale - 1);
      }

      private void GraphicsViewer_Resize(object sender, System.EventArgs e)
      {
         Invalidate();
      }
   }

}
