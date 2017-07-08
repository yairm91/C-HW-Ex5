using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Ex05.Controller;

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
                addGuessLinesControlsToForm(newGuessLine);
            }
        }

        private void addGuessLinesControlsToForm(GuessLine i_NewGuessLine)
        {
            Controls.Add(i_NewGuessLine.EnterGuess);
            i_NewGuessLine.EnterGuess.Click += new EventHandler(enterGuess_Click);

            addResultButtons(i_NewGuessLine);

            addColorButtons(i_NewGuessLine);
        }

        private void enterGuess_Click(object sender, EventArgs e)
        {
            List<int> guess = getGuess();
            List<Color> answerForTheUser = Program.CheckGuess(guess);
            updateGameStatus(answerForTheUser);
        }

        private void updateGameStatus(List<Color> answerForTheUser)
        {
            int countOfBulls = colorResultButtons(answerForTheUser);
            if (countOfBulls == 4)
            {
                colorCorrectGuessButtons();
            }
            else
            {
                updateGame();
            }
        }

        private void updateGame()
        {
            disableEnterGuessButtonPerLine(m_CurrentGuessNumber);
            int tempCurrentGuessNumber = m_CurrentGuessNumber + 1;
            if (m_GuessLinesList.Count <= tempCurrentGuessNumber)
            {
                colorCorrectGuessButtons();
            }
            else
            {
                m_CurrentGuessNumber = tempCurrentGuessNumber;
                EnableGuessLine(m_CurrentGuessNumber);
            }
        }

        private void colorCorrectGuessButtons()
        {
            List<int> computerGuess = Program.GetComputerGuess();
            List<Color> translatedComputerGuess = translateComputerGuess(computerGuess);
            for (int i = 0; i < translatedComputerGuess.Count; i++)
            {
                m_CorrectGuessLine[i].BackColor = translatedComputerGuess[i];
            }
        }

        private List<Color> translateComputerGuess(List<int> i_ComputerGuess)
        {
            List<Color> translatedComputerGuess = new List<Color>();
            foreach (int computerGuessCharacter in i_ComputerGuess)
            {
                translatedComputerGuess.Add(ColorPicker.ColorsOfButtons[computerGuessCharacter]);
            }

            return translatedComputerGuess;
        }

        private int colorResultButtons(List<Color> i_AnswerForTheUser)
        {
            int counterOfBulls = 0;
            for (int i = 0; i < i_AnswerForTheUser.Count; i++)
            {
                m_GuessLinesList[m_CurrentGuessNumber].ResultButtons[i].BackColor = i_AnswerForTheUser[i];
                if (i_AnswerForTheUser[i] == Color.Black)
                {
                    counterOfBulls++;
                }
            }

            return counterOfBulls;
        }

        private List<int> getGuess()
        {
            List<int> guess = new List<int>();
            foreach (ColorButton guessButton in m_GuessLinesList[m_CurrentGuessNumber].GuessButtons)
            {
                guess.Add(Array.IndexOf(ColorPicker.ColorsOfButtons, guessButton.BackColor));
            }

            return guess;
        }

        private void addColorButtons(GuessLine i_NewGuessLine)
        {
            foreach (ColorButton guessButton in i_NewGuessLine.GuessButtons)
            {
                Controls.Add(guessButton);
                guessButton.Click += new EventHandler(colorButton_Click);
            }
        }

        private void colorButton_Click(object sender, EventArgs eventArgs)
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

        private void enableEnterGuessButtonPerLine(int i_LineNumber)
        {
            m_GuessLinesList[i_LineNumber].EnableEnterGuessButton();
        }

        private void disableEnterGuessButtonPerLine(int i_LineNumber)
        {
            m_GuessLinesList[i_LineNumber].DisableEnterGuessButton();
        }
    }  
}
