using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.UI
{
    internal class GuessLine
    {
        private const int k_MarginOfButtonFromLeft = -200;
        private const int k_ButtonMargin = 8;
        private const int k_MarginOfButtonFromTop = 100;
        private const int k_MarginBetweenButtonRows = 5;
        private const int k_NumberOfButtonsInARow = 4;
        private const bool k_IsButtonEnabled = true;
        private const string k_EnterGuessButtonText = "-->>";
        private const int k_EnterGuessButtonWidth = 40;
        private const int k_EnterGuessButtonHeight = 20;
        private const int k_HeightOffsetForFirstRowOfResultButtons = 0;
        private const int k_HeightOffsetForSecondRowOfResultButtons = 1;
        private const int k_EnterGuessButtonRightOffset = 4;
        private const int k_ResultButtonRightOffset = 10;
        private const int k_ResultButtonMargin = 20;
        private const int k_ResultButtonHeightOffset = 5;
        private const int k_SizeOffsetForResultButtons = 10;
        private const int k_SizeOffsetForEnterGuessButton = 4;

        private List<ColorButton> m_GuessButtons = new List<ColorButton>();

        public List<ColorButton> GuessButtons
        {
            get
            {
                return m_GuessButtons;
            }
        }
        
        private Button m_EnterGuess;

        public Button EnterGuess
        {
            get
            {
                return m_EnterGuess;
            }
        }

        private List<ResultButton> m_ResultButtons = new List<ResultButton>();

        public List<ResultButton> ResultButtons
        {
            get
            {
                return m_ResultButtons;
            }
        }

        public GuessLine(int i_RowOffset)
        {
            setGuessButtons(i_RowOffset);
            setEnterGuessButton();
            setResultButtons(i_RowOffset);
        }

        private void setEnterGuessButton()
        {
            m_EnterGuess = new Button();
            m_EnterGuess.Enabled = !k_IsButtonEnabled;
            m_EnterGuess.Text = k_EnterGuessButtonText;
            m_EnterGuess.Size = new Size(k_EnterGuessButtonWidth, k_EnterGuessButtonHeight);
            m_EnterGuess.Location = new Point(
                m_GuessButtons[m_GuessButtons.Count - 1].Right + k_EnterGuessButtonRightOffset, 
                m_GuessButtons[m_GuessButtons.Count - 1].Top + (m_GuessButtons[m_GuessButtons.Count - 1].Height / k_SizeOffsetForEnterGuessButton));
        }

        private void setResultButtons(int i_RowOffset)
        {
            m_ResultButtons = new List<ResultButton>();
            for (int i = 0; i < k_NumberOfButtonsInARow / 2; i++)
            {
                setRowOfResultButtons(i, k_HeightOffsetForFirstRowOfResultButtons);
            }

            for (int i = 0; i < k_NumberOfButtonsInARow / 2; i++)
            {
                setRowOfResultButtons(i, k_HeightOffsetForSecondRowOfResultButtons);
            }
        }

        private void setRowOfResultButtons(int i_NumberOfButtonInRow, int i_RowOffset)
        {
            ResultButton newButton = new ResultButton();
            int topOffset = m_GuessButtons[m_GuessButtons.Count - 1].Top + (m_GuessButtons[m_GuessButtons.Count - 1].Height / k_SizeOffsetForResultButtons)
                + (i_RowOffset * (newButton.Height + k_ResultButtonHeightOffset));
            newButton.Location = new Point(
                m_EnterGuess.Right + k_ResultButtonRightOffset + (i_NumberOfButtonInRow * k_ResultButtonMargin),
                topOffset);
            m_ResultButtons.Add(newButton);
        }

        private void setGuessButtons(int i_RowOffset)
        {
            m_GuessButtons = new List<ColorButton>();
            for (int i = 0; i < k_NumberOfButtonsInARow; i++)
            {
                ColorButton newButton = new ColorButton(Button.DefaultBackColor);
                newButton.Enabled = !k_IsButtonEnabled;
                newButton.Location = new Point(
                    k_MarginOfButtonFromLeft + (newButton.Width / 2) + (k_NumberOfButtonsInARow * newButton.Width) + (k_ButtonMargin * i) + (i * newButton.Width),
                    k_MarginOfButtonFromTop + (i_RowOffset * (newButton.Height + k_MarginBetweenButtonRows)));
                m_GuessButtons.Add(newButton);
            }
        }

        public void EnableGuessButtons()
        {
            foreach (ColorButton guessButton in m_GuessButtons)
            {
                guessButton.Enabled = k_IsButtonEnabled;
            }
        }

        public void EnableEnterGuessButton()
        {
            EnterGuess.Enabled = k_IsButtonEnabled;
        }

        public void DisableEnterGuessButton()
        {
            EnterGuess.Enabled = !k_IsButtonEnabled;
        }
        
        public bool CheckIfAllButtonsAreColored()
        {
            bool everyoneColored = true;
            foreach (ColorButton colorButton in m_GuessButtons)
            {
                if (colorButton.BackColor == Button.DefaultBackColor)
                {
                    everyoneColored = false;
                }
            }

            return everyoneColored;
        }
    }
}
