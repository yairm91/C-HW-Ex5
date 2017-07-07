using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05.UI
{
    public class GameWindow : Form
    {
        private const int k_NumberOfButtonsInLine = 4;
        private const int k_MarginOfButtonFromLeft = -200;
        private const int k_ButtonMargin = 8;
        private const int k_MarginOfButtonFromTop = 20;
        private const int k_MarginBetweenButtonRows = 5;
        private const bool k_IsButtonEnabled = true;
        private const string k_GameWindowName = "Bool Pgia";
        private const int k_GameWindowWidth = 370;
        private const int k_GameWindowHeight = 150;
        private const int k_GameWindowHeightOffset = 55;
        private List<GuessLine> m_GuessLinesList;
        private List<ColorButton> m_CorrectGuessLine;
        private int m_CurrentGuessNumber;
        
        public GameWindow(int i_NumberOfGuesses)
        {
            m_CurrentGuessNumber = 0;
            Size = new Size(k_GameWindowWidth, k_GameWindowHeight + (i_NumberOfGuesses * k_GameWindowHeightOffset));
            StartPosition = FormStartPosition.CenterScreen;
            Text = k_GameWindowName;
            setListOfCorrectGuess();
            setListOfGuessLines(i_NumberOfGuesses);
        }

        private void setListOfGuessLines(int i_NumberOfGuesses)
        {
            m_GuessLinesList = new List<GuessLine>();
            for (int i = 0; i < i_NumberOfGuesses; i++)
            {
                GuessLine newGuessLine = new GuessLine(i);
                m_GuessLinesList.Add(newGuessLine);
                AddGuessLinesControlsToForm(newGuessLine);
            }
        }

        private void AddGuessLinesControlsToForm(GuessLine i_NewGuessLine)
        {
            Controls.Add(i_NewGuessLine.EnterGuess);

            addResultButtons(i_NewGuessLine);

            addColorButtons(i_NewGuessLine);
        }

        private void addColorButtons(GuessLine i_NewGuessLine)
        {
            foreach (ColorButton guessButton in i_NewGuessLine.GuessButtons)
            {
                Controls.Add(guessButton);
                guessButton.Click += new EventHandler(ColorButton_Click);
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorButton clickedButton = sender as ColorButton;
            ColorPicker colorPicker = new ColorPicker();
            colorPicker.ShowDialog();
            int chosenColorIndex = colorPicker.ChosenColor;
            if (chosenColorIndex != -1)
            {
                clickedButton.BackColor = ColorPicker.ColorsOfButtons[chosenColorIndex];
            }

            checkIfFullGuess(m_CurrentGuessNumber);
        }

        private void checkIfFullGuess(int i_LineOfGuessToCheck)
        {    
            if (m_GuessLinesList[i_LineOfGuessToCheck].CheckIfAllButtonsAreColored())
            {
                m_GuessLinesList[i_LineOfGuessToCheck].EnableEnterGuessButton();
            }
        }

        private void addResultButtons(GuessLine i_NewGuessLine)
        {
            foreach (ResultButton resultButton in i_NewGuessLine.ResultButtons)
            {
                Controls.Add(resultButton);
            }
        }

        private void setListOfCorrectGuess()
        {
            m_CorrectGuessLine = new List<ColorButton>();

            for (int i = 0; i < k_NumberOfButtonsInLine; i++)
            {
                ColorButton newButton = new ColorButton(Color.Black);
                newButton.Enabled = !k_IsButtonEnabled;
                newButton.Location = new Point(
                    k_MarginOfButtonFromLeft + (newButton.Width / 2) + (k_NumberOfButtonsInLine * newButton.Width) + (i * k_ButtonMargin) + (i * newButton.Width),
                    k_MarginOfButtonFromTop);
                m_CorrectGuessLine.Add(newButton);
                Controls.Add(newButton);
            }
        }

        public void EnableGuessLine(int i_LineNumber)
        {
            m_GuessLinesList[i_LineNumber].EnableGuessButtons();
        }

        public void EnableEnterGuessButtonPerLine(int i_LineNumber)
        {
            m_GuessLinesList[i_LineNumber].EnableEnterGuessButton();
        }
    }  
}
