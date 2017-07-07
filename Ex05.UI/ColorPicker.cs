using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.UI
{
    public class ColorPicker : Form
    {    
        private const int k_ColorPickerWidth = 285;
        private const int k_ColorPickerHeight = 170;
        private const int k_NumberOfRows = 2;
        private const string k_ColorPickerScreenName = "Pick A Color: ";
        private const int k_MarginOfButtonFromLeft = -8;
        private const int k_ButtonMargin = 12;
        private const int k_MarginOfButtonFromTop = 15;
        private const int k_MarginBetweenButtonRows = 5;
        internal static readonly Color[] ColorsOfButtons = { Color.Purple, Color.Red, Color.Green, Color.SkyBlue, Color.Blue, Color.Yellow, Color.SaddleBrown, Color.White };
        private int m_ChosenColor = -1;

        public int ChosenColor
        {
            get
            {
                return m_ChosenColor;
            }
        }

        public ColorPicker()
        {
            Size = new Size(k_ColorPickerWidth, k_ColorPickerHeight);
            StartPosition = FormStartPosition.CenterScreen;
            Text = k_ColorPickerScreenName;
            
            SetRow(0);
            SetRow(1);
        }

        private void SetRow(int i_RowOffset)
        {
            int numberOfButtonsInARow = ColorsOfButtons.Length / k_NumberOfRows;
            for (int numberOfButtonInTheRow = 0; numberOfButtonInTheRow < numberOfButtonsInARow; numberOfButtonInTheRow++)
            {
                ColorButton newButton = new ColorButton(ColorsOfButtons[numberOfButtonInTheRow + (i_RowOffset * numberOfButtonsInARow)]);
                newButton.Location = new Point(
                    k_MarginOfButtonFromLeft + (newButton.Width / 2) + (numberOfButtonInTheRow * newButton.Width) + (numberOfButtonInTheRow * k_ButtonMargin),
                    k_MarginOfButtonFromTop + (i_RowOffset * (newButton.Height + k_MarginBetweenButtonRows)));

                Controls.Add(newButton);

                newButton.Click += new EventHandler(ColorButton_Click);
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorButton clickedButton = sender as ColorButton;
            m_ChosenColor = Array.IndexOf(ColorsOfButtons, clickedButton.BackColor);
            Close();
        }
    }
}
