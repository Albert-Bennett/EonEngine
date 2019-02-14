using System.Drawing;
using System.Windows.Forms;

namespace EonEngineTool.Lib.Controls
{
    /// <summary>
    /// Used to define a panel that has an image as it's background.
    /// </summary>
    public class ImageBox
    {
        protected FlowLayoutPanel parent;
        protected Panel imagePanel;
        protected Bitmap image;

        public Bitmap Image
        {
            get { return image; }
        }

        public ImageBox(FlowLayoutPanel parent, string filepath)
        {
            imagePanel = new Panel();

            try
            {
                image = new Bitmap(filepath);

                imagePanel.BackColor = Color.Transparent;
                imagePanel.BackgroundImage = image;
                imagePanel.BackgroundImageLayout = ImageLayout.Zoom;
                imagePanel.Size = parent.Size - new Size(6, 6);
            }
            catch { }

            parent.Controls.Add(imagePanel);
            this.parent = parent;
        }

        public virtual void ChangeImage(string filepath)
        {
            try
            {
                image = new Bitmap(filepath);
                imagePanel.BackgroundImage = image;
            }
            catch { }
        }

        public virtual FlowLayoutPanel Destroy()
        {
            parent.Controls.Remove(imagePanel);
            return parent;
        }
    }
}
