using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolProjectGui
{
    public partial class CustomDataGridView : DataGridView
    {

        private const int CAPTIONHEIGHT = 21;
        private const int BORDERWIDTH = 2;

        public CustomDataGridView()
        {
            InitializeComponent();
            VerticalScrollBar.Visible = true;
            VerticalScrollBar.VisibleChanged += new EventHandler(ShowScrollBars);
        }

        private void ShowScrollBars(object sender, EventArgs e)
        {
            if (!VerticalScrollBar.Visible)
            {
                int width = VerticalScrollBar.Width;

                VerticalScrollBar.Location =
                new Point(ClientRectangle.Width - width - BORDERWIDTH, CAPTIONHEIGHT);

                VerticalScrollBar.Size =
                new Size(width, ClientRectangle.Height - CAPTIONHEIGHT - BORDERWIDTH);

                VerticalScrollBar.Show();
            }
        }
    }
}