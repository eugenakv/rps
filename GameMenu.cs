using System;

namespace RockPaperScissors
{
    public class GameMenu
    {
        private readonly string[] variants;

        public GameMenu(string[] variants)
        {
            this.variants = variants ?? throw new ArgumentNullException(nameof(variants));
        }
        
        public void Build()
        {
            int i = 1;
            Console.WriteLine("Available moves: ");
            foreach (var variant in variants)
            {
                Console.WriteLine($"{i} - {variant}");
                i++;
            }
            Console.WriteLine("0 - [exit]");
        }

        public void Show(string text)
        {
            Console.WriteLine(text);
        }

        public bool TryGetPlayerChoice(out int choice)
        {
            Console.Write("move> ");
            return int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= variants.Length;
        }
    }
}