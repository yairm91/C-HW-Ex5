using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
    internal class Game
    {
        private const char k_CorrectCharacterCorrectPlace = 'V';
        private const char k_CorrectCharacterIncorrectPlace = 'X';
        private const int k_CharacterNotFoundInGuess = -1;
        private const string k_WinningGuess = "VVVV";
        internal const int k_MaxLengthOfGuessWords = 4;
        private List<char> m_ComputerGuess;
        private bool m_GameInProgress;

        private Game()
        {
            m_GameInProgress = true;
            setComputerGuess();
        }

        private static List<char> TranslateGuess(List<int> guessFromUI)
        {
            List<char> translatedGuessFromUI = new List<char>();
            foreach (int colorIndex in guessFromUI)
            {
                char tempTranslatedCharacter = (char)(colorIndex + 65);
                translatedGuessFromUI.Add(tempTranslatedCharacter);
            }

            return translatedGuessFromUI;
        }

        internal static string CastCharListToString(List<char> i_CharacterList)
        {
            StringBuilder stringFromList = new StringBuilder();
            foreach (char characterInList in i_CharacterList)
            {
                stringFromList.Append(characterInList);
            }

            return stringFromList.ToString();
        }

        private static List<char> createComputerGuess()
        {
            Random randomGenerator = new Random();
            List<char> computerGuess = new List<char>(k_MaxLengthOfGuessWords);

            for (int i = 0; i < k_MaxLengthOfGuessWords; i++)
            {
                char characterToAdd = (char)randomGenerator.Next('A', 'H' + 1);
                if (computerGuess.IndexOf(characterToAdd) == k_CharacterNotFoundInGuess)
                {
                    computerGuess.Add(characterToAdd);
                }
                else
                {
                    i--;
                }
            }

            return computerGuess;
        }

        private static void gameRunner(Game i_MyGame)
        {
           List<char> currentGuess; //toDO in controller
           List<char> answerForTheUser = new List<char>();
           List<char> answerToTheUser = i_MyGame.checkGuess(currentGuess, out answerForTheUser);
           //Send Guess to Controller
        }

        private void setComputerGuess()
        {
            List<char> computerGuess = createComputerGuess();
            m_ComputerGuess = computerGuess;
        }

        private List<char> checkGuess(List<char> i_CurrentGuess, out List<char> io_AnswerForTheUser)
        {
            io_AnswerForTheUser = new List<char>();

            checkCorrectCharactersCorrectPlaces(i_CurrentGuess, io_AnswerForTheUser);
            checkCorrectCharacterIncorrectPlaces(i_CurrentGuess, io_AnswerForTheUser);

            return io_AnswerForTheUser;
        }

        private void checkCorrectCharacterIncorrectPlaces(List<char> i_CurrentGuess, List<char> io_AnswerForTheUser)
        {
            for (int indexOfCharacterInGuess = 0; indexOfCharacterInGuess < k_MaxLengthOfGuessWords; indexOfCharacterInGuess++)
            {
                if (m_ComputerGuess.IndexOf(i_CurrentGuess[indexOfCharacterInGuess]) != k_CharacterNotFoundInGuess
                    && m_ComputerGuess.IndexOf(i_CurrentGuess[indexOfCharacterInGuess]) != indexOfCharacterInGuess)
                {
                    io_AnswerForTheUser.Add(k_CorrectCharacterIncorrectPlace);
                }
            }
        }

        private void checkCorrectCharactersCorrectPlaces(List<char> i_CurrentGuess, List<char> io_AnswerForTheUser)
        {
            for (int indexOfCharacterInGuess = 0; indexOfCharacterInGuess < k_MaxLengthOfGuessWords; indexOfCharacterInGuess++)
            {
                if (i_CurrentGuess[indexOfCharacterInGuess].Equals(m_ComputerGuess[indexOfCharacterInGuess]))
                {
                    io_AnswerForTheUser.Add(k_CorrectCharacterCorrectPlace);
                }
            }
        }
    }
}
