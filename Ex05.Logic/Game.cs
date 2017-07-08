using System;
using System.Collections.Generic;

namespace Ex05
{
    public class Game
    {
        private const char k_CorrectCharacterCorrectPlace = 'V';
        private const char k_CorrectCharacterIncorrectPlace = 'X';
        private const int k_CharacterNotFoundInGuess = -1;
        private const string k_WinningGuess = "VVVV";
        internal const int k_MaxLengthOfGuessWords = 4;
        public static readonly List<char> m_ComputerGuess;

        static Game()
        {
            List<char> computerGuess = createComputerGuess();
            m_ComputerGuess = computerGuess;
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

        public static List<char> GameRunner(List<char> i_GuessFromUser)
        {
           List<char> answerForTheUser = new List<char>();
           List<char> answerToTheUser = checkGuess(i_GuessFromUser, out answerForTheUser);
           return answerForTheUser;
        }

        private static List<char> checkGuess(List<char> i_CurrentGuess, out List<char> io_AnswerForTheUser)
        {
            io_AnswerForTheUser = new List<char>();

            checkCorrectCharactersCorrectPlaces(i_CurrentGuess, io_AnswerForTheUser);
            checkCorrectCharacterIncorrectPlaces(i_CurrentGuess, io_AnswerForTheUser);

            return io_AnswerForTheUser;
        }

        private static void checkCorrectCharacterIncorrectPlaces(List<char> i_CurrentGuess, List<char> io_AnswerForTheUser)
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

        private static void checkCorrectCharactersCorrectPlaces(List<char> i_CurrentGuess, List<char> io_AnswerForTheUser)
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
