using System;
using System.Collections.Generic;
using System.Drawing;
using Ex05.UI;

namespace Ex05.Controller
{
    public class Program
    {
        private const char k_CorrectCharacterCorrectPlace = 'V';
        private const int k_IntegerToCharOffset = 65;
        private const int k_FirstGuessLineIndex = 0;

        public static void Main()
        {
            playGame();
        }

        private static void playGame()
        {
            OpeningScreen myOpeningScreen = new OpeningScreen();
            myOpeningScreen.ShowDialog();
            int numberOfGuesses = myOpeningScreen.NumOfChance;
            GameWindow myGameWindow = new GameWindow(numberOfGuesses);
            myGameWindow.EnableGuessLine(k_FirstGuessLineIndex);
            myGameWindow.ShowDialog();
        }

        private static List<char> translateGuess(List<int> i_GuessFromUI)
        {
            List<char> translatedGuessFromUI = new List<char>();
            foreach (int colorIndex in i_GuessFromUI)
            {
                char tempTranslatedCharacter = (char)(colorIndex + k_IntegerToCharOffset);
                translatedGuessFromUI.Add(tempTranslatedCharacter);
            }

            return translatedGuessFromUI;
        }

        private static List<Color> translateAnswer(List<char> i_AnswerFromLogic)
        {
            List<Color> translatedAnswerFromLogic = new List<Color>();
            foreach (char characterInAnswerFromLogic in i_AnswerFromLogic)
            {
                if (characterInAnswerFromLogic == k_CorrectCharacterCorrectPlace)
                {
                    translatedAnswerFromLogic.Add(Color.Black);
                }
                else
                {
                    translatedAnswerFromLogic.Add(Color.Yellow);
                }
            }

            return translatedAnswerFromLogic;
        }

        private static List<int> translateComputerGuess(List<char> i_ComputerGuess)
        {
            List<int> translateComputerGuess = new List<int>();
            foreach (char characterInAnswerFromLogic in i_ComputerGuess)
            {
                int tempTranslatedCharacter = (int)characterInAnswerFromLogic - k_IntegerToCharOffset;
                translateComputerGuess.Add(tempTranslatedCharacter);
            }

            return translateComputerGuess;
        }

        public static List<Color> CheckGuess(List<int> i_GuessFromUI)
        {
            List<char> translatedGuess = translateGuess(i_GuessFromUI);
            List<char> answerFromLogic = Game.GameRunner(translatedGuess);
            List<Color> translatedAnswer = translateAnswer(answerFromLogic);
            return translatedAnswer;
        }

        public static List<int> GetComputerGuess()
        {
            List<int> translatedComputerGuess = translateComputerGuess(Game.m_ComputerGuess);
            return translatedComputerGuess;
        }
    }
}
