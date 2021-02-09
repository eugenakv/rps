using System;

namespace RockPaperScissors
{
    public class GameLogic
    {
        private readonly string[] variants;

        public event Action PlayerWon;
        public event Action ComputerWon;
        public event Action Draw;

        public string PlayerChoice { get; set; }
        public string ComputerChoice { get; set; }

        public GameLogic(string[] variants)
        {
            this.variants = variants ?? throw new ArgumentNullException(nameof(variants));
        }

        public GameLogic SetPlayerChoice(string choice)
        {
            PlayerChoice = choice;
            return this;
        }

        public GameLogic SetComputerChoice(string choice)
        {
            ComputerChoice = choice;
            return this;
        }

        public void ChooseWinner()
        {
            if (PlayerChoice == ComputerChoice)
            {
                Draw?.Invoke();
            }
            else
            {
                int distance = variants.IndexOf(PlayerChoice) - variants.IndexOf(ComputerChoice);
                double halfLength = (double)(variants.Length - 1) / 2;
                if (CheckVictoryCriterion(distance, halfLength))
                {
                    ComputerWon?.Invoke();
                }
                else
                {
                    PlayerWon?.Invoke();
                }
            }
        }

        private static bool CheckVictoryCriterion(int distance, double halfLength)
        {
            return (distance < 0 && Math.Abs(distance) <= halfLength) || (distance > 0 && distance > halfLength);
        }
    }
}