using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.UI
{
    public class OpeningScreen : Form
    {
        private const string k_OpeningScreenName = "Bool Pgia";
        private const string k_StartButtonName = "Start";
        private const string k_NumberOfChancesButtonName = "Number Of Chances: {0}";
        private const int k_StartGameButtonLocationWidthOffset = 40;
        private const int k_StartGameButtonLocationHeightOffset = 10;
        private const int k_NumberOfChancesButtonWidthOffset = 40;
        private const int k_NumberOfChancesButtonLocationWidthOffset = 295;
        private const int k_NumberOfChancesButtonLocationHeightOffset = 10;
        private const int k_OpeningScreenWidth = 370;
        private const int k_OpeningScreenHeight = 180;

        private Button m_NumOfChancesButton;

        private Button m_StartGameButton;

        private int m_NumOfChances;

        public int NumOfChange
        {
            get
            {
                return m_NumOfChances;
            }

            set
            {
                m_NumOfChances = value;
            }
        }

        public OpeningScreen()
        {
            m_NumOfChancesButton = new Button();
            m_StartGameButton = new Button();
            m_NumOfChances = 4;

            Size = new Size(k_OpeningScreenWidth, k_OpeningScreenHeight);
            StartPosition = FormStartPosition.CenterScreen;
            Text = k_OpeningScreenName;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            m_StartGameButton.Text = k_StartButtonName;
            m_StartGameButton.Location = new Point(
                Width - m_StartGameButton.Width - k_StartGameButtonLocationWidthOffset,
                ClientSize.Height - m_StartGameButton.Height - k_StartGameButtonLocationHeightOffset);

            setNumOfChancesButtonText();
            m_NumOfChancesButton.Width = ClientSize.Width - k_NumberOfChancesButtonWidthOffset;
            m_NumOfChancesButton.Location = new Point(
                m_NumOfChancesButton.Width - k_NumberOfChancesButtonLocationWidthOffset,
                 m_NumOfChancesButton.Height - k_NumberOfChancesButtonLocationHeightOffset);

            Controls.AddRange(new Control[] { m_StartGameButton, m_NumOfChancesButton });

            m_NumOfChancesButton.Click += new EventHandler(m_NumOfChancesButton_Click);
            m_StartGameButton.Click += new EventHandler(m_StartGameButton_Click);
        }

        private void m_StartGameButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void m_NumOfChancesButton_Click(object sender, EventArgs e)
        {
            int tempNumOfChances = m_NumOfChances + 1;
            // TODO: Chance this Shite
            if (tempNumOfChances <= 10)
            {
                m_NumOfChances = tempNumOfChances;
                setNumOfChancesButtonText();
            }
        }
          
        private void setNumOfChancesButtonText()
        {
            m_NumOfChancesButton.Text = string.Format(k_NumberOfChancesButtonName, m_NumOfChances);
        }
    }
}
