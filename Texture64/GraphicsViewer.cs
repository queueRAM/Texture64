using System.Drawing;
using System.Windows.Forms;

namespace Texture64
{
   public partial class GraphicsViewer : UserControl
   {
      private byte[] data;
      private byte[] palette;
      private int offset;

      public int PixWidth { get; set; }
      public int PixHeight { get; set; }
      public int PixScale { get; set; }
      public N64Codec Codec { get; set; }

      public GraphicsViewer()
      {
         InitializeComponent();
         DoubleBuffered = true;
         PixHeight = 0;
         PixWidth = 0;
         PixScale = 2;
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
         return (PixHeight > 0) ? PixHeight : Height / PixScale;
      }

      public int GetPixelWidth()
      {
         return (PixWidth > 0) ? PixWidth : Width / PixScale;
      }

      private void renderTexture(Graphics g, int scale)
      {
         N64Graphics.RenderTexture(g, data, palette, offset, GetPixelWidth(), GetPixelHeight(), scale, Codec);
      }

      private void GraphicsViewer_Paint(object sender, PaintEventArgs e)
      {
         if (data != null)
         {
            renderTexture(e.Graphics, PixScale);
         }
         e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, GetPixelWidth() * PixScale - 1, GetPixelHeight() * PixScale - 1);
      }

      private void GraphicsViewer_Resize(object sender, System.EventArgs e)
      {
         Invalidate();
      }
   }
}
