using System;

class Program
{
    static void Main()
    {
        Console.Title = "WORD GUESSER";

        PrintBanner();

        string[] randomWords = { "ZANDER", "DESKTOP", "STRANGERS", "HONDA", "SKATEBOARD", "BLANKET" };
        Random rnd = new Random();

        string magicWord = randomWords[rnd.Next(randomWords.Length)];
        int lives = magicWord.Length; 
        bool[] revealed = new bool[magicWord.Length];

        Console.WriteLine("Info Before game starts, please read below:\n");
        Console.WriteLine($"- The word length is {magicWord.Length}.");
        Console.WriteLine("- Type the full word to guess.");
        Console.WriteLine("- Type 'hint' for a hint (costs 1 life).");
        Console.WriteLine("\n=============================================================================\n");

        while (lives > 0)
        {
            PrintHUD(magicWord, revealed, lives);

            Console.Write("Enter your answer: ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(input))
                continue;

            // HINT
            if (input.Equals("hint", StringComparison.OrdinalIgnoreCase))
            {
                lives--;

                if (AllRevealed(revealed))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("All letters are already revealed, hint wasted.");
                    Console.ResetColor();
                    continue;
                }

                RevealRandomLetter(magicWord, revealed, rnd);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Hint used. Lives remaining: {lives}");
                Console.ResetColor();

                continue;
            }

            
            if (input.Equals(magicWord, StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nCorrect! You guessed the word: {magicWord}");
                Console.WriteLine($"You had {lives} lives remaining.");
                Console.ResetColor();
                return;
            }

            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect guess. Try again.\n");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nGame Over! You ran out of lives.");
        Console.WriteLine($"The word was: {magicWord}");
        Console.ResetColor();
    }

    static void PrintBanner()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("__        __   ___   ____   ____     ____ _   _ _____ ____ ____  _____ ____  ");
        Console.WriteLine("\\ \\      / /  / _ \\ |  _ \\ |  _ \\   / ___| | | | ____/ ___/ ___|| ____|  _ \\ ");
        Console.WriteLine(" \\ \\ /\\ / /  | | | || |_) || | | | | |  _| | | |  _| \\___ \\___ \\|  _| | |_) |");
        Console.WriteLine("  \\ V  V /   | |_| ||  _ < | |_| | | |_| | |_| | |___ ___) |__) | |___|  _ < ");
        Console.WriteLine("   \\_/\\_/     \\___/ |_| \\_\\|____/   \\____|\\___/|_____|____/____/|_____|_| \\_\\");
        Console.WriteLine("=============================================================================\n");
        Console.ResetColor();
    }

    static void PrintHUD(string word, bool[] revealed, int lives)
    {
        Console.WriteLine("----- GAME STATUS -----");
        Console.Write("Word: ");

        for (int i = 0; i < word.Length; i++)
        {
            Console.Write(revealed[i] ? $"{word[i]} " : "_ ");
        }

        Console.WriteLine($"\nLives Left: {lives}");
        Console.WriteLine("------------------------\n");
    }

    static void RevealRandomLetter(string word, bool[] revealed, Random rnd)
    {
        int index;
        do
        {
            index = rnd.Next(word.Length);
        }
        while (revealed[index]);

        revealed[index] = true;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Hint: '{word[index]}' is at position {index}");
        Console.ResetColor();
    }

    static bool AllRevealed(bool[] revealed)
    {
        foreach (bool r in revealed)
        {
            if (!r) return false;
        }
        return true;
    }
}
