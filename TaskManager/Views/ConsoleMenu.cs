namespace TaskManager;

public class ConsoleMenu
{
    private int SelectedIndex;
    private string[] Options;
    private string Prompt;

    public ConsoleMenu(string prompt, string[] options)
    {
        Prompt = prompt;
        Options = options;
        SelectedIndex = 0;
    }

    private void DisplayOptions()
    {
        Console.WriteLine(Prompt + "\n");
        for (int i = 0; i < Options.Length; i++)
        {
            string currentOption = Options[i];
            if (i == SelectedIndex)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Black;
                System.Console.BackgroundColor = System.ConsoleColor.White;
            }
            else
            {
                System.Console.ForegroundColor = System.ConsoleColor.White;
                System.Console.BackgroundColor = System.ConsoleColor.Black;
            }
            Console.WriteLine($"{currentOption}");
        }

        Console.WriteLine();
        System.Console.ResetColor();
    }

    public int GetNumOfOptions()
    {
        return Options.Length;
    }

    public int Run()
    {
        ConsoleKey keyPressed;
        do
        {
            Console.Clear();
            DisplayOptions();
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = Options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                SelectedIndex++;
                if (SelectedIndex == Options.Length)
                {
                    SelectedIndex = 0;
                }
            }
            
        } while (keyPressed != ConsoleKey.Enter);

        return SelectedIndex;
    }
}