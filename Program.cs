using System;
using System.Security.Cryptography;

namespace RockPaperScissors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var validator = new ArgumentValidator(args);
            if (!validator.Validate())
            {
                foreach (string claim in validator.Claims)
                {
                    Console.WriteLine(claim);
                }
                return;
            }

            int computerChoice = RandomNumberGenerator.GetInt32(args.Length);
            string key, hash;
            using (var generator = new HashGenerator())
            {
                (key, hash) = generator.ComputeHash(args[computerChoice]);
            }

            var menu = new GameMenu(args);
            menu.Show($"HMAC hex:  {hash}");
            menu.Build();

            int playerChoice;
            while(!menu.TryGetPlayerChoice(out playerChoice))
            {
                menu.Build();
            }

            if (playerChoice == 0)
            {
                menu.Show("Exit");
                return;
            }

            playerChoice--;
            menu.Show($"Your choice: {args[playerChoice]}");
            menu.Show($"Computer choice: {args[computerChoice]}");

            var logic = new GameLogic(args);
            logic.ComputerWon += () => menu.Show("Computer won");
            logic.PlayerWon += () => menu.Show("Player won");
            logic.Draw += () => menu.Show("Draw");

            logic.SetPlayerChoice(args[playerChoice]).SetComputerChoice(args[computerChoice]).ChooseWinner();

            menu.Show($"Key hex:  {key}");
        }
    }
}
