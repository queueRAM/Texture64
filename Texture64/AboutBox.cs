using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Texture64.Properties;

namespace Texture64
{
   partial class AboutBox : Form
   {
      private int frame = 0;
      private readonly Bitmap[] frameImages = new Bitmap[] {
         Resources.flower_0,
         Resources.flower_1,
         Resources.flower_2,
         Resources.flower_3,
         Resources.flower_3,
         Resources.flower_2,
         Resources.flower_1,
         Resources.flower_0
      };
      public AboutBox()
      {
         InitializeComponent();
         this.Text = String.Format("About {0}", AssemblyTitle);
         this.labelProductName.Text = AssemblyProduct;
         this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
         this.labelCopyright.Text = AssemblyCopyright;
         this.textBoxDescription.Text = AssemblyDescription;
      }

      #region Assembly Attribute Accessors

      public string AssemblyTitle
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
               AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
               if (titleAttribute.Title != "")
               {
                  return titleAttribute.Title;
               }
            }
            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
         }
      }

      public string AssemblyVersion
      {
         get
         {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
         }
      }

      public string AssemblyDescription
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
         }
      }

      public string AssemblyProduct
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyProductAttribute)attributes[0]).Product;
         }
      }

      public string AssemblyCopyright
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
         }
      }

      public string AssemblyCompany
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
         }
      }
      #endregion

      private void timerAnimate_Tick(object sender, EventArgs e)
      {
         frame++;
         if (frame >= frameImages.Length)
         {
            frame = 0;
         }
         logoPictureBox.Image = frameImages[frame];
         logoPictureBox.Refresh();
      }
   }
}
