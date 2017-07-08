using System.Drawing;
using System.Windows.Forms;

namespace Ex05.UI
{
    internal class ColorButton : Button
    {
        private const int k_ButtonHeightAndWidth = 50;

        public ColorButton(Color i_ButtonColor) : base()
        {
            Size = new Size(k_ButtonHeightAndWidth, k_ButtonHeightAndWidth);
            BackColor = i_ButtonColor;
        }
    }
}
