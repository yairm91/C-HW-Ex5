using System.Windows.Forms;
using System.Drawing;

namespace Ex05.UI
{
    internal class ResultButton : Button
    {
        private const int k_ResultButtonWidthAndHeight = 15;
        private const bool k_ButtonIsEnabled = true;

        public ResultButton() : base()
        {
            Size = new Size(k_ResultButtonWidthAndHeight, k_ResultButtonWidthAndHeight);
            Enabled = !k_ButtonIsEnabled;
        }
    }
}